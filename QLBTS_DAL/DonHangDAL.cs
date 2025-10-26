using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using QLBTS_DTO;

namespace QLBTS_DAL
{
    public class DonHangDAL
    {
        // Lấy danh sách đơn hàng cần xác nhận (trạng thái: "Cho xac nhan")
        // Dán phương thức này vào file DonHangDAL.cs

        /// <summary>
        /// Lấy lịch sử các đơn hàng đã giao thành công
        /// </summary>
        public List<DonHangDTO> GetLichSuDonHangDaGiao()
        {
            List<DonHangDTO> list = new List<DonHangDTO>();

            // Câu lệnh SQL này lấy các thông tin bạn yêu cầu từ bảng LichSuGiaoHang và DonHang
            string query = @"
        SELECT 
            dh.MaDH,
            lsgh.MaNVGiao,
            dh.TongTien,
            lsgh.TrangThai,
            lsgh.ThoiGian
        FROM LichSuGiaoHang lsgh
        JOIN DonHang dh ON lsgh.MaDH = dh.MaDH
        WHERE lsgh.TrangThai = 'Da giao'
        ORDER BY lsgh.ThoiGian DESC";

            try
            {
                DataTable dt = DataProvider.ExecuteQuery(query);

                foreach (DataRow row in dt.Rows)
                {
                    list.Add(new DonHangDTO
                    {
                        MaDH = Convert.ToInt32(row["MaDH"]),
                        MaNVGiao = Convert.ToInt32(row["MaNVGiao"]),
                        TongTien = Convert.ToInt32(row["TongTien"]),
                        TrangThai = row["TrangThai"].ToString(),
                        ThoiGianGiao = Convert.ToDateTime(row["ThoiGian"]) // Gán vào thuộc tính mới
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy lịch sử đơn hàng đã giao: " + ex.Message);
            }

            return list;
        }
        public List<DonHangDTO> GetDonHangCanXacNhan()
        {
            List<DonHangDTO> list = new List<DonHangDTO>();

            string query = @"SELECT dh.MaDH, dh.MaKH, dh.NgayDat, dh.TongTien, dh.TrangThai,
                                 kh.HoTen as TenKhachHang, kh.SDT as SDTKhachHang, kh.DiaChi as DiaChiKhachHang
                                 FROM DonHang dh
                                 LEFT JOIN KhachHang kh ON dh.MaKH = kh.MaKH
                                 WHERE dh.TrangThai = 'Cho xac nhan'
                                 ORDER BY dh.NgayDat DESC";

            try
            {
                DataTable dt = DataProvider.ExecuteQuery(query);

                foreach (DataRow row in dt.Rows)
                {
                    DonHangDTO dh = new DonHangDTO
                    {
                        MaDH = Convert.ToInt32(row["MaDH"]),
                        MaKH = row["MaKH"] != DBNull.Value ? Convert.ToInt32(row["MaKH"]) : (int?)null,
                        NgayDat = Convert.ToDateTime(row["NgayDat"]),
                        TongTien = Convert.ToInt32(row["TongTien"]),
                        TrangThai = row["TrangThai"].ToString(),
                        TenKhachHang = row["TenKhachHang"] != DBNull.Value ? row["TenKhachHang"].ToString() : "Khách vãng lai",
                        SDTKhachHang = row["SDTKhachHang"] != DBNull.Value ? row["SDTKhachHang"].ToString() : "",
                        DiaChiKhachHang = row["DiaChiKhachHang"] != DBNull.Value ? row["DiaChiKhachHang"].ToString() : ""
                    };
                    list.Add(dh);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy danh sách đơn hàng: " + ex.Message);
            }

            return list;
        }

        // Cập nhật trạng thái đơn hàng và gán nhân viên xử lý
        public bool NhanDonHang(int maDH, int maNVQuay)
        {
            string query = @"UPDATE DonHang 
                             SET TrangThai = 'Da xac nhan', 
                                 MaNVQuay = @MaNVQuay 
                             WHERE MaDH = @MaDH";

            MySqlParameter[] parameters = {
                new MySqlParameter("@MaDH", maDH),
                new MySqlParameter("@MaNVQuay", maNVQuay)
            };

            try
            {
                int result = DataProvider.ExecuteNonQuery(query, parameters);
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi nhận đơn hàng: " + ex.Message);
            }
        }

        // ==========================================================
        // ===            PHƯƠNG THỨC MỚI ĐƯỢC THÊM VÀO           ===
        // ==========================================================

        /// <summary>
        /// Lấy danh sách các đơn hàng đã được xác nhận, sẵn sàng để giao
        /// </summary>
        public List<DonHangDTO> GetDonHangDaNhan()
        {
            List<DonHangDTO> list = new List<DonHangDTO>();
            // Lấy các đơn hàng có trạng thái "Da xac nhan"
            string query = @"SELECT dh.MaDH, dh.TongTien 
                             FROM DonHang dh
                             WHERE dh.TrangThai = 'Da xac nhan'
                             ORDER BY dh.NgayDat ASC"; // Ưu tiên đơn cũ hơn

            try
            {
                DataTable dt = DataProvider.ExecuteQuery(query);
                foreach (DataRow row in dt.Rows)
                {
                    // Chỉ cần thông tin cơ bản để hiển thị trên lưới
                    DonHangDTO dh = new DonHangDTO
                    {
                        MaDH = Convert.ToInt32(row["MaDH"]),
                        TongTien = Convert.ToInt32(row["TongTien"])
                    };
                    list.Add(dh);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy danh sách đơn hàng đã nhận: " + ex.Message);
            }
            return list;
        }

        /// <summary>
        /// Cập nhật trạng thái đơn hàng thành 'Dang giao' và gán nhân viên giao hàng
        /// </summary>
        public bool GiaoDonHang(int maDH, int maNVGiao)
        {
            // Cập nhật trạng thái và gán MaNVGiao
            string query = @"UPDATE DonHang 
                             SET TrangThai = 'Dang giao', 
                                 MaNVGiao = @MaNVGiao 
                             WHERE MaDH = @MaDH AND TrangThai = 'Da xac nhan'";

            MySqlParameter[] parameters = {
                new MySqlParameter("@MaNVGiao", maNVGiao),
                new MySqlParameter("@MaDH", maDH)
            };

            try
            {
                int result = DataProvider.ExecuteNonQuery(query, parameters);
                return result > 0; // Trả về true nếu có dòng được cập nhật
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi giao đơn hàng: " + ex.Message);
            }
        }

        // ==========================================================
        // ===      CÁC PHƯƠNG THỨC CŨ VẪN GIỮ NGUYÊN BÊN DƯỚI     ===
        // ==========================================================

        // Lấy thông tin chi tiết đơn hàng theo MaDH
        public DonHangDTO GetDonHangById(int maDH)
        {
            string query = @"SELECT dh.MaDH, dh.MaKH, dh.MaNVQuay, dh.MaNVGiao, dh.NgayDat, dh.TongTien, dh.TrangThai,
                                 kh.HoTen as TenKhachHang, kh.SDT as SDTKhachHang, kh.DiaChi as DiaChiKhachHang
                                 FROM DonHang dh
                                 LEFT JOIN KhachHang kh ON dh.MaKH = kh.MaKH
                                 WHERE dh.MaDH = @MaDH";

            MySqlParameter[] parameters = {
                new MySqlParameter("@MaDH", maDH)
            };

            try
            {
                DataTable dt = DataProvider.ExecuteQuery(query, parameters);

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    return new DonHangDTO
                    {
                        MaDH = Convert.ToInt32(row["MaDH"]),
                        MaKH = row["MaKH"] != DBNull.Value ? Convert.ToInt32(row["MaKH"]) : (int?)null,
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
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy thông tin đơn hàng: " + ex.Message);
            }

            return null;
        }

        // Lấy chi tiết sản phẩm trong đơn hàng
        public List<ChiTietDonHangDTO> GetChiTietDonHang(int maDH)
        {
            List<ChiTietDonHangDTO> list = new List<ChiTietDonHangDTO>();

            string query = @"SELECT ct.MaCT, ct.MaDH, ct.MaSP, ct.SoLuong, ct.DonGia,
                                 sp.TenSP, sp.Size
                                 FROM ChiTietDonHang ct
                                 INNER JOIN SanPham sp ON ct.MaSP = sp.MaSP
                                 WHERE ct.MaDH = @MaDH";

            MySqlParameter[] parameters = {
                new MySqlParameter("@MaDH", maDH)
            };

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

        // Hủy đơn hàng
        public bool HuyDonHang(int maDH)
        {
            string query = @"UPDATE DonHang 
                             SET TrangThai = 'Da huy' 
                             WHERE MaDH = @MaDH";

            MySqlParameter[] parameters = {
                new MySqlParameter("@MaDH", maDH)
            };

            try
            {
                int result = DataProvider.ExecuteNonQuery(query, parameters);
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi hủy đơn hàng: " + ex.Message);
            }
        }

        // Lấy tất cả đơn hàng (không lọc trạng thái)
        public List<DonHangDTO> GetAllDonHang()
        {
            List<DonHangDTO> list = new List<DonHangDTO>();

            string query = @"SELECT dh.MaDH, dh.MaKH, dh.NgayDat, dh.TongTien, dh.TrangThai,
                                 kh.HoTen as TenKhachHang, kh.SDT as SDTKhachHang, kh.DiaChi as DiaChiKhachHang
                                 FROM DonHang dh
                                 LEFT JOIN KhachHang kh ON dh.MaKH = kh.MaKH
                                 ORDER BY dh.NgayDat DESC";

            try
            {
                DataTable dt = DataProvider.ExecuteQuery(query);

                foreach (DataRow row in dt.Rows)
                {
                    DonHangDTO dh = new DonHangDTO
                    {
                        MaDH = Convert.ToInt32(row["MaDH"]),
                        MaKH = row["MaKH"] != DBNull.Value ? Convert.ToInt32(row["MaKH"]) : (int?)null,
                        NgayDat = Convert.ToDateTime(row["NgayDat"]),
                        TongTien = Convert.ToInt32(row["TongTien"]),
                        TrangThai = row["TrangThai"].ToString(),
                        TenKhachHang = row["TenKhachHang"] != DBNull.Value ? row["TenKhachHang"].ToString() : "Khách vãng lai",
                        SDTKhachHang = row["SDTKhachHang"] != DBNull.Value ? row["SDTKhachHang"].ToString() : "",
                        DiaChiKhachHang = row["DiaChiKhachHang"] != DBNull.Value ? row["DiaChiKhachHang"].ToString() : ""
                    };
                    list.Add(dh);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy tất cả đơn hàng: " + ex.Message);
            }

            return list;
        }
    }
}