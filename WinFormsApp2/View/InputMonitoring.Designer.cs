namespace WinFormsApp2.View
{
    partial class InputMonitoring
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InputMonitoring));
            panel1 = new Panel();
            textBox2 = new TextBox();
            comboBox2 = new ComboBox();
            comboBox1 = new ComboBox();
            dateTimePicker1 = new DateTimePicker();
            textBox1 = new TextBox();
            textBoxId = new TextBox();
            comboBoxTanaman = new ComboBox();
            comboBoxPetugas = new ComboBox();
            dataGridView1 = new DataGridView();
            btnSave = new Button();
            btnEdit = new Button();
            btnReset = new Button();
            button1 = new Button();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(textBox2);
            panel1.Controls.Add(comboBox2);
            panel1.Controls.Add(comboBox1);
            panel1.Controls.Add(dateTimePicker1);
            panel1.Controls.Add(textBox1);
            panel1.Controls.Add(textBoxId);
            panel1.Controls.Add(comboBoxTanaman);
            panel1.Controls.Add(comboBoxPetugas);
            panel1.Location = new Point(22, 25);
            panel1.Name = "panel1";
            panel1.Size = new Size(473, 129);
            panel1.TabIndex = 0;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(287, 23);
            textBox2.Name = "textBox2";
            textBox2.PlaceholderText = "catatan";
            textBox2.Size = new Size(125, 27);
            textBox2.TabIndex = 11;
            // 
            // comboBox2
            // 
            comboBox2.Items.AddRange(new object[] { "Cerah", "Hujan" });
            comboBox2.Location = new Point(146, 59);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(125, 28);
            comboBox2.TabIndex = 10;
            comboBox2.Text = " Cuaca";
            // 
            // comboBox1
            // 
            comboBox1.Items.AddRange(new object[] { "Ulat", "Tidak Ada" });
            comboBox1.Location = new Point(147, 93);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(125, 28);
            comboBox1.TabIndex = 9;
            comboBox1.Text = "Hama";
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(16, 57);
            dateTimePicker1.Margin = new Padding(2);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(125, 27);
            dateTimePicker1.TabIndex = 8;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(16, 22);
            textBox1.Name = "textBox1";
            textBox1.PlaceholderText = "Id Monitoring";
            textBox1.Size = new Size(125, 27);
            textBox1.TabIndex = 0;
            // 
            // textBoxId
            // 
            textBoxId.Location = new Point(0, 0);
            textBoxId.Name = "textBoxId";
            textBoxId.Size = new Size(10, 27);
            textBoxId.TabIndex = 6;
            textBoxId.Visible = false;
            // 
            // comboBoxTanaman
            // 
            comboBoxTanaman.Items.AddRange(new object[] { "Kopi", "Kakao" });
            comboBoxTanaman.Location = new Point(16, 89);
            comboBoxTanaman.Name = "comboBoxTanaman";
            comboBoxTanaman.Size = new Size(125, 28);
            comboBoxTanaman.TabIndex = 6;
            comboBoxTanaman.Text = "Jenis tanaman";
            // 
            // comboBoxPetugas
            // 
            comboBoxPetugas.Items.AddRange(new object[] { "Sehat", "Layu" });
            comboBoxPetugas.Location = new Point(147, 22);
            comboBoxPetugas.Name = "comboBoxPetugas";
            comboBoxPetugas.Size = new Size(125, 28);
            comboBoxPetugas.TabIndex = 7;
            comboBoxPetugas.Text = "Kondisi";
            comboBoxPetugas.SelectedIndexChanged += comboBoxPetugas_SelectedIndexChanged;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(22, 220);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(473, 199);
            dataGridView1.TabIndex = 1;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(68, 172);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 30);
            btnSave.TabIndex = 0;
            btnSave.Text = "Simpan";
            btnSave.Click += btnSave_Click;
            // 
            // btnEdit
            // 
            btnEdit.Enabled = false;
            btnEdit.Location = new Point(169, 172);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(75, 30);
            btnEdit.TabIndex = 1;
            btnEdit.Text = "Ubah";
            btnEdit.Click += btnEdit_Click;
            // 
            // btnReset
            // 
            btnReset.Location = new Point(270, 172);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(75, 30);
            btnReset.TabIndex = 2;
            btnReset.Text = "Reset";
            btnReset.Click += btnReset_Click;
            // 
            // button1
            // 
            button1.Location = new Point(374, 172);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 3;
            button1.Text = "hapus";
            button1.UseVisualStyleBackColor = true;
            button1.Click += Button1_Click;
            // 
            // InputMonitoring
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(800, 450);
            Controls.Add(button1);
            Controls.Add(btnSave);
            Controls.Add(btnEdit);
            Controls.Add(btnReset);
            Controls.Add(dataGridView1);
            Controls.Add(panel1);
            DoubleBuffered = true;
            Name = "InputMonitoring";
            Text = "Monitoring";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        public TextBox textBox1;
        public DataGridView dataGridView1;
        public TextBox textBoxId;
        public ComboBox comboBoxTanaman;
        public ComboBox comboBoxPetugas;
        public Button btnSave;
        public Button btnEdit;
        public Button btnReset;
        public ComboBox comboBox2;
        public ComboBox comboBox1;
        public DateTimePicker dateTimePicker1;
        public TextBox textBox2;
        private Button button1;
    }
}