using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using QLBTS_DTO;

namespace QLBTS_DAL
{
    public class GioHangDAL
    {
        // 1️⃣ Lấy giỏ hàng theo mã khách
        public static List<SanPhamDTO> LayGioHangTheoMaKH(int maKH)
        {
            List<SanPhamDTO> danhSach = new List<SanPhamDTO>();

            string query = @"
                SELECT 
                    sp.MaSP,
                    sp.TenSP,
                    sp.Size,
                    sp.Gia,
                    sp.KhuyenMai,
                    sp.HinhAnh,
                    ctgh.SoLuong
                FROM ChiTietGioHang ctgh
                JOIN SanPham sp ON ctgh.MaSP = sp.MaSP
                JOIN GioHang gh ON ctgh.MaGH = gh.MaGH
                WHERE gh.MaKH = @MaKH;
            ";

            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaKH", maKH);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            SanPhamDTO sp = new SanPhamDTO
                            {
                                MaSP = reader.GetInt32("MaSP"),
                                TenSP = reader.GetString("TenSP"),
                                Size = reader.GetString("Size"),
                                Gia = reader.GetInt32("Gia"),
                                KhuyenMai = reader.GetInt32("KhuyenMai"),
                                SoLuong = reader.GetInt32("SoLuong"),
                                HinhAnh = reader["HinhAnh"] == DBNull.Value ? null : (byte[])reader["HinhAnh"]
                            };
                            danhSach.Add(sp);
                        }
                    }
                }
            }

            return danhSach;
        }

        // 2️⃣ Cập nhật số lượng sản phẩm trong giỏ hàng
        public static void CapNhatSoLuong(int maKH, int maSP, int soLuong)
        {
            string query = @"
                UPDATE ChiTietGioHang ctgh
                JOIN GioHang gh ON ctgh.MaGH = gh.MaGH
                SET ctgh.SoLuong = @SoLuong
                WHERE gh.MaKH = @MaKH AND ctgh.MaSP = @MaSP;
            ";

            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SoLuong", soLuong);
                    cmd.Parameters.AddWithValue("@MaKH", maKH);
                    cmd.Parameters.AddWithValue("@MaSP", maSP);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // 3️⃣ Xóa sản phẩm khỏi giỏ hàng
        public static void XoaSanPhamKhoiGio(int maKH, int maSP)
        {
            string query = @"
                DELETE ctgh FROM ChiTietGioHang ctgh
                JOIN GioHang gh ON ctgh.MaGH = gh.MaGH
                WHERE gh.MaKH = @MaKH AND ctgh.MaSP = @MaSP;
            ";

            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaKH", maKH);
                    cmd.Parameters.AddWithValue("@MaSP", maSP);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // 4️⃣ Xóa toàn bộ giỏ hàng sau khi thanh toán
        public static void XoaToanBoGio(int maKH)
        {
            string query = @"
                DELETE ctgh FROM ChiTietGioHang ctgh
                JOIN GioHang gh ON ctgh.MaGH = gh.MaGH
                WHERE gh.MaKH = @MaKH;
            ";

            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaKH", maKH);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
