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
    }
}
