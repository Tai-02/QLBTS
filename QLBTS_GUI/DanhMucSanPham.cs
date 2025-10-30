using MySql.Data.MySqlClient;
using QLBTS_BLL;
using QLBTS_DAL;
using QLBTS_DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBTS_GUI
{
    public partial class DanhMucSanPham : Form
    {
        private DanhMucSanPhamBLL bll = new DanhMucSanPhamBLL();
        List<SanPhamDTO> list = new List<SanPhamDTO>();
        private QuanLiSanPhamBLL qlspBLL = new QuanLiSanPhamBLL();

        public DanhMucSanPham()
        {
            InitializeComponent();
        }


        private Image ByteArrayToImage(byte[] byteArray)
        {
            if (byteArray == null || byteArray.Length == 0) return null;
            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                Image image = Image.FromStream(ms);
                return image;
            }
        }

        private byte[] ImageToByteArray(Image image)
        {
            if (image == null) return null;
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, image.RawFormat);
                return ms.ToArray();
            }
        }

        private void chkBestseller_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBestseller.Checked)
            {
                chkMonmoi.Checked = false;
                chkTrasua.Checked = false;
                chkTopping.Checked = false;
                cbb_loai.SelectedIndex = -1;

                list.Clear();
                list = bll.LayDanhSachSanPham("BestSeller");
                LoadSanPhamList(list);
            }
        }

        private void chkMonmoi_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMonmoi.Checked)
            {
                chkBestseller.Checked = false;
                chkTrasua.Checked = false;
                chkTopping.Checked = false;
                cbb_loai.SelectedIndex = -1;

                list.Clear();
                list = bll.LayDanhSachSanPham("MonMoi");
                LoadSanPhamList(list);
            }
        }

        private void chkTraSua_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTrasua.Checked)
            {
                chkMonmoi.Checked = false;
                chkBestseller.Checked = false;
                chkTopping.Checked = false;
                cbb_loai.SelectedIndex = -1;

                list.Clear();
                list = bll.LayDanhSachSanPham("TraSua");
                LoadSanPhamList(list);
            }
        }

        private void chkTopping_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTopping.Checked)
            {
                chkMonmoi.Checked = false;
                chkBestseller.Checked = false;
                chkTrasua.Checked = false;
                cbb_loai.SelectedIndex = -1;

                list.Clear();
                list = bll.LayDanhSachSanPham("Topping");
                LoadSanPhamList(list);
            }
        }

        private void cbb_loai_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbb_loai.SelectedIndex != -1)
            {
                chkBestseller.Checked = false;
                chkMonmoi.Checked = false;
                chkTrasua.Checked = false;
                chkTopping.Checked = false;

                list.Clear();
                list = bll.LayDanhSachSanPham(cbb_loai.Text.ToString());
                LoadSanPhamList(list);
            }
        }

        private void DanhMucSanPham_Load(object sender, EventArgs e)
        {
            chkBestseller.Checked = true;
            Loadcbb();
        }

        private void Loadcbb()
        {
            cbb_loai.Items.Clear();

            var loaiSPList = qlspBLL.GetDistinctLoaiSP();
            foreach (var loai in loaiSPList)
            {
                cbb_loai.Items.Add(loai);
            }
        }

        private Panel TaoPanelSanPham(SanPhamDTO sp)
        {
            Image hinhAnh = sp.HinhAnh != null && sp.HinhAnh.Length > 0
                ? ByteArrayToImage(sp.HinhAnh)
                : Properties.Resources.icons8_camera_100;

            Panel productPanel = new Panel
            {
                Size = new Size(220, 220),
                BackColor = Color.White,
                Margin = new Padding(10),
                Cursor = Cursors.Hand
            };

            PictureBox picImage = new PictureBox
            {
                Image = hinhAnh,
                Size = new Size(150, 120),
                Location = new Point(30, 10),
                SizeMode = PictureBoxSizeMode.Zoom
            };

            PictureBox picBestSeller = new PictureBox
            {
                Size = new Size(38, 38),
                Location = new Point(productPanel.Width - 48, 10),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.Transparent,
                Visible = sp.KhuyenMai > 0
            };

            Label lblName = new Label
            {
                Text = sp.TenSP,
                Font = new Font("Arial", 10, FontStyle.Bold),
                Location = new Point(20, 140),
                Size = new Size(160, 40),
                TextAlign = ContentAlignment.TopCenter
            };

            Label lblPrice = new Label
            {
                Text = $"Giá: {sp.Gia:N0}đ",
                Font = new Font("Arial", 9, FontStyle.Regular),
                ForeColor = Color.Red,
                Location = new Point(60, 185),
                AutoSize = true
            };

            productPanel.Controls.Add(picImage);
            productPanel.Controls.Add(lblName);
            productPanel.Controls.Add(lblPrice);
            productPanel.Controls.Add(picBestSeller);
            picBestSeller.BringToFront();

            // Gắn sự kiện click cho tất cả control
            productPanel.Click += ProductPanel_Click;
            picImage.Click += ProductPanel_Click;
            picBestSeller.Click += ProductPanel_Click;
            lblName.Click += ProductPanel_Click;
            lblPrice.Click += ProductPanel_Click;

            return productPanel;
        }

        private void LoadSanPhamList(List<SanPhamDTO> danhSachSanPham)
        {
            flpSanPham.Controls.Clear();
            flpSanPham.SuspendLayout();

            if (danhSachSanPham == null || danhSachSanPham.Count == 0)
            {
                Label lblNotFound = new Label
                {
                    Text = "Không tìm thấy sản phẩm",
                    Font = new Font("Arial", 16, FontStyle.Italic),
                    ForeColor = Color.Gray,
                    Size = new Size(flpSanPham.ClientSize.Width - 20, 50),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Margin = new Padding(50)
                };
                flpSanPham.Controls.Add(lblNotFound);
            }
            else
            {
                foreach (var sp in danhSachSanPham)
                {
                    Panel panel = TaoPanelSanPham(sp);
                    flpSanPham.Controls.Add(panel);
                }
            }

            flpSanPham.ResumeLayout(true);
        }


        // Hàm xử lý sự kiện Click (để highlight)
        private void ProductPanel_Click(object sender, EventArgs e)
        {
            Control clickedControl = sender as Control;
            Panel selectedPanel = null;

            if (clickedControl is Panel)
            {
                selectedPanel = (Panel)clickedControl;
            }
            else
            {
                selectedPanel = clickedControl.Parent as Panel;
            }

            if (selectedPanel == null) return;

            foreach (Control ctrl in flpSanPham.Controls)
            {
                if (ctrl is Panel p)
                {
                    p.BackColor = Color.White;
                    p.Padding = new Padding(0);
                }
            }

            selectedPanel.BackColor = Color.DeepSkyBlue;
            selectedPanel.Padding = new Padding(3);
        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTimkiem.Text))
            {
                return;
            }
            else
            {
                chkBestseller.Checked = false;
                chkMonmoi.Checked = false;
                chkTrasua.Checked = false;
                chkTopping.Checked = false;
                cbb_loai.SelectedIndex = -1;
                list = bll.TimKiemSanPham(txtTimkiem.Text.Trim().ToString());
                LoadSanPhamList(list);
            }
        }
    }
}
