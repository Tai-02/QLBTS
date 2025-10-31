using MySql.Data.MySqlClient;
using QLBTS_DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace QLBTS_DAL
{
    public class QuanliSanPhamDAL
    {
        // 📦 Lấy tất cả sản phẩm
        public static List<SanPhamDTO> GetAll()
        {
            List<SanPhamDTO> list = new List<SanPhamDTO>();
            string query = "SELECT * FROM SanPham";

            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new SanPhamDTO
                    {
                        MaSP = reader.GetInt32("MaSP"),
                        TenSP = reader.GetString("TenSP"),
                        LoaiSP = reader.GetString("LoaiSP"),
                        SoLuong = reader.GetInt32("SoLuong"),
                        GiaM = reader.GetInt32("GiaM"),
                        GiaL = reader.GetInt32("GiaL"),
                        KhuyenMaiM = reader.GetInt32("KhuyenMaiM"),
                        KhuyenMaiL = reader.GetInt32("KhuyenMaiL"),
                        HinhAnh = reader["HinhAnh"] == DBNull.Value ? null : (byte[])reader["HinhAnh"],
                        TrangThai = reader["TrangThai"] == DBNull.Value ? null : reader.GetString("TrangThai")
                    });
                }
            }
            return list;
        }

        // ➕ Thêm sản phẩm
        public static bool Insert(SanPhamDTO sp)
        {
            string query = @"INSERT INTO SanPham 
                            (TenSP, LoaiSP, SoLuong, GiaM, GiaL, KhuyenMaiM, KhuyenMaiL, HinhAnh)
                             VALUES (@TenSP, @LoaiSP, @SoLuong, @GiaM, @GiaL, @KhuyenMaiM, @KhuyenMaiL, @HinhAnh)";
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@TenSP", sp.TenSP);
                cmd.Parameters.AddWithValue("@LoaiSP", sp.LoaiSP);
                cmd.Parameters.AddWithValue("@SoLuong", sp.SoLuong);
                cmd.Parameters.AddWithValue("@GiaM", sp.GiaM);
                cmd.Parameters.AddWithValue("@GiaL", sp.GiaL);
                cmd.Parameters.AddWithValue("@KhuyenMaiM", sp.KhuyenMaiM);
                cmd.Parameters.AddWithValue("@KhuyenMaiL", sp.KhuyenMaiL);
                cmd.Parameters.Add("@HinhAnh", MySqlDbType.Blob).Value = sp.HinhAnh == null ? DBNull.Value : (object)sp.HinhAnh;

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // ✏️ Cập nhật sản phẩm
        public static bool Update(SanPhamDTO sp)
        {
            string query = @"UPDATE SanPham 
                             SET TenSP=@TenSP, LoaiSP=@LoaiSP, SoLuong=@SoLuong, 
                                 GiaM=@GiaM, GiaL=@GiaL, KhuyenMaiM=@KhuyenMaiM, KhuyenMaiL=@KhuyenMaiL, HinhAnh=@HinhAnh
                             WHERE MaSP=@MaSP";

            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@MaSP", sp.MaSP);
                cmd.Parameters.AddWithValue("@TenSP", sp.TenSP);
                cmd.Parameters.AddWithValue("@LoaiSP", sp.LoaiSP);
                cmd.Parameters.AddWithValue("@SoLuong", sp.SoLuong);
                cmd.Parameters.AddWithValue("@GiaM", sp.GiaM);
                cmd.Parameters.AddWithValue("@GiaL", sp.GiaL);
                cmd.Parameters.AddWithValue("@KhuyenMaiM", sp.KhuyenMaiM);
                cmd.Parameters.AddWithValue("@KhuyenMaiL", sp.KhuyenMaiL);

                if (sp.HinhAnh == null || sp.HinhAnh.Length == 0)
                    cmd.Parameters.AddWithValue("@HinhAnh", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@HinhAnh", sp.HinhAnh);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // ❌ Xóa sản phẩm
        public static bool Delete(int maSP)
        {
            string query = "DELETE FROM SanPham WHERE MaSP=@MaSP";
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaSP", maSP);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // 📋 Lấy danh sách loại sản phẩm (phục vụ combobox, filter,...)
        public static List<string> GetDistinctLoaiSP()
        {
            List<string> list = new List<string>();
            string query = "SELECT DISTINCT LoaiSP FROM SanPham";
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(reader.GetString("LoaiSP"));
                }
            }
            return list;
        }
    }
}
