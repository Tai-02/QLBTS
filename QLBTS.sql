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

-- CREATE TABLE SANPHAM (
--     MaSP CHAR(5) PRIMARY KEY,
--     TenSP VARCHAR(100) NOT NULL,
--     LoaiSP VARCHAR(50),
--     Size ENUM('M', 'L'),
--     Gia DECIMAL(10,2),
--     TrangThaiGiao VARCHAR(50)
-- );

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

-- ✅ Thêm dữ liệu mẫu vào bảng SanPham mới
INSERT INTO SanPham (TenSP, Size, SoLuong, Gia, KhuyenMai, HinhAnh)
VALUES
-- 🧋 Nhóm 1: Trà sữa Best Seller
('Trà sữa trân châu', 'M', 50, 40000, 0, 'https://raw.githubusercontent.com/hoangyennewit/QLHINHANH/main/trasuatranchau.png'),
('Trà sữa thái xanh', 'M', 40, 45000, 0, 'https://raw.githubusercontent.com/hoangyennewit/QLHINHANH/main/trasuathaixanh.png'),
('Trà sữa Matcha', 'M', 30, 35000, 0, 'https://raw.githubusercontent.com/hoangyennewit/QLHINHANH/main/trasuamatcha.png'),
('Trà sữa bạc hà', 'M', 25, 35000, 0, 'https://raw.githubusercontent.com/hoangyennewit/QLHINHANH/main/trasuabacha.png'),
('Trà sữa xoài kem cheese', 'M', 20, 40000, 0, 'https://raw.githubusercontent.com/hoangyennewit/QLHINHANH/main/trasuaxoaikemcheese.png');
-- 🧋 Nhóm 2: Trà sữa khác
/*
('Trà sữa kem trứng nướng', 'M', 15, 35000, 0, LOAD_FILE('D:\HK5\HK5-NHAP MON CONG NGHE PHAN MEM\QLBTS HinhAnh\Mon moi\trasuakemtrungnuong.jpg')),
('Trà sữa khoai môn', 'M', 20, 40000, 0, LOAD_FILE('D:\HK5\HK5-NHAP MON CONG NGHE PHAN MEM\QLBTS HinhAnh\Mon moi\trasuakhoaimon.jpg')),
('Trà sữa Oreo Cake Cream', 'M', 18, 35000, 0, LOAD_FILE('D:\HK5\HK5-NHAP MON CONG NGHE PHAN MEM\QLBTS HinhAnh\Mon moi\trasuaoreocakecream.jpg')),
('Trà sữa matcha đậu đỏ', 'M', 10, 40000, 0, LOAD_FILE('"D:\HK5\HK5-NHAP MON CONG NGHE PHAN MEM\QLBTS HinhAnh\Mon moi\trasuamatchadaudo.jpg"')),
('Trà sữa Pudding đậu đỏ', 'M', 12, 40000, 0, LOAD_FILE('"D:\HK5\HK5-NHAP MON CONG NGHE PHAN MEM\QLBTS HinhAnh\Mon moi\trasuapuddingdaudo.jpg"')),

-- 🍑 Nhóm 3: Trà trái cây
('Trà đào cam sả', 'M', 25, 35000, 0, LOAD_FILE('D:\HK5\HK5-NHAP MON CONG NGHE PHAN MEM\QLBTS HinhAnh\Tra trai cay\tradaocamsa.jpg')),
('Nước dừa', 'M', 30, 40000, 0, LOAD_FILE('D:\HK5\HK5-NHAP MON CONG NGHE PHAN MEM\QLBTS HinhAnh\Tra trai cay\nuocdua.jpg')),
('Trà trái cây nhiệt đới', 'M', 22, 40000, 0, LOAD_FILE('"D:\HK5\HK5-NHAP MON CONG NGHE PHAN MEM\QLBTS HinhAnh\Tra trai cay\tratraicaynhietdoi.jpg"')),
-- 🍡 Nhóm 4: Topping
('Kem Sữa', 'M', 100, 10000, 0, LOAD_FILE('D:\HK5\HK5-NHAP MON CONG NGHE PHAN MEM\QLBTS HinhAnh\Toping\kemsua.jpg')),
('Trân Châu Đen', 'M', 120, 10000, 0, LOAD_FILE('"D:\HK5\HK5-NHAP MON CONG NGHE PHAN MEM\QLBTS HinhAnh\Toping\tranchauden.jpg"')),
('Trân Châu Trắng', 'M', 90, 10000, 0, LOAD_FILE('"D:\HK5\HK5-NHAP MON CONG NGHE PHAN MEM\QLBTS HinhAnh\Toping\tranchautrang.jpg"')),
('Sương Sáo', 'M', 85, 10000, 0, LOAD_FILE('"D:\HK5\HK5-NHAP MON CONG NGHE PHAN MEM\QLBTS HinhAnh\Toping\suongsao.jpg"')),
('Đậu đỏ', 'M', 80, 10000, 0, LOAD_FILE('"D:\HK5\HK5-NHAP MON CONG NGHE PHAN MEM\QLBTS HinhAnh\Toping\daudo.jpg"'));
*/

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

-- INSERT INTO SANPHAM (MaSP, TenSP, LoaiSP, Size, Gia, TrangThaiGiao)
-- VALUES
-- -- ✅ Nhóm 1: Trà sữa Best Seller
-- ('SP001', 'Trà sữa trân châu', 'Trà sữa', 'M', 40000, 'Còn giao'),
-- ('SP002', 'Trà sữa thái xanh', 'Trà sữa', 'M', 45000, 'Còn giao'),
-- ('SP003', 'Trà sữa Matcha', 'Trà sữa', 'M', 35000, 'Còn giao'),
-- ('SP004', 'Trà sữa bạc hà', 'Trà sữa', 'M', 35000, 'Còn giao'),
-- ('SP005', 'Trà sữa xoài kem cheese', 'Trà sữa', 'M', 40000, 'Còn giao'),

-- -- ✅ Nhóm 2: Trà sữa khác
-- ('SP006', 'Trà sữa kem trứng nướng', 'Trà sữa', 'M', 35000, 'Còn giao'),
-- ('SP007', 'Trà sữa khoai môn', 'Trà sữa', 'M', 40000, 'Còn giao'),
-- ('SP008', 'Trà sữa Oreo Cake Cream', 'Trà sữa', 'M', 35000, 'Còn giao'),
-- ('SP009', 'Trà sữa matcha đậu đỏ', 'Trà sữa', 'M', 40000, 'Còn giao'),
-- ('SP010', 'Trà sữa Pudding đậu đỏ', 'Trà sữa', 'M', 40000, 'Còn giao'),

-- -- ✅ Nhóm 3: Topping
-- ('TP001', 'Kem Sữa', 'Topping', 'M', 10000, 'Còn giao'),
-- ('TP002', 'Trân Châu Đen', 'Topping', 'M', 10000, 'Còn giao'),
-- ('TP003', 'Trân Châu Trắng', 'Topping', 'M', 10000, 'Còn giao'),
-- ('TP005', 'Đậu đỏ', 'Topping', 'M', 10000, 'Còn giao'),
-- ('TP004', 'Sương Sáo', 'Topping', 'M', 10000, 'Còn giao'),

-- -- ✅ Nhóm 4: Trà trái cây
-- ('TC001', 'Trà đào cam sả', 'Trà trái cây', 'M', 35000, 'Còn giao'),
-- ('TC002', 'Nước dừa', 'Trà trái cây', 'M', 40000, 'Còn giao'),
-- ('TC003', 'Trà trái cây nhiệt đới', 'Trà trái cây', 'M', 40000, 'Còn giao');

-- ALTER TABLE SANPHAM ADD COLUMN HinhAnh VARCHAR(255);

-- UPDATE SANPHAM SET HinhAnh = 'https://raw.githubusercontent.com/hoangyennewit/QLHINHANH/main/trasuatranchau.png' WHERE MaSP = 'SP001';
-- UPDATE SANPHAM SET HinhAnh = 'https://raw.githubusercontent.com/hoangyennewit/QLHINHANH/main/trasuathaixanh.png' WHERE MaSP = 'SP002';
-- UPDATE SANPHAM SET HinhAnh = 'https://raw.githubusercontent.com/hoangyennewit/QLHINHANH/main/trasuamatcha.png' WHERE MaSP = 'SP003';
-- UPDATE SANPHAM SET HinhAnh = 'https://raw.githubusercontent.com/hoangyennewit/QLHINHANH/main/trasuabacha.png' WHERE MaSP = 'SP004';
-- UPDATE SANPHAM SET HinhAnh = 'https://raw.githubusercontent.com/hoangyennewit/QLHINHANH/main/trasuaxoaikemcheese.png' WHERE MaSP = 'SP005';

-- UPDATE SANPHAM SET HinhAnh = 'https://raw.githubusercontent.com/hoangyennewit/QLHINHANH/main/trasuakemtrungnuong.jpg' WHERE MaSP = 'SP006';
-- UPDATE SANPHAM SET HinhAnh = 'https://raw.githubusercontent.com/hoangyennewit/QLHINHANH/main/trasuakhoaimon.jpg' WHERE MaSP = 'SP007';
-- UPDATE SANPHAM SET HinhAnh = 'https://raw.githubusercontent.com/hoangyennewit/QLHINHANH/main/trasuaoreocakecream.jpg' WHERE MaSP = 'SP008';
-- UPDATE SANPHAM SET HinhAnh = 'https://raw.githubusercontent.com/hoangyennewit/QLHINHANH/main/trasuamatchadaudo.jpg' WHERE MaSP = 'SP009';
-- UPDATE SANPHAM SET HinhAnh = 'https://raw.githubusercontent.com/hoangyennewit/QLHINHANH/main/trasuapuddingdaudo.jpg' WHERE MaSP = 'SP010';

-- UPDATE SANPHAM SET HinhAnh = 'https://raw.githubusercontent.com/hoangyennewit/QLHINHANH/main/kemsua.jpg' WHERE MaSP = 'TP001';
-- UPDATE SANPHAM SET HinhAnh = 'https://raw.githubusercontent.com/hoangyennewit/QLHINHANH/main/tranchauden.jpg' WHERE MaSP = 'TP002';
-- UPDATE SANPHAM SET HinhAnh = 'https://raw.githubusercontent.com/hoangyennewit/QLHINHANH/main/tranchautrang.jpg' WHERE MaSP = 'TP003';
-- UPDATE SANPHAM SET HinhAnh = 'https://raw.githubusercontent.com/hoangyennewit/QLHINHANH/main/suongsao.jpg' WHERE MaSP = 'TP004';
-- UPDATE SANPHAM SET HinhAnh = 'https://raw.githubusercontent.com/hoangyennewit/QLHINHANH/main/daudo.jpg' WHERE MaSP = 'TP005';

-- UPDATE SANPHAM SET HinhAnh = 'https://raw.githubusercontent.com/hoangyennewit/QLHINHANH/main/tradaocamsa.jpg' WHERE MaSP = 'TC001';
-- UPDATE SANPHAM SET HinhAnh = 'https://raw.githubusercontent.com/hoangyennewit/QLHINHANH/main/nuocdua.jpg' WHERE MaSP = 'TC002';
-- UPDATE SANPHAM SET HinhAnh = 'https://raw.githubusercontent.com/hoangyennewit/QLHINHANH/main/tratraicaynhietdoi.jpg' WHERE MaSP = 'TC003';

DROP TABLE ChiTietDonHang;
DROP TABLE ChiTietGioHang;
DROP TABLE SanPham;


SELECT * FROM SanPham;
