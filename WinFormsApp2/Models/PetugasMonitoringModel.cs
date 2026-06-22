using System;
using System.Data;
using Npgsql;

namespace MonitoringKopiKakao.Model
{
    public class PetugasModel
    {
        public int IdUser { get; set; }
        public int IdUserLama { get; set; }  // Menyimpan ID lama untuk Update
        public string Username { get; set; }
        public string Password { get; set; }
        public string NamaPetugas { get; set; }

        private DatabaseConfig db = new DatabaseConfig();

        public DataTable GetAllPetugas()
        {
            string query = @"SELECT u.id_user AS ""ID"", u.username AS ""Username"", 
                             u.password AS ""Password"", pm.nama AS ""Nama Petugas""
                             FROM users u 
                             JOIN petugas_monitoring pm ON u.id_user = pm.id_user 
                             ORDER BY u.id_user DESC";
            return db.ExecuteQuery(query);
        }

        public void InsertPetugas()
        {
            
            string queryUser = "INSERT INTO users (id_user, username, password) VALUES (@id, @user, @pass)";
            string queryPetugas = "INSERT INTO petugas_monitoring (id_user, nama) VALUES (@id, @nama)";

            using (NpgsqlConnection conn = db.GetConnection())
            {
                using (NpgsqlCommand cmdUser = new NpgsqlCommand(queryUser, conn))
                {
                    cmdUser.Parameters.AddWithValue("@id", IdUser);
                    cmdUser.Parameters.AddWithValue("@user", Username);
                    cmdUser.Parameters.AddWithValue("@pass", Password);
                    cmdUser.ExecuteNonQuery();

                    using (NpgsqlCommand cmdPetugas = new NpgsqlCommand(queryPetugas, conn))
                    {
                        cmdPetugas.Parameters.AddWithValue("@id", IdUser);
                        cmdPetugas.Parameters.AddWithValue("@nama", NamaPetugas);
                        cmdPetugas.ExecuteNonQuery();
                    }
                }
            }
        }
          
        public void UpdatePetugas()
        {
            // Jika ID berubah, perlu update 3 tabel yang saling referensi
            if (IdUserLama > 0 && IdUserLama != IdUser)
            {
                // PENTING: Urutan update untuk cascade update
                // 1. Update tabel monitoring (child yang referensi users)
                // 2. Update tabel petugas_monitoring (child yang referensi users)
                // 3. Update tabel users (parent)

                using (NpgsqlConnection conn = db.GetConnection())
                {
                    using (NpgsqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            // Disable foreign key check sementara untuk semua
                            using (NpgsqlCommand disableFk = new NpgsqlCommand("SET CONSTRAINTS ALL DEFERRED", conn, transaction))
                            {
                                disableFk.ExecuteNonQuery();
                            }

                            // Step 1: Update tabel monitoring DULU (child level 1)
                            string queryMonitoring = "UPDATE monitoring SET id_user = @id_new WHERE id_user = @id_old";
                            using (NpgsqlCommand cmdMonitoring = new NpgsqlCommand(queryMonitoring, conn, transaction))
                            {
                                cmdMonitoring.Parameters.AddWithValue("@id_new", IdUser);
                                cmdMonitoring.Parameters.AddWithValue("@id_old", IdUserLama);
                                cmdMonitoring.ExecuteNonQuery();
                            }

                            // Step 2: Update tabel petugas_monitoring (child level 1)
                            string queryPetugas = "UPDATE petugas_monitoring SET id_user = @id_new WHERE id_user = @id_old";
                            using (NpgsqlCommand cmdPetugas = new NpgsqlCommand(queryPetugas, conn, transaction))
                            {
                                cmdPetugas.Parameters.AddWithValue("@id_new", IdUser);
                                cmdPetugas.Parameters.AddWithValue("@id_old", IdUserLama);
                                cmdPetugas.ExecuteNonQuery();
                            }

                            // Step 3: Baru update tabel users setelah kedua child table sudah ter-update
                            string queryUser = "UPDATE users SET id_user = @id_new, username = @user, password = @pass WHERE id_user = @id_old";
                            using (NpgsqlCommand cmdUser = new NpgsqlCommand(queryUser, conn, transaction))
                            {
                                cmdUser.Parameters.AddWithValue("@id_new", IdUser);
                                cmdUser.Parameters.AddWithValue("@id_old", IdUserLama);
                                cmdUser.Parameters.AddWithValue("@user", Username);
                                cmdUser.Parameters.AddWithValue("@pass", Password);
                                cmdUser.ExecuteNonQuery();
                            }

                            // Commit transaction jika semua berhasil
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            // Rollback jika ada error
                            transaction.Rollback();
                            throw new Exception("Gagal mengubah ID petugas: " + ex.Message);
                        }
                    }
                }
            }
            else
            {
                // Update tanpa perubahan ID
                string queryUser = "UPDATE users SET username = @user, password = @pass WHERE id_user = @id";
                string queryPetugas = "UPDATE petugas_monitoring SET nama = @nama WHERE id_user = @id";

                using (NpgsqlConnection conn = db.GetConnection())
                {
                    using (NpgsqlCommand cmdUser = new NpgsqlCommand(queryUser, conn))
                    {
                        cmdUser.Parameters.AddWithValue("@id", IdUser);
                        cmdUser.Parameters.AddWithValue("@user", Username);
                        cmdUser.Parameters.AddWithValue("@pass", Password);
                        cmdUser.ExecuteNonQuery();
                    }
                    using (NpgsqlCommand cmdPetugas = new NpgsqlCommand(queryPetugas, conn))
                    {
                        cmdPetugas.Parameters.AddWithValue("@id", IdUser);
                        cmdPetugas.Parameters.AddWithValue("@nama", NamaPetugas);
                        cmdPetugas.ExecuteNonQuery();
                    }
                }
            }
        }

        public void DeletePetugas(int id)
        {
            
            string query = "DELETE FROM users WHERE id_user = @id";
            using (NpgsqlCommand cmd = new NpgsqlCommand(query))
            {
                cmd.Parameters.AddWithValue("@id", id);
                db.ExecuteNonQuery(cmd);
            }
        }
    }
}