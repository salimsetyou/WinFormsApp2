namespace WinFormsApp2.View
{
    partial class Lahan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Lahan));
            txtLokasi = new TextBox();
            txtNamaLahan = new TextBox();
            txtLuas = new TextBox();
            txtIdLahan = new TextBox();
            cmbJenis = new ComboBox();
            btnHapus = new Button();
            btnEdit = new Button();
            btnSimpan = new Button();
            dgvLahan = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvLahan).BeginInit();
            SuspendLayout();
            // 
            // txtLokasi
            // 
            txtLokasi.Location = new Point(619, 107);
            txtLokasi.Margin = new Padding(5, 5, 5, 5);
            txtLokasi.Name = "txtLokasi";
            txtLokasi.PlaceholderText = "Masukkan Lokasi Lahan";
            txtLokasi.Size = new Size(201, 39);
            txtLokasi.TabIndex = 0;
            // 
            // txtNamaLahan
            // 
            txtNamaLahan.Location = new Point(102, 190);
            txtNamaLahan.Margin = new Padding(5, 5, 5, 5);
            txtNamaLahan.Name = "txtNamaLahan";
            txtNamaLahan.PlaceholderText = "Masukkan Nama Lahan";
            txtNamaLahan.Size = new Size(201, 39);
            txtNamaLahan.TabIndex = 2;
            // 
            // txtLuas
            // 
            txtLuas.Location = new Point(354, 190);
            txtLuas.Margin = new Padding(5, 5, 5, 5);
            txtLuas.Name = "txtLuas";
            txtLuas.PlaceholderText = "Luas (Ha)";
            txtLuas.Size = new Size(201, 39);
            txtLuas.TabIndex = 3;
            // 
            // txtIdLahan
            // 
            txtIdLahan.Location = new Point(102, 107);
            txtIdLahan.Margin = new Padding(5, 5, 5, 5);
            txtIdLahan.Name = "txtIdLahan";
            txtIdLahan.PlaceholderText = "ID Lahan";
            txtIdLahan.Size = new Size(201, 39);
            txtIdLahan.TabIndex = 4;
            // 
            // cmbJenis
            // 
            cmbJenis.FormattingEnabled = true;
            cmbJenis.Items.AddRange(new object[] { "Andosol", "Latosol", "Gersang", "Regosol" });
            cmbJenis.Location = new Point(354, 107);
            cmbJenis.Margin = new Padding(5, 5, 5, 5);
            cmbJenis.Name = "cmbJenis";
            cmbJenis.Size = new Size(201, 40);
            cmbJenis.TabIndex = 5;
            cmbJenis.Text = "Jenis";
            // 
            // btnHapus
            // 
            btnHapus.Location = new Point(754, 293);
            btnHapus.Margin = new Padding(5, 5, 5, 5);
            btnHapus.Name = "btnHapus";
            btnHapus.Size = new Size(153, 46);
            btnHapus.TabIndex = 7;
            btnHapus.Text = "HAPUS";
            btnHapus.UseVisualStyleBackColor = true;
            btnHapus.Click += btnHapus_Click;
            // 
            // btnEdit
            // 
            btnEdit.Location = new Point(546, 293);
            btnEdit.Margin = new Padding(5, 5, 5, 5);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(153, 46);
            btnEdit.TabIndex = 8;
            btnEdit.Text = "EDIT";
            btnEdit.UseVisualStyleBackColor = true;
            btnEdit.Click += btnEdit_Click;
            // 
            // btnSimpan
            // 
            btnSimpan.Location = new Point(336, 293);
            btnSimpan.Margin = new Padding(5, 5, 5, 5);
            btnSimpan.Name = "btnSimpan";
            btnSimpan.Size = new Size(153, 46);
            btnSimpan.TabIndex = 9;
            btnSimpan.Text = "SIMPAN";
            btnSimpan.UseVisualStyleBackColor = true;
            btnSimpan.Click += btnSimpan_Click;
            // 
            // dgvLahan
            // 
            dgvLahan.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLahan.Location = new Point(102, 370);
            dgvLahan.Margin = new Padding(5, 5, 5, 5);
            dgvLahan.Name = "dgvLahan";
            dgvLahan.RowHeadersWidth = 51;
            dgvLahan.Size = new Size(804, 301);
            dgvLahan.TabIndex = 10;
            dgvLahan.CellClick += dgvLahan_CellClick;
            // 
            // Lahan
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1017, 720);
            Controls.Add(dgvLahan);
            Controls.Add(btnSimpan);
            Controls.Add(btnEdit);
            Controls.Add(btnHapus);
            Controls.Add(cmbJenis);
            Controls.Add(txtIdLahan);
            Controls.Add(txtLuas);
            Controls.Add(txtNamaLahan);
            Controls.Add(txtLokasi);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(5, 5, 5, 5);
            Name = "Lahan";
            Text = "Form1";
            Load += Lahan_Load;
            ((System.ComponentModel.ISupportInitialize)dgvLahan).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        public TextBox txtLokasi;
        public TextBox txtNamaLahan;
        public TextBox txtLuas;
        public TextBox txtIdLahan;
        public ComboBox cmbJenis;
        private Button btnSimpan;
        public DataGridView dgvLahan;
        public Button btnHapus;
        public Button btnEdit;
        public Button btnReset;
    }
}