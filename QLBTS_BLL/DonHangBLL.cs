using QLBTS_DAL;
using QLBTS_DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace QLBTS_BLL
{
    public class DonHangBLL
    {
        private DonHangDAL dal = new DonHangDAL();

        // 🔹 1️⃣ Đổi trạng thái đơn hàng
        public bool DoiTrangThai(int maDH, string trangThaiMoi)
        {
            if (string.IsNullOrWhiteSpace(trangThaiMoi))
                throw new ArgumentException("Trạng thái mới không hợp lệ.");

            return dal.DoiTrangThai(maDH, trangThaiMoi);
        }

        // 🔹 2️⃣ Tính tổng tiền đơn hàng
        public decimal[] TinhTongTienDonHang(int maDH)
        {
            try
            {
                return dal.TinhTongTienDonHang(maDH);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi BLL - TinhTongTienDonHang: {ex.Message}", ex);
            }
        }

        // 🔹 3️⃣ Lấy danh sách đơn hàng theo khách hoặc toàn bộ
        public List<DonHangDTO> LayDonHangTheoKhach(int maKhach)
        {
            return dal.LayDSDonHang(maKhach);
        }

        public List<DonHangDTO> LayTatCaDonHang()
        {
            return dal.LayDSDonHang();
        }

        // 🔹 4️⃣ Hủy đơn hàng
        public bool HuyDonHang(int maDH)
        {
            try
            {
                return dal.HuyDonHang(maDH);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi BLL - HuyDonHang: {ex.Message}", ex);
            }
        }
    }
}
