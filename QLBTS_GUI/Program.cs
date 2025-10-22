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
            //Form form = new Form1();
            //form.ShowDialog();

            /*CHECK CODE*/
            Form form = new TimSanPham();
=======
            Form form = new DangNhap();
>>>>>>> origin/master
            form.ShowDialog();

            //Form form = new ThongTinCaNhan();
            //form.ShowDialog();
        }
    }
}