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
    public partial class ChiTietDonHang : Form
    {
        private int maTK;
        private int maDH;
        ChiTietDonHangBLL ctdhBLL = new ChiTietDonHangBLL();
        DonHangBLL donHangBLL = new DonHangBLL();

        public ChiTietDonHang(int _maDH)
        {
            InitializeComponent();
            maTK = Khung.MaTK_temp;
            maDH = _maDH;
        }

        private void Chitietdonhang_Load(object sender, EventArgs e)
        {
            maTK = Khung.MaTK_temp;
            LoadChiTietDonhang();
        }

        private void LoadChiTietDonhang()
        {
            flowCart.Controls.Clear();
            var danhSachSP = ctdhBLL.LayChiTietDonHangTheoMaDH(maDH);

            foreach (var sp in danhSachSP)
            {
                Panel pnl = new Panel()
                {
                    Width = 580,
                    Height = 110,
                    BorderStyle = BorderStyle.FixedSingle,
                    Margin = new Padding(5)
                };

                // ===== ẢNH SẢN PHẨM =====
                PictureBox pic = new PictureBox()
                {
                    Width = 90,
                    Height = 90,
                    Location = new Point(10, 10),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    BorderStyle = BorderStyle.None
                };

                if (sp.HinhAnh != null && sp.HinhAnh.Length > 0)
                {
                    using (var ms = new MemoryStream(sp.HinhAnh))
                        pic.Image = Image.FromStream(ms);
                }

                pic.Paint += (s, e) =>
                {
                    using (GraphicsPath gp = new GraphicsPath())
                    {
                        gp.AddEllipse(0, 0, pic.Width - 1, pic.Height - 1);
                        pic.Region = new Region(gp);
                        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    }
                };

                // ===== THÔNG TIN =====
                Label lblTen = new Label()
                {
                    Text = sp.TenSP,
                    Font = new Font("Times New Roman", 14, FontStyle.Bold),
                    AutoSize = true,
                    Location = new Point(110, 10)
                };

                Label lblSize = new Label()
                {
                    Text = $"Size: {sp.Size}",
                    Location = new Point(110, 45),
                    AutoSize = true
                };

                Label lblGia = new Label()
                {
                    Text = $"Giá: {sp.Gia:N0}đ",
                    Location = new Point(110, 70),
                    AutoSize = true
                };

                Label lblSoLuong = new Label()
                {
                    Text = $"Số lượng: {sp.SoLuong}",
                    Location = new Point(250, 70),
                    AutoSize = true
                };

                pnl.Controls.Add(pic);
                pnl.Controls.Add(lblTen);
                pnl.Controls.Add(lblSize);
                pnl.Controls.Add(lblGia);
                pnl.Controls.Add(lblSoLuong);

                flowCart.Controls.Add(pnl);
            }

            label2.Text = danhSachSP.Count.ToString();
            decimal[] tongTien = donHangBLL.TinhTongTienDonHang(maDH);

            // Tổng tiền trước và sau giảm giá
            label7.Text = $"{tongTien[0]:N0}đ";   // Tổng trước khuyến mãi
            label12.Text = $"{tongTien[1]:N0}đ";  // Tổng sau khuyến mãi
        }
    }
}
