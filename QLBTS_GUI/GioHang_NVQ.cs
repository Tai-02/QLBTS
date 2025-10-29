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
        private int maKH = 1;
        private List<SanPhamDTO> danhSachSP = new();
        private GioHangBLL gioHangBLL = new GioHangBLL();

        public GioHang_NVQ()
        {
            InitializeComponent();
            button1.Click += BtnXacNhan_Click;
        }

        private void GioHang_Load(object sender, EventArgs e)
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

                // ======= ẢNH SẢN PHẨM =======
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
                    {
                        pic.Image = Image.FromStream(ms);
                    }
                }

                pic.Paint += (s, e) =>
                {
                    using (GraphicsPath gp = new GraphicsPath())
                    {
                        gp.AddEllipse(0, 0, pic.Width - 1, pic.Height - 1);
                        pic.Region = new Region(gp);
                        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                        if (pic.Image == null)
                        {
                            e.Graphics.FillEllipse(Brushes.LightGray, 0, 0, pic.Width - 1, pic.Height - 1);
                            StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                            e.Graphics.DrawString("No Img", new Font("Segoe UI", 8), Brushes.Gray, new RectangleF(0, 0, pic.Width, pic.Height), sf);
                        }
                    }
                };

                // ======= THÔNG TIN =======
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

                // ======= NÚT SỐ LƯỢNG =======
                Button btnTru = new Button() { Text = "-", Width = 30, Height = 30, Location = new Point(300, 40) };
                Label lblSoLuong = new Label()
                {
                    Text = sp.SoLuong.ToString(),
                    Font = new Font("Segoe UI", 11, FontStyle.Bold),
                    Width = 30,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Location = new Point(335, 45)
                };
                Button btnCong = new Button() { Text = "+", Width = 30, Height = 30, Location = new Point(370, 40) };
                Button btnXoa = new Button()
                {
                    Text = "🗑",
                    Width = 40,
                    Height = 35,
                    Location = new Point(420, 40),
                    BackColor = Color.LightCoral
                };

                // ======= SỰ KIỆN =======
                btnCong.Click += (s, e) =>
                {
                    gioHangBLL.ThayDoiSoLuong(maKH, sp, 1);
                    lblSoLuong.Text = sp.SoLuong.ToString();
                    TinhTongTien();
                };

                btnTru.Click += (s, e) =>
                {
                    gioHangBLL.ThayDoiSoLuong(maKH, sp, -1);
                    lblSoLuong.Text = sp.SoLuong.ToString();
                    TinhTongTien();
                };

                btnXoa.Click += (s, e) =>
                {
                    if (MessageBox.Show("Xóa sản phẩm khỏi giỏ hàng?", "Xác nhận",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        gioHangBLL.XoaSanPhamKhoiGio(maKH, sp.MaSP);
                        LoadGioHang();
                    }
                };

                pnl.Controls.Add(pic);
                pnl.Controls.Add(lblTen);
                pnl.Controls.Add(lblSize);
                pnl.Controls.Add(lblGia);
                pnl.Controls.Add(btnTru);
                pnl.Controls.Add(lblSoLuong);
                pnl.Controls.Add(btnCong);
                pnl.Controls.Add(btnXoa);

                flowCart.Controls.Add(pnl);
            }

            label2.Text = danhSachSP.Count.ToString();
            TinhTongTien();
        }

        private void TinhTongTien()
        {
            decimal tong = gioHangBLL.TinhTongTien(danhSachSP);
            label7.Text = $"{tong:N0}đ"; // Tạm tính

            string giam = label8.Text.Replace("%", "").Trim();
            label12.Text = $"{gioHangBLL.TinhThanhTien(tong, giam):N0}đ";
        }

        private void BtnXacNhan_Click(object sender, EventArgs e)
        {
            if (danhSachSP.Count == 0)
            {
                MessageBox.Show("Giỏ hàng trống!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string bill = gioHangBLL.TaoHoaDonText(danhSachSP, label8.Text);
            MessageBox.Show(bill, "In Bill", MessageBoxButtons.OK, MessageBoxIcon.Information);

            gioHangBLL.XoaToanBoGio(maKH);
            LoadGioHang();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
