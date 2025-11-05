# Software Requirements Specification (SRS)
## Ứng dụng Quản lý Bán Trà Sữa

---

## 1. Giới thiệu

### 1.1. Mục đích
Tài liệu này mô tả đầy đủ các yêu cầu chức năng và phi chức năng của Ứng dụng Quản lý Bán Trà Sữa. Tài liệu được sử dụng làm cơ sở cho việc thiết kế, phát triển và kiểm thử hệ thống.

### 1.2. Phạm vi dự án
Ứng dụng Quản lý Bán Trà Sữa là một hệ thống desktop được phát triển để:
- Hỗ trợ quản lý bán hàng tại quầy (POS)
- Quản lý nhân viên và phân quyền
- Quản lý sản phẩm (trà sữa, topping)
- Theo dõi và xử lý đơn hàng
- Quản lý giao hàng
- Thống kê và báo cáo doanh thu

### 1.3. Định nghĩa, từ viết tắt
- **POS**: Point of Sale - Điểm bán hàng
- **CRUD**: Create, Read, Update, Delete
- **UI**: User Interface - Giao diện người dùng
- **DB**: Database - Cơ sở dữ liệu

### 1.4. Tài liệu tham khảo
- Tài liệu thiết kế cơ sở dữ liệu: [DATABASE.md](DATABASE.md)
- Hướng dẫn sử dụng: [USER_GUIDE.md](USER_GUIDE.md)
- Tài liệu kiểm thử: [TESTING.md](TESTING.md)

---

## 2. Mô tả tổng quan

### 2.1. Quan điểm sản phẩm
Ứng dụng được thiết kế cho các cửa hàng trà sữa quy mô vừa và nhỏ, giúp tự động hóa quy trình bán hàng và quản lý.

### 2.2. Chức năng sản phẩm
1. Đăng nhập và phân quyền người dùng
2. Quản lý nhân viên
3. Quản lý sản phẩm (trà sữa, topping)
4. Bán hàng (POS)
5. Quản lý đơn hàng
6. Quản lý giao hàng
7. Thống kê và báo cáo

### 2.3. Người dùng
- **Admin**: Chủ cửa hàng, quản lý cấp cao
- **Nhân viên**: Nhân viên bán hàng, nhân viên giao hàng

### 2.4. Môi trường hoạt động
- **Hệ điều hành**: Windows 10/11
- **Framework**: .NET 8
- **Database**: SQL Server 2019+ hoặc MySQL 8.0+

---

## 3. Yêu cầu chức năng (Functional Requirements)

### 3.1. Đăng nhập & Phân quyền

#### FR-01: Đăng nhập hệ thống
- **Mô tả**: Người dùng phải đăng nhập để sử dụng hệ thống
- **Đầu vào**: Username, Password
- **Xử lý**: Xác thực thông tin đăng nhập với database
- **Đầu ra**: Thành công → Chuyển đến màn hình chính; Thất bại → Hiển thị thông báo lỗi
- **Yêu cầu đặc biệt**: Mật khẩu phải được mã hóa (hash)

#### FR-02: Phân quyền người dùng
- **Mô tả**: Hệ thống phân quyền theo vai trò
- **Vai trò Admin**: Toàn quyền truy cập tất cả chức năng
- **Vai trò Nhân viên**: Chỉ truy cập bán hàng và quản lý đơn hàng được phân công

### 3.2. Quản lý Nhân viên

#### FR-03: Thêm nhân viên mới
- **Mô tả**: Admin có thể thêm nhân viên mới
- **Đầu vào**: Họ tên, ngày sinh, số điện thoại, địa chỉ, chức vụ, username, password
- **Yêu cầu**: 
  - Họ tên không được để trống
  - Số điện thoại phải đúng định dạng (10-11 số)
  - Username phải duy nhất trong hệ thống

#### FR-04: Cập nhật thông tin nhân viên
- **Mô tả**: Admin có thể sửa thông tin nhân viên
- **Đầu vào**: Thông tin cần cập nhật
- **Ràng buộc**: Không được cập nhật username đã tồn tại

#### FR-05: Xóa nhân viên
- **Mô tả**: Admin có thể xóa nhân viên khỏi hệ thống
- **Ràng buộc**: Không xóa được nhân viên đang có đơn hàng chưa hoàn thành

#### FR-06: Tìm kiếm nhân viên
- **Mô tả**: Tìm kiếm nhân viên theo tên, số điện thoại hoặc chức vụ

### 3.3. Quản lý Sản phẩm

#### FR-07: Thêm sản phẩm mới
- **Mô tả**: Admin có thể thêm sản phẩm trà sữa hoặc topping
- **Đầu vào**: Tên sản phẩm, loại (trà sữa/topping), size, giá, mô tả
- **Yêu cầu**:
  - Tên sản phẩm không được trùng
  - Giá phải là số dương

#### FR-08: Cập nhật sản phẩm
- **Mô tả**: Admin cập nhật thông tin sản phẩm
- **Cho phép**: Cập nhật giá, mô tả, trạng thái (còn/hết)

#### FR-09: Xóa sản phẩm
- **Mô tả**: Admin xóa sản phẩm khỏi menu
- **Ràng buộc**: Chỉ xóa được sản phẩm không có trong đơn hàng đang xử lý

#### FR-10: Tìm kiếm sản phẩm
- **Mô tả**: Tìm kiếm sản phẩm theo tên hoặc loại

### 3.4. Bán hàng (POS)

#### FR-11: Tạo đơn hàng mới
- **Mô tả**: Nhân viên tạo đơn hàng cho khách
- **Quy trình**:
  1. Chọn sản phẩm từ danh sách
  2. Chọn size (S/M/L)
  3. Chọn topping (nếu có)
  4. Nhập số lượng
  5. Thêm vào giỏ hàng
  6. Xác nhận đơn hàng

#### FR-12: Tính tổng tiền
- **Mô tả**: Hệ thống tự động tính tổng tiền
- **Công thức**: Tổng = Σ(Giá sản phẩm × Số lượng + Giá topping)

#### FR-13: Thanh toán
- **Mô tả**: Xử lý thanh toán đơn hàng
- **Phương thức**: Tiền mặt, chuyển khoản
- **Đầu ra**: Tạo hóa đơn

#### FR-14: In hóa đơn
- **Mô tả**: In hoặc xuất hóa đơn PDF
- **Nội dung**: Mã đơn, ngày giờ, danh sách sản phẩm, tổng tiền, nhân viên bán

### 3.5. Quản lý Đơn hàng

#### FR-15: Xem danh sách đơn hàng
- **Mô tả**: Hiển thị tất cả đơn hàng
- **Thông tin**: Mã đơn, khách hàng, sản phẩm, tổng tiền, trạng thái, ngày tạo

#### FR-16: Cập nhật trạng thái đơn hàng
- **Mô tả**: Thay đổi trạng thái đơn
- **Các trạng thái**:
  1. Chờ xác nhận
  2. Đang pha chế
  3. Đang giao hàng
  4. Hoàn thành
  5. Đã hủy

#### FR-17: Tìm kiếm đơn hàng
- **Mô tả**: Tìm đơn theo mã đơn, khách hàng, ngày tạo

#### FR-18: Hủy đơn hàng
- **Mô tả**: Hủy đơn hàng chưa xử lý
- **Ràng buộc**: Chỉ hủy được đơn ở trạng thái "Chờ xác nhận"

### 3.6. Quản lý Giao hàng

#### FR-19: Phân công giao hàng
- **Mô tả**: Admin phân công đơn cho nhân viên giao hàng
- **Đầu vào**: Mã đơn, nhân viên giao hàng

#### FR-20: Cập nhật trạng thái giao hàng
- **Mô tả**: Nhân viên cập nhật trạng thái
- **Trạng thái**: Đang giao, Đã giao, Giao thất bại

#### FR-21: Ghi nhận feedback
- **Mô tả**: Ghi nhận phản hồi từ khách hàng sau khi giao

### 3.7. Thống kê & Báo cáo

#### FR-22: Thống kê doanh thu
- **Mô tả**: Thống kê doanh thu theo thời gian
- **Lọc theo**: Ngày, tuần, tháng, năm
- **Hiển thị**: Biểu đồ, bảng số liệu

#### FR-23: Sản phẩm bán chạy
- **Mô tả**: Xếp hạng sản phẩm theo số lượng bán
- **Lọc theo**: Khoảng thời gian

#### FR-24: Báo cáo hiệu suất nhân viên
- **Mô tả**: Thống kê số đơn, doanh thu theo nhân viên

---

## 4. Yêu cầu phi chức năng (Non-functional Requirements)

### 4.1. Hiệu năng (Performance)
- **NFR-01**: Thời gian tạo đơn hàng < 3 giây
- **NFR-02**: Thời gian tải danh sách sản phẩm < 1 giây
- **NFR-03**: Hệ thống xử lý ổn định với 10 người dùng đồng thời

### 4.2. Bảo mật (Security)
- **NFR-04**: Mật khẩu phải được mã hóa (SHA-256 hoặc BCrypt)
- **NFR-05**: Phân quyền rõ ràng cho từng vai trò
- **NFR-06**: Session timeout sau 30 phút không hoạt động
- **NFR-07**: Ghi log tất cả hoạt động quan trọng

### 4.3. Khả năng sử dụng (Usability)
- **NFR-08**: Giao diện thân thiện, dễ sử dụng
- **NFR-09**: Thông báo lỗi rõ ràng, dễ hiểu
- **NFR-10**: Hỗ trợ phím tắt cho các thao tác thường dùng

### 4.4. Khả năng mở rộng (Scalability)
- **NFR-11**: Dễ dàng thêm module mới (khuyến mãi, loyalty program)
- **NFR-12**: Hỗ trợ thêm phương thức thanh toán mới

### 4.5. Khả năng bảo trì (Maintainability)
- **NFR-13**: Code tuân thủ coding convention của C#
- **NFR-14**: Document đầy đủ cho developer
- **NFR-15**: Sử dụng design patterns phù hợp

---

## 5. Ràng buộc kỹ thuật

### 5.1. Công nghệ
- **Ngôn ngữ**: C# (.NET 8)
- **UI Framework**: Windows Forms
- **Database**: SQL Server 2019+ hoặc MySQL 8.0+
- **ORM**: Entity Framework Core

### 5.2. Kiến trúc
- **Mô hình**: 3-Layer Architecture
  - Presentation Layer (UI)
  - Business Logic Layer (BLL)
  - Data Access Layer (DAL)

### 5.3. Cơ sở dữ liệu
- Tối thiểu 7 bảng chính:
  1. NhanVien
  2. TaiKhoan
  3. SanPham
  4. DonHang
  5. ChiTietDonHang
  6. GiaoHang
  7. ThanhToan

---

## 6. Use Cases chính

### UC-01: Đăng nhập
**Actor**: Admin, Nhân viên  
**Precondition**: Có tài khoản hợp lệ  
**Flow**:
1. Nhập username và password
2. Click "Đăng nhập"
3. Hệ thống xác thực
4. Chuyển đến màn hình chính theo role

### UC-02: Bán hàng
**Actor**: Nhân viên  
**Precondition**: Đã đăng nhập  
**Flow**:
1. Chọn sản phẩm
2. Chọn size, topping
3. Thêm vào giỏ
4. Xác nhận đơn
5. Thanh toán
6. In hóa đơn

### UC-03: Quản lý nhân viên
**Actor**: Admin  
**Precondition**: Đăng nhập với quyền Admin  
**Flow**:
1. Vào menu "Quản lý nhân viên"
2. Chọn thao tác (Thêm/Sửa/Xóa)
3. Nhập/cập nhật thông tin
4. Lưu

---

## 7. Giao diện người dùng (UI Requirements)

### 7.1. Màn hình đăng nhập
- Logo ứng dụng
- Textbox: Username, Password
- Button: Đăng nhập, Thoát
- Checkbox: Ghi nhớ đăng nhập

### 7.2. Màn hình chính (Dashboard)
- Menu bar với các chức năng chính
- Thông tin người dùng đang đăng nhập
- Thống kê tổng quan (doanh thu ngày, số đơn)

### 7.3. Màn hình bán hàng
- Danh sách sản phẩm (Grid/List)
- Giỏ hàng
- Tổng tiền
- Button: Thêm, Xóa, Thanh toán

### 7.4. Màn hình quản lý
- DataGridView hiển thị dữ liệu
- Toolbar: Thêm, Sửa, Xóa, Tìm kiếm
- Form nhập liệu

---

## 8. Kế hoạch kiểm thử

Xem chi tiết tại: [TESTING.md](TESTING.md)

---

## 9. Phụ lục

### 9.1. Tài liệu liên quan
- [DATABASE.md](DATABASE.md) - Thiết kế cơ sở dữ liệu
- [USER_GUIDE.md](USER_GUIDE.md) - Hướng dẫn sử dụng
- [UI_DESIGN.md](UI_DESIGN.md) - Thiết kế giao diện

### 9.2. Lịch sử thay đổi

| Phiên bản | Ngày | Người thay đổi | Nội dung |
|-----------|------|----------------|----------|
| 1.0 | 05/11/2024 | QLBTS Team | Phiên bản đầu tiên |

---

*Tài liệu này được tạo bởi QLBTS Development Team*