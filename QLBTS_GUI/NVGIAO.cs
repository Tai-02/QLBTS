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
            //ui.OpenChildForm(, NVG_pn_tab);
        }

        private void NVG_lb_dagiao_Click(object sender, EventArgs e)
        {
            ui.OpenChildForm(new LichSu(Khung.MaTK_temp), NVG_pn_tab);
        }

        private void NVG_lb_danhan_Click(object sender, EventArgs e)
        {
            //ui.OpenChildForm(, NVG_pn_tab);
        }
    }
}
