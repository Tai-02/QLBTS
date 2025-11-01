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
    public partial class Admin : Form
    {
        UI_Form ui;
        public Admin()
        {
            ui = new UI_Form();
            InitializeComponent();
        }


        private void doimaulb(Control lb)
        {
            ADMIN_lb_qltknv.ForeColor = Color.White;
            ADMIN_lb_doanhthu.ForeColor = Color.White;
            ADMIN_lb_hieusuat.ForeColor = Color.White;
            lb.ForeColor = Color.Yellow;
        }

        private void NVGIAO_Load(object sender, EventArgs e)
        {
            ui.OpenChildForm(new QuanLiTaiKhoanNV(), ADMIN_pn_tab);
            doimaulb(ADMIN_lb_qltknv);
        }

        private void ADMIN_lb_qltknv_Click(object sender, EventArgs e)
        {
            ui.OpenChildForm(new QuanLiTaiKhoanNV(), ADMIN_pn_tab);
            doimaulb(ADMIN_lb_qltknv);
        }

        private void ADMIN_lb_doanhthu_Click(object sender, EventArgs e)
        {
            ui.OpenChildForm(new DoanhThu(), ADMIN_pn_tab);
            doimaulb(ADMIN_lb_doanhthu);
        }

        private void ADMIN_lb_hieusuat_Click(object sender, EventArgs e)
        {
            doimaulb(ADMIN_lb_hieusuat);
        }
    }
}
