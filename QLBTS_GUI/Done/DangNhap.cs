using MySql.Data.MySqlClient;
using QLBTS_BLL;
using QLBTS_DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBTS_GUI
{
    public partial class DangNhap : Form
    {
        private TaiKhoanBLL bll = new TaiKhoanBLL();
        Control ParentPanel;
        UI_Form ui = new UI_Form();

        public DangNhap(Control pn)
        {
            InitializeComponent();
            ParentPanel = pn;
        }


        private void Dn_btn_dangnhap_Click(object sender, EventArgs e)
        {
            string username = Dn_txt_tendangnhap.Text.Trim();
            string password = Dn_txt_mk.Text.Trim();

            if(username == "")
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập", "Thông báo");
                return;
            }
            if(password == "")
            {
                MessageBox.Show("Vui lòng nhập mật khẩu", "Thông báo");
                return;
            }

            TaiKhoanDTO tk = new TaiKhoanDTO
            {
                TenDangNhap = username,
                MatKhau = password
            };

            bool ok = bll.KiemTraTaiKhoan(tk);

            if (ok)
            {
                MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Khung.lvID_temp = bll.LayLevelID(username);
                if (Khung.lvID_temp == 0)
                {
                    ui.OpenChildForm(new Khachhang(), ParentPanel);
                }
                if (Khung.lvID_temp == 2)
                {
                    ui.OpenChildForm(new NVQUAY(), ParentPanel);
                }
                Khung.MaTK_temp = bll.LayMaTK(username);
            }
            else
            {
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Dn_txt_tendangnhap.Clear();
                Dn_txt_mk.Clear();
                return;
            }        
        }

        private void Dn_btn_Dangkingay_Click(object sender, EventArgs e)
        {
            UI_Form ui = new UI_Form();
            ui.OpenChildForm(new DangKi(ParentPanel), ParentPanel);
        }
    }
}
