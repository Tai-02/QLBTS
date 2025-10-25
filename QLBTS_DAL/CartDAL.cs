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
        /// Lấy giỏ hàng của Khách hàng
        /// </summary>
        public List<CartItemViewModel> GetCartByCustomer(int maKH)
        {
            return GetCart(maKH, null);
        }

        /// <summary>
        /// Lấy giỏ hàng của Nhân viên
        /// </summary>
        public List<CartItemViewModel> GetCartByEmployee(int maNV)
        {
            return GetCart(null, maNV);
        }

        /// <summary>
        /// Lấy giỏ hàng (JOIN 3 bảng)
        /// </summary>
        private List<CartItemViewModel> GetCart(int? maKH, int? maNV)
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
                            gh.MaKH,
                            gh.MaNV,
                            gh.NgayTao
                        FROM ChiTietGioHang ctgh
                        INNER JOIN GioHang gh ON ctgh.MaGH = gh.MaGH
                        INNER JOIN SanPham sp ON ctgh.MaSP = sp.MaSP
                        WHERE ";

                    if (maKH.HasValue)
                        query += "gh.MaKH = @MaKH";
                    else if (maNV.HasValue)
                        query += "gh.MaNV = @MaNV";
                    else
                        throw new ArgumentException("Phải cung cấp MaKH hoặc MaNV");

                    query += " ORDER BY gh.NgayTao DESC";

                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    if (maKH.HasValue)
                        cmd.Parameters.AddWithValue("@MaKH", maKH.Value);
                    if (maNV.HasValue)
                        cmd.Parameters.AddWithValue("@MaNV", maNV.Value);

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
                            MaKH = reader.IsDBNull(reader.GetOrdinal("MaKH")) ? null : (int?)reader.GetInt32("MaKH"),
                            MaNV = reader.IsDBNull(reader.GetOrdinal("MaNV")) ? null : (int?)reader.GetInt32("MaNV"),
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
        private int GetOrCreateCart(int? maKH, int? maNV, MySqlConnection conn)
        {
            // Kiểm tra giỏ hàng đã tồn tại chưa
            string checkQuery = "SELECT MaGH FROM GioHang WHERE ";

            if (maKH.HasValue)
                checkQuery += "MaKH = @MaKH";
            else if (maNV.HasValue)
                checkQuery += "MaNV = @MaNV";

            MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn);

            if (maKH.HasValue)
                checkCmd.Parameters.AddWithValue("@MaKH", maKH.Value);
            if (maNV.HasValue)
                checkCmd.Parameters.AddWithValue("@MaNV", maNV.Value);

            object result = checkCmd.ExecuteScalar();

            if (result != null)
            {
                return Convert.ToInt32(result);
            }

            // Tạo mới giỏ hàng
            string insertQuery = "INSERT INTO GioHang (MaKH, MaNV) VALUES (@MaKH, @MaNV); SELECT LAST_INSERT_ID();";
            MySqlCommand insertCmd = new MySqlCommand(insertQuery, conn);
            insertCmd.Parameters.AddWithValue("@MaKH", maKH.HasValue ? (object)maKH.Value : DBNull.Value);
            insertCmd.Parameters.AddWithValue("@MaNV", maNV.HasValue ? (object)maNV.Value : DBNull.Value);

            return Convert.ToInt32(insertCmd.ExecuteScalar());
        }

        /// <summary>
        /// Thêm sản phẩm vào giỏ hàng
        /// </summary>
        public bool AddToCart(int? maKH, int? maNV, int maSP, int soLuong)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Lấy hoặc tạo giỏ hàng
                    int maGH = GetOrCreateCart(maKH, maNV, conn);

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
        public bool ClearCart(int? maKH, int? maNV)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Lấy MaGH
                    string getMaGHQuery = "SELECT MaGH FROM GioHang WHERE ";

                    if (maKH.HasValue)
                        getMaGHQuery += "MaKH = @MaKH";
                    else if (maNV.HasValue)
                        getMaGHQuery += "MaNV = @MaNV";

                    MySqlCommand getMaGHCmd = new MySqlCommand(getMaGHQuery, conn);

                    if (maKH.HasValue)
                        getMaGHCmd.Parameters.AddWithValue("@MaKH", maKH.Value);
                    if (maNV.HasValue)
                        getMaGHCmd.Parameters.AddWithValue("@MaNV", maNV.Value);

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
    }
}
