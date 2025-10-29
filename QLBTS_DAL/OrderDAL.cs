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
                    dh.TongTien,
                    dh.TrangThai,
                    tk.HoTen as TenKhach
                FROM DonHang dh
                LEFT JOIN TaiKhoan tk ON dh.MaKhach = tk.MaTK
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
                    dh.TongTien,
                    dh.TrangThai,
                    tk.HoTen as TenKhach
                FROM DonHang dh
                LEFT JOIN TaiKhoan tk ON dh.MaKhach = tk.MaTK
                WHERE dh.MaKhach = @MaTK
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
                    conn.Open();

                    // 1. Lấy thông tin đơn hàng
                    string querySelect = "SELECT MaKhach, TongTien FROM DonHang WHERE MaDH = @MaDH";
                    MySqlCommand cmdSelect = new MySqlCommand(querySelect, conn);
                    cmdSelect.Parameters.AddWithValue("@MaDH", maDH);

                    MySqlDataReader reader = cmdSelect.ExecuteReader();

                    int maKhach = 0;
                    int tongTien = 0;

                    if (reader.Read())
                    {
                        maKhach = reader.GetInt32("MaKhach");
                        tongTien = reader.GetInt32("TongTien");
                    }
                    reader.Close();

                    // 2. Cập nhật trạng thái
                    string query = @"
                UPDATE DonHang 
                SET TrangThai = @TrangThai 
                WHERE MaDH = @MaDH";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaDH", maDH);
                    cmd.Parameters.AddWithValue("@TrangThai", trangThaiMoi);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    // ✅ 3. GHI LỊCH SỬ (nếu không dùng trigger)
                    if (rowsAffected > 0 && maKhach > 0)
                    {
                        ThemLichSu(maKhach, maDH, tongTien, trangThaiMoi, "MuaHang");
                    }

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi DAL - UpdateOrderStatus: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Hủy đơn hàng (khách hàng)
        /// </summary>
        public bool CancelOrder(int maDH, int maTK)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    MySqlTransaction transaction = conn.BeginTransaction();

                    try
                    {
                        // 1. Kiểm tra đơn hàng có thuộc về khách này không
                        string checkQuery = @"
                    SELECT TrangThai 
                    FROM DonHang 
                    WHERE MaDH = @MaDH AND MaKhach = @MaTK";

                        MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn, transaction);
                        checkCmd.Parameters.AddWithValue("@MaDH", maDH);
                        checkCmd.Parameters.AddWithValue("@MaTK", maTK);

                        object result = checkCmd.ExecuteScalar();

                        if (result == null)
                        {
                            transaction.Rollback();
                            throw new Exception("Không tìm thấy đơn hàng!");
                        }

                        string trangThai = result.ToString();

                        // 2. Chỉ cho phép hủy đơn "Chờ xác nhận"
                        if (trangThai != "Chờ xác nhận")
                        {
                            transaction.Rollback();
                            throw new Exception($"Không thể hủy đơn hàng ở trạng thái '{trangThai}'!");
                        }

                        // 3. Lấy tổng tiền (RIÊNG 1 QUERY)
                        string getTongTienQuery = "SELECT TongTien FROM DonHang WHERE MaDH = @MaDH";
                        MySqlCommand getTongTienCmd = new MySqlCommand(getTongTienQuery, conn, transaction);
                        getTongTienCmd.Parameters.AddWithValue("@MaDH", maDH);
                        int tongTienDonHang = Convert.ToInt32(getTongTienCmd.ExecuteScalar());

                        // 4. Lấy chi tiết đơn hàng để cộng lại số lượng
                        string getDetailsQuery = @"
                    SELECT MaSP, SoLuong 
                    FROM ChiTietDonHang 
                    WHERE MaDH = @MaDH";

                        MySqlCommand getDetailsCmd = new MySqlCommand(getDetailsQuery, conn, transaction);
                        getDetailsCmd.Parameters.AddWithValue("@MaDH", maDH);

                        MySqlDataReader reader = getDetailsCmd.ExecuteReader();

                        var orderDetails = new System.Collections.Generic.List<(int MaSP, int SoLuong)>();

                        while (reader.Read())
                        {
                            int maSP = reader.GetInt32("MaSP");
                            int soLuong = reader.GetInt32("SoLuong");
                            orderDetails.Add((maSP, soLuong));
                        }
                        reader.Close();

                        // 5. Cộng lại số lượng sản phẩm
                        foreach (var detail in orderDetails)
                        {
                            string updateStockQuery = @"
                        UPDATE SanPham 
                        SET SoLuong = SoLuong + @SoLuong 
                        WHERE MaSP = @MaSP";

                            MySqlCommand updateStockCmd = new MySqlCommand(updateStockQuery, conn, transaction);
                            updateStockCmd.Parameters.AddWithValue("@SoLuong", detail.SoLuong);
                            updateStockCmd.Parameters.AddWithValue("@MaSP", detail.MaSP);
                            updateStockCmd.ExecuteNonQuery();
                        }

                        // 6. Cập nhật trạng thái đơn hàng
                        string cancelQuery = @"
                    UPDATE DonHang 
                    SET TrangThai = 'Đã hủy' 
                    WHERE MaDH = @MaDH";

                        MySqlCommand cancelCmd = new MySqlCommand(cancelQuery, conn, transaction);
                        cancelCmd.Parameters.AddWithValue("@MaDH", maDH);
                        cancelCmd.ExecuteNonQuery();

                        // 7. Commit transaction
                        transaction.Commit();

                        // 8. Ghi lịch sử (SAU commit, connection tự động mở lại trong ThemLichSu)
                        ThemLichSu(maTK, maDH, tongTienDonHang, "Đã hủy", "MuaHang");

                        return true;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi DAL - CancelOrder: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Lấy lịch sử hoạt động của khách hàng
        /// </summary>
        public List<LichSuViewModel> GetLichSuByCustomer(int maTK)
        {
            List<LichSuViewModel> lichSu = new List<LichSuViewModel>();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string query = @"
                SELECT 
                    MaLS,
                    MaTK,
                    MaDH,
                    TongTien,
                    TrangThai,
                    ThoiGian,
                    LoaiLichSu
                FROM LichSuHoatDong
                WHERE MaTK = @MaTK
                ORDER BY ThoiGian DESC";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaTK", maTK);

                    conn.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        lichSu.Add(new LichSuViewModel
                        {
                            MaLS = reader.GetInt32("MaLS"),
                            MaTK = reader.IsDBNull(reader.GetOrdinal("MaTK")) ? null : (int?)reader.GetInt32("MaTK"),
                            MaDH = reader.IsDBNull(reader.GetOrdinal("MaDH")) ? null : (int?)reader.GetInt32("MaDH"),
                            TongTien = reader.IsDBNull(reader.GetOrdinal("TongTien")) ? null : (int?)reader.GetInt32("TongTien"),
                            TrangThai = reader.IsDBNull(reader.GetOrdinal("TrangThai")) ? "" : reader.GetString("TrangThai"),
                            ThoiGian = reader.GetDateTime("ThoiGian"),
                            LoaiLichSu = reader.GetString("LoaiLichSu")
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi DAL - GetLichSuByCustomer: {ex.Message}", ex);
            }

            return lichSu;
        }
        /// <summary>
        /// Thêm lịch sử hoạt động
        /// </summary>
        public void ThemLichSu(int maTK, int maDH, int tongTien, string trangThai, string loaiLichSu)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string query = @"
                INSERT INTO LichSuHoatDong (MaTK, MaDH, TongTien, TrangThai, ThoiGian, LoaiLichSu)
                VALUES (@MaTK, @MaDH, @TongTien, @TrangThai, NOW(), @LoaiLichSu)";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaTK", maTK);
                    cmd.Parameters.AddWithValue("@MaDH", maDH);
                    cmd.Parameters.AddWithValue("@TongTien", tongTien);
                    cmd.Parameters.AddWithValue("@TrangThai", trangThai);
                    cmd.Parameters.AddWithValue("@LoaiLichSu", loaiLichSu);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi DAL - ThemLichSu: {ex.Message}", ex);
            }
        }
    }
}
