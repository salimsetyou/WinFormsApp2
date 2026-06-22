using System;
using System.Data;
using System.Windows.Forms;
using WinFormsApp2.Models;
using WinFormsApp2.View;

namespace WinFormsApp2.UserController
{
    public class LaporanController
    {
        private Laporan view;
        private MonitoringModel model;

        public LaporanController(Laporan view)
        {
            this.view = view;
            this.model = new MonitoringModel();
        }

        public void TampilData()
        {
            try
            {
                view.dgvLaporan.DataSource = model.GetAllMonitoring();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat laporan: " + ex.Message);
            }
        }

        public void TampilFilter(string namaPetugas, string jenisTanaman, DateTime? tanggal)
        {
            try
            {
                var dt = model.GetAllMonitoring();
                if (dt == null) dt = new DataTable();


                DataView dv = new DataView(dt);
                string filter = "";
                if (!string.IsNullOrWhiteSpace(namaPetugas))
                {
                    filter += $"PETUGAS LIKE '%" + namaPetugas.Replace("'", "''") + "%'";
                }
                if (!string.IsNullOrWhiteSpace(jenisTanaman))
                {
                    if (!string.IsNullOrEmpty(filter)) filter += " AND ";
                    filter += $"TANAMAN = '" + jenisTanaman.Replace("'", "''") + "'";
                }
                if (tanggal.HasValue)
                {
                    if (!string.IsNullOrEmpty(filter)) filter += " AND ";

                    filter += $"CONVERT(varchar, TANGGAL, 23) = '" + tanggal.Value.ToString("yyyy-MM-dd") + "'";
                }

                if (!string.IsNullOrEmpty(filter))
                    dv.RowFilter = filter;

                view.dgvLaporan.DataSource = dv;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memfilter laporan: " + ex.Message);
            }
        }

        public void HapusFromLaporan()
        {
            try
            {
                if (view.dgvLaporan.CurrentRow == null) return;
                var val = view.dgvLaporan.CurrentRow.Cells["ID"].Value;
                if (val == null) return;
                int id = Convert.ToInt32(val);
                if (MessageBox.Show("Hapus monitoring terpilih?", "Konfirmasi", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    model.DeleteMonitoring(id);
                    MessageBox.Show("Data monitoring berhasil dihapus!");
                    TampilData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menghapus monitoring: " + ex.Message);
            }
        }

        internal void TampilFilter(string namaPetugas, DateTime? tanggal)
        {
            throw new NotImplementedException();
        }
    }
}
