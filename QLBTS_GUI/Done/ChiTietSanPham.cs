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
            _allSizes = new List<SanPhamDTO>();

            sanpham = sp;
            sanphamBLL = new SanPhamBLL();
            giohangBLL = new GioHangBLL();
            if (sanpham.Size == "M")
            {
                rdoSizeM.Checked = true;
                rdoSizeL.Checked = false;
            }
            else if (sanpham.Size == "L")
            {
                rdoSizeL.Checked = true;
                rdoSizeM.Checked = false;
            }


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
            if (_currentQuantity < sanphamBLL.GetSoLuongTon(sanpham.MaSP))
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
            if (_currentQuantity > sanphamBLL.GetSoLuongTon(sanpham.MaSP))
            {
                MessageBox.Show(
                    $"Chỉ còn {sanpham.SoLuong} sản phẩm!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                _currentQuantity = sanphamBLL.GetSoLuongTon(sanpham.MaSP);

                _currentQuantity = 1;
                txtQuantity.Text = "1";
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
                MessageBox.Show(ex.Message,"Lỗi khi thêm sản phẩm vào giỏ hàng",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }


        private void rdoSizeL_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoSizeL.Checked)
            {
                if (sanphamBLL.GetGiaHienTai(sanpham.MaSP, "L") == 0)  // kiểm tra giá size L
                {
                    MessageBox.Show("Sản phẩm không có size L, không thể chọn.");
                    rdoSizeL.Checked = false;
                    return;
                }

                sanpham.Size = "L";
                rdoSizeM.Checked = false;
            }
        }

        private void rdoSizeM_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoSizeM.Checked)
            {
                if (sanphamBLL.GetGiaHienTai(sanpham.MaSP, "M") == 0)  // kiểm tra giá size M
                {
                    MessageBox.Show("Sản phẩm không có size M, không thể chọn.");
                    rdoSizeM.Checked = false;
                    return;
                }

                sanpham.Size = "M";
                rdoSizeL.Checked = false;
            }
        }


        private void ChiTietSanPham_Load(object sender, EventArgs e)
        {
            if (sanpham == null) return;

            lblProductName.Text = sanpham.TenSP;
            if (!string.IsNullOrEmpty(sanpham.Size))
            {
                lblPrice.Text = sanphamBLL.GetTextGia(sanpham, sanpham.Size);
            }
            else
            {
                lblPrice.Text = sanphamBLL.GetTextGia(sanpham, "");
            }

            if (sanpham.HinhAnh != null && sanpham.HinhAnh.Length > 0)
            {
                productImage.Image = LoadImageFromBytes(sanpham.HinhAnh);
            }
            else
            {
                productImage.Image = null; 
            }
            productImage.BackColor = (productImage.Image == null) ? Color.LightGray : Color.Transparent;

            txtQuantity.Text = _currentQuantity.ToString();
        }

        private Image? LoadImageFromBytes(byte[]? bytes)
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

