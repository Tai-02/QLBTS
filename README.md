<div align="center">

# 🧋 Ứng dụng Quản lý Bán Trà Sữa

[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-12.0-239120?logo=c-sharp)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![Windows Forms](https://img.shields.io/badge/Windows-Forms-0078D6?logo=windows)](https://docs.microsoft.com/en-us/dotnet/desktop/winforms/)
[![SQL Server](https://img.shields.io/badge/SQL-Server-CC2927?logo=microsoft-sql-server)](https://www.microsoft.com/sql-server)
[![License](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)

**Ứng dụng desktop quản lý toàn diện cho cửa hàng trà sữa**

Hỗ trợ bán hàng (POS) • Quản lý nhân viên • Quản lý kho • Thống kê doanh thu

[Tính năng](#-tính-năng-chính) •
[Cài đặt](#-hướng-dẫn-cài-đặt) •
[Tài liệu](#-tài-liệu) •
[Đóng góp](#-đóng-góp)

</div>

---

## 📋 Mục lục
- [Giới thiệu](#-giới-thiệu)
- [Tính năng chính](#-tính-năng-chính)
- [Công nghệ sử dụng](#️-công-nghệ-sử-dụng)
- [Yêu cầu hệ thống](#-yêu-cầu-hệ-thống)
- [Hướng dẫn cài đặt](#-hướng-dẫn-cài-đặt)
- [Cấu trúc dự án](#-cấu-trúc-dự-án)
- [Tài liệu](#-tài-liệu)
- [Tác giả](#-tác-giả)
- [License](#-license)

## 🎯 Giới thiệu

Ứng dụng Quản lý Bán Trà Sữa là một giải pháp phần mềm desktop toàn diện được phát triển bằng C# WinForms, giúp tự động hóa và tối ưu hóa quy trình vận hành cửa hàng trà sữa. Từ việc bán hàng, quản lý nhân viên, sản phẩm đến theo dõi đơn hàng và thống kê doanh thu - tất cả được tích hợp trong một giao diện thân thiện và dễ sử dụng.

### 🎓 Dự án môn học
Đây là đồ án môn học **Nhập môn công nghệ phần mềm**, được phát triển với mục tiêu áp dụng các kiến thức về:
- Phân tích yêu cầu hệ thống
- Thiết kế kiến trúc phần mềm theo mô hình 3-layer
- Thiết kế cơ sở dữ liệu
- Phát triển ứng dụng desktop với C# WinForms
- Quản lý source code với Git/GitHub

## ✨ Tính năng chính

<table>
<tr>
<td width="50%">

### 🔐 Đăng nhập & Phân quyền
- Xác thực người dùng an toàn
- Phân quyền theo vai trò:
  - **Admin**: Toàn quyền quản lý
  - **Nhân viên**: Bán hàng & quản lý đơn

### 👥 Quản lý Nhân viên
- CRUD thông tin nhân viên
- Quản lý chức vụ & phân quyền
- Theo dõi hiệu suất làm việc

### 🧋 Quản lý Sản phẩm
- Quản lý trà sữa & topping
- Phân loại theo size, giá
- Tìm kiếm & lọc sản phẩm

</td>
<td width="50%">

### 💰 Bán hàng (POS)
- Giao diện bán hàng trực quan
- Tùy chỉnh size & topping
- Tính tiền tự động
- Xuất hóa đơn (PDF/In)

### 📦 Quản lý Đơn hàng
- Tracking đơn hàng realtime
- Quản lý trạng thái đơn
- Lịch sử giao dịch

### 📊 Thống kê & Báo cáo
- Doanh thu theo thời gian
- Sản phẩm bán chạy
- Báo cáo hiệu suất

</td>
</tr>
</table>

### 🚚 Quản lý Giao hàng
- Phân công nhân viên giao hàng
- Cập nhật trạng thái giao hàng

## 🛠️ Công nghệ sử dụng

<div align="center">

| Công nghệ | Phiên bản | Mục đích |
|-----------|-----------|----------|
| ![C#](https://img.shields.io/badge/C%23-12.0-239120?style=flat-square&logo=c-sharp) | .NET 8 | Ngôn ngữ lập trình |
| ![Windows Forms](https://img.shields.io/badge/WinForms-UI-0078D6?style=flat-square&logo=windows) | .NET 8 | Giao diện người dùng |
| ![SQL Server](https://img.shields.io/badge/SQL_Server-Database-CC2927?style=flat-square&logo=microsoft-sql-server) | 2019+ | Cơ sở dữ liệu |
| ![Entity Framework](https://img.shields.io/badge/EF_Core-ORM-512BD4?style=flat-square&logo=.net) | 8.0 | Object-Relational Mapping |
| ![Visual Studio](https://img.shields.io/badge/Visual_Studio-IDE-5C2D91?style=flat-square&logo=visual-studio) | 2022/2025 | Môi trường phát triển |

</div>

### 🏗️ Kiến trúc hệ thống
- **Mô hình**: 3-Layer Architecture
  - **Presentation Layer** (QLBTS_GUI): Giao diện người dùng
  - **Business Logic Layer** (QLBTS_BLL): Xử lý nghiệp vụ
  - **Data Access Layer** (QLBTS_DAL): Truy xuất dữ liệu
  - **Data Transfer Objects** (QLBTS_DTO): Đối tượng truyền dữ liệu

## 💻 Yêu cầu hệ thống

### Phần cứng tối thiểu
- **CPU**: Intel Core i3 hoặc tương đương
- **RAM**: 4GB (khuyến nghị 8GB)
- **Ổ cứng**: 500MB dung lượng trống
- **Màn hình**: Độ phân giải 1366x768 trở lên

### Phần mềm
- **Hệ điều hành**: Windows 10/11 (64-bit)
- **.NET Runtime**: 8.0 trở lên
- **Database**: SQL Server 2019+ hoặc MySQL 8.0+
- **IDE** (cho development): Visual Studio 2022/2025 Insiders

## 📥 Hướng dẫn cài đặt

### 🔧 Bước 1: Chuẩn bị môi trường

<details>
<summary><b>1.1. Cài đặt Visual Studio</b></summary>

1. Tải Visual Studio 2022 từ [visualstudio.microsoft.com](https://visualstudio.microsoft.com/)
2. Chạy file cài đặt và chọn các workload:
   - ✅ **.NET desktop development**
   - ✅ **Data storage and processing** (nếu cần SQL Server)

</details>

<details>
<summary><b>1.2. Cài đặt .NET 8 Runtime</b></summary>

1. Mở **Visual Studio Installer**
2. Click **Modify** trên phiên bản Visual Studio đã cài
3. Chọn tab **Individual components**
4. Tìm và tích chọn: **.NET 8.0 Runtime**
5. Click **Modify** để cài đặt

</details>

### 📦 Bước 2: Clone dự án
```bash
# Sử dụng HTTPS
git clone https://github.com/Tai-02/QLBTS.git

# Hoặc sử dụng SSH
git clone git@github.com:Tai-02/QLBTS.git

# Di chuyển vào thư mục dự án
cd QLBTS
```

### 🔨 Bước 3: Mở và build dự án

1. **Mở Solution**
```
   Visual Studio → File → Open → Project/Solution
   → Chọn file QLBTS.sln
```

2. **Restore NuGet Packages**
```bash
   # Trong Terminal của Visual Studio
   dotnet restore
```

3. **Cấu hình Startup Project**
   - Chuột phải vào **Solution** trong Solution Explorer
   - Chọn **Properties**
   - **Startup Project** → Chọn **Single startup project**: `QLBTS_GUI`
   - Click **OK**

### 🗄️ Bước 4: Cấu hình Database

<details>
<summary><b>4.1. Cấu hình SQL Server (khuyến nghị)</b></summary>

1. Cài đặt SQL Server 2019+ và SQL Server Management Studio (SSMS)
2. Mở file `appsettings.json` trong project `QLBTS_GUI`
3. Cập nhật connection string:
```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=localhost;Database=QLBTS_DB;Trusted_Connection=True;TrustServerCertificate=True;"
     }
   }
```
4. Chạy migration để tạo database:
```bash
   dotnet ef database update
```

</details>

<details>
<summary><b>4.2. Hoặc import database từ script</b></summary>

1. Mở SSMS và kết nối đến SQL Server
2. Chọn **File** → **Open** → **File**
3. Chọn file `Database/QLBTS_Script.sql`
4. Click **Execute** để tạo database và tables

</details>

### ▶️ Bước 5: Chạy ứng dụng
```
Nhấn F5 hoặc click nút Start (▶️) trong Visual Studio
```

**Tài khoản mặc định:**
- **Admin**: 
  - Username: `admin`
  - Password: `0866582512`


## 📁 Cấu trúc dự án
```
QLBTS/
│
├── 📂 QLBTS_GUI/              # Presentation Layer
│   ├── Forms/                 # Windows Forms
│   ├── Resources/             # Images, icons
│   └── appsettings.json       # Cấu hình ứng dụng
│
├── 📂 QLBTS_BLL/              # Business Logic Layer
│   ├── Services/              # Business services
│   └── Validators/            # Data validation
│
├── 📂 QLBTS_DAL/              # Data Access Layer
│   ├── Repositories/          # Data repositories
│   ├── Entities/              # Entity models
│   └── DbContext/             # EF Core context
│
├── 📂 QLBTS_DTO/              # Data Transfer Objects
│   └── Models/                # DTO classes
│
├── 📂 docs/                   # Tài liệu dự án
│   ├── SRS.md                 # Software Requirements Specification
│   ├── USER_GUIDE.md          # Hướng dẫn sử dụng
│
├── 📄 DATABASE.md             # script database
├── 📄 QLBTS.slnx               # file sln
├── 📄 README.md               # File này
├── 📄 LICENSE                 # MIT License
└── 📄 .gitignore              # Git ignore rules
```

## 📚 Tài liệu

Tài liệu chi tiết của dự án được lưu trong thư mục [`docs/`](docs/):

| Tài liệu | Mô tả | Link |
|----------|-------|------|
| 📋 **SRS** | Đặc tả yêu cầu phần mềm (Software Requirements Specification) | [Xem tài liệu](docs/SRS.md) |
| 👤 **User Guide** | Hướng dẫn sử dụng cho người dùng cuối | [Xem tài liệu](docs/USER_GUIDE.md) |


## 👥 Tác giả

<div align="center">

### 🎓 Nhóm Phát triển

<table>
<tr>
<td align="center" width="20%">
<img src="https://github.com/identicons/user1.png" width="100px" alt="Nguyễn Minh Đại"/><br/>
<b>Trần Quốc Tài</b><br/>
<sub>Team Leader, Database Designer</sub><br/>
<a href="https://github.com/Tai-02">GitHub</a>
</td>
<td align="center" width="20%">
<img src="https://github.com/identicons/user2.png" width="100px" alt="Trần Quốc Tài"/><br/>
<b>Nguyễn Minh Đại</b><br/>
<sub> Backend Developer, UX Designer </sub><br/>
<a href="https://github.com/nmdai679">GitHub</a>
</td>
<td align="center" width="20%">
<img src="https://github.com/identicons/user3.png" width="100px" alt="Hồ Thị Hoàng Yến"/><br/>
<b>Hồ Thị Hoàng Yến</b><br/>
<sub> Backend Developer, UX Designer </sub><br/>
<a href="https://github.com/hoangyennewit">GitHub</a>
</td>
<td align="center" width="20%">
<img src="https://github.com/identicons/user4.png" width="100px" alt="Đinh Văn Quốc Tuấn"/><br/>
<b>Đinh Văn Quốc Tuấn</b><br/>
<sub> Backend Developer, UX Designer </sub><br/>
<a href="https://github.com/qtuan26">GitHub</a>
</td>
<td align="center" width="20%">
<img src="https://github.com/identicons/user5.png" width="100px" alt="Nguyễn Huy Đông"/><br/>
<b>Nguyễn Huy Đông</b><br/>
<sub> Backend Developer, UX Designer </sub><br/>
<a href="https://github.com/huydong0911">GitHub</a>
</td>
</tr>
</table>

</div>

### 📫 Liên hệ
- **Email**: [minhdainguyen.2005@gmail.com](mailto:minhdainguyen.2005@gmail.com)
- **GitHub**: [github.com/Tai-02/QLBTS](https://github.com/Tai-02/QLBTS)

## 📄 License

Dự án này được phân phối dưới giấy phép **MIT License** - xem file [LICENSE](LICENSE.txt) để biết thêm chi tiết.


## 🤝 Đóng góp

Chúng tôi rất hoan nghênh mọi đóng góp cho dự án! 

### Cách đóng góp:

1. **Fork** dự án này
2. **Clone** fork về máy: `git clone https://github.com/your-username/QLBTS.git`
3. Tạo **branch** mới: `git checkout -b feature/TenTinhNang`
4. **Commit** thay đổi: `git commit -m 'Add: Thêm tính năng X'`
5. **Push** lên branch: `git push origin feature/TenTinhNang`
6. Tạo **Pull Request**

### Quy tắc đóng góp:
- Code phải tuân thủ coding conventions của C#
- Có comment đầy đủ cho code phức tạp
- Test kỹ trước khi tạo PR
- Mô tả rõ ràng về thay đổi trong PR

### Báo lỗi (Bug Report):
Nếu phát hiện lỗi, vui lòng tạo **Issue** với thông tin:
- Mô tả lỗi chi tiết
- Các bước tái hiện lỗi
- Screenshots (nếu có)
- Môi trường (OS, .NET version, SQL Server version)

## 🙏 Lời cảm ơn

- Cảm ơn **Thầy Trần Sơn Hải và Anh Lê Thanh Thoại** đã tận tình hỗ trợ trong quá trình thực hiện đồ án
- Cảm ơn cộng đồng **Microsoft .NET** và **Stack Overflow** về các tài liệu và giải đáp
- Cảm ơn các bạn sinh viên đã góp ý và hỗ trợ trong quá trình phát triển

---

<div align="center">

### ⭐ Star History

[![Star History Chart](https://api.star-history.com/svg?repos=Tai-02/QLBTS&type=Date)](https://star-history.com/#Tai-02/QLBTS&Date)

**Nếu dự án hữu ích, đừng quên để lại ⭐ nhé!**

Made with ❤️ by QLBTS Team

</div>

