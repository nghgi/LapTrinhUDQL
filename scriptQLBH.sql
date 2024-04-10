drop database QLBH
CREATE DATABASE QLBH
USE QuanLyBanHang


CREATE SEQUENCE SP_MaSP
MINVALUE 1
MAXVALUE 99999

-- Tạo bảng KhuVuc
CREATE TABLE KhuVuc (
    Ma INT PRIMARY KEY,
    Ten NVARCHAR(100),
    GhiChu NVARCHAR(MAX)
);

-- Tạo bảng SanPham
CREATE TABLE SanPham (
	MaSP NVARCHAR(100) PRIMARY KEY,
    TenSP NVARCHAR(100),
    LoaiSP NVARCHAR(100),
    SoLuong INT,
    DonGia MONEY,
    MaKhuVuc INT,
    FOREIGN KEY (MaKhuVuc) REFERENCES KhuVuc(Ma)
);

select * from SanPham
ALTER TABLE SanPham
ADD MaLoaiSanPham INT;


-- Tạo bảng HoaDon
CREATE TABLE HoaDon (
    Ma INT PRIMARY KEY,
    TenKH NVARCHAR(100),
    Ngay DATE,
    TongTien MONEY,
    NhanVien NVARCHAR(100)
);

ALTER TABLE HoaDon DROP CONSTRAINT PK_HoaDon;
ALTER TABLE HoaDon ALTER COLUMN Ma INT IDENTITY(1,1);

-- Tạo bảng HoaDonCT
CREATE TABLE HoaDonCT (
    MaHoaDon INT,
    SanPham NVARCHAR(100),
    SoLuong INT,
    DonGia MONEY,
    FOREIGN KEY (MaHoaDon) REFERENCES HoaDon(Ma)
);

-- Tạo bảng NhanVien
CREATE TABLE NhanVien (
    Ma INT PRIMARY KEY,
    TenDangNhap NVARCHAR(50),
    Ten NVARCHAR(100),
    MatKhau NVARCHAR(50),
    DienThoai NVARCHAR(20),
    DiaChi NVARCHAR(200),
    CMND NVARCHAR(20)
);

-- Tạo bảng DaiLySanPham
CREATE TABLE DaiLySanPham (
    MaSanPham NVARCHAR(100),
    MaDaiLy INT,
    FOREIGN KEY (MaSanPham) REFERENCES SanPham(MaSP),
    FOREIGN KEY (MaDaiLy) REFERENCES DaiLy(Ma)
);

-- Tạo bảng DaiLy
CREATE TABLE DaiLy (
    Ma INT PRIMARY KEY,
    Ten NVARCHAR(100),
    DiaChi NVARCHAR(200),
    DienThoai NVARCHAR(20)
);

-- Tạo bảng PhieuNhap
CREATE TABLE PhieuNhap (
    Ma INT PRIMARY KEY,
    MaDaiLy INT,
    Ngay DATE,
    TongTien MONEY,
    FOREIGN KEY (MaDaiLy) REFERENCES DaiLy(Ma)
);

-- Tạo bảng PhieuNhapCT
CREATE TABLE PhieuNhapCT (
    MaPN INT,
    SanPham NVARCHAR(100),
    SoLuong INT,
    DonGia MONEY,
    FOREIGN KEY (MaPN) REFERENCES PhieuNhap(Ma)
);

-- Tạo bảng PhieuChi
CREATE TABLE PhieuChi (
    Ma INT PRIMARY KEY,
    Ngay DATE,
    SoTien MONEY,
    LyDo NVARCHAR(MAX),
    NhanVien NVARCHAR(100)
);

-- Tạo bảng LoaiSanPham
CREATE TABLE LoaiSanPham (
    Ma INT PRIMARY KEY,
    Loai NVARCHAR(100),
    GhiChu NVARCHAR(MAX)
);

ALTER TABLE SanPham
ADD CONSTRAINT FK_LSP_SP
FOREIGN KEY (MaLoaiSanPham) REFERENCES LoaiSanPham(Ma)

--Them bang NguoiDung
CREATE TABLE NguoiDung (
	Ma INT PRIMARY KEY,
	Ten NVARCHAR(100),
	MatKhau NVARCHAR(100),
	Loai INT,
)

CREATE TABLE HoaDon (
    Ma INT IDENTITY(1,1) PRIMARY KEY,
    TenKH NVARCHAR(100),
    Ngay DATE,
    TongTien MONEY,
    NhanVien NVARCHAR(100)
);

-- Tạo bảng HoaDonCT
CREATE TABLE HoaDonCT (
    MaHoaDon INT,
    SanPham NVARCHAR(100),
    SoLuong INT,
    DonGia MONEY,
    FOREIGN KEY (MaHoaDon) REFERENCES HoaDon(Ma)
);

-- Thêm dữ liệu vào bảng KhuVuc
INSERT INTO KhuVuc (Ma, Ten, GhiChu)
VALUES 
(1, N'Khu vực A', N'Ghi chú cho khu vực A'),
(2, N'Khu vực B', N'Ghi chú cho khu vực B'),
(3, N'Khu vực C', N'Ghi chú cho khu vực C');

-- Thêm dữ liệu vào bảng SanPham
INSERT INTO SanPham (MaSP, TenSP, LoaiSP, SoLuong, DonGia, MaKhuVuc)
VALUES 
(N'SP001', N'Sản phẩm 1', N'Loại 1', 100, 50000, 1),
(N'SP002', N'Sản phẩm 2', N'Loại 2', 200, 60000, 2),
(N'SP003', N'Sản phẩm 3', N'Loại 1', 150, 70000, 1);

-- Thêm dữ liệu vào bảng HoaDon
INSERT INTO HoaDon (TenKH, Ngay, TongTien, NhanVien)
VALUES 
(N'Khách hàng A', '2024-03-12', 200000, N'Nhân viên A'),
(N'Khách hàng B', '2024-03-13', 300000, N'Nhân viên B'),
(N'Khách hàng C', '2024-03-14', 400000, N'Nhân viên C');


-- Thêm dữ liệu vào bảng HoaDonCT
INSERT INTO HoaDonCT (MaHoaDon, SanPham, SoLuong, DonGia)
VALUES 
(1, N'Sản phẩm 1', 2, 50000),
(1, N'Sản phẩm 2', 3, 60000),
(2, N'Sản phẩm 2', 4, 60000);

-- Thêm dữ liệu vào bảng NhanVien
INSERT INTO NhanVien (Ma, TenDangNhap, Ten, MatKhau, DienThoai, DiaChi, CMND)
VALUES 
(1, N'nv1', N'Nhân viên 1', N'123456', N'0987654321', N'Địa chỉ 1', N'123456789'),
(2, N'nv2', N'Nhân viên 2', N'abcdef', N'0123456789', N'Địa chỉ 2', N'987654321');

-- Thêm dữ liệu vào bảng DaiLySanPham
INSERT INTO DaiLySanPham (MaSanPham, MaDaiLy)
VALUES 
(N'SP001', 1),
(N'SP002', 2),
(N'SP003', 3);

-- Thêm dữ liệu vào bảng DaiLy
INSERT INTO DaiLy (Ma, Ten, DiaChi, DienThoai)
VALUES 
(1, N'Đại lý A', N'Địa chỉ A', N'0987654321'),
(2, N'Đại lý B', N'Địa chỉ B', N'0123456789'),
(3, N'Đại lý C', N'Địa chỉ C', N'0123453389');

-- Thêm dữ liệu vào bảng PhieuNhap
INSERT INTO PhieuNhap (Ma, MaDaiLy, Ngay, TongTien)
VALUES 
(1, 1, '2024-03-12', 150000),
(2, 2, '2024-03-13', 200000);

-- Thêm dữ liệu vào bảng PhieuNhapCT
INSERT INTO PhieuNhapCT (MaPN, SanPham, SoLuong, DonGia)
VALUES 
(1, N'Sản phẩm 1', 3, 50000),
(1, N'Sản phẩm 2', 2, 60000),
(2, N'Sản phẩm 2', 4, 60000);

-- Thêm dữ liệu vào bảng PhieuChi
INSERT INTO PhieuChi (Ma, Ngay, SoTien, LyDo, NhanVien)
VALUES 
(1, '2024-03-12', 50000, N'Lý do chi phí 1', N'Nhân viên A'),
(2, '2024-03-13', 60000, N'Lý do chi phí 2', N'Nhân viên B');

-- Thêm dữ liệu vào bảng LoaiSanPham
INSERT INTO LoaiSanPham (Ma, Loai, GhiChu)
VALUES 
(1, N'Loại sản phẩm 1', N'Ghi chú cho loại sản phẩm 1'),
(2, N'Loại sản phẩm 2', N'Ghi chú cho loại sản phẩm 2'),
(3, N'Loại sản phẩm 3', N'Ghi chú cho loại sản phẩm 3');


-- Thêm dữ liệu vào bảng NguoiDung
INSERT INTO NguoiDung VALUES (1, 'loc', 'abc', 1);
INSERT INTO NguoiDung VALUES (2, 'hung', 'abc', 2);

--Them MaLoaiSanPham
UPDATE SanPham SET MaLoaiSanPham = 1 WHERE MaSP = 'SP001'
UPDATE SanPham SET MaLoaiSanPham = 2 WHERE MaSP = 'SP002'
UPDATE SanPham SET MaLoaiSanPham = 3 WHERE MaSP = 'SP003'


-- Add auto increase
drop table HoaDon
select * from HoaDonct
select * from SANPHAM
select * from KhuVuc

delete from HoaDon where Ma = 4
select * from HoaDonCT
INSERT INTO HoaDonCT (MaHoaDon, SanPham, SoLuong, DonGia)
VALUES 
(1, N'Sản phẩm 1', 2, 50000);
update SANPHAM set MaLoaiSanPham = 2 where MASP = 'SP003'