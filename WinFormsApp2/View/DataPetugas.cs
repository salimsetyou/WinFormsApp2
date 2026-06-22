using System;
using System.Windows.Forms;
using MonitoringKopiKakao.Controller;

namespace WinFormsApp2.View
{
    public partial class DataPetugas : Form
    {
        private PetugasController controller;

        public TextBox TxtIdPetugas => txtIdPetugas;
        public TextBox TxtUsername => txtUsername;
        public TextBox TxtPassword => txtPassword;
        public TextBox TxtNamaPetugas => txtNama;
        public DataGridView DataGridPetugas => dgvDataPetugas;

        public DataPetugas()
        {
            InitializeComponent();
            controller = new PetugasController(this);
        }

        private void DataPetugas_Load(object sender, EventArgs e)
        {
            controller.TampilData();
            ResetForm();
        }

        private void btnSimpan_Click(object sender, EventArgs e) => controller.Simpan();
        private void btnEdit_Click(object sender, EventArgs e) => controller.Ubah();
        private void btnHapus_Click(object sender, EventArgs e) => controller.Hapus();
        private void btnReset_Click(object sender, EventArgs e) => ResetForm();

        public void ResetForm()
        {
            txtIdPetugas.Clear();
            txtIdPetugas.ReadOnly = false;
            txtNama.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
            btnEdit.Enabled = false;
            btnHapus.Enabled = false;
            btnSimpan.Enabled = true;
        }

        private void DataGridPetugas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvDataPetugas.Rows[e.RowIndex];

                txtIdPetugas.Text = row.Cells["ID"].Value.ToString();
                txtIdPetugas.ReadOnly = false;
                txtUsername.Text = row.Cells["Username"].Value.ToString();
                txtPassword.Text = row.Cells["Password"].Value.ToString();
                txtNama.Text = row.Cells["Nama Petugas"].Value.ToString();

                btnSimpan.Enabled = false;
                btnEdit.Enabled = true;
                btnHapus.Enabled = true;
            }
        }
    }
}