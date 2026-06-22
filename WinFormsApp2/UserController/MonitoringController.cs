using System;
using System.Windows.Forms;
using System.Data;
using WinFormsApp2.Models;
using WinFormsApp2.View;

namespace WinFormsApp2.UserController
{
    public class MonitoringController
    {
        private InputMonitoring viewInput;
        private RiwayatMonitoring viewRiwayat;
        private MonitoringModel model;

        public MonitoringController(InputMonitoring view)
        {
            this.viewInput = view;
            this.model = new MonitoringModel();
        }

        public MonitoringController(RiwayatMonitoring view)
        {
            this.viewRiwayat = view;
            this.model = new MonitoringModel();
        }

        public void TampilDataInput()
        {
            try
            {
                viewInput.dataGridView1.DataSource = model.GetAllMonitoring();
                // reset selection and buttons
                viewInput.btnEdit.Enabled = false;
                viewInput.btnSave.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data monitoring: " + ex.Message);
            }
        }

        public void LoadDropdowns()
        {
            try
            {
                var dtTanaman = model.GetTanamanList();
                if (viewInput.comboBoxTanaman != null)
                {
                    // Defensive: if database returns valid tanaman rows use them; otherwise fall back
                    // to the two expected items (Kopi, Kakao) to avoid showing petugas names here.
                    bool useFallback = true;
                    if (dtTanaman != null && dtTanaman.Rows.Count > 0)
                    {
                        // check if the returned rows look like tanaman (contain expected columns)
                        try
                        {
                            foreach (DataRow r in dtTanaman.Rows)
                            {
                                var name = r["nama_tanaman"]?.ToString() ?? "";
                                if (!string.IsNullOrWhiteSpace(name))
                                {
                                    // if any row looks like a plant name, consider it valid
                                    useFallback = false;
                                    break;
                                }
                            }
                        }
                        catch
                        {
                            useFallback = true;
                        }
                    }

                    if (!useFallback)
                    {
                        viewInput.comboBoxTanaman.DisplayMember = "nama_tanaman";
                        viewInput.comboBoxTanaman.ValueMember = "id_tanaman";
                        viewInput.comboBoxTanaman.DataSource = dtTanaman;
                        viewInput.comboBoxTanaman.DropDownStyle = ComboBoxStyle.DropDownList;
                        if (viewInput.comboBoxTanaman.Items.Count > 0)
                            viewInput.comboBoxTanaman.SelectedIndex = 0;
                    }
                    else
                    {
                        // fallback to static choices
                        viewInput.comboBoxTanaman.DataSource = null;
                        viewInput.comboBoxTanaman.Items.Clear();
                        viewInput.comboBoxTanaman.Items.AddRange(new object[] { "Kopi", "Kakao" });
                        viewInput.comboBoxTanaman.DropDownStyle = ComboBoxStyle.DropDownList;
                        if (viewInput.comboBoxTanaman.Items.Count > 0)
                            viewInput.comboBoxTanaman.SelectedIndex = 0;
                    }
                }

                // NOTE: comboBoxPetugas on the InputMonitoring form is used as the "Kondisi" selector
                // (values: Sehat, Layu). Do NOT load the petugas (user) list into that combobox or it
                // will overwrite the kondisi options. The IdUser for an input should come from the
                // current Session (when a petugas is logged in). If you need a selectable petugas
                // list in the form, add a dedicated ComboBox (e.g. comboBoxPetugasUser) to the form
                // and load model.GetPetugasList() into that control instead.
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat dropdown: " + ex.Message);
            }
        }

        public void TampilDataRiwayat()
        {
            try
            {
                viewRiwayat.dataGridView1.DataSource = model.GetAllMonitoring();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat riwayat monitoring: " + ex.Message);
            }
        }

        public void SimpanFromInput()
        {
            try
            {
                // Mapping yang sesuai dengan kontrol di form InputMonitoring
                model.Tanggal = viewInput.dateTimePicker1?.Value ?? DateTime.Now;

                // Ambil ID Monitoring dari textBox1 (WAJIB DIISI)
                model.IdMonitoring = 0;
                if (string.IsNullOrWhiteSpace(viewInput.textBox1?.Text))
                {
                    MessageBox.Show("ID Monitoring harus diisi! Silakan input ID monitoring secara manual.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(viewInput.textBox1.Text.Trim(), out int idMonitoring) || idMonitoring <= 0)
                {
                    MessageBox.Show("ID Monitoring harus berupa angka positif!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                model.IdMonitoring = idMonitoring;

                // Ambil id tanaman dari comboBoxTanaman dengan beberapa pendekatan aman
                model.IdTanaman = 0;
                try
                {
                    if (viewInput.comboBoxTanaman != null)
                    {
                        var sel = viewInput.comboBoxTanaman.SelectedValue;
                        if (sel != null && sel != DBNull.Value)
                        {
                            model.IdTanaman = Convert.ToInt32(sel);
                        }
                        else if (viewInput.comboBoxTanaman.SelectedItem is DataRowView drv)
                        {
                            model.IdTanaman = Convert.ToInt32(drv["id_tanaman"]);
                        }
                        else if (!string.IsNullOrWhiteSpace(viewInput.comboBoxTanaman.Text))
                        {
                            // fallback: cari id dari nama pada daftar tanaman
                            var dtLookup = model.GetTanamanList();
                            if (dtLookup != null)
                            {
                                foreach (DataRow r in dtLookup.Rows)
                                {
                                    if (string.Equals(r["nama_tanaman"]?.ToString(), viewInput.comboBoxTanaman.Text, StringComparison.OrdinalIgnoreCase))
                                    {
                                        model.IdTanaman = Convert.ToInt32(r["id_tanaman"]);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                catch
                {
                    model.IdTanaman = 0;
                }

                // Ambil Kondisi dari comboBoxPetugas (sebelumnya gunakan ini)
                model.Kondisi = viewInput.comboBoxPetugas?.Text ?? string.Empty;

                // Ambil Hama dari comboBox1
                model.Hama = viewInput.comboBox1?.Text ?? string.Empty;

                // Ambil Cuaca dari comboBox2
                model.Cuaca = viewInput.comboBox2?.Text ?? string.Empty;

                // Ambil Catatan dari textBox2
                model.Catatan = viewInput.textBox2?.Text ?? string.Empty;


                if (WinFormsApp2.Session.IdUser > 0)
                    model.IdUser = WinFormsApp2.Session.IdUser;
                else if (viewInput.comboBoxPetugas != null && viewInput.comboBoxPetugas.SelectedValue != null && int.TryParse(viewInput.comboBoxPetugas.SelectedValue.ToString(), out var idu))
                    model.IdUser = idu;
                else
                    model.IdUser = 0;


                if (model.IdTanaman <= 0)
                {
                    MessageBox.Show("Pilih tanaman yang valid sebelum menyimpan.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (model.IdUser <= 0)
                {
                    MessageBox.Show("Id petugas tidak tersedia. Pastikan Anda sudah login atau pilih petugas yang valid.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                model.InsertMonitoring();
                MessageBox.Show("Data monitoring berhasil disimpan dengan ID: " + model.IdMonitoring);
                TampilDataInput();
                viewInput.textBox1?.Clear();
                viewInput.textBox2?.Clear();
                viewInput.comboBoxTanaman?.SelectedIndex = -1;
                viewInput.comboBoxPetugas?.SelectedIndex = -1;
                viewInput.comboBox1?.SelectedIndex = -1;
                viewInput.comboBox2?.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menyimpan monitoring: " + ex.Message);
            }
        }

        public void HapusFromRiwayat()
        {
            try
            {
                if (viewRiwayat.dataGridView1.CurrentRow == null) return;
                var val = viewRiwayat.dataGridView1.CurrentRow.Cells["ID"].Value;
                if (val == null) return;
                int id = Convert.ToInt32(val);
                if (MessageBox.Show("Hapus monitoring terpilih?", "Konfirmasi", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    model.DeleteMonitoring(id);
                    MessageBox.Show("Data monitoring berhasil dihapus!");
                    TampilDataRiwayat();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menghapus monitoring: " + ex.Message);
            }
        }

        public void EditFromInput()
        {
            try
            {
                if (viewInput.dataGridView1.CurrentRow == null) return;

                var val = viewInput.dataGridView1.CurrentRow.Cells["ID"].Value;
                if (val == null) return;
                int id = Convert.ToInt32(val);

                model.IdMonitoring = id;
                model.Tanggal = viewInput.dateTimePicker1?.Value ?? DateTime.Now;

                model.IdTanaman = 0;
                try
                {
                    if (viewInput.comboBoxTanaman != null)
                    {
                        var sel = viewInput.comboBoxTanaman.SelectedValue;
                        if (sel != null && sel != DBNull.Value)
                        {
                            model.IdTanaman = Convert.ToInt32(sel);
                        }
                        else if (viewInput.comboBoxTanaman.SelectedItem is DataRowView drv)
                        {
                            model.IdTanaman = Convert.ToInt32(drv["id_tanaman"]);
                        }
                        else if (!string.IsNullOrWhiteSpace(viewInput.comboBoxTanaman.Text))
                        {
                            var dtLookup = model.GetTanamanList();
                            if (dtLookup != null)
                            {
                                foreach (DataRow r in dtLookup.Rows)
                                {
                                    if (string.Equals(r["nama_tanaman"]?.ToString(), viewInput.comboBoxTanaman.Text, StringComparison.OrdinalIgnoreCase))
                                    {
                                        model.IdTanaman = Convert.ToInt32(r["id_tanaman"]);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                catch
                {
                    model.IdTanaman = 0;
                }

                model.Kondisi = viewInput.comboBoxPetugas?.Text ?? string.Empty;
                model.Hama = viewInput.comboBox1?.Text ?? string.Empty;
                model.Cuaca = viewInput.comboBox2?.Text ?? string.Empty;
                model.Catatan = viewInput.textBox2?.Text ?? string.Empty;

                if (WinFormsApp2.Session.IdUser > 0)
                    model.IdUser = WinFormsApp2.Session.IdUser;
                else if (viewInput.comboBoxPetugas != null && viewInput.comboBoxPetugas.SelectedValue != null && int.TryParse(viewInput.comboBoxPetugas.SelectedValue.ToString(), out var idu))
                    model.IdUser = idu;
                else
                    model.IdUser = 0;

                if (model.IdTanaman <= 0)
                {
                    MessageBox.Show("Pilih tanaman yang valid sebelum mengubah.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                model.UpdateMonitoring();
                MessageBox.Show("Data monitoring berhasil diperbarui!");
                TampilDataInput();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal mengubah monitoring: " + ex.Message);
            }
        }

        public void HapusFromInput()
        {
            try
            {
                if (viewInput.dataGridView1.CurrentRow == null) return;
                var val = viewInput.dataGridView1.CurrentRow.Cells["ID"].Value;
                if (val == null) return;
                int id = Convert.ToInt32(val);
                if (MessageBox.Show("Hapus monitoring terpilih?", "Konfirmasi", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    model.DeleteMonitoring(id);
                    MessageBox.Show("Data monitoring berhasil dihapus!");
                    TampilDataInput();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menghapus monitoring: " + ex.Message);
            }
        }

        public void FilterRiwayat(string namaTanaman, DateTime? tanggal)
        {
            try
            {
                var dt = model.GetAllMonitoring();
                if (dt == null) dt = new DataTable();

                DataView dv = new DataView(dt);
                string filter = "";

                if (!string.IsNullOrWhiteSpace(namaTanaman))
                {
                    filter += $"TANAMAN LIKE '%" + namaTanaman.Replace("'", "''") + "%'";
                }

                if (tanggal.HasValue)
                {
                    if (!string.IsNullOrEmpty(filter)) filter += " AND ";
                    filter += $"CONVERT(varchar, TANGGAL, 23) = '" + tanggal.Value.ToString("yyyy-MM-dd") + "'";
                }

                if (!string.IsNullOrEmpty(filter))
                    dv.RowFilter = filter;

                viewRiwayat.dataGridView1.DataSource = dv;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memfilter riwayat: " + ex.Message);
            }
        }
    }
}
