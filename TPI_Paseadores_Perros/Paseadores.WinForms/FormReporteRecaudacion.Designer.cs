namespace Paseadores.WinForms
{
    partial class FormReporteRecaudacion
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
            grafico = new ScottPlot.WinForms.FormsPlot();
            pnlFiltros = new Panel();
            lblDesde = new Label();
            dtpDesde = new DateTimePicker();
            lblHasta = new Label();
            dtpHasta = new DateTimePicker();
            btnGenerar = new Button();
            lblTotalGeneral = new Label();
            dgvDetalle = new DataGridView();
            colMes = new DataGridViewTextBoxColumn();
            colPaseador = new DataGridViewTextBoxColumn();
            colPaseos = new DataGridViewTextBoxColumn();
            colMinutos = new DataGridViewTextBoxColumn();
            colTotal = new DataGridViewTextBoxColumn();
            pnlFiltros.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDetalle).BeginInit();
            SuspendLayout();
            //
            // grafico
            //
            grafico.DisplayScale = 1F;
            grafico.Dock = DockStyle.Fill;
            grafico.Location = new Point(0, 45);
            grafico.Name = "grafico";
            grafico.Size = new Size(900, 327);
            grafico.TabIndex = 1;
            //
            // pnlFiltros
            //
            pnlFiltros.Controls.Add(lblDesde);
            pnlFiltros.Controls.Add(dtpDesde);
            pnlFiltros.Controls.Add(lblHasta);
            pnlFiltros.Controls.Add(dtpHasta);
            pnlFiltros.Controls.Add(btnGenerar);
            pnlFiltros.Controls.Add(lblTotalGeneral);
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
            // lblTotalGeneral
            //
            lblTotalGeneral.AutoSize = true;
            lblTotalGeneral.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTotalGeneral.Location = new Point(490, 13);
            lblTotalGeneral.Name = "lblTotalGeneral";
            lblTotalGeneral.Size = new Size(0, 19);
            lblTotalGeneral.TabIndex = 5;
            //
            // dgvDetalle
            //
            dgvDetalle.AllowUserToAddRows = false;
            dgvDetalle.AllowUserToDeleteRows = false;
            dgvDetalle.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDetalle.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDetalle.Columns.AddRange(new DataGridViewColumn[] { colMes, colPaseador, colPaseos, colMinutos, colTotal });
            dgvDetalle.Dock = DockStyle.Bottom;
            dgvDetalle.Location = new Point(0, 372);
            dgvDetalle.Name = "dgvDetalle";
            dgvDetalle.ReadOnly = true;
            dgvDetalle.RowHeadersWidth = 51;
            dgvDetalle.Size = new Size(900, 200);
            dgvDetalle.TabIndex = 2;
            //
            // colMes
            //
            colMes.DataPropertyName = "Mes";
            colMes.HeaderText = "Mes";
            colMes.MinimumWidth = 6;
            colMes.Name = "colMes";
            colMes.ReadOnly = true;
            //
            // colPaseador
            //
            colPaseador.DataPropertyName = "Paseador";
            colPaseador.HeaderText = "Paseador";
            colPaseador.MinimumWidth = 6;
            colPaseador.Name = "colPaseador";
            colPaseador.ReadOnly = true;
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
            colTotal.HeaderText = "Recaudado";
            colTotal.MinimumWidth = 6;
            colTotal.Name = "colTotal";
            colTotal.ReadOnly = true;
            //
            // FormReporteRecaudacion
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(900, 572);
            Controls.Add(grafico);
            Controls.Add(dgvDetalle);
            Controls.Add(pnlFiltros);
            Name = "FormReporteRecaudacion";
            Text = "Reporte: recaudación mensual por paseador";
            WindowState = FormWindowState.Maximized;
            Load += FormReporteRecaudacion_Load;
            pnlFiltros.ResumeLayout(false);
            pnlFiltros.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDetalle).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private ScottPlot.WinForms.FormsPlot grafico;
        private Panel pnlFiltros;
        private Label lblDesde;
        private DateTimePicker dtpDesde;
        private Label lblHasta;
        private DateTimePicker dtpHasta;
        private Button btnGenerar;
        private Label lblTotalGeneral;
        private DataGridView dgvDetalle;
        private DataGridViewTextBoxColumn colMes;
        private DataGridViewTextBoxColumn colPaseador;
        private DataGridViewTextBoxColumn colPaseos;
        private DataGridViewTextBoxColumn colMinutos;
        private DataGridViewTextBoxColumn colTotal;
    }
}
