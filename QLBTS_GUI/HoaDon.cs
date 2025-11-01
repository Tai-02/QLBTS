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

        _tongThanhToan = 0;
        foreach (var sp in _chiTiet)
        {
            int thanhTien = (sp.GiaHienTai - sp.KhuyenMaiHienTai) * sp.SoLuong;
            _tongThanhToan += thanhTien;
        }
    }

    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Margin(20);
            page.Size(PageSizes.A4);

            page.Content().Column(col =>
            {
                col.Item().Text(_tenCuaHang).FontSize(20).Bold().AlignCenter();
                col.Item().Text($"Hóa đơn #{_maDH}").FontSize(14).Bold().AlignCenter();
                col.Item().LineHorizontal(1).LineColor(Colors.Black);

                col.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(30);
                        columns.RelativeColumn(3);
                        columns.ConstantColumn(60);
                        columns.ConstantColumn(40);
                        columns.ConstantColumn(70);
                        columns.ConstantColumn(70);
                        columns.ConstantColumn(80);
                    });

                    table.Header(header =>
                    {
                        header.Cell().Text("STT").Bold();
                        header.Cell().Text("Tên SP").Bold();
                        header.Cell().Text("SL").Bold();
                        header.Cell().Text("Size").Bold();
                        header.Cell().Text("Giá").Bold();
                        header.Cell().Text("Tạm tính").Bold();
                        header.Cell().Text("Thành tiền").Bold();
                    });

                    int stt = 1;
                    foreach (var sp in _chiTiet)
                    {
                        int giaGoc = sp.GiaHienTai;
                        int giaSauKM = giaGoc - sp.KhuyenMaiHienTai;
                        int thanhTien = giaSauKM * sp.SoLuong;

                        table.Cell().Text(stt.ToString());
                        table.Cell().Text(sp.TenSP ?? "");
                        table.Cell().Text(sp.SoLuong.ToString());
                        table.Cell().Text(sp.Size ?? "");
                        table.Cell().Text($"{giaSauKM:N0}");
                        table.Cell().Text($"{giaGoc * sp.SoLuong:N0}");
                        table.Cell().Text($"{thanhTien:N0}");
                        stt++;
                    }
                });

                col.Item().LineHorizontal(1).LineColor(Colors.Black);
                col.Item().Text($"Tổng thanh toán: {_tongThanhToan:N0}").FontSize(12).Bold().AlignRight();
            });
        });
    }

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
}
