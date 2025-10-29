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
using QLBTS_BLL;
using QLBTS_DTO;

namespace QLBTS_GUI
{
    /// <summary>
    /// Form Giỏ hàng - Hiển thị và quản lý giỏ hàng
    /// Code hoàn chỉnh theo kiến trúc DTO của Leader
    /// </summary>
    public partial class CartForm : Form
    {
        private void lblItemCount_Click(object sender, EventArgs e)
        {
        }
        private void btnCheckout_Click(object sender, EventArgs e)
        {
            // Kiểm tra giỏ hàng trống
            if (_cartItems == null || !_cartItems.Any())
            {
                ShowWarning("Giỏ hàng trống! Vui lòng thêm sản phẩm trước khi thanh toán.");
                return;
            }

            // Kiểm tra tồn kho
            try
            {
                string stockError = _cartBLL.ValidateStockBeforeCheckout(_cartItems);

                if (stockError != null)
                {
                    ShowWarning(stockError);
                    return;
                }

                // Xác nhận thanh toán
                DialogResult result = MessageBox.Show(
                    $"Xác nhận thanh toán đơn hàng?\n\nTổng cộng: {lblTotalValue.Text}",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    // Kiểm tra lại lần cuối
                    if (_maTK <= 0)
                    {
                        ShowError("Lỗi: Không xác định được tài khoản!");
                        return;
                    }

                    try
                    {
                        int tongTien;
                        int maDH = _cartBLL.Checkout(_maTK, out tongTien);

                        // Thành công
                        ShowSuccess($"Đặt hàng thành công!\n\nMã đơn hàng: #{maDH}\nTổng tiền: {tongTien:N0}đ");

                        // Refresh giỏ hàng
                        LoadCartFromDatabase(_maTK);
                    }
                    catch (Exception ex)
                    {
                        ShowError($"Lỗi khi thanh toán:\n{ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError($"Lỗi khi thanh toán:\n{ex.Message}");
            }
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            
        }
        // ===== FIELDS =====
        private List<CartItemViewModel> _cartItems;
        private CartBLL _cartBLL;
        private int _maTK;

        // ===== CONSTRUCTOR =====
        public CartForm(int maTK)
        {
            InitializeComponent();

            if (maTK <= 0)
            {
                throw new ArgumentException("Mã tài khoản không hợp lệ!");
            }

            _cartBLL = new CartBLL();
            _maTK = maTK;
        }

        // ===== PUBLIC METHODS  =====

        /// <summary>
        /// Load giỏ hàng của Khách hàng từ database
        /// </summary>
        public void LoadCartFromDatabase(int maTK)
        {
            try
            {
                _cartItems = _cartBLL.GetCart(maTK);
                RefreshCartDisplay();
            }
            catch (Exception ex)
            {
                ShowError($"Lỗi khi tải giỏ hàng: {ex.Message}");
            }
        }

        /// <summary>
        /// Load giỏ hàng từ List (dùng cho test)
        /// </summary>
        public void LoadCart(List<CartItemViewModel> cartItems)
        {
            _cartItems = cartItems ?? new List<CartItemViewModel>();
            RefreshCartDisplay();
        }

        // ===== PRIVATE METHODS =====

        /// <summary>
        /// Refresh toàn bộ giao diện
        /// </summary>
        private void RefreshCartDisplay()
        {
            // Xóa UI cũ
            productContainer.Controls.Clear();

            // Kiểm tra giỏ hàng rỗng
            if (_cartItems == null || !_cartItems.Any())
            {
                ShowEmptyCartMessage();
                UpdateSummary();
                return;
            }

            // Hiển thị từng item
            foreach (var item in _cartItems)
            {
                AddProductItemToUI(item);
            }

            // Cập nhật tổng kết
            UpdateSummary();
        }

        /// <summary>
        /// Hiển thị thông báo giỏ hàng trống
        /// </summary>
        private void ShowEmptyCartMessage()
        {
            Label emptyLabel = new Label
            {
                Text = "Giỏ hàng trống\n\nHãy thêm sản phẩm vào giỏ hàng!",
                Font = new Font("Segoe UI", 16, FontStyle.Italic),
                ForeColor = Color.Gray,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };
            productContainer.Controls.Add(emptyLabel);
        }
        /// <summary>
        /// Refresh giỏ hàng (gọi sau khi thêm sản phẩm từ form khác)
        /// </summary>
        public void RefreshCart()
        {
            if (_cartItems != null && _cartItems.Any())
            {
                // Lấy MaKH hoặc MaNV từ item đầu tiên
                var firstItem = _cartItems.First();

                LoadCartFromDatabase(firstItem.MaTK);
            }
        }

        /// <summary>
        /// Tạo UI cho 1 sản phẩm
        /// </summary>
        private void AddProductItemToUI(CartItemViewModel item)
        {
            Panel itemPanel = new Panel
            {
                Width = productContainer.Width - 20,
                Height = 110,
                BackColor = Color.White,
                Margin = new Padding(0, 0, 0, 10),
                Tag = item
            };

            // ===== ẢNH SẢN PHẨM =====
            PictureBox productImage = new PictureBox
            {
                Size = new Size(80, 80),
                Location = new Point(15, 15),
                SizeMode = PictureBoxSizeMode.Zoom,
                BorderStyle = BorderStyle.None
            };

            LoadProductImage(productImage, item.HinhAnh);

            // Làm tròn góc ảnh
            productImage.Paint += (s, e) =>
            {
                System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
                path.AddEllipse(0, 0, 80, 80);
                ((PictureBox)s).Region = new Region(path);
            };
            itemPanel.Controls.Add(productImage);

            // ===== TÊN SẢN PHẨM =====
            Label nameLabel = new Label
            {
                Text = item.TenSP,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Location = new Point(110, 15),
                Size = new Size(350, 30),  // ← THAY ĐỔI: 200 → 350 (width), 25 → 30 (height)
                AutoSize = false,
                ForeColor = Color.Black
            };
            itemPanel.Controls.Add(nameLabel);

            // ===== SIZE =====
            Label sizeLabel = new Label
            {
                Text = $"Size: {item.Size}",
                Font = new Font("Segoe UI", 10),
                Location = new Point(110, 45),
                AutoSize = true,
                ForeColor = Color.Gray
            };
            itemPanel.Controls.Add(sizeLabel);

            // ===== GIÁ (Có khuyến mãi hoặc không) =====
            AddPriceLabels(itemPanel, item);

            // ===== NÚT GIẢM (-) =====
            Button minusBtn = new Button
            {
                Text = "−",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Size = new Size(35, 35),
                Location = new Point(300, 50),
                BackColor = Color.WhiteSmoke,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Tag = item
            };
            minusBtn.FlatAppearance.BorderSize = 0;
            minusBtn.Click += MinusBtn_Click;
            itemPanel.Controls.Add(minusBtn);

            // ===== LABEL SỐ LƯỢNG =====
            Label qtyLabel = new Label
            {
                Text = item.SoLuong.ToString(),
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Location = new Point(340, 50),
                Size = new Size(45, 30),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.Black,
                Tag = item
            };
            itemPanel.Controls.Add(qtyLabel);

            // ===== NÚT TĂNG (+) =====
            Button plusBtn = new Button
            {
                Text = "+",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Size = new Size(35, 35),
                Location = new Point(390, 50),
                BackColor = Color.WhiteSmoke,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Tag = item
            };
            plusBtn.FlatAppearance.BorderSize = 0;
            plusBtn.Click += PlusBtn_Click;
            itemPanel.Controls.Add(plusBtn);

            // ===== NÚT XÓA =====
            Button deleteBtn = new Button
            {
                Text = "🗑",
                Font = new Font("Segoe UI", 14),
                Size = new Size(40, 35),
                Location = new Point(440, 50),
                BackColor = ColorTranslator.FromHtml("#FFEBEE"),
                ForeColor = Color.Red,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Tag = item
            };
            deleteBtn.FlatAppearance.BorderSize = 0;
            deleteBtn.Click += DeleteBtn_Click;
            itemPanel.Controls.Add(deleteBtn);

            // ===== TRẠNG THÁI (Nếu hết hàng) =====
            if (!item.ConHang)
            {
                Label outOfStockLabel = new Label
                {
                    Text = "HẾT HÀNG",
                    Font = new Font("Segoe UI", 9, FontStyle.Bold),
                    Location = new Point(500, 50),
                    AutoSize = true,
                    ForeColor = Color.Red
                };
                itemPanel.Controls.Add(outOfStockLabel);
            }

            // ===== ĐƯỜNG KẺ PHÂN CÁCH =====
            Panel separator = new Panel
            {
                Size = new Size(itemPanel.Width, 1),
                Location = new Point(0, 109),
                BackColor = Color.LightGray
            };
            itemPanel.Controls.Add(separator);
            // Thêm vào container
            productContainer.Controls.Add(itemPanel);
        }

        /// <summary>
        /// Thêm labels hiển thị giá (có xử lý khuyến mãi)
        /// </summary>
        private void AddPriceLabels(Panel itemPanel, CartItemViewModel item)
        {
            if (item.CoKhuyenMai)
            {
                // Giá gốc (gạch ngang)
                Label originalPriceLabel = new Label
                {
                    Text = $"{item.GiaGoc:N0}đ",  // ✓ Hiển thị giá gốc
                    Font = new Font("Segoe UI", 9, FontStyle.Strikeout),
                    Location = new Point(110, 70),
                    AutoSize = true,
                    ForeColor = Color.Gray
                };
                itemPanel.Controls.Add(originalPriceLabel);

                // Giá sau khuyến mãi (ĐÃ SỬA)
                Label discountPriceLabel = new Label
                {
                    Text = $"Giá: {item.GiaSauKM:N0}đ",  // ✓ SỬA: dùng GiaSauKM
                    Font = new Font("Segoe UI", 9),
                    Location = new Point(160, 70),
                    AutoSize = true,
                    ForeColor = Color.Red
                };
                itemPanel.Controls.Add(discountPriceLabel);
            }
            else
            {
                Label priceLabel = new Label
                {
                    Text = $"Giá: {item.Gia:N0}đ",  // ✓ Đúng (không có KM)
                    Font = new Font("Segoe UI", 10),
                    Location = new Point(110, 70),
                    AutoSize = true,
                    ForeColor = Color.Black
                };
                itemPanel.Controls.Add(priceLabel);
            }
        }

        /// <summary>
        /// Load ảnh từ byte[] (LONGBLOB)
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

                // Chuyển byte[] thành Image
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

        // ===== EVENT HANDLERS =====

        /// <summary>
        /// Sự kiện giảm số lượng
        /// </summary>
        private void MinusBtn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            CartItemViewModel item = (CartItemViewModel)btn.Tag;

            if (item.SoLuong > 1)
            {
                item.SoLuong--;

                try
                {
                    bool success = _cartBLL.UpdateQuantity(item.MaCTGH, item.SoLuong);

                    if (success)
                    {
                        RefreshCartDisplay();
                    }
                    else
                    {
                        ShowWarning("Không thể cập nhật số lượng!");
                        item.SoLuong++; // Rollback
                    }
                }
                catch (Exception ex)
                {
                    ShowError($"Lỗi: {ex.Message}");
                    item.SoLuong++; // Rollback
                }
            }
        }

        /// <summary>
        /// Sự kiện tăng số lượng
        /// </summary>
        private void PlusBtn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            CartItemViewModel item = (CartItemViewModel)btn.Tag;

            item.SoLuong++;

            try
            {
                bool success = _cartBLL.UpdateQuantity(item.MaCTGH, item.SoLuong);

                if (success)
                {
                    RefreshCartDisplay();
                }
                else
                {
                    ShowWarning("Không thể cập nhật số lượng!");
                    item.SoLuong--; // Rollback
                }
            }
            catch (Exception ex)
            {
                ShowError($"Lỗi: {ex.Message}");
                item.SoLuong--; // Rollback
            }
        }

        /// <summary>
        /// Sự kiện xóa sản phẩm
        /// </summary>
        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            CartItemViewModel item = (CartItemViewModel)btn.Tag;

            DialogResult result = MessageBox.Show(
                $"Bạn có chắc muốn xóa '{item.TenSP}' khỏi giỏ hàng?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    bool success = _cartBLL.RemoveItem(item.MaCTGH);

                    if (success)
                    {
                        _cartItems.Remove(item);
                        RefreshCartDisplay();
                        ShowSuccess("Đã xóa sản phẩm khỏi giỏ hàng!");
                    }
                    else
                    {
                        ShowWarning("Không thể xóa sản phẩm!");
                    }
                }
                catch (Exception ex)
                {
                    ShowError($"Lỗi: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Cập nhật phần tổng kết bên phải
        /// </summary>
        private void UpdateSummary()
        {
            if (_cartItems == null || !_cartItems.Any())
            {
                lblItemCount.Text = "0 sản phẩm";
                lblTempValue.Text = "0đ";
                lblDiscountValue.Text = "0đ";
                lblTotalValue.Text = "0đ";
                return;
            }

            // Tính toán
            int totalItems = _cartBLL.GetTotalItems(_cartItems);
            decimal totalAmount = _cartBLL.GetTotalAmount(_cartItems);
            decimal totalDiscount = _cartBLL.GetTotalDiscount(_cartItems);
            decimal finalTotal = totalAmount - totalDiscount;
            // Hiển thị
            lblItemCount.Text = $"{totalItems} sản phẩm";
            lblTempValue.Text = $"{totalAmount:N0}đ";
            lblDiscountValue.Text = totalDiscount > 0 ? $"-{totalDiscount:N0}đ" : "0đ";
            lblTotalValue.Text = $"{finalTotal:N0}đ";  
        }

        /// <summary>
        /// Cấu hình layout form
        /// </summary>

        private void ShowError(string message)
        {
            MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ShowWarning(string message)
        {
            MessageBox.Show(message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void ShowSuccess(string message)
        {
            MessageBox.Show(message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
