using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using QLBTS_DTO;

namespace QLBTS_DAL
{
    public class LichSuNhanDonDAL
    {
        public static List<LichSuGiaoHangDTO> LayLichSuTheoMaNV(int maNVGiao, string trangThai = "")
        {
            List<LichSuGiaoHangDTO> list = new List<LichSuGiaoHangDTO>();

            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();

                string query = @"
                    SELECT MaLSGH, MaNVGiao, MaDH, TongTien, TrangThai, ThoiGian
                    FROM LichSuGiaoHang
                    WHERE MaNVGiao = @MaNVGiao";

                if (!string.IsNullOrEmpty(trangThai))
                {
                    query += " AND TrangThai = @TrangThai";
                }

                query += " ORDER BY ThoiGian DESC";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaNVGiao", maNVGiao);
                    if (!string.IsNullOrEmpty(trangThai))
                        cmd.Parameters.AddWithValue("@TrangThai", trangThai);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new LichSuGiaoHangDTO
                            {
                                MaLSGH = reader.GetInt32("MaLSGH"),
                                MaNVGiao = reader.GetInt32("MaNVGiao"),
                                MaDH = reader.GetInt32("MaDH"),
                                TongTien = reader.GetInt32("TongTien"),
                                TrangThai = reader.GetString("TrangThai"),
                                ThoiGian = reader.GetDateTime("ThoiGian")
                            });
                        }
                    }
                }
            }

            return list;
        }
    }
}
