using MySql.Data.MySqlClient;
using QLBTS_DTO;
using System;
using System.Collections.Generic;
using System.Data;

namespace QLBTS_DAL
{
    public class SanPhamDAL
    {

        public List<SanPhamDTO> GetSanPham(string filter)
        {
            List<SanPhamDTO> danhSach = new List<SanPhamDTO>();
            string query;

            if (filter == "bestseller")
            {
                query = "SELECT TenSP, Gia, HinhAnh FROM SanPham WHERE TenSP IN " +
                        "('Trà sữa trân châu', 'Trà sữa thái xanh', 'Trà sữa Matcha', 'Trà sữa bạc hà', 'Trà sữa xoài kem cheese')";
            }
            else if (filter == "monmoi")
            {
                query = "SELECT TenSP, Gia, HinhAnh FROM SanPham WHERE TenSP IN " +
                        "('Trà sữa kem trứng nướng', 'Trà sữa khoai môn', 'Trà sữa Oreo Cake Cream', 'Trà sữa matcha đậu đỏ', 'Trà sữa Pudding đậu đỏ')";
            }
            else if (filter == "tratraicay")
            {
                query = "SELECT TenSP, Gia, HinhAnh FROM SanPham WHERE TenSP IN " +
                        "('Trà đào cam sả', 'Nước dừa', 'Trà trái cây nhiệt đới')";
            }
            else if (filter == "topping")
            {
                query = "SELECT TenSP, Gia, HinhAnh FROM SanPham WHERE TenSP IN " +
                        "('Kem sữa', 'Trân châu đen', 'Trân châu trắng', 'Sương sáo')";
            }
            else
            {
                query = "SELECT TenSP, Gia, HinhAnh FROM SanPham WHERE Size = 'L'";
            }

            MySqlConnection conn = null;
            MySqlDataReader reader = null;

            try
            {
                conn = DatabaseHelper.GetConnection();
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    SanPhamDTO sp = new SanPhamDTO();
                    sp.TenSP = reader["TenSP"].ToString();
                    sp.Gia = Convert.ToInt32(reader["Gia"]);

                    // --- Sửa lỗi của bạn ở đây ---
                    // Lớp DAL chỉ lấy byte[], không chuyển đổi sang Image
                    if (reader["HinhAnh"] != DBNull.Value)
                    {
                        sp.HinhAnh = (byte[])reader["HinhAnh"];
                    }
                    else
                    {
                        sp.HinhAnh = null; // Gán là null nếu CSDL không có ảnh
                    }

                    danhSach.Add(sp);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi SanPhamDAL.GetSanPham: " + ex.Message);
            }
            finally
            {
                if (reader != null) reader.Close();
                if (conn != null && conn.State == ConnectionState.Open) conn.Close();
            }

            return danhSach;
        }


        // 2. Hàm đọc file từ đường dẫn và CẬP NHẬT vào CSDL
        public void UpdateProductImage(string tenSP, byte[] imageBytes)
        {
            // 1. Kiểm tra dữ liệu
            if (imageBytes == null || imageBytes.Length == 0)
            {
                Console.WriteLine($"Không có dữ liệu ảnh cho: {tenSP}.");
                return;
            }

            // 2. Tạo câu lệnh UPDATE với tham số (parameter)
            string query = "UPDATE SanPham SET HinhAnh = @HinhAnh WHERE TenSP = @TenSP";

            MySqlConnection conn = null;
            try
            {
                conn = DatabaseHelper.GetConnection();
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);

                // 3. Gán tham số (cách an toàn nhất)
                cmd.Parameters.Add("@TenSP", MySqlDbType.VarChar).Value = tenSP;
                cmd.Parameters.Add("@HinhAnh", MySqlDbType.LongBlob).Value = imageBytes;

                // 4. Thực thi
                cmd.ExecuteNonQuery();
                Console.WriteLine($"Cập nhật ảnh cho: {tenSP} thành công.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi cập nhật ảnh cho {tenSP}: {ex.Message}");
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open) conn.Close();
            }
        }
    }
}
