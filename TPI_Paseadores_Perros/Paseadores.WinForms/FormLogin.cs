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

                //  Hacemos el POST al endpoint que creamos
                HttpResponseMessage response = await ApiClient.Http.PostAsJsonAsync("/api/auth/login", loginData);

                if (response.IsSuccessStatusCode)
                {
                    LoginResponseDTO? respuesta = await response.Content.ReadFromJsonAsync<LoginResponseDTO>();

                    if (respuesta == null || string.IsNullOrWhiteSpace(respuesta.Token))
                    {
                        MessageBox.Show("La API no devolvió un token válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // A partir de acá todas las llamadas llevan el token
                    ApiClient.GuardarToken(respuesta.Token);
                    UsuarioLogueado = respuesta.Usuario;

                    this.DialogResult = DialogResult.OK;

                    MessageBox.Show($"¡Bienvenido {UsuarioLogueado.Nombre}!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    // Error 401: Credenciales incorrectas
                    MessageBox.Show("Email o contraseña incorrectos.", "Error de Acceso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Ocurrió un error en el servidor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Por si la API está apagada o no hay internet
                MessageBox.Show("Error de conexión: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
