namespace Paseadores.WinForms
{
    partial class FormPaseadores
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
            dgvPaseadores = new DataGridView();
            colId = new DataGridViewTextBoxColumn();
            colNombre = new DataGridViewTextBoxColumn();
            colApellido = new DataGridViewTextBoxColumn();
            colEmail = new DataGridViewTextBoxColumn();
            colTelefono = new DataGridViewTextBoxColumn();
            colTarifa = new DataGridViewTextBoxColumn();
            colZona = new DataGridViewTextBoxColumn();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            txtNombre = new TextBox();
            txtApellido = new TextBox();
            txtEmail = new TextBox();
            txtTelefono = new TextBox();
            txtTarifa = new TextBox();
            btnGuardar = new Button();
            btnEliminar = new Button();
            btnLimpiar = new Button();
            label6 = new Label();
            txtZona = new TextBox();
            label7 = new Label();
            txtContrasena = new TextBox();
            label8 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvPaseadores).BeginInit();
            SuspendLayout();
            // 
            // dgvPaseadores
            // 
            dgvPaseadores.AllowUserToAddRows = false;
            dgvPaseadores.AllowUserToDeleteRows = false;
            dgvPaseadores.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvPaseadores.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPaseadores.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPaseadores.Columns.AddRange(new DataGridViewColumn[] { colId, colNombre, colApellido, colEmail, colTelefono, colTarifa, colZona });
            dgvPaseadores.Location = new Point(311, 2);
            dgvPaseadores.Margin = new Padding(3, 2, 3, 2);
            dgvPaseadores.Name = "dgvPaseadores";
            dgvPaseadores.ReadOnly = true;
            dgvPaseadores.RowHeadersWidth = 51;
            dgvPaseadores.Size = new Size(387, 416);
            dgvPaseadores.TabIndex = 9;
            dgvPaseadores.CellDoubleClick += dgvPaseadores_CellDoubleClick;
            // 
            // colId
            // 
            colId.DataPropertyName = "Id";
            colId.HeaderText = "ID";
            colId.MinimumWidth = 6;
            colId.Name = "colId";
            colId.ReadOnly = true;
            // 
            // colNombre
            // 
            colNombre.DataPropertyName = "Nombre";
            colNombre.HeaderText = "Nombre";
            colNombre.MinimumWidth = 6;
            colNombre.Name = "colNombre";
            colNombre.ReadOnly = true;
            // 
            // colApellido
            // 
            colApellido.DataPropertyName = "Apellido";
            colApellido.HeaderText = "Apellido";
            colApellido.MinimumWidth = 6;
            colApellido.Name = "colApellido";
            colApellido.ReadOnly = true;
            // 
            // colEmail
            // 
            colEmail.DataPropertyName = "Email";
            colEmail.HeaderText = "Email";
            colEmail.MinimumWidth = 6;
            colEmail.Name = "colEmail";
            colEmail.ReadOnly = true;
            // 
            // colTelefono
            // 
            colTelefono.DataPropertyName = "Telefono";
            colTelefono.HeaderText = "Telefono";
            colTelefono.MinimumWidth = 6;
            colTelefono.Name = "colTelefono";
            colTelefono.ReadOnly = true;
            // 
            // colTarifa
            // 
            colTarifa.DataPropertyName = "TarifaPorHora";
            colTarifa.HeaderText = "Tarifa";
            colTarifa.MinimumWidth = 6;
            colTarifa.Name = "colTarifa";
            colTarifa.ReadOnly = true;
            // 
            // colZona
            // 
            colZona.DataPropertyName = "Zona";
            colZona.HeaderText = "Zona";
            colZona.MinimumWidth = 6;
            colZona.Name = "colZona";
            colZona.ReadOnly = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(4, 7);
            label1.Name = "label1";
            label1.Size = new Size(51, 15);
            label1.TabIndex = 10;
            label1.Text = "Nombre";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(4, 53);
            label2.Name = "label2";
            label2.Size = new Size(51, 15);
            label2.TabIndex = 11;
            label2.Text = "Apellido";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(4, 100);
            label3.Name = "label3";
            label3.Size = new Size(36, 15);
            label3.TabIndex = 12;
            label3.Text = "Email";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(4, 143);
            label4.Name = "label4";
            label4.Size = new Size(53, 15);
            label4.TabIndex = 13;
            label4.Text = "Teléfono";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(4, 190);
            label5.Name = "label5";
            label5.Size = new Size(86, 15);
            label5.TabIndex = 14;
            label5.Text = "Tarifa Por Hora";
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(4, 24);
            txtNombre.Margin = new Padding(3, 2, 3, 2);
            txtNombre.Name = "txtNombre";
            txtNombre.PlaceholderText = "Peter";
            txtNombre.Size = new Size(288, 23);
            txtNombre.TabIndex = 0;
            // 
            // txtApellido
            // 
            txtApellido.Location = new Point(4, 70);
            txtApellido.Margin = new Padding(3, 2, 3, 2);
            txtApellido.Name = "txtApellido";
            txtApellido.PlaceholderText = "Parker";
            txtApellido.Size = new Size(288, 23);
            txtApellido.TabIndex = 1;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(4, 117);
            txtEmail.Margin = new Padding(3, 2, 3, 2);
            txtEmail.Name = "txtEmail";
            txtEmail.PlaceholderText = "peter@example.com";
            txtEmail.Size = new Size(288, 23);
            txtEmail.TabIndex = 2;
            // 
            // txtTelefono
            // 
            txtTelefono.Location = new Point(4, 160);
            txtTelefono.Margin = new Padding(3, 2, 3, 2);
            txtTelefono.Name = "txtTelefono";
            txtTelefono.PlaceholderText = "3415555555";
            txtTelefono.Size = new Size(288, 23);
            txtTelefono.TabIndex = 3;
            // 
            // txtTarifa
            // 
            txtTarifa.Location = new Point(4, 207);
            txtTarifa.Margin = new Padding(3, 2, 3, 2);
            txtTarifa.Name = "txtTarifa";
            txtTarifa.PlaceholderText = "150,50";
            txtTarifa.Size = new Size(288, 23);
            txtTarifa.TabIndex = 4;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(4, 387);
            btnGuardar.Margin = new Padding(3, 2, 3, 2);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(82, 22);
            btnGuardar.TabIndex = 6;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(108, 387);
            btnEliminar.Margin = new Padding(3, 2, 3, 2);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(82, 22);
            btnEliminar.TabIndex = 7;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Location = new Point(210, 387);
            btnLimpiar.Margin = new Padding(3, 2, 3, 2);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(82, 22);
            btnLimpiar.TabIndex = 8;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(4, 236);
            label6.Name = "label6";
            label6.Size = new Size(34, 15);
            label6.TabIndex = 15;
            label6.Text = "Zona";
            // 
            // txtZona
            // 
            txtZona.Location = new Point(4, 254);
            txtZona.Margin = new Padding(3, 2, 3, 2);
            txtZona.Name = "txtZona";
            txtZona.PlaceholderText = "Centro";
            txtZona.Size = new Size(288, 23);
            txtZona.TabIndex = 5;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(4, 288);
            label7.Name = "label7";
            label7.Size = new Size(67, 15);
            label7.TabIndex = 16;
            label7.Text = "Contraseña";
            // 
            // txtContrasena
            // 
            txtContrasena.Location = new Point(4, 306);
            txtContrasena.Name = "txtContrasena";
            txtContrasena.PasswordChar = '*';
            txtContrasena.Size = new Size(288, 23);
            txtContrasena.TabIndex = 17;
            txtContrasena.UseSystemPasswordChar = true;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 7F);
            label8.Location = new Point(4, 332);
            label8.Name = "label8";
            label8.Size = new Size(231, 12);
            label8.TabIndex = 18;
            label8.Text = "Al editar, dejar vacio para no cambiar la contraseña ";
            // 
            // FormPaseadores
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(700, 420);
            Controls.Add(label8);
            Controls.Add(txtContrasena);
            Controls.Add(label7);
            Controls.Add(txtZona);
            Controls.Add(label6);
            Controls.Add(btnLimpiar);
            Controls.Add(btnEliminar);
            Controls.Add(btnGuardar);
            Controls.Add(txtTarifa);
            Controls.Add(txtTelefono);
            Controls.Add(txtEmail);
            Controls.Add(txtApellido);
            Controls.Add(txtNombre);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(dgvPaseadores);
            Margin = new Padding(3, 2, 3, 2);
            Name = "FormPaseadores";
            StartPosition = FormStartPosition.CenterParent;
            Text = "FormPaseadores";
            WindowState = FormWindowState.Maximized;
            Load += FormPaseadores_Load;
            ((System.ComponentModel.ISupportInitialize)dgvPaseadores).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvPaseadores;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private TextBox txtNombre;
        private TextBox txtApellido;
        private TextBox txtEmail;
        private TextBox txtTelefono;
        private TextBox txtTarifa;
        private Button btnGuardar;
        private Button btnEliminar;
        private Button btnLimpiar;
        private Label label6;
        private TextBox txtZona;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colNombre;
        private DataGridViewTextBoxColumn colApellido;
        private DataGridViewTextBoxColumn colEmail;
        private DataGridViewTextBoxColumn colTelefono;
        private DataGridViewTextBoxColumn colTarifa;
        private DataGridViewTextBoxColumn colZona;
        private Label label7;
        private TextBox txtContrasena;
        private Label label8;
    }
}