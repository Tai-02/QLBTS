#nullable disable
namespace QLBTS_DTO
{
    public class NhanVienDTO
    {
        public int MaNV { get; set; }
        public int MaTK { get; set; }
        // Khởi tạo giá trị mặc định để tránh cảnh báo null
        public string HoTen { get; set; } = string.Empty;
        public string TenDangNhap { get; set; } = string.Empty;
        public string MatKhau { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string SDT { get; set; } = string.Empty;
        public string VaiTro { get; set; } = string.Empty; // <-- Thuộc tính bị thiếu
    }
}