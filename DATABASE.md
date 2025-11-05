CREATE DATABASE IF NOT EXISTS QLBTS;
USE QLBTS;

-- ===========================
-- BẢNG TÀI KHOẢN
-- ===========================
CREATE TABLE TaiKhoan (
    MaTK INT AUTO_INCREMENT PRIMARY KEY,
    TenDangNhap VARCHAR(100) UNIQUE NOT NULL,
    MatKhau VARCHAR(255) NOT NULL,
    Email VARCHAR(100) UNIQUE NOT NULL,
    HoTen VARCHAR(100),
    SDT VARCHAR(15),
    DiaChi VARCHAR(255),
    Anh LONGBLOB,
    Otp VARCHAR(50),
    VaiTro ENUM('Admin', 'NhanVienQuay', 'NhanVienGiao', 'KhachHang') NOT NULL DEFAULT 'KhachHang',
    Active BIT DEFAULT 0,
    NgayTao DATETIME DEFAULT CURRENT_TIMESTAMP,
    NgayKichHoat DATETIME,
    LanDangNhapCuoi DATETIME,
    LevelID INT DEFAULT 0, -- KH:0, Admin:1, NVQ:2, NVG:3
    TrangThai ENUM('Hoạt động', 'Ngưng') DEFAULT 'Hoạt động'
);

-- ===========================
-- BẢNG SẢN PHẨM
-- ===========================
CREATE TABLE SanPham (
    MaSP INT AUTO_INCREMENT PRIMARY KEY,
    TenSP VARCHAR(100) NOT NULL UNIQUE,
    LoaiSP VARCHAR(50) NOT NULL DEFAULT 'Khac',
    SoLuong INT DEFAULT 0,
    GiaM INT DEFAULT 0,
    GiaL INT DEFAULT 0,
    KhuyenMaiM INT DEFAULT 0,
    KhuyenMaiL INT DEFAULT 0,
    HinhAnh LONGBLOB,
    TrangThai ENUM('Còn hàng','Hết hàng')
        GENERATED ALWAYS AS (
            CASE WHEN SoLuong > 0 THEN 'Còn hàng' ELSE 'Hết hàng' END
        ) STORED,
    CONSTRAINT chk_gia_hop_le CHECK ((GiaM > 0) OR (GiaL > 0))
);

-- ===========================
-- BẢNG ĐƠN HÀNG
-- ===========================
CREATE TABLE DonHang (
    MaDH INT AUTO_INCREMENT PRIMARY KEY,
    MaKhach INT,
    MaNVQuay INT,
    MaNVGiao INT, 
    NgayDat DATETIME DEFAULT CURRENT_TIMESTAMP,
    TongTien INT DEFAULT 0,
    TrangThai ENUM(
        'Chờ xác nhận',
        'Đã nhận',
        'Chờ giao',
        'Đang giao',
        'Đã giao',
        'Hoàn tất',
        'Đã hủy'
    ) DEFAULT 'Chờ xác nhận',
    FOREIGN KEY (MaKhach) REFERENCES TaiKhoan(MaTK),
    FOREIGN KEY (MaNVQuay) REFERENCES TaiKhoan(MaTK),
    FOREIGN KEY (MaNVGiao) REFERENCES TaiKhoan(MaTK)
);

-- ===========================
-- CHI TIẾT ĐƠN HÀNG
-- ===========================
CREATE TABLE ChiTietDonHang (
    MaCT INT AUTO_INCREMENT PRIMARY KEY,
    MaDH INT NOT NULL,
    MaSP INT NOT NULL,
    Size ENUM('M', 'L') NOT NULL,
    SoLuong INT DEFAULT 1,
    DonGia INT NOT NULL,
    FOREIGN KEY (MaDH) REFERENCES DonHang(MaDH) ON DELETE CASCADE,
    FOREIGN KEY (MaSP) REFERENCES SanPham(MaSP)
);

-- ===========================
-- GIỎ HÀNG
-- ===========================
CREATE TABLE GioHang (
    MaGH INT AUTO_INCREMENT PRIMARY KEY,
    MaTK INT NOT NULL,
    NgayTao DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (MaTK) REFERENCES TaiKhoan(MaTK)
);

CREATE TABLE ChiTietGioHang (
    MaCTGH INT AUTO_INCREMENT PRIMARY KEY,
    MaGH INT NOT NULL,
    MaSP INT NOT NULL,
    Size ENUM('M', 'L') NOT NULL,
    SoLuong INT DEFAULT 1,
    FOREIGN KEY (MaGH) REFERENCES GioHang(MaGH) ON DELETE CASCADE,
    FOREIGN KEY (MaSP) REFERENCES SanPham(MaSP)
);

-- ===========================
-- LỊCH SỬ HOẠT ĐỘNG
-- ===========================
CREATE TABLE LichSuHoatDong (
    MaLS INT AUTO_INCREMENT PRIMARY KEY,
    MaTK INT,
    MaDH INT,
    TongTien INT,    
    ThoiGian DATETIME,
    LoaiLichSu ENUM('MuaHang', 'BanHang', 'GiaoHang'),
    MoTa VARCHAR(255),
    FOREIGN KEY (MaTK) REFERENCES TaiKhoan(MaTK),
    FOREIGN KEY (MaDH) REFERENCES DonHang(MaDH)
);

-- ===========================
-- THỐNG KÊ DOANH THU + NHÂN VIÊN
-- ===========================
CREATE TABLE ThongKeDoanhThu (
    MaTKDT INT AUTO_INCREMENT PRIMARY KEY,
    Ngay DATE NOT NULL UNIQUE,
    TongDoanhThu INT DEFAULT 0
);

CREATE TABLE ThongKeNhanVien (
    MaTKDTNV INT AUTO_INCREMENT PRIMARY KEY,
    MaNV INT NOT NULL,
    Ngay DATE NOT NULL,
    DoanhThu INT DEFAULT 0,
    FOREIGN KEY (MaNV) REFERENCES TaiKhoan(MaTK)
);

-- ===========================
-- TRIGGER 1: CẬP NHẬT DOANH THU
-- ===========================
DELIMITER //
CREATE TRIGGER trg_CapNhatThongKeNhanVienSauKhiHoanTat
AFTER UPDATE ON DonHang
FOR EACH ROW
BEGIN
    IF NEW.TrangThai = 'Hoàn tất' AND OLD.TrangThai <> 'Hoàn tất' THEN
        -- Cập nhật tổng doanh thu ngày
        IF EXISTS (SELECT 1 FROM ThongKeDoanhThu WHERE Ngay = CURDATE()) THEN
            UPDATE ThongKeDoanhThu
            SET TongDoanhThu = TongDoanhThu + NEW.TongTien
            WHERE Ngay = CURDATE();
        ELSE
            INSERT INTO ThongKeDoanhThu (Ngay, TongDoanhThu)
            VALUES (CURDATE(), NEW.TongTien);
        END IF;

        -- Cập nhật doanh thu nhân viên quầy
        IF NEW.MaNVQuay IS NOT NULL THEN
            IF EXISTS (SELECT 1 FROM ThongKeNhanVien WHERE MaNV = NEW.MaNVQuay AND Ngay = CURDATE()) THEN
                UPDATE ThongKeNhanVien
                SET DoanhThu = DoanhThu + NEW.TongTien
                WHERE MaNV = NEW.MaNVQuay AND Ngay = CURDATE();
            ELSE
                INSERT INTO ThongKeNhanVien (MaNV, Ngay, DoanhThu)
                VALUES (NEW.MaNVQuay, CURDATE(), NEW.TongTien);
            END IF;
        END IF;
    END IF;
END //
DELIMITER ;

-- ===========================
-- TRIGGER 2: GIỚI HẠN SỐ LƯỢNG KHI CẬP NHẬT SẢN PHẨM
-- ===========================
DELIMITER //
CREATE TRIGGER trg_CapNhatSL_SanPhamMoi
AFTER UPDATE ON SanPham
FOR EACH ROW
BEGIN
    -- Giỏ hàng
    UPDATE ChiTietGioHang AS ctgh
    JOIN GioHang AS gh ON ctgh.MaGH = gh.MaGH
    SET ctgh.SoLuong = NEW.SoLuong
    WHERE ctgh.MaSP = NEW.MaSP
      AND ctgh.SoLuong > NEW.SoLuong;

    -- Đơn hàng đang chờ xác nhận
    UPDATE ChiTietDonHang AS ctdh
    JOIN DonHang AS dh ON ctdh.MaDH = dh.MaDH
    SET ctdh.SoLuong = NEW.SoLuong
    WHERE ctdh.MaSP = NEW.MaSP
      AND dh.TrangThai = 'Chờ xác nhận'
      AND ctdh.SoLuong > NEW.SoLuong;
END //
DELIMITER ;
DELIMITER //
CREATE TRIGGER trg_XoaSPTrongGioHangKhiHetHang
AFTER UPDATE ON SanPham
FOR EACH ROW
BEGIN
    -- Chỉ thực hiện khi số lượng = 0
    IF NEW.SoLuong = 0 THEN
        -- Xóa sản phẩm khỏi tất cả giỏ hàng
        DELETE FROM ChiTietGioHang
        WHERE MaSP = NEW.MaSP;
    END IF;
END //
DELIMITER ;


INSERT INTO TaiKhoan (TenDangNhap, MatKhau, Email, HoTen, SDT, DiaChi, VaiTro, Active, LevelID)
VALUES 
('admin', 'afb295c95be7ff3276b9a4fdfe593b8c0b8271861ca9dda80e86dae11db46c91', 'admin@example.com', 'Nguyen Van Admin', '0912345678', 'Hanoi', 'Admin', 1, 1);

-- ===========================
-- KIỂM TRA
-- ===========================
SELECT * FROM TaiKhoan;
SELECT * FROM SanPham;
SELECT * FROM DonHang;
SELECT * FROM ChiTietDonHang;
SELECT * FROM GioHang;
SELECT * FROM ChiTietGioHang;
SELECT * FROM LichSuHoatDong;
SELECT * FROM ThongKeNhanVien;
SHOW TRIGGERS;
