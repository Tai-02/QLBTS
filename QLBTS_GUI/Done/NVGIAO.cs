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
    public partial class NVGIAO : Form
    {
        UI_Form ui;
        public NVGIAO()
        {
            ui = new UI_Form();
            InitializeComponent();
        }

        private void NVG_lb_choxacnhan_Click(object sender, EventArgs e)
        {
            ui.OpenChildForm(new ChoXacNhan_NVG(), NVG_pn_tab);
            doimaulb(NVG_lb_choxacnhan);
        }

        private void NVG_lb_dagiao_Click(object sender, EventArgs e)
        {
            ui.OpenChildForm(new LichSu(Khung.MaTK_temp), NVG_pn_tab);
            doimaulb(NVG_lb_dagiao);
        }

        private void NVG_lb_danhan_Click(object sender, EventArgs e)
        {
            ui.OpenChildForm(new DaNhan_NVG(), NVG_pn_tab);
            doimaulb(NVG_lb_danhan);
        }

        private void doimaulb(Control lb)
        {
            NVG_lb_choxacnhan.ForeColor = Color.White;
            NVG_lb_danhan.ForeColor = Color.White;
            NVG_lb_dagiao.ForeColor = Color.White;
            lb.ForeColor = Color.Yellow;
        }

        private void NVGIAO_Load(object sender, EventArgs e)
        {
            ui.OpenChildForm(new ChoXacNhan_NVG(), NVG_pn_tab);
            doimaulb(NVG_lb_choxacnhan);
        }

        private void btn_dangxuat_Click(object sender, EventArgs e)
        {
            ui.OpenChildForm(new DangNhap(Khung.Khung_pn), Khung.Khung_pn);
        }
    }
}
