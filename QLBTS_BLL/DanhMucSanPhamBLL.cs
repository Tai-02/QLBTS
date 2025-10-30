using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLBTS_DAL;
using QLBTS_DTO;
using System.Collections.Generic;

namespace QLBTS_BLL
{
    public class DanhMucSanPhamBLL
    {
        private DanhMucSanPhamDAL dal = new DanhMucSanPhamDAL();

        public List<SanPhamDTO> LayDanhSachSanPham(string filterType = null)
        {
            return dal.LayDanhSachSanPham(filterType);
        }

        public List<SanPhamDTO> TimKiemSanPham(string keyword)
        {
            return dal.TimKiemSanPham(keyword);
        }
    }
}

