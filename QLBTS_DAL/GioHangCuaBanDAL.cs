using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using QLBTS_DTO;

namespace QLBTS_DAL
{
    public class GioHangCuaBanDAL
    {
        public static List<SanPhamDTO> LayGioHangTheoMaKH(int maKH)
        {
            List<SanPhamDTO> danhSach = new List<SanPhamDTO>();
            string query = @"
                SELECT sp.MaSP, sp.TenSP, sp.Size, sp.Gia, sp.HinhAnh, ctgh.SoLuong
                FROM ChiTietGioHang ctgh
                JOIN SanPham sp ON ctgh.MaSP = sp.MaSP
                JOIN GioHang gh ON ctgh.MaGH = gh.MaGH
                WHERE gh.MaKH = @MaKH;
            ";

            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaKH", maKH);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            danhSach.Add(new SanPhamDTO
                            {
                                MaSP = reader.GetInt32("MaSP"),
                                TenSP = reader.GetString("TenSP"),
                                Size = reader.GetString("Size"),
                                Gia = reader.GetInt32("Gia"),
                                SoLuong = reader.GetInt32("SoLuong"),
                                HinhAnh = reader["HinhAnh"] == DBNull.Value ? null : (byte[])reader["HinhAnh"]
                            });
                        }
                    }
                }
            }
            return danhSach;
        }
    }
}
