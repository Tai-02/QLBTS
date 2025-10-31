using MySql.Data.MySqlClient;
using QLBTS_DTO;
using System;
using System.Security.Cryptography;
using System.Text;

namespace QLBTS_DAL
{
    public class TaiKhoanDAL
    {
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
