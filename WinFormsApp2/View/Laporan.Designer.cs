    namespace WinFormsApp2.View
{
    partial class Laporan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Laporan));
            txtPetugas = new TextBox();
            btnTampilkanLaporan = new Button();
            dgvLaporan = new DataGridView();
            dateTimePicker1 = new DateTimePicker();
            btnHapus = new Button();
            btnRefresh = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvLaporan).BeginInit();
            SuspendLayout();
            // 
            // txtPetugas
            // 
            txtPetugas.Location = new Point(44, 74);
            txtPetugas.Name = "txtPetugas";
            txtPetugas.PlaceholderText = "Nama Petugas";
            txtPetugas.Size = new Size(125, 27);
            txtPetugas.TabIndex = 2;
            // 
            // btnTampilkanLaporan
            // 
            btnTampilkanLaporan.Location = new Point(225, 134);
            btnTampilkanLaporan.Name = "btnTampilkanLaporan";
            btnTampilkanLaporan.Size = new Size(180, 29);
            btnTampilkanLaporan.TabIndex = 5;
            btnTampilkanLaporan.Text = "TAMPILKAN  LAPORAN";
            btnTampilkanLaporan.UseVisualStyleBackColor = true;
            // 
            // dgvLaporan
            // 
            dgvLaporan.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLaporan.Location = new Point(44, 182);
            dgvLaporan.Name = "dgvLaporan";
            dgvLaporan.RowHeadersWidth = 51;
            dgvLaporan.Size = new Size(521, 247);
            dgvLaporan.TabIndex = 7;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(238, 72);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(250, 27);
            dateTimePicker1.TabIndex = 9;
            // 
            // btnHapus
            // 
            btnHapus.Location = new Point(38, 130);
            btnHapus.Name = "btnHapus";
            btnHapus.Size = new Size(79, 26);
            btnHapus.TabIndex = 10;
            btnHapus.Text = "HAPUS";
            btnHapus.UseVisualStyleBackColor = true;
            btnHapus.Click += BtnHapus_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(123, 127);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(83, 33);
            btnRefresh.TabIndex = 11;
            btnRefresh.Text = "REFRESH";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += BtnRefresh_Click;
            // 
            // Laporan
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(632, 450);
            Controls.Add(btnRefresh);
            Controls.Add(btnHapus);
            Controls.Add(dateTimePicker1);
            Controls.Add(dgvLaporan);
            Controls.Add(btnTampilkanLaporan);
            Controls.Add(txtPetugas);
            DoubleBuffered = true;
            Name = "Laporan";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dgvLaporan).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtPetugas;
        private Button button1;
        private Button button2;
        private Button btnTampilkanLaporan;
        public DataGridView dgvLaporan;
        private DateTimePicker periodeawal;
        private DateTimePicker dateTimePicker1;
        private Button btnHapus;
        private Button btnRefresh;
    }
}