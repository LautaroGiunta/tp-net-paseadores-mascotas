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
    public partial class FormLogin : Form
    {
        public UsuarioDTO UsuarioLogueado { get; private set; }
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

                var loginData = new
                {
                    Email = txtEmail.Text,
                    Contrasena = txtContraseña.Text
                };

                //  Preparamos el cliente ignorando el certificado de desarrollo de HTTPS
                using HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:5206"); 

                //  Hacemos el POST al endpoint que creamos
                HttpResponseMessage response = await client.PostAsJsonAsync("/api/auth/login", loginData);

                if (response.IsSuccessStatusCode)
                {
                    UsuarioLogueado = await response.Content.ReadFromJsonAsync<UsuarioDTO>();
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
