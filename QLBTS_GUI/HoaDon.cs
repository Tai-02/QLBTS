using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QLBTS_DTO;
using QuestPDF.Infrastructure;
using QuestPDF.Helpers;
using QuestPDF.Fluent;


public class HoaDonDocument : IDocument
{
    private readonly List<SanPhamDTO> _chiTiet;
    private readonly int _maDH;
    private readonly string _tenCuaHang;
    private readonly int _tongThanhToan;

    public HoaDonDocument(int maDH, string tenCuaHang, List<SanPhamDTO> chiTiet)
    {
        _maDH = maDH;
        _tenCuaHang = tenCuaHang;
        _chiTiet = chiTiet;

        _tongThanhToan = _chiTiet.Sum(sp => (sp.GiaHienTai - sp.KhuyenMaiHienTai) * sp.SoLuong);
    }

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Margin(30);
            page.Size(PageSizes.A4);

            // Header hóa đơn
            page.Header()
                .AlignCenter()
                .Text($"{_tenCuaHang}")
                .FontSize(22)
                .Bold()
                .FontColor(Colors.Blue.Medium);

            page.Content().Column(col =>
            {
                col.Spacing(10);

                col.Item().Text($"HÓA ĐƠN BÁN HÀNG #{_maDH}")
                    .FontSize(14)
                    .Bold()
                    .AlignCenter()
                    .FontColor(Colors.Black);

                col.Item().LineHorizontal(1).LineColor(Colors.Grey.Medium);

                // Bảng chi tiết sản phẩm
                col.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(30);    // STT
                        columns.RelativeColumn(3);     // Tên SP
                        columns.ConstantColumn(40);    // SL
                        columns.ConstantColumn(50);    // Size
                        columns.ConstantColumn(70);    // Giá
                        columns.ConstantColumn(80);    // Tạm tính
                        columns.ConstantColumn(80);    // Thành tiền
                    });

                    // Header
                    table.Header(header =>
                    {
                        header.Cell().Background(Colors.Grey.Lighten2).Padding(4).Text("STT").Bold();
                        header.Cell().Background(Colors.Grey.Lighten2).Padding(4).Text("Tên sản phẩm").Bold();
                        header.Cell().Background(Colors.Grey.Lighten2).Padding(4).Text("SL").Bold();
                        header.Cell().Background(Colors.Grey.Lighten2).Padding(4).Text("Size").Bold();
                        header.Cell().Background(Colors.Grey.Lighten2).Padding(4).AlignRight().Text("Giá").Bold();
                        header.Cell().Background(Colors.Grey.Lighten2).Padding(4).AlignRight().Text("Tạm tính").Bold();
                        header.Cell().Background(Colors.Grey.Lighten2).Padding(4).AlignRight().Text("Thành tiền").Bold();
                    });

                    int stt = 1;
                    foreach (var sp in _chiTiet)
                    {
                        int giaGoc = sp.GiaHienTai;
                        int giaSauKM = giaGoc - sp.KhuyenMaiHienTai;
                        int thanhTien = giaSauKM * sp.SoLuong;

                        table.Cell().PaddingVertical(4).Text(stt.ToString());
                        table.Cell().PaddingVertical(4).Text(sp.TenSP ?? "");
                        table.Cell().PaddingVertical(4).AlignCenter().Text(sp.SoLuong.ToString());
                        table.Cell().PaddingVertical(4).AlignCenter().Text(sp.Size ?? "");
                        table.Cell().PaddingVertical(4).AlignRight().Text($"{giaSauKM:N0}");
                        table.Cell().PaddingVertical(4).AlignRight().Text($"{giaGoc * sp.SoLuong:N0}");
                        table.Cell().PaddingVertical(4).AlignRight().Text($"{thanhTien:N0}");
                        stt++;
                    }
                });

                col.Item().LineHorizontal(1).LineColor(Colors.Grey.Medium);

                // Tổng cộng
                col.Item().AlignRight().Text($"Tổng thanh toán: {_tongThanhToan:N0} VND")
                    .FontSize(13)
                    .Bold()
                    .FontColor(Colors.Red.Medium);

                col.Item().PaddingTop(15).AlignCenter().Text("Xin cảm ơn Quý khách đã mua hàng!")
                    .FontSize(11)
                    .FontColor(Colors.Grey.Darken1)
                    .Italic();
            });
        });
    }
}
