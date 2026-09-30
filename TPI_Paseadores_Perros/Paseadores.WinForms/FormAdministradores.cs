using API.Clients;
using DTOs;
using System.Text.RegularExpressions;

namespace Paseadores.WinForms
{
    // ABM de los usuarios con rol Admin (solo lo ve otro Admin)
    public partial class FormAdministradores : Form
    {
        private int idAdministradorSeleccionado = 0;

        public FormAdministradores()
        {
            InitializeComponent();
        }

        private async void FormAdministradores_Load(object sender, EventArgs e)
        {
            dgvAdministradores.AutoGenerateColumns = false;
            await CargarAdministradoresAsync();
        }

        private async Task CargarAdministradoresAsync()
        {
            try
            {
                List<UsuarioDTO> lista = await ApiClient.Administradores.GetAllAsync();
                dgvAdministradores.DataSource = lista;
                dgvAdministradores.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo conectar con la API. ¿Está encendida?\n\nError: " + ex.Message,
                    "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- Validación a nivel UI (la API y el dominio vuelven a validar) ---
        private bool EsValido()
        {
            string? error = null;

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
                error = "El nombre es obligatorio.";
            else if (string.IsNullOrWhiteSpace(txtApellido.Text))
                error = "El apellido es obligatorio.";
            else if (!Regex.IsMatch(txtEmail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                error = "El email no tiene un formato válido.";
            else if (!Regex.IsMatch(txtTelefono.Text, @"^[0-9]{8,15}$"))
                error = "El teléfono debe tener entre 8 y 15 números, sin espacios ni letras.";
            else if (idAdministradorSeleccionado == 0 && txtContrasena.Text.Length < 8)
                error = "La contraseña debe tener al menos 8 caracteres.";
            else if (idAdministradorSeleccionado != 0 && txtContrasena.Text.Length > 0 && txtContrasena.Text.Length < 8)
                error = "La contraseña nueva debe tener al menos 8 caracteres.";

            if (error != null)
            {
                MessageBox.Show(error, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!EsValido())
                return;

            var administrador = new UsuarioDTO
            {
                Id = idAdministradorSeleccionado,
                Nombre = txtNombre.Text,
                Apellido = txtApellido.Text,
                Email = txtEmail.Text,
                Telefono = txtTelefono.Text,
                Contrasena = txtContrasena.Text
            };

            try
            {
                if (idAdministradorSeleccionado == 0)
                    await ApiClient.Administradores.AddAsync(administrador);
                else
                    await ApiClient.Administradores.UpdateAsync(administrador);

                MessageBox.Show("¡Administrador guardado con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarFormulario();
                await CargarAdministradoresAsync();
            }
            catch (ApiException ex)
            {
                MessageBox.Show(ex.Message, "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error inesperado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            dgvAdministradores.ClearSelection();
        }

        private void LimpiarFormulario()
        {
            txtNombre.Clear();
            txtApellido.Clear();
            txtEmail.Clear();
            txtTelefono.Clear();
            txtContrasena.Clear();
            txtNombre.Focus();
            idAdministradorSeleccionado = 0;
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvAdministradores.CurrentRow == null)
            {
                MessageBox.Show("Por favor, seleccioná un administrador de la grilla primero.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult respuesta = MessageBox.Show("¿Estás seguro de que querés eliminar a este administrador?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                try
                {
                    int idSeleccionado = Convert.ToInt32(dgvAdministradores.CurrentRow.Cells["colId"].Value);
                    await ApiClient.Administradores.DeleteAsync(idSeleccionado);

                    MessageBox.Show("Administrador eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarFormulario();
                    await CargarAdministradoresAsync();
                }
                catch (ApiException ex)
                {
                    // Ej: "No podés eliminar tu propio usuario."
                    MessageBox.Show("La API rechazó la eliminación.\n\nDetalles:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hubo un error al intentar conectar:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvAdministradores_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow fila = dgvAdministradores.Rows[e.RowIndex];

            idAdministradorSeleccionado = Convert.ToInt32(fila.Cells["colId"].Value);
            txtNombre.Text = fila.Cells["colNombre"].Value?.ToString();
            txtApellido.Text = fila.Cells["colApellido"].Value?.ToString();
            txtEmail.Text = fila.Cells["colEmail"].Value?.ToString();
            txtTelefono.Text = fila.Cells["colTelefono"].Value?.ToString();
            txtContrasena.Clear();
        }
    }
}
