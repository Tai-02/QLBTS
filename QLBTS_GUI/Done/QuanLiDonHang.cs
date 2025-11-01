using QLBTS_BBL;
using QLBTS_BLL;
using QLBTS_DTO;
using QuestPDF.Fluent;
using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace QLBTS_GUI
{
    public partial class QuanLiDonHang : Form
    {
        private readonly DonHangBLL bll;
        private readonly ChiTietDonHangBLL ctdh;
        UI_Form ui = new UI_Form();

        public QuanLiDonHang()
        {
            bll = new DonHangBLL();
            ctdh = new ChiTietDonHangBLL();
            InitializeComponent();
            dgvOrders.CellContentClick -= dgvOrders_CellContentClick;
            dgvOrders.CellContentClick += dgvOrders_CellContentClick;
            dgvOrders.CellPainting += dgvOrders_CellPainting;
            dgvOrders.CellFormatting += DgvOrders_CellFormatting;
        }

        private void QLDonHangForm_Load(object sender, EventArgs e)
        {
            LoadDonHang();
        }

        private void LoadDonHang()
        {
            dgvOrders.AutoGenerateColumns = false;

            List<DonHangDTO> dsDonHang = new List<DonHangDTO>();

            dsDonHang.AddRange(bll.LayDSDonHangTheoTrangThai("Chờ xác nhận"));
            dsDonHang.AddRange(bll.LayDSDonHangTheoTrangThai("Đã nhận"));
            dgvOrders.DataSource = dsDonHang.OrderByDescending(d => d.NgayDat).ToList();

            dgvOrders.AlternatingRowsDefaultCellStyle.BackColor = Color.White;
            dgvOrders.RowsDefaultCellStyle.BackColor = Color.White;
            dgvOrders.DefaultCellStyle.SelectionBackColor = Color.White;
            dgvOrders.DefaultCellStyle.SelectionForeColor = Color.Black;

            AddButtonColumn("btnNhan", "Nhận đơn", "Nhận", Color.LimeGreen);
            AddButtonColumn("btnHuy", "Hủy đơn", "Hủy", Color.Red);
            AddButtonColumn("btnChuyen", "Chuyển hàng", "Chuyển", Color.RoyalBlue);
            AddButtonColumn("btnChiTiet", "Chi tiết", "Xem chi tiết", Color.Black);

            dgvOrders.EnableHeadersVisualStyles = false;
            dgvOrders.ColumnHeadersDefaultCellStyle.BackColor = Color.Black;
            dgvOrders.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvOrders.ColumnHeadersDefaultCellStyle.Font = new Font("Times New Roman", 12, FontStyle.Bold);

            if (dgvOrders.Columns.Contains("Gia"))
            {
                dgvOrders.Columns["Gia"].DefaultCellStyle.Format = "N0";
                dgvOrders.Columns["Gia"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }
            dgvOrders.Refresh();
        }

        private void AddButtonColumn(string name, string header, string text, Color color)
        {
            if (dgvOrders.Columns.Contains(name)) return;

            var btn = new DataGridViewButtonColumn
            {
                Name = name,
                HeaderText = header,
                Text = text,
                UseColumnTextForButtonValue = true,
                FlatStyle = FlatStyle.Flat
            };

            btn.DefaultCellStyle.BackColor = color;
            btn.DefaultCellStyle.ForeColor = Color.White;
            btn.DefaultCellStyle.Font = new Font("Times New Roman", 11, FontStyle.Bold);
            dgvOrders.Columns.Add(btn);
        }

        private void DgvOrders_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvOrders.Columns[e.ColumnIndex].Name == "SDT" && dgvOrders.Rows[e.RowIndex].DataBoundItem is DonHangDTO dh)
            {
                e.Value = new TaiKhoanBLL().LaySDT(dh.MaKhach);
            }
            if (dgvOrders.Columns[e.ColumnIndex].Name == "DiaChi" && dgvOrders.Rows[e.RowIndex].DataBoundItem is DonHangDTO dh2)
            {
                e.Value = new TaiKhoanBLL().LayDiaChi(dh2.MaKhach);
            }
        }


        private void dgvOrders_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {

            if (e.RowIndex < 0) return; // bỏ qua header
            if (!dgvOrders.Columns[e.ColumnIndex].Name.StartsWith("btn")) return; // chỉ button

            // Lấy trạng thái của dòng hiện tại
            string trangThai = dgvOrders.Rows[e.RowIndex].Cells["TrangThai"].Value?.ToString() ?? "";
            string col = dgvOrders.Columns[e.ColumnIndex].Name;

            // Quyết định nút có hiển thị không
            bool hienThi = col switch
            {
                "btnNhan" => trangThai == "Chờ xác nhận",
                "btnHuy" => trangThai == "Chờ xác nhận" || trangThai == "Đã nhận",
                "btnChuyen" => trangThai == "Đã nhận",
                "btnChiTiet" => true,
                _ => false
            };

            if (!hienThi)
            {
                e.Handled = true; // không vẽ nút
                return;
            }

            // Vẽ nền và nội dung ô
            e.PaintBackground(e.CellBounds, true);

            // Tạo nút nhỏ gọn ở giữa ô
            Rectangle rect = new Rectangle(
                e.CellBounds.X + 4,
                e.CellBounds.Y + 4,
                e.CellBounds.Width - 8,
                e.CellBounds.Height - 8
            );

            Color btnColor = col switch
            {
                "btnNhan" => Color.LimeGreen,
                "btnHuy" => Color.Red,
                "btnChuyen" => Color.RoyalBlue,
                "btnChiTiet" => Color.Black,
                _ => Color.Black
            };

            using (SolidBrush brush = new SolidBrush(btnColor))
                e.Graphics.FillRectangle(brush, rect);

            TextRenderer.DrawText(e.Graphics, (string)e.FormattedValue,
                new Font("Times New Roman", 11, FontStyle.Bold),
                rect, Color.White,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

            e.Handled = true;
        }
        private void dgvOrders_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            int maDH = Convert.ToInt32(dgvOrders.Rows[e.RowIndex].Cells["MaDH"].Value);
            string col = dgvOrders.Columns[e.ColumnIndex].Name;
            string trangThai = dgvOrders.Rows[e.RowIndex].Cells["TrangThai"].Value?.ToString() ?? "";

            bool buttonHienThi = col switch
            {
                "btnNhan" => trangThai == "Chờ xác nhận",
                "btnHuy" => trangThai == "Chờ xác nhận" || trangThai == "Đã nhận",
                "btnChuyen" => trangThai == "Đã nhận",
                "btnChiTiet" => true,
                _ => false
            };

            if (!buttonHienThi) return;

            bool ok = false;
            DialogResult result = DialogResult.None;
            string actionText = "";
            string mota = "";


            if (col == "btnNhan")
            {
                actionText = "Đánh dấu đơn hàng này là 'Đã nhận'";
            }
            else if (col == "btnHuy")
            {
                actionText = "HỦY đơn hàng này vĩnh viễn";
            }
            else if (col == "btnChuyen")
            {
                // Logic cho nút "Chờ giao" cần xác nhận và tùy chọn In bill

                // BƯỚC 1: Hộp thoại xác nhận chuyển trạng thái VÀ In bill
                result = MessageBox.Show(
                    "Xác nhận chuyển đơn hàng sang trạng thái 'Chờ giao'?\n\nChọn 'Yes' để chuyển và In hóa đơn.\nChọn 'No' để chỉ chuyển trạng thái.",
                    "Xác nhận & In hóa đơn",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Cancel)
                {
                    return; // Hủy bỏ toàn bộ thao tác
                }

                // BƯỚC 2: Cập nhật trạng thái đơn hàng (luôn thực hiện nếu không phải Cancel)
                ok = bll.DoiTrangThai(maDH, "Chờ giao");

                // BƯỚC 3: Nếu cập nhật thành công và người dùng chọn Yes, In hóa đơn
                if (ok && result == DialogResult.Yes)
                {
                    List<SanPhamDTO> dsSanPham = ctdh.LayChiTietDonHangTheoMaDH(maDH);
                    string tenCuaHang = "Cửa hàng Momocha";

                    var hoaDonPdf = new HoaDonDocument(maDH, tenCuaHang, dsSanPham);

                    // Sinh PDF vào MemoryStream
                    using (var stream = new MemoryStream())
                    {
                        hoaDonPdf.GeneratePdf(stream);
                        stream.Position = 0;

                        // Tạo file tạm trong temp folder
                        var tempFile = Path.Combine(Path.GetTempPath(), $"HoaDon_{maDH}.pdf");
                        // Giả định bạn đã thêm 'using System.IO;'
                        File.WriteAllBytes(tempFile, stream.ToArray());

                        // Mở PDF bằng trình xem mặc định
                        // Giả định bạn đã thêm 'using System.Diagnostics;'
                        System.Diagnostics.Process.Start(new ProcessStartInfo
                        {
                            FileName = tempFile,
                            UseShellExecute = true
                        });
                    }
                }
            }
            else if (col == "btnChiTiet")
            {
                ui.OpenChildForm(new ChiTietDonHang(maDH), NVQUAY.NVQ_pn_tab);
                return;
            }

            // Xử lý xác nhận cho btnNhan và btnHuy (Chỉ áp dụng nếu không phải btnChuyen hoặc btnChiTiet)
            if (col == "btnNhan" || col == "btnHuy")
            {
                result = MessageBox.Show(
                    $"Bạn có chắc chắn muốn {actionText} không?",
                    "Xác nhận hành động",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning // Dùng Warning cho Hủy, Information cho Nhận nếu cần phân biệt
                );

                if (result == DialogResult.Yes)
                {
                    if (col == "btnNhan")
                    {
                        ok = bll.DoiTrangThai(maDH, "Đã nhận");
                        bll.setmanv(maDH, Khung.MaTK_temp, 0);
                    }
                    else if (col == "btnHuy")
                    {
                        ok = bll.HuyDonHang(maDH);
                    }
                }
            }

            if (ok)
            {
                // Thông báo thành công (tùy chọn)
                MessageBox.Show("Thao tác thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Tải lại form quản lý đơn hàng
                ui.OpenChildForm(new QuanLiDonHang(), NVQUAY.NVQ_pn_tab);
            }
            else if ((col == "btnNhan" || col == "btnHuy" || col == "btnChuyen") && result != DialogResult.No && result != DialogResult.Cancel)
            {
                // Chỉ hiển thị lỗi nếu không phải do người dùng tự hủy/chọn No
                // (Thực tế, bll.DoiTrangThai/HuyDonHang cần trả về false khi có lỗi)
                // MessageBox.Show("Thao tác thất bại. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
