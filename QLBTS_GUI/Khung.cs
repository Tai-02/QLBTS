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
        public static int lvID = -1;
        UI_Form ui = new UI_Form();
        public Khung()
        {
            InitializeComponent();
        }

        private void Khung_Load(object sender, EventArgs e)
        {
            ui.OpenChildForm(new DangNhap(Khung_pn), Khung_pn);
        }
    }
}
