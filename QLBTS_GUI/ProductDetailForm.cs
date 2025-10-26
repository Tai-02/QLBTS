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
    public partial class ProductDetailForm : Form
    {
        private ProductDetailViewModel _product;
        private ProductBLL _productBLL;
        private CartBLL _cartBLL;
        private int _currentQuantity = 1;
        private int _maTK;

        public ProductDetailForm()
        {
            InitializeComponent();
            _productBLL = new ProductBLL();
            _cartBLL = new CartBLL();

            // Clear default text của Guna2TextBox
            txtQuantity.Text = "1";

            // Setup validation
            SetupTextBoxValidation();
        }

        /// <summary>
        /// Load sản phẩm theo MaSP (cho Khách hàng)
        /// </summary>
        public void LoadProduct(int maSP, int maTK)
        {
            _maTK = maTK;
            LoadProductData(maSP);
        }

        /// <summary>
        /// Setup validation cho TextBox số lượng
        /// </summary>
        private void SetupTextBoxValidation()
        {
            // Chỉ cho phép nhập số
            txtQuantity.KeyPress += (s, e) =>
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            };

            // Validate khi thay đổi text
            txtQuantity.TextChanged += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtQuantity.Text))
                {
                    txtQuantity.Text = "1";
                    txtQuantity.SelectionStart = txtQuantity.Text.Length;
                    return;
                }

                if (int.TryParse(txtQuantity.Text, out int quantity))
                {
                    if (quantity < 1)
                    {
                        txtQuantity.Text = "1";
                        txtQuantity.SelectionStart = txtQuantity.Text.Length;
                    }
                    else if (_product != null && quantity > _product.SoLuong)
                    {
                        txtQuantity.Text = _product.SoLuong.ToString();
                        txtQuantity.SelectionStart = txtQuantity.Text.Length;
                        ShowWarning($"Chỉ còn {_product.SoLuong} sản phẩm!");
                    }

                    _currentQuantity = int.Parse(txtQuantity.Text);
                }
            };

            // Validate khi mất focus
            txtQuantity.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtQuantity.Text))
                {
                    txtQuantity.Text = "1";
                    _currentQuantity = 1;
                }
            };
        }

        /// <summary>
        /// Load dữ liệu sản phẩm
        /// </summary>
        private void LoadProductData(int maSP)
        {
            try
            {
                _product = _productBLL.GetProductDetail(maSP);

                if (_product == null)
                {
                    ShowError("Không tìm thấy sản phẩm!");
                    this.Close();
                    return;
                }

                // Hiển thị thông tin
                lblProductName.Text = _product.TenSP;
                lblPrice.Text = _product.TextGia;

                // Load hình ảnh
                LoadProductImage();

                // Set size radio button theo sản phẩm
                if (_product.Size == "M")
                {
                    rdoSizeM.Checked = true;
                }
                else if (_product.Size == "L")
                {
                    rdoSizeL.Checked = true;
                }

                // Kiểm tra còn hàng
                if (!_product.ConHang)
                {
                    btnAddToCart.Enabled = false;
                    btnBuyNow.Enabled = false;
                    btnAddToCart.Text = "HẾT HÀNG";
                    btnBuyNow.Text = "HẾT HÀNG";
                    btnAddToCart.FillColor = Color.Gray;
                    btnBuyNow.FillColor = Color.Gray;
                }
                else
                {
                    btnAddToCart.Enabled = true;
                    btnBuyNow.Enabled = true;
                    btnAddToCart.Text = "THÊM VÀO GIỎ";
                    btnBuyNow.Text = "MUA NGAY";
                    btnAddToCart.FillColor = Color.Red;
                    btnBuyNow.FillColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                ShowError($"Lỗi khi tải sản phẩm:\n{ex.Message}\n\nChi tiết:\n{ex.InnerException?.Message}");
            }
        }

        /// <summary>
        /// Load hình ảnh từ BLOB
        /// </summary>
        private void LoadProductImage()
        {
            try
            {
                if (_product == null)
                {
                    productImage.BackColor = Color.LightGray;
                    return;
                }

                if (_product.HinhAnh != null && _product.HinhAnh.Length > 0)
                {
                    using (var ms = new MemoryStream(_product.HinhAnh))
                    {
                        if (productImage.Image != null)
                        {
                            productImage.Image.Dispose();
                            productImage.Image = null;
                        }

                        productImage.Image = Image.FromStream(ms);
                    }
                }
                else
                {
                    productImage.BackColor = Color.LightGray;
                    productImage.Image = null;
                }
            }
            catch (Exception ex)
            {
                productImage.BackColor = Color.LightGray;
                productImage.Image = null;
                Console.WriteLine($"Lỗi load ảnh: {ex.Message}");
            }
        }

        /// <summary>
        /// Nút giảm số lượng
        /// </summary>
        private void btnMinus_Click(object sender, EventArgs e)
        {
            if (_product == null)
            {
                ShowError("Chưa load sản phẩm!");
                return;
            }

            if (_currentQuantity > 1)
            {
                _currentQuantity--;
                txtQuantity.Text = _currentQuantity.ToString();
            }
        }

        /// <summary>
        /// Nút tăng số lượng
        /// </summary>
        private void btnPlus_Click(object sender, EventArgs e)
        {
            if (_product == null)
            {
                ShowError("Chưa load sản phẩm!");
                return;
            }

            if (_currentQuantity < _product.SoLuong)
            {
                _currentQuantity++;
                txtQuantity.Text = _currentQuantity.ToString();
            }
            else
            {
                ShowWarning($"Chỉ còn {_product.SoLuong} sản phẩm trong kho!");
            }
        }

        /// <summary>
        /// Nút THÊM VÀO GIỎ
        /// </summary>
        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            try
            {
                if (_product == null)
                {
                    ShowError("Chưa load sản phẩm!");
                    return;
                }

                // Validate số lượng
                if (!int.TryParse(txtQuantity.Text, out int quantity) || quantity < 1)
                {
                    ShowWarning("Số lượng không hợp lệ!");
                    return;
                }

                if (quantity > _product.SoLuong)
                {
                    ShowWarning($"Chỉ còn {_product.SoLuong} sản phẩm trong kho!");
                    return;
                }

                // Thêm vào giỏ
                bool success = _cartBLL.AddToCart(_maTK, _product.MaSP, quantity);

                if (success)
                {
                    ShowSuccess($"Đã thêm {quantity} sản phẩm vào giỏ hàng!");
                    _currentQuantity = 1;
                    txtQuantity.Text = "1";
                }
                else
                {
                    ShowError("Không thể thêm vào giỏ hàng!");
                }
            }
            catch (Exception ex)
            {
                ShowError($"Lỗi: {ex.Message}");
            }
        }

        /// <summary>
        /// Nút MUA NGAY
        /// </summary>
        private void btnBuyNow_Click(object sender, EventArgs e)
        {
            btnAddToCart_Click(sender, e);
            MessageBox.Show(
                "Chức năng 'Mua ngay' sẽ chuyển sang trang thanh toán.\n\n" +
                "Hiện tại chưa được triển khai.",
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        // ===== HELPER METHODS =====

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


