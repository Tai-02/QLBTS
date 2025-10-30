using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using QLBTS_DTO;
using System;
using System.Collections.Generic;

namespace QLBTS_DAL
{
    public class LichSuHoatDongDAL
    {
        public static List<LichSuHoatDongDTO> GetLichSu(int maTK)
        {
            List<LichSuHoatDongDTO> list = new List<LichSuHoatDongDTO>();

            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();

                string query = @"
                    SELECT MaDH, TongTien, MoTa, ThoiGian
                    FROM LichSuHoatDong
                    WHERE MaTK = @MaTK
                    ORDER BY ThoiGian DESC";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaTK", maTK);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new LichSuHoatDongDTO
                            {
                                MaDH = reader.IsDBNull(reader.GetOrdinal("MaDH")) ? 0 : reader.GetInt32("MaDH"),
                                TongTien = reader.IsDBNull(reader.GetOrdinal("TongTien")) ? 0 : reader.GetInt32("TongTien"),
                                MoTa = reader.IsDBNull(reader.GetOrdinal("MoTa")) ? "" : reader.GetString("MoTa"),
                                ThoiGian = reader.IsDBNull(reader.GetOrdinal("ThoiGian")) ? DateTime.MinValue : reader.GetDateTime("ThoiGian")
                            });
                        }
                    }
                }
            }

            if (list.Count == 0)
            {
                list.Add(new LichSuHoatDongDTO
                {
                    MaDH = 0,
                    TongTien = 0,
                    MoTa = "Không có dữ liệu",
                    ThoiGian = DateTime.MinValue
                });
            }

            return list;
        }
    }
}
