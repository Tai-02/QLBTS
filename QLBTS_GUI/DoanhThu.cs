using QLBTS_BLL;
using QLBTS_DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace QLBTS_GUI
{
    public partial class DoanhThu : Form
    {
        private ThongKeDoanhThuBLL thongKeBLL = new ThongKeDoanhThuBLL();

        public DoanhThu()
        {
            InitializeComponent();
        }

        private void DoanhThu_Load(object sender, EventArgs e)
        {
            try
            {
                cbbTieuChi.Items.Clear();
                cbbTieuChi.Items.AddRange(new string[] { "Ngày", "Tháng", "Năm" });
                cbbTieuChi.SelectedIndex = 1; // mặc định Tháng
                LoadDuLieuVaVeBieuDo();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải form: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDuLieuVaVeBieuDo()
        {
            try
            {
                if (cbbTieuChi.SelectedItem == null) return;

                DateTime ngayChon = dtpNgayThangNam.Value;
                string tieuChi = cbbTieuChi.SelectedItem.ToString();

                // Lấy dữ liệu từ BLL (bỏ comment dòng dưới để dùng dữ liệu thật)
                object[,] data = thongKeBLL.GetDoanhThuArray(ngayChon, tieuChi);

                // Dùng dữ liệu mẫu để test (comment dòng dưới khi dùng database)
                // object[,] data = TaoDuLieuMau(tieuChi);

                VeBieuDo(data, tieuChi, ngayChon);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Tạo dữ liệu mẫu để test biểu đồ
        private object[,] TaoDuLieuMau(string tieuChi)
        {
            Random random = new Random();

            if (tieuChi == "Ngày")
            {
                // Dữ liệu mẫu cho 24 giờ
                object[,] data = new object[2, 24];
                for (int h = 0; h < 24; h++)
                {
                    data[0, h] = h; // Giờ
                    // Giờ làm việc (8h-22h) có doanh thu cao hơn
                    if (h >= 8 && h <= 22)
                    {
                        data[1, h] = random.Next(100000, 500000);
                    }
                    else
                    {
                        data[1, h] = random.Next(0, 100000);
                    }
                }
                return data;
            }
            else if (tieuChi == "Tháng")
            {
                // Dữ liệu mẫu cho 30 ngày
                object[,] data = new object[2, 30];
                for (int d = 1; d <= 30; d++)
                {
                    data[0, d - 1] = d; // Ngày
                    // Cuối tuần có doanh thu cao hơn
                    if (d % 7 == 0 || d % 7 == 6)
                    {
                        data[1, d - 1] = random.Next(800000, 1500000);
                    }
                    else
                    {
                        data[1, d - 1] = random.Next(300000, 800000);
                    }
                }
                return data;
            }
            else if (tieuChi == "Năm")
            {
                // Dữ liệu mẫu cho 12 tháng
                object[,] data = new object[2, 12];
                for (int m = 1; m <= 12; m++)
                {
                    data[0, m - 1] = m; // Tháng
                    // Tháng 12 (mùa lễ) có doanh thu cao nhất
                    if (m == 12)
                    {
                        data[1, m - 1] = random.Next(20000000, 30000000);
                    }
                    // Tháng 1, 2 (Tết) cũng cao
                    else if (m == 1 || m == 2)
                    {
                        data[1, m - 1] = random.Next(18000000, 25000000);
                    }
                    else
                    {
                        data[1, m - 1] = random.Next(10000000, 18000000);
                    }
                }
                return data;
            }

            return new object[2, 0];
        }

        private void VeBieuDo(object[,] data, string tieuChi, DateTime ngayChon)
        {
            chartThongKe.Series.Clear();
            chartThongKe.Titles.Clear();
            chartThongKe.ChartAreas.Clear();

            // ẨN LEGEND (chú thích)
            chartThongKe.Legends.Clear();

            // Tạo ChartArea mới
            ChartArea area = new ChartArea("MainArea");
            chartThongKe.ChartAreas.Add(area);

            // Cấu hình trục X
            area.AxisX.Title = tieuChi;
            area.AxisX.MajorGrid.LineColor = Color.LightGray;
            area.AxisX.IsMarginVisible = false;

            // Tự động điều chỉnh interval dựa trên số lượng dữ liệu
            int soLuong = data.GetLength(1);
            if (tieuChi == "Ngày")
            {
                area.AxisX.Interval = 1;
                area.AxisX.LabelStyle.Angle = 0; // Không xoay
            }
            else if (tieuChi == "Tháng")
            {
                area.AxisX.Interval = 1;
                area.AxisX.LabelStyle.Angle = -45;
            }
            else if (tieuChi == "Năm")
            {
                area.AxisX.Interval = 1; // Hiển thị tất cả tháng
                area.AxisX.LabelStyle.Angle = 0;
            }

            // Cấu hình trục Y
            area.AxisY.Title = "Tổng Doanh Thu (VNĐ)";
            area.AxisY.LabelStyle.Format = "N0";
            area.AxisY.MajorGrid.LineColor = Color.LightGray;

            // Tạo Series dạng cột
            Series series = new Series("Tiền bán hàng");
            series.ChartType = SeriesChartType.Column;
            series.Color = Color.DodgerBlue;
            series["PointWidth"] = "0.6";

            // TẮT label trên cột để tránh lộn xộn
            series.IsValueShownAsLabel = false;

            // Tính tổng doanh thu
            decimal tongDoanhThu = 0;

            // Thêm dữ liệu từ mảng 2 chiều vào series
            for (int i = 0; i < soLuong; i++)
            {
                int thoiGian = Convert.ToInt32(data[0, i]);
                int doanhThu = Convert.ToInt32(data[1, i]);

                string label = "";
                if (tieuChi == "Ngày")
                {
                    label = thoiGian + "h";
                }
                else if (tieuChi == "Tháng")
                {
                    label = thoiGian.ToString();
                }
                else if (tieuChi == "Năm")
                {
                    label = "T" + thoiGian;
                }

                // Thêm điểm dữ liệu
                int pointIndex = series.Points.AddXY(thoiGian, doanhThu);
                series.Points[pointIndex].AxisLabel = label;
                series.Points[pointIndex].ToolTip = $"{label}: {doanhThu:N0} VNĐ";

                tongDoanhThu += doanhThu;
            }

            chartThongKe.Series.Add(series);

            // Thêm title với thông tin ngày/tháng/năm đang xem
            string titleText = "";
            if (tieuChi == "Ngày")
            {
                titleText = $"Biểu Đồ Doanh Thu Ngày {ngayChon:dd/MM/yyyy}";
            }
            else if (tieuChi == "Tháng")
            {
                titleText = $"Biểu Đồ Doanh Thu Tháng {ngayChon:MM/yyyy}";
            }
            else if (tieuChi == "Năm")
            {
                titleText = $"Biểu Đồ Doanh Thu Năm {ngayChon:yyyy}";
            }

            Title mainTitle = new Title(titleText);
            mainTitle.Font = new Font("Arial", 14, FontStyle.Bold);
            chartThongKe.Titles.Add(mainTitle);

            // Thêm subtitle hiển thị tổng
            Title subtitle = new Title($"Tổng: {tongDoanhThu:N0} VNĐ");
            subtitle.Font = new Font("Arial", 10, FontStyle.Regular);
            subtitle.ForeColor = Color.Gray;
            subtitle.Docking = Docking.Bottom;
            chartThongKe.Titles.Add(subtitle);
        }

        private void btnXemBieuDo_Click(object sender, EventArgs e)
        {
            LoadDuLieuVaVeBieuDo();
        }

        private void cbbTieuChi_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDuLieuVaVeBieuDo();
        }

        private void dtpNgayThangNam_ValueChanged(object sender, EventArgs e)
        {
            LoadDuLieuVaVeBieuDo();
        }
    }
}