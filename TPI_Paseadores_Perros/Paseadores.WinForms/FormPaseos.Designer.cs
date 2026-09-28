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
            colId = new DataGridViewTextBoxColumn();
            colPaseador = new DataGridViewTextBoxColumn();
            colPerro = new DataGridViewTextBoxColumn();
            colInicio = new DataGridViewTextBoxColumn();
            colFin = new DataGridViewTextBoxColumn();
            colDuracion = new DataGridViewTextBoxColumn();
            colPrecio = new DataGridViewTextBoxColumn();
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
            dgvPaseos.Location = new Point(311, 94);
            dgvPaseos.Margin = new Padding(3, 2, 3, 2);
            dgvPaseos.Name = "dgvPaseos";
            dgvPaseos.ReadOnly = true;
            dgvPaseos.RowHeadersWidth = 51;
            dgvPaseos.Size = new Size(387, 242);
            dgvPaseos.TabIndex = 11;
            dgvPaseos.CellDoubleClick += dgvPaseos_CellDoubleClick;
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
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(4, 7);
            label1.Name = "label1";
            label1.Size = new Size(55, 15);
            label1.TabIndex = 12;
            label1.Text = "Paseador";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(4, 53);
            label2.Name = "label2";
            label2.Size = new Size(35, 15);
            label2.TabIndex = 13;
            label2.Text = "Perro";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(4, 100);
            label3.Name = "label3";
            label3.Size = new Size(122, 15);
            label3.TabIndex = 14;
            label3.Text = "Fecha y hora de inicio";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(4, 146);
            label4.Name = "label4";
            label4.Size = new Size(110, 15);
            label4.TabIndex = 15;
            label4.Text = "Duración (minutos)";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(311, 4);
            label5.Name = "label5";
            label5.Size = new Size(109, 15);
            label5.TabIndex = 16;
            label5.Text = "Filtrar por paseador";
            // 
            // cmbPaseador
            // 
            cmbPaseador.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPaseador.FormattingEnabled = true;
            cmbPaseador.Location = new Point(4, 24);
            cmbPaseador.Margin = new Padding(3, 2, 3, 2);
            cmbPaseador.Name = "cmbPaseador";
            cmbPaseador.Size = new Size(288, 23);
            cmbPaseador.TabIndex = 0;
            // 
            // cmbPerro
            // 
            cmbPerro.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPerro.FormattingEnabled = true;
            cmbPerro.Location = new Point(4, 70);
            cmbPerro.Margin = new Padding(3, 2, 3, 2);
            cmbPerro.Name = "cmbPerro";
            cmbPerro.Size = new Size(288, 23);
            cmbPerro.TabIndex = 1;
            // 
            // dtpFechaHora
            // 
            dtpFechaHora.CustomFormat = "dd/MM/yyyy HH:mm";
            dtpFechaHora.Format = DateTimePickerFormat.Custom;
            dtpFechaHora.Location = new Point(4, 117);
            dtpFechaHora.Margin = new Padding(3, 2, 3, 2);
            dtpFechaHora.Name = "dtpFechaHora";
            dtpFechaHora.Size = new Size(288, 23);
            dtpFechaHora.TabIndex = 2;
            // 
            // nudDuracion
            // 
            nudDuracion.Increment = new decimal(new int[] { 15, 0, 0, 0 });
            nudDuracion.Location = new Point(4, 164);
            nudDuracion.Margin = new Padding(3, 2, 3, 2);
            nudDuracion.Maximum = new decimal(new int[] { 480, 0, 0, 0 });
            nudDuracion.Minimum = new decimal(new int[] { 15, 0, 0, 0 });
            nudDuracion.Name = "nudDuracion";
            nudDuracion.Size = new Size(287, 23);
            nudDuracion.TabIndex = 3;
            nudDuracion.Value = new decimal(new int[] { 60, 0, 0, 0 });
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(4, 292);
            btnGuardar.Margin = new Padding(3, 2, 3, 2);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(82, 22);
            btnGuardar.TabIndex = 4;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(104, 292);
            btnEliminar.Margin = new Padding(3, 2, 3, 2);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(82, 22);
            btnEliminar.TabIndex = 5;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Location = new Point(209, 292);
            btnLimpiar.Margin = new Padding(3, 2, 3, 2);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(82, 22);
            btnLimpiar.TabIndex = 6;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // cmbFiltroPaseador
            // 
            cmbFiltroPaseador.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFiltroPaseador.FormattingEnabled = true;
            cmbFiltroPaseador.Location = new Point(311, 22);
            cmbFiltroPaseador.Margin = new Padding(3, 2, 3, 2);
            cmbFiltroPaseador.Name = "cmbFiltroPaseador";
            cmbFiltroPaseador.Size = new Size(228, 23);
            cmbFiltroPaseador.TabIndex = 7;
            // 
            // chkFiltrarFechas
            // 
            chkFiltrarFechas.AutoSize = true;
            chkFiltrarFechas.Location = new Point(311, 49);
            chkFiltrarFechas.Margin = new Padding(3, 2, 3, 2);
            chkFiltrarFechas.Name = "chkFiltrarFechas";
            chkFiltrarFechas.Size = new Size(114, 19);
            chkFiltrarFechas.TabIndex = 8;
            chkFiltrarFechas.Text = "Filtrar por fechas";
            chkFiltrarFechas.UseVisualStyleBackColor = true;
            // 
            // dtpDesde
            // 
            dtpDesde.Format = DateTimePickerFormat.Short;
            dtpDesde.Location = new Point(311, 69);
            dtpDesde.Margin = new Padding(3, 2, 3, 2);
            dtpDesde.Name = "dtpDesde";
            dtpDesde.Size = new Size(110, 23);
            dtpDesde.TabIndex = 9;
            // 
            // dtpHasta
            // 
            dtpHasta.Format = DateTimePickerFormat.Short;
            dtpHasta.Location = new Point(429, 69);
            dtpHasta.Margin = new Padding(3, 2, 3, 2);
            dtpHasta.Name = "dtpHasta";
            dtpHasta.Size = new Size(110, 23);
            dtpHasta.TabIndex = 10;
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(591, 22);
            btnBuscar.Margin = new Padding(3, 2, 3, 2);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(82, 22);
            btnBuscar.TabIndex = 17;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = true;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // btnVerTodos
            // 
            btnVerTodos.Location = new Point(591, 49);
            btnVerTodos.Margin = new Padding(3, 2, 3, 2);
            btnVerTodos.Name = "btnVerTodos";
            btnVerTodos.Size = new Size(82, 22);
            btnVerTodos.TabIndex = 18;
            btnVerTodos.Text = "Ver todos";
            btnVerTodos.UseVisualStyleBackColor = true;
            btnVerTodos.Click += btnVerTodos_Click;
            // 
            // FormPaseos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(700, 338);
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
            Margin = new Padding(3, 2, 3, 2);
            Name = "FormPaseos";
            StartPosition = FormStartPosition.CenterParent;
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
