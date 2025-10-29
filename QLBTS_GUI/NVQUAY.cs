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
            ui.OpenChildForm(new QuanLiSanPham(), NVQ_pn_tab);
        }

        private void NVQ_lb_sp_Click(object sender, EventArgs e)
        {
            //ui.OpenChildForm
        }

        private void NVQ_lb_gh_Click(object sender, EventArgs e)
        {

        }

        private void NVQ_lb_qlsp_Click(object sender, EventArgs e)
        {
            ui.OpenChildForm(new QuanLiSanPham(), NVQ_pn_tab);
        }

        private void NVQ_lb_qldh_Click(object sender, EventArgs e)
        {

        }

        private void NVQ_lb_ls_Click(object sender, EventArgs e)
        {
            ui.OpenChildForm(new LichSuNhanDon(), NVQ_pn_tab);
        }
    }
}
