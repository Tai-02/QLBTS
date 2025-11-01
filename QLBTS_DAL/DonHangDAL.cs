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
                SUM(ct.SoLuong * COALESCE(sp.GiaM, sp.GiaL)) AS TongTruocKM,
                SUM(ct.SoLuong * (
                    CASE 
                    WHEN sp.GiaM IS NOT NULL THEN sp.GiaM * (1 - sp.KhuyenMaiM / 100)
                    ELSE sp.GiaL * (1 - sp.KhuyenMaiL / 100)
                    END
                )) AS TongSauKM
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

        public bool HuyDonHang(int maDH)
        {
            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();

                    using (var transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            // 1️⃣ Lấy danh sách sản phẩm và số lượng trong đơn
                            string queryChiTiet = @"
                        SELECT MaSP, SoLuong
                        FROM ChiTietDonHang
                        WHERE MaDH = @MaDH;
                    ";

                            var chiTiet = new List<(int MaSP, int SoLuong)>();
                            using (var cmdChiTiet = new MySqlCommand(queryChiTiet, conn, transaction))
                            {
                                cmdChiTiet.Parameters.AddWithValue("@MaDH", maDH);
                                using (var reader = cmdChiTiet.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        chiTiet.Add((
                                            reader.GetInt32("MaSP"),
                                            reader.GetInt32("SoLuong")
                                        ));
                                    }
                                }
                            }

                            // 2️⃣ Cập nhật trạng thái đơn hàng
                            string queryUpdateDH = @"
                        UPDATE DonHang
                        SET TrangThai = 'Đã hủy'
                        WHERE MaDH = @MaDH;
                    ";
                            using (var cmdUpdateDH = new MySqlCommand(queryUpdateDH, conn, transaction))
                            {
                                cmdUpdateDH.Parameters.AddWithValue("@MaDH", maDH);
                                cmdUpdateDH.ExecuteNonQuery();
                            }

                            // 3️⃣ Cộng lại số lượng sản phẩm vào kho
                            string queryUpdateSP = @"
                        UPDATE SanPham
                        SET SoLuong = SoLuong + @SoLuong
                        WHERE MaSP = @MaSP;
                    ";

                            foreach (var item in chiTiet)
                            {
                                using (var cmdUpdateSP = new MySqlCommand(queryUpdateSP, conn, transaction))
                                {
                                    cmdUpdateSP.Parameters.AddWithValue("@SoLuong", item.SoLuong);
                                    cmdUpdateSP.Parameters.AddWithValue("@MaSP", item.MaSP);
                                    cmdUpdateSP.ExecuteNonQuery();
                                }
                            }

                            transaction.Commit();
                            return true;
                        }
                        catch
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi DAL - HuyDonHang: {ex.Message}", ex);
            }
        }

        public List<DonHangDTO> LayDSDonHangTheoTrangThai(string trangThai)
        {
            List<DonHangDTO> danhSach = new List<DonHangDTO>();

            string query = @"
                SELECT MaDH, MaKhach, NgayDat, TongTien, TrangThai
                FROM DonHang
                WHERE TrangThai = @TrangThai
                ORDER BY NgayDat DESC;
            ";

            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TrangThai", trangThai);

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
                throw new Exception($"Lỗi DAL - LayDSDonHangTheoTrangThai: {ex.Message}", ex);
            }

            return danhSach;
        }
        public int? LayMaNVG(int maDH)
        {
            string query = @"
                SELECT MaNVGiao
                FROM DonHang
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

                        object result = cmd.ExecuteScalar();

                        // Trả về NULL nếu giá trị DB là DBNull.Value, ngược lại trả về giá trị int
                        if (result == null || result == DBNull.Value)
                        {
                            return null;
                        }
                        return Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi DAL - LayMaNVG: {ex.Message}", ex);
            }
        }

        public bool SetMaNV(int maDH, int? maNVG, int? maNVQ)
        {
            string query = @"
        UPDATE DonHang
        SET MaNVG = @MaNVG,
            MaNVQ = @MaNVQ
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
                        if (maNVG.HasValue && maNVG.Value != 0)
                        {
                            // Nếu có giá trị và giá trị khác 0, gán giá trị đó
                            cmd.Parameters.AddWithValue("@MaNVG", maNVG.Value);
                        }
                        else
                        {
                            // Nếu là NULL hoặc bằng 0, gán DBNull.Value
                            cmd.Parameters.AddWithValue("@MaNVG", DBNull.Value);
                        }
                        if (maNVQ.HasValue && maNVQ.Value != 0)
                        {
                            // Nếu có giá trị và giá trị khác 0, gán giá trị đó
                            cmd.Parameters.AddWithValue("@MaNVQ", maNVQ.Value);
                        }
                        else
                        {
                            // Nếu là NULL hoặc bằng 0, gán DBNull.Value
                            cmd.Parameters.AddWithValue("@MaNVQ", DBNull.Value);
                        }

                        int row = cmd.ExecuteNonQuery();
                        return row > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi DAL - SetMaNV: {ex.Message}", ex);
            }
        }
    }
}
