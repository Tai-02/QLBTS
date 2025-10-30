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
    public partial class ChiTietSanPham : Form
    {
        UI_Form ui = new UI_Form();
        private SanPhamDTO sanpham;
        private SanPhamBLL sanphamBLL;
        private GioHangBLL giohangBLL;
        private int _maTK;
        private List<SanPhamDTO> _allSizes;
        private int _currentQuantity = 1;

        public ChiTietSanPham(SanPhamDTO sp)
        {
            InitializeComponent();
            _maTK = Khung.MaTK_temp;

            sanpham = sp;
            sanphamBLL = new SanPhamBLL();
            giohangBLL = new GioHangBLL();
            if (sp.Size == "M") rdoSizeM.Checked = true;
            else rdoSizeL.Checked = false;

            txtQuantity.Text = "1";
            txtQuantity.ReadOnly = true;

        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            if (_currentQuantity > 1)
            {
                _currentQuantity--;
                txtQuantity.Text = _currentQuantity.ToString();
            }
        }

        private void btnPlus_Click(object sender, EventArgs e)
        {
            if (_currentQuantity < sanpham.SoLuong)
            {
                _currentQuantity++;
                txtQuantity.Text = _currentQuantity.ToString();
            }
            else
            {
                MessageBox.Show($"Chỉ còn {sanpham.SoLuong} sản phẩm!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            if (sanpham == null) return;

            // Kiểm tra tồn kho
            if (_currentQuantity > sanpham.SoLuong)
            {
                MessageBox.Show(
                    $"Chỉ còn {sanpham.SoLuong} sản phẩm!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            try
            {
                // Thêm sản phẩm vào giỏ
                giohangBLL.ThemSanPhamVaoGio(_maTK, sanpham, _currentQuantity);

                MessageBox.Show(
                    $"Đã thêm {_currentQuantity} {sanpham.TenSP} vào giỏ!",
                    "Thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                // Reset lại số lượng
                _currentQuantity = 1;
                txtQuantity.Text = "1";
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("đã có trong giỏ"))
                    throw new Exception("Sản phẩm này đã có trong giỏ hàng!");

                throw new Exception("Lỗi khi thêm sản phẩm vào giỏ hàng: " + ex.Message);
            }
        }


        private void rdoSizeL_CheckedChanged(object sender, EventArgs e)
        {
            if (sanphamBLL.GetSanPhamCungTenKhacSize(sanpham) != null)
            {
                sanpham = sanphamBLL.GetSanPhamCungTenKhacSize(sanpham);
                rdoSizeL.Checked = true;
                rdoSizeM.Checked = false;
            }
        }

        private void rdoSizeM_CheckedChanged(object sender, EventArgs e)
        {
            if (sanphamBLL.GetSanPhamCungTenKhacSize(sanpham) != null)
            {
                sanpham = sanphamBLL.GetSanPhamCungTenKhacSize(sanpham);
                rdoSizeL.Checked = true;
                rdoSizeM.Checked = false;
            }
        }

        private void ChiTietSanPham_Load(object sender, EventArgs e)
        {
            if (sanpham == null) return;

            lblProductName.Text = sanpham.TenSP;
            lblPrice.Text = sanphamBLL.GetTextGia(sanpham);

            productImage.Image = LoadImageFromBytes(sanpham.HinhAnh);
            productImage.BackColor = (productImage.Image == null) ? Color.LightGray : Color.Transparent;

            txtQuantity.Text = _currentQuantity.ToString();
        }

        private Image LoadImageFromBytes(byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0) return null;

            using (var ms = new MemoryStream(bytes))
            {
                return new Bitmap(Image.FromStream(ms));
            }
        }

        private void btnBuyNow_Click(object sender, EventArgs e)
        {
            if (sanpham == null) return;

            // 🔹 Kiểm tra tồn kho
            if (_currentQuantity > sanpham.SoLuong)
            {
                MessageBox.Show($"Chỉ còn {sanpham.SoLuong} sản phẩm!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 🔹 Thêm sản phẩm vào giỏ
                giohangBLL.ThemSanPhamVaoGio(_maTK, sanpham, _currentQuantity);

                // 🔹 Mở form giỏ hàng
                if (Khung.lvID_temp == 0)
                {
                    ui.OpenChildForm(new GioHang_KH(), Khachhang.KH_pn_tab);
                }
                else
                {
                    ui.OpenChildForm(new GioHang_NVQ(), NVQUAY.NVQ_pn_tab);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

    }
}

