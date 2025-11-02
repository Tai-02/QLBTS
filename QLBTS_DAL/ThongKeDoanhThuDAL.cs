using QLBTS_DTO;
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace QLBTS_DAL
{
    public class ThongKeDoanhThuDAL
    {
        public List<ThongKeDoanhThuDTO> GetAllDoanhThu()
        {
            List<ThongKeDoanhThuDTO> list = new List<ThongKeDoanhThuDTO>();

            string sql = @"
                SELECT Ngay, TongDoanhThu
                FROM ThongKeDoanhThu
                ORDER BY Ngay";

            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ThongKeDoanhThuDTO dto = new ThongKeDoanhThuDTO
                            {
                                Ngay = reader.GetDateTime("Ngay"),
                                TongDoanhThu = reader.GetInt32("TongDoanhThu")
                            };
                            list.Add(dto);
                        }
                    }
                }
            }

            return list;
        }
    }
}
