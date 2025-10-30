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
    public partial class NVQUAY : Form
    {
        UI_Form ui = new UI_Form();
        public NVQUAY()
        {
            InitializeComponent();
        }

        private void NVQUAY_Load(object sender, EventArgs e)
        {

            NVQ_lb_sp_Click(sender, e);
        }

        private void NVQ_lb_sp_Click(object sender, EventArgs e)
        {
            doimaulb(NVQ_lb_sp);
            ui.OpenChildForm(new DanhMucSanPham(), NVQ_pn_tab);
        }

        private void NVQ_lb_gh_Click(object sender, EventArgs e)
        {
            doimaulb(NVQ_lb_gh);
            ui.OpenChildForm(new GioHang_NVQ(), NVQ_pn_tab);
        }

        private void NVQ_lb_qlsp_Click(object sender, EventArgs e)
        {
            doimaulb(NVQ_lb_qlsp);
            ui.OpenChildForm(new QuanLiSanPham(), NVQ_pn_tab);
        }

        private void NVQ_lb_qldh_Click(object sender, EventArgs e)
        {
            doimaulb(NVQ_lb_qldh);
        }

        private void NVQ_lb_ls_Click(object sender, EventArgs e)
        {
            ui.OpenChildForm(new LichSu(Khung.MaTK_temp), NVQ_pn_tab);
            doimaulb(NVQ_lb_ls);
        }

        private void doimaulb(Control lb)
        {
            NVQ_lb_gh.ForeColor = Color.White;
            NVQ_lb_ls.ForeColor = Color.White;
            NVQ_lb_qldh.ForeColor = Color.White;
            NVQ_lb_qlsp.ForeColor = Color.White;
            NVQ_lb_sp.ForeColor = Color.White;
            lb.ForeColor = Color.Yellow;
        }
    }
}
