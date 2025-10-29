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
        private List<ProductDetailViewModel> _allSizes;
        private string _selectedSize = "M";

        public ProductDetailForm()
        {
            InitializeComponent();
            _productBLL = new ProductBLL();
            _cartBLL = new CartBLL();

            // Clear default text của Guna2TextBox
            txtQuantity.Text = "1";

            //  Tắt khả năng chỉnh sửa TextBox
            txtQuantity.ReadOnly = true;

            // Setup validation (giữ lại để xử lý khi dùng nút +/-)
            SetupTextBoxValidation();

            // Setup event cho radio button
            rdoSizeM.CheckedChanged += RdoSize_CheckedChanged;
            rdoSizeL.CheckedChanged += RdoSize_CheckedChanged;
        }

        /// <summary>
        /// Load sản phẩm theo MaSP (cho Khách hàng)
        /// </summary>
        public void LoadProduct(int maSP, int maTK)
        {
            _maTK = maTK;

            //  Load tất cả size TRƯỚC
            LoadProductData(maSP);
            LoadAllSizes();

            //  Cập nhật UI dựa trên size hiện tại
            UpdateUIForCurrentProduct();
        }

        /// <summary>
        /// Setup validation cho TextBox số lượng
        /// </summary>
        private void SetupTextBoxValidation()
        {
            //   validate khi text thay đổi (do nút +/-)
            txtQuantity.TextChanged += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtQuantity.Text))
                {
                    txtQuantity.Text = "1";
                    return;
                }

                if (int.TryParse(txtQuantity.Text, out int quantity))
                {
                    if (quantity < 1)
                    {
                        txtQuantity.Text = "1";
                    }
                    else if (_product != null && quantity > _product.SoLuong)
                    {
                        txtQuantity.Text = _product.SoLuong.ToString();
                        ShowWarning($"Chỉ còn {_product.SoLuong} sản phẩm!");
                    }

                    _currentQuantity = int.Parse(txtQuantity.Text);
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

                // Hiển thị thông tin cơ bản
                lblProductName.Text = _product.TenSP;
                lblPrice.Text = _product.TextGia;

                // Load hình ảnh
                LoadProductImage();

                //  Lưu size ban đầu
                _selectedSize = _product.Size;
            }
            catch (Exception ex)
            {
                ShowError($"Lỗi khi tải sản phẩm:\n{ex.Message}\n\nChi tiết:\n{ex.InnerException?.Message}");
            }
        }

        /// <summary>
        /// Load tất cả size của sản phẩm
        /// </summary>
        private void LoadAllSizes()
        {
            try
            {
                _allSizes = _productBLL.GetProductAllSizes(_product.TenSP);

                if (_allSizes != null && _allSizes.Any())
                {
                    var sizeM = _allSizes.FirstOrDefault(x => x.Size == "M");
                    var sizeL = _allSizes.FirstOrDefault(x => x.Size == "L");

                    //  Cập nhật trạng thái và text của radio button
                    if (sizeM != null)
                    {
                        rdoSizeM.Enabled = sizeM.ConHang;
                        rdoSizeM.Text = sizeM.ConHang
                            ? $"M (Còn {sizeM.SoLuong})"
                            : "M (Hết hàng)";
                    }
                    else
                    {
                        rdoSizeM.Enabled = false;
                        rdoSizeM.Text = "M (Không có)";
                    }

                    if (sizeL != null)
                    {
                        rdoSizeL.Enabled = sizeL.ConHang;
                        rdoSizeL.Text = sizeL.ConHang
                            ? $"L (Còn {sizeL.SoLuong})"
                            : "L (Hết hàng)";
                    }
                    else
                    {
                        rdoSizeL.Enabled = false;
                        rdoSizeL.Text = "L (Không có)";
                    }

                    //  Chọn radio button theo size hiện tại
                    if (_product.Size == "M")
                    {
                        rdoSizeM.Checked = true;
                    }
                    else if (_product.Size == "L")
                    {
                        rdoSizeL.Checked = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError($"Lỗi khi tải size: {ex.Message}");
            }
        }

        /// <summary>
        ///  Cập nhật UI dựa trên sản phẩm hiện tại
        /// </summary>
        private void UpdateUIForCurrentProduct()
        {
            if (_product == null) return;

            // Cập nhật giá
            lblPrice.Text = _product.TextGia;

            // Reset số lượng về 1
            _currentQuantity = 1;
            txtQuantity.Text = "1";

            // Cập nhật trạng thái nút
            if (!_product.ConHang)
            {
                

                // Disable cả nút +/-
                btnMinus.Enabled = false;
                btnPlus.Enabled = false;
            }
            else
            {
                

                // Enable nút +/-
                btnMinus.Enabled = true;
                btnPlus.Enabled = true;
            }
        }

        /// <summary>
        /// Load hình ảnh từ BLOB
        /// </summary>
        private void LoadProductImage()
        {
            try
            {
                if (_product?.HinhAnh == null || _product.HinhAnh.Length == 0)
                {
                    productImage.BackColor = Color.LightGray;
                    return;
                }

                // Dispose ảnh cũ trước
                if (productImage.Image != null)
                {
                    var oldImage = productImage.Image;
                    productImage.Image = null;
                    oldImage.Dispose();
                }

                // Clone image ra khỏi stream
                using (var ms = new MemoryStream(_product.HinhAnh))
                {
                    productImage.Image = new Bitmap(Image.FromStream(ms));
                }
            }
            catch (Exception ex)
            {
                productImage.BackColor = Color.LightGray;
                Console.WriteLine($"Lỗi load ảnh: {ex.Message}");
            }
        }

        /// <summary>
        /// Nút giảm số lượng
        /// </summary>
        private void btnMinus_Click(object sender, EventArgs e)
        {
            if (_product == null || !_product.ConHang)
            {
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
            if (_product == null || !_product.ConHang)
            {
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

                if (!_product.ConHang)
                {
                    ShowWarning("Sản phẩm đã hết hàng!");
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
            try 
            {

                if (_product == null)
                {
                    ShowError("Chưa load sản phẩm!");
                    return;
                }

                if (!_product.ConHang) // Kiểm tra sản phẩm hiện tại (theo size đã chọn)
                {
                    ShowWarning("Sản phẩm đã hết hàng!");
                    return;
                }

                if (!int.TryParse(txtQuantity.Text, out int quantity) || quantity < 1)
                {
                    ShowWarning("Số lượng không hợp lệ!");
                    return;
                }

                if (quantity > _product.SoLuong) // Kiểm tra tồn kho của size hiện tại
                {
                    ShowWarning($"Chỉ còn {_product.SoLuong} sản phẩm trong kho!");
                    return;
                }

                // Thực hiện thêm vào giỏ
                bool addToCartSuccess = _cartBLL.AddToCart(_maTK, _product.MaSP, quantity);

                // ===  Kiểm tra kết quả và mở giỏ hàng ===
                if (addToCartSuccess)
                {
                    ShowSuccess($"Đã thêm {quantity} x '{_product.TenSP}' vào giỏ hàng!");

                    // Đóng form chi tiết sản phẩm hiện tại
                    this.Hide();

                    // Tạo và mở CartForm
                    CartForm cartForm = new CartForm(_maTK);
                    cartForm.LoadCartFromDatabase(_maTK); 
                    cartForm.ShowDialog(); 

                    //  Đóng hẳn form chi tiết sau khi xem giỏ hàng
                    this.Close();
                }
                else
                {
                    // Nếu thêm không thành công, chỉ hiện lỗi, không mở giỏ hàng
                    ShowError("Không thể thêm vào giỏ hàng!");
                }
            }
            catch (Exception ex) 
            {
                ShowError($"Lỗi khi xử lý mua ngay: {ex.Message}");
            }
        }

        /// <summary>
        /// Sự kiện khi đổi size
        /// </summary>
        private void RdoSize_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdo = (RadioButton)sender;

            if (!rdo.Checked)
                return;

            // Xác định size được chọn
            _selectedSize = rdo == rdoSizeM ? "M" : "L";

            // Tìm sản phẩm có size tương ứng
            var newProduct = _allSizes?.FirstOrDefault(x => x.Size == _selectedSize);

            if (newProduct != null)
            {
                _product = newProduct;

                //  Gọi hàm tổng hợp để cập nhật toàn bộ UI
                UpdateUIForCurrentProduct();
            }
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