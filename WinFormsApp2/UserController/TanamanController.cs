using System;
using System.Windows.Forms;
using WinFormsApp2.Models; 
using WinFormsApp2.View;   

namespace WinFormsApp2.UserController
{
    public class TanamanController
    {
        private Tanaman view;
        private TanamanModel model;

        public TanamanController(Tanaman view)
        {
            this.view = view;
            this.model = new TanamanModel();
        }

        public void TampilData()
        {
            try
            {
                if (view.dgvTanaman != null)
                {
                    view.dgvTanaman.DataSource = model.GetAllTanaman();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Simpan()
        {
            try
            {
                if (view.txtIdTanaman == null || view.txtNamaTanaman == null || view.txtVarietas == null ||
                    view.cboKomoditas == null)
                {
                    MessageBox.Show("Komponen UI gagal dimuat!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(view.txtIdTanaman.Text) ||
                    string.IsNullOrWhiteSpace(view.txtNamaTanaman.Text) ||
                    string.IsNullOrWhiteSpace(view.txtVarietas.Text) ||
                    view.cboKomoditas.SelectedItem == null)
                {
                    MessageBox.Show("ID Tanaman, Nama tanaman, varietas, dan jenis komoditas wajib diisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(view.txtIdTanaman.Text.Trim(), out int id) || id <= 0)
                {
                    MessageBox.Show("ID Tanaman harus berupa angka positif!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                model.IdTanaman = id;
                model.NamaTanaman = view.txtNamaTanaman.Text.Trim();
                model.Varietas = view.txtVarietas.Text.Trim();
                model.TanggalTanam = null;
                model.JenisKomoditas = view.cboKomoditas.SelectedItem?.ToString() ?? "";
                model.UmurTanaman = 0;

                model.InsertTanaman();
                MessageBox.Show("Data Tanaman berhasil disimpan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                if (view.txtIdTanaman == null || view.txtNamaTanaman == null ||
                    view.txtVarietas == null || view.cboKomoditas == null) return;

                // Validasi input
                if (!int.TryParse(view.txtIdTanaman.Text.Trim(), out int idBaru) || idBaru <= 0)
                {
                    MessageBox.Show("ID Tanaman harus berupa angka positif!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(view.txtNamaTanaman.Text) ||
                    string.IsNullOrWhiteSpace(view.txtVarietas.Text) ||
                    view.cboKomoditas.SelectedItem == null)
                {
                    MessageBox.Show("Nama tanaman, varietas, dan jenis komoditas wajib diisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Ambil ID lama dari DataGridView
                if (view.dgvTanaman != null && view.dgvTanaman.CurrentRow != null)
                {
                    var idLamaValue = view.dgvTanaman.CurrentRow.Cells["ID"].Value;
                    if (idLamaValue != null && int.TryParse(idLamaValue.ToString(), out int idLama))
                    {
                        model.IdTanamanLama = idLama;
                    }
                }

                model.IdTanaman = idBaru;
                model.NamaTanaman = view.txtNamaTanaman.Text.Trim();
                model.Varietas = view.txtVarietas.Text.Trim();
                model.TanggalTanam = null;
                model.JenisKomoditas = view.cboKomoditas.SelectedItem?.ToString() ?? "";
                model.UmurTanaman = 0;

                model.UpdateTanaman();
                MessageBox.Show("Data Tanaman berhasil diubah!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                if (view.txtIdTanaman == null) return;

                if (view.txtIdTanaman.Tag == null || !int.TryParse(view.txtIdTanaman.Tag?.ToString() ?? "0", out int id) || id <= 0)
                {
                    MessageBox.Show("Pilih data yang ingin dihapus!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var konfirmasi = MessageBox.Show("Apakah Anda yakin menghapus data ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (konfirmasi == DialogResult.Yes)
                {
                    model.DeleteTanaman(id);
                    MessageBox.Show("Data berhasil dihapus!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
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