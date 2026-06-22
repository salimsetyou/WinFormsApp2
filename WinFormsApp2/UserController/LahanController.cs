using System;
using System.Windows.Forms;
using MonitoringKopiKakao.Model;
using WinFormsApp2.View; 

namespace MonitoringKopiKakao.Controller
{
    public class LahanController
    {
        private Lahan view;
        private LahanModel model;

       
        public LahanController(Lahan view)
        {
            this.view = view;
            this.model = new LahanModel();
        }

        public void TampilData()
        {
            try
            {
                view.dgvLahan.DataSource = model.GetAllLahan();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data lahan: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        

        public void Simpan()
        {
            try
            {
                // Validasi input kosong
                if (string.IsNullOrEmpty(view.txtIdLahan.Text) || 
                    string.IsNullOrEmpty(view.txtNamaLahan.Text) || 
                    string.IsNullOrEmpty(view.txtLokasi.Text) || 
                    string.IsNullOrEmpty(view.txtLuas.Text))
                {
                    MessageBox.Show("ID Lahan, Nama Lahan, Lokasi, dan Luas wajib diisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validasi ID Lahan
                if (!int.TryParse(view.txtIdLahan.Text.Trim(), out int idLahan) || idLahan <= 0)
                {
                    MessageBox.Show("ID Lahan harus berupa angka positif!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                model.IdLahan = idLahan;
                model.NamaLahan = view.txtNamaLahan.Text;
                model.Lokasi = view.txtLokasi.Text;
                model.LuasLahan = Convert.ToDouble(view.txtLuas.Text);
                model.JenisTanah = view.cboJenisTanah.SelectedItem.ToString();

                model.InsertLahan();
                MessageBox.Show("Data Lahan berhasil ditambahkan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TampilData();
                view.ResetForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menyimpan data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Ubah()
        {
            try
            {
                // Validasi input kosong
                if (string.IsNullOrEmpty(view.txtIdLahan.Text) || 
                    string.IsNullOrEmpty(view.txtNamaLahan.Text) || 
                    string.IsNullOrEmpty(view.txtLokasi.Text) || 
                    string.IsNullOrEmpty(view.txtLuas.Text))
                {
                    MessageBox.Show("ID Lahan, Nama Lahan, Lokasi, dan Luas wajib diisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validasi ID Lahan baru
                if (!int.TryParse(view.txtIdLahan.Text.Trim(), out int idLahanBaru) || idLahanBaru <= 0)
                {
                    MessageBox.Show("ID Lahan harus berupa angka positif!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Ambil ID lama dari row yang sedang dipilih di DataGridView
                if (view.dgvLahan.CurrentRow != null)
                {
                    var idLamaValue = view.dgvLahan.CurrentRow.Cells["ID"].Value;
                    if (idLamaValue != null && int.TryParse(idLamaValue.ToString(), out int idLamaInt))
                    {
                        model.IdLahanLama = idLamaInt;
                    }
                }

                model.IdLahan = idLahanBaru;
                model.NamaLahan = view.txtNamaLahan.Text;
                model.Lokasi = view.txtLokasi.Text;
                model.LuasLahan = Convert.ToDouble(view.txtLuas.Text);
                model.JenisTanah = view.cboJenisTanah.SelectedItem.ToString();

                model.UpdateLahan();
                MessageBox.Show("Data Lahan berhasil diperbarui!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TampilData();
                view.ResetForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal mengubah data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Hapus()
        {
            try
            {
                int id = Convert.ToInt32(view.txtIdLahan.Text);
                var konfirmasi = MessageBox.Show("Hapus lahan ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (konfirmasi == DialogResult.Yes)
                {
                    model.DeleteLahan(id);
                    MessageBox.Show("Data Lahan berhasil dihapus!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TampilData();
                    view.ResetForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menghapus data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}