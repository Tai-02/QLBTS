#nullable disable
using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using QLBTS_DTO;

namespace QLBTS_DAL
{
    public class NhanVienDAL
    {
        public List<NhanVienDTO> GetTatCaNhanVien()
        {
            var danhSachNhanVien = new List<NhanVienDTO>();
            string query = @"
                SELECT nv.MaNV, tk.MaTK, nv.HoTen, tk.TenDangNhap, tk.MatKhau, tk.Email, nv.SDT, tk.VaiTro 
                FROM NhanVien nv
                JOIN TaiKhoan tk ON nv.MaTK = tk.MaTK";

            DataTable dt = DataProvider.ExecuteQuery(query);
            foreach (DataRow row in dt.Rows)
            {
                danhSachNhanVien.Add(new NhanVienDTO
                {
                    MaNV = Convert.ToInt32(row["MaNV"]),
                    MaTK = Convert.ToInt32(row["MaTK"]),
                    HoTen = row["HoTen"].ToString(),
                    TenDangNhap = row["TenDangNhap"].ToString(),
                    MatKhau = row["MatKhau"].ToString(),
                    Email = row["Email"].ToString(),
                    SDT = row["SDT"].ToString(),
                    VaiTro = row["VaiTro"].ToString()
                });
            }
            return danhSachNhanVien;
        }

        public bool ThemNhanVien(NhanVienDTO nv)
        {
            using (MySqlConnection conn = DataProvider.GetConnection())
            {
                conn.Open();
                MySqlTransaction transaction = conn.BeginTransaction();
                try
                {
                    // 1. Thêm vào bảng TaiKhoan
                    string queryTK = "INSERT INTO TaiKhoan (TenDangNhap, MatKhau, Email, VaiTro, Active, NgayKichHoat) VALUES (@TenDN, @MK, @Email, @VaiTro, 1, NOW()); SELECT LAST_INSERT_ID();";
                    MySqlCommand cmdTK = new MySqlCommand(queryTK, conn, transaction);
                    cmdTK.Parameters.AddWithValue("@TenDN", nv.TenDangNhap);
                    cmdTK.Parameters.AddWithValue("@MK", nv.MatKhau);
                    cmdTK.Parameters.AddWithValue("@Email", nv.Email);
                    cmdTK.Parameters.AddWithValue("@VaiTro", nv.VaiTro);

                    int maTK = Convert.ToInt32(cmdTK.ExecuteScalar());

                    // 2. Thêm vào bảng NhanVien
                    string queryNV = "INSERT INTO NhanVien (MaTK, HoTen, SDT, DiaChi) VALUES (@MaTK, @HoTen, @SDT, NULL);";
                    MySqlCommand cmdNV = new MySqlCommand(queryNV, conn, transaction);
                    cmdNV.Parameters.AddWithValue("@MaTK", maTK);
                    cmdNV.Parameters.AddWithValue("@HoTen", nv.HoTen);
                    cmdNV.Parameters.AddWithValue("@SDT", nv.SDT);

                    cmdNV.ExecuteNonQuery();

                    transaction.Commit();
                    return true;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw; // Ném lại lỗi để lớp GUI có thể bắt
                }
            }
        }

        public bool CapNhatNhanVien(NhanVienDTO nv)
        {
            using (MySqlConnection conn = DataProvider.GetConnection())
            {
                conn.Open();
                MySqlTransaction transaction = conn.BeginTransaction();
                try
                {
                    // 1. Cập nhật bảng TaiKhoan
                    string queryTK = "UPDATE TaiKhoan SET TenDangNhap = @TenDN, MatKhau = @MK, Email = @Email, VaiTro = @VaiTro WHERE MaTK = @MaTK";
                    MySqlCommand cmdTK = new MySqlCommand(queryTK, conn, transaction);
                    cmdTK.Parameters.AddWithValue("@TenDN", nv.TenDangNhap);
                    cmdTK.Parameters.AddWithValue("@MK", nv.MatKhau);
                    cmdTK.Parameters.AddWithValue("@Email", nv.Email);
                    cmdTK.Parameters.AddWithValue("@VaiTro", nv.VaiTro);
                    cmdTK.Parameters.AddWithValue("@MaTK", nv.MaTK);
                    cmdTK.ExecuteNonQuery();

                    // 2. Cập nhật bảng NhanVien
                    string queryNV = "UPDATE NhanVien SET HoTen = @HoTen, SDT = @SDT WHERE MaNV = @MaNV";
                    MySqlCommand cmdNV = new MySqlCommand(queryNV, conn, transaction);
                    cmdNV.Parameters.AddWithValue("@HoTen", nv.HoTen);
                    cmdNV.Parameters.AddWithValue("@SDT", nv.SDT);
                    cmdNV.Parameters.AddWithValue("@MaNV", nv.MaNV);
                    cmdNV.ExecuteNonQuery();

                    transaction.Commit();
                    return true;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public bool XoaNhanVien(int maTK)
        {
            // Do có ON DELETE CASCADE, chỉ cần xóa trong bảng TaiKhoan
            string query = "DELETE FROM TaiKhoan WHERE MaTK = @MaTK";
            MySqlParameter[] parameters = { new MySqlParameter("@MaTK", maTK) };
            int result = DataProvider.ExecuteNonQuery(query, parameters);
            return result > 0;
        }
    }
}