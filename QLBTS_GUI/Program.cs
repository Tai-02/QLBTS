using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using MySql.Data.MySqlClient;
namespace QLBTS_GUI
{
    internal static class Program
    {
        ///// <summary>
        /////  The main entry point for the application.
        ///// </summary>
        //[STAThread]
        //static void Main()
        //{
        //    // To customize application configuration such as set high DPI settings or default font,
        //    // see https://aka.ms/applicationconfiguration.
        //    ApplicationConfiguration.Initialize();
        //    Form form = new CartForm();
        //    form.ShowDialog();
        //}
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                // TEST: Load giỏ hàng của Khách hàng MaKH = 1
                CartForm cartForm = new CartForm();
                cartForm.LoadCartFromDatabaseCustomer(maKH: 1);

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
    }
}