using QLBTS_BLL;
using QLBTS_DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace QLBTS_GUI
{
    public partial class GioHang_NVQ : Form
    {
        private int maTK;
        private List<SanPhamDTO> danhSachSP = new();
        private GioHangBLL gioHangBLL;

        public GioHang_NVQ()
        {
            InitializeComponent();
            maTK = Khung.MaTK_temp;
            gioHangBLL = new GioHangBLL(); 
        }

        private void GioHang_NVQ_Load(object sender, EventArgs e)
        {
            LoadGioHang();
        }

        private void LoadGioHang()
        {
            flowCart.Controls.Clear();
            danhSachSP = gioHangBLL.LayGioHangTheoMaTK(maTK);

            foreach (var sp in danhSachSP)
            {
                Panel pnl = new Panel
                {
                    Width = 580,
                    Height = 110,
                    BorderStyle = BorderStyle.FixedSingle,
                    Margin = new Padding(5)
                };

                // Ảnh sản phẩm
                PictureBox pic = new PictureBox
                {
                    Width = 90,
                    Height = 90,
                    Location = new Point(10, 10),
                    SizeMode = PictureBoxSizeMode.Zoom
                };

                if (sp.HinhAnh != null && sp.HinhAnh.Length > 0)
                {
                    using (var ms = new MemoryStream(sp.HinhAnh))
                        pic.Image = Image.FromStream(ms);
                }

                pic.Paint += (s, e) =>
                {
                    using GraphicsPath gp = new GraphicsPath();
                    gp.AddEllipse(0, 0, pic.Width - 1, pic.Height - 1);
                    pic.Region = new Region(gp);
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                    if (pic.Image == null)
                        e.Graphics.FillEllipse(Brushes.LightGray, 0, 0, pic.Width - 1, pic.Height - 1);
                };

                // Thông tin sản phẩm
                Label lblTen = new Label
                {
                    Text = sp.TenSP,
                    Font = new Font("Times New Roman", 14, FontStyle.Bold),
                    AutoSize = true,
                    Location = new Point(110, 10)
                };

                Label lblSize = new Label
                {
                    Text = $"Size: {sp.Size}",
                    Location = new Point(110, 45),
                    AutoSize = true
                };

                Label lblGia = new Label
                {
                    Text = $"Giá: {sp.Gia:N0}đ",
                    Location = new Point(110, 70),
                    AutoSize = true
                };

                // Nút số lượng
                Button btnTru = new Button { Text = "-", Width = 30, Height = 30, Location = new Point(300, 40) };
                Label lblSoLuong = new Label
                {
                    Text = sp.SoLuong.ToString(),
                    Font = new Font("Segoe UI", 11, FontStyle.Bold),
                    Width = 30,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Location = new Point(335, 45)
                };
                Button btnCong = new Button { Text = "+", Width = 30, Height = 30, Location = new Point(370, 40) };
                Button btnXoa = new Button
                {
                    Text = "🗑",
                    Width = 40,
                    Height = 35,
                    Location = new Point(420, 40),
                    BackColor = Color.LightCoral
                };

                // Sự kiện thay đổi số lượng
                btnCong.Click += (s, e) => { gioHangBLL.TangSoLuong(maTK, sp.MaSP) ; lblSoLuong.Text = sp.SoLuong.ToString(); TinhTongTien(); };
                btnTru.Click += (s, e) => { gioHangBLL.GiamSoLuong(maTK, sp.MaSP); lblSoLuong.Text = sp.SoLuong.ToString(); TinhTongTien(); };
                btnXoa.Click += (s, e) =>
                {
                    if (MessageBox.Show("Xóa sản phẩm khỏi giỏ hàng?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        gioHangBLL.XoaSanPhamKhoiGio(maTK, sp.MaSP);
                        LoadGioHang();
                    }
                };

                pnl.Controls.AddRange(new Control[] { pic, lblTen, lblSize, lblGia, btnTru, lblSoLuong, btnCong, btnXoa });
                flowCart.Controls.Add(pnl);
            }

            label2.Text = danhSachSP.Count.ToString(); // số lượng sản phẩm
            TinhTongTien();
        }

        private void TinhTongTien()
        {
            decimal[] tong = gioHangBLL.TinhTongTienGioHang(maTK);
            label7.Text = $"{tong[0]:N0}đ"; // Tạm tính
            label12.Text = $"{tong[1]:N0}đ"; // Thành tiền
        }

        private void BtnXacNhan_Click(object sender, EventArgs e)
        {
            if (danhSachSP.Count == 0)
            {
                MessageBox.Show("Giỏ hàng trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            gioHangBLL.BanHangNVQ(gioHangBLL.LayGioHangTheoMaTK(maTK));
            
            // Xóa toàn bộ giỏ hàng
            gioHangBLL.XoaToanBoGio(maTK);

            // Reload giỏ hàng
            LoadGioHang();
        }
    }
}
