# Quản lý bán trà 

Ứng dụng mô phỏng quản lý bán hàng, nhân viên và kho hàng bằng C# WinForms.

## Các công nghệ chính:
- Ngôn ngữ: C# (.NET 8)
- Giao diện: WinForms
- CSDL: SQL Server
- IDE: Visual Studio 2022 or  Visual Studio 2026 insiders
- ORM: Entity Framework Core

### Hướng dẫn cài đặt & chạy:
- Cài đặt Visual Studio 2022 or  Visual Studio 2026 insiders
- Vào Visual Studio Intaller -> ở tab Workloads -> chọn .NET desktop development
- Vào Visual Studio Intaller -> chọn Modify -> ở tab Individual components -> tìm chọn .NET 8 Runtime
- Tiến hành cài đặt các gói trên
- Mở Git Bash hoặc Command Prompt, gõ lệnh: git clone https://github.com/Tai-02/QLBTS.git
- Mở Visual Studio → chọn: File → Open → Project/Solution -> tìm đến thư mục đã lưu -> chọn file .sln
- Khôi phục các gói NuGet -> Mở Terminal trong VS, gõ: dotnet restore
- Chuột phải vào Solution -> chọn property -> ở tab Configure Startup Projects -> mục Single startup projects: chọn QLBTS_GUI
- Chạy chương trình bằng F5
