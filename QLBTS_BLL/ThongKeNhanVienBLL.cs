using QLBTS_DAL;
using QLBTS_DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBTS_BLL
{
    public class ThongKeNhanVienBLL
    {
        private ThongKeNhanVienDAL dal = new ThongKeNhanVienDAL();

        public List<ThongKeNhanVienDTO> LayHieuSuatNV(double mucTieu, DateTime ngay, string kieuHieuSuat)
        {
            return dal.GetHieuSuatNhanVien(mucTieu, ngay, kieuHieuSuat);
        }

    }
}
