using API.Clients;
using DTOs;
using System;
using System.Net.Http.Json;
using System.Windows.Forms;

namespace Paseadores.WinForms
{
    public partial class FormLogin : Form
    {
        public UsuarioDTO? UsuarioLogueado { get; private set; }
        public FormLogin()
        {
            InitializeComponent();
        }

        private async void btnIngresar_Click(object sender, EventArgs e)
        {
            //  Validar que no manden campos vacíos
            if (string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtContraseña.Text))
            {
                MessageBox.Show("Por favor, completá tu email y contraseña.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var loginData = new LoginRequestDTO
                {
                    Email = txtEmail.Text.Trim(),
                    Contrasena = txtContraseña.Text
                };

                // Una sola línea: el cliente se encarga del HTTP y de los errores
                LoginResponseDTO? respuesta = await ApiClient.Auth.LoginAsync(loginData);

                if (respuesta == null || string.IsNullOrWhiteSpace(respuesta.Token))
                {
                    MessageBox.Show("La API no devolvió un token válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                ApiClient.GuardarToken(respuesta.Token);
                UsuarioLogueado = respuesta.Usuario;
                this.DialogResult = DialogResult.OK;
                MessageBox.Show($"¡Bienvenido {UsuarioLogueado.Nombre}!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (ApiException ex) when (ex.StatusCode == 401)
            {
                // Credenciales incorrectas
                MessageBox.Show("Email o contraseña incorrectos.", "Error de Acceso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (ApiException ex)
            {
                // Cualquier otro error que devolvió la API (con su mensaje real)
                MessageBox.Show("Ocurrió un error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                // La API está apagada / sin conexión
                MessageBox.Show("Error de conexión: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
