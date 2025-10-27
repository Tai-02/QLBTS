using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using QLBTS_DTO;

namespace QLBTS_DAL
{
    public class OrderDAL
    {
        private string connectionString = "Server=localhost;Database=QLBTS;Uid=root;Pwd=12345gray;CharSet=utf8mb4;";

        /// <summary>
        /// Lấy chi tiết đơn hàng (JOIN 3 bảng)
        /// </summary>
        public List<OrderDetailViewModel> GetOrderDetails(int maDH)
        {
            List<OrderDetailViewModel> items = new List<OrderDetailViewModel>();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string query = @"
                        SELECT 
                            dh.MaDH,
                            dh.MaKhach,
                            dh.MaNVQuay,
                            dh.MaNVGiao,
                            dh.NgayDat,
                            dh.TongTien,
                            dh.TrangThai,
                            ct.MaCT,
                            ct.MaSP,
                            ct.SoLuong,
                            ct.DonGia,
                            sp.TenSP,
                            sp.Size,
                            sp.HinhAnh,
                            sp.KhuyenMai
                        FROM DonHang dh
                        INNER JOIN ChiTietDonHang ct ON dh.MaDH = ct.MaDH
                        INNER JOIN SanPham sp ON ct.MaSP = sp.MaSP
                        WHERE dh.MaDH = @MaDH
                        ORDER BY ct.MaCT";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaDH", maDH);

                    conn.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        OrderDetailViewModel item = new OrderDetailViewModel
                        {
                            MaDH = reader.GetInt32("MaDH"),
                            MaKhach = reader.IsDBNull(reader.GetOrdinal("MaKhach")) ? null : (int?)reader.GetInt32("MaKhach"),
                            MaNVQuay = reader.IsDBNull(reader.GetOrdinal("MaNVQuay")) ? null : (int?)reader.GetInt32("MaNVQuay"),
                            MaNVGiao = reader.IsDBNull(reader.GetOrdinal("MaNVGiao")) ? null : (int?)reader.GetInt32("MaNVGiao"),
                            NgayDat = reader.GetDateTime("NgayDat"),
                            TongTien = reader.GetInt32("TongTien"),
                            TrangThai = reader.GetString("TrangThai"),
                            MaCT = reader.GetInt32("MaCT"),
                            MaSP = reader.GetInt32("MaSP"),
                            SoLuong = reader.GetInt32("SoLuong"),
                            DonGia = reader.GetInt32("DonGia"),
                            TenSP = reader.GetString("TenSP"),
                            Size = reader.GetString("Size"),
                            KhuyenMai = reader.GetInt32("KhuyenMai")
                        };

                        // Đọc LONGBLOB
                        if (!reader.IsDBNull(reader.GetOrdinal("HinhAnh")))
                        {
                            item.HinhAnh = (byte[])reader["HinhAnh"];
                        }

                        items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi DAL - GetOrderDetails: {ex.Message}", ex);
            }

            return items;
        }
        /// <summary>
        /// Lấy danh sách tất cả đơn hàng
        /// </summary>
        public List<OrderListViewModel> GetAllOrders()
        {
            List<OrderListViewModel> orders = new List<OrderListViewModel>();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string query = @"
                    SELECT 
                    dh.MaDH,
                    dh.MaKhach,
                    dh.NgayDat,
                    COALESCE(SUM(ct.SoLuong * ct.DonGia), 0) as TongTien,
                    dh.TrangThai,
                    tk.HoTen as TenKhach
                    FROM DonHang dh
                    LEFT JOIN TaiKhoan tk ON dh.MaKhach = tk.MaTK
                    LEFT JOIN ChiTietDonHang ct ON dh.MaDH = ct.MaDH
                    GROUP BY dh.MaDH, dh.MaKhach, dh.NgayDat, dh.TrangThai, tk.HoTen
                    ORDER BY dh.NgayDat DESC";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    conn.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        orders.Add(new OrderListViewModel
                        {
                            MaDH = reader.GetInt32("MaDH"),
                            MaKhach = reader.IsDBNull(reader.GetOrdinal("MaKhach")) ? null : (int?)reader.GetInt32("MaKhach"),
                            NgayDat = reader.GetDateTime("NgayDat"),
                            TongTien = reader.GetInt32("TongTien"),
                            TrangThai = reader.GetString("TrangThai"),
                            TenKhach = reader.IsDBNull(reader.GetOrdinal("TenKhach")) ? "Khách vãng lai" : reader.GetString("TenKhach")
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi DAL - GetAllOrders: {ex.Message}", ex);
            }

            return orders;
        }

        /// <summary>
        /// Lấy đơn hàng theo trạng thái
        /// </summary>
        public List<OrderListViewModel> GetOrdersByStatus(string trangThai)
        {
            List<OrderListViewModel> orders = new List<OrderListViewModel>();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string query = @"
                    SELECT 
                    dh.MaDH,
                    dh.MaKhach,
                    dh.NgayDat,
                    COALESCE(SUM(ct.SoLuong * ct.DonGia), 0) as TongTien,
                    dh.TrangThai,
                    tk.HoTen as TenKhach
                    FROM DonHang dh
                    LEFT JOIN TaiKhoan tk ON dh.MaKhach = tk.MaTK
                    LEFT JOIN ChiTietDonHang ct ON dh.MaDH = ct.MaDH
                    WHERE dh.TrangThai = @TrangThai
                    GROUP BY dh.MaDH, dh.MaKhach, dh.NgayDat, dh.TrangThai, tk.HoTen
                    ORDER BY dh.NgayDat DESC";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@TrangThai", trangThai);

                    conn.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        orders.Add(new OrderListViewModel
                        {
                            MaDH = reader.GetInt32("MaDH"),
                            MaKhach = reader.IsDBNull(reader.GetOrdinal("MaKhach")) ? null : (int?)reader.GetInt32("MaKhach"),
                            NgayDat = reader.GetDateTime("NgayDat"),
                            TongTien = reader.GetInt32("TongTien"),
                            TrangThai = reader.GetString("TrangThai"),
                            TenKhach = reader.IsDBNull(reader.GetOrdinal("TenKhach")) ? "Khách vãng lai" : reader.GetString("TenKhach")
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi DAL - GetOrdersByStatus: {ex.Message}", ex);
            }

            return orders;
        }

        /// <summary>
        /// Lấy đơn hàng của khách hàng
        /// </summary>
        public List<OrderListViewModel> GetOrdersByCustomer(int maTK)
        {
            List<OrderListViewModel> orders = new List<OrderListViewModel>();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string query = @"
                    SELECT 
                    dh.MaDH,
                    dh.MaKhach,
                    dh.NgayDat,
                    COALESCE(SUM(ct.SoLuong * ct.DonGia), 0) as TongTien,
                    dh.TrangThai,
                    tk.HoTen as TenKhach
                    FROM DonHang dh
                    LEFT JOIN TaiKhoan tk ON dh.MaKhach = tk.MaTK
                    LEFT JOIN ChiTietDonHang ct ON dh.MaDH = ct.MaDH
                    WHERE dh.MaKhach = @MaTK
                    GROUP BY dh.MaDH, dh.MaKhach, dh.NgayDat, dh.TrangThai, tk.HoTen
                    ORDER BY dh.NgayDat DESC";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaTK", maTK);

                    conn.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        orders.Add(new OrderListViewModel
                        {
                            MaDH = reader.GetInt32("MaDH"),
                            MaKhach = reader.IsDBNull(reader.GetOrdinal("MaKhach")) ? null : (int?)reader.GetInt32("MaKhach"),
                            NgayDat = reader.GetDateTime("NgayDat"),
                            TongTien = reader.GetInt32("TongTien"),
                            TrangThai = reader.GetString("TrangThai"),
                            TenKhach = reader.IsDBNull(reader.GetOrdinal("TenKhach")) ? "Khách vãng lai" : reader.GetString("TenKhach")
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi DAL - GetOrdersByCustomer: {ex.Message}", ex);
            }

            return orders;
        }
        /// <summary>
        /// Cập nhật trạng thái đơn hàng
        /// </summary>
        public bool UpdateOrderStatus(int maDH, string trangThaiMoi)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string query = @"
                UPDATE DonHang 
                SET TrangThai = @TrangThai 
                WHERE MaDH = @MaDH";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaDH", maDH);
                    cmd.Parameters.AddWithValue("@TrangThai", trangThaiMoi);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi DAL - UpdateOrderStatus: {ex.Message}", ex);
            }
        }
    }
}
