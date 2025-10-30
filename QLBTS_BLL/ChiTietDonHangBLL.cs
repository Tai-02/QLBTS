using QLBTS_DAL;
using QLBTS_DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBTS_BLL
{
    public class ChiTietDonHangBLL
    {
        private ChiTietDonHangDAL dal = new ChiTietDonHangDAL();

        public List<SanPhamDTO> LayChiTietDonHangTheoMaDH(int maDH)
        {
            if (maDH <= 0)
                throw new ArgumentException("Mã đơn hàng không hợp lệ!");

            try
            {
                return dal.LayChiTietDonHangTheoMaDH(maDH);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi BLL - LayChiTietDonHangTheoMaDH: {ex.Message}", ex);
            }
        }
    }
}
