namespace Paseadores.WinForms
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            archivoToolStripMenuItem = new ToolStripMenuItem();
            cerrarSesiónToolStripMenuItem = new ToolStripMenuItem();
            salirToolStripMenuItem = new ToolStripMenuItem();
            gestionToolStripMenuItem = new ToolStripMenuItem();
            paseadoresToolStripMenuItem = new ToolStripMenuItem();
            dueñosToolStripMenuItem = new ToolStripMenuItem();
            perrosToolStripMenuItem = new ToolStripMenuItem();
            paseosToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { archivoToolStripMenuItem, gestionToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 28);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // archivoToolStripMenuItem
            // 
            archivoToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { cerrarSesiónToolStripMenuItem, salirToolStripMenuItem });
            archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            archivoToolStripMenuItem.Size = new Size(73, 24);
            archivoToolStripMenuItem.Text = "Archivo";
            // 
            // cerrarSesiónToolStripMenuItem
            // 
            cerrarSesiónToolStripMenuItem.Name = "cerrarSesiónToolStripMenuItem";
            cerrarSesiónToolStripMenuItem.Size = new Size(224, 26);
            cerrarSesiónToolStripMenuItem.Text = "Cerrar Sesión";
            cerrarSesiónToolStripMenuItem.Click += cerrarSesiónToolStripMenuItem_Click;
            // 
            // salirToolStripMenuItem
            // 
            salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            salirToolStripMenuItem.Size = new Size(224, 26);
            salirToolStripMenuItem.Text = "Salir";
            salirToolStripMenuItem.Click += salirToolStripMenuItem_Click;
            // 
            // gestionToolStripMenuItem
            // 
            gestionToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { paseadoresToolStripMenuItem, dueñosToolStripMenuItem, perrosToolStripMenuItem, paseosToolStripMenuItem });
            gestionToolStripMenuItem.Name = "gestionToolStripMenuItem";
            gestionToolStripMenuItem.Size = new Size(73, 24);
            gestionToolStripMenuItem.Text = "Gestión";
            // 
            // paseadoresToolStripMenuItem
            // 
            paseadoresToolStripMenuItem.Name = "paseadoresToolStripMenuItem";
            paseadoresToolStripMenuItem.Size = new Size(166, 26);
            paseadoresToolStripMenuItem.Text = "Paseadores";
            paseadoresToolStripMenuItem.Click += paseadoresToolStripMenuItem_Click;
            // 
            // dueñosToolStripMenuItem
            // 
            dueñosToolStripMenuItem.Name = "dueñosToolStripMenuItem";
            dueñosToolStripMenuItem.Size = new Size(166, 26);
            dueñosToolStripMenuItem.Text = "Dueños";
            dueñosToolStripMenuItem.Click += dueñosToolStripMenuItem_Click;
            // 
            // perrosToolStripMenuItem
            // 
            perrosToolStripMenuItem.Name = "perrosToolStripMenuItem";
            perrosToolStripMenuItem.Size = new Size(166, 26);
            perrosToolStripMenuItem.Text = "Perros";
            perrosToolStripMenuItem.Click += perrosToolStripMenuItem_Click;
            // 
            // paseosToolStripMenuItem
            // 
            paseosToolStripMenuItem.Name = "paseosToolStripMenuItem";
            paseosToolStripMenuItem.Size = new Size(166, 26);
            paseosToolStripMenuItem.Text = "Paseos";
            paseosToolStripMenuItem.Click += paseosToolStripMenuItem_Click;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(menuStrip1);
            IsMdiContainer = true;
            MainMenuStrip = menuStrip1;
            Name = "FormMain";
            Text = "FormMain";
            WindowState = FormWindowState.Maximized;
            Load += FormMain_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem archivoToolStripMenuItem;
        private ToolStripMenuItem cerrarSesiónToolStripMenuItem;
        private ToolStripMenuItem salirToolStripMenuItem;
        private ToolStripMenuItem gestionToolStripMenuItem;
        private ToolStripMenuItem paseadoresToolStripMenuItem;
        private ToolStripMenuItem dueñosToolStripMenuItem;
        private ToolStripMenuItem perrosToolStripMenuItem;
        private ToolStripMenuItem paseosToolStripMenuItem;
    }
}