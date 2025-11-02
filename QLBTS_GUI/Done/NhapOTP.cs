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
        int lenh;
        UI_Form ui = new UI_Form();

        public NhapOTP(Control pn, string email, int _lenh)
        {
            InitializeComponent();
            ParentPanel = pn;
            email_temp = email;
            lenh = _lenh;
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

                bool hopLe = bll.KiemTraOTP(email_temp, otp, lenh);
                if (hopLe)
                {
                    if (lenh == 0)
                    {
                        MessageBox.Show("Xác thực thành công! Tài khoản đã được kích hoạt.",
                            "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ui.OpenChildForm(new DangNhap(ParentPanel), ParentPanel);
                    }
                    else if (lenh == 1) 
                    {
                        // Hiển thị form nhập tên đăng nhập + mật khẩu mới
                        string[] loginInfo = ShowLoginForm(
                            "Nhập tên đăng nhập và mật khẩu mới",
                            "Tên đăng nhập:",
                            "Mật khẩu mới:");

                        if (loginInfo != null)
                        {
                            string tenDangNhapMoi = loginInfo[0];
                            string matKhauMoi = loginInfo[1];

                            try
                            {
                                // Gọi BLL để cập nhật
                                bool capNhat = bll.QuenThongTin(email_temp, tenDangNhapMoi, matKhauMoi);

                                if (capNhat)
                                {
                                    MessageBox.Show("Cập nhật thông tin thành công! Vui lòng đăng nhập lại.",
                                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    ui.OpenChildForm(new DangNhap(ParentPanel), ParentPanel);
                                }
                                else
                                {
                                    MessageBox.Show("Cập nhật thất bại!", "Lỗi",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Lỗi",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
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

        public string[] ShowLoginForm(string title, string labelUser, string labelPass)
        {
            string[] result = null;

            Form form = new Form();
            form.Text = title;
            form.StartPosition = FormStartPosition.CenterParent;
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.Width = 320;
            form.Height = 200;

            // Label và TextBox cho tên đăng nhập
            Label lblUser = new Label() { Left = 10, Top = 10, Text = labelUser, AutoSize = true };
            TextBox txtUser = new TextBox() { Left = 10, Top = 35, Width = 280 };

            // Label và TextBox cho mật khẩu
            Label lblPass = new Label() { Left = 10, Top = 70, Text = labelPass, AutoSize = true };
            TextBox txtPass = new TextBox() { Left = 10, Top = 95, Width = 280, UseSystemPasswordChar = true };

            // Nút OK và Cancel
            Button btnOK = new Button() { Text = "OK", Left = 50, Width = 80, Top = 130, DialogResult = DialogResult.OK };
            Button btnCancel = new Button() { Text = "Cancel", Left = 150, Width = 80, Top = 130, DialogResult = DialogResult.Cancel };

            form.Controls.Add(lblUser);
            form.Controls.Add(txtUser);
            form.Controls.Add(lblPass);
            form.Controls.Add(txtPass);
            form.Controls.Add(btnOK);
            form.Controls.Add(btnCancel);

            form.AcceptButton = btnOK;
            form.CancelButton = btnCancel;

            if (form.ShowDialog() == DialogResult.OK)
            {
                result = new string[] { txtUser.Text.Trim(), txtPass.Text.Trim() };
            }

            form.Dispose();
            return result;
        }

    }
}
