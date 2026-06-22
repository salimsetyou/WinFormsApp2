using System;
using System.Data;
using Npgsql;

namespace MonitoringKopiKakao.Model
{
    public class LahanModel
    {
        public int IdLahan { get; set; }
        public int IdLahanLama { get; set; }  // Menyimpan ID lama untuk Update
        public string NamaLahan { get; set; }
        public string Lokasi { get; set; }
        public double LuasLahan { get; set; }
        public string JenisTanah { get; set; }

        private DatabaseConfig db = new DatabaseConfig();

        // Ambil semua data lahan
        public DataTable GetAllLahan()
        {
            string query = "SELECT id_lahan AS \"ID\", nama_lahan AS \"Nama Lahan\", lokasi AS \"Lokasi\", luas_lahan AS \"Luas (Ha)\", jenis_tanah AS \"Jenis Tanah\" FROM lahan ORDER BY id_lahan DESC";
            return db.ExecuteQuery(query);
        }

        // Simpan data
        public void InsertLahan()
        {
            string query = "INSERT INTO lahan (id_lahan, nama_lahan, lokasi, luas_lahan, jenis_tanah) VALUES (@id, @nama, @lokasi, @luas, @tanah)";
            using (NpgsqlCommand cmd = new NpgsqlCommand(query))
            {
                cmd.Parameters.AddWithValue("@id", IdLahan);
                cmd.Parameters.AddWithValue("@nama", NamaLahan ?? "");
                cmd.Parameters.AddWithValue("@lokasi", Lokasi ?? "");
                cmd.Parameters.AddWithValue("@luas", LuasLahan);
                cmd.Parameters.AddWithValue("@tanah", JenisTanah ?? "");
                db.ExecuteNonQuery(cmd);
            }
        }

        // Ubah data
        public void UpdateLahan()
        {
            // Jika ID berubah, gunakan ID lama untuk WHERE clause, dan update ID juga
            if (IdLahanLama > 0 && IdLahanLama != IdLahan)
            {
                using (NpgsqlConnection conn = db.GetConnection())
                {
                    using (NpgsqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            // Disable foreign key check sementara
                            using (NpgsqlCommand disableFk = new NpgsqlCommand("SET CONSTRAINTS ALL DEFERRED", conn, transaction))
                            {
                                disableFk.ExecuteNonQuery();
                            }

                            // Step 1: Update tabel monitoring DULU jika ada referensi ke lahan
                            // (jika monitoring table memiliki foreign key ke lahan)
                            // Untuk sekarang kita update jika monitoring ada id_lahan column
                            string queryMonitoring = "UPDATE monitoring SET id_lahan = @id_new WHERE id_lahan = @id_old";
                            try
                            {
                                using (NpgsqlCommand cmdMonitoring = new NpgsqlCommand(queryMonitoring, conn, transaction))
                                {
                                    cmdMonitoring.Parameters.AddWithValue("@id_new", IdLahan);
                                    cmdMonitoring.Parameters.AddWithValue("@id_old", IdLahanLama);
                                    cmdMonitoring.ExecuteNonQuery();
                                }
                            }
                            catch
                            {
                                // Jika monitoring tidak memiliki id_lahan column, skip
                            }

                            // Step 2: Update tabel lahan
                            string query = "UPDATE lahan SET id_lahan = @id_new, nama_lahan = @nama, lokasi = @lokasi, luas_lahan = @luas, jenis_tanah = @tanah WHERE id_lahan = @id_old";
                            using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@id_new", IdLahan);
                                cmd.Parameters.AddWithValue("@id_old", IdLahanLama);
                                cmd.Parameters.AddWithValue("@nama", NamaLahan ?? "");
                                cmd.Parameters.AddWithValue("@lokasi", Lokasi ?? "");
                                cmd.Parameters.AddWithValue("@luas", LuasLahan);
                                cmd.Parameters.AddWithValue("@tanah", JenisTanah ?? "");
                                cmd.ExecuteNonQuery();
                            }

                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw new Exception("Gagal mengubah ID lahan: " + ex.Message);
                        }
                    }
                }
            }
            else
            {
                // Update tanpa perubahan ID
                string query = "UPDATE lahan SET nama_lahan = @nama, lokasi = @lokasi, luas_lahan = @luas, jenis_tanah = @tanah WHERE id_lahan = @id";
                using (NpgsqlCommand cmd = new NpgsqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@id", IdLahan);
                    cmd.Parameters.AddWithValue("@nama", NamaLahan ?? "");
                    cmd.Parameters.AddWithValue("@lokasi", Lokasi ?? "");
                    cmd.Parameters.AddWithValue("@luas", LuasLahan);
                    cmd.Parameters.AddWithValue("@tanah", JenisTanah ?? "");
                    db.ExecuteNonQuery(cmd);
                }
            }
        }

        // Hapus data
        public void DeleteLahan(int id)
        {
            string query = "DELETE FROM lahan WHERE id_lahan = @id";
            using (NpgsqlCommand cmd = new NpgsqlCommand(query))
            {
                cmd.Parameters.AddWithValue("@id", id);
                db.ExecuteNonQuery(cmd);
            }
        }
    }
}