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
    public partial class FormDueños : Form
    {
        private static readonly HttpClient client = new HttpClient();
        int idDueñoSeleccionado = 0;
        private readonly string urlApi = "https://localhost:7140/duenos";
        public FormDueños()
        {
            InitializeComponent();
        }

        private async void FormDueños_Load(object sender, EventArgs e)
        {
            dgvDueños.AutoGenerateColumns = false;
            await CargarDueñosAsync();
        }

        private async Task CargarDueñosAsync()
        {
            try
            {
                List<DuenoDTO> lista = await client.GetFromJsonAsync<List<DuenoDTO>>(urlApi);
                dgvDueños.DataSource = lista;

            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo conectar con la API. ¿Está encendida?\n\nError: " + ex.Message,
                    "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            var nuevoDueño = new DuenoDTO
            {
                Id = idDueñoSeleccionado,
                Nombre = txtNombre.Text,
                Apellido = txtApellido.Text,
                Telefono = txtTelefono.Text,
                Email = txtEmail.Text,
                Direccion = txtDireccion.Text
            };
            try
            {
                HttpResponseMessage response;
                if (idDueñoSeleccionado == 0)
                {
                    response = await client.PostAsJsonAsync(urlApi, nuevoDueño);
                }
                else
                {
                    response = await client.PutAsJsonAsync(urlApi, nuevoDueño);
                }
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("¡Paseador guardado con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    txtNombre.Clear();
                    txtApellido.Clear();
                    txtEmail.Clear();
                    txtTelefono.Clear();
                    txtDireccion.Clear();
                    idDueñoSeleccionado = 0;
                    await CargarDueñosAsync();
                    dgvDueños.ClearSelection();
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
            txtDireccion.Clear();
            txtNombre.Focus();
            idDueñoSeleccionado = 0;
            dgvDueños.ClearSelection();
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvDueños.CurrentRow == null)
            {
                MessageBox.Show("Por favor, seleccioná un dueño de la grilla primero.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult respuesta = MessageBox.Show("¿Estás seguro de que querés eliminar a este dueño?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                try
                {
                    int idSeleccionado = Convert.ToInt32(dgvDueños.CurrentRow.Cells["colId"].Value);
                    HttpResponseMessage response = await client.DeleteAsync($"{urlApi}/{idSeleccionado}");

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Dueño eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await CargarDueñosAsync();
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

        private void dgvDueños_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow fila = dgvDueños.Rows[e.RowIndex];

            idDueñoSeleccionado = Convert.ToInt32(fila.Cells["colId"].Value);
            txtNombre.Text = fila.Cells["colNombre"].Value?.ToString();
            txtApellido.Text = fila.Cells["colApellido"].Value?.ToString();
            txtEmail.Text = fila.Cells["colEmail"].Value?.ToString();
            txtTelefono.Text = fila.Cells["colTelefono"].Value?.ToString();
            txtDireccion.Text = fila.Cells["colDireccion"].Value?.ToString();
        }
    }
}
