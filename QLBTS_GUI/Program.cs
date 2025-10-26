using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using MySql.Data.MySqlClient;
namespace QLBTS_GUI
{
    internal static class Program
    {

        [STAThread]
        //static void Main()
        //{
        //    Application.EnableVisualStyles();
        //    Application.SetCompatibleTextRenderingDefault(false);

        //    try
        //    {
        //        // TEST: Hiển thị chi tiết đơn hàng MaDH = 1
        //        OrderDetailForm orderForm = new OrderDetailForm();
        //        orderForm.LoadOrderDetail(maDH: 1);

        //        Application.Run(orderForm);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                // TEST: Load giỏ hàng của Khách hàng MaKH = 1
                CartForm cartForm = new CartForm();
                cartForm.LoadCartFromDatabase(maTK: 6);

                Application.Run(cartForm);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Lỗi khởi động ứng dụng:\n{ex.Message}\n\n" +
                    $"Chi tiết:\n{ex.StackTrace}\n\n" +
                    $"Kiểm tra:\n" +
                    $"1. MySQL đã chạy chưa?\n" +
                    $"2. Database QLBTS đã tạo chưa?\n" +
                    $"3. Connection string trong CartDAL.cs đúng chưa?\n" +
                    $"4. Đã cài MySql.Data NuGet chưa?\n" +
                    $"5. Đã thêm Reference QLBTS_DTO vào các project chưa?",
                    "Lỗi khởi động",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        //===== TEST LOAD CHI TIẾT SẢN PHẨM =====
        //static void Main()
        //{
        //    Application.EnableVisualStyles();
        //    Application.SetCompatibleTextRenderingDefault(false);

        //    try
        //    {
        //        // TEST: Load chi tiết sản phẩm
        //        ProductDetailForm detailForm = new ProductDetailForm();
        //        detailForm.LoadProduct(maSP: 21, maTK: 6);

        //        Application.Run(detailForm);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(
        //            $"Lỗi khởi động:\n{ex.Message}\n\n" +
        //            $"Chi tiết:\n{ex.InnerException?.Message}",
        //            "Lỗi",
        //            MessageBoxButtons.OK,
        //            MessageBoxIcon.Error
        //        );
        //    }
        //}

    }
}



