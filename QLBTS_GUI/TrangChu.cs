using MySql.Data.MySqlClient;
using QLBTS_DAL;
using QLBTS_DTO;
using QLBTS_BLL;
using QLBTS_GUI.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBTS_GUI
{
    public partial class TrangChu : Form
    {
        private DanhMucSanPhamBLL sanPhamBLL = new DanhMucSanPhamBLL();

        public TrangChu()
        {
            InitializeComponent();
        }

        private void TrangChu_Load(object sender, EventArgs e)
        {
            LoadBestSellers();
        }

        private void LoadBestSellers()
        {
            List<Panel> panels = new List<Panel> { pnBestSeller1, pnBestSeller2, pnBestSeller3, pnBestSeller4, pnBestSeller5 };
            List<PictureBox> pics = new List<PictureBox> { pcBest1, pcBest2, pcBest3, pcBest4, pcBest5 };
            List<PictureBox> pickm = new List<PictureBox> { guna2CirclePictureBox1, guna2CirclePictureBox2, guna2CirclePictureBox3, guna2CirclePictureBox4, guna2CirclePictureBox5 };

            foreach (var p in panels)
            {
                p.Visible = false;
                p.Controls.Clear();
                p.BorderStyle = BorderStyle.None;
            }

            try
            {
                List<SanPhamDTO> ds = sanPhamBLL.LayDanhSachSanPham("BestSeller");
                for (int i = 0; i < ds.Count && i < panels.Count; i++)
                {
                    SanPhamDTO sp = ds[i];
                    Panel pn = panels[i];

                    // Ảnh sản phẩm
                    PictureBox pic = pics[i];
                    pic.Dock = DockStyle.Fill;
                    pic.SizeMode = PictureBoxSizeMode.StretchImage;
                    pic.Image = null;
                    if (sp.HinhAnh != null && sp.HinhAnh.Length > 0)
                    {
                        using (MemoryStream ms = new MemoryStream(sp.HinhAnh))
                        {
                            pic.Image = Image.FromStream(ms);
                        }
                    }
                    pn.Controls.Add(pic);

                    PictureBox pi = pickm[i];                    
                    pi.SizeMode = PictureBoxSizeMode.StretchImage;
                    pi.Image = Properties.Resources.Bestseller;
                    pic.Controls.Add(pi);
                    pi.BringToFront();
                    
                    // Tên sản phẩm (in đậm)
                    Label lbTen = new Label();
                    lbTen.Text = sp.TenSP;
                    lbTen.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    lbTen.AutoSize = false;
                    lbTen.TextAlign = ContentAlignment.MiddleCenter;
                    lbTen.Dock = DockStyle.Bottom;
                    lbTen.Height = 25;
                    pn.Controls.Add(lbTen);

                    // Giá gốc (den, có gạch ngang)
                    Label lbGiaGoc = new Label();
                    lbGiaGoc.Text = $"{sp.Gia:N0} VNĐ";
                    lbGiaGoc.Font = new Font("Segoe UI", 9, FontStyle.Strikeout);
                    lbGiaGoc.ForeColor = Color.Black;
                    lbGiaGoc.AutoSize = false;
                    lbGiaGoc.TextAlign = ContentAlignment.MiddleCenter;
                    lbGiaGoc.Dock = DockStyle.Bottom;
                    lbGiaGoc.Height = 18;
                    pn.Controls.Add(lbGiaGoc);

                    // Giá khuyến mãi (đỏ, dưới cùng)
                    int giaKM = (int)(Math.Round((sp.Gia - (sp.Gia * sp.KhuyenMai / 100.0)) / 1000.0) * 1000);
                    Label lbGiaKM = new Label();
                    lbGiaKM.Text = $"{giaKM:N0} VNĐ (-{sp.KhuyenMai}%)";
                    lbGiaKM.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    lbGiaKM.ForeColor = Color.Red;
                    lbGiaKM.AutoSize = false;
                    lbGiaKM.TextAlign = ContentAlignment.MiddleCenter;
                    lbGiaKM.Dock = DockStyle.Bottom;
                    lbGiaKM.Height = 22;
                    pn.Controls.Add(lbGiaKM);

                    pn.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách Best Seller: " + ex.Message);
            }
        }

    }
}
