namespace Paseadores.WinForms
{
    partial class FormPerros
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
            dgvPerros = new DataGridView();
            colId = new DataGridViewTextBoxColumn();
            colDueno = new DataGridViewTextBoxColumn();
            colNombre = new DataGridViewTextBoxColumn();
            colRaza = new DataGridViewTextBoxColumn();
            colEdad = new DataGridViewTextBoxColumn();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            cmbDueno = new ComboBox();
            txtNombre = new TextBox();
            txtRaza = new TextBox();
            nudEdad = new NumericUpDown();
            btnGuardar = new Button();
            btnEliminar = new Button();
            btnLimpiar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvPerros).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudEdad).BeginInit();
            SuspendLayout();
            // 
            // dgvPerros
            // 
            dgvPerros.AllowUserToAddRows = false;
            dgvPerros.AllowUserToDeleteRows = false;
            dgvPerros.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvPerros.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPerros.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPerros.Columns.AddRange(new DataGridViewColumn[] { colId, colDueno, colNombre, colRaza, colEdad });
            dgvPerros.Location = new Point(311, 2);
            dgvPerros.Margin = new Padding(3, 2, 3, 2);
            dgvPerros.Name = "dgvPerros";
            dgvPerros.ReadOnly = true;
            dgvPerros.RowHeadersWidth = 51;
            dgvPerros.Size = new Size(388, 334);
            dgvPerros.TabIndex = 5;
            dgvPerros.CellDoubleClick += dgvPerros_CellDoubleClick;
            // 
            // colId
            // 
            colId.DataPropertyName = "Id";
            colId.HeaderText = "ID";
            colId.MinimumWidth = 6;
            colId.Name = "colId";
            colId.ReadOnly = true;
            // 
            // colDueno
            // 
            colDueno.DataPropertyName = "Dueno";
            colDueno.HeaderText = "Dueño";
            colDueno.MinimumWidth = 6;
            colDueno.Name = "colDueno";
            colDueno.ReadOnly = true;
            // 
            // colNombre
            // 
            colNombre.DataPropertyName = "Nombre";
            colNombre.HeaderText = "Nombre";
            colNombre.MinimumWidth = 6;
            colNombre.Name = "colNombre";
            colNombre.ReadOnly = true;
            // 
            // colRaza
            // 
            colRaza.DataPropertyName = "Raza";
            colRaza.HeaderText = "Raza";
            colRaza.MinimumWidth = 6;
            colRaza.Name = "colRaza";
            colRaza.ReadOnly = true;
            // 
            // colEdad
            // 
            colEdad.DataPropertyName = "Edad";
            colEdad.HeaderText = "Edad";
            colEdad.MinimumWidth = 6;
            colEdad.Name = "colEdad";
            colEdad.ReadOnly = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(4, 7);
            label1.Name = "label1";
            label1.Size = new Size(42, 15);
            label1.TabIndex = 6;
            label1.Text = "Dueño";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(4, 53);
            label2.Name = "label2";
            label2.Size = new Size(51, 15);
            label2.TabIndex = 7;
            label2.Text = "Nombre";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(4, 100);
            label3.Name = "label3";
            label3.Size = new Size(31, 15);
            label3.TabIndex = 8;
            label3.Text = "Raza";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(4, 146);
            label4.Name = "label4";
            label4.Size = new Size(33, 15);
            label4.TabIndex = 9;
            label4.Text = "Edad";
            // 
            // cmbDueno
            // 
            cmbDueno.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDueno.FormattingEnabled = true;
            cmbDueno.Location = new Point(4, 24);
            cmbDueno.Margin = new Padding(3, 2, 3, 2);
            cmbDueno.Name = "cmbDueno";
            cmbDueno.Size = new Size(288, 23);
            cmbDueno.TabIndex = 0;
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(4, 70);
            txtNombre.Margin = new Padding(3, 2, 3, 2);
            txtNombre.Name = "txtNombre";
            txtNombre.PlaceholderText = "Firulais";
            txtNombre.Size = new Size(288, 23);
            txtNombre.TabIndex = 1;
            // 
            // txtRaza
            // 
            txtRaza.Location = new Point(4, 117);
            txtRaza.Margin = new Padding(3, 2, 3, 2);
            txtRaza.Name = "txtRaza";
            txtRaza.PlaceholderText = "Caniche";
            txtRaza.Size = new Size(288, 23);
            txtRaza.TabIndex = 2;
            // 
            // nudEdad
            // 
            nudEdad.Location = new Point(4, 164);
            nudEdad.Margin = new Padding(3, 2, 3, 2);
            nudEdad.Maximum = new decimal(new int[] { 30, 0, 0, 0 });
            nudEdad.Name = "nudEdad";
            nudEdad.Size = new Size(287, 23);
            nudEdad.TabIndex = 3;
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
            btnEliminar.TabIndex = 10;
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
            btnLimpiar.TabIndex = 11;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // FormPerros
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(700, 338);
            Controls.Add(btnLimpiar);
            Controls.Add(btnEliminar);
            Controls.Add(btnGuardar);
            Controls.Add(nudEdad);
            Controls.Add(txtRaza);
            Controls.Add(txtNombre);
            Controls.Add(cmbDueno);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(dgvPerros);
            Margin = new Padding(3, 2, 3, 2);
            Name = "FormPerros";
            StartPosition = FormStartPosition.CenterParent;
            Text = "FormPerros";
            WindowState = FormWindowState.Maximized;
            Load += FormPerros_Load;
            ((System.ComponentModel.ISupportInitialize)dgvPerros).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudEdad).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvPerros;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private ComboBox cmbDueno;
        private TextBox txtNombre;
        private TextBox txtRaza;
        private NumericUpDown nudEdad;
        private Button btnGuardar;
        private Button btnEliminar;
        private Button btnLimpiar;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colDueno;
        private DataGridViewTextBoxColumn colNombre;
        private DataGridViewTextBoxColumn colRaza;
        private DataGridViewTextBoxColumn colEdad;
    }
}
