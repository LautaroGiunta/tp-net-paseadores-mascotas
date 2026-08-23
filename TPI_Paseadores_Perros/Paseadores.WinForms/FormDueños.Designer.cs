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
            ((System.ComponentModel.ISupportInitialize)dgvDueños).BeginInit();
            SuspendLayout();
            // 
            // btnLimpiar
            // 
            btnLimpiar.Location = new Point(236, 390);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(94, 29);
            btnLimpiar.TabIndex = 24;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(116, 390);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(94, 29);
            btnEliminar.TabIndex = 23;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(1, 390);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(94, 29);
            btnGuardar.TabIndex = 22;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // txtDireccion
            // 
            txtDireccion.Location = new Point(1, 276);
            txtDireccion.Name = "txtDireccion";
            txtDireccion.PlaceholderText = "Sarmiento 1786";
            txtDireccion.Size = new Size(329, 27);
            txtDireccion.TabIndex = 20;
            // 
            // txtTelefono
            // 
            txtTelefono.Location = new Point(1, 214);
            txtTelefono.Name = "txtTelefono";
            txtTelefono.PlaceholderText = "3415555555";
            txtTelefono.Size = new Size(329, 27);
            txtTelefono.TabIndex = 19;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(2, 156);
            txtEmail.Name = "txtEmail";
            txtEmail.PlaceholderText = "peter@example.com";
            txtEmail.Size = new Size(328, 27);
            txtEmail.TabIndex = 18;
            // 
            // txtApellido
            // 
            txtApellido.Location = new Point(2, 94);
            txtApellido.Name = "txtApellido";
            txtApellido.PlaceholderText = "Parker";
            txtApellido.Size = new Size(328, 27);
            txtApellido.TabIndex = 17;
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(2, 32);
            txtNombre.Name = "txtNombre";
            txtNombre.PlaceholderText = "Peter";
            txtNombre.Size = new Size(328, 27);
            txtNombre.TabIndex = 16;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(1, 253);
            label5.Name = "label5";
            label5.Size = new Size(72, 20);
            label5.TabIndex = 30;
            label5.Text = "Dirección";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(1, 191);
            label4.Name = "label4";
            label4.Size = new Size(67, 20);
            label4.TabIndex = 29;
            label4.Text = "Teléfono";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(2, 71);
            label2.Name = "label2";
            label2.Size = new Size(66, 20);
            label2.TabIndex = 27;
            label2.Text = "Apellido";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(2, 9);
            label1.Name = "label1";
            label1.Size = new Size(64, 20);
            label1.TabIndex = 26;
            label1.Text = "Nombre";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(2, 133);
            label3.Name = "label3";
            label3.Size = new Size(46, 20);
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
            dgvDueños.Location = new Point(352, 3);
            dgvDueños.Name = "dgvDueños";
            dgvDueños.ReadOnly = true;
            dgvDueños.RowHeadersWidth = 51;
            dgvDueños.Size = new Size(447, 445);
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
            // FormDueños
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
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
            Name = "FormDueños";
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
    }
}