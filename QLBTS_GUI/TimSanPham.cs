using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBTS_GUI
{
    public partial class TimSanPham : Form
    {
        private QlbtsContext db = new QlbtsContext();

        private CancellationTokenSource _ctsTimKiem; // để hủy tìm kiếm cũ


        public TimSanPham()
        {
            InitializeComponent();
            txtTimkiem.Text = "Tìm kiếm...";
            txtTimkiem.ForeColor = Color.Gray;

            txtTimkiem.Enter += txtTimkiem_Enter;
            txtTimkiem.Leave += txtTimkiem_Leave;
        }

        private async void txtTimkiem_TextChanged(object sender, EventArgs e)
        {
        }

        private static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(5); // Giới hạn 5 ảnh tải cùng lúc

        private async Task<Image> LoadImageAsync(string imagePath)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    await _semaphore.WaitAsync();

                    if (Uri.IsWellFormedUriString(imagePath, UriKind.Absolute))
                    {
                        using (var client = new WebClient())
                        {
                            byte[] data = await client.DownloadDataTaskAsync(imagePath);
                            using (var ms = new MemoryStream(data))
                                return Image.FromStream(ms);
                        }
                    }
                    else
                    {
                        string folder = @"D:\HK5\HK5-NHAP MON CONG NGHE PHAN MEM\QLBTS HinhAnh";
                        string localPath = Path.Combine(folder, imagePath);

                        if (File.Exists(localPath))
                            return Image.FromFile(localPath);
                    }
                }
                catch
                {
                    // bỏ qua lỗi tải ảnh
                }
                finally
                {
                    _semaphore.Release();
                }

                // Ảnh mặc định
                return Image.FromFile(@"D:\HK5\HK5-NHAP MON CONG NGHE PHAN MEM\QLBTS HinhAnh\unnamed.jpg");
            });
        }


        /*private void TimKiemSanPham()
        {
            string tuKhoa = txtTimkiem.Text.Trim().ToLower();

            flpSanPham.Controls.Clear(); // Xóa các sản phẩm cũ

            // Lọc sản phẩm theo tên hoặc loại có chứa từ khóa
            var dsSp = db.SanPhams
                .Where(sp =>
                    (sp.TenSP != null && sp.TenSP.ToLower().Contains(tuKhoa)) ||
                    (sp.LoaiSP != null && sp.LoaiSP.ToLower().Contains(tuKhoa)))
                .ToList();

            if (dsSp.Any())
            {
                HienThiSanPham(dsSp);
            }
            else
            {
                Label lbl = new Label();
                lbl.Text = "Không tìm thấy sản phẩm";
                lbl.Font = new Font("Segoe UI", 12, FontStyle.Italic);
                lbl.ForeColor = Color.Gray;
                lbl.AutoSize = false;
                lbl.Size = new Size(flpSanPham.Width, 40);
                lbl.TextAlign = ContentAlignment.MiddleCenter;
                flpSanPham.Controls.Add(lbl);
            }
        }
        // --- Hàm hiển thị danh sách sản phẩm ---

        private void HienThiSanPham(List<SanPham> dsSp)
        {
            foreach (var sp in dsSp)
            {
                Panel p = new Panel();
                p.Size = new Size(200, 250);
                p.Margin = new Padding(10);
                p.BackColor = Color.White;
                p.BorderStyle = BorderStyle.FixedSingle;

                // Tạo PictureBox hiển thị hình sản phẩm
                PictureBox pic = new PictureBox();
                pic.Size = new Size(180, 150);
                pic.Location = new Point(10, 10);
                pic.SizeMode = PictureBoxSizeMode.Zoom;
                pic.BorderStyle = BorderStyle.None;

                string folder = @"D:\HK5\HK5-NHAP MON CONG NGHE PHAN MEM\QLBTS HinhAnh\Best seller\";
                string imagePath = Path.Combine(folder, sp.HinhAnh ?? "").Trim();

                if (File.Exists(imagePath))
                {
                    pic.Image = Image.FromFile(imagePath);
                }



                // Label tên sản phẩm
                Label lblTen = new Label();
                lblTen.Text = sp.TenSP;
                lblTen.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                lblTen.AutoSize = false;
                lblTen.Size = new Size(180, 40);
                lblTen.Location = new Point(10, 170);
                lblTen.TextAlign = ContentAlignment.MiddleCenter;

                // Label giá
                Label lblGia = new Label();
                lblGia.Text = "Giá: " + sp.Gia.ToString("N0") + "đ";
                lblGia.Font = new Font("Segoe UI", 9, FontStyle.Italic);
                lblGia.ForeColor = Color.Red;
                lblGia.AutoSize = false;
                lblGia.Size = new Size(180, 20);
                lblGia.Location = new Point(10, 210);
                lblGia.TextAlign = ContentAlignment.MiddleCenter;

                // Thêm control con vào panel
                p.Controls.Add(pic);
                p.Controls.Add(lblTen);
                p.Controls.Add(lblGia);

                // Thêm panel sản phẩm vào FlowLayoutPanel
                flpSanPham.Controls.Add(p);
            }


        }
        */
        private async Task TimKiemSanPhamAsync(string tuKhoa)
        {
            if (string.IsNullOrWhiteSpace(tuKhoa) || tuKhoa == "tìm kiếm...")
            {
                flpSanPham.Controls.Clear();
                return;
            }

            flpSanPham.Controls.Clear();

            // 🔹 Dùng task nền để không chặn UI
            var dsSp = await Task.Run(() =>
            {
                using (var db = new QlbtsContext())
                {
                    return db.SanPhams
                        .AsNoTracking() // tối ưu đọc dữ liệu
                        .Where(sp =>
                            (sp.TenSP != null && sp.TenSP.ToLower().Contains(tuKhoa)) ||
                            (sp.LoaiSP != null && sp.LoaiSP.ToLower().Contains(tuKhoa)))
                        .ToList();
                }
            });

            flpSanPham.SuspendLayout(); // Tạm ngưng cập nhật UI để tránh lag


            // Hiển thị kết quả lên giao diện
            if (dsSp.Any())
            {
                foreach (var sp in dsSp)
                {
                    Panel pnl = new Panel
                    {
                        Width = 180,
                        Height = 220,
                        BorderStyle = BorderStyle.FixedSingle,
                        Margin = new Padding(10),
                        BackColor = Color.White
                    };

                    PictureBox pic = new PictureBox
                    {
                        Width = 160,
                        Height = 120,
                        SizeMode = PictureBoxSizeMode.Zoom,
                        Top = 10,
                        Left = 10,
                        Image = Image.FromFile(@"D:\HK5\HK5-NHAP MON CONG NGHE PHAN MEM\QLBTS HinhAnh\unnamed.jpg") // placeholder
                    };

                    Label lblTen = new Label
                    {
                        Text = sp.TenSP,
                        AutoSize = false,
                        Width = 160,
                        Top = 140,
                        Left = 10,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Font = new Font("Segoe UI", 10, FontStyle.Bold)
                    };

                    Label lblGia = new Label
                    {
                        Text = $"{sp.Gia:N0} VNĐ",
                        AutoSize = false,
                        Width = 160,
                        Top = 170,
                        Left = 10,
                        TextAlign = ContentAlignment.MiddleCenter,
                        ForeColor = Color.DarkGreen
                    };

                    pnl.Controls.Add(pic);
                    pnl.Controls.Add(lblTen);
                    pnl.Controls.Add(lblGia);
                    flpSanPham.Controls.Add(pnl);

                    // 🔹 Load ảnh thật song song (không chặn UI)
                    _ = Task.Run(async () =>
                    {
                        var img = await LoadImageAsync(sp.HinhAnh?.Trim() ?? "");
                        if (img != null)
                        {
                            try
                            {
                                pic.Invoke(new Action(() => pic.Image = img));
                            }
                            catch { }
                        }
                    });
                }
            }
            else
            {
                Label lbl = new Label
                {
                    Text = "Không tìm thấy sản phẩm!",
                    AutoSize = true,
                    ForeColor = Color.Gray,
                    Font = new Font("Segoe UI", 12, FontStyle.Italic),
                    Margin = new Padding(10)
                };
                flpSanPham.Controls.Add(lbl);
            }

            flpSanPham.ResumeLayout();
        }

        private void HienThiSanPhamBestSeller()
        {
            flpSanPham.Controls.Clear();

            // Danh sách 5 sản phẩm Best Seller cố định
            // TAO MUC LUU TRU HINH ANH SAN PHAM DE CHAY DUOC HINH
            var bestSellers = new List<(string Ten, int Gia, string Hinh)>
    {
        ("Trà sữa trân châu", 40000, @"D:\HK5\HK5-NHAP MON CONG NGHE PHAN MEM\QLBTS HinhAnh\trasuatranchau.png"),
        ("Trà sữa thái xanh", 45000, @"D:\HK5\HK5-NHAP MON CONG NGHE PHAN MEM\QLBTS HinhAnh\trasuathaixanh.png"),
        ("Trà sữa Matcha", 35000, @"D:\HK5\HK5-NHAP MON CONG NGHE PHAN MEM\QLBTS HinhAnh\trasuamatcha.png"),
        ("Trà sữa bạc hà", 35000, @"D:\HK5\HK5-NHAP MON CONG NGHE PHAN MEM\QLBTS HinhAnh\trasuabacha.png"),
        ("Trà sữa xoài kem cheese", 40000, @"D:\HK5\HK5-NHAP MON CONG NGHE PHAN MEM\QLBTS HinhAnh\trasuaxoaikemcheese.png")
    };

            foreach (var sp in bestSellers)
            {
                Panel pnl = new Panel
                {
                    Width = 200,
                    Height = 250,
                    BorderStyle = BorderStyle.FixedSingle,
                    Margin = new Padding(10),
                    BackColor = Color.White
                };

                PictureBox pic = new PictureBox
                {
                    Width = 180,
                    Height = 150,
                    Top = 10,
                    Left = 10,
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Image = File.Exists(sp.Hinh)
                        ? Image.FromFile(sp.Hinh)
                        : Image.FromFile(@"D:\HK5\HK5-NHAP MON CONG NGHE PHAN MEM\QLBTS HinhAnh\unnamed.jpg")
                };

                Label lblTen = new Label
                {
                    Text = sp.Ten,
                    AutoSize = false,
                    Width = 180,
                    Height = 40,
                    Top = 170,
                    Left = 10,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold)
                };

                Label lblGia = new Label
                {
                    Text = $"Giá: {sp.Gia:N0}đ",
                    AutoSize = false,
                    Width = 180,
                    Height = 20,
                    Top = 210,
                    Left = 10,
                    ForeColor = Color.Red,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 9, FontStyle.Italic)
                };

                pnl.Controls.Add(pic);
                pnl.Controls.Add(lblTen);
                pnl.Controls.Add(lblGia);
                flpSanPham.Controls.Add(pnl);
            }
        }


        private async void txtTimkiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                await TimKiemSanPhamAsync(txtTimkiem.Text.Trim().ToLower());
                e.SuppressKeyPress = true; // tránh tiếng beep
            }
        }

        private async void TimSanPham_Load(object sender, EventArgs e)
        {
            txtTimkiem.Text = "Tìm kiếm...";
            txtTimkiem.ForeColor = Color.Gray;

            // Hiển thị danh sách sản phẩm Best Seller khi form mở
            HienThiSanPhamBestSeller();

        }

        private void txtTimkiem_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTimkiem.Text))
            {
                txtTimkiem.Text = "Tìm kiếm...";
                txtTimkiem.ForeColor = Color.Gray;
            }
        }

        private void txtTimkiem_Enter(object sender, EventArgs e)
        {
            if (txtTimkiem.Text == "Tìm kiếm...")
            {
                txtTimkiem.Text = "";
                txtTimkiem.ForeColor = Color.Black;
            }
        }

        private async void btnTimkiem_Click(object sender, EventArgs e)
        {
            await TimKiemSanPhamAsync(txtTimkiem.Text.Trim().ToLower());
        }
    }
}
