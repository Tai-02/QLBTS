using MySql.Data.MySqlClient;
using QLBTS_DTO;
using System;
using System.Security.Cryptography;
using System.Text;

namespace QLBTS_DAL
{
    public class TaiKhoanDAL
    {
        public TaiKhoanDTO LayThongTinTaiKhoanTheoMaTK(int maTK)
        {
            TaiKhoanDTO tk = null;
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = @"SELECT TenDangNhap, MatKhau, Email, HoTen, SDT, DiaChi, Anh 
                         FROM TaiKhoan 
                         WHERE MaTK = @MaTK";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaTK", maTK);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            tk = new TaiKhoanDTO
                            {
                                TenDangNhap = reader["TenDangNhap"].ToString(),
                                MatKhau = reader["MatKhau"].ToString(),
                                Email = reader["Email"].ToString(),
                                HoTen = reader["HoTen"].ToString(),
                                SDT = reader["SDT"].ToString(),
                                DiaChi = reader["DiaChi"].ToString(),
                                Anh = reader["Anh"] != DBNull.Value ? (byte[])reader["Anh"] : null
                            };
                        }
                    }
                }
            }
            return tk;
        }

        public bool CapNhatThongTin(TaiKhoanDTO tk)
        {
            // Kiểm tra email đã tồn tại (trừ email của chính tài khoản này)
            if (EmailTonTai_koTK(tk.Email, tk.MaTK))
                throw new Exception("Email đã tồn tại, vui lòng chọn email khác.");

            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string sql = @"UPDATE TaiKhoan 
                           SET HoTen = @HoTen,
                               Email = @Email,
                               SDT = @SDT,
                               DiaChi = @DiaChi";

                    if (tk.Anh != null)
                        sql += ", Anh = @Anh";

                    sql += " WHERE MaTK = @MaTK";

                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@HoTen", tk.HoTen);
                        cmd.Parameters.AddWithValue("@Email", tk.Email);
                        cmd.Parameters.AddWithValue("@SDT", tk.SDT);
                        cmd.Parameters.AddWithValue("@DiaChi", tk.DiaChi);
                        cmd.Parameters.AddWithValue("@MaTK", tk.MaTK);

                        if (tk.Anh != null)
                            cmd.Parameters.AddWithValue("@Anh", tk.Anh);

                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi DAL - CapNhatThongTin: " + ex.Message, ex);
            }
        }


        private string HashMatKhau(string matKhau)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(matKhau);
                byte[] hash = sha.ComputeHash(bytes);
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hash)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }

        public bool Kiemtra(TaiKhoanDTO tk)
        {
            try
            {
                using (MySqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();

                    string sql = "SELECT COUNT(*) FROM TaiKhoan WHERE TenDangNhap=@u AND MatKhau=@p";
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@u", tk.TenDangNhap); 
                        if (tk.MatKhau == null)
                            throw new ArgumentNullException(nameof(tk.MatKhau));

                        cmd.Parameters.AddWithValue("@p", HashMatKhau(tk.MatKhau));


                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi kiểm tra tài khoản: " + ex.Message);
                return false;
            }
        }

        public bool TenDangNhapTonTai(string tenDangNhap)
        {
            try
            {
                using (MySqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string sql = "SELECT COUNT(*) FROM TaiKhoan WHERE TenDangNhap = @u";
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@u", tenDangNhap);
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0;
                    }
                }
            }
            catch
            {
                return true;
            }
        }

        public bool EmailTonTai(string email)
        {
            try
            {
                using (MySqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string sql = "SELECT COUNT(*) FROM TaiKhoan WHERE Email = @e";
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@e", email);
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0;
                    }
                }
            }
            catch
            {
                return true;
            }
        }
        public bool EmailTonTai_koTK(string email, int maTK)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string sql = "SELECT COUNT(*) FROM TaiKhoan WHERE Email = @e AND MaTK <> @MaTK";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@e", email);
                    cmd.Parameters.AddWithValue("@MaTK", maTK);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }


        public bool InsertTaiKhoan(TaiKhoanDTO tk)
        {
            try
            {
                using (MySqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string sql = @"INSERT INTO TaiKhoan 
                                   (TenDangNhap, MatKhau, Email, Otp, VaiTro, Active, NgayTao, LevelID)
                                   VALUES (@TenDangNhap, @MatKhau, @Email, @Otp, @VaiTro, @Active, @NgayTao, @LevelID)";
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@TenDangNhap", tk.TenDangNhap);
                        if (tk.MatKhau == null)
                            throw new ArgumentNullException(nameof(tk.MatKhau));

                        cmd.Parameters.AddWithValue("@p", HashMatKhau(tk.MatKhau));
                        cmd.Parameters.AddWithValue("@Email", tk.Email);
                        cmd.Parameters.AddWithValue("@Otp", tk.Otp);
                        cmd.Parameters.AddWithValue("@VaiTro", tk.VaiTro);
                        cmd.Parameters.AddWithValue("@Active", tk.Active);
                        cmd.Parameters.AddWithValue("@NgayTao", tk.NgayTao);
                        cmd.Parameters.AddWithValue("@LevelID", tk.LevelID);

                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public bool KichHoatTaiKhoan(string email, string otp)
        {
            try
            {
                using (MySqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string sql = @"UPDATE TaiKhoan 
                                   SET Active = 1, NgayKichHoat = NOW() 
                                   WHERE Email = @Email AND Otp = @Otp";
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Otp", otp);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public bool KiemTraOTP(string email, string otp)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string sql = "SELECT COUNT(*) FROM TaiKhoan WHERE Email=@e AND Otp=@o";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@e", email);
                    cmd.Parameters.AddWithValue("@o", otp);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    if (count > 0)
                    {
                        string update = "UPDATE TaiKhoan SET Active=1, NgayKichHoat=NOW() WHERE Email=@e";
                        using (var up = new MySqlCommand(update, conn))
                        {
                            up.Parameters.AddWithValue("@e", email);
                            up.ExecuteNonQuery();
                        }
                        return true;
                    }
                    return false;
                }
            }
        }

        public int LayLevelID(string tenDangNhap)
        {
            int levelID = -1;

            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = "SELECT LevelID FROM TaiKhoan WHERE TenDangNhap = @TenDangNhap";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                    object result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                        levelID = Convert.ToInt32(result);
                }
            }

            return levelID;
        }

        public int LayMaTK(string tenDangNhap)
        {
            try
            {
                using (MySqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT MaTK FROM TaiKhoan WHERE TenDangNhap = @TenDangNhap";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                        object result = cmd.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                            return Convert.ToInt32(result);
                        else
                            return -1; 
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi DAL - LayMaTK: {ex.Message}", ex);
            }
        }
    }
}
