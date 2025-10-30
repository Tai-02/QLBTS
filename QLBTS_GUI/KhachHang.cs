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
        }

        private void KH_lb_Trangchu_Click(object sender, EventArgs e)
        {
            ui.OpenChildForm(new TrangChu(), KH_pn_tab);
        }

        private void KH_lb_sp_Click(object sender, EventArgs e)
        {

        }

        private void KH_lb_gh_Click(object sender, EventArgs e)
        {

        }

        private void KH_lb_ttd_Click(object sender, EventArgs e)
        {

        }

        private void KH_lb_ttcn_Click(object sender, EventArgs e)
        {

        }

        private void KH_lb_ls_Click(object sender, EventArgs e)
        {

        }
    }
}
