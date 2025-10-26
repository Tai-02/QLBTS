using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace QLBTS_GUI
{
    public class UI_Form
    {
        // 📌 Hàm mở form con trong panel
        public void OpenChildForm(Form childForm, Control pn)
        {
            if (pn.Controls.Count > 0)
                pn.Controls.Clear();

            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            pn.Controls.Add(childForm);
            pn.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }        

    }
}
