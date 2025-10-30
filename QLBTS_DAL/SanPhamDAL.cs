using MySql.Data.MySqlClient;
using QLBTS_DTO;
using System;
using System.Security.Cryptography;
using System.Text;

namespace QLBTS_DAL
{
    public class SanPhamDAL
    {
        // 1️⃣ Lấy chi tiết sản phẩm theo MaSP
        public SanPhamDTO GetSanPham(int maSP)
        {
            SanPhamDTO sp = null;

            try
            {
                using (MySqlConnection conn = DatabaseHelper.GetConnection())
                {
                    string query = @"
                        SELECT MaSP, TenSP, LoaiSP, Size, SoLuong, Gia, KhuyenMai, HinhAnh, TrangThai
                        FROM SanPham
                        WHERE MaSP = @MaSP";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaSP", maSP);

                    conn.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        sp = new SanPhamDTO
                        {
                            MaSP = reader.GetInt32("MaSP"),
                            TenSP = reader.GetString("TenSP"),
                            LoaiSP = reader.GetString("LoaiSP"),
                            Size = reader.GetString("Size"),
                            SoLuong = reader.GetInt32("SoLuong"),
                            Gia = reader.GetInt32("Gia"),
                            KhuyenMai = reader.GetInt32("KhuyenMai"),
                            TrangThai = reader.GetString("TrangThai"),
                            HinhAnh = reader["HinhAnh"] != DBNull.Value ? (byte[])reader["HinhAnh"] : null
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi DAL - GetSanPham: {ex.Message}", ex);
            }

            return sp;
        }

        // 2️⃣ Lấy tất cả sản phẩm cùng tên (để hiển thị tất cả size)
        public List<SanPhamDTO> GetSanPhamTheoTen(string tenSP)
        {
            List<SanPhamDTO> list = new List<SanPhamDTO>();

            try
            {
                using (MySqlConnection conn = DatabaseHelper.GetConnection())
                {
                    string query = @"
                        SELECT MaSP, TenSP, LoaiSP, Size, SoLuong, Gia, KhuyenMai, HinhAnh, TrangThai
                        FROM SanPham
                        WHERE TenSP = @TenSP
                        ORDER BY Size";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@TenSP", tenSP);

                    conn.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        SanPhamDTO sp = new SanPhamDTO
                        {
                            MaSP = reader.GetInt32("MaSP"),
                            TenSP = reader.GetString("TenSP"),
                            LoaiSP = reader.GetString("LoaiSP"),
                            Size = reader.GetString("Size"),
                            SoLuong = reader.GetInt32("SoLuong"),
                            Gia = reader.GetInt32("Gia"),
                            KhuyenMai = reader.GetInt32("KhuyenMai"),
                            TrangThai = reader.GetString("TrangThai"),
                            HinhAnh = reader["HinhAnh"] != DBNull.Value ? (byte[])reader["HinhAnh"] : null
                        };
                        list.Add(sp);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi DAL - GetSanPhamTheoTen: {ex.Message}", ex);
            }

            return list;
        }

        // 3️⃣ Kiểm tra tồn kho
        public bool CheckTonKho(int maSP, int soLuongCanMua)
        {
            try
            {
                int tonKho = GetTonKhoHienTai(maSP);
                return tonKho >= soLuongCanMua;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi DAL - CheckTonKho: {ex.Message}", ex);
            }
        }

        // 4️⃣ Cập nhật tồn kho (trừ khi đặt hàng, cộng khi hủy)
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

        // 5️⃣ Lấy số lượng tồn kho hiện tại
        public int GetTonKhoHienTai(int maSP)
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
                    return result != null ? Convert.ToInt32(result) : 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi DAL - GetTonKhoHienTai: {ex.Message}", ex);
            }
        }

        public SanPhamDTO GetSanPhamCungTenKhacSize(int maSP, string tenSP, string sizeHienTai)
        {
            SanPhamDTO sp = null;

            try
            {
                using (MySqlConnection conn = DatabaseHelper.GetConnection())
                {
                    string query = @"
                SELECT MaSP, TenSP, LoaiSP, Size, SoLuong, Gia, KhuyenMai, HinhAnh, TrangThai
                FROM SanPham
                WHERE TenSP = @TenSP AND Size != @SizeHienTai AND MaSP != @MaSP
                LIMIT 1";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TenSP", tenSP);
                        cmd.Parameters.AddWithValue("@SizeHienTai", sizeHienTai);
                        cmd.Parameters.AddWithValue("@MaSP", maSP);

                        conn.Open();
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                sp = new SanPhamDTO
                                {
                                    MaSP = reader.GetInt32("MaSP"),
                                    TenSP = reader.GetString("TenSP"),
                                    LoaiSP = reader.GetString("LoaiSP"),
                                    Size = reader.GetString("Size"),
                                    SoLuong = reader.GetInt32("SoLuong"),
                                    Gia = reader.GetInt32("Gia"),
                                    KhuyenMai = reader.GetInt32("KhuyenMai"),
                                    TrangThai = reader.GetString("TrangThai"),
                                    HinhAnh = reader["HinhAnh"] != DBNull.Value ? (byte[])reader["HinhAnh"] : null
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi DAL - GetSanPhamCungTenKhacSize: {ex.Message}", ex);
            }

            return sp;
        }


    }
}

