using MySql.Data.MySqlClient;
using QLBTS_DAL;
using QLBTS_DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBTS_GUI
{
    public partial class TimKiemSanPham : Form
    {

        private SanPhamDAL sanPhamDAL;
        public TimKiemSanPham()
        {
            InitializeComponent();
            sanPhamDAL = new SanPhamDAL();
        }

        private void TimKiemSanPham_Load(object sender, EventArgs e)
        {
            // PHAN CODE NÀY CHỈ CHẠY 1 LẦN ĐỂ NẠP ẢNH VÀO CSDL RỒI XÓA HOẶC COMMNET
            /*
            string filePath = ""; // Biến lưu tên file đang xử lý
            try
            {
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                string imageDir = Path.Combine(baseDir, "Images");

                string[] productNames = {
                    "Trà sữa trân châu", "Trà sữa thái xanh", "Trà sữa Matcha",
                    "Trà sữa bạc hà", "Trà sữa xoài kem cheese", "Trà sữa kem trứng nướng",
                    "Trà sữa khoai môn", "Trà sữa Oreo Cake Cream","Trà sữa matcha đậu đỏ",
                    "Trà sữa Pudding đậu đỏ", "Trà đào cam sả","Nước dừa","Trà trái cây nhiệt đới",
                    "Kem sữa","Trân châu đen", "Trân châu trắng", "Sương sáo"
                };

                string[] fileNames = {
                    "trasuatranchau.png", "trasuathaixanh.png", "trasuamatcha.png",
                    "trasuabacha.png", "trasuaxoaikemcheese.png", "trasuakemtrungnuong.jpg",
                    "trasuakhoaimon.jpg", "trasuaoreocakecream.jpg","trasuamatchadaudo.jpg",
                    "trasuapuddingdaudo.jpg","TraDaoCamSa.jpg","nuocdua.jpg","TraTraiCayNhietDoi.jpg",
                    "kemsua.jpg","tranchauden.jpg", "tranchautrang.jpg", "suongsao.jpg"
                };

                for (int i = 0; i < productNames.Length; i++)
                {
                    string tenSP = productNames[i];
                    // Lưu lại tên file đang xử lý
                    filePath = Path.Combine(imageDir, fileNames[i]);

                    // 1. Lớp GUI đọc file
                    Image tempImage = new Bitmap(filePath);

                    // 2. Lớp GUI chuyển đổi
                    byte[] imageBytes = ImageToByteArray(tempImage);
                    tempImage.Dispose();

                    // 3. Lớp GUI gọi DAL với dữ liệu byte[] thô
                    sanPhamDAL.UpdateProductImage(tenSP, imageBytes);
                }
            }
            catch (Exception ex)
            {
                // NÓ SẼ BÁO LỖI VÀ TÊN FILE CHÍNH XÁC Ở ĐÂY
                MessageBox.Show($"CÓ LỖI KHI ĐỌC FILE:\n{filePath}\n\nLỗi: {ex.Message}");
            }
            */
            loadSanPham();
        }

        // HÀM HỖ TRỢ CHUYỂN byte[] SANG Image
        private Image ByteArrayToImage(byte[] byteArray)
        {
            if (byteArray == null || byteArray.Length == 0) return null;
            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                Image image = Image.FromStream(ms);
                return image;
            }
        }

        // HÀM HỖ TRỢ CHUYỂN Image SANG byte[] (Dùng khi Thêm/Sửa sản phẩm)
        private byte[] ImageToByteArray(Image image)
        {
            if (image == null) return null;
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, image.RawFormat);
                return ms.ToArray();
            }
        }

        private void loadSanPham()
        {
            flpSanPham.Controls.Clear();

            string query;
            bool showBestSellerTag = false;
            query = "SELECT TenSP, Gia, HinhAnh FROM SanPham";

            MySqlConnection conn = null;
            MySqlDataReader reader = null;

            try
            {
                conn = DatabaseHelper.GetConnection();
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string tenSP = reader["TenSP"].ToString();
                    int gia = Convert.ToInt32(reader["Gia"]);
                    Image hinhAnh = Properties.Resources.icons8_camera_100; // Mặc định

                    if (reader["HinhAnh"] != DBNull.Value)
                    {
                        try
                        {
                            byte[] imgBytes = (byte[])reader["HinhAnh"];
                            using (MemoryStream ms = new MemoryStream(imgBytes))
                            {
                                hinhAnh = Image.FromStream(ms);
                            }
                        }
                        catch (Exception) { /* Dùng ảnh placeholder */ }
                    }

                    // --- Tạo thẻ sản phẩm động ---
                    Panel productPanel = new Panel();
                    productPanel.Size = new Size(220, 220);
                    productPanel.BackColor = Color.White;
                    productPanel.Margin = new Padding(10);
                    productPanel.Cursor = Cursors.Hand;

                    PictureBox picImage = new PictureBox();
                    picImage.Image = hinhAnh;
                    picImage.Size = new Size(150, 120);
                    picImage.Location = new Point(30, 10);
                    picImage.SizeMode = PictureBoxSizeMode.Zoom;

                    PictureBox picBestSeller = new PictureBox();
                    picBestSeller.Size = new Size(180, 120);
                    picBestSeller.Location = new Point(135, 5);
                    picBestSeller.SizeMode = PictureBoxSizeMode.Zoom;
                    picBestSeller.BackColor = Color.Transparent;
                    // --- Logic quan trọng ---
                    picBestSeller.Visible = showBestSellerTag; // Chỉ hiển thị khi cờ là true

                    Label lblName = new Label();
                    lblName.Text = tenSP;
                    lblName.Font = new Font("Arial", 10, FontStyle.Bold);
                    lblName.Location = new Point(20, 140);
                    lblName.Size = new Size(160, 40);
                    lblName.TextAlign = ContentAlignment.TopCenter;

                    Label lblPrice = new Label();
                    lblPrice.Text = $"Giá: {gia:N0}đ";
                    lblPrice.Font = new Font("Arial", 9, FontStyle.Regular);
                    lblPrice.ForeColor = Color.Red;
                    lblPrice.Location = new Point(60, 185);
                    lblPrice.AutoSize = true;

                    productPanel.Controls.Add(picImage);
                    productPanel.Controls.Add(lblName);
                    productPanel.Controls.Add(lblPrice);
                    productPanel.Controls.Add(picBestSeller);
                    picBestSeller.BringToFront();

                    productPanel.Click += ProductPanel_Click;
                    picImage.Click += ProductPanel_Click;
                    picBestSeller.Click += ProductPanel_Click;
                    lblName.Click += ProductPanel_Click;
                    lblPrice.Click += ProductPanel_Click;

                    flpSanPham.Controls.Add(productPanel);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải sản phẩm: " + ex.Message);
            }
            finally
            {
                if (reader != null) reader.Close();
                if (conn != null && conn.State == ConnectionState.Open) conn.Close();
            }
        }

        // 3. Hàm xử lý sự kiện Click (để highlight)
        private void ProductPanel_Click(object sender, EventArgs e)
        {
            Control clickedControl = sender as Control;
            Panel selectedPanel = null;

            if (clickedControl is Panel)
            {
                // Nếu click vào chính Panel
                selectedPanel = (Panel)clickedControl;
            }
            else
            {
                selectedPanel = clickedControl.Parent as Panel;
            }

            if (selectedPanel == null) return;

            // 1. Bỏ chọn tất cả các Panel khác
            foreach (Control ctrl in flpSanPham.Controls)
            {
                if (ctrl is Panel p)
                {
                    p.BackColor = Color.White; // Trả về màu nền cũ
                    p.Padding = new Padding(0); // Bỏ viền
                }
            }

            selectedPanel.BackColor = Color.DeepSkyBlue; // Đặt màu nền highlight
            selectedPanel.Padding = new Padding(3);      // Tạo hiệu ứng viền xanh
        }

        private void chkBestseller_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void chkMonmoi_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
