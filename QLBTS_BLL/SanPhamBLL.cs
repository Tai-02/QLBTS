using QLBTS_DAL;
using QLBTS_DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBTS_BLL
{
    public class SanPhamBLL
    {
        private SanPhamDAL dal = new SanPhamDAL();

        // Lấy chi tiết sản phẩm theo MaSP
        public SanPhamDTO LaySanPham(int maSP)
        {
            return dal.GetSanPham(maSP);
        }

        // Lấy tất cả sản phẩm cùng tên (tất cả size)
        public List<SanPhamDTO> LaySanPhamTheoTen(string tenSP)
        {
            return dal.GetSanPhamTheoTen(tenSP);
        }

        // Kiểm tra tồn kho trước khi đặt hàng
        public bool KiemTraTonKho(int maSP, int soLuongCanMua)
        {
            return dal.CheckTonKho(maSP, soLuongCanMua);
        }

        // Cập nhật tồn kho (trừ khi đặt hàng, cộng khi hủy)
        public bool CapNhatTonKho(int maSP, int soLuongThayDoi)
        {
            return dal.CapNhatTonKho(maSP, soLuongThayDoi);
        }

        // Lấy tồn kho hiện tại
        public int LayTonKhoHienTai(int maSP)
        {
            return dal.GetTonKhoHienTai(maSP);
        }

        // Ví dụ: đặt hàng 1 sản phẩm
        public bool DatHang(int maSP, int soLuong)
        {
            if (!KiemTraTonKho(maSP, soLuong))
                return false;

            // Trừ tồn kho
            return CapNhatTonKho(maSP, -soLuong);
        }

        // Ví dụ: hủy đơn, trả lại hàng
        public bool HuyDon(int maSP, int soLuong)
        {
            // Cộng tồn kho
            return CapNhatTonKho(maSP, soLuong);
        }
        public string GetTextGia(SanPhamDTO sp)
        {
            if (sp == null) return "0đ";

            // Giá sau khuyến mãi
            double giaSauKM = sp.Gia - sp.Gia * sp.KhuyenMai / 100.0;

            // Làm tròn về bội số 1000
            int giaLamTron = (int)(Math.Round(giaSauKM / 1000.0) * 1000);

            return $"{giaLamTron:N0}đ";
        }

        // Kiểm tra có sản phẩm cùng tên cùng size khác MaSP
        public SanPhamDTO GetSanPhamCungTenKhacSize(SanPhamDTO sp)
        {
            return dal.GetSanPhamCungTenKhacSize(sp.MaSP, sp.TenSP, sp.Size);
        }

    }
}
