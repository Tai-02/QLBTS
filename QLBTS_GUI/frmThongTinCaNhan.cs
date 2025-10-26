using Microsoft.VisualBasic.ApplicationServices;
using MySql.Data.MySqlClient;
using QLBTS_DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace QLBTS_GUI
{
    public partial class frmThongTinCaNhan : Form
    {
        private string connectionString = "Server=127.0.0.1;Database=QLBTS;Uid=root;Pwd=48692005;";

        //private int MaTK = 1; // TEST 
        private TaiKhoanDTO TenTK;

        private byte[] newImageData = null;

        private Label lblHoTenLeft;
        private Label lblVaiTroLeft;

        public frmThongTinCaNhan(TaiKhoanDTO user)
        {
            InitializeComponent();

            //Lưu user được truyền vào ---
            this.TenTK = user;
            // 1. Khởi tạo Label HoTen
            lblHoTenLeft = new Label();
            lblHoTenLeft.Text = "Đang tải..."; // Text tạm thời
            lblHoTenLeft.Font = new Font("Arial", 14, FontStyle.Bold);
            lblHoTenLeft.AutoSize = true;
            lblHoTenLeft.BackColor = Color.Transparent; // Nền trong suốt
            lblHoTenLeft.ForeColor = Color.Black;

            // 2. Khởi tạo Label VaiTro
            lblVaiTroLeft = new Label();
            lblVaiTroLeft.Text = "Đang tải..."; // Text tạm thời
            lblVaiTroLeft.Font = new Font("Arial", 10, FontStyle.Regular);
            lblVaiTroLeft.AutoSize = true;
            lblVaiTroLeft.BackColor = Color.Transparent;
            // (Trong hình của bạn nó màu xám hơn)
            lblVaiTroLeft.ForeColor = Color.DimGray;

            // 3. Thêm 2 label vào Panel bên trái
            TTCN_pnAnhdaidien.Controls.Add(lblHoTenLeft);
            TTCN_pnAnhdaidien.Controls.Add(lblVaiTroLeft);
        }
        // 3. HÀM TẢI DỮ LIỆU (LOAD)
        private void LoadUserProfile(int maTK)
        {
            newImageData = null;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string query = "SELECT HoTen, Email, SDT, DiaChi, Anh, VaiTro FROM TaiKhoan WHERE MaTK = @MaTK";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaTK", maTK);

                    conn.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Đổ dữ liệu vào các TextBox bên phải
                            TTCN_txt_Tenuser.Text = reader["HoTen"].ToString();
                            TTCN_txt_Email.Text = reader["Email"].ToString();
                            TTCN_txt_Sodienthoai.Text = reader["SDT"].ToString();
                            TTCN_txt_DiaChi.Text = reader["DiaChi"].ToString();

                            // --- LOAD DỮ LIỆU VÀO 2 LABEL ĐỘNG ---
                            // 1. Gán text
                            lblHoTenLeft.Text = reader["HoTen"].ToString();
                            lblVaiTroLeft.Text = reader["VaiTro"].ToString(); // Lấy từ cột VaiTro

                            // 2. Căn chỉnh vị trí (sau khi đã có text và AutoSize)
                            // Căn giữa theo chiều ngang của panelLeft
                            int x_center = TTCN_pnAnhdaidien.Width / 2;

                            // Đặt vị trí cho Họ Tên (dưới ảnh 15px)
                            int y_top_HoTen = TTCN_pc_Anhdaidien.Bottom + 40;
                            lblHoTenLeft.Location = new Point(x_center - (lblHoTenLeft.Width / 2), y_top_HoTen);

                            // Đặt vị trí cho Vai Trò (dưới Họ Tên 5px)
                            int y_top_VaiTro = lblHoTenLeft.Bottom + 30;
                            lblVaiTroLeft.Location = new Point(x_center - (lblVaiTroLeft.Width / 2), y_top_VaiTro);

                            // Xử lý ảnh (LONGBLOB)
                            if (reader["Anh"] != DBNull.Value)
                            {
                                byte[] imgData = (byte[])reader["Anh"];
                                TTCN_pc_Anhdaidien.Image = ByteArrayToImage(imgData);
                            }
                            else
                            {
                                // Gán ảnh placeholder (nếu bạn có)
                                TTCN_pc_Anhdaidien.Image = Properties.Resources.icons8_camera_100; 
                            }
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy tài khoản.");
                        }
                    }
                }
            }
        }

        private void frmThongTinCaNhan_Load(object sender, EventArgs e)
        {
            // Kiểm tra xem user có được truyền vào không
            if (this.TenTK == null)
            {
                MessageBox.Show("Lỗi: Không có thông tin người dùng để tải.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close(); // Đóng form nếu lỗi
                return;
            }

            try
            {
                // Dùng MaTK của user đã đăng nhập
                LoadUserProfile(this.TenTK.MaTK);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
        }

        private void TTCN_btnCapnhat_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    // Câu query cơ bản
                    string query = "UPDATE TaiKhoan SET HoTen = @HoTen, Email = @Email, SDT = @SDT, DiaChi = @DiaChi";

                    // CHỈ cập nhật ảnh NẾU người dùng đã chọn ảnh mới
                    if (newImageData != null)
                    {
                        query += ", Anh = @Anh";
                    }

                    query += " WHERE MaTK = @MaTK";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        // Thêm tham số (Parameters) - Rất quan trọng để chống SQL Injection
                        cmd.Parameters.AddWithValue("@HoTen", TTCN_txt_Tenuser.Text);
                        cmd.Parameters.AddWithValue("@Email", TTCN_txt_Email.Text);
                        cmd.Parameters.AddWithValue("@SDT", TTCN_txt_Sodienthoai.Text);
                        cmd.Parameters.AddWithValue("@DiaChi", TTCN_txt_DiaChi.Text);
                        cmd.Parameters.AddWithValue("@MaTK", this.TenTK.MaTK);

                        // CHỈ thêm tham số ảnh NẾU có ảnh mới
                        if (newImageData != null)
                        {
                            cmd.Parameters.AddWithValue("@Anh", newImageData);
                        }

                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Cập nhật thành công!");
                            // Tải lại thông tin để hiển thị tên/ảnh mới ở panel trái
                            LoadUserProfile(this.TenTK.MaTK);
                        }
                        else
                        {
                            MessageBox.Show("Cập nhật thất bại.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật: " + ex.Message);
            }
        }

        private void TTCN_btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                LoadUserProfile(this.TenTK.MaTK);
                MessageBox.Show("Đã hoàn tác các thay đổi chưa lưu.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải lại dữ liệu: " + ex.Message);
            }
        }

        // Chuyển từ Image sang byte[] để lưu vào CSDL
        private byte[] ImageToByteArray(Image image)
        {
            if (image == null) return null;
            using (MemoryStream ms = new MemoryStream())
            {
                // Lưu ảnh vào stream với định dạng Jpeg (bạn có thể chọn Png)
                image.Save(ms, ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }

        // Chuyển từ byte[] (đọc từ CSDL) sang Image để hiển thị
        private Image ByteArrayToImage(byte[] byteArray)
        {
            if (byteArray == null || byteArray.Length == 0) return null;
            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                Image image = Image.FromStream(ms);
                return image;
            }
        }

        private void TTCN_pc_Anhdaidien_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    // Hiển thị ảnh mới lên PictureBox
                    Image tempImage = new Bitmap(ofd.FileName);
                    TTCN_pc_Anhdaidien.Image = tempImage;

                    // Chuyển ảnh sang byte[] và lưu vào biến tạm
                    newImageData = ImageToByteArray(tempImage);
                }
            }
        }
    }
}
