using QLBTS_BLL;
using QLBTS_DAL;
using QLBTS_DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace QLBTS_GUI
{
    public partial class GioHangCuaBan : Form
    {
        private int maKH = 1;
        private List<SanPhamDTO> danhSachSP = new();
        private GioHangCuaBanBLL gioHangBLL = new();

        public GioHangCuaBan()
        {
            InitializeComponent();
        }

        private void GioHangCuaBan_Load(object sender, EventArgs e)
        {
            LoadGioHang();
        }

        private void LoadGioHang()
        {
            flowCart.Controls.Clear();
            danhSachSP = gioHangBLL.LayGioHangTheoMaKH(maKH);

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
            label7.Text = $"{gioHangBLL.TinhTongTien(danhSachSP):N0}đ";
            label12.Text = $"{gioHangBLL.TinhThanhTienSauGiam(label7.Text, label8.Text):N0}đ";
        }
    }
}
