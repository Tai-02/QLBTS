using System.Collections.Generic;
using MySql.Data.MySqlClient;
using QLBTS_DTO;

namespace QLBTS_DAL
{
    public class GioHangDAL
    {
        // 🔹 1️⃣ Lấy giỏ hàng theo mã tài khoản
        public static List<SanPhamDTO> LayGioHangTheoMaTK(int maTK)
        {
            List<SanPhamDTO> danhSach = new List<SanPhamDTO>();

            string query = @"
                SELECT 
                    sp.MaSP,
                    sp.TenSP,
                    sp.LoaiSP,
                    sp.SoLuong AS TonKho,
                    sp.GiaM,
                    sp.GiaL,
                    sp.KhuyenMaiM,
                    sp.KhuyenMaiL,
                    sp.HinhAnh,
                    ctgh.SoLuong,
                    ctgh.Size
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
                                LoaiSP = reader.GetString("LoaiSP"),
                                Size = reader["Size"].ToString(),
                                SoLuong = reader.GetInt32("SoLuong"),
                                GiaM = reader.GetInt32("GiaM"),
                                GiaL = reader.GetInt32("GiaL"),
                                KhuyenMaiM = reader.GetInt32("KhuyenMaiM"),
                                KhuyenMaiL = reader.GetInt32("KhuyenMaiL"),
                                HinhAnh = reader["HinhAnh"] == DBNull.Value ? null : (byte[])reader["HinhAnh"]
                            };
                            danhSach.Add(sp);
                        }
                    }
                }
            }

            return danhSach;
        }

        // 🔹 2️⃣ Thêm sản phẩm vào giỏ
        public static void ThemSanPhamVaoGio(int maTK, SanPhamDTO sp, int soLuong = 1)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        int maGH = 0;

                        // 🔹 Lấy hoặc tạo giỏ hàng
                        string queryLayGH = "SELECT MaGH FROM GioHang WHERE MaTK = @MaTK LIMIT 1;";
                        using (var cmdGH = new MySqlCommand(queryLayGH, conn, transaction))
                        {
                            cmdGH.Parameters.AddWithValue("@MaTK", maTK);
                            var result = cmdGH.ExecuteScalar();

                            if (result != null)
                                maGH = Convert.ToInt32(result);
                            else
                            {
                                string queryTaoGH = "INSERT INTO GioHang (MaTK) VALUES (@MaTK); SELECT LAST_INSERT_ID();";
                                using (var cmdTao = new MySqlCommand(queryTaoGH, conn, transaction))
                                {
                                    cmdTao.Parameters.AddWithValue("@MaTK", maTK);
                                    maGH = Convert.ToInt32(cmdTao.ExecuteScalar());
                                }
                            }
                        }

                        // 🔹 Lấy số lượng tồn kho từ bảng SanPham
                        int tonKho = 0;
                        string queryTonKho = "SELECT SoLuong FROM SanPham WHERE MaSP = @MaSP;";
                        using (var cmdTon = new MySqlCommand(queryTonKho, conn, transaction))
                        {
                            cmdTon.Parameters.AddWithValue("@MaSP", sp.MaSP);
                            var result = cmdTon.ExecuteScalar();
                            if (result == null)
                                throw new Exception("Sản phẩm không tồn tại!");
                            tonKho = Convert.ToInt32(result);
                        }

                        // 🔹 Kiểm tra xem sản phẩm + size đã có trong giỏ chưa
                        string queryLaySL = "SELECT SoLuong FROM ChiTietGioHang WHERE MaGH = @MaGH AND MaSP = @MaSP AND Size = @Size;";
                        int slHienCo = 0;

                        using (var cmdKT = new MySqlCommand(queryLaySL, conn, transaction))
                        {
                            cmdKT.Parameters.AddWithValue("@MaGH", maGH);
                            cmdKT.Parameters.AddWithValue("@MaSP", sp.MaSP);
                            cmdKT.Parameters.AddWithValue("@Size", sp.Size);
                            var result = cmdKT.ExecuteScalar();
                            if (result != null)
                                slHienCo = Convert.ToInt32(result);
                        }

                        int slMoi = slHienCo + soLuong;

                        // 🔹 Kiểm tra vượt tồn kho
                        if (slMoi > tonKho)
                            throw new Exception($"Sản phẩm vượt quá số lượng tồn kho (còn {tonKho}).");

                        if (slHienCo > 0)
                        {
                            // 🔹 Cập nhật tăng số lượng
                            string queryCapNhat = @"
                        UPDATE ChiTietGioHang
                        SET SoLuong = @SoLuongMoi
                        WHERE MaGH = @MaGH AND MaSP = @MaSP AND Size = @Size;";
                            using (var cmdUpdate = new MySqlCommand(queryCapNhat, conn, transaction))
                            {
                                cmdUpdate.Parameters.AddWithValue("@SoLuongMoi", slMoi);
                                cmdUpdate.Parameters.AddWithValue("@MaGH", maGH);
                                cmdUpdate.Parameters.AddWithValue("@MaSP", sp.MaSP);
                                cmdUpdate.Parameters.AddWithValue("@Size", sp.Size);
                                cmdUpdate.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            // 🔹 Thêm mới
                            string queryThem = @"
                        INSERT INTO ChiTietGioHang (MaGH, MaSP, Size, SoLuong)
                        VALUES (@MaGH, @MaSP, @Size, @SoLuong);";
                            using (var cmdThem = new MySqlCommand(queryThem, conn, transaction))
                            {
                                cmdThem.Parameters.AddWithValue("@MaGH", maGH);
                                cmdThem.Parameters.AddWithValue("@MaSP", sp.MaSP);
                                cmdThem.Parameters.AddWithValue("@Size", sp.Size);
                                cmdThem.Parameters.AddWithValue("@SoLuong", soLuong);
                                cmdThem.ExecuteNonQuery();
                            }
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


        // 🔹 3️⃣ Cập nhật số lượng
        public static void CapNhatSoLuong(int maTK, int maSP, string size, int soLuong)
        {
            string query = @"
                UPDATE ChiTietGioHang ctgh
                JOIN GioHang gh ON ctgh.MaGH = gh.MaGH
                SET ctgh.SoLuong = @SoLuong
                WHERE gh.MaTK = @MaTK AND ctgh.MaSP = @MaSP AND ctgh.Size = @Size;
            ";

            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SoLuong", soLuong);
                    cmd.Parameters.AddWithValue("@MaTK", maTK);
                    cmd.Parameters.AddWithValue("@MaSP", maSP);
                    cmd.Parameters.AddWithValue("@Size", size);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // 🔹 4️⃣ Xóa 1 sản phẩm khỏi giỏ
        public static void XoaSanPhamKhoiGio(int maTK, int maSP, string size)
        {
            string query = @"
                DELETE ctgh FROM ChiTietGioHang ctgh
                JOIN GioHang gh ON ctgh.MaGH = gh.MaGH
                WHERE gh.MaTK = @MaTK AND ctgh.MaSP = @MaSP AND ctgh.Size = @Size;
            ";

            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaTK", maTK);
                    cmd.Parameters.AddWithValue("@MaSP", maSP);
                    cmd.Parameters.AddWithValue("@Size", size);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // 🔹 5️⃣ Xóa toàn bộ giỏ
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

        // 🔹 6️⃣ Đặt hàng khách hàng (có size)
        public static int DatHangKH(int maKhach, List<SanPhamDTO> listSP)
        {
            if (listSP == null || listSP.Count == 0)
                throw new Exception("Danh sách sản phẩm trống.");

            int maDH = 0;

            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        int tongTien = 0;

                        // 🔹 1️⃣ Kiểm tra tồn kho và tính tổng tiền
                        foreach (var sp in listSP)
                        {
                            string queryCheck = "SELECT SoLuong, TenSP FROM SanPham WHERE MaSP = @MaSP;";
                            using (var cmd = new MySqlCommand(queryCheck, conn, tran))
                            {
                                cmd.Parameters.AddWithValue("@MaSP", sp.MaSP);
                                using (var reader = cmd.ExecuteReader())
                                {
                                    if (!reader.Read())
                                        throw new Exception($"Không tồn tại sản phẩm có mã {sp.MaSP}.");

                                    int tonKho = Convert.ToInt32(reader["SoLuong"]);
                                    string tenSP = Convert.ToString(reader["TenSP"]) ?? "";

                                    if (tonKho < sp.SoLuong)
                                        throw new Exception($"'{tenSP}' chỉ còn {tonKho} sản phẩm trong kho.");

                                    tongTien += sp.GiaHienTai * sp.SoLuong;
                                }
                            }
                        }

                        // 🔹 2️⃣ Tạo đơn hàng mới
                        string queryDH = @"
                    INSERT INTO DonHang (MaKhach, NgayDat, TongTien, TrangThai)
                    VALUES (@MaKhach, NOW(), @TongTien, 'Chờ xác nhận');
                    SELECT LAST_INSERT_ID();";
                        using (var cmd = new MySqlCommand(queryDH, conn, tran))
                        {
                            cmd.Parameters.AddWithValue("@MaKhach", maKhach);
                            cmd.Parameters.AddWithValue("@TongTien", tongTien);
                            maDH = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        // 🔹 3️⃣ Thêm chi tiết đơn hàng và trừ tồn kho
                        foreach (var sp in listSP)
                        {
                            // Chi tiết đơn hàng
                            string queryCT = @"
                        INSERT INTO ChiTietDonHang (MaDH, MaSP, Size, SoLuong, DonGia)
                        VALUES (@MaDH, @MaSP, @Size, @SoLuong, @DonGia);";
                            using (var cmd = new MySqlCommand(queryCT, conn, tran))
                            {
                                cmd.Parameters.AddWithValue("@MaDH", maDH);
                                cmd.Parameters.AddWithValue("@MaSP", sp.MaSP);
                                cmd.Parameters.AddWithValue("@Size", string.IsNullOrEmpty(sp.Size) ? DBNull.Value : (object)sp.Size);
                                cmd.Parameters.AddWithValue("@SoLuong", sp.SoLuong);
                                cmd.Parameters.AddWithValue("@DonGia", sp.GiaHienTai);
                                cmd.ExecuteNonQuery();
                            }

                            // Cập nhật tồn kho
                            string queryUpdate = "UPDATE SanPham SET SoLuong = SoLuong - @SL WHERE MaSP = @MaSP;";
                            using (var cmdU = new MySqlCommand(queryUpdate, conn, tran))
                            {
                                cmdU.Parameters.AddWithValue("@SL", sp.SoLuong);
                                cmdU.Parameters.AddWithValue("@MaSP", sp.MaSP);
                                cmdU.ExecuteNonQuery();
                            }
                        }

                        // 🔹 4️⃣ Xác nhận giao dịch
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        throw new Exception($"Đặt hàng thất bại: {ex.Message}");
                    }
                }
            }

            return maDH;
        }

        // 7 ⃣ Bán hàng NVQ (có size)
        public static int BanHangNVQ(List<SanPhamDTO> listSP)
        {
            if (listSP == null || listSP.Count == 0)
                throw new Exception("Danh sách sản phẩm trống.");

            int maDH = 0;

            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        int tongTien = 0;

                        // 🔹 1️⃣ Kiểm tra tồn kho + tính tổng tiền
                        foreach (var sp in listSP)
                        {
                            string queryCheck = "SELECT SoLuong, TenSP FROM SanPham WHERE MaSP = @MaSP;";
                            using (var cmd = new MySqlCommand(queryCheck, conn, tran))
                            {
                                cmd.Parameters.AddWithValue("@MaSP", sp.MaSP);
                                using (var reader = cmd.ExecuteReader())
                                {
                                    if (!reader.Read())
                                        throw new Exception($"Không tồn tại sản phẩm có mã {sp.MaSP}.");

                                    int tonKho = Convert.ToInt32(reader["SoLuong"]);
                                    string tenSP = Convert.ToString(reader["TenSP"]) ?? "";

                                    if (tonKho < sp.SoLuong)
                                        throw new Exception($"'{tenSP}' chỉ còn {tonKho} sản phẩm trong kho.");

                                    tongTien += sp.GiaHienTai * sp.SoLuong;
                                }
                            }
                        }

                        // 🔹 2️⃣ Tạo đơn hàng mới (nhân viên bán trực tiếp => MaKhach = NULL)
                        string queryDH = @"
                    INSERT INTO DonHang (MaKhach, NgayDat, TongTien, TrangThai)
                    VALUES (NULL, NOW(), @TongTien, 'Hoàn tất');
                    SELECT LAST_INSERT_ID();";
                        using (var cmd = new MySqlCommand(queryDH, conn, tran))
                        {
                            cmd.Parameters.AddWithValue("@TongTien", tongTien);
                            maDH = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        // 🔹 3️⃣ Thêm chi tiết đơn hàng + trừ tồn kho
                        foreach (var sp in listSP)
                        {
                            // Tính giá sau khuyến mãi và làm tròn
                            int khuyenMai = sp.KhuyenMaiHienTai;
                            int donGia = ((sp.GiaHienTai - sp.GiaHienTai * khuyenMai / 100 + 500) / 1000) * 1000;

                            // Thêm chi tiết đơn hàng
                            string queryCT = @"
                        INSERT INTO ChiTietDonHang (MaDH, MaSP, Size, SoLuong, DonGia)
                        VALUES (@MaDH, @MaSP, @Size, @SoLuong, @DonGia);";
                            using (var cmdCT = new MySqlCommand(queryCT, conn, tran))
                            {
                                cmdCT.Parameters.AddWithValue("@MaDH", maDH);
                                cmdCT.Parameters.AddWithValue("@MaSP", sp.MaSP);
                                cmdCT.Parameters.AddWithValue("@Size", string.IsNullOrEmpty(sp.Size) ? DBNull.Value : (object)sp.Size);
                                cmdCT.Parameters.AddWithValue("@SoLuong", sp.SoLuong);
                                cmdCT.Parameters.AddWithValue("@DonGia", donGia);
                                cmdCT.ExecuteNonQuery();
                            }

                            // Giảm tồn kho
                            string queryUpdate = "UPDATE SanPham SET SoLuong = SoLuong - @SL WHERE MaSP = @MaSP;";
                            using (var cmdU = new MySqlCommand(queryUpdate, conn, tran))
                            {
                                cmdU.Parameters.AddWithValue("@SL", sp.SoLuong);
                                cmdU.Parameters.AddWithValue("@MaSP", sp.MaSP);
                                cmdU.ExecuteNonQuery();
                            }
                        }

                        // 🔹 4️⃣ Hoàn tất giao dịch
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        throw new Exception($"Bán hàng thất bại: {ex.Message}");
                    }
                }
            }

            return maDH;
        }


        // 2️⃣.1 Tăng số lượng sản phẩm (+1)
        public static void TangSoLuong(int maTK, int maSP, string size)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        // 🔹 1️⃣ Lấy số lượng tồn kho sản phẩm
                        int soLuongTon = 0;
                        string queryTonKho = "SELECT SoLuong, TenSP FROM SanPham WHERE MaSP = @MaSP;";
                        string tenSP = "";

                        using (var cmd = new MySqlCommand(queryTonKho, conn, tran))
                        {
                            cmd.Parameters.AddWithValue("@MaSP", maSP);
                            using (var reader = cmd.ExecuteReader())
                            {
                                if (!reader.Read())
                                    throw new Exception("Sản phẩm không tồn tại!");
                                soLuongTon = Convert.ToInt32(reader["SoLuong"]);
                                tenSP = Convert.ToString(reader["TenSP"]) ?? "";
                            }
                        }

                        // 🔹 2️⃣ Lấy số lượng hiện tại trong giỏ theo size
                        int soLuongTrongGio = 0;
                        string querySLGio = @"
                    SELECT ctgh.SoLuong
                    FROM ChiTietGioHang ctgh
                    JOIN GioHang gh ON ctgh.MaGH = gh.MaGH
                    WHERE gh.MaTK = @MaTK AND ctgh.MaSP = @MaSP AND ctgh.Size = @Size;";

                        using (var cmd = new MySqlCommand(querySLGio, conn, tran))
                        {
                            cmd.Parameters.AddWithValue("@MaTK", maTK);
                            cmd.Parameters.AddWithValue("@MaSP", maSP);
                            cmd.Parameters.AddWithValue("@Size", size);
                            var result = cmd.ExecuteScalar();
                            soLuongTrongGio = result == null ? 0 : Convert.ToInt32(result);
                        }

                        // 🔹 3️⃣ Kiểm tra tồn kho
                        if (soLuongTrongGio + 1 > soLuongTon)
                            throw new Exception($"'{tenSP}' chỉ còn {soLuongTon} sản phẩm trong kho.");

                        // 🔹 4️⃣ Cập nhật tăng số lượng +1
                        string queryTang = @"
                    UPDATE ChiTietGioHang ctgh
                    JOIN GioHang gh ON ctgh.MaGH = gh.MaGH
                    SET ctgh.SoLuong = ctgh.SoLuong + 1
                    WHERE gh.MaTK = @MaTK AND ctgh.MaSP = @MaSP AND ctgh.Size = @Size;";

                        using (var cmd = new MySqlCommand(queryTang, conn, tran))
                        {
                            cmd.Parameters.AddWithValue("@MaTK", maTK);
                            cmd.Parameters.AddWithValue("@MaSP", maSP);
                            cmd.Parameters.AddWithValue("@Size", size);
                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        throw new Exception($"Không thể tăng số lượng: {ex.Message}");
                    }
                }
            }
        }


        // 2️⃣.2 Giảm số lượng sản phẩm (-1)
        public static void GiamSoLuong(int maTK, int maSP, string size)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        // 🔹 1️⃣ Lấy số lượng trong giỏ hiện tại theo size
                        int soLuongTrongGio = 0;
                        string querySLGio = @"
                    SELECT ctgh.SoLuong
                    FROM ChiTietGioHang ctgh
                    JOIN GioHang gh ON ctgh.MaGH = gh.MaGH
                    WHERE gh.MaTK = @MaTK AND ctgh.MaSP = @MaSP AND ctgh.Size = @Size;
                ";
                        using (var cmd = new MySqlCommand(querySLGio, conn, tran))
                        {
                            cmd.Parameters.AddWithValue("@MaTK", maTK);
                            cmd.Parameters.AddWithValue("@MaSP", maSP);
                            cmd.Parameters.AddWithValue("@Size", size);
                            var result = cmd.ExecuteScalar();
                            soLuongTrongGio = result == null ? 0 : Convert.ToInt32(result);
                        }

                        if (soLuongTrongGio <= 1)
                            throw new Exception("Số lượng tối thiểu là 1!");

                        // 🔹 2️⃣ Kiểm tra tồn kho thực tế (phòng lỗi dữ liệu)
                        int soLuongTon = 0;
                        string querySLTon = "SELECT SoLuong FROM SanPham WHERE MaSP = @MaSP;";
                        using (var cmdTon = new MySqlCommand(querySLTon, conn, tran))
                        {
                            cmdTon.Parameters.AddWithValue("@MaSP", maSP);
                            var result = cmdTon.ExecuteScalar();
                            soLuongTon = result == null ? 0 : Convert.ToInt32(result);
                        }

                        if (soLuongTon <= 0)
                            throw new Exception("Sản phẩm này hiện đã hết hàng trong kho!");

                        // 🔹 3️⃣ Giảm -1 trong giỏ hàng theo size
                        string queryUpdate = @"
                    UPDATE ChiTietGioHang ctgh
                    JOIN GioHang gh ON ctgh.MaGH = gh.MaGH
                    SET ctgh.SoLuong = ctgh.SoLuong - 1
                    WHERE gh.MaTK = @MaTK AND ctgh.MaSP = @MaSP AND ctgh.Size = @Size;
                ";
                        using (var cmdUp = new MySqlCommand(queryUpdate, conn, tran))
                        {
                            cmdUp.Parameters.AddWithValue("@MaTK", maTK);
                            cmdUp.Parameters.AddWithValue("@MaSP", maSP);
                            cmdUp.Parameters.AddWithValue("@Size", size);
                            cmdUp.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        throw new Exception($"Không thể giảm số lượng: {ex.Message}");
                    }
                }
            }
        }

        // 6️⃣ Tính tổng tiền giỏ hàng
        public static decimal[] TinhTongTienGioHang(int maTK)
        {
            string query = @"
        SELECT 
            ct.SoLuong,
            ct.Size,
            sp.GiaM,
            sp.GiaL,
            sp.KhuyenMaiM,
            sp.KhuyenMaiL
        FROM ChiTietGioHang ct
        JOIN GioHang gh ON ct.MaGH = gh.MaGH
        JOIN SanPham sp ON ct.MaSP = sp.MaSP
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
                        decimal tongTruocKM = 0, tongSauKM = 0;

                        while (reader.Read())
                        {
                            int soLuong = Convert.ToInt32(reader["SoLuong"]);
                            string size = reader["Size"].ToString();

                            decimal giaSP = size == "M" ? Convert.ToDecimal(reader["GiaM"])
                                                        : Convert.ToDecimal(reader["GiaL"]);
                            decimal km = size == "M" ? Convert.ToDecimal(reader["KhuyenMaiM"])
                                                     : Convert.ToDecimal(reader["KhuyenMaiL"]);

                            decimal giaSauKM = giaSP * (1 - km / 100);

                            // Làm tròn từng sản phẩm
                            giaSauKM = Math.Round(giaSauKM / 1000m) * 1000;

                            tongTruocKM += giaSP * soLuong;
                            tongSauKM += giaSauKM * soLuong;
                        }

                        // Tổng cuối cùng làm tròn
                        tongTruocKM = Math.Ceiling(tongTruocKM / 1000m) * 1000;
                        tongSauKM = Math.Ceiling(tongSauKM / 1000m) * 1000;

                        return new decimal[] { tongTruocKM, tongSauKM };
                    }
                }
            }
        }
    }
}

