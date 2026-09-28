namespace Paseadores.WinForms
{
    partial class FormDueños
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
            btnLimpiar = new Button();
            btnEliminar = new Button();
            btnGuardar = new Button();
            txtDireccion = new TextBox();
            txtTelefono = new TextBox();
            txtEmail = new TextBox();
            txtApellido = new TextBox();
            txtNombre = new TextBox();
            label5 = new Label();
            label4 = new Label();
            label2 = new Label();
            label1 = new Label();
            label3 = new Label();
            dgvDueños = new DataGridView();
            colId = new DataGridViewTextBoxColumn();
            colNombre = new DataGridViewTextBoxColumn();
            colApellido = new DataGridViewTextBoxColumn();
            colEmail = new DataGridViewTextBoxColumn();
            colTelefono = new DataGridViewTextBoxColumn();
            colDireccion = new DataGridViewTextBoxColumn();
            label6 = new Label();
            txtContrasena = new TextBox();
            label8 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvDueños).BeginInit();
            SuspendLayout();
            // 
            // btnLimpiar
            // 
            btnLimpiar.Location = new Point(207, 333);
            btnLimpiar.Margin = new Padding(3, 2, 3, 2);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(82, 22);
            btnLimpiar.TabIndex = 24;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(103, 333);
            btnEliminar.Margin = new Padding(3, 2, 3, 2);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(82, 22);
            btnEliminar.TabIndex = 23;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(2, 333);
            btnGuardar.Margin = new Padding(3, 2, 3, 2);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(82, 22);
            btnGuardar.TabIndex = 22;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // txtDireccion
            // 
            txtDireccion.Location = new Point(1, 207);
            txtDireccion.Margin = new Padding(3, 2, 3, 2);
            txtDireccion.Name = "txtDireccion";
            txtDireccion.PlaceholderText = "Sarmiento 1786";
            txtDireccion.Size = new Size(288, 23);
            txtDireccion.TabIndex = 20;
            // 
            // txtTelefono
            // 
            txtTelefono.Location = new Point(1, 160);
            txtTelefono.Margin = new Padding(3, 2, 3, 2);
            txtTelefono.Name = "txtTelefono";
            txtTelefono.PlaceholderText = "3415555555";
            txtTelefono.Size = new Size(288, 23);
            txtTelefono.TabIndex = 19;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(2, 117);
            txtEmail.Margin = new Padding(3, 2, 3, 2);
            txtEmail.Name = "txtEmail";
            txtEmail.PlaceholderText = "peter@example.com";
            txtEmail.Size = new Size(288, 23);
            txtEmail.TabIndex = 18;
            // 
            // txtApellido
            // 
            txtApellido.Location = new Point(2, 70);
            txtApellido.Margin = new Padding(3, 2, 3, 2);
            txtApellido.Name = "txtApellido";
            txtApellido.PlaceholderText = "Parker";
            txtApellido.Size = new Size(288, 23);
            txtApellido.TabIndex = 17;
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(2, 24);
            txtNombre.Margin = new Padding(3, 2, 3, 2);
            txtNombre.Name = "txtNombre";
            txtNombre.PlaceholderText = "Peter";
            txtNombre.Size = new Size(288, 23);
            txtNombre.TabIndex = 16;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(1, 190);
            label5.Name = "label5";
            label5.Size = new Size(57, 15);
            label5.TabIndex = 30;
            label5.Text = "Dirección";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(1, 143);
            label4.Name = "label4";
            label4.Size = new Size(53, 15);
            label4.TabIndex = 29;
            label4.Text = "Teléfono";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(2, 53);
            label2.Name = "label2";
            label2.Size = new Size(51, 15);
            label2.TabIndex = 27;
            label2.Text = "Apellido";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(2, 7);
            label1.Name = "label1";
            label1.Size = new Size(51, 15);
            label1.TabIndex = 26;
            label1.Text = "Nombre";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(2, 100);
            label3.Name = "label3";
            label3.Size = new Size(36, 15);
            label3.TabIndex = 28;
            label3.Text = "Email";
            // 
            // dgvDueños
            // 
            dgvDueños.AllowUserToAddRows = false;
            dgvDueños.AllowUserToDeleteRows = false;
            dgvDueños.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvDueños.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDueños.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDueños.Columns.AddRange(new DataGridViewColumn[] { colId, colNombre, colApellido, colEmail, colTelefono, colDireccion });
            dgvDueños.Location = new Point(308, 2);
            dgvDueños.Margin = new Padding(3, 2, 3, 2);
            dgvDueños.Name = "dgvDueños";
            dgvDueños.ReadOnly = true;
            dgvDueños.RowHeadersWidth = 51;
            dgvDueños.Size = new Size(391, 362);
            dgvDueños.TabIndex = 25;
            dgvDueños.CellDoubleClick += dgvDueños_CellDoubleClick;
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
            // colDireccion
            // 
            colDireccion.DataPropertyName = "Direccion";
            colDireccion.HeaderText = "Direccion";
            colDireccion.MinimumWidth = 6;
            colDireccion.Name = "colDireccion";
            colDireccion.ReadOnly = true;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(2, 237);
            label6.Name = "label6";
            label6.Size = new Size(67, 15);
            label6.TabIndex = 31;
            label6.Text = "Contraseña";
            // 
            // txtContrasena
            // 
            txtContrasena.Location = new Point(2, 255);
            txtContrasena.Name = "txtContrasena";
            txtContrasena.PasswordChar = '*';
            txtContrasena.Size = new Size(287, 23);
            txtContrasena.TabIndex = 32;
            txtContrasena.UseSystemPasswordChar = true;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 7F);
            label8.Location = new Point(2, 281);
            label8.Name = "label8";
            label8.Size = new Size(231, 12);
            label8.TabIndex = 33;
            label8.Text = "Al editar, dejar vacio para no cambiar la contraseña ";
            // 
            // FormDueños
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(700, 366);
            Controls.Add(label8);
            Controls.Add(txtContrasena);
            Controls.Add(label6);
            Controls.Add(btnLimpiar);
            Controls.Add(btnEliminar);
            Controls.Add(btnGuardar);
            Controls.Add(txtDireccion);
            Controls.Add(txtTelefono);
            Controls.Add(txtEmail);
            Controls.Add(txtApellido);
            Controls.Add(txtNombre);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(label3);
            Controls.Add(dgvDueños);
            Margin = new Padding(3, 2, 3, 2);
            Name = "FormDueños";
            StartPosition = FormStartPosition.CenterParent;
            Text = "FormDueños";
            WindowState = FormWindowState.Maximized;
            Load += FormDueños_Load;
            ((System.ComponentModel.ISupportInitialize)dgvDueños).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnLimpiar;
        private Button btnEliminar;
        private Button btnGuardar;
        private TextBox txtDireccion;
        private TextBox txtTelefono;
        private TextBox txtEmail;
        private TextBox txtApellido;
        private TextBox txtNombre;
        private Label label5;
        private Label label4;
        private Label label2;
        private Label label1;
        private Label label3;
        private DataGridView dgvDueños;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colNombre;
        private DataGridViewTextBoxColumn colApellido;
        private DataGridViewTextBoxColumn colEmail;
        private DataGridViewTextBoxColumn colTelefono;
        private DataGridViewTextBoxColumn colDireccion;
        private Label label6;
        private TextBox txtContrasena;
        private Label label8;
    }
}