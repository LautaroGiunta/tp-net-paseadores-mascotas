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
    public partial class FormPaseadores : Form
    {
        private int idPaseadorSeleccionado = 0;

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
                List<PaseadorDTO> lista = await ApiClient.Paseadores.GetAllAsync();
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
            if (idPaseadorSeleccionado == 0 && txtContrasena.Text.Length < 8)
            {
                MessageBox.Show("La contraseña debe tener al menos 8 caracteres.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtContrasena.Focus();
                return;
            }
            var nuevoPaseador = new PaseadorDTO
            {
                Id = idPaseadorSeleccionado,
                Nombre = txtNombre.Text,
                Apellido = txtApellido.Text,
                Telefono = txtTelefono.Text,
                Email = txtEmail.Text,
                TarifaPorHora = decimal.Parse(txtTarifa.Text),
                Zona = txtZona.Text,
                Contrasena = txtContrasena.Text
            };

            try
            {
                if (idPaseadorSeleccionado == 0)
                    await ApiClient.Paseadores.AddAsync(nuevoPaseador);
                else
                    await ApiClient.Paseadores.UpdateAsync(nuevoPaseador);

                MessageBox.Show("¡Paseador guardado con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtNombre.Clear();
                txtApellido.Clear();
                txtEmail.Clear();
                txtTelefono.Clear();
                txtTarifa.Clear();
                txtZona.Clear();
                idPaseadorSeleccionado = 0;
                await CargarPaseadoresAsync();
                dgvPaseadores.ClearSelection();
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
            txtNombre.Clear();
            txtApellido.Clear();
            txtEmail.Clear();
            txtTelefono.Clear();
            txtTarifa.Clear();
            txtZona.Clear();
            txtContrasena.Clear();
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
                    await ApiClient.Paseadores.DeleteAsync(idSeleccionado);

                    MessageBox.Show("Paseador eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await CargarPaseadoresAsync();
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
            txtContrasena.Clear();
        }

    }
}
