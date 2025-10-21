CREATE DATABASE QLBTS;
USE QLBTS;

CREATE TABLE SANPHAM (
    MaSP CHAR(5) PRIMARY KEY,
    TenSP VARCHAR(100) NOT NULL,
    LoaiSP VARCHAR(50),
    Size ENUM('M', 'L'),
    Gia DECIMAL(10,2),
    TrangThaiGiao VARCHAR(50)
);
INSERT INTO SANPHAM (MaSP, TenSP, LoaiSP, Size, Gia, TrangThaiGiao)
VALUES
-- ✅ Nhóm 1: Trà sữa Best Seller
('SP001', 'Trà sữa trân châu', 'Trà sữa', 'M', 40000, 'Còn giao'),
('SP002', 'Trà sữa thái xanh', 'Trà sữa', 'M', 45000, 'Còn giao'),
('SP003', 'Trà sữa Matcha', 'Trà sữa', 'M', 35000, 'Còn giao'),
('SP004', 'Trà sữa bạc hà', 'Trà sữa', 'M', 35000, 'Còn giao'),
('SP005', 'Trà sữa xoài kem cheese', 'Trà sữa', 'M', 40000, 'Còn giao'),

-- ✅ Nhóm 2: Trà sữa khác
('SP006', 'Trà sữa kem trứng nướng', 'Trà sữa', 'M', 35000, 'Còn giao'),
('SP007', 'Trà sữa khoai môn', 'Trà sữa', 'M', 40000, 'Còn giao'),
('SP008', 'Trà sữa Oreo Cake Cream', 'Trà sữa', 'M', 35000, 'Còn giao'),
('SP009', 'Trà sữa matcha đậu đỏ', 'Trà sữa', 'M', 40000, 'Còn giao'),
('SP010', 'Trà sữa Pudding đậu đỏ', 'Trà sữa', 'M', 40000, 'Còn giao'),

-- ✅ Nhóm 3: Topping
('TP001', 'Kem Sữa', 'Topping', 'M', 10000, 'Còn giao'),
('TP002', 'Trân Châu Đen', 'Topping', 'M', 10000, 'Còn giao'),
('TP003', 'Trân Châu Trắng', 'Topping', 'M', 10000, 'Còn giao'),
('TP004', 'Sương Sáo', 'Topping', 'M', 10000, 'Còn giao');

ALTER TABLE SANPHAM ADD COLUMN HinhAnh VARCHAR(255);

UPDATE SANPHAM SET HinhAnh = 'https://raw.githubusercontent.com/hoangyennewit/QLHINHANH/main/trasuatranchau.png' WHERE MaSP = 'SP001';
UPDATE SANPHAM SET HinhAnh = 'https://raw.githubusercontent.com/hoangyennewit/QLHINHANH/main/trasuathaixanh.png' WHERE MaSP = 'SP002';
UPDATE SANPHAM SET HinhAnh = 'https://raw.githubusercontent.com/hoangyennewit/QLHINHANH/main/trasuamatcha.png' WHERE MaSP = 'SP003';
UPDATE SANPHAM SET HinhAnh = 'https://raw.githubusercontent.com/hoangyennewit/QLHINHANH/main/trasuabacha.png' WHERE MaSP = 'SP004';
UPDATE SANPHAM SET HinhAnh = 'https://raw.githubusercontent.com/hoangyennewit/QLHINHANH/main/trasuaxoaikemcheese.png' WHERE MaSP = 'SP005';

UPDATE SANPHAM SET HinhAnh = 'C:/QLBTS/Images/trasua_bacha.jpg' WHERE MaSP = 'SP006';
UPDATE SANPHAM SET HinhAnh = 'C:/QLBTS/Images/trachanhleo.jpg' WHERE MaSP = 'SP007';
UPDATE SANPHAM SET HinhAnh = 'C:/QLBTS/Images/trasua_oreo.jpg' WHERE MaSP = 'SP008';
UPDATE SANPHAM SET HinhAnh = 'C:/QLBTS/Images/trasua_thachdaua.jpg' WHERE MaSP = 'SP009';
UPDATE SANPHAM SET HinhAnh = 'C:/QLBTS/Images/travai_nhadam.jpg' WHERE MaSP = 'SP010';

UPDATE SANPHAM SET HinhAnh = 'C:/QLBTS/Images/trasua_bacha.jpg' WHERE MaSP = 'TP001';
UPDATE SANPHAM SET HinhAnh = 'C:/QLBTS/Images/trachanhleo.jpg' WHERE MaSP = 'SP002';
UPDATE SANPHAM SET HinhAnh = 'C:/QLBTS/Images/trasua_oreo.jpg' WHERE MaSP = 'SP003';

DROP TABLE SANPHAM;

SELECT * FROM SANPHAM;
