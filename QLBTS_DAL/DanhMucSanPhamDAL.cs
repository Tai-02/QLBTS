using MySql.Data.MySqlClient;
using QLBTS_DTO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;
using QLBTS_DTO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace QLBTS_DAL
{
    public class DanhMucSanPhamDAL
    {
        // 🔹 Lấy danh sách sản phẩm còn hàng, có thể filter theo loại
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
                        int maSP = Convert.ToInt32(reader["MaSP"]);
                        string tenSP = reader["TenSP"].ToString();
                        string loaiSP = reader["LoaiSP"].ToString();
                        int soLuong = Convert.ToInt32(reader["SoLuong"]);
                        byte[] hinhAnh = reader["HinhAnh"] != DBNull.Value ? (byte[])reader["HinhAnh"] : null;

                        int giaM = reader["GiaM"] != DBNull.Value ? Convert.ToInt32(reader["GiaM"]) : 0;
                        int giaL = reader["GiaL"] != DBNull.Value ? Convert.ToInt32(reader["GiaL"]) : 0;
                        int kmM = reader["KhuyenMaiM"] != DBNull.Value ? Convert.ToInt32(reader["KhuyenMaiM"]) : 0;
                        int kmL = reader["KhuyenMaiL"] != DBNull.Value ? Convert.ToInt32(reader["KhuyenMaiL"]) : 0;
                        string trangThai = reader["TrangThai"].ToString();

                        // Size M
                        if (giaM > 0)
                        {
                            list.Add(new SanPhamDTO
                            {
                                MaSP = maSP,
                                TenSP = tenSP,
                                LoaiSP = loaiSP,
                                SoLuong = soLuong,
                                GiaM = giaM,
                                KhuyenMaiM = kmM,
                                Size = "M",
                                TrangThai = trangThai,
                                HinhAnh = hinhAnh
                            });
                        }

                        // Size L
                        if (giaL > 0)
                        {
                            list.Add(new SanPhamDTO
                            {
                                MaSP = maSP,
                                TenSP = tenSP,
                                LoaiSP = loaiSP,
                                SoLuong = soLuong,
                                GiaL = giaL,
                                KhuyenMaiL = kmL,
                                Size = "L",
                                TrangThai = trangThai,
                                HinhAnh = hinhAnh
                            });
                        }
                    }
                }
            }

            return list;
        }


        // 🔹 Xây dựng câu lệnh SQL filter
        private string BuildFilterQuery(string filterType = null)
        {
            string baseQuery = "SELECT MaSP, TenSP, LoaiSP, SoLuong, GiaM, GiaL, KhuyenMaiM, KhuyenMaiL, HinhAnh, TrangThai FROM SanPham";
            string condition = " WHERE TrangThai != 'Hết hàng'"; // luôn loại trừ sản phẩm hết hàng
            string query = baseQuery;

            if (string.IsNullOrWhiteSpace(filterType))
                return query + condition; // tất cả sản phẩm còn hàng

            switch (filterType)
            {
                case "BestSeller":
                    query += condition + " ORDER BY KhuyenMaiM DESC LIMIT 10";  // 10 sản phẩm có KM cao nhất
                    break;

                case "Topping":
                    query += condition + " AND LOWER(LoaiSP) = 'topping'";
                    break;

                case "TraSua":
                    query += condition + " AND REPLACE(LOWER(LoaiSP), ' ', '') LIKE '%trasua%'";
                    break;

                case "MonMoi":
                    query += condition + " ORDER BY MaSP DESC LIMIT 10";
                    break;

                default:
                    query += condition + " AND LOWER(LoaiSP) = '" + filterType.ToLower() + "'";
                    break;
            }

            return query;
        }

        // 🔹 Tìm kiếm sản phẩm theo từ khóa (không dấu, không phân biệt hoa thường)
        public List<SanPhamDTO> TimKiemSanPham(string keyword)
        {
            List<SanPhamDTO> list = new List<SanPhamDTO>();

            if (string.IsNullOrWhiteSpace(keyword))
                return list;

            string normalizedKeyword = RemoveAccents(keyword).ToLower().Replace(" ", "");

            string query = "SELECT MaSP, TenSP, LoaiSP, SoLuong, GiaM, GiaL, KhuyenMaiM, KhuyenMaiL, HinhAnh, TrangThai FROM SanPham WHERE TrangThai != 'Hết hàng'";

            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string tenSP = reader["TenSP"].ToString();
                        string normalizedTenSP = RemoveAccents(tenSP).ToLower().Replace(" ", "");

                        if (!normalizedTenSP.Contains(normalizedKeyword))
                            continue;

                        SanPhamDTO sp = new SanPhamDTO
                        {
                            MaSP = Convert.ToInt32(reader["MaSP"]),
                            TenSP = tenSP,
                            LoaiSP = reader["LoaiSP"].ToString(),
                            SoLuong = Convert.ToInt32(reader["SoLuong"]),
                            GiaM = Convert.ToInt32(reader["GiaM"]),
                            GiaL = Convert.ToInt32(reader["GiaL"]),
                            KhuyenMaiM = Convert.ToInt32(reader["KhuyenMaiM"]),
                            KhuyenMaiL = Convert.ToInt32(reader["KhuyenMaiL"]),
                            TrangThai = reader["TrangThai"].ToString(),
                            HinhAnh = reader["HinhAnh"] != DBNull.Value ? (byte[])reader["HinhAnh"] : null
                        };

                        list.Add(sp);
                    }
                }
            }

            return list;
        }

        // 🔹 Loại bỏ dấu tiếng Việt
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


