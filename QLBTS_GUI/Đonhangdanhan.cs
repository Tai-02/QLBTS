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
    public partial class Đonhangdanhan : Form
    {
        public Đonhangdanhan()
        {
            InitializeComponent();
        }

        private void Đonhangdanhan_Load(object sender, EventArgs e)
        {
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Black;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            dataGridView1.ColumnHeadersHeight = 40;
            dataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 11);
            dataGridView1.RowTemplate.Height = 35;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridView1.Columns["btnGiao"].DefaultCellStyle.BackColor = Color.LimeGreen;
            dataGridView1.Columns["btnGiao"].DefaultCellStyle.ForeColor = Color.White;
            dataGridView1.Columns["btnGiao"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView1.Columns["btnChiTiet"].DefaultCellStyle.BackColor = Color.Black;
            dataGridView1.Columns["btnChiTiet"].DefaultCellStyle.ForeColor = Color.White;
            dataGridView1.Columns["btnChiTiet"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Rows.Add("12345", "100.000đ");
            dataGridView1.Rows.Add("12345", "100.000đ");
            
        }

        private void Namedau_Click(object sender, EventArgs e)
        {

        }
    }
}
