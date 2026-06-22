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
            // Jika ID berubah, lakukan perpindahan entitas dengan aman
            if (IdUserLama > 0 && IdUserLama != IdUser)
            {
                using (NpgsqlConnection conn = db.GetConnection())
                {
                    using (NpgsqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            // Pastikan ID baru belum dipakai
                            string checkNewId = "SELECT COUNT(*) FROM users WHERE id_user = @id_new";
                            using (var cmdCheck = new NpgsqlCommand(checkNewId, conn, transaction))
                            {
                                cmdCheck.Parameters.AddWithValue("@id_new", IdUser);
                                var exists = Convert.ToInt64(cmdCheck.ExecuteScalar());
                                if (exists > 0)
                                    throw new Exception("ID baru sudah digunakan. Pilih ID lain.");
                            }

                            // Pastikan username tidak dipakai oleh account lain (kecuali id lama)
                            string checkUser = "SELECT id_user FROM users WHERE username = @user";
                            object obj = null;
                            int? foundId = null;
                            using (var cmdCheckUser = new NpgsqlCommand(checkUser, conn, transaction))
                            {
                                cmdCheckUser.Parameters.AddWithValue("@user", Username ?? string.Empty);
                                obj = cmdCheckUser.ExecuteScalar();
                                if (obj != null && obj != DBNull.Value)
                                {
                                    foundId = Convert.ToInt32(obj);
                                    if (foundId != IdUserLama)
                                        throw new Exception("Username sudah digunakan oleh akun lain.");
                                }
                            }


                            // 1) Jika username yang akan digunakan saat ini dimiliki oleh akun lama,
                            //    maka ganti username akun lama sementara untuk menghindari
                            //    pelanggaran constraint unique saat melakukan insert user baru.
                            bool usernameBelongsToOld = false;
                            string tempUsername = null;
                            if (foundId.HasValue && foundId == IdUserLama)
                            {
                                usernameBelongsToOld = true;
                            }

                            if (usernameBelongsToOld)
                            {
                                tempUsername = (Username ?? string.Empty) + "_tmp_" + Guid.NewGuid().ToString("N");
                                string updateOldUsername = "UPDATE users SET username = @temp WHERE id_user = @id_old";
                                using (var cmdTemp = new NpgsqlCommand(updateOldUsername, conn, transaction))
                                {
                                    cmdTemp.Parameters.AddWithValue("@temp", tempUsername);
                                    cmdTemp.Parameters.AddWithValue("@id_old", IdUserLama);
                                    cmdTemp.ExecuteNonQuery();
                                }
                            }

                            // 2) Masukkan record users baru dengan ID baru (nama pengguna asli)
                            string insertUser = "INSERT INTO users (id_user, username, password) VALUES (@id_new, @user, @pass)";
                            using (var cmdInsert = new NpgsqlCommand(insertUser, conn, transaction))
                            {
                                cmdInsert.Parameters.AddWithValue("@id_new", IdUser);
                                cmdInsert.Parameters.AddWithValue("@user", Username ?? string.Empty);
                                cmdInsert.Parameters.AddWithValue("@pass", Password ?? string.Empty);
                                cmdInsert.ExecuteNonQuery();
                            }

                            // 2) Pindahkan referensi di tabel monitoring ke ID baru
                            string queryMonitoring = "UPDATE monitoring SET id_user = @id_new WHERE id_user = @id_old";
                            using (NpgsqlCommand cmdMonitoring = new NpgsqlCommand(queryMonitoring, conn, transaction))
                            {
                                cmdMonitoring.Parameters.AddWithValue("@id_new", IdUser);
                                cmdMonitoring.Parameters.AddWithValue("@id_old", IdUserLama);
                                cmdMonitoring.ExecuteNonQuery();
                            }

                            // 3) Update petugas_monitoring untuk menggunakan ID baru
                            string queryPetugas = "UPDATE petugas_monitoring SET id_user = @id_new WHERE id_user = @id_old";
                            using (NpgsqlCommand cmdPetugas = new NpgsqlCommand(queryPetugas, conn, transaction))
                            {
                                cmdPetugas.Parameters.AddWithValue("@id_new", IdUser);
                                cmdPetugas.Parameters.AddWithValue("@id_old", IdUserLama);
                                cmdPetugas.ExecuteNonQuery();
                            }

                            // 4) Hapus record users lama
                            string deleteOldUser = "DELETE FROM users WHERE id_user = @id_old";
                            using (var cmdDelete = new NpgsqlCommand(deleteOldUser, conn, transaction))
                            {
                                cmdDelete.Parameters.AddWithValue("@id_old", IdUserLama);
                                cmdDelete.ExecuteNonQuery();
                            }

                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
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
                        cmdUser.Parameters.AddWithValue("@user", Username ?? string.Empty);
                        cmdUser.Parameters.AddWithValue("@pass", Password ?? string.Empty);
                        cmdUser.ExecuteNonQuery();
                    }
                    using (NpgsqlCommand cmdPetugas = new NpgsqlCommand(queryPetugas, conn))
                    {
                        cmdPetugas.Parameters.AddWithValue("@id", IdUser);
                        cmdPetugas.Parameters.AddWithValue("@nama", NamaPetugas ?? string.Empty);
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