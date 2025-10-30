using QLBTS_DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace QLBTS_DAL
{
    public class ChiTietDonHangDAL
    {
        public List<SanPhamDTO> LayChiTietDonHangTheoMaDH(int maDH)
        {
            List<SanPhamDTO> listSP = new List<SanPhamDTO>();

            string query = @"
                SELECT 
                    sp.MaSP, 
                    sp.TenSP, 
                    sp.HinhAnh,
                    ctdh.SoLuong, 
                    ctdh.DonGia
                FROM ChiTietDonHang ctdh
                INNER JOIN SanPham sp ON ctdh.MaSP = sp.MaSP
                WHERE ctdh.MaDH = @MaDH;";

            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaDH", maDH);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                SanPhamDTO sp = new SanPhamDTO
                                {
                                    MaSP = reader.GetInt32("MaSP"),
                                    TenSP = reader.GetString("TenSP"),
                                    SoLuong = reader.GetInt32("SoLuong"),
                                    Gia = reader.GetInt32("DonGia"),
                                    HinhAnh = reader["HinhAnh"] != DBNull.Value
                                        ? (byte[])reader["HinhAnh"]
                                        : null
                                };
                                listSP.Add(sp);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi DAL - LayChiTietDonHangTheoMaDH: {ex.Message}", ex);
            }

            return listSP;
        }
    }
}
