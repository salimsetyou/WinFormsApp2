using MonitoringKopiKakao;
using Npgsql;
using System;
using System.Data;


namespace WinFormsApp2.Models
{
    public class TanamanModel
    {
        public int IdTanaman { get; set; }
        public int IdTanamanLama { get; set; }  // Menyimpan ID lama untuk Update
        public string NamaTanaman { get; set; } = string.Empty;
        public string Varietas { get; set; } = string.Empty;
        public int UmurTanaman { get; set; }
        public DateTime? TanggalTanam { get; set; }
        public string JenisKomoditas { get; set; } = string.Empty;


        private DatabaseConfig db = new DatabaseConfig();

        public DataTable GetAllTanaman()
        {
            string query = "SELECT id_tanaman AS \"ID\", nama_tanaman AS \"Nama Tanaman\", jenis_komoditas AS \"Jenis\", varietas AS \"Varietas\" FROM tanaman ORDER BY id_tanaman DESC";
            return db.ExecuteQuery(query);
        }

        public void InsertTanaman()
        {
            string query = "INSERT INTO tanaman (id_tanaman, nama_tanaman, varietas, umur_tanaman, tanggal_tanam, jenis_komoditas) VALUES (@id, @nama, @varietas, @umur, @tanggal, @komoditas)";
            using (NpgsqlCommand cmd = new NpgsqlCommand(query))
            {
                cmd.Parameters.AddWithValue("@id", IdTanaman);
                cmd.Parameters.AddWithValue("@nama", NamaTanaman);
                cmd.Parameters.AddWithValue("@varietas", Varietas);
                cmd.Parameters.AddWithValue("@umur", UmurTanaman);
                cmd.Parameters.AddWithValue("@tanggal", (object)TanggalTanam ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@komoditas", JenisKomoditas);
                db.ExecuteNonQuery(cmd);
            }
        }

        public void UpdateTanaman()
        {
            // Jika ID berubah, gunakan ID lama untuk WHERE clause, dan update ID juga
            if (IdTanamanLama > 0 && IdTanamanLama != IdTanaman)
            {
                using (NpgsqlConnection conn = db.GetConnection())
                {
                    using (NpgsqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            // Pastikan ID baru belum ada
                            string checkNew = "SELECT COUNT(*) FROM tanaman WHERE id_tanaman = @id_new";
                            using (var cmdCheck = new NpgsqlCommand(checkNew, conn, transaction))
                            {
                                cmdCheck.Parameters.AddWithValue("@id_new", IdTanaman);
                                var exists = Convert.ToInt64(cmdCheck.ExecuteScalar());
                                if (exists > 0)
                                    throw new Exception("ID tanaman baru sudah digunakan. Pilih ID lain.");
                            }

                            // 1) Masukkan record tanaman baru dengan ID baru (salin data)
                            string insertTanaman = "INSERT INTO tanaman (id_tanaman, nama_tanaman, varietas, umur_tanaman, tanggal_tanam, jenis_komoditas) VALUES (@id_new, @nama, @varietas, @umur, @tanggal, @komoditas)";
                            using (var cmdInsert = new NpgsqlCommand(insertTanaman, conn, transaction))
                            {
                                cmdInsert.Parameters.AddWithValue("@id_new", IdTanaman);
                                cmdInsert.Parameters.AddWithValue("@nama", NamaTanaman);
                                cmdInsert.Parameters.AddWithValue("@varietas", Varietas);
                                cmdInsert.Parameters.AddWithValue("@umur", UmurTanaman);
                                cmdInsert.Parameters.AddWithValue("@tanggal", (object)TanggalTanam ?? DBNull.Value);
                                cmdInsert.Parameters.AddWithValue("@komoditas", JenisKomoditas);
                                cmdInsert.ExecuteNonQuery();
                            }

                            // 2) Update tabel monitoring untuk merujuk ke ID baru
                            string queryMonitoring = "UPDATE monitoring SET id_tanaman = @id_new WHERE id_tanaman = @id_old";
                            using (NpgsqlCommand cmdMonitoring = new NpgsqlCommand(queryMonitoring, conn, transaction))
                            {
                                cmdMonitoring.Parameters.AddWithValue("@id_new", IdTanaman);
                                cmdMonitoring.Parameters.AddWithValue("@id_old", IdTanamanLama);
                                cmdMonitoring.ExecuteNonQuery();
                            }

                            // 3) Hapus record tanaman lama
                            string deleteOld = "DELETE FROM tanaman WHERE id_tanaman = @id_old";
                            using (var cmdDelete = new NpgsqlCommand(deleteOld, conn, transaction))
                            {
                                cmdDelete.Parameters.AddWithValue("@id_old", IdTanamanLama);
                                cmdDelete.ExecuteNonQuery();
                            }

                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw new Exception("Gagal mengubah ID tanaman: " + ex.Message);
                        }
                    }
                }
            }
            else
            {
                // Update tanpa perubahan ID
                string query = "UPDATE tanaman SET nama_tanaman = @nama, varietas = @varietas, umur_tanaman = @umur, tanggal_tanam = @tanggal, jenis_komoditas = @komoditas WHERE id_tanaman = @id";
                using (NpgsqlCommand cmd = new NpgsqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@id", IdTanaman);
                    cmd.Parameters.AddWithValue("@nama", NamaTanaman);
                    cmd.Parameters.AddWithValue("@varietas", Varietas);
                    cmd.Parameters.AddWithValue("@umur", UmurTanaman);
                    cmd.Parameters.AddWithValue("@tanggal", (object)TanggalTanam ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@komoditas", JenisKomoditas);
                    db.ExecuteNonQuery(cmd);
                }
            }
        }

        public void DeleteTanaman(int id)
        {
            string query = "DELETE FROM tanaman WHERE id_tanaman = @id";
            using (NpgsqlCommand cmd = new NpgsqlCommand(query))
            {
                cmd.Parameters.AddWithValue("@id", id);
                db.ExecuteNonQuery(cmd);
            }
        }
    }
}