using Microsoft.VisualBasic.ApplicationServices;
using MySql.Data.MySqlClient;
using QLBTS_DTO;
using QLBTS_BBL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using QLBTS_BLL;

namespace QLBTS_GUI
{
    public partial class ThongTinCaNhan : Form
    {
        private TaiKhoanBLL taikhoanBLL;

        public ThongTinCaNhan()
        {
            InitializeComponent();
            taikhoanBLL = new TaiKhoanBLL();
        }

        private void ThongTinCaNhan_Load(object sender, EventArgs e)
        {
            TaiKhoanDTO tk = taikhoanBLL.LayThongTinTaiKhoanTheoMaTK(Khung.MaTK_temp);

            TTCN_txt_Tenuser.Text = tk.HoTen;
            TTCN_txt_DiaChi.Text = tk.DiaChi;
            TTCN_txt_Email.Text = tk.Email;
            TTCN_txt_Sodienthoai.Text = tk.SDT;

            if (tk.Anh != null && tk.Anh.Length > 0)
    {
                using (MemoryStream ms = new MemoryStream(tk.Anh))
                {
                    TTCN_pc_Anhdaidien.Image = Image.FromStream(ms);
                }
            }
            else
            {
                TTCN_pc_Anhdaidien.Image = null; 
            }
        }

        private void TTCN_btnCapnhat_Click(object sender, EventArgs e)
        {
            try
            {
                TaiKhoanDTO tk = new TaiKhoanDTO
                {
                    MaTK = Khung.MaTK_temp,
                    HoTen = TTCN_txt_Tenuser.Text.Trim().ToString(),                    
                    Email = TTCN_txt_Email.Text.Trim().ToString(),
                    SDT = TTCN_txt_Sodienthoai.Text.Trim().ToString(),
                    DiaChi = TTCN_txt_DiaChi.Text.Trim(),
                    Anh = GetImageFromPictureBox(TTCN_pc_Anhdaidien)
                };
                if (taikhoanBLL.CapNhatThongTin(tk))
                {
                    MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);                    
                    ThongTinCaNhan_Load(sender, e);
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật: " + ex.Message);
            }
        }

        private void TTCN_btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                ThongTinCaNhan_Load(sender, e);
                MessageBox.Show("Đã hoàn tác các thay đổi chưa lưu.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải lại dữ liệu: " + ex.Message);
            }
        }

        private void TTCN_pc_Anhdaidien_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    Image tempImage = new Bitmap(ofd.FileName);
                    TTCN_pc_Anhdaidien.Image = tempImage;
                }
            }
        }

        private byte[] GetImageFromPictureBox(PictureBox pb)
        {
            if (pb.Image == null) return null;

            using (var ms = new System.IO.MemoryStream())
            {
                // Lưu ảnh từ PictureBox sang MemoryStream dưới dạng PNG (hoặc JPEG)
                pb.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }
    }
}
