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

        public static bool ThemLichSu(int maTK, int maDH, string moTa, int tongTien, string loaiLichSu)
        {
            // Thời gian hoạt động là thời gian hiện tại
            DateTime thoiGian = DateTime.Now;

            string query = @"
            INSERT INTO LichSuHoatDong (MaTK, MaDH, TongTien, MoTa, ThoiGian, LoaiLichSu)
            VALUES (@MaTK, @MaDH, @TongTien, @MoTa, @ThoiGian, @LoaiLichSu)";

            try
            {
                using (MySqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        // Thêm tham số
                        cmd.Parameters.AddWithValue("@MaTK", maTK);
                        cmd.Parameters.AddWithValue("@MaDH", maDH);
                        cmd.Parameters.AddWithValue("@TongTien", tongTien);
                        cmd.Parameters.AddWithValue("@MoTa", moTa);
                        cmd.Parameters.AddWithValue("@ThoiGian", thoiGian);

                        // ✅ THAM SỐ MỚI
                        cmd.Parameters.AddWithValue("@LoaiLichSu", loaiLichSu);

                        // Thực thi lệnh
                        int rowsAffected = cmd.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi DAL - ThemLichSu: " + ex.Message, ex);
            }
        }
    }
    
}
