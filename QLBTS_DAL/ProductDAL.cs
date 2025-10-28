using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using QLBTS_DTO;

namespace QLBTS_DAL
{
    public class ProductDAL
    {
        private string connectionString = "Server=localhost;Database=QLBTS;Uid=root;Pwd=12345gray;CharSet=utf8mb4;";

        public ProductDetailViewModel GetProductDetail(int maSP)
        {
            ProductDetailViewModel product = null;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string query = @"
                        SELECT 
                            MaSP, TenSP, Size, SoLuong, Gia, 
                            KhuyenMai, HinhAnh, TrangThai
                        FROM SanPham
                        WHERE MaSP = @MaSP";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaSP", maSP);

                    conn.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        product = new ProductDetailViewModel
                        {
                            MaSP = reader.GetInt32("MaSP"),
                            TenSP = reader.GetString("TenSP"),
                            Size = reader.GetString("Size"),
                            SoLuong = reader.GetInt32("SoLuong"),
                            Gia = reader.GetInt32("Gia"),
                            KhuyenMai = reader.GetInt32("KhuyenMai"),
                            TrangThai = reader.GetString("TrangThai")
                        };

                        if (!reader.IsDBNull(reader.GetOrdinal("HinhAnh")))
                        {
                            product.HinhAnh = (byte[])reader["HinhAnh"];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi DAL - GetProductDetail: {ex.Message}", ex);
            }

            return product;
        }

        public List<ProductDetailViewModel> GetProductAllSizes(string tenSP)
        {
            List<ProductDetailViewModel> products = new List<ProductDetailViewModel>();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string query = @"
                        SELECT 
                            MaSP, TenSP, Size, SoLuong, Gia, 
                            KhuyenMai, HinhAnh, TrangThai
                        FROM SanPham
                        WHERE TenSP = @TenSP
                        ORDER BY Size";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@TenSP", tenSP);

                    conn.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var product = new ProductDetailViewModel
                        {
                            MaSP = reader.GetInt32("MaSP"),
                            TenSP = reader.GetString("TenSP"),
                            Size = reader.GetString("Size"),
                            SoLuong = reader.GetInt32("SoLuong"),
                            Gia = reader.GetInt32("Gia"),
                            KhuyenMai = reader.GetInt32("KhuyenMai"),
                            TrangThai = reader.GetString("TrangThai")
                        };

                        if (!reader.IsDBNull(reader.GetOrdinal("HinhAnh")))
                        {
                            product.HinhAnh = (byte[])reader["HinhAnh"];
                        }

                        products.Add(product);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi DAL - GetProductAllSizes: {ex.Message}", ex);
            }

            return products;
        }
        /// <summary>
        /// Kiểm tra số lượng tồn kho
        /// </summary>
        public bool CheckStock(int maSP, int soLuongCanMua)
        {
            try
            {
                int soLuongTonKho = GetCurrentStock(maSP);
                return soLuongTonKho >= soLuongCanMua;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi DAL - CheckStock: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Cập nhật số lượng sản phẩm (trừ khi đặt hàng, cộng khi hủy)
        /// </summary>
        public bool UpdateStock(int maSP, int soLuongThayDoi)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    // soLuongThayDoi: âm = trừ (đặt hàng), dương = cộng (hủy đơn)
                    string query = @"
                        UPDATE SanPham 
                        SET SoLuong = SoLuong + @SoLuongThayDoi 
                        WHERE MaSP = @MaSP";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaSP", maSP);
                    cmd.Parameters.AddWithValue("@SoLuongThayDoi", soLuongThayDoi);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi DAL - UpdateStock: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Lấy số lượng tồn kho hiện tại
        /// </summary>
        public int GetCurrentStock(int maSP)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
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
                throw new Exception($"Lỗi DAL - GetCurrentStock: {ex.Message}", ex);
            }
        }
    }
}
