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


        public FormPaseos()
        {
            InitializeComponent();
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
                perros = await ApiClient.Perros.GetAllAsync();

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
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo conectar con la API. ¿Está encendida?\n\nError: " + ex.Message,
                    "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task CargarPaseosAsync()
        {
            try
            {
                paseosCargados = await ApiClient.Paseos.GetAllAsync();
                MostrarPaseos();
            }
            catch (Exception ex)
            {
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
            dtpFechaHora.Value = DateTime.Now.AddHours(1);
            nudDuracion.Value = 60;
            idPaseoSeleccionado = 0;
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvPaseos.CurrentRow == null)
            {
                MessageBox.Show("Por favor, seleccioná un paseo de la grilla primero.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult respuesta = MessageBox.Show("¿Estás seguro de que querés eliminar este paseo?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                try
                {
                    int idSeleccionado = Convert.ToInt32(dgvPaseos.CurrentRow.Cells["colId"].Value);
                    await ApiClient.Paseos.DeleteAsync(idSeleccionado);

                    MessageBox.Show("Paseo eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarFormulario();
                    await CargarPaseosAsync();
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

                paseosCargados = await ApiClient.Paseos.BuscarAsync(paseadorId, desde, hasta);
                MostrarPaseos();
            }
            catch (ApiException ex)
            {
                MessageBox.Show("Error en la búsqueda: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void btnVerTodos_Click(object sender, EventArgs e)
        {
            cmbFiltroPaseador.SelectedIndex = -1;
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
