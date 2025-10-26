using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace QLBTS_DAL
{
    public static class QLDonHangFormDAL
    {
        // Lấy danh sách đơn hàng (cơ bản)
        public static DataTable LayDanhSachDonHang()
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                string query = @"
                    SELECT 
                        MaDH,
                        TongTien AS Gia,
                        TrangThai
                    FROM DonHang
                    ORDER BY NgayDat DESC;";

                MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        // Lấy chi tiết đơn hàng (tên khách, ngày đặt, sản phẩm)
        public static DataTable LayChiTietDonHang(int maDH)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                string query = @"
                    SELECT 
                        kh.HoTen AS TenKhachHang,
                        dh.NgayDat,
                        sp.TenSP,
                        ctdh.SoLuong,
                        ctdh.DonGia
                    FROM DonHang dh
                    LEFT JOIN KhachHang kh ON dh.MaKH = kh.MaKH
                    INNER JOIN ChiTietDonHang ctdh ON dh.MaDH = ctdh.MaDH
                    INNER JOIN SanPham sp ON ctdh.MaSP = sp.MaSP
                    WHERE dh.MaDH = @MaDH;";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaDH", maDH);

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        // Cập nhật trạng thái đơn hàng -> trả về true nếu thành công
        public static bool CapNhatTrangThai(int maDH, string trangThai)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    string query = "UPDATE DonHang SET TrangThai = @TrangThai WHERE MaDH = @MaDH;";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@TrangThai", trangThai);
                    cmd.Parameters.AddWithValue("@MaDH", maDH);

                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows > 0;
                }
                catch (Exception ex)
                {
                    // Log hoặc xử lý theo nhu cầu (ở đây in ra console để debug)
                    Console.WriteLine("Lỗi khi cập nhật trạng thái: " + ex.Message);
                    return false;
                }
            }
        }
    }
}
