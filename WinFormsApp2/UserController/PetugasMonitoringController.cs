using System;
using System.Windows.Forms;
using MonitoringKopiKakao.Model;
using WinFormsApp2.View;

namespace MonitoringKopiKakao.Controller
{
    public class PetugasController
    {
        private DataPetugas view;
        private PetugasModel model;

        public PetugasController(DataPetugas view)
        {
            this.view = view;
            this.model = new PetugasModel();
        }

        public void TampilData()
        {
            try { view.DataGridPetugas.DataSource = model.GetAllPetugas(); }
            catch (Exception ex) { MessageBox.Show("Gagal memuat petugas: " + ex.Message); }
        }

        public void Simpan()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(view.TxtIdPetugas.Text) ||
                    string.IsNullOrWhiteSpace(view.TxtUsername.Text) ||
                    string.IsNullOrWhiteSpace(view.TxtPassword.Text) ||
                    string.IsNullOrWhiteSpace(view.TxtNamaPetugas.Text))
                {
                    MessageBox.Show("Semua field wajib diisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(view.TxtIdPetugas.Text.Trim(), out int idPetugas) || idPetugas <= 0)
                {
                    MessageBox.Show("ID Petugas harus berupa angka positif!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                model.IdUser = idPetugas;
                model.Username = view.TxtUsername.Text;
                model.Password = view.TxtPassword.Text;
                model.NamaPetugas = view.TxtNamaPetugas.Text;

                model.InsertPetugas();
                MessageBox.Show("Data Petugas berhasil ditambahkan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TampilData();
                view.ResetForm();
            }
            catch (Exception ex) { MessageBox.Show("Gagal menambahkan petugas: " + ex.Message); }
        }

        public void Ubah()
        {
            try
            {
                // Validasi semua field terisi
                if (string.IsNullOrWhiteSpace(view.TxtIdPetugas.Text) ||
                    string.IsNullOrWhiteSpace(view.TxtUsername.Text) ||
                    string.IsNullOrWhiteSpace(view.TxtPassword.Text) ||
                    string.IsNullOrWhiteSpace(view.TxtNamaPetugas.Text))
                {
                    MessageBox.Show("Semua field wajib diisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validasi ID Petugas baru
                if (!int.TryParse(view.TxtIdPetugas.Text.Trim(), out int idPetugasBaru) || idPetugasBaru <= 0)
                {
                    MessageBox.Show("ID Petugas harus berupa angka positif!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Ambil ID lama dari row yang dipilih di DataGridView
                if (view.DataGridPetugas != null && view.DataGridPetugas.CurrentRow != null)
                {
                    var idLamaValue = view.DataGridPetugas.CurrentRow.Cells["ID"].Value;
                    if (idLamaValue != null && int.TryParse(idLamaValue.ToString(), out int idLama))
                    {
                        model.IdUserLama = idLama;
                    }
                }

                model.IdUser = idPetugasBaru;
                model.Username = view.TxtUsername.Text;
                model.Password = view.TxtPassword.Text;
                model.NamaPetugas = view.TxtNamaPetugas.Text;

                model.UpdatePetugas();
                MessageBox.Show("Data Petugas berhasil diperbarui!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TampilData();
                view.ResetForm();
            }
            catch (Exception ex) { MessageBox.Show("Gagal memperbarui petugas: " + ex.Message); }
        }

        public void Hapus()
        {
            try
            {
                if (!int.TryParse(view.TxtIdPetugas.Text.Trim(), out int idPetugas) || idPetugas <= 0)
                {
                    MessageBox.Show("ID Petugas tidak valid!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (MessageBox.Show("Hapus akun petugas terpilih?", "Konfirmasi", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    model.DeletePetugas(idPetugas);
                    MessageBox.Show("Data Petugas berhasil dihapus!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TampilData();
                    view.ResetForm();
                }
            }
            catch (Exception ex) { MessageBox.Show("Gagal menghapus petugas: " + ex.Message); }
        }
    }
}