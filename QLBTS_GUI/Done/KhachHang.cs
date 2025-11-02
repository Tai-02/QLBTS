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
    public partial class Khachhang : Form
    {
        UI_Form ui = new UI_Form();
        public Khachhang()
        {
            InitializeComponent();
        }

        private void Khachhang_Load(object sender, EventArgs e)
        {
            ui.OpenChildForm(new TrangChu(), KH_pn_tab);
            doimaulb(KH_lb_Trangchu);
        }

        private void KH_lb_Trangchu_Click(object sender, EventArgs e)
        {
            ui.OpenChildForm(new TrangChu(), KH_pn_tab);
            doimaulb(KH_lb_Trangchu);
        }

        private void KH_lb_sp_Click(object sender, EventArgs e)
        {
            ui.OpenChildForm(new DanhMucSanPham(), KH_pn_tab);
            doimaulb(KH_lb_sp);
        }

        private void KH_lb_gh_Click(object sender, EventArgs e)
        {
            ui.OpenChildForm(new GioHang_KH(), KH_pn_tab);
            doimaulb(KH_lb_gh);
        }

        private void KH_lb_ttd_Click(object sender, EventArgs e)
        {
            ui.OpenChildForm(new TrangThaiDonHang_KH(), KH_pn_tab);
            doimaulb(KH_lb_ttd);
        }

        private void KH_lb_ttcn_Click(object sender, EventArgs e)
        {
            ui.OpenChildForm(new ThongTinCaNhan(), KH_pn_tab);
            doimaulb(KH_lb_ttcn);
        }

        private void KH_lb_ls_Click(object sender, EventArgs e)
        {
            ui.OpenChildForm(new LichSu(Khung.MaTK_temp), KH_pn_tab);
            doimaulb(KH_lb_ls);
        }

        private void doimaulb(Control lb)
        {
            KH_lb_Trangchu.ForeColor = Color.White;
            KH_lb_sp.ForeColor = Color.White;
            KH_lb_gh.ForeColor = Color.White;
            KH_lb_ttd.ForeColor = Color.White;
            KH_lb_ttcn.ForeColor = Color.White;
            KH_lb_ls.ForeColor = Color.White;
            lb.ForeColor = Color.Yellow;
        }

        private void btn_dangxuat_Click(object sender, EventArgs e)
        {
            ui.OpenChildForm(new DangNhap(Khung.Khung_pn), Khung.Khung_pn);
        }
    }
}
