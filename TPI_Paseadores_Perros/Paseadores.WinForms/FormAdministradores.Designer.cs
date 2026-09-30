namespace Paseadores.WinForms
{
    partial class FormAdministradores
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
            txtTelefono = new TextBox();
            txtEmail = new TextBox();
            txtApellido = new TextBox();
            txtNombre = new TextBox();
            label4 = new Label();
            label2 = new Label();
            label1 = new Label();
            label3 = new Label();
            dgvAdministradores = new DataGridView();
            colId = new DataGridViewTextBoxColumn();
            colNombre = new DataGridViewTextBoxColumn();
            colApellido = new DataGridViewTextBoxColumn();
            colEmail = new DataGridViewTextBoxColumn();
            colTelefono = new DataGridViewTextBoxColumn();
            label6 = new Label();
            txtContrasena = new TextBox();
            label8 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvAdministradores).BeginInit();
            SuspendLayout();
            //
            // btnLimpiar
            //
            btnLimpiar.Location = new Point(207, 290);
            btnLimpiar.Margin = new Padding(3, 2, 3, 2);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(82, 22);
            btnLimpiar.TabIndex = 7;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            //
            // btnEliminar
            //
            btnEliminar.Location = new Point(103, 290);
            btnEliminar.Margin = new Padding(3, 2, 3, 2);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(82, 22);
            btnEliminar.TabIndex = 6;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            //
            // btnGuardar
            //
            btnGuardar.Location = new Point(2, 290);
            btnGuardar.Margin = new Padding(3, 2, 3, 2);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(82, 22);
            btnGuardar.TabIndex = 5;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            //
            // txtTelefono
            //
            txtTelefono.Location = new Point(1, 160);
            txtTelefono.Margin = new Padding(3, 2, 3, 2);
            txtTelefono.Name = "txtTelefono";
            txtTelefono.PlaceholderText = "3415555555";
            txtTelefono.Size = new Size(288, 23);
            txtTelefono.TabIndex = 3;
            //
            // txtEmail
            //
            txtEmail.Location = new Point(2, 117);
            txtEmail.Margin = new Padding(3, 2, 3, 2);
            txtEmail.Name = "txtEmail";
            txtEmail.PlaceholderText = "admin2@paseos.com";
            txtEmail.Size = new Size(288, 23);
            txtEmail.TabIndex = 2;
            //
            // txtApellido
            //
            txtApellido.Location = new Point(2, 70);
            txtApellido.Margin = new Padding(3, 2, 3, 2);
            txtApellido.Name = "txtApellido";
            txtApellido.PlaceholderText = "Parker";
            txtApellido.Size = new Size(288, 23);
            txtApellido.TabIndex = 1;
            //
            // txtNombre
            //
            txtNombre.Location = new Point(2, 24);
            txtNombre.Margin = new Padding(3, 2, 3, 2);
            txtNombre.Name = "txtNombre";
            txtNombre.PlaceholderText = "Peter";
            txtNombre.Size = new Size(288, 23);
            txtNombre.TabIndex = 0;
            //
            // label4
            //
            label4.AutoSize = true;
            label4.Location = new Point(1, 143);
            label4.Name = "label4";
            label4.Size = new Size(53, 15);
            label4.TabIndex = 11;
            label4.Text = "Teléfono";
            //
            // label2
            //
            label2.AutoSize = true;
            label2.Location = new Point(2, 53);
            label2.Name = "label2";
            label2.Size = new Size(51, 15);
            label2.TabIndex = 9;
            label2.Text = "Apellido";
            //
            // label1
            //
            label1.AutoSize = true;
            label1.Location = new Point(2, 7);
            label1.Name = "label1";
            label1.Size = new Size(51, 15);
            label1.TabIndex = 8;
            label1.Text = "Nombre";
            //
            // label3
            //
            label3.AutoSize = true;
            label3.Location = new Point(2, 100);
            label3.Name = "label3";
            label3.Size = new Size(36, 15);
            label3.TabIndex = 10;
            label3.Text = "Email";
            //
            // dgvAdministradores
            //
            dgvAdministradores.AllowUserToAddRows = false;
            dgvAdministradores.AllowUserToDeleteRows = false;
            dgvAdministradores.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvAdministradores.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAdministradores.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAdministradores.Columns.AddRange(new DataGridViewColumn[] { colId, colNombre, colApellido, colEmail, colTelefono });
            dgvAdministradores.Location = new Point(308, 2);
            dgvAdministradores.Margin = new Padding(3, 2, 3, 2);
            dgvAdministradores.Name = "dgvAdministradores";
            dgvAdministradores.ReadOnly = true;
            dgvAdministradores.RowHeadersWidth = 51;
            dgvAdministradores.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAdministradores.Size = new Size(391, 362);
            dgvAdministradores.TabIndex = 14;
            dgvAdministradores.CellDoubleClick += dgvAdministradores_CellDoubleClick;
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
            // label6
            //
            label6.AutoSize = true;
            label6.Location = new Point(2, 190);
            label6.Name = "label6";
            label6.Size = new Size(67, 15);
            label6.TabIndex = 12;
            label6.Text = "Contraseña";
            //
            // txtContrasena
            //
            txtContrasena.Location = new Point(2, 208);
            txtContrasena.Name = "txtContrasena";
            txtContrasena.PasswordChar = '*';
            txtContrasena.Size = new Size(287, 23);
            txtContrasena.TabIndex = 4;
            txtContrasena.UseSystemPasswordChar = true;
            //
            // label8
            //
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 7F);
            label8.Location = new Point(2, 234);
            label8.Name = "label8";
            label8.Size = new Size(231, 12);
            label8.TabIndex = 13;
            label8.Text = "Al editar, dejar vacio para no cambiar la contraseña ";
            //
            // FormAdministradores
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(700, 366);
            Controls.Add(label8);
            Controls.Add(txtContrasena);
            Controls.Add(label6);
            Controls.Add(dgvAdministradores);
            Controls.Add(btnLimpiar);
            Controls.Add(btnEliminar);
            Controls.Add(btnGuardar);
            Controls.Add(txtTelefono);
            Controls.Add(txtEmail);
            Controls.Add(txtApellido);
            Controls.Add(txtNombre);
            Controls.Add(label4);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(label3);
            Margin = new Padding(3, 2, 3, 2);
            Name = "FormAdministradores";
            Text = "Administradores";
            WindowState = FormWindowState.Maximized;
            Load += FormAdministradores_Load;
            ((System.ComponentModel.ISupportInitialize)dgvAdministradores).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnLimpiar;
        private Button btnEliminar;
        private Button btnGuardar;
        private TextBox txtTelefono;
        private TextBox txtEmail;
        private TextBox txtApellido;
        private TextBox txtNombre;
        private Label label4;
        private Label label2;
        private Label label1;
        private Label label3;
        private DataGridView dgvAdministradores;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colNombre;
        private DataGridViewTextBoxColumn colApellido;
        private DataGridViewTextBoxColumn colEmail;
        private DataGridViewTextBoxColumn colTelefono;
        private Label label6;
        private TextBox txtContrasena;
        private Label label8;
    }
}
