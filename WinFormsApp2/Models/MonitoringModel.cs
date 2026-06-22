using MonitoringKopiKakao;
using Npgsql;
using System;
using System.Data;

namespace WinFormsApp2.Models
{
    public class MonitoringModel
    {
        public int IdMonitoring { get; set; }
        public int IdMonitoringLama { get; set; }
        public DateTime Tanggal { get; set; }
        public int IdTanaman { get; set; }
        public string Kondisi { get; set; } = string.Empty;
        public string Hama { get; set; } = string.Empty;
        public string Cuaca { get; set; } = string.Empty;
        public string Catatan { get; set; } = string.Empty;
        public int IdUser { get; set; }

        private DatabaseConfig db = new DatabaseConfig();

        public DataTable GetAllMonitoring()
        {
            // Query dengan kolom catatan dan hama
            string query = "SELECT m.id_monitoring AS \"ID\", m.tanggal AS \"TANGGAL\", t.nama_tanaman AS \"TANAMAN\", m.kondisi_tanaman AS \"KONDISI\", m.hama AS \"HAMA\", m.cuaca AS \"CUACA\", m.catatan AS \"CATATAN\", p.nama AS \"PETUGAS\" FROM monitoring m JOIN tanaman t ON m.id_tanaman = t.id_tanaman LEFT JOIN petugas_monitoring p ON m.id_user = p.id_user ORDER BY m.tanggal DESC";
            return db.ExecuteQuery(query);
        }

        public void InsertMonitoring()
        {
            // INSERT dengan id_monitoring yang custom (tidak auto-increment)
            string query = "INSERT INTO monitoring (id_monitoring, tanggal, id_tanaman, kondisi_tanaman, hama, cuaca, catatan, id_user) VALUES (@id_monitoring, @tanggal, @id_tanaman, @kondisi, @hama, @cuaca, @catatan, @id_user)";
            using (NpgsqlCommand cmd = new NpgsqlCommand(query))
            {
                cmd.Parameters.AddWithValue("@id_monitoring", IdMonitoring);
                cmd.Parameters.AddWithValue("@tanggal", Tanggal);
                cmd.Parameters.AddWithValue("@id_tanaman", IdTanaman);
                cmd.Parameters.AddWithValue("@kondisi", Kondisi ?? string.Empty);
                cmd.Parameters.AddWithValue("@hama", Hama ?? string.Empty);
                cmd.Parameters.AddWithValue("@cuaca", Cuaca ?? string.Empty);
                cmd.Parameters.AddWithValue("@catatan", Catatan ?? string.Empty);
                cmd.Parameters.AddWithValue("@id_user", IdUser);
                db.ExecuteNonQuery(cmd);
            }
        }

        public void UpdateMonitoring()
        {
            // Jika IdMonitoringLama diset dan berbeda, izinkan perubahan primary key secara aman
            if (IdMonitoringLama > 0 && IdMonitoringLama != IdMonitoring)
            {
                using (NpgsqlConnection conn = db.GetConnection())
                {
                    using (var transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            // Pastikan ID baru belum dipakai
                            string check = "SELECT COUNT(*) FROM monitoring WHERE id_monitoring = @newid";
                            using (var cmdCheck = new NpgsqlCommand(check, conn, transaction))
                            {
                                cmdCheck.Parameters.AddWithValue("@newid", IdMonitoring);
                                var exists = Convert.ToInt64(cmdCheck.ExecuteScalar());
                                if (exists > 0)
                                    throw new Exception("ID monitoring baru sudah digunakan. Pilih ID lain.");
                            }

                            string query = "UPDATE monitoring SET id_monitoring = @newid, tanggal = @tanggal, id_tanaman = @id_tanaman, kondisi_tanaman = @kondisi, hama = @hama, cuaca = @cuaca, catatan = @catatan, id_user = @id_user WHERE id_monitoring = @oldid";
                            using (var cmd = new NpgsqlCommand(query, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@newid", IdMonitoring);
                                cmd.Parameters.AddWithValue("@oldid", IdMonitoringLama);
                                cmd.Parameters.AddWithValue("@tanggal", Tanggal);
                                cmd.Parameters.AddWithValue("@id_tanaman", IdTanaman);
                                cmd.Parameters.AddWithValue("@kondisi", Kondisi ?? string.Empty);
                                cmd.Parameters.AddWithValue("@hama", Hama ?? string.Empty);
                                cmd.Parameters.AddWithValue("@cuaca", Cuaca ?? string.Empty);
                                cmd.Parameters.AddWithValue("@catatan", Catatan ?? string.Empty);
                                cmd.Parameters.AddWithValue("@id_user", IdUser);
                                cmd.ExecuteNonQuery();
                            }

                            transaction.Commit();
                        }
                        catch
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            else
            {
                // Update tanpa perubahan ID
                string query = "UPDATE monitoring SET tanggal = @tanggal, id_tanaman = @id_tanaman, kondisi_tanaman = @kondisi, hama = @hama, cuaca = @cuaca, catatan = @catatan, id_user = @id_user WHERE id_monitoring = @id";
                using (NpgsqlCommand cmd = new NpgsqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@id", IdMonitoring);
                    cmd.Parameters.AddWithValue("@tanggal", Tanggal);
                    cmd.Parameters.AddWithValue("@id_tanaman", IdTanaman);
                    cmd.Parameters.AddWithValue("@kondisi", Kondisi ?? string.Empty);
                    cmd.Parameters.AddWithValue("@hama", Hama ?? string.Empty);
                    cmd.Parameters.AddWithValue("@cuaca", Cuaca ?? string.Empty);
                    cmd.Parameters.AddWithValue("@catatan", Catatan ?? string.Empty);
                    cmd.Parameters.AddWithValue("@id_user", IdUser);
                    db.ExecuteNonQuery(cmd);
                }
            }
        }

        public void DeleteMonitoring(int id)
        {
            string query = "DELETE FROM monitoring WHERE id_monitoring = @id";
            using (NpgsqlCommand cmd = new NpgsqlCommand(query))
            {
                cmd.Parameters.AddWithValue("@id", id);
                db.ExecuteNonQuery(cmd);
            }
        }

        public DataTable GetTanamanList()
        {
            string query = "SELECT id_tanaman, nama_tanaman FROM tanaman ORDER BY nama_tanaman";
            return db.ExecuteQuery(query);
        }

        public DataTable GetPetugasList()
        {
            string query = "SELECT id_user, nama FROM petugas_monitoring ORDER BY nama";
            return db.ExecuteQuery(query);
        }
    }
}
