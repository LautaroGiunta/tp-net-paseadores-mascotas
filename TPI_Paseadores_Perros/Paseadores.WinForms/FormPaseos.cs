using API.Clients;
using DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paseadores.WinForms
{
    public partial class FormPaseos : Form
    {
        private int idPaseoSeleccionado = 0;
        private List<PaseoDTO> paseosCargados = new List<PaseoDTO>();
        private List<PaseadorDTO> paseadores = new List<PaseadorDTO>();
        private List<PerroDTO> perros = new List<PerroDTO>();

        // El dueño solo ve los paseos de sus perros y el paseador solo los suyos
        // (la API igual rechaza con 403 si alguno toca algo ajeno)
        private readonly UsuarioDTO usuario;
        private bool EsDueno => usuario.Rol == "Dueno";
        private bool EsPaseador => usuario.Rol == "Paseador";


        public FormPaseos(UsuarioDTO usuarioLogueado)
        {
            InitializeComponent();
            usuario = usuarioLogueado;
        }

        private async void FormPaseos_Load(object sender, EventArgs e)
        {
            dgvPaseos.AutoGenerateColumns = false;
            dtpFechaHora.Value = DateTime.Now.AddHours(1);
            dtpDesde.Value = DateTime.Today;
            dtpHasta.Value = DateTime.Today.AddMonths(1);
            await CargarCombosAsync();
            await CargarPaseosAsync();
        }

        // Paseadores y perros se eligen de una lista para no tener que escribir Ids
        private async Task CargarCombosAsync()
        {
            try
            {
                paseadores = await ApiClient.Paseadores.GetAllAsync();
                perros = EsDueno
                    ? await ApiClient.Perros.GetByDuenoAsync(usuario.Id)
                    : await ApiClient.Perros.GetAllAsync();

                cmbPaseador.DataSource = paseadores
                    .Select(p => new { p.Id, Descripcion = p.Apellido + ", " + p.Nombre + " (" + p.Zona + ")" })
                    .ToList();
                cmbPaseador.DisplayMember = "Descripcion";
                cmbPaseador.ValueMember = "Id";
                cmbPaseador.SelectedIndex = -1;

                cmbPerro.DataSource = perros
                    .Select(p => new { p.Id, Descripcion = p.Nombre + " (" + p.Raza + ")" })
                    .ToList();
                cmbPerro.DisplayMember = "Descripcion";
                cmbPerro.ValueMember = "Id";
                cmbPerro.SelectedIndex = -1;

                // El filtro de busqueda usa la misma lista de paseadores
                cmbFiltroPaseador.DataSource = paseadores
                    .Select(p => new { p.Id, Descripcion = p.Apellido + ", " + p.Nombre })
                    .ToList();
                cmbFiltroPaseador.DisplayMember = "Descripcion";
                cmbFiltroPaseador.ValueMember = "Id";
                cmbFiltroPaseador.SelectedIndex = -1;
                FijarPaseadorSiCorresponde();
            }
            catch (ApiException ex) when (ex.StatusCode == 401)
            {
                ApiClient.SesionVencida();
            }
            catch (ApiException ex)
            {
                // La API respondió, pero con un error
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (HttpRequestException ex)
            {
                // No hubo respuesta: la API está apagada o la URL está mal
                MessageBox.Show("No se pudo conectar con la API. ¿Está encendida?\n\nError: " + ex.Message,
                    "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Si es paseador, los combos quedan fijos en él: no puede registrar paseos de otro
        private void FijarPaseadorSiCorresponde()
        {
            if (!EsPaseador) return;

            cmbPaseador.SelectedValue = usuario.Id;
            cmbPaseador.Enabled = false;
            cmbFiltroPaseador.SelectedValue = usuario.Id;
            cmbFiltroPaseador.Enabled = false;
        }

        // Deja solo los paseos que le corresponden al usuario logueado
        private List<PaseoDTO> FiltrarSegunUsuario(List<PaseoDTO> lista)
        {
            if (EsDueno)
            {
                // El dueño de un paseo es el dueño del perro
                var idsDeMisPerros = perros.Select(p => p.Id).ToHashSet();
                return lista.Where(p => idsDeMisPerros.Contains(p.PerroId)).ToList();
            }
            if (EsPaseador)
            {
                return lista.Where(p => p.PaseadorId == usuario.Id).ToList();
            }
            return lista;
        }

        private async Task CargarPaseosAsync()
        {
            try
            {
                paseosCargados = FiltrarSegunUsuario(await ApiClient.Paseos.GetAllAsync());
                MostrarPaseos();
            }
            catch (ApiException ex) when (ex.StatusCode == 401)
            {
                ApiClient.SesionVencida();
            }
            catch (ApiException ex)
            {
                // La API respondió, pero con un error
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (HttpRequestException ex)
            {
                // No hubo respuesta: la API está apagada o la URL está mal
                MessageBox.Show("No se pudo conectar con la API. ¿Está encendida?\n\nError: " + ex.Message,
                    "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void MostrarPaseos()
        {
            dgvPaseos.DataSource = paseosCargados.Select(p => new
            {
                p.Id,
                Paseador = p.PaseadorNombre,
                Perro = p.PerroNombre,
                Inicio = p.FechaHoraInicio.ToString("dd/MM/yyyy HH:mm"),
                Fin = p.FechaHoraInicio.AddMinutes(p.DuracionMinutos).ToString("HH:mm"),
                p.DuracionMinutos,
                p.PrecioTotal
            }).ToList();
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            if (cmbPaseador.SelectedValue == null || cmbPerro.SelectedValue == null)
            {
                MessageBox.Show("Seleccioná un paseador y un perro.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // El precio no se carga: lo calcula la API con la tarifa del paseador
            var nuevoPaseo = new PaseoDTO
            {
                Id = idPaseoSeleccionado,
                PaseadorId = Convert.ToInt32(cmbPaseador.SelectedValue),
                PerroId = Convert.ToInt32(cmbPerro.SelectedValue),
                FechaHoraInicio = dtpFechaHora.Value,
                DuracionMinutos = (int)nudDuracion.Value
            };

            try
            {
                if (idPaseoSeleccionado == 0)
                    await ApiClient.Paseos.AddAsync(nuevoPaseo);
                else
                    await ApiClient.Paseos.UpdateAsync(nuevoPaseo);

                MessageBox.Show("¡Paseo guardado con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarFormulario();
                await CargarPaseosAsync();
                dgvPaseos.ClearSelection();
            }
            catch (ApiException ex) when (ex.StatusCode == 401)
            {
                ApiClient.SesionVencida();
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

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            dgvPaseos.ClearSelection();
        }

        private void LimpiarFormulario()
        {
            cmbPaseador.SelectedIndex = -1;
            cmbPerro.SelectedIndex = -1;
            FijarPaseadorSiCorresponde();
            dtpFechaHora.Value = DateTime.Now.AddHours(1);
            nudDuracion.Value = 60;
            idPaseoSeleccionado = 0;
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvPaseos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccioná un paseo de la grilla primero.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult respuesta = MessageBox.Show("¿Estás seguro de que querés eliminar este paseo?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                try
                {
                    int idSeleccionado = Convert.ToInt32(dgvPaseos.SelectedRows[0].Cells["colId"].Value);
                    await ApiClient.Paseos.DeleteAsync(idSeleccionado);

                    MessageBox.Show("Paseo eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarFormulario();
                    await CargarPaseosAsync();
                }
                catch (ApiException ex) when (ex.StatusCode == 401)
                {
                    ApiClient.SesionVencida();
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

        // Busqueda por criterio: arma el query string solo con los filtros cargados
        private async void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                int? paseadorId = cmbFiltroPaseador.SelectedValue != null
                    ? Convert.ToInt32(cmbFiltroPaseador.SelectedValue) : (int?)null;

                DateTime? desde = chkFiltrarFechas.Checked ? dtpDesde.Value.Date : (DateTime?)null;
                DateTime? hasta = chkFiltrarFechas.Checked
                    ? dtpHasta.Value.Date.AddDays(1).AddSeconds(-1) : (DateTime?)null;

                paseosCargados = FiltrarSegunUsuario(await ApiClient.Paseos.BuscarAsync(paseadorId, desde, hasta));
                MostrarPaseos();
            }
            catch (ApiException ex) when (ex.StatusCode == 401)
            {
                ApiClient.SesionVencida();
            }
            catch (ApiException ex)
            {
                MessageBox.Show("Error en la búsqueda: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void btnVerTodos_Click(object sender, EventArgs e)
        {
            cmbFiltroPaseador.SelectedIndex = -1;
            FijarPaseadorSiCorresponde();
            chkFiltrarFechas.Checked = false;
            await CargarPaseosAsync();
        }

        private void dgvPaseos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int id = Convert.ToInt32(dgvPaseos.Rows[e.RowIndex].Cells["colId"].Value);
            PaseoDTO? paseo = paseosCargados.FirstOrDefault(p => p.Id == id);
            if (paseo == null) return;

            idPaseoSeleccionado = paseo.Id;
            cmbPaseador.SelectedValue = paseo.PaseadorId;
            cmbPerro.SelectedValue = paseo.PerroId;
            dtpFechaHora.Value = paseo.FechaHoraInicio;
            nudDuracion.Value = paseo.DuracionMinutos;
        }
    }
}
