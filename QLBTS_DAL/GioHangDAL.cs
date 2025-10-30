using System.Collections.Generic;
using MySql.Data.MySqlClient;
using QLBTS_DTO;

namespace QLBTS_DAL
{
    public class GioHangDAL
    {
        // 1️⃣ Lấy giỏ hàng theo mã tài khoản (MaTK)
        public static List<SanPhamDTO> LayGioHangTheoMaTK(int maTK)
        {
            List<SanPhamDTO> danhSach = new List<SanPhamDTO>();

            string query = @"
                SELECT 
                    sp.MaSP,
                    sp.TenSP,
                    sp.Size,
                    sp.Gia,
                    sp.KhuyenMai,
                    sp.HinhAnh,
                    ctgh.SoLuong
                FROM ChiTietGioHang ctgh
                JOIN SanPham sp ON ctgh.MaSP = sp.MaSP
                JOIN GioHang gh ON ctgh.MaGH = gh.MaGH
                WHERE gh.MaTK = @MaTK;
            ";

            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaTK", maTK);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            SanPhamDTO sp = new SanPhamDTO
                            {
                                MaSP = reader.GetInt32("MaSP"),
                                TenSP = reader.GetString("TenSP"),
                                Size = reader.GetString("Size"),
                                Gia = reader.GetInt32("Gia"),
                                KhuyenMai = reader.GetInt32("KhuyenMai"),
                                SoLuong = reader.GetInt32("SoLuong"),
                                HinhAnh = reader["HinhAnh"] == DBNull.Value ? null : (byte[])reader["HinhAnh"]
                            };
                            danhSach.Add(sp);
                        }
                    }
                }
            }

            return danhSach;
        }

        public static void ThemSanPhamVaoGio(int maTK, SanPhamDTO sp, int soLuong = 1)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // 1️⃣ Kiểm tra giỏ hàng của maTK
                        int maGH = 0;
                        string queryLayGH = "SELECT MaGH FROM GioHang WHERE MaTK = @MaTK LIMIT 1;";
                        using (var cmdGH = new MySqlCommand(queryLayGH, conn, transaction))
                        {
                            cmdGH.Parameters.AddWithValue("@MaTK", maTK);
                            var result = cmdGH.ExecuteScalar();
                            if (result != null)
                                maGH = Convert.ToInt32(result);
                            else
                            {
                                // Tạo giỏ hàng mới
                                string queryTaoGH = "INSERT INTO GioHang (MaTK) VALUES (@MaTK); SELECT LAST_INSERT_ID();";
                                using (var cmdTao = new MySqlCommand(queryTaoGH, conn, transaction))
                                {
                                    cmdTao.Parameters.AddWithValue("@MaTK", maTK);
                                    maGH = Convert.ToInt32(cmdTao.ExecuteScalar());
                                }
                            }
                        }

                        // 2️⃣ Kiểm tra sản phẩm đã có trong giỏ chưa
                        string queryKiemTra = "SELECT COUNT(*) FROM ChiTietGioHang WHERE MaGH = @MaGH AND MaSP = @MaSP;";
                        using (var cmdKT = new MySqlCommand(queryKiemTra, conn, transaction))
                        {
                            cmdKT.Parameters.AddWithValue("@MaGH", maGH);
                            cmdKT.Parameters.AddWithValue("@MaSP", sp.MaSP);
                            int count = Convert.ToInt32(cmdKT.ExecuteScalar());
                            if (count > 0)
                                throw new Exception("Sản phẩm đã có trong giỏ hàng!");
                        }

                        // 3️⃣ Thêm sản phẩm mới vào giỏ
                        string queryThem = @"
                            INSERT INTO ChiTietGioHang (MaGH, MaSP, SoLuong)
                            VALUES (@MaGH, @MaSP, @SoLuong);";

                        using (var cmdThem = new MySqlCommand(queryThem, conn, transaction))
                        {
                            cmdThem.Parameters.AddWithValue("@MaGH", maGH);
                            cmdThem.Parameters.AddWithValue("@MaSP", sp.MaSP);
                            cmdThem.Parameters.AddWithValue("@SoLuong", soLuong);
                            cmdThem.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }


        // 2️⃣ Cập nhật số lượng sản phẩm trong giỏ hàng
        public static void CapNhatSoLuong(int maTK, int maSP, int soLuong)
        {
            string query = @"
                UPDATE ChiTietGioHang ctgh
                JOIN GioHang gh ON ctgh.MaGH = gh.MaGH
                SET ctgh.SoLuong = @SoLuong
                WHERE gh.MaTK = @MaTK AND ctgh.MaSP = @MaSP;
            ";

            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SoLuong", soLuong);
                    cmd.Parameters.AddWithValue("@MaTK", maTK);
                    cmd.Parameters.AddWithValue("@MaSP", maSP);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // 3️⃣ Xóa sản phẩm khỏi giỏ hàng
        public static void XoaSanPhamKhoiGio(int maTK, int maSP)
        {
            string query = @"
                DELETE ctgh FROM ChiTietGioHang ctgh
                JOIN GioHang gh ON ctgh.MaGH = gh.MaGH
                WHERE gh.MaTK = @MaTK AND ctgh.MaSP = @MaSP;
            ";

            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaTK", maTK);
                    cmd.Parameters.AddWithValue("@MaSP", maSP);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // 4️⃣ Xóa toàn bộ giỏ hàng sau khi thanh toán
        public static void XoaToanBoGio(int maTK)
        {
            string query = @"
                DELETE ctgh FROM ChiTietGioHang ctgh
                JOIN GioHang gh ON ctgh.MaGH = gh.MaGH
                WHERE gh.MaTK = @MaTK;
            ";

            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaTK", maTK);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Đặt hàng danh cho khách
        public static int DatHangKH(int maKhach, List<SanPhamDTO> listSP)
        {
            if (listSP == null || listSP.Count == 0)
                throw new Exception("Danh sách sản phẩm trống.");

            int maDH = 0;

            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // 🔹 1. Kiểm tra tồn kho từng sản phẩm
                        foreach (var sp in listSP)
                        {
                            string queryCheck = "SELECT SoLuong, TenSP FROM SanPham WHERE MaSP = @MaSP";
                            using (var cmdCheck = new MySqlCommand(queryCheck, conn, transaction))
                            {
                                cmdCheck.Parameters.AddWithValue("@MaSP", sp.MaSP);

                                using (var reader = cmdCheck.ExecuteReader())
                                {
                                    if (!reader.Read())
                                        throw new Exception($"Sản phẩm có mã {sp.MaSP} không tồn tại.");

                                    // ✅ sửa lại đây
                                    int soLuongTon = Convert.ToInt32(reader["SoLuong"]);
                                    string tenSP = reader["TenSP"].ToString();

                                    if (soLuongTon < sp.SoLuong)
                                    {
                                        reader.Close();
                                        throw new Exception($"Sản phẩm '{tenSP}' chỉ còn {soLuongTon} sản phẩm, không đủ để đặt hàng.");
                                    }
                                }
                            }
                        }

                        // 🔹 2. Tính tổng tiền
                        int tongTien = 0;
                        foreach (var sp in listSP)
                        {
                            int donGia = ((sp.Gia - sp.Gia * sp.KhuyenMai / 100 + 500) / 1000) * 1000;
                            tongTien += donGia * sp.SoLuong;
                        }

                        // 🔹 3. Tạo đơn hàng mới
                        string queryDonHang = @"
                    INSERT INTO DonHang (MaKhach, NgayDat, TongTien, TrangThai)
                    VALUES (@MaKhach, @NgayDat, @TongTien, 'Chờ xác nhận');
                    SELECT LAST_INSERT_ID();";

                        using (var cmd = new MySqlCommand(queryDonHang, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@MaKhach", maKhach);
                            cmd.Parameters.AddWithValue("@NgayDat", DateTime.Now);
                            cmd.Parameters.AddWithValue("@TongTien", tongTien);
                            maDH = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        // 🔹 4. Thêm chi tiết đơn hàng + cập nhật tồn kho
                        string queryCT = @"
                    INSERT INTO ChiTietDonHang (MaDH, MaSP, SoLuong, DonGia)
                    VALUES (@MaDH, @MaSP, @SoLuong, @DonGia);";

                        string queryUpdateTonKho = @"
                    UPDATE SanPham 
                    SET SoLuong = SoLuong - @SoLuong
                    WHERE MaSP = @MaSP;";

                        foreach (var sp in listSP)
                        {
                            int donGia = ((sp.Gia - sp.Gia * sp.KhuyenMai / 100 + 500) / 1000) * 1000;

                            using (var cmdCT = new MySqlCommand(queryCT, conn, transaction))
                            {
                                cmdCT.Parameters.AddWithValue("@MaDH", maDH);
                                cmdCT.Parameters.AddWithValue("@MaSP", sp.MaSP);
                                cmdCT.Parameters.AddWithValue("@SoLuong", sp.SoLuong);
                                cmdCT.Parameters.AddWithValue("@DonGia", donGia);
                                cmdCT.ExecuteNonQuery();
                            }

                            using (var cmdUpdate = new MySqlCommand(queryUpdateTonKho, conn, transaction))
                            {
                                cmdUpdate.Parameters.AddWithValue("@SoLuong", sp.SoLuong);
                                cmdUpdate.Parameters.AddWithValue("@MaSP", sp.MaSP);
                                cmdUpdate.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception($"Đặt hàng thất bại: {ex.Message}");
                    }
                }
            }

            return maDH;
        }



        public static int BanHangNVQ(List<SanPhamDTO> listSP)
        {
            if (listSP == null || listSP.Count == 0)
                throw new Exception("Danh sách sản phẩm trống.");

            int maDH = 0;

            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // 🔹 1. Kiểm tra tồn kho
                        foreach (var sp in listSP)
                        {
                            string queryCheck = "SELECT SoLuong, TenSP FROM SanPham WHERE MaSP = @MaSP";
                            using (var cmdCheck = new MySqlCommand(queryCheck, conn, transaction))
                            {
                                cmdCheck.Parameters.AddWithValue("@MaSP", sp.MaSP);

                                using (var reader = cmdCheck.ExecuteReader())
                                {
                                    if (!reader.Read())
                                        throw new Exception($"Sản phẩm có mã {sp.MaSP} không tồn tại.");

                                    int tonKho = Convert.ToInt32(reader["SoLuong"]);
                                    string tenSP = reader["TenSP"].ToString();

                                    if (tonKho < sp.SoLuong)
                                    {
                                        reader.Close();
                                        throw new Exception($"Sản phẩm '{tenSP}' chỉ còn {tonKho} sản phẩm trong kho.");
                                    }
                                }
                            }
                        }

                        // 🔹 2. Tính tổng tiền
                        int tongTien = 0;
                        foreach (var sp in listSP)
                        {
                            int donGia = ((sp.Gia - sp.Gia * sp.KhuyenMai / 100 + 500) / 1000) * 1000;
                            tongTien += donGia * sp.SoLuong;
                        }

                        // 🔹 3. Tạo đơn hàng mới
                        string queryDonHang = @"
                            INSERT INTO DonHang (MaKhach, NgayDat, TongTien, TrangThai)
                            VALUES (NULL, @NgayDat, @TongTien, 'Hoàn tất');
                            SELECT LAST_INSERT_ID();";

                        using (var cmd = new MySqlCommand(queryDonHang, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@NgayDat", DateTime.Now);
                            cmd.Parameters.AddWithValue("@TongTien", tongTien);
                            maDH = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        // 🔹 4. Thêm chi tiết đơn hàng + trừ tồn kho
                        string queryCT = @"
                            INSERT INTO ChiTietDonHang (MaDH, MaSP, SoLuong, DonGia)
                            VALUES (@MaDH, @MaSP, @SoLuong, @DonGia);";

                        string queryUpdateTonKho = @"
                            UPDATE SanPham 
                            SET SoLuong = SoLuong - @SoLuong
                            WHERE MaSP = @MaSP;";

                        foreach (var sp in listSP)
                        {
                            int donGia = ((sp.Gia - sp.Gia * sp.KhuyenMai / 100 + 500) / 1000) * 1000;

                            // Chi tiết đơn hàng
                            using (var cmdCT = new MySqlCommand(queryCT, conn, transaction))
                            {
                                cmdCT.Parameters.AddWithValue("@MaDH", maDH);
                                cmdCT.Parameters.AddWithValue("@MaSP", sp.MaSP);
                                cmdCT.Parameters.AddWithValue("@SoLuong", sp.SoLuong);
                                cmdCT.Parameters.AddWithValue("@DonGia", donGia);
                                cmdCT.ExecuteNonQuery();
                            }

                            // Giảm tồn kho
                            using (var cmdUpdate = new MySqlCommand(queryUpdateTonKho, conn, transaction))
                            {
                                cmdUpdate.Parameters.AddWithValue("@SoLuong", sp.SoLuong);
                                cmdUpdate.Parameters.AddWithValue("@MaSP", sp.MaSP);
                                cmdUpdate.ExecuteNonQuery();
                            }
                        }

                        // 🔹 5. Hoàn tất
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception($"Bán hàng thất bại: {ex.Message}");
                    }
                }
            }

            return maDH;
        }


        // 2️⃣.1 Tăng số lượng sản phẩm (+1)
        public static void TangSoLuong(int maTK, int maSP)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();

                // Lấy số lượng tồn kho
                int soLuongTon = 0;
                string querySLTon = "SELECT SoLuong FROM SanPham WHERE MaSP = @MaSP;";
                using (var cmdTon = new MySqlCommand(querySLTon, conn))
                {
                    cmdTon.Parameters.AddWithValue("@MaSP", maSP);
                    var result = cmdTon.ExecuteScalar();
                    soLuongTon = result == null ? 0 : Convert.ToInt32(result);
                }

                // Lấy số lượng hiện tại trong giỏ
                int soLuongTrongGio = 0;
                string querySLGio = @"
            SELECT ctgh.SoLuong
            FROM ChiTietGioHang ctgh
            JOIN GioHang gh ON ctgh.MaGH = gh.MaGH
            WHERE gh.MaTK = @MaTK AND ctgh.MaSP = @MaSP;";
                using (var cmdGio = new MySqlCommand(querySLGio, conn))
                {
                    cmdGio.Parameters.AddWithValue("@MaTK", maTK);
                    cmdGio.Parameters.AddWithValue("@MaSP", maSP);
                    var result = cmdGio.ExecuteScalar();
                    soLuongTrongGio = result == null ? 0 : Convert.ToInt32(result);
                }

                if (soLuongTrongGio + 1 > soLuongTon)
                    throw new Exception("Sản phẩm trong kho không đủ số lượng!");

                // Tăng +1
                string queryUpdate = @"
            UPDATE ChiTietGioHang ctgh
            JOIN GioHang gh ON ctgh.MaGH = gh.MaGH
            SET ctgh.SoLuong = ctgh.SoLuong + 1
            WHERE gh.MaTK = @MaTK AND ctgh.MaSP = @MaSP;";
                using (var cmdUp = new MySqlCommand(queryUpdate, conn))
                {
                    cmdUp.Parameters.AddWithValue("@MaTK", maTK);
                    cmdUp.Parameters.AddWithValue("@MaSP", maSP);
                    cmdUp.ExecuteNonQuery();
                }
            }
        }

        // 2️⃣.2 Giảm số lượng sản phẩm (-1)
        public static void GiamSoLuong(int maTK, int maSP)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();

                // Lấy số lượng hiện tại
                int soLuongTrongGio = 0;
                string querySL = @"
                    SELECT ctgh.SoLuong
                    FROM ChiTietGioHang ctgh
                    JOIN GioHang gh ON ctgh.MaGH = gh.MaGH
                    WHERE gh.MaTK = @MaTK AND ctgh.MaSP = @MaSP;";
                using (var cmd = new MySqlCommand(querySL, conn))
                {
                    cmd.Parameters.AddWithValue("@MaTK", maTK);
                    cmd.Parameters.AddWithValue("@MaSP", maSP);
                    var result = cmd.ExecuteScalar();
                    soLuongTrongGio = result == null ? 0 : Convert.ToInt32(result);
                }

                if (soLuongTrongGio <= 1)
                    throw new Exception("Số lượng tối thiểu là 1!");

                // Giảm -1
                string queryUpdate = @"
            UPDATE ChiTietGioHang ctgh
            JOIN GioHang gh ON ctgh.MaGH = gh.MaGH
            SET ctgh.SoLuong = ctgh.SoLuong - 1
            WHERE gh.MaTK = @MaTK AND ctgh.MaSP = @MaSP;";
                using (var cmdUp = new MySqlCommand(queryUpdate, conn))
                {
                    cmdUp.Parameters.AddWithValue("@MaTK", maTK);
                    cmdUp.Parameters.AddWithValue("@MaSP", maSP);
                    cmdUp.ExecuteNonQuery();
                }
            }
        }

        public static decimal[] TinhTongTienGioHang(int maTK)
        {
            string query = @"
                SELECT 
                SUM(ct.SoLuong * sp.Gia) AS TongTruocKM,
                SUM(ct.SoLuong * (sp.Gia - (sp.Gia * sp.KhuyenMai / 100))) AS TongSauKM
                FROM ChiTietGioHang ct
                JOIN GioHang gh ON ct.MaGH = gh.MaGH
                JOIN SanPham sp ON ct.MaSP = sp.MaSP
                WHERE gh.MaTK = @MaTK;
            ";

            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaTK", maTK);

                        using (var reader = cmd.ExecuteReader())
                        {
                            decimal tongTruocKM = 0;
                            decimal tongSauKM = 0;

                            if (reader.Read())
                            {
                                tongTruocKM = reader["TongTruocKM"] != DBNull.Value
                                    ? Convert.ToDecimal(reader["TongTruocKM"])
                                    : 0;

                                tongSauKM = reader["TongSauKM"] != DBNull.Value
                                    ? Convert.ToDecimal(reader["TongSauKM"])
                                    : 0;
                            }

                            // 🔹 Làm tròn đến hàng nghìn
                            tongTruocKM = Math.Round(tongTruocKM / 1000m, 0) * 1000;
                            tongSauKM = Math.Round(tongSauKM / 1000m, 0) * 1000;

                            return new decimal[] { tongTruocKM, tongSauKM };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi DAL - TinhTongTienGioHang: {ex.Message}", ex);
            }
        }

    }
}

