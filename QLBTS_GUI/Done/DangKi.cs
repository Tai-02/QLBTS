using QLBTS_BLL;
using QLBTS_DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBTS_GUI
{
    public partial class DangKi : Form
    {
        private TaiKhoanBLL bll = new TaiKhoanBLL();
        Control ParentPanel;

        public DangKi(Control pn)
        {
            InitializeComponent();
            ParentPanel = pn;
        }

        private void Dk_btn_Thoat_Click(object sender, EventArgs e)
        {
            UI_Form ui = new UI_Form();
            ui.OpenChildForm(new DangNhap(ParentPanel), ParentPanel);
        }

        private void Dk_btn_dangki_Click(object sender, EventArgs e)
        {
            try
            {
                string username = Dk_txt_tendangki.Text.Trim();
                string email = Dk_txt_email.Text.Trim();
                string password = Dk_txt_mk.Text.Trim();

                if (password != Dk_txt_remk.Text.Trim())
                {
                    MessageBox.Show("Mật khẩu nhập lại không khớp", "Thông báo");
                    return;
                }

                if (!email.Contains("@"))
                {
                    MessageBox.Show("Vui lòng nhập email hợp lệ", "Thông báo");
                    return;
                }
                

                TaiKhoanDTO tk = new TaiKhoanDTO
                {
                    TenDangNhap = username,
                    MatKhau = password,
                    Email = email,
                };
                Random rd = new Random();
                tk.Otp = rd.Next(0, 999999).ToString();
                tk.VaiTro = "KhachHang";
                tk.Active = false;
                tk.NgayTao = DateTime.Now;
                tk.LevelID = 0;

                if (bll.DangKy(tk))
                {
                    MessageBox.Show("Đăng ký thành công! Vui lòng kiểm tra email để nhận mã OTP.",
                                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    UI_Form ui = new UI_Form();
                    ui.OpenChildForm(new NhapOTP(ParentPanel, email), ParentPanel);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
    }
}
