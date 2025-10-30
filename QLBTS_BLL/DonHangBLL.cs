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

        public bool DoiTrangThai(int maDH, string trangThaiMoi)
        {
            return dal.DoiTrangThai(maDH, trangThaiMoi);
        }
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

        public List<DonHangDTO> LayDonHangTheoKhach(int maKhach) => dal.LayDSDonHang(maKhach);
    }
}
