using QLBTS_DAL;
using QLBTS_DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QLBTS_BLL
{
    public class ThongKeDoanhThuBLL
    {
        private ThongKeDoanhThuDAL dal = new ThongKeDoanhThuDAL();

        // Trả về mảng 2 chiều: [0] = thời gian, [1] = doanh thu
        public object[,] GetDoanhThuArray(DateTime ngayChon, string tieuChi)
        {
            // Xác định khoảng thời gian và kích thước mảng
            DateTime tuNgay, denNgay;
            int soLuong = 0;

            if (tieuChi == "Ngày")
            {
                // Lấy ngày cụ thể: 3/10/2025 -> từ 3/10/2025 00:00:00 đến 3/10/2025 23:59:59
                tuNgay = ngayChon.Date;
                denNgay = ngayChon.Date.AddDays(1).AddSeconds(-1);
                soLuong = 24; // 24 giờ (0-23)
            }
            else if (tieuChi == "Tháng")
            {
                // Lấy cả tháng: 3/10/2025 -> từ 1/10/2025 đến 31/10/2025
                tuNgay = new DateTime(ngayChon.Year, ngayChon.Month, 1);
                denNgay = tuNgay.AddMonths(1).AddSeconds(-1);
                soLuong = DateTime.DaysInMonth(ngayChon.Year, ngayChon.Month); // số ngày trong tháng
            }
            else if (tieuChi == "Năm")
            {
                // Lấy cả năm: 3/10/2025 -> từ 1/1/2025 đến 31/12/2025
                tuNgay = new DateTime(ngayChon.Year, 1, 1);
                denNgay = new DateTime(ngayChon.Year, 12, 31, 23, 59, 59);
                soLuong = 12; // 12 tháng
            }
            else
            {
                return new object[2, 0];
            }

            // Lấy tất cả dữ liệu từ DAL
            List<ThongKeDoanhThuDTO> allData = dal.GetAllDoanhThu();

            // Lọc dữ liệu theo khoảng thời gian
            List<ThongKeDoanhThuDTO> dataTheo = allData
                .Where(x => x.Ngay >= tuNgay && x.Ngay <= denNgay)
                .ToList();

            // Tạo mảng 2 chiều [2, n]
            object[,] result = new object[2, soLuong];

            if (tieuChi == "Ngày")
            {
                // Lọc theo giờ (0-23)
                var grouped = dataTheo.GroupBy(x => x.Ngay.Hour)
                                      .ToDictionary(g => g.Key, g => g.Sum(x => x.TongDoanhThu));

                for (int h = 0; h < 24; h++)
                {
                    result[0, h] = h; // Giờ
                    result[1, h] = grouped.ContainsKey(h) ? grouped[h] : 0; // Doanh thu
                }
            }
            else if (tieuChi == "Tháng")
            {
                // Lọc theo ngày (1-31)
                var grouped = dataTheo.GroupBy(x => x.Ngay.Day)
                                      .ToDictionary(g => g.Key, g => g.Sum(x => x.TongDoanhThu));

                int days = DateTime.DaysInMonth(ngayChon.Year, ngayChon.Month);
                for (int d = 1; d <= days; d++)
                {
                    result[0, d - 1] = d; // Ngày
                    result[1, d - 1] = grouped.ContainsKey(d) ? grouped[d] : 0; // Doanh thu
                }
            }
            else if (tieuChi == "Năm")
            {
                // Lọc theo tháng (1-12)
                var grouped = dataTheo.GroupBy(x => x.Ngay.Month)
                                      .ToDictionary(g => g.Key, g => g.Sum(x => x.TongDoanhThu));

                for (int m = 1; m <= 12; m++)
                {
                    result[0, m - 1] = m; // Tháng
                    result[1, m - 1] = grouped.ContainsKey(m) ? grouped[m] : 0; // Doanh thu
                }
            }

            return result;
        }

        // Giữ lại method cũ để tương thích với GUI hiện tại
        public List<ThongKeDoanhThuDTO> GetDoanhThu(DateTime ngayChon, string tieuChi)
        {
            // Chuyển từ mảng 2 chiều sang List
            object[,] array = GetDoanhThuArray(ngayChon, tieuChi);
            List<ThongKeDoanhThuDTO> result = new List<ThongKeDoanhThuDTO>();

            int n = array.GetLength(1);
            for (int i = 0; i < n; i++)
            {
                ThongKeDoanhThuDTO dto = new ThongKeDoanhThuDTO();

                if (tieuChi == "Ngày")
                {
                    int gio = (int)array[0, i];
                    dto.Ngay = new DateTime(ngayChon.Year, ngayChon.Month, ngayChon.Day, gio, 0, 0);
                }
                else if (tieuChi == "Tháng")
                {
                    int ngay = (int)array[0, i];
                    dto.Ngay = new DateTime(ngayChon.Year, ngayChon.Month, ngay);
                }
                else if (tieuChi == "Năm")
                {
                    int thang = (int)array[0, i];
                    dto.Ngay = new DateTime(ngayChon.Year, thang, 1);
                }

                dto.TongDoanhThu = (int)array[1, i];
                result.Add(dto);
            }

            return result;
        }
    }
}