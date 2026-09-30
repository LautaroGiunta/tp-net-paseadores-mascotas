using API.Clients;
using DTOs;

namespace Paseadores.WinForms
{
    // CRUD Maestro/Detalle: la cabecera (paseador + período) y sus líneas (paseos)
    // se arman juntas en pantalla y se mandan a la API en un solo guardado.
    public partial class FormLiquidaciones : Form
    {
        private int idLiquidacionSeleccionada = 0;
        private List<LiquidacionDTO> liquidacionesCargadas = new List<LiquidacionDTO>();
        private List<PaseadorDTO> paseadores = new List<PaseadorDTO>();

        // Las dos grillas de abajo usan el mismo tipo: pasar un paseo de una a otra es mover la línea
        private List<LiquidacionDetalleDTO> pendientes = new List<LiquidacionDetalleDTO>();
        private List<LiquidacionDetalleDTO> detalle = new List<LiquidacionDetalleDTO>();

        // Paseador con el que se armó el detalle: si se cambia, las líneas ya no corresponden
        private int? paseadorDelDetalle = null;

        public FormLiquidaciones()
        {
            InitializeComponent();
        }

        private async void FormLiquidaciones_Load(object sender, EventArgs e)
        {
            dgvLiquidaciones.AutoGenerateColumns = false;
            dgvPendientes.AutoGenerateColumns = false;
            dgvDetalle.AutoGenerateColumns = false;
            LimpiarFormulario();
            await CargarPaseadoresAsync();
            await CargarLiquidacionesAsync();
        }

        private async Task CargarPaseadoresAsync()
        {
            try
            {
                paseadores = await ApiClient.Paseadores.GetAllAsync();

                cmbPaseador.DataSource = paseadores
                    .Select(p => new { p.Id, Descripcion = p.Apellido + ", " + p.Nombre + " (" + p.Zona + ")" })
                    .ToList();
                cmbPaseador.DisplayMember = "Descripcion";
                cmbPaseador.ValueMember = "Id";
                cmbPaseador.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo conectar con la API. ¿Está encendida?\n\nError: " + ex.Message,
                    "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task CargarLiquidacionesAsync()
        {
            try
            {
                liquidacionesCargadas = await ApiClient.Liquidaciones.GetAllAsync();
                dgvLiquidaciones.DataSource = liquidacionesCargadas.Select(l => new
                {
                    l.Id,
                    Paseador = l.PaseadorNombre,
                    Desde = l.FechaDesde.ToString("dd/MM/yyyy"),
                    Hasta = l.FechaHasta.ToString("dd/MM/yyyy"),
                    Paseos = l.Detalles.Count,
                    l.Total
                }).ToList();
                dgvLiquidaciones.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo conectar con la API. ¿Está encendida?\n\nError: " + ex.Message,
                    "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarLineas()
        {
            dgvPendientes.DataSource = pendientes.OrderBy(d => d.FechaHoraInicio).Select(ALinea).ToList();
            dgvDetalle.DataSource = detalle.OrderBy(d => d.FechaHoraInicio).Select(ALinea).ToList();
            dgvPendientes.ClearSelection();
            dgvDetalle.ClearSelection();

            lblTotal.Text = "Total: $" + detalle.Sum(d => d.Importe).ToString("N2");
        }

        private static object ALinea(LiquidacionDetalleDTO d)
        {
            return new
            {
                d.PaseoId,
                Fecha = d.FechaHoraInicio.ToString("dd/MM/yyyy HH:mm"),
                Perro = d.PerroNombre,
                Minutos = d.DuracionMinutos,
                d.Importe
            };
        }

        private async void btnBuscarPaseos_Click(object sender, EventArgs e)
        {
            if (cmbPaseador.SelectedValue == null)
            {
                MessageBox.Show("Seleccioná un paseador.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dtpDesde.Value.Date > dtpHasta.Value.Date)
            {
                MessageBox.Show("La fecha desde no puede ser posterior a la fecha hasta.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int paseadorId = Convert.ToInt32(cmbPaseador.SelectedValue);

            if (detalle.Count > 0 && paseadorDelDetalle != paseadorId)
            {
                DialogResult respuesta = MessageBox.Show(
                    "El detalle tiene paseos de otro paseador. ¿Querés vaciarlo y empezar de nuevo?",
                    "Cambio de paseador", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (respuesta != DialogResult.Yes)
                    return;

                detalle.Clear();
            }

            paseadorDelDetalle = paseadorId;
            await CargarPendientesAsync(paseadorId, avisarSiNoHay: true);
        }

        private async Task CargarPendientesAsync(int paseadorId, bool avisarSiNoHay)
        {
            try
            {
                var paseos = await ApiClient.Liquidaciones.GetPaseosPendientesAsync(paseadorId, dtpDesde.Value.Date, dtpHasta.Value.Date);

                // Los que ya están en el detalle (y todavía no se guardaron) no se repiten
                pendientes = paseos
                    .Where(p => !detalle.Any(d => d.PaseoId == p.Id))
                    .Select(p => new LiquidacionDetalleDTO
                    {
                        PaseoId = p.Id,
                        FechaHoraInicio = p.FechaHoraInicio,
                        DuracionMinutos = p.DuracionMinutos,
                        PerroNombre = p.PerroNombre,
                        Importe = p.PrecioTotal
                    })
                    .ToList();

                MostrarLineas();

                if (avisarSiNoHay && pendientes.Count == 0)
                {
                    MessageBox.Show("Ese paseador no tiene paseos terminados sin liquidar en el período.", "Sin paseos",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (ApiException ex)
            {
                MessageBox.Show("Error al buscar los paseos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hubo un error al intentar conectar:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            MoverSeleccionados(dgvPendientes, colPendPaseoId.Name, pendientes, detalle);
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            MoverSeleccionados(dgvDetalle, colDetPaseoId.Name, detalle, pendientes);
        }

        private void dgvPendientes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                MoverSeleccionados(dgvPendientes, colPendPaseoId.Name, pendientes, detalle);
        }

        private void dgvDetalle_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                MoverSeleccionados(dgvDetalle, colDetPaseoId.Name, detalle, pendientes);
        }

        private void MoverSeleccionados(DataGridView grilla, string columnaId,
                                        List<LiquidacionDetalleDTO> origen, List<LiquidacionDetalleDTO> destino)
        {
            var ids = grilla.SelectedRows.Cast<DataGridViewRow>()
                .Select(r => Convert.ToInt32(r.Cells[columnaId].Value))
                .ToList();

            if (ids.Count == 0)
            {
                MessageBox.Show("Seleccioná uno o más paseos de la grilla.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            foreach (var linea in origen.Where(d => ids.Contains(d.PaseoId)).ToList())
            {
                origen.Remove(linea);
                destino.Add(linea);
            }

            MostrarLineas();
        }

        // --- Validación a nivel UI: la API y el dominio vuelven a validar todo ---
        private bool EsValido()
        {
            if (cmbPaseador.SelectedValue == null)
            {
                MessageBox.Show("Seleccioná un paseador.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (dtpDesde.Value.Date > dtpHasta.Value.Date)
            {
                MessageBox.Show("La fecha desde no puede ser posterior a la fecha hasta.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (detalle.Count == 0)
            {
                MessageBox.Show("Agregá al menos un paseo al detalle.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (paseadorDelDetalle != Convert.ToInt32(cmbPaseador.SelectedValue))
            {
                MessageBox.Show("Cambiaste el paseador: volvé a buscar sus paseos antes de guardar.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (detalle.Any(d => d.FechaHoraInicio.Date < dtpDesde.Value.Date || d.FechaHoraInicio.Date > dtpHasta.Value.Date))
            {
                MessageBox.Show("Hay paseos en el detalle que quedan fuera del período. Quitálos o ampliá las fechas.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!EsValido())
                return;

            // Cabecera y detalle viajan juntos; de cada línea alcanza con el paseo,
            // el importe lo congela la API con el precio del paseo
            var liquidacion = new LiquidacionDTO
            {
                Id = idLiquidacionSeleccionada,
                PaseadorId = Convert.ToInt32(cmbPaseador.SelectedValue),
                FechaDesde = dtpDesde.Value.Date,
                FechaHasta = dtpHasta.Value.Date,
                Detalles = detalle.Select(d => new LiquidacionDetalleDTO { PaseoId = d.PaseoId }).ToList()
            };

            try
            {
                if (idLiquidacionSeleccionada == 0)
                    await ApiClient.Liquidaciones.AddAsync(liquidacion);
                else
                    await ApiClient.Liquidaciones.UpdateAsync(liquidacion);

                MessageBox.Show("¡Liquidación guardada con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarFormulario();
                await CargarLiquidacionesAsync();
            }
            catch (ApiException ex)
            {
                MessageBox.Show("La API rechazó el guardado.\n\nDetalles:\n" + ex.Message, "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error inesperado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvLiquidaciones.CurrentRow == null)
            {
                MessageBox.Show("Por favor, seleccioná una liquidación de la grilla primero.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult respuesta = MessageBox.Show(
                "¿Estás seguro de que querés eliminar esta liquidación?\nSus paseos vuelven a quedar pendientes de liquidar.",
                "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                try
                {
                    int idSeleccionado = Convert.ToInt32(dgvLiquidaciones.CurrentRow.Cells["colId"].Value);
                    await ApiClient.Liquidaciones.DeleteAsync(idSeleccionado);

                    MessageBox.Show("Liquidación eliminada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarFormulario();
                    await CargarLiquidacionesAsync();
                }
                catch (ApiException ex)
                {
                    MessageBox.Show("La API rechazó la eliminación.\n\nDetalles:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hubo un error al intentar conectar:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnNueva_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            dgvLiquidaciones.ClearSelection();
        }

        private void LimpiarFormulario()
        {
            idLiquidacionSeleccionada = 0;
            paseadorDelDetalle = null;
            grpCabecera.Text = "Nueva liquidación";
            cmbPaseador.SelectedIndex = -1;

            // Por defecto, el mes pasado completo
            DateTime inicioMesActual = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            dtpDesde.Value = inicioMesActual.AddMonths(-1);
            dtpHasta.Value = inicioMesActual.AddDays(-1);

            pendientes.Clear();
            detalle.Clear();
            MostrarLineas();
        }

        private async void dgvLiquidaciones_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int id = Convert.ToInt32(dgvLiquidaciones.Rows[e.RowIndex].Cells["colId"].Value);
            LiquidacionDTO? liquidacion = liquidacionesCargadas.FirstOrDefault(l => l.Id == id);
            if (liquidacion == null) return;

            idLiquidacionSeleccionada = liquidacion.Id;
            paseadorDelDetalle = liquidacion.PaseadorId;
            grpCabecera.Text = $"Editando liquidación N° {liquidacion.Id}";
            cmbPaseador.SelectedValue = liquidacion.PaseadorId;
            dtpDesde.Value = liquidacion.FechaDesde;
            dtpHasta.Value = liquidacion.FechaHasta;

            detalle = liquidacion.Detalles.ToList();

            // También se muestran los paseos que todavía se le podrían sumar
            await CargarPendientesAsync(liquidacion.PaseadorId, avisarSiNoHay: false);
        }
    }
}
