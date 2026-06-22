using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WinFormsApp2.View
{
    public partial class RiwayatMonitoring : Form
    {
        private WinFormsApp2.UserController.MonitoringController controller;

        public RiwayatMonitoring()
        {
            InitializeComponent();
            controller = new WinFormsApp2.UserController.MonitoringController(this);
            Load += RiwayatMonitoring_Load;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void RiwayatMonitoring_Load(object sender, EventArgs e)
        {
            controller.TampilDataRiwayat();
            dateTimePicker1.Value = DateTime.Now;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            controller.TampilDataRiwayat();
            textBox3.Clear();
            dateTimePicker1.Value = DateTime.Now;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string namaTanaman = textBox3?.Text ?? "";
            DateTime? tanggal = dateTimePicker1?.Value;

            controller.FilterRiwayat(namaTanaman, tanggal);
        }
    }
}
