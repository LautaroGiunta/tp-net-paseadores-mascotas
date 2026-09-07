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
    public partial class FormPerros : Form
    {
        private static readonly HttpClient client = new HttpClient();
        private int idPerroSeleccionado = 0;
        private List<PerroDTO> perrosCargados = new List<PerroDTO>();

        private readonly string urlApi = "https://localhost:7140/perros";
        private readonly string urlDuenos = "https://localhost:7140/duenos";

        public FormPerros()
        {
            InitializeComponent();
        }

        private async void FormPerros_Load(object sender, EventArgs e)
        {
            dgvPerros.AutoGenerateColumns = false;
            await CargarDuenosAsync();
            await CargarPerrosAsync();
        }

        // Los dueños se cargan en el combo para no tener que escribir el Id a mano
        private async Task CargarDuenosAsync()
        {
            try
            {
                List<DuenoDTO> duenos = await client.GetFromJsonAsync<List<DuenoDTO>>(urlDuenos);
                cmbDueno.DataSource = duenos
                    .Select(d => new { d.Id, Descripcion = d.Apellido + ", " + d.Nombre })
                    .ToList();
                cmbDueno.DisplayMember = "Descripcion";
                cmbDueno.ValueMember = "Id";
                cmbDueno.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo conectar con la API. ¿Está encendida?\n\nError: " + ex.Message,
                    "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task CargarPerrosAsync()
        {
            try
            {
                perrosCargados = await client.GetFromJsonAsync<List<PerroDTO>>(urlApi);
                List<DuenoDTO> duenos = await client.GetFromJsonAsync<List<DuenoDTO>>(urlDuenos);

                // Se arma una lista para mostrar el nombre del dueño en vez del Id
                dgvPerros.DataSource = perrosCargados.Select(p => new
                {
                    p.Id,
                    Dueno = duenos.Where(d => d.Id == p.DuenoId)
                                  .Select(d => d.Apellido + ", " + d.Nombre)
                                  .FirstOrDefault() ?? "",
                    p.Nombre,
                    p.Raza,
                    p.Edad
                }).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo conectar con la API. ¿Está encendida?\n\nError: " + ex.Message,
                    "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            if (cmbDueno.SelectedValue == null)
            {
                MessageBox.Show("Seleccioná un dueño para el perro.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var nuevoPerro = new PerroDTO
            {
                Id = idPerroSeleccionado,
                DuenoId = Convert.ToInt32(cmbDueno.SelectedValue),
                Nombre = txtNombre.Text,
                Raza = txtRaza.Text,
                Edad = (int)nudEdad.Value
            };

            try
            {
                HttpResponseMessage response;

                if (idPerroSeleccionado == 0)
                {
                    response = await client.PostAsJsonAsync(urlApi, nuevoPerro);
                }
                else
                {
                    response = await client.PutAsJsonAsync(urlApi, nuevoPerro);
                }

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("¡Perro guardado con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarFormulario();
                    await CargarPerrosAsync();
                    dgvPerros.ClearSelection();
                }
                else
                {
                    // Leemos el mensaje exacto que nos mandó la API con los detalles del error
                    string errorDetalle = await response.Content.ReadAsStringAsync();
                    MessageBox.Show("La API rechazó el guardado.\n\nDetalles:\n" + errorDetalle, "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error inesperado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            txtNombre.Focus();
            dgvPerros.ClearSelection();
        }

        private void LimpiarFormulario()
        {
            txtNombre.Clear();
            txtRaza.Clear();
            nudEdad.Value = 0;
            cmbDueno.SelectedIndex = -1;
            idPerroSeleccionado = 0;
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvPerros.CurrentRow == null)
            {
                MessageBox.Show("Por favor, seleccioná un perro de la grilla primero.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult respuesta = MessageBox.Show("¿Estás seguro de que querés eliminar a este perro?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                try
                {
                    int idSeleccionado = Convert.ToInt32(dgvPerros.CurrentRow.Cells["colId"].Value);
                    HttpResponseMessage response = await client.DeleteAsync($"{urlApi}/{idSeleccionado}");

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Perro eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LimpiarFormulario();
                        await CargarPerrosAsync();
                    }
                    else
                    {
                        string errorDetalle = await response.Content.ReadAsStringAsync();
                        MessageBox.Show("La API rechazó la eliminación.\n\nDetalles:\n" + errorDetalle, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hubo un error al intentar conectar:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvPerros_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int id = Convert.ToInt32(dgvPerros.Rows[e.RowIndex].Cells["colId"].Value);
            PerroDTO? perro = perrosCargados.FirstOrDefault(p => p.Id == id);
            if (perro == null) return;

            idPerroSeleccionado = perro.Id;
            cmbDueno.SelectedValue = perro.DuenoId;
            txtNombre.Text = perro.Nombre;
            txtRaza.Text = perro.Raza;
            nudEdad.Value = perro.Edad;
        }
    }
}
