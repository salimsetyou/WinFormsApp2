namespace WinFormsApp2.View
{
    partial class RiwayatMonitoring
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RiwayatMonitoring));
            panel1 = new Panel();
            //label1 = new Label();
            //label2 = new Label();
            //checkBoxTanaman = new CheckBox();
            //checkBoxTanggal = new CheckBox();
            dateTimePicker1 = new DateTimePicker();
            textBox3 = new TextBox();
            dataGridView1 = new DataGridView();
            btnRefresh = new Button();
            button1 = new Button();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            //panel1.Controls.Add(label1);
            //panel1.Controls.Add(label2);
            //panel1.Controls.Add(checkBoxTanaman);
            //panel1.Controls.Add(checkBoxTanggal);
            panel1.Controls.Add(dateTimePicker1);
            panel1.Controls.Add(textBox3);
            panel1.Location = new Point(39, 28);
            panel1.Name = "panel1";
            panel1.Size = new Size(462, 100);
            panel1.TabIndex = 1;
            panel1.Paint += panel1_Paint;
            // 
            // label1
            // 
            //label1.AutoSize = true;
            //label1.Location = new Point(309, 2);
            //label1.Name = "label1";
            //label1.Size = new Size(99, 20);
            //label1.TabIndex = 5;
            //label1.Text = "Nama Tanaman";
            // 
            // label2
            // 
            //label2.AutoSize = true;
            //label2.Location = new Point(25, 2);
            //label2.Name = "label2";
            //label2.Size = new Size(120, 20);
            //label2.TabIndex = 4;
            //label2.Text = "Tanggal Monitoring";
            //// 
            //// checkBoxTanaman
            //// 
            //checkBoxTanaman.AutoSize = true;
            //checkBoxTanaman.Location = new Point(309, 50);
            //checkBoxTanaman.Name = "checkBoxTanaman";
            //checkBoxTanaman.Size = new Size(105, 24);
            //checkBoxTanaman.TabIndex = 3;
            //checkBoxTanaman.Text = "Aktifkan Filter";
            //checkBoxTanaman.UseVisualStyleBackColor = true;
            //// 
            //// checkBoxTanggal
            //// 
            //checkBoxTanggal.AutoSize = true;
            //checkBoxTanggal.Location = new Point(25, 50);
            //checkBoxTanggal.Name = "checkBoxTanggal";
            //checkBoxTanggal.Size = new Size(105, 24);
            //checkBoxTanggal.TabIndex = 2;
            //checkBoxTanggal.Text = "Aktifkan Filter";
            //checkBoxTanggal.UseVisualStyleBackColor = true;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(25, 22);
            dateTimePicker1.Margin = new Padding(2, 2, 2, 2);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(248, 27);
            dateTimePicker1.TabIndex = 3;
            dateTimePicker1.Enabled = false;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(309, 22);
            textBox3.Name = "textBox3";
            textBox3.PlaceholderText = "Cari nama tanaman...";
            textBox3.Size = new Size(125, 27);
            textBox3.TabIndex = 2;
            textBox3.Enabled = false;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(39, 215);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(720, 201);
            dataGridView1.TabIndex = 2;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(270, 140);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(103, 30);
            btnRefresh.TabIndex = 1;
            btnRefresh.Text = "Reset / Refresh";
            btnRefresh.Click += btnRefresh_Click;
            // 
            // button1
            // 
            button1.Location = new Point(39, 140);
            button1.Name = "button1";
            button1.Size = new Size(225, 30);
            button1.TabIndex = 3;
            button1.Text = "Cari (Search)";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // RiwayatMonitoring
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(800, 450);
            Controls.Add(button1);
            Controls.Add(btnRefresh);
            Controls.Add(dataGridView1);
            Controls.Add(panel1);
            DoubleBuffered = true;
            Name = "RiwayatMonitoring";
            Text = "RiwayatMonitoring";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        public TextBox textBox3;
        public DataGridView dataGridView1;
        public Button btnRefresh;
        private DateTimePicker dateTimePicker1;
        private Button button1;
    }
}