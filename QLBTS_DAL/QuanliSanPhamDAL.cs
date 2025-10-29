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
                        Size = reader.GetString("Size"),
                        SoLuong = reader.GetInt32("SoLuong"),
                        Gia = reader.GetInt32("Gia"),
                        KhuyenMai = reader.GetInt32("KhuyenMai"),
                        HinhAnh = reader["HinhAnh"] == DBNull.Value ? null : (byte[])reader["HinhAnh"],
                        TrangThai = reader["TrangThai"] == DBNull.Value ? null : reader.GetString("TrangThai")
                    });
                }
            }
            return list;
        }

        public static bool Insert(SanPhamDTO sp)
        {
            string query = "INSERT INTO SanPham (TenSP, Size, SoLuong, Gia, KhuyenMai, HinhAnh) " +
                           "VALUES (@TenSP, @Size, @SoLuong, @Gia, @KhuyenMai, @HinhAnh)";
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@TenSP", sp.TenSP);
                cmd.Parameters.AddWithValue("@Size", sp.Size);
                cmd.Parameters.AddWithValue("@SoLuong", sp.SoLuong);
                cmd.Parameters.AddWithValue("@Gia", sp.Gia);
                cmd.Parameters.AddWithValue("@KhuyenMai", sp.KhuyenMai);
                cmd.Parameters.Add("@HinhAnh", MySqlDbType.Blob).Value = sp.HinhAnh == null ? DBNull.Value : (object)sp.HinhAnh;

                return cmd.ExecuteNonQuery() > 0;
            }
        }


        public static bool Update(SanPhamDTO sp)
        {
            string query = "UPDATE SanPham SET TenSP=@TenSP, Size=@Size, SoLuong=@SoLuong, Gia=@Gia, KhuyenMai=@KhuyenMai, HinhAnh=@HinhAnh WHERE MaSP=@MaSP";
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaSP", sp.MaSP);
                cmd.Parameters.AddWithValue("@TenSP", sp.TenSP);
                cmd.Parameters.AddWithValue("@Size", sp.Size);
                cmd.Parameters.AddWithValue("@SoLuong", sp.SoLuong);
                cmd.Parameters.AddWithValue("@Gia", sp.Gia);
                cmd.Parameters.AddWithValue("@KhuyenMai", sp.KhuyenMai);

                if (sp.HinhAnh == null || sp.HinhAnh.Length == 0)
                    cmd.Parameters.AddWithValue("@HinhAnh", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@HinhAnh", sp.HinhAnh);
                
                return cmd.ExecuteNonQuery() > 0;
            }
        }


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
    }
}
