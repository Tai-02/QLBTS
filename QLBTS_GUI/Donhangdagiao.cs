#nullable disable
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text; // Thêm thư viện này để dùng StringBuilder
using System.Windows.Forms;
using QLBTS_DAL;
using QLBTS_DTO;

namespace QLBTS_GUI
{
    public partial class Donhangdagiao : Form
    {
        // Thay thế DataGridView bằng một RichTextBox
        private RichTextBox rtbLichSu;
        private Label lblTitle;
        private DonHangDAL donHangDAL;

        public Donhangdagiao()
        {
            InitializeComponent();
            donHangDAL = new DonHangDAL();
            SetupControls();
            LoadLichSuGiaoHang();
        }

        private void SetupControls()
        {
            // Title Label (giữ nguyên)
            lblTitle = new Label
            {
                Text = "Lịch Sử Đơn Hàng Đã Giao",
                Font = new Font("Segoe UI", 22, FontStyle.Bold),
                AutoSize = false,
                Size = new Size(this.ClientSize.Width, 50),
                Location = new Point(0, 40),
                TextAlign = ContentAlignment.MiddleCenter,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            this.Controls.Add(lblTitle);

            // RichTextBox để hiển thị danh sách
            rtbLichSu = new RichTextBox
            {
                Location = new Point(50, 120),
                Size = new Size(this.ClientSize.Width - 100, this.ClientSize.Height - 170),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                ReadOnly = true, // Chỉ cho phép đọc, không cho sửa
                Font = new Font("Consolas", 11f), // Dùng font Consolas cho dễ nhìn
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White
            };
            this.Controls.Add(rtbLichSu);
        }

        private void LoadLichSuGiaoHang()
        {
            try
            {
                // Dùng StringBuilder để tạo chuỗi hiệu quả hơn
                StringBuilder sb = new StringBuilder();
                List<DonHangDTO> danhSach = donHangDAL.GetLichSuDonHangDaGiao();

                if (danhSach.Count > 0)
                {
                    foreach (var dh in danhSach)
                    {
                        // Thêm thông tin của mỗi đơn hàng vào chuỗi theo thứ tự bạn yêu cầu
                        sb.AppendLine($"Mã Đơn Hàng  : {dh.MaDH}");
                        sb.AppendLine($"Mã NV Giao    : {dh.MaNVGiao}");
                        sb.AppendLine($"Tổng Tiền     : {string.Format("{0:N0}đ", dh.TongTien)}");
                        sb.AppendLine($"Trạng Thái    : {dh.TrangThai}");
                        sb.AppendLine($"Thời Gian Giao: {dh.ThoiGianGiao.ToString("dd/MM/yyyy HH:mm:ss")}");
                        sb.AppendLine("-------------------------------------------------"); // Thêm dấu gạch ngang để phân cách các đơn hàng
                    }
                }
                else
                {
                    sb.AppendLine("Hiện tại chưa có đơn hàng nào được giao thành công.");
                    lblTitle.Text = "Chưa có đơn hàng nào được giao";
                }

                // Gán toàn bộ chuỗi đã tạo vào RichTextBox
                rtbLichSu.Text = sb.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải lịch sử đơn hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Các phương thức này không cần dùng đến nữa
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void Donhangdagiao_Load(object sender, EventArgs e) { }
    }
}