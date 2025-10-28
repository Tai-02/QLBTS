CREATE DATABASE QLBTS;
USE QLBTS;

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
    LevelID INT DEFAULT 0, -- 0: Khách hàng, 1: Admin, 2: Nhân viên quầy, 3: Nhân viên giao
    TrangThai ENUM('Hoạt động', 'Ngưng') DEFAULT 'Hoạt động'
);

INSERT INTO TaiKhoan
(TenDangNhap, MatKhau, Email, VaiTro, Active, LevelID, HoTen)
VALUES
('Admin', 'afb295c95be7ff3276b9a4fdfe593b8c0b8271861ca9dda80e86dae11db46c91',
 'tai@gmail.com', 'Admin', 1, 1, 'Quản trị viên');


CREATE TABLE SanPham (
    MaSP INT AUTO_INCREMENT PRIMARY KEY,
    TenSP VARCHAR(100) NOT NULL,
    Size ENUM('M', 'L') NOT NULL,
    SoLuong INT DEFAULT 0,
    Gia INT NOT NULL,
    KhuyenMai INT DEFAULT 0,
    HinhAnh LONGBLOB,
    TrangThai ENUM('Còn hàng', 'Hết hàng')
        GENERATED ALWAYS AS (
            CASE WHEN SoLuong > 0 THEN 'Còn hàng' ELSE 'Hết hàng' END
        ) STORED
);

-- Tạo một vài sản phẩm mẫu (chưa có hình ảnh)
INSERT INTO SanPham (TenSP, Size, SoLuong, Gia, KhuyenMai) VALUES
('Trà sữa trân châu', 'L', 100, 40000, 0),
('Trà sữa thái xanh', 'L', 100, 45000, 0),
('Trà sữa Matcha', 'L', 100, 35000, 0),
('Trà sữa bạc hà', 'L', 100, 35000, 0),
('Trà sữa xoài kem cheese', 'L', 100, 40000, 0),

('Trà sữa kem trứng nướng', 'M', 50, 35000, 0),
('Trà sữa khoai môn', 'L', 30, 40000, 0),
('Trà sữa Oreo Cake Cream', 'M', 45, 35000, 0),
('Trà sữa matcha đậu đỏ', 'L', 20, 40000, 0),
('Trà sữa Pudding đậu đỏ', 'M', 55, 40000, 0),

('Trà đào cam sả', 'L', 100, 35000, 0),
('Nước dừa', 'L', 100, 40000, 0),
('Trà trái cây nhiệt đới', 'L', 100, 40000, 0),

('Kem sữa', 'M', 100, 10000, 0),
('Trân châu đen', 'M', 100, 10000, 0),
('Trân châu trắng', 'M', 100, 10000, 0),
('Sương sáo', 'M', 100, 10000, 0);


CREATE TABLE DonHang (
    MaDH INT AUTO_INCREMENT PRIMARY KEY,
    MaKhach INT,      -- Là MaTK của Khách hàng
    MaNVQuay INT,     -- Là MaTK của Nhân viên quầy
    MaNVGiao INT,     -- Là MaTK của Nhân viên giao hàng
    NgayDat DATETIME DEFAULT CURRENT_TIMESTAMP,
    TongTien INT DEFAULT 0,
    TrangThai ENUM(
        'Chờ xác nhận',
        'Đã xác nhận',
        'Đang pha chế',
        'Chờ giao hàng',
        'Đang giao',
        'Đã giao',
        'Đã hủy'
    ) DEFAULT 'Chờ xác nhận',
    FOREIGN KEY (MaKhach) REFERENCES TaiKhoan(MaTK),
    FOREIGN KEY (MaNVQuay) REFERENCES TaiKhoan(MaTK),
    FOREIGN KEY (MaNVGiao) REFERENCES TaiKhoan(MaTK)
);


CREATE TABLE ChiTietDonHang (
    MaCT INT AUTO_INCREMENT PRIMARY KEY,
    MaDH INT NOT NULL,
    MaSP INT NOT NULL,
    SoLuong INT DEFAULT 1,
    DonGia INT NOT NULL,
    FOREIGN KEY (MaDH) REFERENCES DonHang(MaDH) ON DELETE CASCADE,
    FOREIGN KEY (MaSP) REFERENCES SanPham(MaSP)
);


CREATE TABLE GioHang (
    MaGH INT AUTO_INCREMENT PRIMARY KEY,
    MaTK INT NOT NULL,   -- có thể là khách hoặc nhân viên
    NgayTao DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (MaTK) REFERENCES TaiKhoan(MaTK)
);

CREATE TABLE ChiTietGioHang (
    MaCTGH INT AUTO_INCREMENT PRIMARY KEY,
    MaGH INT NOT NULL,
    MaSP INT NOT NULL,
    SoLuong INT DEFAULT 1,
    FOREIGN KEY (MaGH) REFERENCES GioHang(MaGH) ON DELETE CASCADE,
    FOREIGN KEY (MaSP) REFERENCES SanPham(MaSP)
);

CREATE TABLE LichSuHoatDong (
    MaLS INT AUTO_INCREMENT PRIMARY KEY,
    MaTK INT,
    MaDH INT,
    TongTien INT,
    TrangThai VARCHAR(50),
    ThoiGian DATETIME,
    LoaiLichSu ENUM('MuaHang', 'BanHang', 'GiaoHang'),
    FOREIGN KEY (MaTK) REFERENCES TaiKhoan(MaTK),
    FOREIGN KEY (MaDH) REFERENCES DonHang(MaDH)
);


CREATE TABLE ThongKeDoanhThu (
    MaTKDT INT AUTO_INCREMENT PRIMARY KEY,
    Ngay DATE NOT NULL UNIQUE,
    TongDoanhThu INT DEFAULT 0
);

