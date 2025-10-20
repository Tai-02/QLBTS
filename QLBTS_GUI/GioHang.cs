using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBTS_GUI
{
    public partial class GioHang : Form
    {
        public GioHang()
        {
            InitializeComponent();
        }

        private void GioHang_Load(object sender, EventArgs e)
        {

            // Giả lập dữ liệu mẫu (sau này có thể lấy từ DB)
            List<SanPham> dsSanPham = new List<SanPham>
            {
                new SanPham { Ten = "Trà sữa truyền thống", Size = "M", Gia = 35000, HinhAnh = "images/trasua1.jpg" },
                new SanPham { Ten = "Trà sữa matcha", Size = "L", Gia = 45000, HinhAnh = "images/trasua2.jpg" },
                new SanPham { Ten = "Hồng trà sữa trân châu", Size = "M", Gia = 40000, HinhAnh = "images/trasua3.jpg" },
                new SanPham { Ten = "Hồng trà sữa hihi châu", Size = "M", Gia = 40000, HinhAnh = "images/trasua3.jpg" }

            };

            HienThiSanPham(dsSanPham);

        }

        private void HienThiSanPham(List<SanPham> danhSach)
        {
            flowCart.Controls.Clear();

            foreach (var sp in danhSach)
            {
                // Panel chứa toàn bộ thông tin sản phẩm
                Panel item = new Panel();
                item.Size = new Size(580, 130);
                item.BackColor = Color.White;
                item.BorderStyle = BorderStyle.FixedSingle;
                item.Margin = new Padding(5);

                // Ảnh sản phẩm
                PictureBox pic = new PictureBox();
                pic.Size = new Size(100, 100);
                pic.Location = new Point(20, 15);
                pic.SizeMode = PictureBoxSizeMode.Zoom;
                if (System.IO.File.Exists(sp.HinhAnh))
                    pic.Image = Image.FromFile(sp.HinhAnh);
                pic.Region = System.Drawing.Region.FromHrgn(
                    CreateRoundRectRgn(0, 0, pic.Width, pic.Height, 100, 100)); // bo tròn ảnh

                // Tên sản phẩm
                Label lblTen = new Label();
                lblTen.Text = sp.Ten;
                lblTen.Font = new Font("Segoe UI", 14, FontStyle.Bold);
                lblTen.Location = new Point(140, 20);
                lblTen.AutoSize = true;

                // Size
                Label lblSize = new Label();
                lblSize.Text = $"Size: {sp.Size}";
                lblSize.Font = new Font("Segoe UI", 12, FontStyle.Regular);
                lblSize.Location = new Point(200, 60);
                lblSize.AutoSize = true;

                // Giá
                Label lblGia = new Label();
                lblGia.Text = $"Giá: {sp.Gia.ToString("N0")}đ";
                lblGia.Font = new Font("Segoe UI", 12, FontStyle.Regular);
                lblGia.Location = new Point(260, 90);
                lblGia.AutoSize = true;

                // Nhãn “Số lượng”
                Label lblSL = new Label();
                lblSL.Text = "Số lượng:";
                lblSL.Font = new Font("Segoe UI", 12);
                lblSL.Location = new Point(350, 60);
                lblSL.AutoSize = true;

                // TextBox số lượng
                TextBox txtSL = new TextBox();
                txtSL.Text = "1";
                txtSL.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                txtSL.TextAlign = HorizontalAlignment.Center;
                txtSL.Size = new Size(40, 35);
                txtSL.Location = new Point(490, 55);

                // Nút trừ
                Button btnMinus = new Button();
                btnMinus.Text = "–";
                btnMinus.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                btnMinus.Size = new Size(35, 35);
                btnMinus.Location = new Point(450, 55);
                btnMinus.Click += (s, e) =>
                {
                    int sl = int.Parse(txtSL.Text);
                    if (sl > 1) txtSL.Text = (sl - 1).ToString();
                };

                // Nút cộng
                Button btnPlus = new Button();
                btnPlus.Text = "+";
                btnPlus.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                btnPlus.Size = new Size(35, 35);
                btnPlus.Location = new Point(535, 55);
                btnPlus.Click += (s, e) =>
                {
                    int sl = int.Parse(txtSL.Text);
                    txtSL.Text = (sl + 1).ToString();
                };

                // Nút xóa (icon thùng rác)
                Button btnXoa = new Button();
                btnXoa.Text = "🗑";
                btnXoa.Font = new Font("Segoe UI Emoji", 16);
                btnXoa.ForeColor = Color.Red;
                btnXoa.FlatStyle = FlatStyle.Flat;
                btnXoa.FlatAppearance.BorderSize = 0;
                btnXoa.Size = new Size(50, 50);
                btnXoa.Location = new Point(535, 85);
                btnXoa.Click += (s, e) => flowCart.Controls.Remove(item);

                // Thêm các control vào panel
                item.Controls.Add(pic);
                item.Controls.Add(lblTen);
                item.Controls.Add(lblSize);
                item.Controls.Add(lblGia);
                item.Controls.Add(lblSL);
                item.Controls.Add(txtSL);
                item.Controls.Add(btnMinus);
                item.Controls.Add(btnPlus);
                item.Controls.Add(btnXoa);

                // Thêm vào flowCart
                flowCart.Controls.Add(item);
            }
        }

        // Import hàm WinAPI để bo tròn ảnh
        [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect, int nTopRect, int nRightRect, int nBottomRect,
            int nWidthEllipse, int nHeightEllipse);


    }
}
