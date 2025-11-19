using QLBTS_BLL;
using QLBTS_DAL;
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
    public partial class HieuSuat : Form
    {
        ThongKeNhanVienBLL bll = new ThongKeNhanVienBLL();
        public HieuSuat()
        {
            InitializeComponent();
        }

        private void HieuSuat_Load(object sender, EventArgs e)
        {
            cbbKieuHieuSuat.Items.Add("Ngày");
            cbbKieuHieuSuat.Items.Add("Tháng");
            cbbKieuHieuSuat.SelectedIndex = 0;
        }

        private void txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TTCN_btnCapnhat_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMucTieu.Text))
            {
                MessageBox.Show("Vui lòng nhập mục tiêu doanh thu!");
                return;
            }

            double mucTieu;
            if (!double.TryParse(txtMucTieu.Text, out mucTieu) || mucTieu <= 0)
            {
                MessageBox.Show("Mục tiêu phải là số > 0");
                return;
            }

            DateTime ngay = dtpNgay.Value;
            string kieuHieuSuat = cbbKieuHieuSuat.SelectedItem.ToString();

            try
            {
                var list = bll.LayHieuSuatNV(mucTieu, ngay, kieuHieuSuat);
                dgvHieuSuat.DataSource = list;

                // Đặt header
                dgvHieuSuat.Columns["MaNV"].HeaderText = "Mã NV";
                dgvHieuSuat.Columns["HoTen"].HeaderText = "Họ tên";
                dgvHieuSuat.Columns["SoNgayLam"].HeaderText = "Số ngày làm";
                dgvHieuSuat.Columns["TongDoanhThu"].HeaderText = "Tổng doanh thu";
                dgvHieuSuat.Columns["HieuSuat"].HeaderText = "Hiệu suất (%)";

                // Format cột Hiệu suất
                dgvHieuSuat.Columns["HieuSuat"].DefaultCellStyle.Format = "N2";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tính hiệu suất: " + ex.Message);
            }
        }

    }
}