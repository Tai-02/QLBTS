using Microsoft.VisualBasic.ApplicationServices;
using QLBTS_DTO;

namespace QLBTS_GUI
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            Form form = new DangNhap();
            form.ShowDialog();

            //Form form = new frmThongTinCaNhan(TaiKhoanDTO user);
            //form.ShowDialog();
        }
    }
}