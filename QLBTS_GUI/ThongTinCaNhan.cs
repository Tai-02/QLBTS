using QLBTS_DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLBTS_DTO;
using Microsoft.EntityFrameworkCore;

namespace QLBTS_GUI
{
    public partial class ThongTinCaNhan : Form
    {

        private int maTK = 1; // Mã tài khoản ví dụ
        private QlbtsContext db = new QlbtsContext();
        private KhachHangDTO khachHang;

        public ThongTinCaNhan()
        {
            InitializeComponent();
        }

        private void ThongTinCaNhan_Load(object sender, EventArgs e)
        {
            LoadThongTin();

            // Ảnh đại diện
            TTCN_pc_Anhdaidien.SizeMode = PictureBoxSizeMode.Zoom;
            TTCN_pc_Anhdaidien.FillColor = Color.White;



            // Icon máy ảnh
            TTCN_pcCamera.Parent = TTCN_pc_Anhdaidien; // Đặt icon nằm chồng lên ảnh
            TTCN_pcCamera.BackColor = Color.Transparent;
            TTCN_pcCamera.Image = Properties.Resources.icons8_camera_100; // icon máy ảnh
            TTCN_pcCamera.Size = new Size(130, 130);
            TTCN_pcCamera.SizeMode = PictureBoxSizeMode.Zoom;
            // Căn icon ở chính giữa ảnh đại diện
            TTCN_pcCamera.Location = new Point(
                (TTCN_pc_Anhdaidien.Width - TTCN_pcCamera.Width) / 2,
                (TTCN_pc_Anhdaidien.Height - TTCN_pcCamera.Height) / 2
            );
            TTCN_pc_Anhdaidien.Cursor = Cursors.Hand;


            //Hien icon
            // Họ tên
            TTCN_pc_iconUser.Image = Properties.Resources.icons8_user_32;
            TTCN_pc_iconUser.SizeMode = PictureBoxSizeMode.Zoom;
            TTCN_pc_iconUser.BackColor = Color.Transparent;
            // Email
            TTCN_pc_iconEmail.Image = Properties.Resources.email;
            TTCN_pc_iconEmail.SizeMode = PictureBoxSizeMode.Zoom;
            TTCN_pc_iconEmail.BackColor = Color.Transparent;
            // Số điện thoại
            TTCN_pc_iconSDT.Image = Properties.Resources.z7136937637719_6cff850f2bdf54ef43e237c3110059461;
            TTCN_pc_iconSDT.SizeMode = PictureBoxSizeMode.Zoom;
            TTCN_pc_iconSDT.BackColor = Color.Transparent;
            // Địa chỉ
            TTCN_pc_iconDiachi.Image = Properties.Resources.location;
            TTCN_pc_iconDiachi.SizeMode = PictureBoxSizeMode.Zoom;
            TTCN_pc_iconDiachi.BackColor = Color.Transparent;

            // Tạo label họ tên
            Label TTCN_lbl_HoTen = new Label();
            Label TTCN_lbl_VaiTro = new Label();
            this.Controls.Add(TTCN_lbl_HoTen);
            this.Controls.Add(TTCN_lbl_VaiTro);

            // Thiết lập
            TTCN_lbl_HoTen.Parent = TTCN_pnAnhdaidien; // Đặt label ten nằm chồng lên ảnh

            TTCN_lbl_HoTen.Text = khachHang?.HoTen ?? "Nguyễn Văn A";
            TTCN_lbl_HoTen.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            TTCN_lbl_HoTen.ForeColor = Color.Black;
            TTCN_lbl_HoTen.TextAlign = ContentAlignment.MiddleCenter;
            TTCN_lbl_HoTen.AutoSize = false;
            TTCN_lbl_HoTen.Size = new Size(TTCN_pc_Anhdaidien.Width, 40);
            TTCN_lbl_HoTen.Location = new Point(
                TTCN_pc_Anhdaidien.Left,
                TTCN_pc_Anhdaidien.Bottom + 40
            );

            // Vai trò
            TTCN_lbl_VaiTro.Parent = TTCN_pnAnhdaidien; // Đặt label vai tro nằm chồng lên ảnh

            TTCN_lbl_VaiTro.Text = khachHang?.TaiKhoan?.VaiTro ?? "Vai trò";
            TTCN_lbl_VaiTro.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            TTCN_lbl_VaiTro.ForeColor = Color.Gray;
            TTCN_lbl_VaiTro.TextAlign = ContentAlignment.MiddleCenter;
            TTCN_lbl_VaiTro.AutoSize = false;
            TTCN_lbl_VaiTro.Size = new Size(TTCN_pc_Anhdaidien.Width, 30);
            TTCN_lbl_VaiTro.Location = new Point(
                TTCN_pc_Anhdaidien.Left,
                TTCN_lbl_HoTen.Bottom + 30
            );

        }
        private void LoadThongTin()
        {
            khachHang = db.KhachHang
            .Include(kh => kh.TaiKhoan)
            .FirstOrDefault(kh => kh.MaTK == maTK);

            if (khachHang != null)
            {
                TTCN_txt_Tenuser.Text = khachHang.HoTen;
                TTCN_txt_Email.Text = khachHang.TaiKhoan.Email;
                TTCN_txt_Sodienthoai.Text = khachHang.SDT;
                TTCN_txt_DiaChi.Text = khachHang.DiaChi;

                // Hiển thị ảnh
                //if (khachHang.AnhKH != null && khachHang.AnhKH.Length > 0)
                //{
                //    using (var ms = new MemoryStream(khachHang.AnhKH))
                //    {
                //        picAnh.Image = Image.FromStream(ms);
                //    }
                //}
                //else
                //{
                //    picAnh.Image = Properties.Resources.default_avatar; // ảnh mặc định
                //}
            }
            else
            {
                MessageBox.Show("Không tìm thấy thông tin khách hàng!", "Thông báo");
            }
        }

        private void TTCN_btnCapnhat_Click(object sender, EventArgs e)
        {
            if (khachHang == null) return;

            khachHang.HoTen = TTCN_txt_Tenuser.Text;
            khachHang.SDT = TTCN_txt_Sodienthoai.Text;
            khachHang.DiaChi = TTCN_txt_DiaChi.Text;
            khachHang.TaiKhoan.Email = TTCN_txt_Email.Text;

            // Cập nhật ảnh (nếu có)
            //if (picAnh.Image != null)
            //{
            //    using (var ms = new MemoryStream())
            //    {
            //        picAnh.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            //        khachHang.AnhKH = ms.ToArray();
            //    }
            //}
            db.SaveChanges();
            MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo");
        }

        private void TTCN_btnReset_Click(object sender, EventArgs e)
        {
            //// Xóa toàn bộ nội dung các textbox
            //TTCN_txt_Tenuser.Clear();
            //TTCN_txt_Email.Clear();
            //TTCN_txt_Sodienthoai.Clear();
            //TTCN_txt_DiaChi.Clear();

            //TTCN_pcCamera.Image = Properties.Resources.icons8_camera_100;

            //TTCN_txt_Tenuser.PlaceholderText = "Nhập họ tên";
            //TTCN_txt_Email.PlaceholderText = "Nhập email";
            //TTCN_txt_Sodienthoai.PlaceholderText = "Nhập số điện thoại";
            //TTCN_txt_DiaChi.PlaceholderText = "Nhập địa chỉ";

            //MessageBox.Show("Đã đặt lại thông tin!", "Thông báo");
        }

        private void TTCN_pcCamera_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Chọn ảnh đại diện";
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    TTCN_pcCamera.Image = Image.FromFile(ofd.FileName);
                    MessageBox.Show("Đã cập nhật ảnh đại diện!", "Thông báo");
                }
            }
        }
    }
}
