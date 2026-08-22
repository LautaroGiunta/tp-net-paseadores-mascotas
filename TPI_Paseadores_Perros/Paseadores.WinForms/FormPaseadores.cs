using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTOs;
using System.Net.Http.Json;

namespace Paseadores.WinForms
{
    public partial class FormPaseadores : Form
    {
        private static readonly HttpClient client = new HttpClient();
        private int idPaseadorSeleccionado = 0;

        private readonly string urlApi = "https://localhost:7140/paseadores";
        public FormPaseadores()
        {
            InitializeComponent();
        }

        private async void FormPaseadores_Load(object sender, EventArgs e)
        {
            dgvPaseadores.AutoGenerateColumns = false;
            await CargarPaseadoresAsync();
        }

        private async Task CargarPaseadoresAsync()
        {
            try
            {
                List<PaseadorDTO> lista = await client.GetFromJsonAsync<List<PaseadorDTO>>(urlApi);
                dgvPaseadores.DataSource = lista;

            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo conectar con la API. ¿Está encendida?\n\nError: " + ex.Message,
                    "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            var nuevoPaseador = new PaseadorDTO
            {
                Id = idPaseadorSeleccionado,
                Nombre = txtNombre.Text,
                Apellido = txtApellido.Text,
                Telefono = txtTelefono.Text,
                Email = txtEmail.Text,
                TarifaPorHora = decimal.Parse(txtTarifa.Text),
                Zona = txtZona.Text
            };

            try
            {
                HttpResponseMessage response;

                if(idPaseadorSeleccionado == 0)
                {
                    response = await client.PostAsJsonAsync(urlApi, nuevoPaseador);
                }
                else
                {
                    response = await client.PutAsJsonAsync(urlApi, nuevoPaseador);
                }

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("¡Paseador guardado con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    txtNombre.Clear();
                    txtApellido.Clear();
                    txtEmail.Clear();
                    txtTelefono.Clear();
                    txtTarifa.Clear();
                    txtZona.Clear();
                    idPaseadorSeleccionado = 0;
                    dgvPaseadores.ClearSelection();
                    await CargarPaseadoresAsync();
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
                MessageBox.Show("Hubo un error al intentar guardar:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtNombre.Clear();
            txtApellido.Clear();
            txtEmail.Clear();
            txtTelefono.Clear();
            txtTarifa.Clear();
            txtZona.Clear();
            txtNombre.Focus();
            idPaseadorSeleccionado = 0;
            dgvPaseadores.ClearSelection();
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvPaseadores.CurrentRow == null)
            {
                MessageBox.Show("Por favor, seleccioná un paseador de la grilla primero.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult respuesta = MessageBox.Show("¿Estás seguro de que querés eliminar a este paseador?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                try
                {
                    int idSeleccionado = Convert.ToInt32(dgvPaseadores.CurrentRow.Cells["colId"].Value);
                    HttpResponseMessage response = await client.DeleteAsync($"{urlApi}/{idSeleccionado}");

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Paseador eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await CargarPaseadoresAsync();
                    }
                    else
                    {
                        MessageBox.Show("La API rechazó la eliminación. Código: " + response.StatusCode, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hubo un error al intentar conectar:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvPaseadores_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow fila = dgvPaseadores.Rows[e.RowIndex];

            idPaseadorSeleccionado = Convert.ToInt32(fila.Cells["colId"].Value);
            txtNombre.Text = fila.Cells["colNombre"].Value?.ToString();
            txtApellido.Text = fila.Cells["colApellido"].Value?.ToString();
            txtEmail.Text = fila.Cells["colEmail"].Value?.ToString();
            txtTelefono.Text = fila.Cells["colTelefono"].Value?.ToString();
            txtTarifa.Text = fila.Cells["colTarifa"].Value?.ToString();
            txtZona.Text = fila.Cells["colZona"].Value?.ToString();
        }
    }
}
