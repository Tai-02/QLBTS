using MySql.Data.MySqlClient;
using QLBTS_DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBTS_DAL
{
    public class CartDAL
    {
        private string connectionString = "Server=localhost;Database=QLBTS;Uid=root;Pwd=12345gray;CharSet=utf8mb4;";

        /// <summary>
        /// Lấy giỏ hàng theo MaTK (gộp chung Customer + Employee)
        /// </summary>
        public List<CartItemViewModel> GetCart(int maTK)
        {
            List<CartItemViewModel> items = new List<CartItemViewModel>();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string query = @"
                        SELECT 
                            ctgh.MaCTGH,
                            ctgh.MaGH,
                            ctgh.MaSP,
                            ctgh.SoLuong,
                            sp.TenSP,
                            sp.Size,
                            sp.Gia,
                            sp.KhuyenMai,
                            sp.HinhAnh,
                            sp.TrangThai,
                            gh.MaTK,
                            gh.NgayTao
                        FROM ChiTietGioHang ctgh
                        INNER JOIN GioHang gh ON ctgh.MaGH = gh.MaGH
                        INNER JOIN SanPham sp ON ctgh.MaSP = sp.MaSP
                        WHERE gh.MaTK = @MaTK
                        ORDER BY gh.NgayTao DESC";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaTK", maTK);

                    conn.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        CartItemViewModel item = new CartItemViewModel
                        {
                            MaCTGH = reader.GetInt32("MaCTGH"),
                            MaGH = reader.GetInt32("MaGH"),
                            MaSP = reader.GetInt32("MaSP"),
                            SoLuong = reader.GetInt32("SoLuong"),
                            TenSP = reader.GetString("TenSP"),
                            Size = reader.GetString("Size"),
                            Gia = reader.GetInt32("Gia"),
                            KhuyenMai = reader.GetInt32("KhuyenMai"),
                            TrangThai = reader.GetString("TrangThai"),
                            MaTK = reader.GetInt32("MaTK"),
                            NgayTao = reader.IsDBNull(reader.GetOrdinal("NgayTao")) ? null : (DateTime?)reader.GetDateTime("NgayTao")
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
                throw new Exception($"Lỗi DAL - GetCart: {ex.Message}", ex);
            }

            return items;
        }

        /// <summary>
        /// Cập nhật số lượng sản phẩm trong giỏ
        /// </summary>
        public bool UpdateQuantity(int maCTGH, int soLuong)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string query = "UPDATE ChiTietGioHang SET SoLuong = @SoLuong WHERE MaCTGH = @MaCTGH";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@SoLuong", soLuong);
                    cmd.Parameters.AddWithValue("@MaCTGH", maCTGH);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi cập nhật số lượng: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Xóa sản phẩm khỏi giỏ hàng
        /// </summary>
        public bool DeleteCartItem(int maCTGH)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string query = "DELETE FROM ChiTietGioHang WHERE MaCTGH = @MaCTGH";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaCTGH", maCTGH);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi xóa sản phẩm: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Lấy hoặc tạo giỏ hàng cho user
        /// </summary>
        private int GetOrCreateCart(int maTK, MySqlConnection conn)
        {
            // Kiểm tra giỏ hàng đã tồn tại chưa
            string checkQuery = "SELECT MaGH FROM GioHang WHERE MaTK = @MaTK";
            MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn);
            checkCmd.Parameters.AddWithValue("@MaTK", maTK);

            object result = checkCmd.ExecuteScalar();

            if (result != null)
            {
                return Convert.ToInt32(result);
            }

            // Tạo mới giỏ hàng
            string insertQuery = "INSERT INTO GioHang (MaTK) VALUES (@MaTK); SELECT LAST_INSERT_ID();";
            MySqlCommand insertCmd = new MySqlCommand(insertQuery, conn);
            insertCmd.Parameters.AddWithValue("@MaTK", maTK);

            return Convert.ToInt32(insertCmd.ExecuteScalar());
        }

        /// <summary>
        /// Thêm sản phẩm vào giỏ hàng
        /// </summary>
        public bool AddToCart(int maTK, int maSP, int soLuong)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Lấy hoặc tạo giỏ hàng
                    int maGH = GetOrCreateCart(maTK, conn);
                    // Kiểm tra sản phẩm đã có trong giỏ chưa
                    string checkQuery = @"
                        SELECT MaCTGH, SoLuong 
                        FROM ChiTietGioHang 
                        WHERE MaGH = @MaGH AND MaSP = @MaSP";

                    MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn);
                    checkCmd.Parameters.AddWithValue("@MaGH", maGH);
                    checkCmd.Parameters.AddWithValue("@MaSP", maSP);

                    MySqlDataReader reader = checkCmd.ExecuteReader();

                    if (reader.Read())
                    {
                        // Đã có → Cập nhật số lượng
                        int existingMaCTGH = reader.GetInt32("MaCTGH");
                        int existingSoLuong = reader.GetInt32("SoLuong");
                        reader.Close();

                        string updateQuery = "UPDATE ChiTietGioHang SET SoLuong = @SoLuong WHERE MaCTGH = @MaCTGH";
                        MySqlCommand updateCmd = new MySqlCommand(updateQuery, conn);
                        updateCmd.Parameters.AddWithValue("@SoLuong", existingSoLuong + soLuong);
                        updateCmd.Parameters.AddWithValue("@MaCTGH", existingMaCTGH);

                        return updateCmd.ExecuteNonQuery() > 0;
                    }
                    else
                    {
                        // Chưa có → Thêm mới
                        reader.Close();

                        string insertQuery = @"
                            INSERT INTO ChiTietGioHang (MaGH, MaSP, SoLuong) 
                            VALUES (@MaGH, @MaSP, @SoLuong)";

                        MySqlCommand insertCmd = new MySqlCommand(insertQuery, conn);
                        insertCmd.Parameters.AddWithValue("@MaGH", maGH);
                        insertCmd.Parameters.AddWithValue("@MaSP", maSP);
                        insertCmd.Parameters.AddWithValue("@SoLuong", soLuong);

                        return insertCmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi thêm vào giỏ hàng: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Xóa toàn bộ giỏ hàng (sau khi thanh toán)
        /// </summary>
        public bool ClearCart(int maTK)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Lấy MaGH
                    string getMaGHQuery = "SELECT MaGH FROM GioHang WHERE MaTK = @MaTK";
                    MySqlCommand getMaGHCmd = new MySqlCommand(getMaGHQuery, conn);
                    getMaGHCmd.Parameters.AddWithValue("@MaTK", maTK);

                    object result = getMaGHCmd.ExecuteScalar();

                    if (result == null)
                        return false;

                    int maGH = Convert.ToInt32(result);

                    // Xóa ChiTietGioHang
                    string deleteQuery = "DELETE FROM ChiTietGioHang WHERE MaGH = @MaGH";
                    MySqlCommand deleteCmd = new MySqlCommand(deleteQuery, conn);
                    deleteCmd.Parameters.AddWithValue("@MaGH", maGH);
                    deleteCmd.ExecuteNonQuery();

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi xóa giỏ hàng: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Tạo đơn hàng từ giỏ hàng
        /// </summary>
        public int CreateOrderFromCart(int maTK, out int tongTien)
        {
            tongTien = 0;
            int maDH = 0;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    MySqlTransaction transaction = conn.BeginTransaction();

                    try
                    {
                        // 1. Lấy giỏ hàng của user
                        string getCartQuery = @"
                    SELECT 
                        ctgh.MaSP,
                        ctgh.SoLuong,
                        sp.Gia,
                        sp.KhuyenMai
                    FROM ChiTietGioHang ctgh
                    INNER JOIN GioHang gh ON ctgh.MaGH = gh.MaGH
                    INNER JOIN SanPham sp ON ctgh.MaSP = sp.MaSP
                    WHERE gh.MaTK = @MaTK";

                        MySqlCommand getCartCmd = new MySqlCommand(getCartQuery, conn, transaction);
                        getCartCmd.Parameters.AddWithValue("@MaTK", maTK);

                        MySqlDataReader reader = getCartCmd.ExecuteReader();

                        var cartItems = new System.Collections.Generic.List<(int MaSP, int SoLuong, int DonGia)>();

                        while (reader.Read())
                        {
                            int maSP = reader.GetInt32("MaSP");
                            int soLuong = reader.GetInt32("SoLuong");
                            int giaGoc = reader.GetInt32("Gia");
                            int khuyenMai = reader.GetInt32("KhuyenMai");

                            // Tính giá sau khuyến mãi
                            int donGia = giaGoc - (giaGoc * khuyenMai / 100);
                            cartItems.Add((maSP, soLuong, donGia));

                            tongTien += donGia * soLuong;
                        }
                        reader.Close();

                        if (cartItems.Count == 0)
                        {
                            transaction.Rollback();
                            throw new Exception("Giỏ hàng trống!");
                        }

                        // 2. Tạo đơn hàng
                        string createOrderQuery = @"
                    INSERT INTO DonHang (MaKhach, NgayDat, TongTien, TrangThai)
                    VALUES (@MaKhach, NOW(), @TongTien, 'Chờ xác nhận');
                    SELECT LAST_INSERT_ID();";

                        MySqlCommand createOrderCmd = new MySqlCommand(createOrderQuery, conn, transaction);
                        createOrderCmd.Parameters.AddWithValue("@MaKhach", maTK);
                        createOrderCmd.Parameters.AddWithValue("@TongTien", tongTien);

                        maDH = Convert.ToInt32(createOrderCmd.ExecuteScalar());

                        // 3. Thêm chi tiết đơn hàng & Trừ số lượng sản phẩm
                        ProductDAL productDAL = new ProductDAL();

                        foreach (var item in cartItems)
                        {
                            // Kiểm tra tồn kho
                            int tonKho = productDAL.GetCurrentStock(item.MaSP);
                            if (tonKho < item.SoLuong)
                            {
                                transaction.Rollback();
                                throw new Exception($"Sản phẩm MaSP={item.MaSP} không đủ số lượng!");
                            }

                            // Thêm chi tiết đơn hàng
                            string insertDetailQuery = @"
                        INSERT INTO ChiTietDonHang (MaDH, MaSP, SoLuong, DonGia)
                        VALUES (@MaDH, @MaSP, @SoLuong, @DonGia)";

                            MySqlCommand insertDetailCmd = new MySqlCommand(insertDetailQuery, conn, transaction);
                            insertDetailCmd.Parameters.AddWithValue("@MaDH", maDH);
                            insertDetailCmd.Parameters.AddWithValue("@MaSP", item.MaSP);
                            insertDetailCmd.Parameters.AddWithValue("@SoLuong", item.SoLuong);
                            insertDetailCmd.Parameters.AddWithValue("@DonGia", item.DonGia);
                            insertDetailCmd.ExecuteNonQuery();

                            // Trừ số lượng sản phẩm
                            string updateStockQuery = @"
                        UPDATE SanPham 
                        SET SoLuong = SoLuong - @SoLuong 
                        WHERE MaSP = @MaSP";

                            MySqlCommand updateStockCmd = new MySqlCommand(updateStockQuery, conn, transaction);
                            updateStockCmd.Parameters.AddWithValue("@SoLuong", item.SoLuong);
                            updateStockCmd.Parameters.AddWithValue("@MaSP", item.MaSP);
                            updateStockCmd.ExecuteNonQuery();
                        }

                        // 4. Xóa giỏ hàng
                        string clearCartQuery = @"
                    DELETE ctgh FROM ChiTietGioHang ctgh
                    INNER JOIN GioHang gh ON ctgh.MaGH = gh.MaGH
                    WHERE gh.MaTK = @MaTK";

                        MySqlCommand clearCartCmd = new MySqlCommand(clearCartQuery, conn, transaction);
                        clearCartCmd.Parameters.AddWithValue("@MaTK", maTK);
                        clearCartCmd.ExecuteNonQuery();

                        // 5. Commit transaction
                        transaction.Commit();
                        OrderDAL orderDAL = new OrderDAL();
                        orderDAL.ThemLichSu(maTK, maDH, tongTien, "Chờ xác nhận", "MuaHang");

                        return maDH;
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
                throw new Exception($"Lỗi DAL - CreateOrderFromCart: {ex.Message}", ex);
            }
        }
    }
}
