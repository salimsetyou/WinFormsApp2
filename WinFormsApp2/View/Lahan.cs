using System;
using System.Windows.Forms;
using MonitoringKopiKakao.Controller;

namespace WinFormsApp2.View
{
    public partial class Lahan : Form
    {
        private LahanController controller;

        public ComboBox cboJenisTanah => cmbJenis;

        public Lahan()
        {
            InitializeComponent();
           
            controller = new LahanController(this);
        }

        private void Lahan_Load(object sender, EventArgs e)
        {
      
            if (cmbJenis.Items.Count == 0)
            {
                cmbJenis.Items.Add("Tanah Liat");
                cmbJenis.Items.Add("Tanah Berpasir");
                cmbJenis.Items.Add("Tanah Humus");
                cmbJenis.Items.Add("Tanah Aluvial");
                cmbJenis.Items.Add("Tanah Vulkanik");
            }

            controller.TampilData();
            ResetForm();
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            controller.Simpan();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            controller.Ubah();
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            controller.Hapus();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        public void ResetForm()
        {
            txtIdLahan.Clear();
            txtIdLahan.ReadOnly = false;
            if (txtNamaLahan != null) txtNamaLahan.Clear();
            txtLokasi.Clear();
            txtLuas.Clear();
            if (cmbJenis.Items.Count > 0) cmbJenis.SelectedIndex = 0;

            btnSimpan.Enabled = true;
            btnEdit.Enabled = false;
            btnHapus.Enabled = false;
        }

    
        private void dgvLahan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvLahan.Rows[e.RowIndex];
                txtIdLahan.Text = row.Cells["ID"].Value.ToString();
                txtIdLahan.ReadOnly = false;
                txtNamaLahan.Text = row.Cells["Nama Lahan"].Value?.ToString() ?? "";
                txtLokasi.Text = row.Cells["Lokasi"].Value.ToString();
                txtLuas.Text = row.Cells["Luas (Ha)"].Value.ToString();
                cmbJenis.SelectedItem = row.Cells["Jenis Tanah"].Value.ToString();


                btnSimpan.Enabled = false;
                btnEdit.Enabled = true;
                btnHapus.Enabled = true;
            }
        }
    }
}