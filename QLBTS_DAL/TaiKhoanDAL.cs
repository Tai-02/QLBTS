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

                    // SỬA ĐỔI SQL: Thêm điều kiện Active = 1 (hoặc Active = TRUE)
                    string sql = "SELECT COUNT(*) FROM TaiKhoan WHERE TenDangNhap=@u AND MatKhau=@p AND Active = 1";

                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@u", tk.TenDangNhap);

                        if (tk.MatKhau == null)
                            throw new ArgumentNullException(nameof(tk.MatKhau));

                        // Giả định HashMatKhau(tk.MatKhau) là hàm băm mật khẩu
                        cmd.Parameters.AddWithValue("@p", HashMatKhau(tk.MatKhau));

                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Vui lòng đổi Console.WriteLine thành ghi Log hoặc ném Exception cho BLL xử lý
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
        public bool TenDangNhapTonTai_koTK(string tenDangNhap, int maTK)
        {
            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string sql = "SELECT COUNT(*) FROM TaiKhoan WHERE TenDangNhap = @u AND MaTK <> @MaTK";
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@u", tenDangNhap);
                        cmd.Parameters.AddWithValue("@MaTK", maTK);

                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Ghi log lỗi hoặc ném ra exception chi tiết hơn nếu cần
                Console.WriteLine("Lỗi DAL - TenDangNhapTonTai_koTK: " + ex.Message);
                // Trong trường hợp lỗi kết nối, ta giả định là lỗi (true) để ngăn việc cập nhật dữ liệu sai
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
            try
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
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi DAL - EmailTonTai_koTK: " + ex.Message);
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
                        cmd.Parameters.AddWithValue("@MatKhau", HashMatKhau(tk.MatKhau));

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
            catch (Exception ex) 
            {
                Console.WriteLine("Lỗi InsertTaiKhoan: " + ex.Message);
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
        public List<TaiKhoanDTO> LayDanhSachNhanVien()
        {
            List<TaiKhoanDTO> danhSach = new List<TaiKhoanDTO>();

            // Sắp xếp: ORDER BY Active DESC (1 lên trước 0), MaTK ASC
            // Trong QLBTS_BLL/TaiKhoanBLL.LayDanhSachNhanVien()
            string query = @"
                SELECT 
                MaTK, HoTen, TenDangNhap, SDT, DiaChi, Email, VaiTro, Active, LevelID, TrangThai  
                FROM 
                TaiKhoan
                WHERE 
                LevelID IN (2, 3) 
                ORDER BY 
                Active DESC, MaTK ASC";

            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                danhSach.Add(new TaiKhoanDTO
                                {
                                    // Chỉ lấy các trường bạn yêu cầu:
                                    MaTK = Convert.ToInt32(reader["MaTK"]),
                                    HoTen = reader["HoTen"] != DBNull.Value ? reader["HoTen"].ToString() : null,
                                    TenDangNhap = reader["TenDangNhap"].ToString(),
                                    SDT = reader["SDT"] != DBNull.Value ? reader["SDT"].ToString() : null,
                                    DiaChi = reader["DiaChi"] != DBNull.Value ? reader["DiaChi"].ToString() : null,
                                    Email = reader["Email"].ToString(),
                                    VaiTro = reader["VaiTro"].ToString(),
                                    LevelID = Convert.ToInt32(reader["LevelID"]),
                                    Active = Convert.ToBoolean(reader["Active"]), // Lấy trường Active để phục vụ sắp xếp
                                    TrangThai = reader["TrangThai"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Ghi log lỗi hoặc ném ra Exception nếu cần xử lý ở tầng trên
                Console.WriteLine("Lỗi DAL - LayDanhSachNhanVien: " + ex.Message);
                // Có thể throw new Exception("..."); ở đây
            }

            return danhSach;
        }

        public bool XoaTaiKhoan(int maTK)
        {
            try
            {
                using (MySqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    // Xóa tài khoản với điều kiện MaTK
                    string sql = "DELETE FROM TaiKhoan WHERE MaTK = @MaTK";

                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaTK", maTK);

                        // ExecuteNonQuery trả về số hàng bị ảnh hưởng.
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Ghi log lỗi nếu có vấn đề về kết nối hoặc ràng buộc khóa ngoại (Foreign Key)
                Console.WriteLine("Lỗi DAL - XoaTaiKhoan: " + ex.Message);
                // Ném ra Exception để BLL có thể bắt và thông báo chi tiết hơn
                throw new Exception("Xóa tài khoản thất bại. Có thể tài khoản này đang liên kết với dữ liệu khác (ví dụ: hóa đơn).", ex);
            }
        }

        public int LayMaTK_TheoEmail(string email)
        {
            try
            {
                using (MySqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT MaTK FROM TaiKhoan WHERE Email = @Email";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);
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
                throw new Exception($"Lỗi DAL - LayMaTK_TheoEmail: {ex.Message}", ex);
            }
        }

        // Trong TaiKhoanDAL.cs

        public bool CapNhatTaiKhoan_QuanLy(TaiKhoanDTO tk)
        {
            // Kiểm tra tên đăng nhập đã tồn tại (trừ tài khoản này)
            if (TenDangNhapTonTai_koTK(tk.TenDangNhap, tk.MaTK)) // GỌI HÀM MỚI
                throw new Exception("Tên đăng nhập đã tồn tại, vui lòng chọn tên khác.");

            // Kiểm tra email đã tồn tại (trừ email của chính tài khoản này)
            if (EmailTonTai_koTK(tk.Email, tk.MaTK))
                throw new Exception("Email đã tồn tại, vui lòng chọn email khác.");

            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string sql = @"UPDATE TaiKhoan 
                        SET TenDangNhap = @TenDangNhap,
                            HoTen = @HoTen,
                            Email = @Email,
                            SDT = @SDT,
                            DiaChi = @DiaChi,
                            VaiTro = @VaiTro, 
                            LevelID = @LevelID, 
                            Active = @Active, 
                            TrangThai = @TrangThai
                            WHERE MaTK = @MaTK";

                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@TenDangNhap", tk.TenDangNhap); 
                        cmd.Parameters.AddWithValue("@HoTen", tk.HoTen);
                        cmd.Parameters.AddWithValue("@Email", tk.Email);
                        cmd.Parameters.AddWithValue("@SDT", tk.SDT);
                        cmd.Parameters.AddWithValue("@DiaChi", tk.DiaChi);
                        cmd.Parameters.AddWithValue("@VaiTro", tk.VaiTro);
                        cmd.Parameters.AddWithValue("@LevelID", tk.LevelID);
                        cmd.Parameters.AddWithValue("@Active", tk.Active);
                        cmd.Parameters.AddWithValue("@TrangThai", tk.TrangThai);
                        cmd.Parameters.AddWithValue("@MaTK", tk.MaTK);

                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("tồn tại")) // Bắt lỗi nghiệp vụ
                {
                    throw;
                }
                throw new Exception("Lỗi DAL - CapNhatTaiKhoan_QuanLy: " + ex.Message, ex);
            }
        }

        public bool KiemTraMatKhau(TaiKhoanDTO tk)
        {
            try
            {
                using (MySqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    // CHỈ KIỂM TRA TÊN ĐĂNG NHẬP VÀ MẬT KHẨU
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
                // Ném lỗi SQL/kết nối để tầng BLL biết không phải lỗi nghiệp vụ
                throw new Exception("Lỗi DAL khi kiểm tra mật khẩu.", ex);
            }
        }

        // Hàm lấy trạng thái Active (0: inactive, 1: active, -1: không tìm thấy)
        public int LayTrangThaiActive(string tenDangNhap)
        {
            try
            {
                using (MySqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    // CHỈ SELECT Active
                    string query = "SELECT Active FROM TaiKhoan WHERE TenDangNhap = @u";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@u", tenDangNhap);
                        object result = cmd.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                            return Convert.ToInt32(result); // Trả về 0 hoặc 1
                        else
                            return -1; // Không tìm thấy tài khoản
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi DAL khi lấy trạng thái Active.", ex);
            }
        }

        public bool CapNhatOTPVaNgayTao(string email, string otp)
        {
            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string sql = @"UPDATE TaiKhoan 
                                   SET Otp = @Otp, 
                                       NgayTao = NOW() 
                                   WHERE Email = @Email";

                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Otp", otp);
                        cmd.Parameters.AddWithValue("@Email", email);

                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi DAL - CapNhatOTPVaNgayTao: " + ex.Message);
                throw new Exception("Cập nhật OTP và thời gian thất bại.", ex);
            }
        }

        public string LayEmailTheoTenDangNhap(string tenDangNhap)
        {
            try
            {
                using (MySqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT Email FROM TaiKhoan WHERE TenDangNhap = @TenDangNhap";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                        object result = cmd.ExecuteScalar();

                        // Kiểm tra kết quả trả về
                        if (result != null && result != DBNull.Value)
                            return result.ToString(); // Trả về chuỗi Email
                        else
                            return null; // Không tìm thấy tài khoản
                    }
                }
            }
            catch (Exception ex)
            {
                // Ném ra Exception để BLL có thể bắt và xử lý lỗi kết nối/SQL
                throw new Exception($"Lỗi DAL - LayEmailTheoTenDangNhap: {ex.Message}", ex);
            }
        }
    }
}
