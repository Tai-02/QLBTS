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
    public partial class Khung : Form
    {
        public static int lvID_temp = -1;
        public static int MaTK_temp;
        UI_Form ui = new UI_Form();
        public Khung(int _LvID)
        {
            InitializeComponent();
            lvID_temp = _LvID;
        }

        private void Khung_Load(object sender, EventArgs e)
        {
            ui.OpenChildForm(new DangNhap(Khung_pn), Khung_pn);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            TaiKhoanBLL tkBLL = new TaiKhoanBLL();
            TaiKhoanDTO tk = tkBLL.LayThongTinTaiKhoanTheoMaTK(MaTK_temp);
            if (!tkBLL.kttk(tk))
            {
                ui.OpenChildForm(new DangNhap(Khung_pn), Khung_pn);
            }
        }
    }
}
