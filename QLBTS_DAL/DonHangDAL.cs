#nullable disable
using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using QLBTS_DTO;

namespace QLBTS_DAL
{
    public class DonHangDAL
    {
        // Lấy danh sách đơn hàng cần xác nhận
        public List<DonHangDTO> GetDonHangCanXacNhan()
        {
            var list = new List<DonHangDTO>();
            // Sửa lại: JOIN với bảng TaiKhoan để lấy thông tin khách hàng
            string query = @"
                SELECT dh.MaDH, dh.MaKhach, dh.NgayDat, dh.TongTien, dh.TrangThai,
                       tk.HoTen as TenKhachHang, tk.SDT as SDTKhachHang, tk.DiaChi as DiaChiKhachHang
                FROM DonHang dh
                LEFT JOIN TaiKhoan tk ON dh.MaKhach = tk.MaTK
                WHERE dh.TrangThai = 'Chờ xác nhận'
                ORDER BY dh.NgayDat DESC";

            DataTable dt = DataProvider.ExecuteQuery(query);
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new DonHangDTO
                {
                    MaDH = Convert.ToInt32(row["MaDH"]),
                    MaKH = row["MaKhach"] != DBNull.Value ? Convert.ToInt32(row["MaKhach"]) : (int?)null, // Đổi tên cột
                    NgayDat = Convert.ToDateTime(row["NgayDat"]),
                    TongTien = Convert.ToInt32(row["TongTien"]),
                    TrangThai = row["TrangThai"].ToString(),
                    TenKhachHang = row["TenKhachHang"] != DBNull.Value ? row["TenKhachHang"].ToString() : "Khách vãng lai",
                    SDTKhachHang = row["SDTKhachHang"] != DBNull.Value ? row["SDTKhachHang"].ToString() : "",
                    DiaChiKhachHang = row["DiaChiKhachHang"] != DBNull.Value ? row["DiaChiKhachHang"].ToString() : ""
                });
            }
            return list;
        }

        // Cập nhật trạng thái và gán MaTK của nhân viên quầy
        public bool NhanDonHang(int maDH, int maTkNhanVienQuay)
        {
            string query = "UPDATE DonHang SET TrangThai = 'Đã xác nhận', MaNVQuay = @MaNVQuay WHERE MaDH = @MaDH";
            MySqlParameter[] parameters = {
                new MySqlParameter("@MaDH", maDH),
                new MySqlParameter("@MaNVQuay", maTkNhanVienQuay)
            };
            return DataProvider.ExecuteNonQuery(query, parameters) > 0;
        }

        // Lấy danh sách đơn hàng đã nhận, chờ giao
        public List<DonHangDTO> GetDonHangDaNhan()
        {
            var list = new List<DonHangDTO>();
            // Lấy các trạng thái sau khi đã xác nhận
            string query = "SELECT MaDH, TongTien FROM DonHang WHERE TrangThai IN ('Đã xác nhận', 'Đang pha chế', 'Chờ giao hàng')";
            DataTable dt = DataProvider.ExecuteQuery(query);
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new DonHangDTO
                {
                    MaDH = Convert.ToInt32(row["MaDH"]),
                    TongTien = Convert.ToInt32(row["TongTien"])
                });
            }
            return list;
        }

        // Giao đơn hàng, gán MaTK của nhân viên giao hàng
        public bool GiaoDonHang(int maDH, int maTkNhanVienGiao)
        {
            string query = "UPDATE DonHang SET TrangThai = 'Đang giao', MaNVGiao = @MaNVGiao WHERE MaDH = @MaDH";
            MySqlParameter[] parameters = {
                new MySqlParameter("@MaDH", maDH),
                new MySqlParameter("@MaNVGiao", maTkNhanVienGiao)
            };
            return DataProvider.ExecuteNonQuery(query, parameters) > 0;
        }

        // Lấy thông tin chi tiết của một đơn hàng
        public DonHangDTO GetDonHangById(int maDH)
        {
            // Sửa lại: JOIN với bảng TaiKhoan
            string query = @"
                SELECT dh.MaDH, dh.MaKhach, dh.MaNVQuay, dh.MaNVGiao, dh.NgayDat, dh.TongTien, dh.TrangThai,
                       tk.HoTen as TenKhachHang, tk.SDT as SDTKhachHang, tk.DiaChi as DiaChiKhachHang
                FROM DonHang dh
                LEFT JOIN TaiKhoan tk ON dh.MaKhach = tk.MaTK
                WHERE dh.MaDH = @MaDH";

            MySqlParameter[] parameters = { new MySqlParameter("@MaDH", maDH) };
            DataTable dt = DataProvider.ExecuteQuery(query, parameters);
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                return new DonHangDTO
                {
                    MaDH = Convert.ToInt32(row["MaDH"]),
                    MaKH = row["MaKhach"] != DBNull.Value ? Convert.ToInt32(row["MaKhach"]) : (int?)null, // Đổi tên cột
                    MaNVQuay = row["MaNVQuay"] != DBNull.Value ? Convert.ToInt32(row["MaNVQuay"]) : (int?)null,
                    MaNVGiao = row["MaNVGiao"] != DBNull.Value ? Convert.ToInt32(row["MaNVGiao"]) : (int?)null,
                    NgayDat = Convert.ToDateTime(row["NgayDat"]),
                    TongTien = Convert.ToInt32(row["TongTien"]),
                    TrangThai = row["TrangThai"].ToString(),
                    TenKhachHang = row["TenKhachHang"] != DBNull.Value ? row["TenKhachHang"].ToString() : "Khách vãng lai",
                    SDTKhachHang = row["SDTKhachHang"] != DBNull.Value ? row["SDTKhachHang"].ToString() : "",
                    DiaChiKhachHang = row["DiaChiKhachHang"] != DBNull.Value ? row["DiaChiKhachHang"].ToString() : ""
                };
            }
            return null;
        }

        // Lấy chi tiết sản phẩm của một đơn hàng (đọc ảnh BLOB)
        // Lấy chi tiết sản phẩm trong đơn hàng (phiên bản không có ảnh)
        public List<ChiTietDonHangDTO> GetChiTietDonHang(int maDH)
        {
            List<ChiTietDonHangDTO> list = new List<ChiTietDonHangDTO>();

            // Sửa lại câu query, đã xóa sp.HinhAnh
            string query = @"SELECT ct.MaCT, ct.MaDH, ct.MaSP, ct.SoLuong, ct.DonGia,
                         sp.TenSP, sp.Size 
                         FROM ChiTietDonHang ct
                         INNER JOIN SanPham sp ON ct.MaSP = sp.MaSP
                         WHERE ct.MaDH = @MaDH";

            MySqlParameter[] parameters = { new MySqlParameter("@MaDH", maDH) };

            try
            {
                DataTable dt = DataProvider.ExecuteQuery(query, parameters);
                foreach (DataRow row in dt.Rows)
                {
                    ChiTietDonHangDTO ct = new ChiTietDonHangDTO
                    {
                        MaCT = Convert.ToInt32(row["MaCT"]),
                        MaDH = Convert.ToInt32(row["MaDH"]),
                        MaSP = Convert.ToInt32(row["MaSP"]),
                        SoLuong = Convert.ToInt32(row["SoLuong"]),
                        DonGia = Convert.ToInt32(row["DonGia"]),
                        TenSP = row["TenSP"].ToString(),
                        Size = row["Size"].ToString()
                        // Đã xóa dòng gán giá trị cho HinhAnh
                    };
                    list.Add(ct);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy chi tiết đơn hàng: " + ex.Message);
            }
            return list;
        }

        // Lấy lịch sử đơn hàng đã giao từ bảng mới
        public List<DonHangDTO> GetLichSuDonHangDaGiao()
        {
            var list = new List<DonHangDTO>();
            // Sửa lại: Truy vấn bảng LichSuHoatDong
            string query = @"
                SELECT MaDH, MaTK AS MaNVGiao, TongTien, TrangThai, ThoiGian 
                FROM LichSuHoatDong 
                WHERE LoaiLichSu = 'GiaoHang' AND TrangThai = 'Đã giao'
                ORDER BY ThoiGian DESC";

            DataTable dt = DataProvider.ExecuteQuery(query);
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new DonHangDTO
                {
                    MaDH = Convert.ToInt32(row["MaDH"]),
                    MaNVGiao = Convert.ToInt32(row["MaNVGiao"]),
                    TongTien = Convert.ToInt32(row["TongTien"]),
                    TrangThai = row["TrangThai"].ToString(),
                    ThoiGianGiao = Convert.ToDateTime(row["ThoiGian"])
                });
            }
            return list;
        }
    }
}