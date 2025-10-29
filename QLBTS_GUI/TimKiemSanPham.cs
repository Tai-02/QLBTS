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

        private QuanliSanPhamDAL sanPhamDAL;
        public TimKiemSanPham()
        {
            InitializeComponent();
            sanPhamDAL = new QuanliSanPhamDAL();
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
            SetPlaceholder();
        }

        private Image ByteArrayToImage(byte[] byteArray)
        {
            if (byteArray == null || byteArray.Length == 0) return null;
            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                Image image = Image.FromStream(ms);
                return image;
            }
        }

        private byte[] ImageToByteArray(Image image)
        {
            if (image == null) return null;
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, image.RawFormat);
                return ms.ToArray();
            }
        }
        private string BuildFilterQuery(string filterType = null)
        {
            string query = "SELECT MaSP, TenSP, Gia, HinhAnh FROM SanPham";
            List<string> whereConditions = new List<string>();

            if (filterType == "BestSeller")
            {
                whereConditions.Add("MaSP BETWEEN 1 AND 5");
            }
            else if (filterType == "Topping")
            {
                whereConditions.Add("MaSP BETWEEN 14 AND 17");
            }
            else if (filterType == "TraTraiCay")
            {
                whereConditions.Add("(MaSP BETWEEN 11 AND 13 AND TenSP NOT LIKE '%Trà sữa%')");
            }
            else if (filterType == "MonMoi")
            {
                whereConditions.Add("((MaSP BETWEEN 6 AND 10) OR (MaSP BETWEEN 11 AND 13 AND TenSP LIKE '%Trà sữa%'))");
            }

            if (whereConditions.Count > 0)
            {
                query += " WHERE " + string.Join(" AND ", whereConditions);
            }

            return query;
        }

        private void loadSanPham(string searchTerm = null, string filterType = null)
        {
            flpSanPham.Controls.Clear();
            flpSanPham.SuspendLayout();

            string query = BuildFilterQuery(filterType);


            MySqlConnection conn = null;
            MySqlDataReader reader = null;

            // Chuẩn hóa từ khóa tìm kiếm bên ngoài vòng lặp
            string normalizedSearchTerm = null;
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                normalizedSearchTerm = RemoveAccents(searchTerm).ToLower();
            }

            try
            {
                conn = DatabaseHelper.GetConnection();
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string tenSP = reader["TenSP"].ToString();
                    
                    if (!string.IsNullOrWhiteSpace(normalizedSearchTerm))
                    {
                        string normalizedTenSP = RemoveAccents(tenSP).ToLower();
                        if (!normalizedTenSP.Contains(normalizedSearchTerm))
                        {
                            continue;
                        }
                    }
                    int maSP = Convert.ToInt32(reader["MaSP"]);

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
                        catch (Exception) {}
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
                    picBestSeller.Size = new Size(38, 38);
                    picBestSeller.Location = new Point(productPanel.Width - picBestSeller.Width - 10, 10);
                    picBestSeller.SizeMode = PictureBoxSizeMode.Zoom;
                    picBestSeller.BackColor = Color.Transparent;
                    picBestSeller.Visible = (maSP >= 1 && maSP <= 5);


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

            if (flpSanPham.Controls.Count == 0)
            {
                Label lblNotFound = new Label();
                lblNotFound.Text = "Không tìm thấy sản phẩm";
                lblNotFound.Font = new Font("Arial", 16, FontStyle.Italic);
                lblNotFound.ForeColor = Color.Gray;
                lblNotFound.Size = new Size(flpSanPham.ClientSize.Width - 20, 50); // Cho label rộng bằng panel
                lblNotFound.TextAlign = ContentAlignment.MiddleCenter;
                lblNotFound.Margin = new Padding(50);
                flpSanPham.Controls.Add(lblNotFound);
            }
            flpSanPham.ResumeLayout(true);
        }

        // Hàm xử lý sự kiện Click (để highlight)
        private void ProductPanel_Click(object sender, EventArgs e)
        {
            Control clickedControl = sender as Control;
            Panel selectedPanel = null;

            if (clickedControl is Panel)
            {
                selectedPanel = (Panel)clickedControl;
            }
            else
            {
                selectedPanel = clickedControl.Parent as Panel;
            }

            if (selectedPanel == null) return;

            foreach (Control ctrl in flpSanPham.Controls)
            {
                if (ctrl is Panel p)
                {
                    p.BackColor = Color.White;
                    p.Padding = new Padding(0);
                }
            }

            selectedPanel.BackColor = Color.DeepSkyBlue;
            selectedPanel.Padding = new Padding(3);
        }

        /*****TIM KIEM SAN PHAM THEO TEXTBOX*****/
        public static string RemoveAccents(string text) // Hàm loại bỏ dấu tiếng Việt
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            text = text.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();
            foreach (char c in text)
            {
                if (System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c) != System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(c);
                }
            }
            // Thay thế chữ 'Đ'/'đ'
            sb.Replace('Đ', 'D');
            sb.Replace('đ', 'd');
            return sb.ToString().Normalize(NormalizationForm.FormC);
        }

        private void SetPlaceholder()
        {
            txtTimkiem.Text = "Tìm Kiếm";
            txtTimkiem.ForeColor = Color.Gray; // Đặt màu mờ
        }


        private void txtTimkiem_TextChanged(object sender, EventArgs e)
        {
            //loadSanPham(txtTimkiem.Text); // COMMMENT: Tìm kiếm theo từng ký tự không cần nhấn nút nhưng LAG QUÁ
        }
        private void txtTimkiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LocSanPhamBangBoLoc(); // MỚI

                e.SuppressKeyPress = true;
            }
        }
        private void txtTimkiem_Enter(object sender, EventArgs e)
        {
            if (txtTimkiem.Text == "Tìm Kiếm" && txtTimkiem.ForeColor == Color.Gray)
            {
                txtTimkiem.Text = "";
                txtTimkiem.ForeColor = Color.Black;
            }
        }
        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            LocSanPhamBangBoLoc();
        }
        private void txtTimkiem_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTimkiem.Text))
            {
                SetPlaceholder();
            }
        }
        private string GetSearchTerm()
        {
            if (txtTimkiem.Text == "Tìm Kiếm" && txtTimkiem.ForeColor == Color.Gray)
            {
                return null;
            }
            return txtTimkiem.Text;
        }

        /*********Su dung bo loc san pham******/
        private void LocSanPhamBangBoLoc()
        {
            string searchTerm = GetSearchTerm();
            string filterType = null;
            if (chkBestseller.Checked)
            {
                filterType = "BestSeller";
            }
            else if (chkMonmoi.Checked)
            {
                filterType = "MonMoi";
            }
            else if (chkTratraicay.Checked)
            {
                filterType = "TraTraiCay";
            }
            else if (chkTopping.Checked)
            {
                filterType = "Topping";
            }
            loadSanPham(searchTerm, filterType);
        }
        private void chkBestseller_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBestseller.Checked)
            {
                chkMonmoi.Checked = false;
                chkTratraicay.Checked = false;
                chkTopping.Checked = false;
            }
            LocSanPhamBangBoLoc();
        }

        private void chkMonmoi_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMonmoi.Checked)
            {
                chkBestseller.Checked = false;
                chkTratraicay.Checked = false;
                chkTopping.Checked = false;
            }
            LocSanPhamBangBoLoc();
        }

        private void chkTratraicay_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTratraicay.Checked)
            {
                chkBestseller.Checked = false;
                chkMonmoi.Checked = false;
                chkTopping.Checked = false;
            }
            LocSanPhamBangBoLoc();
        }

        private void chkTopping_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTopping.Checked)
            {
                chkBestseller.Checked = false;
                chkMonmoi.Checked = false;
                chkTratraicay.Checked = false;
            }
            LocSanPhamBangBoLoc();
        }
    }
}
