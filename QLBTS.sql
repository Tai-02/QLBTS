CREATE DATABASE QLBTS;
USE QLBTS;

CREATE TABLE TaiKhoan (
    MaTK INT AUTO_INCREMENT PRIMARY KEY,
    TenDangNhap VARCHAR(100) UNIQUE NOT NULL,
    MatKhau VARCHAR(255) NOT NULL,
    Email VARCHAR(100) UNIQUE NOT NULL,
    Otp VARCHAR(50),
    VaiTro ENUM('Admin', 'NhanVien', 'GiaoHang', 'KhachHang') NOT NULL,
    Active BIT DEFAULT 0,
    NgayTao DATETIME DEFAULT CURRENT_TIMESTAMP,
    NgayKichHoat DATETIME,
    LanDangNhapCuoi DATETIME,
    LevelID INT DEFAULT 0  -- (0: khách hàng, 1: admin, 2: nhân viên quầy, 3: giao hàng)
);

CREATE TABLE NhanVien (
    MaNV INT AUTO_INCREMENT PRIMARY KEY,
    MaTK INT NOT NULL,
    HoTen VARCHAR(100) NOT NULL,
    SDT VARCHAR(15),
    DiaChi VARCHAR(255),
    TrangThai ENUM('Hoạt động', 'Ngưng') DEFAULT 'Hoạt động',
    FOREIGN KEY (MaTK) REFERENCES TaiKhoan(MaTK)
);

CREATE TABLE KhachHang (
    MaKH INT AUTO_INCREMENT PRIMARY KEY,
    MaTK INT NOT NULL,
    HoTen VARCHAR(100) NOT NULL,
    SDT VARCHAR(15),
    DiaChi VARCHAR(255),
    AnhKH LONGBLOB,
    FOREIGN KEY (MaTK) REFERENCES TaiKhoan(MaTK)
);

CREATE TABLE SanPham (
    MaSP INT AUTO_INCREMENT PRIMARY KEY,
    TenSP VARCHAR(100) NOT NULL,
    Size ENUM('M', 'L') NOT NULL,
    SoLuong INT DEFAULT 0,
    Gia INT NOT NULL,
    KhuyenMai INT DEFAULT 0,
    -- HinhAnh LONGBLOB,
    HinhAnh VARCHAR(255),
    TrangThai ENUM('Còn hàng', 'Hết hàng')
        GENERATED ALWAYS AS (
            CASE WHEN SoLuong > 0 THEN 'Còn hàng' ELSE 'Hết hàng' END
        ) STORED
);

INSERT INTO SanPham (TenSP, Size, SoLuong, Gia, KhuyenMai, HinhAnh)
VALUES
-- Nhóm 1: Trà sữa Best Seller
('Trà sữa trân châu', 'M', 50, 40000, 0, 'https://raw.githubusercontent.com/hoangyennewit/QLHINHANH/main/trasuatranchau.png'),
('Trà sữa thái xanh', 'M', 40, 45000, 0, 'https://raw.githubusercontent.com/hoangyennewit/QLHINHANH/main/trasuathaixanh.png'),
('Trà sữa Matcha', 'M', 30, 35000, 0, 'https://raw.githubusercontent.com/hoangyennewit/QLHINHANH/main/trasuamatcha.png'),
('Trà sữa bạc hà', 'M', 25, 35000, 0, 'https://raw.githubusercontent.com/hoangyennewit/QLHINHANH/main/trasuabacha.png'),
('Trà sữa xoài kem cheese', 'M', 20, 40000, 0, 'https://raw.githubusercontent.com/hoangyennewit/QLHINHANH/main/trasuaxoaikemcheese.png'),
-- Nhóm 2: Trà sữa khác

('Trà sữa kem trứng nướng', 'M', 15, 35000, 0, 'https://raw.githubusercontent.com/hoangyennewit/QLHINHANH/main/trasuakemtrungnuong.jpg'),
('Trà sữa khoai môn', 'M', 20, 40000, 0, 'https://raw.githubusercontent.com/hoangyennewit/QLHINHANH/main/trasuakhoaimon.jpg'),
('Trà sữa Oreo Cake Cream', 'M', 18, 35000, 0, 'https://raw.githubusercontent.com/hoangyennewit/QLHINHANH/main/trasuaoreocakecream.jpg'),
('Trà sữa matcha đậu đỏ', 'M', 10, 40000, 0, 'https://raw.githubusercontent.com/hoangyennewit/QLHINHANH/main/trasuamatchadaudo.jpg'),
('Trà sữa Pudding đậu đỏ', 'M', 12, 40000, 0, 'https://raw.githubusercontent.com/hoangyennewit/QLHINHANH/main/trasuapuddingdaudo.jpg'),

-- Nhóm 3: Trà trái cây
('Trà đào cam sả', 'M', 25, 35000, 0, 'https://raw.githubusercontent.com/hoangyennewit/QLHINHANH/main/tradaocamsa.jpg'),
('Nước dừa', 'M', 30, 40000, 0, 'https://raw.githubusercontent.com/hoangyennewit/QLHINHANH/main/nuocdua.jpg'),
('Trà trái cây nhiệt đới', 'M', 22, 40000, 0, 'https://raw.githubusercontent.com/hoangyennewit/QLHINHANH/main/tratraicaynhietdoi.jpg'),
-- Nhóm 4: Topping
('Kem Sữa', 'M', 100, 10000, 0, 'https://raw.githubusercontent.com/hoangyennewit/QLHINHANH/main/kemsua.jpg'),
('Trân Châu Đen', 'M', 120, 10000, 0, 'https://raw.githubusercontent.com/hoangyennewit/QLHINHANH/main/tranchauden.jpg'),
('Trân Châu Trắng', 'M', 90, 10000, 0, 'https://raw.githubusercontent.com/hoangyennewit/QLHINHANH/main/tranchautrang.jpg'),
('Sương Sáo', 'M', 85, 10000, 0, 'https://raw.githubusercontent.com/hoangyennewit/QLHINHANH/main/suongsao.jpg'),
('Đậu đỏ', 'M', 80, 10000, 0, 'https://raw.githubusercontent.com/hoangyennewit/QLHINHANH/main/daudo.jpg');


CREATE TABLE DonHang (
    MaDH INT AUTO_INCREMENT PRIMARY KEY,
    MaKH INT,
    MaNVQuay INT,
    MaNVGiao INT,
    NgayDat DATETIME DEFAULT CURRENT_TIMESTAMP,
    TongTien INT,
    TrangThai ENUM(
        'Chờ xác nhận',
        'Đã xác nhận',
        'Đang pha chế',
        'Chờ giao hàng',
        'Đang giao',
        'Đã giao',
        'Đã hủy'
    ) DEFAULT 'Chờ xác nhận',
    FOREIGN KEY (MaKH) REFERENCES KhachHang(MaKH),
    FOREIGN KEY (MaNVQuay) REFERENCES NhanVien(MaNV),
    FOREIGN KEY (MaNVGiao) REFERENCES NhanVien(MaNV)
);

CREATE TABLE ChiTietDonHang (
    MaCT INT AUTO_INCREMENT PRIMARY KEY,
    MaDH INT,
    MaSP INT,
    SoLuong INT,
    DonGia INT,
    FOREIGN KEY (MaDH) REFERENCES DonHang(MaDH),
    FOREIGN KEY (MaSP) REFERENCES SanPham(MaSP)
);

CREATE TABLE GioHang (
    MaGH INT AUTO_INCREMENT PRIMARY KEY,
    MaKH INT NULL,
    MaNV INT NULL,
    NgayTao DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (MaKH) REFERENCES KhachHang(MaKH),
    FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV)
);

CREATE TABLE ChiTietGioHang (
    MaCTGH INT AUTO_INCREMENT PRIMARY KEY,
    MaGH INT NOT NULL,
    MaSP INT NOT NULL,
    SoLuong INT DEFAULT 1,
    FOREIGN KEY (MaGH) REFERENCES GioHang(MaGH),
    FOREIGN KEY (MaSP) REFERENCES SanPham(MaSP)
);

CREATE TABLE LichSuMuaHang (
    MaLSMH INT AUTO_INCREMENT PRIMARY KEY,
    MaKH INT NOT NULL,
    MaDH INT NOT NULL,
    TongTien INT NOT NULL,
    ThoiGian DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (MaKH) REFERENCES KhachHang(MaKH),
    FOREIGN KEY (MaDH) REFERENCES DonHang(MaDH)
);

CREATE TABLE LichSuBanHang (
    MaLSBH INT AUTO_INCREMENT PRIMARY KEY,
    MaNVQuay INT NOT NULL,
    MaDH INT NOT NULL,
    TongTien INT NOT NULL,
    ThoiGian DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (MaNVQuay) REFERENCES NhanVien(MaNV),
    FOREIGN KEY (MaDH) REFERENCES DonHang(MaDH)
);

CREATE TABLE LichSuGiaoHang (
    MaLSGH INT AUTO_INCREMENT PRIMARY KEY,
    MaNVGiao INT NOT NULL,
    MaDH INT NOT NULL,
    TongTien INT NOT NULL,
    TrangThai ENUM('Đã giao', 'Đã hủy') DEFAULT 'Đã giao',
    ThoiGian DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (MaNVGiao) REFERENCES NhanVien(MaNV),
    FOREIGN KEY (MaDH) REFERENCES DonHang(MaDH)
);

CREATE TABLE ThongKeDoanhThu (
    MaTKDT INT AUTO_INCREMENT PRIMARY KEY,
    Ngay DATE NOT NULL UNIQUE,
    TongDoanhThu INT
);
