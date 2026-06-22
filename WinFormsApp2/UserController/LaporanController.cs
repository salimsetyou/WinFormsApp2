using System;
using System.Data;
using System.Linq;
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
                // Apply string-based filters first (PETUGAS, TANAMAN)
                if (!string.IsNullOrEmpty(filter))
                    dv.RowFilter = filter;

                // If tanggal specified, DataColumn type may be DateOnly or DateTime.
                // DataView.RowFilter cannot compare DateOnly with DateTime using '='.
                // So apply date filtering in-memory using LINQ over the rows.
                if (tanggal.HasValue)
                {
                    var targetDate = DateOnly.FromDateTime(tanggal.Value);
                    var tableAfterStringFilter = dv.ToTable();
                    var filtered = tableAfterStringFilter.AsEnumerable().Where(r =>
                    {
                        var val = r["TANGGAL"];
                        if (val == null || val == DBNull.Value) return false;
                        if (val is DateOnly d) return d == targetDate;
                        if (val is DateTime dtv) return DateOnly.FromDateTime(dtv) == targetDate;
                        // fallback: try parse
                        if (DateTime.TryParse(val.ToString(), out var parsed))
                            return DateOnly.FromDateTime(parsed) == targetDate;
                        return false;
                    });

                    if (filtered.Any())
                        view.dgvLaporan.DataSource = filtered.CopyToDataTable();
                    else
                        view.dgvLaporan.DataSource = tableAfterStringFilter.Clone(); // empty table with same schema
                }
                else
                {
                    view.dgvLaporan.DataSource = dv;
                }
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
            // Forward to the main overload and pass empty jenisTanaman
            TampilFilter(namaPetugas, string.Empty, tanggal);
        }
    }
}
