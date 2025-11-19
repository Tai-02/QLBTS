using Microsoft.EntityFrameworkCore;
using QLBTS_DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace QLBTS_DAL
{
    public class ThongKeNhanVienDAL
    {
        public List<ThongKeNhanVienDTO> GetHieuSuatNhanVien(double mucTieu, DateTime ngay, string kieuHieuSuat)
        {
            List<ThongKeNhanVienDTO> list = new List<ThongKeNhanVienDTO>();

            string whereClause = "";
            string groupBy = "";
            string selectNgayLam = "";

            if (kieuHieuSuat == "Ngày")
            {
                // Chọn ngày cụ thể
                whereClause = "tknv.Ngay = @Ngay";
                groupBy = "tk.MaTK, tk.HoTen";
                selectNgayLam = "COUNT(DISTINCT DATE(tknv.Ngay)) AS SoNgayLam";
            }
            else if (kieuHieuSuat == "Tháng")
            {
                // Chọn tháng cụ thể
                whereClause = "YEAR(tknv.Ngay) = @Nam AND MONTH(tknv.Ngay) = @Thang";
                groupBy = "tk.MaTK, tk.HoTen";
                selectNgayLam = "COUNT(DISTINCT DATE(tknv.Ngay)) AS SoNgayLam";
            }

            string query = $@"
                SELECT 
                    tk.MaTK,
                    tk.HoTen,
                    {selectNgayLam},
                    SUM(tknv.DoanhThu) AS TongDoanhThu,
                    (SUM(tknv.DoanhThu) / {mucTieu}) * 100 AS HieuSuat
                FROM ThongKeNhanVien tknv
                JOIN TaiKhoan tk ON tknv.MaNV = tk.MaTK
                WHERE {whereClause}
                GROUP BY {groupBy};
            ";

            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);

                if (kieuHieuSuat == "Ngày")
                {
                    cmd.Parameters.AddWithValue("@Ngay", ngay.Date);
                }
                else if (kieuHieuSuat == "Tháng")
                {
                    cmd.Parameters.AddWithValue("@Nam", ngay.Year);
                    cmd.Parameters.AddWithValue("@Thang", ngay.Month);
                }

                MySqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    list.Add(new ThongKeNhanVienDTO
                    {
                        MaNV = rd.GetInt32("MaTK"),
                        HoTen = rd.GetString("HoTen"),
                        SoNgayLam = rd.GetInt32("SoNgayLam"),
                        TongDoanhThu = rd.GetDouble("TongDoanhThu"),
                        HieuSuat = rd.GetDouble("HieuSuat")
                    });
                }
            }

            return list;
        }
    }
}
