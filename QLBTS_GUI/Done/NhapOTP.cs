using QLBTS_BLL;
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
    public partial class NhapOTP : Form
    {
        private TaiKhoanBLL bll = new TaiKhoanBLL();
        Control ParentPanel;
        private string email_temp;
        public NhapOTP(Control pn, string email)
        {
            InitializeComponent();
            ParentPanel = pn;
            email_temp = email;
        }

        private void OTP_btn_xacnhan_Click(object sender, EventArgs e)
        {
            try
            {
                string otp = OTP_txt_nhap.Text.Trim();
                if (string.IsNullOrEmpty(otp))
                {
                    MessageBox.Show("Vui lòng nhập mã OTP!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool hopLe = bll.KiemTraOTP(email_temp, otp);
                if (hopLe)
                {
                    MessageBox.Show("Xác thực thành công! Tài khoản đã được kích hoạt.",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    UI_Form ui = new UI_Form();
                    ui.OpenChildForm(new DangNhap(ParentPanel), ParentPanel);
                }
                else
                {
                    MessageBox.Show("Mã OTP không đúng hoặc đã hết hạn!",
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OTP_btn_Thoat_Click(object sender, EventArgs e)
        {
            UI_Form ui = new UI_Form();
            ui.OpenChildForm(new DangKi(ParentPanel), ParentPanel);
        }
    }
}
