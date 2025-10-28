using MySql.Data.MySqlClient;
using QLBTS_GUI.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace QLBTS_GUI
{
    public partial class TrangChu : Form
    {
        string connectionString = "Server=127.0.0.1;Database=QLBTS;Uid=root;Pwd=48692005;";
        public TrangChu()
        {
            InitializeComponent();
        }


        private void TrangChu_Load(object sender, EventArgs e)
        {
        //    this.BackgroundImage = Image.FromFile("Images/AnhNenSP.jpg");
        //    this.BackgroundImageLayout = ImageLayout.Stretch;

        //    LoadBestSellerProducts();
        }
        private void LoadBestSellerProducts()
        {
            //flpBestSeller.Padding = new Padding(0, 120, 0, 0);

            //flpBestSeller.Controls.Clear();
            //flpBestSeller.AutoScroll = true;
            //flpBestSeller.WrapContents = true;
            //flpBestSeller.FlowDirection = FlowDirection.LeftToRight;
            //flpBestSeller.BackColor = Color.Transparent; // cho nền nhìn tự nhiên

            //try
            //{
            //    using (MySqlConnection conn = new MySqlConnection(connectionString))
            //    {
            //        conn.Open();
            //        string query = "SELECT MaSP, TenSP, Gia, HinhAnh FROM SanPham WHERE MaSP BETWEEN 1 AND 5";

            //        using (MySqlCommand cmd = new MySqlCommand(query, conn))
            //        using (MySqlDataReader reader = cmd.ExecuteReader())
            //        {
            //            while (reader.Read())
            //            {
            //                try
            //                {
            //                    if (reader["HinhAnh"] == DBNull.Value) continue;

            //                    string tenSP = reader["TenSP"].ToString();
            //                    decimal gia = Convert.ToDecimal(reader["Gia"]);
            //                    byte[] imageData = (byte[])reader["HinhAnh"];

            //                    Image hinhAnh;
            //                    using (MemoryStream ms = new MemoryStream(imageData))
            //                    using (var temp = Image.FromStream(ms))
            //                    {
            //                        hinhAnh = new Bitmap(temp);
            //                    }

            //                    CreateProductDisplay(tenSP, gia, hinhAnh);
            //                }
            //                catch (Exception ex)
            //                {
            //                    Console.WriteLine("Lỗi xử lý sản phẩm: " + ex.Message);
            //                    continue;
            //                }
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            //}
        }

        private void CreateProductDisplay(string tenSP, decimal gia, Image hinhAnh)
        {
            //Panel productPanel = new Panel
            //{
            //    Width = 220,
            //    Height = 300,
            //    BorderStyle = BorderStyle.FixedSingle,
            //    Margin = new Padding(10),
            //    BackColor = Color.White
            //};

            //// ✅ Hình ảnh sản phẩm
            //PictureBox pic = new PictureBox
            //{
            //    Image = hinhAnh,
            //    SizeMode = PictureBoxSizeMode.Zoom,
            //    Width = 200,
            //    Height = 200,
            //    Location = new Point(10, 10),
            //    Cursor = Cursors.Hand
            //};

            //// ✅ Tên sản phẩm
            //Label lblTen = new Label
            //{
            //    Text = tenSP,
            //    Font = new Font("Arial", 11, FontStyle.Bold),
            //    TextAlign = ContentAlignment.MiddleCenter,
            //    Width = 200,
            //    Location = new Point(10, 220)
            //};

            //// ✅ Giá sản phẩm
            //Label lblGia = new Label
            //{
            //    Text = $"Giá: {gia:N0}đ",
            //    Font = new Font("Arial", 10, FontStyle.Regular),
            //    ForeColor = Color.Red,
            //    TextAlign = ContentAlignment.MiddleCenter,
            //    Width = 200,
            //    Location = new Point(10, 250)
            //};

            //// ✅ Thêm vào panel
            //productPanel.Controls.Add(pic);
            //productPanel.Controls.Add(lblTen);
            //productPanel.Controls.Add(lblGia);

            //// ✅ Thêm panel sản phẩm vào FlowLayoutPanel
            //flpBestSeller.Controls.Add(productPanel);
        }
    }
}
