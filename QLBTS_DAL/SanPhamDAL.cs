using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBTS_DAL
{
    public class SanPham
    {
        public int MaSP { get; set; }
        public string TenSP { get; set; }
        public string Size { get; set; }
        public int SoLuong { get; set; }
        public decimal Gia { get; set; }
        public decimal KhuyenMai { get; set; }
        public string HinhAnh { get; set; }
        public string TrangThai { get; set; }

    }
    public class SanPhamDAL
    {
        public static List<SanPham> GetAll()
        {
            List<SanPham> list = new List<SanPham>();
            string query = "SELECT * FROM SanPham";

            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new SanPham
                    {
                        MaSP = reader.GetInt32("MaSP"),
                        TenSP = reader.GetString("TenSP"),
                        Size = reader.GetString("Size"),
                        SoLuong = reader.GetInt32("SoLuong"),
                        Gia = reader.GetDecimal("Gia"),
                        KhuyenMai = reader.GetDecimal("KhuyenMai"),
                        HinhAnh = reader["HinhAnh"] == DBNull.Value ? null : reader.GetString("HinhAnh"),
                        TrangThai = reader.GetString("TrangThai")
                    });
                }
            }
            return list;
        }
        public static bool Insert(SanPham sp)
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
                cmd.Parameters.AddWithValue("@HinhAnh", DBNull.Value);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public static bool Update(SanPham sp)
        {
            string query = "UPDATE SanPham SET TenSP=@TenSP, Size=@Size, SoLuong=@SoLuong, Gia=@Gia, KhuyenMai=@KhuyenMai WHERE MaSP=@MaSP";
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
