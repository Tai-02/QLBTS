using MySql.Data.MySqlClient;
using QLBTS_DTO;
using System;
using System.Security.Cryptography;
using System.Text;

namespace QLBTS_DAL
{
    public class SanPhamDAL
    {
        public SanPhamDTO GetSanPhamTheoMaSP(int maSP, string size)
        {
            SanPhamDTO sp = null;

            try
            {
                using (MySqlConnection conn = DatabaseHelper.GetConnection())
                {
                    string query = "";

                    if (size == "M")
                    {
                        query = @"
                    SELECT MaSP, TenSP, LoaiSP, SoLuong, GiaM, KhuyenMaiM, HinhAnh
                    FROM SanPham
                    WHERE MaSP = @MaSP AND GiaM > 0";
                    }
                    else if (size == "L")
                    {
                        query = @"
                    SELECT MaSP, TenSP, LoaiSP, SoLuong, GiaL, KhuyenMaiL, HinhAnh
                    FROM SanPham
                    WHERE MaSP = @MaSP AND GiaL > 0";
                    }
                    else
                    {
                        throw new ArgumentException("Size không hợp lệ.");
                    }

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaSP", maSP);
                        conn.Open();

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                byte[] hinh = reader["HinhAnh"] != DBNull.Value ? (byte[])reader["HinhAnh"] : Array.Empty<byte>();

                                sp = new SanPhamDTO
                                {
                                    MaSP = reader.GetInt32("MaSP"),
                                    TenSP = reader.GetString("TenSP"),
                                    LoaiSP = reader.GetString("LoaiSP"),
                                    SoLuong = reader.GetInt32("SoLuong"),
                                    Size = size,
                                    HinhAnh = hinh
                                };

                                if (size == "M")
                                {
                                    sp.GiaM = reader["GiaM"] != DBNull.Value ? Convert.ToInt32(reader["GiaM"]) : 0;
                                    sp.KhuyenMaiM = reader.GetInt32("KhuyenMaiM");
                                }
                                else // L
                                {
                                    sp.GiaL = reader["GiaL"] != DBNull.Value ? Convert.ToInt32(reader["GiaL"]) : 0;
                                    sp.KhuyenMaiL = reader.GetInt32("KhuyenMaiL");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi DAL - GetSanPhamTheoMaSP: {ex.Message}", ex);
            }

            return sp;
        }

        // 1️⃣ Lấy chi tiết sản phẩm theo MaSP
        public List<SanPhamDTO> GetSanPhamHienThi(int maSP)
        {
            List<SanPhamDTO> list = new List<SanPhamDTO>();

            try
            {
                using (MySqlConnection conn = DatabaseHelper.GetConnection())
                {
                    string query = @"
                SELECT MaSP, TenSP, LoaiSP, SoLuong, GiaM, GiaL, KhuyenMaiM, KhuyenMaiL, HinhAnh
                FROM SanPham
                WHERE MaSP = @MaSP
                  AND (GiaM > 0 OR GiaL > 0)"; // chỉ lấy SP có giá

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaSP", maSP);

                        conn.Open();
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int ma = reader.GetInt32("MaSP");
                                string ten = reader.GetString("TenSP");
                                string loai = reader.GetString("LoaiSP");
                                int soLuong = reader.GetInt32("SoLuong"); byte[] hinh = reader["HinhAnh"] != DBNull.Value ? (byte[])reader["HinhAnh"] : Array.Empty<byte>();
                                int giaM = reader["GiaM"] != DBNull.Value ? Convert.ToInt32(reader["GiaM"]) : 0;
                                int giaL = reader["GiaL"] != DBNull.Value ? Convert.ToInt32(reader["GiaL"]) : 0;
                                int kmM = reader.GetInt32("KhuyenMaiM");
                                int kmL = reader.GetInt32("KhuyenMaiL");

                                // Size M
                                if (giaM > 0)
                                {
                                    list.Add(new SanPhamDTO
                                    {
                                        MaSP = ma,
                                        TenSP = ten,
                                        LoaiSP = loai,
                                        SoLuong = soLuong,
                                        GiaM = giaM,
                                        KhuyenMaiM = kmM,
                                        Size = "M",
                                        HinhAnh = hinh
                                    });
                                }

                                // Size L
                                if (giaL > 0)
                                {
                                    list.Add(new SanPhamDTO
                                    {
                                        MaSP = ma,
                                        TenSP = ten,
                                        LoaiSP = loai,
                                        SoLuong = soLuong,
                                        GiaL = giaL,
                                        KhuyenMaiL = kmL,
                                        Size = "L",
                                        HinhAnh = hinh
                                    });
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi DAL - GetSanPhamHienThi: {ex.Message}", ex);
            }

            return list;
        }


        public bool CheckTonKho(int maSP, int soLuongCanMua)
        {
            try
            {
                using (MySqlConnection conn = DatabaseHelper.GetConnection())
                {
                    string query = "SELECT SoLuong FROM SanPham WHERE MaSP = @MaSP";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaSP", maSP);

                    conn.Open();
                    object result = cmd.ExecuteScalar();

                    if (result == null)
                        throw new Exception("Sản phẩm không tồn tại.");

                    int tonKho = Convert.ToInt32(result);
                    return tonKho >= soLuongCanMua;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi DAL - CheckTonKho: {ex.Message}", ex);
            }
        }



        public bool CapNhatTonKho(int maSP, int soLuongThayDoi)
        {
            try
            {
                using (MySqlConnection conn = DatabaseHelper.GetConnection())
                {
                    string query = @"
                        UPDATE SanPham
                        SET SoLuong = SoLuong + @SoLuongThayDoi
                        WHERE MaSP = @MaSP";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaSP", maSP);
                    cmd.Parameters.AddWithValue("@SoLuongThayDoi", soLuongThayDoi);

                    conn.Open();
                    int row = cmd.ExecuteNonQuery();
                    return row > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi DAL - CapNhatTonKho: {ex.Message}", ex);
            }
        }

        public int GetGiaHienTai(int maSP, string size)
        {
            try
            {
                using (MySqlConnection conn = DatabaseHelper.GetConnection())
                {
                    string query = "";

                    if (size == "M")
                    {
                        query = "SELECT GiaM FROM SanPham WHERE MaSP = @MaSP";
                    }
                    else if (size == "L")
                    {
                        query = "SELECT GiaL FROM SanPham WHERE MaSP = @MaSP";
                    }
                    else
                    {
                        throw new ArgumentException("Size không hợp lệ.");
                    }

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaSP", maSP);

                        conn.Open();
                        object result = cmd.ExecuteScalar();

                        if (result == null || result == DBNull.Value)
                            return 0;

                        return Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi DAL - GetGiaHienTai: {ex.Message}", ex);
            }
        }

        public int GetSoLuongTon(int maSP)
        {
            try
            {
                using (MySqlConnection conn = DatabaseHelper.GetConnection())
                {
                    string query = "SELECT SoLuong FROM SanPham WHERE MaSP = @MaSP";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaSP", maSP);
                        conn.Open();

                        object result = cmd.ExecuteScalar();

                        if (result == null || result == DBNull.Value)
                            return 0;

                        return Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi DAL - GetSoLuongTon: {ex.Message}", ex);
            }
        }

        public string GetTenSanPham(int maSP)
        {
            try
            {
                using (MySqlConnection conn = DatabaseHelper.GetConnection())
                {
                    string query = "SELECT TenSP FROM SanPham WHERE MaSP = @MaSP";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaSP", maSP);
                        conn.Open();
                        object result = cmd.ExecuteScalar();
                        if (result == null || result == DBNull.Value)
                            return string.Empty;
                        return result.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi DAL - GetTenSanPham: {ex.Message}", ex);
            }
        }
    }
}

