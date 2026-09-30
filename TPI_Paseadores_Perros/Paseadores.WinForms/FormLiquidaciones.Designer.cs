namespace Paseadores.WinForms
{
    partial class FormLiquidaciones
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
            lblLiquidaciones = new Label();
            dgvLiquidaciones = new DataGridView();
            colId = new DataGridViewTextBoxColumn();
            colPaseador = new DataGridViewTextBoxColumn();
            colDesde = new DataGridViewTextBoxColumn();
            colHasta = new DataGridViewTextBoxColumn();
            colCantidad = new DataGridViewTextBoxColumn();
            colTotal = new DataGridViewTextBoxColumn();
            grpCabecera = new GroupBox();
            lblPaseador = new Label();
            cmbPaseador = new ComboBox();
            lblDesde = new Label();
            dtpDesde = new DateTimePicker();
            lblHasta = new Label();
            dtpHasta = new DateTimePicker();
            btnBuscarPaseos = new Button();
            lblPendientes = new Label();
            dgvPendientes = new DataGridView();
            colPendPaseoId = new DataGridViewTextBoxColumn();
            colPendFecha = new DataGridViewTextBoxColumn();
            colPendPerro = new DataGridViewTextBoxColumn();
            colPendMinutos = new DataGridViewTextBoxColumn();
            colPendImporte = new DataGridViewTextBoxColumn();
            btnAgregar = new Button();
            btnQuitar = new Button();
            lblDetalle = new Label();
            dgvDetalle = new DataGridView();
            colDetPaseoId = new DataGridViewTextBoxColumn();
            colDetFecha = new DataGridViewTextBoxColumn();
            colDetPerro = new DataGridViewTextBoxColumn();
            colDetMinutos = new DataGridViewTextBoxColumn();
            colDetImporte = new DataGridViewTextBoxColumn();
            lblTotal = new Label();
            btnGuardar = new Button();
            btnNueva = new Button();
            btnEliminar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvLiquidaciones).BeginInit();
            grpCabecera.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPendientes).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvDetalle).BeginInit();
            SuspendLayout();
            //
            // lblLiquidaciones
            //
            lblLiquidaciones.AutoSize = true;
            lblLiquidaciones.Location = new Point(12, 9);
            lblLiquidaciones.Name = "lblLiquidaciones";
            lblLiquidaciones.Size = new Size(280, 15);
            lblLiquidaciones.TabIndex = 0;
            lblLiquidaciones.Text = "Liquidaciones registradas (doble clic para editar)";
            //
            // dgvLiquidaciones
            //
            dgvLiquidaciones.AllowUserToAddRows = false;
            dgvLiquidaciones.AllowUserToDeleteRows = false;
            dgvLiquidaciones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLiquidaciones.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLiquidaciones.Columns.AddRange(new DataGridViewColumn[] { colId, colPaseador, colDesde, colHasta, colCantidad, colTotal });
            dgvLiquidaciones.Location = new Point(12, 28);
            dgvLiquidaciones.MultiSelect = false;
            dgvLiquidaciones.Name = "dgvLiquidaciones";
            dgvLiquidaciones.ReadOnly = true;
            dgvLiquidaciones.RowHeadersWidth = 51;
            dgvLiquidaciones.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLiquidaciones.Size = new Size(876, 150);
            dgvLiquidaciones.TabIndex = 1;
            dgvLiquidaciones.CellDoubleClick += dgvLiquidaciones_CellDoubleClick;
            //
            // colId
            //
            colId.DataPropertyName = "Id";
            colId.HeaderText = "N°";
            colId.MinimumWidth = 6;
            colId.Name = "colId";
            colId.ReadOnly = true;
            //
            // colPaseador
            //
            colPaseador.DataPropertyName = "Paseador";
            colPaseador.HeaderText = "Paseador";
            colPaseador.MinimumWidth = 6;
            colPaseador.Name = "colPaseador";
            colPaseador.ReadOnly = true;
            //
            // colDesde
            //
            colDesde.DataPropertyName = "Desde";
            colDesde.HeaderText = "Desde";
            colDesde.MinimumWidth = 6;
            colDesde.Name = "colDesde";
            colDesde.ReadOnly = true;
            //
            // colHasta
            //
            colHasta.DataPropertyName = "Hasta";
            colHasta.HeaderText = "Hasta";
            colHasta.MinimumWidth = 6;
            colHasta.Name = "colHasta";
            colHasta.ReadOnly = true;
            //
            // colCantidad
            //
            colCantidad.DataPropertyName = "Paseos";
            colCantidad.HeaderText = "Paseos";
            colCantidad.MinimumWidth = 6;
            colCantidad.Name = "colCantidad";
            colCantidad.ReadOnly = true;
            //
            // colTotal
            //
            colTotal.DataPropertyName = "Total";
            colTotal.HeaderText = "Total";
            colTotal.MinimumWidth = 6;
            colTotal.Name = "colTotal";
            colTotal.ReadOnly = true;
            //
            // grpCabecera
            //
            grpCabecera.Controls.Add(lblPaseador);
            grpCabecera.Controls.Add(cmbPaseador);
            grpCabecera.Controls.Add(lblDesde);
            grpCabecera.Controls.Add(dtpDesde);
            grpCabecera.Controls.Add(lblHasta);
            grpCabecera.Controls.Add(dtpHasta);
            grpCabecera.Controls.Add(btnBuscarPaseos);
            grpCabecera.Location = new Point(12, 188);
            grpCabecera.Name = "grpCabecera";
            grpCabecera.Size = new Size(876, 60);
            grpCabecera.TabIndex = 2;
            grpCabecera.TabStop = false;
            grpCabecera.Text = "Nueva liquidación";
            //
            // lblPaseador
            //
            lblPaseador.AutoSize = true;
            lblPaseador.Location = new Point(10, 26);
            lblPaseador.Name = "lblPaseador";
            lblPaseador.Size = new Size(55, 15);
            lblPaseador.TabIndex = 0;
            lblPaseador.Text = "Paseador";
            //
            // cmbPaseador
            //
            cmbPaseador.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPaseador.FormattingEnabled = true;
            cmbPaseador.Location = new Point(75, 22);
            cmbPaseador.Name = "cmbPaseador";
            cmbPaseador.Size = new Size(230, 23);
            cmbPaseador.TabIndex = 1;
            //
            // lblDesde
            //
            lblDesde.AutoSize = true;
            lblDesde.Location = new Point(322, 26);
            lblDesde.Name = "lblDesde";
            lblDesde.Size = new Size(39, 15);
            lblDesde.TabIndex = 2;
            lblDesde.Text = "Desde";
            //
            // dtpDesde
            //
            dtpDesde.Format = DateTimePickerFormat.Short;
            dtpDesde.Location = new Point(367, 22);
            dtpDesde.Name = "dtpDesde";
            dtpDesde.Size = new Size(110, 23);
            dtpDesde.TabIndex = 3;
            //
            // lblHasta
            //
            lblHasta.AutoSize = true;
            lblHasta.Location = new Point(492, 26);
            lblHasta.Name = "lblHasta";
            lblHasta.Size = new Size(37, 15);
            lblHasta.TabIndex = 4;
            lblHasta.Text = "Hasta";
            //
            // dtpHasta
            //
            dtpHasta.Format = DateTimePickerFormat.Short;
            dtpHasta.Location = new Point(535, 22);
            dtpHasta.Name = "dtpHasta";
            dtpHasta.Size = new Size(110, 23);
            dtpHasta.TabIndex = 5;
            //
            // btnBuscarPaseos
            //
            btnBuscarPaseos.Location = new Point(664, 20);
            btnBuscarPaseos.Name = "btnBuscarPaseos";
            btnBuscarPaseos.Size = new Size(200, 27);
            btnBuscarPaseos.TabIndex = 6;
            btnBuscarPaseos.Text = "Buscar paseos pendientes";
            btnBuscarPaseos.UseVisualStyleBackColor = true;
            btnBuscarPaseos.Click += btnBuscarPaseos_Click;
            //
            // lblPendientes
            //
            lblPendientes.AutoSize = true;
            lblPendientes.Location = new Point(12, 258);
            lblPendientes.Name = "lblPendientes";
            lblPendientes.Size = new Size(170, 15);
            lblPendientes.TabIndex = 3;
            lblPendientes.Text = "Paseos pendientes de liquidar";
            //
            // dgvPendientes
            //
            dgvPendientes.AllowUserToAddRows = false;
            dgvPendientes.AllowUserToDeleteRows = false;
            dgvPendientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPendientes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPendientes.Columns.AddRange(new DataGridViewColumn[] { colPendPaseoId, colPendFecha, colPendPerro, colPendMinutos, colPendImporte });
            dgvPendientes.Location = new Point(12, 277);
            dgvPendientes.Name = "dgvPendientes";
            dgvPendientes.ReadOnly = true;
            dgvPendientes.RowHeadersWidth = 51;
            dgvPendientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPendientes.Size = new Size(400, 230);
            dgvPendientes.TabIndex = 4;
            dgvPendientes.CellDoubleClick += dgvPendientes_CellDoubleClick;
            //
            // colPendPaseoId
            //
            colPendPaseoId.DataPropertyName = "PaseoId";
            colPendPaseoId.HeaderText = "Paseo";
            colPendPaseoId.MinimumWidth = 6;
            colPendPaseoId.Name = "colPendPaseoId";
            colPendPaseoId.ReadOnly = true;
            //
            // colPendFecha
            //
            colPendFecha.DataPropertyName = "Fecha";
            colPendFecha.HeaderText = "Fecha";
            colPendFecha.MinimumWidth = 6;
            colPendFecha.Name = "colPendFecha";
            colPendFecha.ReadOnly = true;
            //
            // colPendPerro
            //
            colPendPerro.DataPropertyName = "Perro";
            colPendPerro.HeaderText = "Perro";
            colPendPerro.MinimumWidth = 6;
            colPendPerro.Name = "colPendPerro";
            colPendPerro.ReadOnly = true;
            //
            // colPendMinutos
            //
            colPendMinutos.DataPropertyName = "Minutos";
            colPendMinutos.HeaderText = "Minutos";
            colPendMinutos.MinimumWidth = 6;
            colPendMinutos.Name = "colPendMinutos";
            colPendMinutos.ReadOnly = true;
            //
            // colPendImporte
            //
            colPendImporte.DataPropertyName = "Importe";
            colPendImporte.HeaderText = "Importe";
            colPendImporte.MinimumWidth = 6;
            colPendImporte.Name = "colPendImporte";
            colPendImporte.ReadOnly = true;
            //
            // btnAgregar
            //
            btnAgregar.Location = new Point(420, 340);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(50, 30);
            btnAgregar.TabIndex = 5;
            btnAgregar.Text = ">";
            btnAgregar.UseVisualStyleBackColor = true;
            btnAgregar.Click += btnAgregar_Click;
            //
            // btnQuitar
            //
            btnQuitar.Location = new Point(420, 380);
            btnQuitar.Name = "btnQuitar";
            btnQuitar.Size = new Size(50, 30);
            btnQuitar.TabIndex = 6;
            btnQuitar.Text = "<";
            btnQuitar.UseVisualStyleBackColor = true;
            btnQuitar.Click += btnQuitar_Click;
            //
            // lblDetalle
            //
            lblDetalle.AutoSize = true;
            lblDetalle.Location = new Point(478, 258);
            lblDetalle.Name = "lblDetalle";
            lblDetalle.Size = new Size(144, 15);
            lblDetalle.TabIndex = 7;
            lblDetalle.Text = "Detalle de la liquidación";
            //
            // dgvDetalle
            //
            dgvDetalle.AllowUserToAddRows = false;
            dgvDetalle.AllowUserToDeleteRows = false;
            dgvDetalle.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDetalle.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDetalle.Columns.AddRange(new DataGridViewColumn[] { colDetPaseoId, colDetFecha, colDetPerro, colDetMinutos, colDetImporte });
            dgvDetalle.Location = new Point(478, 277);
            dgvDetalle.Name = "dgvDetalle";
            dgvDetalle.ReadOnly = true;
            dgvDetalle.RowHeadersWidth = 51;
            dgvDetalle.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDetalle.Size = new Size(410, 230);
            dgvDetalle.TabIndex = 8;
            dgvDetalle.CellDoubleClick += dgvDetalle_CellDoubleClick;
            //
            // colDetPaseoId
            //
            colDetPaseoId.DataPropertyName = "PaseoId";
            colDetPaseoId.HeaderText = "Paseo";
            colDetPaseoId.MinimumWidth = 6;
            colDetPaseoId.Name = "colDetPaseoId";
            colDetPaseoId.ReadOnly = true;
            //
            // colDetFecha
            //
            colDetFecha.DataPropertyName = "Fecha";
            colDetFecha.HeaderText = "Fecha";
            colDetFecha.MinimumWidth = 6;
            colDetFecha.Name = "colDetFecha";
            colDetFecha.ReadOnly = true;
            //
            // colDetPerro
            //
            colDetPerro.DataPropertyName = "Perro";
            colDetPerro.HeaderText = "Perro";
            colDetPerro.MinimumWidth = 6;
            colDetPerro.Name = "colDetPerro";
            colDetPerro.ReadOnly = true;
            //
            // colDetMinutos
            //
            colDetMinutos.DataPropertyName = "Minutos";
            colDetMinutos.HeaderText = "Minutos";
            colDetMinutos.MinimumWidth = 6;
            colDetMinutos.Name = "colDetMinutos";
            colDetMinutos.ReadOnly = true;
            //
            // colDetImporte
            //
            colDetImporte.DataPropertyName = "Importe";
            colDetImporte.HeaderText = "Importe";
            colDetImporte.MinimumWidth = 6;
            colDetImporte.Name = "colDetImporte";
            colDetImporte.ReadOnly = true;
            //
            // lblTotal
            //
            lblTotal.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTotal.Location = new Point(678, 512);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(210, 25);
            lblTotal.TabIndex = 9;
            lblTotal.Text = "Total: $0,00";
            lblTotal.TextAlign = ContentAlignment.MiddleRight;
            //
            // btnGuardar
            //
            btnGuardar.Location = new Point(12, 550);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(110, 30);
            btnGuardar.TabIndex = 10;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            //
            // btnNueva
            //
            btnNueva.Location = new Point(128, 550);
            btnNueva.Name = "btnNueva";
            btnNueva.Size = new Size(110, 30);
            btnNueva.TabIndex = 11;
            btnNueva.Text = "Nueva";
            btnNueva.UseVisualStyleBackColor = true;
            btnNueva.Click += btnNueva_Click;
            //
            // btnEliminar
            //
            btnEliminar.Location = new Point(244, 550);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(110, 30);
            btnEliminar.TabIndex = 12;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            //
            // FormLiquidaciones
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(900, 592);
            Controls.Add(lblLiquidaciones);
            Controls.Add(dgvLiquidaciones);
            Controls.Add(grpCabecera);
            Controls.Add(lblPendientes);
            Controls.Add(dgvPendientes);
            Controls.Add(btnAgregar);
            Controls.Add(btnQuitar);
            Controls.Add(lblDetalle);
            Controls.Add(dgvDetalle);
            Controls.Add(lblTotal);
            Controls.Add(btnGuardar);
            Controls.Add(btnNueva);
            Controls.Add(btnEliminar);
            Name = "FormLiquidaciones";
            Text = "Liquidaciones a paseadores";
            WindowState = FormWindowState.Maximized;
            Load += FormLiquidaciones_Load;
            ((System.ComponentModel.ISupportInitialize)dgvLiquidaciones).EndInit();
            grpCabecera.ResumeLayout(false);
            grpCabecera.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPendientes).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvDetalle).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblLiquidaciones;
        private DataGridView dgvLiquidaciones;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colPaseador;
        private DataGridViewTextBoxColumn colDesde;
        private DataGridViewTextBoxColumn colHasta;
        private DataGridViewTextBoxColumn colCantidad;
        private DataGridViewTextBoxColumn colTotal;
        private GroupBox grpCabecera;
        private Label lblPaseador;
        private ComboBox cmbPaseador;
        private Label lblDesde;
        private DateTimePicker dtpDesde;
        private Label lblHasta;
        private DateTimePicker dtpHasta;
        private Button btnBuscarPaseos;
        private Label lblPendientes;
        private DataGridView dgvPendientes;
        private DataGridViewTextBoxColumn colPendPaseoId;
        private DataGridViewTextBoxColumn colPendFecha;
        private DataGridViewTextBoxColumn colPendPerro;
        private DataGridViewTextBoxColumn colPendMinutos;
        private DataGridViewTextBoxColumn colPendImporte;
        private Button btnAgregar;
        private Button btnQuitar;
        private Label lblDetalle;
        private DataGridView dgvDetalle;
        private DataGridViewTextBoxColumn colDetPaseoId;
        private DataGridViewTextBoxColumn colDetFecha;
        private DataGridViewTextBoxColumn colDetPerro;
        private DataGridViewTextBoxColumn colDetMinutos;
        private DataGridViewTextBoxColumn colDetImporte;
        private Label lblTotal;
        private Button btnGuardar;
        private Button btnNueva;
        private Button btnEliminar;
    }
}
