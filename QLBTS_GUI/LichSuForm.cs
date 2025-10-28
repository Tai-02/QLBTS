using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLBTS_BLL;
using QLBTS_DTO;

namespace QLBTS_GUI
{
    public partial class LichSuForm : Form
    {
        // ===== FIELDS =====
        private OrderBLL _orderBLL;
        private int _maTK;

        // ===== CONSTRUCTOR =====
        public LichSuForm(int maTK)
        {
            InitializeComponent();
            _orderBLL = new OrderBLL();
            _maTK = maTK;

            // Load dữ liệu
            LoadLichSu();
        }

        // ===== PRIVATE METHODS =====

        /// <summary>
        /// Load lịch sử hoạt động
        /// </summary>
        private void LoadLichSu()
        {
            try
            {
                var lichSuList = _orderBLL.GetLichSuByCustomer(_maTK);

                if (lichSuList == null || !lichSuList.Any())
                {
                    txtLichSu.Text = "Chưa có lịch sử hoạt động nào.";
                    return;
                }

                // Ghép các dòng lịch sử
                var lines = lichSuList.Select(ls => ls.NoiDung);
                txtLichSu.Text = string.Join(Environment.NewLine, lines);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Lỗi khi tải lịch sử: {ex.Message}",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}
