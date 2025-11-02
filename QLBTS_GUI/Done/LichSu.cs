using QLBTS_BBL;
using QLBTS_BLL;
using QLBTS_DTO;
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
    public partial class LichSu : Form
    {
        private LichSuHoatDongBBL _lichSuBBL;
        private int _maTK;

        public LichSu(int maTK)
        {
            InitializeComponent();
            _maTK = maTK;
            _lichSuBBL = new LichSuHoatDongBBL();
            LoadLichSu();
        }

        private void LoadLichSu()
        {
            try
            {
                var lichSuList = _lichSuBBL.LayLichSu(_maTK);

                if (lichSuList == null || lichSuList.Count == 0)
                {
                    txtLichSu.Text = "Chưa có lịch sử hoạt động nào.";
                    return;
                }

                StringBuilder sb = new StringBuilder();

                foreach (var ls in lichSuList)
                {
                    string tg = ls.ThoiGian == DateTime.MinValue ? "Không xác định" : ls.ThoiGian.ToString("dd/MM/yyyy HH:mm");
                    string madh = ls.MaDH > 0 ? ls.MaDH.ToString() : "-";
                    string mota = string.IsNullOrEmpty(ls.MoTa) ? "Không có mô tả" : ls.MoTa;
                    string tongtien = ls.TongTien > 0 ? $"{ls.TongTien:N0}đ" : "-";

                    sb.AppendLine($"{tg} | {madh} | {mota} | {tongtien}");
                }

                txtLichSu.Text = sb.ToString();
                txtLichSu.Font = new System.Drawing.Font("Consolas", 10);
                txtLichSu.ScrollBars = ScrollBars.Vertical;
                txtLichSu.WordWrap = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải lịch sử: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
