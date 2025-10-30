using MySql.Data.MySqlClient;
using QLBTS_DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QLBTS_DAL
{
    public class DonHangDAL
    {        
        public bool DoiTrangThai(int maDH, string trangThaiMoi)
        {
            string query = @"
                UPDATE DonHang
                SET TrangThai = @TrangThaiMoi
                WHERE MaDH = @MaDH;
            ";

            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaDH", maDH);
                        cmd.Parameters.AddWithValue("@TrangThaiMoi", trangThaiMoi);

                        int row = cmd.ExecuteNonQuery();
                        return row > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi DAL - DoiTrangThai: {ex.Message}", ex);
            }
        }

        public decimal[] TinhTongTienDonHang(int maDH)
        {
            string query = @"
                SELECT 
                SUM(ct.SoLuong * sp.Gia) AS TongTruocKM,
                SUM(ct.SoLuong * (sp.Gia - (sp.Gia * sp.KhuyenMai / 100))) AS TongSauKM
                FROM ChiTietDonHang ct
                JOIN SanPham sp ON ct.MaSP = sp.MaSP
                WHERE ct.MaDH = @MaDH;
            ";

            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaDH", maDH);

                        using (var reader = cmd.ExecuteReader())
                        {
                            decimal tongTruocKM = 0;
                            decimal tongSauKM = 0;

                            if (reader.Read())
                            {
                                tongTruocKM = reader["TongTruocKM"] != DBNull.Value
                                    ? Convert.ToDecimal(reader["TongTruocKM"])
                                    : 0;

                                tongSauKM = reader["TongSauKM"] != DBNull.Value
                                    ? Convert.ToDecimal(reader["TongSauKM"])
                                    : 0;
                            }

                            // 🔹 Làm tròn đến hàng nghìn
                            tongTruocKM = Math.Round(tongTruocKM / 1000m, 0) * 1000;
                            tongSauKM = Math.Round(tongSauKM / 1000m, 0) * 1000;

                            return new decimal[] { tongTruocKM, tongSauKM };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi DAL - TinhTongTienDonHang: {ex.Message}", ex);
            }
        }

        // 🔹 Lấy danh sách đơn hàng (toàn bộ hoặc theo mã khách)
        public List<DonHangDTO> LayDSDonHang(int? maKhach = null)
        {
            List<DonHangDTO> danhSach = new List<DonHangDTO>();

            string query = @"
                SELECT MaDH, MaKhach, NgayDat, TongTien, TrangThai
                FROM DonHang
                /**where**/
                ORDER BY NgayDat DESC;
            ";

            // Nếu có truyền mã khách → lọc theo mã đó
            if (maKhach.HasValue)
                query = query.Replace("/**where**/", "WHERE MaKhach = @MaKhach");
            else
                query = query.Replace("/**where**/", "");

            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        if (maKhach.HasValue)
                            cmd.Parameters.AddWithValue("@MaKhach", maKhach.Value);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DonHangDTO dh = new DonHangDTO
                                {
                                    MaDH = reader.GetInt32("MaDH"),
                                    NgayDat = reader.GetDateTime("NgayDat"),
                                    TongTien = reader.GetInt32("TongTien"),
                                    TrangThai = reader.GetString("TrangThai")
                                };
                                danhSach.Add(dh);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi DAL - LayDSDonHang: {ex.Message}", ex);
            }

            return danhSach;
        }
    }
}
