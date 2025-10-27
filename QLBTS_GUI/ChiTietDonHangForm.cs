#nullable disable
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO; // Thêm thư viện này để dùng MemoryStream
using System.Windows.Forms;
using QLBTS_DAL;
using QLBTS_DTO;

namespace QLBTS_GUI
{
    public partial class ChiTietDonHangForm : Form
    {
        private int maDonHang;
        private DonHangDAL donHangDAL;

        private Panel leftPanel;
        private Panel rightPanel;

        public ChiTietDonHangForm(int maDonHang)
        {
            this.maDonHang = maDonHang;
            this.donHangDAL = new DonHangDAL();

            InitializeComponent();
            SetupForm();
            LoadOrderDetails();
        }

        private void SetupForm()
        {
            this.Text = "Chi Tiết Đơn Hàng #" + this.maDonHang;
            this.Size = new Size(1000, 600);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.White;

            float heightRatio = 0.75f;
            int contentHeight = (int)(this.ClientSize.Height * heightRatio);
            int topSpace = this.ClientSize.Height - contentHeight;
            int startY = topSpace;

            int margin = 20;
            int gap = 10;
            int rightPanelWidth = 380;

            rightPanel = new Panel
            {
                BackColor = Color.FromArgb(173, 216, 230),
                Location = new Point(this.ClientSize.Width - rightPanelWidth - margin, startY),
                Size = new Size(rightPanelWidth, contentHeight),
                Padding = new Padding(30),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right
            };
            this.Controls.Add(rightPanel);

            leftPanel = new Panel
            {
                Location = new Point(margin, startY),
                Size = new Size(this.ClientSize.Width - rightPanelWidth - (margin * 2) - gap, contentHeight),
                Padding = new Padding(40, 30, 40, 30),
                AutoScroll = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };
            this.Controls.Add(leftPanel);
        }

        private void LoadOrderDetails()
        {
            try
            {
                DonHangDTO donHang = donHangDAL.GetDonHangById(maDonHang);
                List<ChiTietDonHangDTO> chiTietList = donHangDAL.GetChiTietDonHang(maDonHang);

                if (donHang == null)
                {
                    MessageBox.Show("Không tìm thấy thông tin đơn hàng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                PopulateCartItems(chiTietList);
                PopulateSummary(donHang);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải chi tiết đơn hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopulateCartItems(List<ChiTietDonHangDTO> items)
        {
            leftPanel.Controls.Clear();
            int currentY = 0;

            Label cartTitle = new Label { Text = "Giỏ hàng của bạn", Font = new Font("Segoe UI", 20, FontStyle.Bold), Location = new Point(0, currentY), AutoSize = true };
            leftPanel.Controls.Add(cartTitle);
            currentY += cartTitle.Height;

            Label itemCount = new Label { Text = $"{items.Count} sản phẩm", Font = new Font("Segoe UI", 12), ForeColor = Color.Gray, Location = new Point(0, currentY), AutoSize = true };
            leftPanel.Controls.Add(itemCount);
            currentY += itemCount.Height + 40;

            foreach (var item in items)
            {
                AddCartItem(item, ref currentY);
            }
        }

        // ==========================================================
        // ===        PHƯƠNG THỨC ĐÃ ĐƯỢC SỬA LẠI HOÀN TOÀN       ===
        // ==========================================================
        private void AddCartItem(ChiTietDonHangDTO item, ref int yPos)
        {
            // === TOÀN BỘ PHẦN CODE VỀ PICTUREBOX ĐÃ BỊ XÓA ===

            // Tên sản phẩm
            Label nameLabel = new Label
            {
                Text = item.TenSP,
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                Location = new Point(0, yPos + 5), // Vị trí X bắt đầu từ 0
                AutoSize = true
            };
            leftPanel.Controls.Add(nameLabel);

            // Size
            Label sizeLabel = new Label
            {
                Text = $"Size: {item.Size}",
                Font = new Font("Segoe UI", 12),
                Location = new Point(0, yPos + 50), // Vị trí Y được điều chỉnh lại
                AutoSize = true
            };
            leftPanel.Controls.Add(sizeLabel);

            // Số lượng
            Label qtyLabel = new Label
            {
                Text = $"Số lượng: {item.SoLuong}",
                Font = new Font("Segoe UI", 12),
                Location = new Point(140, yPos + 50), // Vị trí X được điều chỉnh lại
                AutoSize = true
            };
            leftPanel.Controls.Add(qtyLabel);

            // Giá
            Label priceLabel = new Label
            {
                Text = $"Giá: {string.Format("{0:N0}đ", item.DonGia)}",
                Font = new Font("Segoe UI", 12),
                ForeColor = Color.Gray,
                Location = new Point(0, yPos + 75), // Vị trí Y được điều chỉnh lại
                AutoSize = true
            };
            leftPanel.Controls.Add(priceLabel);

            // Tăng vị trí Y cho sản phẩm tiếp theo, giá trị nhỏ hơn vì không còn ảnh
            yPos += 120;
        }
        private void PopulateSummary(DonHangDTO order)
        {
            rightPanel.Controls.Clear();

            Label summaryTitle = new Label { Text = "Tổng kết", Font = new Font("Segoe UI Semibold", 28, FontStyle.Bold), AutoSize = true };
            rightPanel.Controls.Add(summaryTitle);
            summaryTitle.Location = new Point((rightPanel.ClientSize.Width - summaryTitle.Width) / 2, 20);

            AddSummaryLine("Tạm tính", string.Format("{0:N0}đ", order.TongTien), 150);
            AddSummaryLine("Giảm giá", "0%", 220);

            Panel divider = new Panel { BackColor = Color.Black, Height = 1, Width = rightPanel.ClientSize.Width, Location = new Point(0, rightPanel.ClientSize.Height - 150) };
            rightPanel.Controls.Add(divider);

            Label totalLabel = new Label { Text = "Tổng cộng", Font = new Font("Segoe UI", 20, FontStyle.Bold), Location = new Point(0, rightPanel.ClientSize.Height - 120), AutoSize = true };
            rightPanel.Controls.Add(totalLabel);

            Label totalValue = new Label { Text = string.Format("{0:N0}đ", order.TongTien), Font = new Font("Segoe UI", 20, FontStyle.Bold), AutoSize = true, Location = new Point(0, rightPanel.ClientSize.Height - 120) };
            rightPanel.Controls.Add(totalValue);
            totalValue.Left = rightPanel.ClientSize.Width - totalValue.Width;
        }

        private void AddSummaryLine(string text, string value, int yPos)
        {
            Label textLabel = new Label { Text = text, Font = new Font("Segoe UI", 16), Location = new Point(0, yPos), AutoSize = true };
            rightPanel.Controls.Add(textLabel);

            Label valueLabel = new Label { Text = value, Font = new Font("Segoe UI", 16), AutoSize = true, Location = new Point(0, yPos) };
            rightPanel.Controls.Add(valueLabel);
            valueLabel.Left = rightPanel.ClientSize.Width - valueLabel.Width;
        }

        private void chitietsanpham_Load(object sender, EventArgs e) { }
    }
}