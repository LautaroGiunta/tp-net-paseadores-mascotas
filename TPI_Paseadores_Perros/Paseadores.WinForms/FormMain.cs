using DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paseadores.WinForms
{
    public partial class FormMain : Form
    {
        public bool QuiereCerrarSesion { get; set; } = false;
        private UsuarioDTO _usuarioActual;
        public FormMain(UsuarioDTO usuarioLogueado)
        {
            InitializeComponent();
            _usuarioActual = usuarioLogueado;
            ConfigurarToolbar();
        }

        private void ConfigurarToolbar()
        {
            // Por defecto, asumimos que el Admin ve todo, así que no ocultamos nada.
            // Pero si es Dueño o Paseador, empezamos a ocultar cosas:

            if (_usuarioActual.Rol == "Dueno")
            {
                // Ejemplo: Un dueño no debería poder gestionar a otros dueños.
                // Ocultamos el botón de la toolbar (REEMPLAZÁ "btnDuenos" POR EL NOMBRE REAL DE TU BOTÓN)
                dueñosToolStripMenuItem.Visible = false;
                paseadoresToolStripMenuItem.Visible = false;

            }
            else if (_usuarioActual.Rol == "Paseador")
            {
                // Ejemplo: Un paseador no debería poder agregar o borrar paseadores.
                dueñosToolStripMenuItem.Visible = false;
                paseadoresToolStripMenuItem.Visible = false;
                perrosToolStripMenuItem.Visible = false;
            }
            else if (_usuarioActual.Rol == "Admin")
            {
                // El Admin ve todo. Podés poner un mensajito en el título del form para que quede lindo
                this.Text = $"Panel de Administración - Bienvenido {_usuarioActual.Nombre}";
            }
        }
        private void FormMain_Load(object sender, EventArgs e)
        {

        }

        private void paseadoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormPaseadores ventanaPaseadores = new FormPaseadores();
            ventanaPaseadores.MdiParent = this;
            ventanaPaseadores.Show();
        }

        private void dueñosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormDueños ventanaDueños = new FormDueños();
            ventanaDueños.MdiParent = this;
            ventanaDueños.Show();
        }

        private void perrosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormPerros ventanaPerros = new FormPerros();
            ventanaPerros.MdiParent = this;
            ventanaPerros.Show();
        }

        private void paseosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormPaseos ventanaPaseos = new FormPaseos();
            ventanaPaseos.MdiParent = this;
            ventanaPaseos.Show();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QuiereCerrarSesion = false;
            this.Close();
        }

        private void cerrarSesiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QuiereCerrarSesion = true;
            this.Close();
        }
    }
}
