using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using QLBTS_DTO;

namespace QLBTS_DAL
{
    public class LichSuHoatDongDAL
    {
        public static List<LichSuHoatDongDTO> GetLichSuBanHang(int maNVQuay)
        {
            List<LichSuHoatDongDTO> list = new List<LichSuHoatDongDTO>();

            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();

                string query = @"
                    SELECT ls.MaLS, ls.MaTK, ls.MaDH, ls.TongTien, ls.TrangThai, ls.ThoiGian, ls.LoaiLichSu
                    FROM LichSuHoatDong ls
                    INNER JOIN TaiKhoan tk ON ls.MaTK = tk.MaTK
                    WHERE ls.MaTK = @MaNVQuay
                      AND tk.LevelID = 2
                    ORDER BY ls.ThoiGian DESC";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaNVQuay", maNVQuay);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new LichSuHoatDongDTO
                            {
                                MaLS = reader.GetInt32("MaLS"),
                                MaTK = reader.GetInt32("MaTK"),
                                MaDH = reader.IsDBNull(reader.GetOrdinal("MaDH")) ? 0 : reader.GetInt32("MaDH"),
                                TongTien = reader.IsDBNull(reader.GetOrdinal("TongTien")) ? 0 : reader.GetInt32("TongTien"),
                                TrangThai = reader.IsDBNull(reader.GetOrdinal("TrangThai")) ? "" : reader.GetString("TrangThai"),
                                ThoiGian = reader.IsDBNull(reader.GetOrdinal("ThoiGian")) ? DateTime.MinValue : reader.GetDateTime("ThoiGian"),
                                LoaiLichSu = reader.IsDBNull(reader.GetOrdinal("LoaiLichSu")) ? "BanHang" : reader.GetString("LoaiLichSu")
                            });
                        }
                    }
                }
            }

            if (list.Count == 0)
            {
                list.Add(new LichSuHoatDongDTO
                {
                    MaLS = 0,
                    MaTK = maNVQuay,
                    MaDH = 0,
                    TongTien = 0,
                    TrangThai = "Không có dữ liệu",
                    ThoiGian = DateTime.MinValue,
                    LoaiLichSu = "BanHang"
                });
            }

            return list;
        }
    }
}