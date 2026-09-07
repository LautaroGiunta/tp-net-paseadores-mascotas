namespace Paseadores.WinForms
{
    partial class FormPaseos
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
            dgvPaseos = new DataGridView();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            cmbPaseador = new ComboBox();
            cmbPerro = new ComboBox();
            dtpFechaHora = new DateTimePicker();
            nudDuracion = new NumericUpDown();
            btnGuardar = new Button();
            btnEliminar = new Button();
            btnLimpiar = new Button();
            cmbFiltroPaseador = new ComboBox();
            chkFiltrarFechas = new CheckBox();
            dtpDesde = new DateTimePicker();
            dtpHasta = new DateTimePicker();
            btnBuscar = new Button();
            btnVerTodos = new Button();
            colId = new DataGridViewTextBoxColumn();
            colPaseador = new DataGridViewTextBoxColumn();
            colPerro = new DataGridViewTextBoxColumn();
            colInicio = new DataGridViewTextBoxColumn();
            colFin = new DataGridViewTextBoxColumn();
            colDuracion = new DataGridViewTextBoxColumn();
            colPrecio = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dgvPaseos).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudDuracion).BeginInit();
            SuspendLayout();
            // 
            // dgvPaseos
            // 
            dgvPaseos.AllowUserToAddRows = false;
            dgvPaseos.AllowUserToDeleteRows = false;
            dgvPaseos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvPaseos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPaseos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPaseos.Columns.AddRange(new DataGridViewColumn[] { colId, colPaseador, colPerro, colInicio, colFin, colDuracion, colPrecio });
            dgvPaseos.Location = new Point(355, 125);
            dgvPaseos.Name = "dgvPaseos";
            dgvPaseos.ReadOnly = true;
            dgvPaseos.RowHeadersWidth = 51;
            dgvPaseos.Size = new Size(447, 323);
            dgvPaseos.TabIndex = 11;
            dgvPaseos.CellDoubleClick += dgvPaseos_CellDoubleClick;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(5, 9);
            label1.Name = "label1";
            label1.Size = new Size(69, 20);
            label1.TabIndex = 12;
            label1.Text = "Paseador";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(5, 71);
            label2.Name = "label2";
            label2.Size = new Size(45, 20);
            label2.TabIndex = 13;
            label2.Text = "Perro";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(5, 133);
            label3.Name = "label3";
            label3.Size = new Size(155, 20);
            label3.TabIndex = 14;
            label3.Text = "Fecha y hora de inicio";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(5, 195);
            label4.Name = "label4";
            label4.Size = new Size(139, 20);
            label4.TabIndex = 15;
            label4.Text = "Duración (minutos)";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(355, 6);
            label5.Name = "label5";
            label5.Size = new Size(144, 20);
            label5.TabIndex = 16;
            label5.Text = "Filtrar por paseador";
            // 
            // cmbPaseador
            // 
            cmbPaseador.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPaseador.FormattingEnabled = true;
            cmbPaseador.Location = new Point(5, 32);
            cmbPaseador.Name = "cmbPaseador";
            cmbPaseador.Size = new Size(328, 28);
            cmbPaseador.TabIndex = 0;
            // 
            // cmbPerro
            // 
            cmbPerro.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPerro.FormattingEnabled = true;
            cmbPerro.Location = new Point(5, 94);
            cmbPerro.Name = "cmbPerro";
            cmbPerro.Size = new Size(328, 28);
            cmbPerro.TabIndex = 1;
            // 
            // dtpFechaHora
            // 
            dtpFechaHora.CustomFormat = "dd/MM/yyyy HH:mm";
            dtpFechaHora.Format = DateTimePickerFormat.Custom;
            dtpFechaHora.Location = new Point(5, 156);
            dtpFechaHora.Name = "dtpFechaHora";
            dtpFechaHora.Size = new Size(328, 27);
            dtpFechaHora.TabIndex = 2;
            // 
            // nudDuracion
            // 
            nudDuracion.Increment = new decimal(new int[] { 15, 0, 0, 0 });
            nudDuracion.Location = new Point(5, 218);
            nudDuracion.Maximum = new decimal(new int[] { 480, 0, 0, 0 });
            nudDuracion.Minimum = new decimal(new int[] { 15, 0, 0, 0 });
            nudDuracion.Name = "nudDuracion";
            nudDuracion.Size = new Size(328, 27);
            nudDuracion.TabIndex = 3;
            nudDuracion.Value = new decimal(new int[] { 60, 0, 0, 0 });
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(5, 390);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(94, 29);
            btnGuardar.TabIndex = 4;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(119, 390);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(94, 29);
            btnEliminar.TabIndex = 5;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Location = new Point(239, 390);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(94, 29);
            btnLimpiar.TabIndex = 6;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // cmbFiltroPaseador
            // 
            cmbFiltroPaseador.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFiltroPaseador.FormattingEnabled = true;
            cmbFiltroPaseador.Location = new Point(355, 29);
            cmbFiltroPaseador.Name = "cmbFiltroPaseador";
            cmbFiltroPaseador.Size = new Size(260, 28);
            cmbFiltroPaseador.TabIndex = 7;
            // 
            // chkFiltrarFechas
            // 
            chkFiltrarFechas.AutoSize = true;
            chkFiltrarFechas.Location = new Point(355, 65);
            chkFiltrarFechas.Name = "chkFiltrarFechas";
            chkFiltrarFechas.Size = new Size(142, 24);
            chkFiltrarFechas.TabIndex = 8;
            chkFiltrarFechas.Text = "Filtrar por fechas";
            chkFiltrarFechas.UseVisualStyleBackColor = true;
            // 
            // dtpDesde
            // 
            dtpDesde.Format = DateTimePickerFormat.Short;
            dtpDesde.Location = new Point(355, 92);
            dtpDesde.Name = "dtpDesde";
            dtpDesde.Size = new Size(125, 27);
            dtpDesde.TabIndex = 9;
            // 
            // dtpHasta
            // 
            dtpHasta.Format = DateTimePickerFormat.Short;
            dtpHasta.Location = new Point(490, 92);
            dtpHasta.Name = "dtpHasta";
            dtpHasta.Size = new Size(125, 27);
            dtpHasta.TabIndex = 10;
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(675, 29);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(94, 29);
            btnBuscar.TabIndex = 17;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = true;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // btnVerTodos
            // 
            btnVerTodos.Location = new Point(675, 65);
            btnVerTodos.Name = "btnVerTodos";
            btnVerTodos.Size = new Size(94, 29);
            btnVerTodos.TabIndex = 18;
            btnVerTodos.Text = "Ver todos";
            btnVerTodos.UseVisualStyleBackColor = true;
            btnVerTodos.Click += btnVerTodos_Click;
            // 
            // colId
            // 
            colId.DataPropertyName = "Id";
            colId.HeaderText = "ID";
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
            // colPerro
            // 
            colPerro.DataPropertyName = "Perro";
            colPerro.HeaderText = "Perro";
            colPerro.MinimumWidth = 6;
            colPerro.Name = "colPerro";
            colPerro.ReadOnly = true;
            // 
            // colInicio
            // 
            colInicio.DataPropertyName = "Inicio";
            colInicio.HeaderText = "Inicio";
            colInicio.MinimumWidth = 6;
            colInicio.Name = "colInicio";
            colInicio.ReadOnly = true;
            // 
            // colFin
            // 
            colFin.DataPropertyName = "Fin";
            colFin.HeaderText = "Fin";
            colFin.MinimumWidth = 6;
            colFin.Name = "colFin";
            colFin.ReadOnly = true;
            // 
            // colDuracion
            // 
            colDuracion.DataPropertyName = "DuracionMinutos";
            colDuracion.HeaderText = "Minutos";
            colDuracion.MinimumWidth = 6;
            colDuracion.Name = "colDuracion";
            colDuracion.ReadOnly = true;
            // 
            // colPrecio
            // 
            colPrecio.DataPropertyName = "PrecioTotal";
            colPrecio.HeaderText = "Precio";
            colPrecio.MinimumWidth = 6;
            colPrecio.Name = "colPrecio";
            colPrecio.ReadOnly = true;
            // 
            // FormPaseos
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnVerTodos);
            Controls.Add(btnBuscar);
            Controls.Add(dtpHasta);
            Controls.Add(dtpDesde);
            Controls.Add(chkFiltrarFechas);
            Controls.Add(cmbFiltroPaseador);
            Controls.Add(btnLimpiar);
            Controls.Add(btnEliminar);
            Controls.Add(btnGuardar);
            Controls.Add(nudDuracion);
            Controls.Add(dtpFechaHora);
            Controls.Add(cmbPerro);
            Controls.Add(cmbPaseador);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(dgvPaseos);
            Name = "FormPaseos";
            Text = "FormPaseos";
            WindowState = FormWindowState.Maximized;
            Load += FormPaseos_Load;
            ((System.ComponentModel.ISupportInitialize)dgvPaseos).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudDuracion).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvPaseos;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private ComboBox cmbPaseador;
        private ComboBox cmbPerro;
        private DateTimePicker dtpFechaHora;
        private NumericUpDown nudDuracion;
        private Button btnGuardar;
        private Button btnEliminar;
        private Button btnLimpiar;
        private ComboBox cmbFiltroPaseador;
        private CheckBox chkFiltrarFechas;
        private DateTimePicker dtpDesde;
        private DateTimePicker dtpHasta;
        private Button btnBuscar;
        private Button btnVerTodos;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colPaseador;
        private DataGridViewTextBoxColumn colPerro;
        private DataGridViewTextBoxColumn colInicio;
        private DataGridViewTextBoxColumn colFin;
        private DataGridViewTextBoxColumn colDuracion;
        private DataGridViewTextBoxColumn colPrecio;
    }
}
