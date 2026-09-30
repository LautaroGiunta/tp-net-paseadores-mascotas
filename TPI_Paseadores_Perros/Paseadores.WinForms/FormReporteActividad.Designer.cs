namespace Paseadores.WinForms
{
    partial class FormReporteActividad
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
            dgvActividad = new DataGridView();
            colPerro = new DataGridViewTextBoxColumn();
            colRaza = new DataGridViewTextBoxColumn();
            colDueno = new DataGridViewTextBoxColumn();
            colPaseos = new DataGridViewTextBoxColumn();
            colMinutos = new DataGridViewTextBoxColumn();
            colTotal = new DataGridViewTextBoxColumn();
            colUltimo = new DataGridViewTextBoxColumn();
            pnlFiltros = new Panel();
            lblDesde = new Label();
            dtpDesde = new DateTimePicker();
            lblHasta = new Label();
            dtpHasta = new DateTimePicker();
            btnGenerar = new Button();
            lblResumen = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvActividad).BeginInit();
            pnlFiltros.SuspendLayout();
            SuspendLayout();
            //
            // dgvActividad
            //
            dgvActividad.AllowUserToAddRows = false;
            dgvActividad.AllowUserToDeleteRows = false;
            dgvActividad.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvActividad.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvActividad.Columns.AddRange(new DataGridViewColumn[] { colPerro, colRaza, colDueno, colPaseos, colMinutos, colTotal, colUltimo });
            dgvActividad.Dock = DockStyle.Fill;
            dgvActividad.Location = new Point(0, 45);
            dgvActividad.Name = "dgvActividad";
            dgvActividad.ReadOnly = true;
            dgvActividad.RowHeadersWidth = 51;
            dgvActividad.Size = new Size(900, 400);
            dgvActividad.TabIndex = 1;
            //
            // colPerro
            //
            colPerro.DataPropertyName = "Perro";
            colPerro.HeaderText = "Perro";
            colPerro.MinimumWidth = 6;
            colPerro.Name = "colPerro";
            colPerro.ReadOnly = true;
            //
            // colRaza
            //
            colRaza.DataPropertyName = "Raza";
            colRaza.HeaderText = "Raza";
            colRaza.MinimumWidth = 6;
            colRaza.Name = "colRaza";
            colRaza.ReadOnly = true;
            //
            // colDueno
            //
            colDueno.DataPropertyName = "Dueno";
            colDueno.HeaderText = "Dueño";
            colDueno.MinimumWidth = 6;
            colDueno.Name = "colDueno";
            colDueno.ReadOnly = true;
            //
            // colPaseos
            //
            colPaseos.DataPropertyName = "Paseos";
            colPaseos.HeaderText = "Paseos";
            colPaseos.MinimumWidth = 6;
            colPaseos.Name = "colPaseos";
            colPaseos.ReadOnly = true;
            //
            // colMinutos
            //
            colMinutos.DataPropertyName = "Minutos";
            colMinutos.HeaderText = "Minutos";
            colMinutos.MinimumWidth = 6;
            colMinutos.Name = "colMinutos";
            colMinutos.ReadOnly = true;
            //
            // colTotal
            //
            colTotal.DataPropertyName = "Total";
            colTotal.HeaderText = "Gastado";
            colTotal.MinimumWidth = 6;
            colTotal.Name = "colTotal";
            colTotal.ReadOnly = true;
            //
            // colUltimo
            //
            colUltimo.DataPropertyName = "UltimoPaseo";
            colUltimo.HeaderText = "Último paseo";
            colUltimo.MinimumWidth = 6;
            colUltimo.Name = "colUltimo";
            colUltimo.ReadOnly = true;
            //
            // pnlFiltros
            //
            pnlFiltros.Controls.Add(lblDesde);
            pnlFiltros.Controls.Add(dtpDesde);
            pnlFiltros.Controls.Add(lblHasta);
            pnlFiltros.Controls.Add(dtpHasta);
            pnlFiltros.Controls.Add(btnGenerar);
            pnlFiltros.Controls.Add(lblResumen);
            pnlFiltros.Dock = DockStyle.Top;
            pnlFiltros.Location = new Point(0, 0);
            pnlFiltros.Name = "pnlFiltros";
            pnlFiltros.Size = new Size(900, 45);
            pnlFiltros.TabIndex = 0;
            //
            // lblDesde
            //
            lblDesde.AutoSize = true;
            lblDesde.Location = new Point(12, 15);
            lblDesde.Name = "lblDesde";
            lblDesde.Size = new Size(39, 15);
            lblDesde.TabIndex = 0;
            lblDesde.Text = "Desde";
            //
            // dtpDesde
            //
            dtpDesde.Format = DateTimePickerFormat.Short;
            dtpDesde.Location = new Point(57, 11);
            dtpDesde.Name = "dtpDesde";
            dtpDesde.Size = new Size(110, 23);
            dtpDesde.TabIndex = 1;
            //
            // lblHasta
            //
            lblHasta.AutoSize = true;
            lblHasta.Location = new Point(182, 15);
            lblHasta.Name = "lblHasta";
            lblHasta.Size = new Size(37, 15);
            lblHasta.TabIndex = 2;
            lblHasta.Text = "Hasta";
            //
            // dtpHasta
            //
            dtpHasta.Format = DateTimePickerFormat.Short;
            dtpHasta.Location = new Point(225, 11);
            dtpHasta.Name = "dtpHasta";
            dtpHasta.Size = new Size(110, 23);
            dtpHasta.TabIndex = 3;
            //
            // btnGenerar
            //
            btnGenerar.Location = new Point(350, 9);
            btnGenerar.Name = "btnGenerar";
            btnGenerar.Size = new Size(120, 27);
            btnGenerar.TabIndex = 4;
            btnGenerar.Text = "Generar reporte";
            btnGenerar.UseVisualStyleBackColor = true;
            btnGenerar.Click += btnGenerar_Click;
            //
            // lblResumen
            //
            lblResumen.AutoSize = true;
            lblResumen.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblResumen.Location = new Point(490, 13);
            lblResumen.Name = "lblResumen";
            lblResumen.Size = new Size(0, 19);
            lblResumen.TabIndex = 5;
            //
            // FormReporteActividad
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(900, 445);
            Controls.Add(dgvActividad);
            Controls.Add(pnlFiltros);
            Name = "FormReporteActividad";
            Text = "Reporte: actividad por perro";
            WindowState = FormWindowState.Maximized;
            Load += FormReporteActividad_Load;
            ((System.ComponentModel.ISupportInitialize)dgvActividad).EndInit();
            pnlFiltros.ResumeLayout(false);
            pnlFiltros.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvActividad;
        private DataGridViewTextBoxColumn colPerro;
        private DataGridViewTextBoxColumn colRaza;
        private DataGridViewTextBoxColumn colDueno;
        private DataGridViewTextBoxColumn colPaseos;
        private DataGridViewTextBoxColumn colMinutos;
        private DataGridViewTextBoxColumn colTotal;
        private DataGridViewTextBoxColumn colUltimo;
        private Panel pnlFiltros;
        private Label lblDesde;
        private DateTimePicker dtpDesde;
        private Label lblHasta;
        private DateTimePicker dtpHasta;
        private Button btnGenerar;
        private Label lblResumen;
    }
}
