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

        // 🧩 Đọc ảnh từ BLOB
        //private async Task<Image> LoadImageFromBlobAsync(byte[] blob)
        //{
        //    return await Task.Run(() =>
        //    {
        //        try
        //        {
        //            if (blob != null && blob.Length > 0)
        //            {
        //                using (var ms = new MemoryStream(blob))
        //                {
        //                    return Image.FromStream(ms);
        //                }
        //            }
        //        }
        //        catch
        //        {
        //            // Bỏ qua lỗi ảnh
        //        }

        //        // Ảnh mặc định
        //        return Image.FromFile(@"D:\HK5\HK5-NHAP MON CONG NGHE PHAN MEM\QLBTS HinhAnh\unnamed.jpg");
        //    });
        //}


        private async Task TimKiemSanPhamAsync(string tuKhoa)
        {
            if (string.IsNullOrWhiteSpace(tuKhoa) || tuKhoa == "tìm kiếm...")
            {
                flpSanPham.Controls.Clear();
                return;
            }

            flpSanPham.Controls.Clear();

            // 🔹 Dùng task nền để không chặn UI
            //var dsSp = await Task.Run(() =>
            //{
            //    using (var db = new QlbtsContext())
            //    {
            //        return db.SanPhams
            //            .AsNoTracking() // tối ưu đọc dữ liệu
            //            .Where(sp =>
            //                (sp.TenSP != null && sp.TenSP.ToLower().Contains(tuKhoa)) ||
            //                (sp.LoaiSP != null && sp.LoaiSP.ToLower().Contains(tuKhoa)))
            //            .ToList();
            //    }
            //});

            // 🔹 Dùng task nền để không chặn UI
            var dsSp = await Task.Run(() =>
            {
                using (var db = new QlbtsContext())
                {
                    return db.SanPhams
                        .AsNoTracking() // tối ưu đọc dữ liệu
                        .Where(sp =>
                            (sp.TenSP != null && sp.TenSP.ToLower().Contains(tuKhoa)))
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
                        //Width = 250,
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
                        ForeColor = Color.Red

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

        //LỌC SẢN PHẨM BẰNG BỘ LỌC CHECKBOX
        //private async void LocSanPham()
        //{
        //    flpSanPham.Controls.Clear();

        //    using (var db = new QlbtsContext())
        //    {
        //        var query = db.SanPhams.AsNoTracking().AsQueryable();

        //        // ✅ Lọc theo từng checkbox
        //        if (chkBestseller.Checked)
        //        {
        //            var maBestSeller = new List<string> { "SP001", "SP002", "SP003", "SP004", "SP005" };
        //            query = query.Where(sp => maBestSeller.Contains(sp.MaSP));
        //        }

        //        if (chkMonmoi.Checked)
        //        {
        //            //query = query.Where(sp =>
        //            //    string.Compare(sp.MaSP, "SP006") >= 0 &&
        //            //    (sp.LoaiSP == null || !sp.LoaiSP.ToLower().Contains("Topping"))
        //            //);

        //            // Danh sách mã Best Seller cần loại bỏ
        //            var maBestSeller = new List<string> { "SP001", "SP002", "SP003", "SP004", "SP005" };

        //            query = query.Where(sp =>
        //                // Không phải sản phẩm Best Seller
        //                !maBestSeller.Contains(sp.MaSP) &&

        //                // Không phải loại Topping
        //                (sp.LoaiSP == null || !sp.LoaiSP.ToLower().Contains("topping")) &&

        //                // Là mã sản phẩm mới (SP006 trở đi hoặc TP004 trở đi)
        //                (
        //                    (sp.MaSP.StartsWith("SP") && string.Compare(sp.MaSP, "SP006") >= 0) ||
        //                    (sp.MaSP.StartsWith("TP") && string.Compare(sp.MaSP, "TP004") >= 0)
        //                )
        //            );
        //        }

        //        if (chkTratraicay.Checked)
        //        {
        //            query = query.Where(sp => sp.LoaiSP != null && sp.LoaiSP.ToLower().Contains("trà trái cây"));
        //        }

        //        if (chkTopping.Checked)
        //        {
        //            query = query.Where(sp => sp.LoaiSP != null && sp.LoaiSP.ToLower().Contains("topping"));
        //        }

        //        // Nếu bạn muốn kết hợp với txtTimkiem (từ khóa), áp dụng phần có thể trên server:
        //        var tuKhoa = txtTimkiem.Text?.Trim().ToLower();
        //        if (!string.IsNullOrEmpty(tuKhoa) && tuKhoa != "tìm kiếm...")
        //        {
        //            query = query.Where(sp =>
        //                (sp.TenSP != null && sp.TenSP.ToLower().Contains(tuKhoa)) ||
        //                (sp.LoaiSP != null && sp.LoaiSP.ToLower().Contains(tuKhoa)) ||
        //                (sp.MaSP != null && sp.MaSP.ToLower().Contains(tuKhoa))
        //            );
        //        }

        //        var dsSp = await query.ToListAsync();

        //        if (!dsSp.Any())
        //        {
        //            flpSanPham.Controls.Add(new Label
        //            {
        //                Text = "Không tìm thấy sản phẩm phù hợp.",
        //                AutoSize = true,
        //                ForeColor = Color.Gray,
        //                Font = new Font("Segoe UI", 12, FontStyle.Italic),
        //                Margin = new Padding(10)
        //            });
        //            return;
        //        }




        //        // ✅ Hiển thị danh sách sản phẩm
        //        foreach (var sp in dsSp)
        //        {
        //            Panel pnl = new Panel
        //            {
        //                Width = 180,
        //                Height = 220,
        //                BorderStyle = BorderStyle.FixedSingle,
        //                Margin = new Padding(10),
        //                BackColor = Color.White
        //            };

        //            PictureBox pic = new PictureBox
        //            {
        //                Width = 160,
        //                Height = 120,
        //                SizeMode = PictureBoxSizeMode.Zoom,
        //                Top = 10,
        //                Left = 10,
        //                Image = Image.FromFile(@"D:\HK5\HK5-NHAP MON CONG NGHE PHAN MEM\QLBTS HinhAnh\unnamed.jpg")
        //            };

        //            Label lblTen = new Label
        //            {
        //                Text = sp.TenSP,
        //                AutoSize = false,
        //                Width = 160,
        //                Top = 140,
        //                Left = 10,
        //                TextAlign = ContentAlignment.MiddleCenter,
        //                Font = new Font("Segoe UI", 10, FontStyle.Bold)
        //            };

        //            Label lblGia = new Label
        //            {
        //                Text = $"{sp.Gia:N0} VNĐ",
        //                AutoSize = false,
        //                Width = 160,
        //                Top = 170,
        //                Left = 10,
        //                TextAlign = ContentAlignment.MiddleCenter,
        //                ForeColor = Color.Red
        //            };

        //            pnl.Controls.Add(pic);
        //            pnl.Controls.Add(lblTen);
        //            pnl.Controls.Add(lblGia);
        //            flpSanPham.Controls.Add(pnl);

        //            // 🔹 Load ảnh song song
        //            _ = Task.Run(async () =>
        //            {
        //                var img = await LoadImageAsync(sp.HinhAnh?.Trim() ?? "");
        //                if (img != null)
        //                {
        //                    try { pic.Invoke(new Action(() => pic.Image = img)); }
        //                    catch { }
        //                }
        //            });
        //        }
        //    }
        //}










        // 🔍 Tìm kiếm sản phẩm
        //private async Task TimKiemSanPhamAsync(string tuKhoa)
        //{
        //    if (string.IsNullOrWhiteSpace(tuKhoa) || tuKhoa == "tìm kiếm...")
        //    {
        //        flpSanPham.Controls.Clear();
        //        return;
        //    }

        //    tuKhoa = tuKhoa.ToLower();

        //    var dsSp = await Task.Run(() =>
        //    {
        //        using (var db = new QlbtsContext())
        //        {
        //            return db.SanPhams
        //                .AsNoTracking()
        //                .Where(sp => sp.TenSP.ToLower().Contains(tuKhoa))
        //                .ToList();
        //        }
        //    });

        //    flpSanPham.Controls.Clear();
        //    flpSanPham.SuspendLayout();

        //    if (dsSp.Any())
        //    {
        //        foreach (var sp in dsSp)
        //        {
        //            Panel pnl = TaoPanelSanPham(sp);
        //            flpSanPham.Controls.Add(pnl);
        //        }
        //    }
        //    else
        //    {
        //        flpSanPham.Controls.Add(new Label
        //        {
        //            Text = "Không tìm thấy sản phẩm!",
        //            AutoSize = true,
        //            ForeColor = Color.Gray,
        //            Font = new Font("Segoe UI", 12, FontStyle.Italic),
        //            Margin = new Padding(10)
        //        });
        //    }

        //    flpSanPham.ResumeLayout();
        //}

        //// 🧩 Hàm tạo panel hiển thị sản phẩm
        //private Panel TaoPanelSanPham(SanPham sp)
        //{
        //    Panel pnl = new Panel
        //    {
        //        Width = 180,
        //        Height = 220,
        //        BorderStyle = BorderStyle.FixedSingle,
        //        Margin = new Padding(10),
        //        BackColor = Color.White
        //    };

        //    PictureBox pic = new PictureBox
        //    {
        //        Width = 160,
        //        Height = 120,
        //        SizeMode = PictureBoxSizeMode.Zoom,
        //        Top = 10,
        //        Left = 10,
        //        Image = Image.FromFile(@"D:\HK5\HK5-NHAP MON CONG NGHE PHAN MEM\QLBTS HinhAnh\unnamed.jpg") // ảnh mặc định
        //    };

        //    Label lblTen = new Label
        //    {
        //        Text = sp.TenSP,
        //        AutoSize = false,
        //        Width = 160,
        //        Top = 140,
        //        Left = 10,
        //        TextAlign = ContentAlignment.MiddleCenter,
        //        Font = new Font("Segoe UI", 10, FontStyle.Bold)
        //    };

        //    Label lblGia = new Label
        //    {
        //        Text = $"{sp.Gia:N0} VNĐ",
        //        AutoSize = false,
        //        Width = 160,
        //        Top = 170,
        //        Left = 10,
        //        TextAlign = ContentAlignment.MiddleCenter,
        //        ForeColor = Color.Red
        //    };

        //    pnl.Controls.Add(pic);
        //    pnl.Controls.Add(lblTen);
        //    pnl.Controls.Add(lblGia);

        //    // Load ảnh từ BLOB song song
        //    _ = Task.Run(async () =>
        //    {
        //        var img = await LoadImageFromBlobAsync(sp.HinhAnh);
        //        if (img != null)
        //        {
        //            try
        //            {
        //                pic.Invoke(new Action(() => pic.Image = img));
        //            }
        //            catch { }
        //        }
        //    });

        //    return pnl;
        //}

        //// ✅ Lọc sản phẩm đơn giản (ví dụ theo Bestseller, Size, ...)
        //private async void LocSanPham()
        //{
        //    flpSanPham.Controls.Clear();

        //    using (var db = new QlbtsContext())
        //    {
        //        var query = db.SanPhams.AsNoTracking().AsQueryable();

        //        // Ví dụ lọc: Bestseller = 5 sản phẩm đầu tiên
        //        if (chkBestseller.Checked)
        //        {
        //            query = query.OrderBy(sp => sp.MaSP).Take(5);
        //        }

        //        // Lọc theo kích cỡ (nếu cần)
        //        if (chkMonmoi.Checked)
        //        {
        //            query = query.OrderByDescending(sp => sp.MaSP).Take(5);
        //        }

        //        var dsSp = await query.ToListAsync();

        //        if (!dsSp.Any())
        //        {
        //            flpSanPham.Controls.Add(new Label
        //            {
        //                Text = "Không tìm thấy sản phẩm phù hợp.",
        //                AutoSize = true,
        //                ForeColor = Color.Gray,
        //                Font = new Font("Segoe UI", 12, FontStyle.Italic),
        //                Margin = new Padding(10)
        //            });
        //            return;
        //        }

        //        foreach (var sp in dsSp)
        //        {
        //            flpSanPham.Controls.Add(TaoPanelSanPham(sp));
        //        }
        //    }
        //}


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

            // Đăng ký sự kiện khi tick checkbox
            //chkBestseller.CheckedChanged += (s, ev) => LocSanPham();
            //chkMonmoi.CheckedChanged += (s, ev) => LocSanPham();
            //chkTratraicay.CheckedChanged += (s, ev) => LocSanPham();
            //chkTopping.CheckedChanged += (s, ev) => LocSanPham();
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

        private void chkBestseller_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void chkMonmoi_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void chkTratraicay_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void chkTopping_CheckedChanged(object sender, EventArgs e)
        {
        }
    }
}
