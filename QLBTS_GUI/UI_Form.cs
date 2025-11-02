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

        public string ShowInputForm(string title, string labelText)
        {
            string result = null;

            Form form = new Form();
            form.Text = title;
            form.StartPosition = FormStartPosition.CenterParent;
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.Width = 300;
            form.Height = 150;

            Label label = new Label() { Left = 10, Top = 10, Text = labelText, AutoSize = true };
            TextBox textBox = new TextBox() { Left = 10, Top = 35, Width = 260 };
            Button btnOK = new Button() { Text = "OK", Left = 50, Width = 80, Top = 70, DialogResult = DialogResult.OK };
            Button btnCancel = new Button() { Text = "Cancel", Left = 150, Width = 80, Top = 70, DialogResult = DialogResult.Cancel };

            form.Controls.Add(label);
            form.Controls.Add(textBox);
            form.Controls.Add(btnOK);
            form.Controls.Add(btnCancel);

            form.AcceptButton = btnOK;
            form.CancelButton = btnCancel;

            if (form.ShowDialog() == DialogResult.OK)
            {
                result = textBox.Text.Trim();
            }

            form.Dispose();
            return result;
        }

    }
}
