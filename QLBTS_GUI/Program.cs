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
<<<<<<< HEAD
            Form form = new DangNhap();
=======
            Form form = new QLDonHangForm();
>>>>>>> 39d8d70 (add form nhan vien quay)
            form.ShowDialog();
        }
    }
}