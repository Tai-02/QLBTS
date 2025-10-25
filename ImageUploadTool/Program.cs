using System;
using System.Collections.Generic;
using System.IO;
using MySql.Data.MySqlClient;

class Program
{
    static void Main()
    {
        // ❌ XÓA DÒNG NÀY
        // Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.WriteLine("==========================================");
        Console.WriteLine("  QLBTS - Image Uploader Tool");
        Console.WriteLine("==========================================\n");

        // ===== CẤU HÌNH =====
        string imageFolder = @"D:\data_image";  // ← ĐỔI ĐƯỜNG DẪN
        string connectionString = "Server=localhost;Database=QLBTS;Uid=root;Pwd=12345gray;CharSet=utf8mb4;";

        ImageUploader uploader = new ImageUploader(connectionString);
        uploader.UploadAllImages(imageFolder);

        Console.WriteLine("\nHoan tat! Nhan phim bat ky de thoat...");
        Console.ReadKey();
    }
}

public class ImageUploader
{
    private string connectionString;

    public ImageUploader(string connStr)
    {
        connectionString = connStr;
    }

    public void UploadAllImages(string imageFolder)
    {
        var imageMapping = new Dictionary<int, string>
        {
            { 1, "TraDaoCamSa.jpg" },
            { 2, "TraDaoCamSa.jpg" },
            { 3, "dauDo.jpg" },
            { 4, "dauDo.jpg" },
            { 5, "dauHu.jpg" },
            { 6, "dauHu.jpg" },
            { 7, "machaDauDo.jpg" },
            { 8, "machaDauDo.jpg" },
            { 9, "nuocDua.jpg" },
            { 10, "nuocDua.jpg" },
            { 11, "suongSao.jpg" },
            { 12, "suongSao.jpg" },
            { 13, "TraTraiCayNhietDoi.jpg" },
            { 14, "TraTraiCayNhietDoi.jpg" },
            { 15, "tranTrauDen.jpg" },
            { 16, "tranTrauDen.jpg" },
            { 17, "tranTrauTrang.jpg" },
            { 18, "tranTrauTrang.jpg" },
            { 19, "traSuaDauDo.jpg" },
            { 20, "traSuaDauDo.jpg" },
            { 21, "traSuaKhoaiMon.jpg" },
            { 22, "traSuaKhoaiMon.jpg" },
            { 23, "traSuaOreo.jpg" },
            { 24, "traSuaOreo.jpg" }
        };

        int success = 0;
        int failed = 0;

        foreach (var pair in imageMapping)
        {
            int maSP = pair.Key;
            string fileName = pair.Value;
            string fullPath = Path.Combine(imageFolder, fileName);

            Console.Write($"MaSP {maSP:D2} ({fileName})... ");

            if (!File.Exists(fullPath))
            {
                Console.WriteLine("Khong tim thay file");
                failed++;
                continue;
            }

            try
            {
                byte[] imageBytes = File.ReadAllBytes(fullPath);

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string query = "UPDATE SanPham SET HinhAnh = @HinhAnh WHERE MaSP = @MaSP";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@HinhAnh", imageBytes);
                    cmd.Parameters.AddWithValue("@MaSP", maSP);

                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        Console.WriteLine("OK");
                        success++;
                    }
                    else
                    {
                        Console.WriteLine("Khong tim thay MaSP trong DB");
                        failed++;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Loi: {ex.Message}");
                failed++;
            }
        }

        Console.WriteLine("\n========================================");
        Console.WriteLine($"Ket qua: {success} thanh cong, {failed} that bai");
    }
}