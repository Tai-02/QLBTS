using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using QLBTS_BLL;
using QLBTS_DTO;

namespace QLBTS_GUI
{
    public partial class OrderDetailForm : Form
    {
        // ===== FIELDS =====
        private List<OrderDetailViewModel> _orderDetails;
        private OrderBLL _orderBLL;

        // ===== CONSTRUCTOR =====
        public OrderDetailForm()
        {
            InitializeComponent();
            _orderBLL = new OrderBLL();
        }

        // ===== PUBLIC METHODS =====

        /// <summary>
        /// Load chi tiết đơn hàng
        /// </summary>
        public void LoadOrderDetail(int maDH)
        {
            try
            {
                _orderDetails = _orderBLL.GetOrderDetails(maDH);

                if (_orderDetails == null || !_orderDetails.Any())
                {
                    ShowError("Không tìm thấy đơn hàng!");
                    this.Close();
                    return;
                }

                // Cập nhật tiêu đề form
                this.Text = $"Chi tiết đơn hàng #{maDH} - MOMOCHA";

                // Hiển thị danh sách sản phẩm
                RefreshProductList();

                // Cập nhật tổng kết
                UpdateSummary();
            }
            catch (Exception ex)
            {
                ShowError($"Lỗi khi tải đơn hàng:\n{ex.Message}");
            }
        }

        // ===== PRIVATE METHODS =====

        /// <summary>
        /// Refresh danh sách sản phẩm
        /// </summary>
        private void RefreshProductList()
        {
            productContainer.Controls.Clear();

            if (_orderDetails == null || !_orderDetails.Any())
            {
                ShowEmptyMessage();
                return;
            }

            // Hiển thị số lượng sản phẩm
            int totalItems = _orderBLL.GetTotalItems(_orderDetails);
            lblItemCount.Text = $"{totalItems} sản phẩm";

            // Hiển thị từng item
            foreach (var item in _orderDetails)
            {
                AddProductItemToUI(item);
            }
        }

        /// <summary>
        /// Hiển thị thông báo giỏ hàng trống
        /// </summary>
        private void ShowEmptyMessage()
        {
            Label emptyLabel = new Label
            {
                Text = "Đơn hàng trống",
                Font = new Font("Segoe UI", 16, FontStyle.Italic),
                ForeColor = Color.Gray,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };
            productContainer.Controls.Add(emptyLabel);
        }

        /// <summary>
        /// Tạo UI cho 1 sản phẩm
        /// </summary>
        private void AddProductItemToUI(OrderDetailViewModel item)
        {
            // Tính width thực tế (trừ scrollbar)
            int actualWidth = productContainer.ClientSize.Width - 10;

            Panel itemPanel = new Panel
            {
                Width = actualWidth,
                Height = 120,
                BackColor = Color.White,
                Margin = new Padding(0, 0, 0, 15)
            };

            // Layout: [Ảnh 90px] [Thông tin 60%] [Giá 30%]
            int imageWidth = 90;
            int infoStartX = imageWidth + 20;
            int priceStartX = actualWidth - 120;  // ✓ TÍNH TỪ BÊN PHẢI

            // ===== ẢNH =====
            PictureBox productImage = new PictureBox
            {
                Size = new Size(90, 90),
                Location = new Point(10, 15),
                SizeMode = PictureBoxSizeMode.Zoom
            };
            LoadProductImage(productImage, item.HinhAnh);
            itemPanel.Controls.Add(productImage);

            // ===== TÊN =====
            Label nameLabel = new Label
            {
                Text = item.TenSP,
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                Location = new Point(infoStartX, 15),
                Size = new Size(priceStartX - infoStartX - 10, 30),  // ✓ TỰ ĐỘNG TÍNH
                AutoSize = false,
                ForeColor = Color.Black
            };
            itemPanel.Controls.Add(nameLabel);

            // ===== SIZE & SỐ LƯỢNG =====
            Label detailLabel = new Label
            {
                Text = $"Size: {item.Size}  |  Số lượng: {item.SoLuong}",
                Font = new Font("Segoe UI", 10),
                Location = new Point(infoStartX, 50),
                AutoSize = true,
                ForeColor = Color.Gray
            };
            itemPanel.Controls.Add(detailLabel);

            // ===== GIÁ (ANCHOR BÊN PHẢI) =====
            Label priceLabel = new Label
            {
                Text = $"{item.DonGia:N0}đ",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Location = new Point(priceStartX, 45),
                Size = new Size(110, 30),
                TextAlign = ContentAlignment.MiddleRight,
                ForeColor = Color.FromArgb(255, 87, 34),
                Anchor = AnchorStyles.Top | AnchorStyles.Right  // ✓ QUAN TRỌNG
            };
            itemPanel.Controls.Add(priceLabel);

            // ===== SEPARATOR =====
            Panel separator = new Panel
            {
                Size = new Size(actualWidth, 1),
                Location = new Point(0, 119),
                BackColor = Color.LightGray
            };
            itemPanel.Controls.Add(separator);

            productContainer.Controls.Add(itemPanel);
        }

        /// <summary>
        /// Load ảnh từ byte[]
        /// </summary>
        private void LoadProductImage(PictureBox pictureBox, byte[] hinhAnh)
        {
            try
            {
                if (hinhAnh == null || hinhAnh.Length == 0)
                {
                    pictureBox.BackColor = Color.LightGray;
                    return;
                }

                using (var ms = new MemoryStream(hinhAnh))
                {
                    pictureBox.Image = Image.FromStream(ms);
                }
            }
            catch
            {
                pictureBox.BackColor = Color.LightGray;
            }
        }

        /// <summary>
        /// Cập nhật phần tổng kết
        /// </summary>
        private void UpdateSummary()
        {
            if (_orderDetails == null || !_orderDetails.Any())
            {
                lblTempValue.Text = "0đ";
                lblDiscountValue.Text = "0%";
                lblTotalValue.Text = "0đ";
                return;
            }

            decimal subTotal = _orderBLL.GetSubTotal(_orderDetails);
            decimal discount = _orderBLL.GetTotalDiscount(_orderDetails);
            decimal grandTotal = _orderBLL.GetGrandTotal(_orderDetails);

            lblTempValue.Text = $"{subTotal:N0}đ";
            lblDiscountValue.Text = discount > 0 ? $"-{discount:N0}đ" : "0%";
            lblTotalValue.Text = $"{grandTotal:N0}đ";
        }

        // ===== HELPER METHODS =====

        private void ShowError(string message)
        {
            MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void lblDiscountValue_Click(object sender, EventArgs e)
        {

        }

        private void lblTempValue_Click(object sender, EventArgs e)
        {

        }
    }
}