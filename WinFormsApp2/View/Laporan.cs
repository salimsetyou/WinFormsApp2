using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WinFormsApp2.View
{
    public partial class Laporan : Form
    {
        private WinFormsApp2.UserController.LaporanController controller;

        public Laporan()
        {
            InitializeComponent();
            controller = new WinFormsApp2.UserController.LaporanController(this);
            Load += Laporan_Load;
        }

        private void Laporan_Load(object sender, EventArgs e)
        {
            controller.TampilData();
            dateTimePicker1.Value = DateTime.Now;
            btnTampilkanLaporan.Click += BtnTampilkanLaporan_Click;
        }

        private void BtnTampilkanLaporan_Click(object sender, EventArgs e)
        {
            string namaPetugas = txtPetugas?.Text ?? "";
            DateTime? tanggal = dateTimePicker1?.Value;

            controller.TampilFilter(namaPetugas, tanggal);
        }

        private void BtnHapus_Click(object sender, EventArgs e)
        {
            controller.HapusFromLaporan();
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            controller.TampilData();
            txtPetugas.Clear();
            dateTimePicker1.Value = DateTime.Now;
        }
    }
}
