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
            // El menú se recorta según el rol, igual que las políticas de la WebAPI
            if (_usuarioActual.Rol == "Dueno")
            {
                // Un dueño administra sus perros y sus paseos, no a los demás usuarios
                dueñosToolStripMenuItem.Visible = false;
                paseadoresToolStripMenuItem.Visible = false;
            }
            else if (_usuarioActual.Rol == "Paseador")
            {
                // Un paseador solo opera la agenda de paseos
                dueñosToolStripMenuItem.Visible = false;
                paseadoresToolStripMenuItem.Visible = false;
                perrosToolStripMenuItem.Visible = false;
            }

            this.Text = $"Paseadores de Perros - {_usuarioActual.Nombre} {_usuarioActual.Apellido} ({_usuarioActual.Rol})";
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
