using MySql.Data.MySqlClient;
using QLBTS_DTO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBTS_DAL
{
    public class DanhMucSanPhamDAL
    {
        public List<SanPhamDTO> LayDanhSachSanPham(string filterType = null)
        {
            List<SanPhamDTO> list = new List<SanPhamDTO>();
            string query = BuildFilterQuery(filterType);

            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        SanPhamDTO sp = new SanPhamDTO
                        {
                            MaSP = Convert.ToInt32(reader["MaSP"]),
                            TenSP = reader["TenSP"].ToString(),
                            LoaiSP = reader["LoaiSP"].ToString(),
                            Size = reader["Size"].ToString(),
                            SoLuong = Convert.ToInt32(reader["SoLuong"]),
                            Gia = Convert.ToInt32(reader["Gia"]),
                            KhuyenMai = Convert.ToInt32(reader["KhuyenMai"]),
                            TrangThai = reader["TrangThai"].ToString(),
                            HinhAnh = reader["HinhAnh"] != DBNull.Value ? (byte[])reader["HinhAnh"] : null
                        };
                        list.Add(sp);
                    }
                }
            }

            return list;
        }


        private string BuildFilterQuery(string filterType = null)
        {
            string baseQuery = "SELECT MaSP, TenSP, LoaiSP, Size, SoLuong, Gia, KhuyenMai, HinhAnh, TrangThai FROM SanPham";

            if (string.IsNullOrWhiteSpace(filterType))
                return baseQuery;

            string query = baseQuery + " ";

            switch (filterType)
            {
                case "BestSeller":
                    query += "ORDER BY KhuyenMai DESC LIMIT 10";  // 10 sản phẩm có khuyến mãi cao nhất
                    break;

                case "Topping":
                    query += "WHERE LOWER(LoaiSP) = 'topping'"; // loại = topping
                    break;

                case "TraSua":
                    query += "WHERE REPLACE(LOWER(LoaiSP), ' ', '') LIKE '%trasua%'"; // loại chứa 'trasua', bỏ dấu, lowercase
                    break;

                case "MonMoi":
                    query += "ORDER BY MaSP DESC LIMIT 10"; // 10 sản phẩm mới nhất theo MaSP
                    break;
                default:
                    // Nếu filterType khác, lọc theo LoaiSP = filterType
                    query += "WHERE LOWER(LoaiSP) = '" + filterType.ToLower() + "'";
                    break;
            }

            return query;
        }

        public List<SanPhamDTO> TimKiemSanPham(string keyword)
        {
            List<SanPhamDTO> list = new List<SanPhamDTO>();

            if (string.IsNullOrWhiteSpace(keyword))
                return list;

            string normalizedKeyword = keyword.ToLower().Replace(" ", "");

            string query = "SELECT MaSP, TenSP, LoaiSP, Size, SoLuong, Gia, KhuyenMai, HinhAnh, TrangThai " +
                           "FROM SanPham";

            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string loaiSP = reader["LoaiSP"].ToString();
                        string tenSP = reader["TenSP"].ToString();

                        string normalizedTenSP = RemoveAccents(tenSP).ToLower().Replace(" ", "");

                        if (!normalizedTenSP.Contains(normalizedKeyword))
                            continue;

                        SanPhamDTO sp = new SanPhamDTO
                        {
                            MaSP = Convert.ToInt32(reader["MaSP"]),
                            TenSP = tenSP,
                            LoaiSP = loaiSP,
                            Size = reader["Size"].ToString(),
                            SoLuong = Convert.ToInt32(reader["SoLuong"]),
                            Gia = Convert.ToInt32(reader["Gia"]),
                            KhuyenMai = Convert.ToInt32(reader["KhuyenMai"]),
                            TrangThai = reader["TrangThai"].ToString(),
                            HinhAnh = reader["HinhAnh"] != DBNull.Value ? (byte[])reader["HinhAnh"] : null
                        };

                        list.Add(sp);
                    }
                }
            }

            return list;
        }

        public static string RemoveAccents(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return text;

            text = text.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();
            foreach (char c in text)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    sb.Append(c);
            }
            sb.Replace('Đ', 'D');
            sb.Replace('đ', 'd');
            return sb.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}

