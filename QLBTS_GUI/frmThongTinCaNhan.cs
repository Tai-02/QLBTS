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

        private TaiKhoanDTO TenTK;

        private byte[] newImageData = null;

        private Label lblHoTenLeft;
        private Label lblVaiTroLeft;

        public frmThongTinCaNhan(TaiKhoanDTO user)
        {
            InitializeComponent();

            this.TenTK = user;
            lblHoTenLeft = new Label();
            lblHoTenLeft.Text = "Đang tải..."; // Text tạm thời
            lblHoTenLeft.Font = new Font("Arial", 14, FontStyle.Bold);
            lblHoTenLeft.AutoSize = true;
            lblHoTenLeft.BackColor = Color.Transparent; // Nền trong suốt
            lblHoTenLeft.ForeColor = Color.Black;

            lblVaiTroLeft = new Label();
            lblVaiTroLeft.Text = "Đang tải..."; // Text tạm thời
            lblVaiTroLeft.Font = new Font("Arial", 10, FontStyle.Regular);
            lblVaiTroLeft.AutoSize = true;
            lblVaiTroLeft.BackColor = Color.Transparent;
            lblVaiTroLeft.ForeColor = Color.DimGray;

            TTCN_pnAnhdaidien.Controls.Add(lblHoTenLeft);
            TTCN_pnAnhdaidien.Controls.Add(lblVaiTroLeft);
        }
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
                            TTCN_txt_Tenuser.Text = reader["HoTen"].ToString();
                            TTCN_txt_Email.Text = reader["Email"].ToString();
                            TTCN_txt_Sodienthoai.Text = reader["SDT"].ToString();
                            TTCN_txt_DiaChi.Text = reader["DiaChi"].ToString();

                            lblHoTenLeft.Text = reader["HoTen"].ToString();
                            lblVaiTroLeft.Text = reader["VaiTro"].ToString(); // Lấy từ cột VaiTro

                            int x_center = TTCN_pnAnhdaidien.Width / 2;

                            int y_top_HoTen = TTCN_pc_Anhdaidien.Bottom + 40;
                            lblHoTenLeft.Location = new Point(x_center - (lblHoTenLeft.Width / 2), y_top_HoTen);

                            int y_top_VaiTro = lblHoTenLeft.Bottom + 30;
                            lblVaiTroLeft.Location = new Point(x_center - (lblVaiTroLeft.Width / 2), y_top_VaiTro);

                            if (reader["Anh"] != DBNull.Value)
                            {
                                byte[] imgData = (byte[])reader["Anh"];
                                TTCN_pc_Anhdaidien.Image = ByteArrayToImage(imgData);
                            }
                            else
                            {
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
            if (this.TenTK == null)
            {
                MessageBox.Show("Lỗi: Không có thông tin người dùng để tải.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close(); // Đóng form nếu lỗi
                return;
            }

            try
            {
                LoadUserProfile(this.TenTK.MaTK);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
        }
        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                string trimmedEmail = email.Trim();

                var addr = new System.Net.Mail.MailAddress(trimmedEmail);

                if (addr.Address != trimmedEmail)
                {
                    return false;
                }
                if (!trimmedEmail.EndsWith("@gmail.com", StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        private bool IsAllDigits(string s)
        {
            if (string.IsNullOrEmpty(s))
                return false;

            foreach (char c in s)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }

        private bool IsValidName(string s)
        {
            if (string.IsNullOrEmpty(s))
                return false;

            foreach (char c in s)
            {
                if (!char.IsLetter(c) && !char.IsWhiteSpace(c))
                {
                    return false;
                }
            }
            return true;
        }

        private void TTCN_btnCapnhat_Click(object sender, EventArgs e)
        {
            string ten = TTCN_txt_Tenuser.Text.Trim();
            string sdt = TTCN_txt_Sodienthoai.Text.Trim();
            string email = TTCN_txt_Email.Text.Trim();

            if (string.IsNullOrWhiteSpace(ten))
            {
                MessageBox.Show("Họ tên không được để trống.",
                                "Lỗi Dữ Liệu",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                TTCN_txt_Tenuser.Focus();
                return;
            }
            if (!IsValidName(ten))
            {
                MessageBox.Show("Họ tên chỉ được chứa chữ cái và khoảng trắng, không chứa số hoặc ký tự đặc biệt.",
                                "Lỗi Dữ Liệu",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                TTCN_txt_Tenuser.Focus();
                TTCN_txt_Tenuser.SelectAll();
                return;
            }

            if (string.IsNullOrWhiteSpace(sdt))
            {
                MessageBox.Show("Số điện thoại không được để trống.",
                               "Lỗi Dữ Liệu",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Warning);
                TTCN_txt_Sodienthoai.Focus();
                return;
            }

            if (!IsAllDigits(sdt))
            {
                MessageBox.Show("Số điện thoại chỉ được chứa các chữ số (0-9), không chứa chữ cái hoặc ký tự đặc biệt.",
                                "Lỗi Dữ Liệu",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                TTCN_txt_Sodienthoai.Focus();
                TTCN_txt_Sodienthoai.SelectAll();
                return;
            }

            if (sdt.Length != 10)
            {
                MessageBox.Show("Số điện thoại phải có đúng 10 chữ số.",
                                "Lỗi Dữ Liệu",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                TTCN_txt_Sodienthoai.Focus();
                TTCN_txt_Sodienthoai.SelectAll();
                return;
            }
            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Email không được để trống.",
                                "Lỗi Dữ Liệu",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                TTCN_txt_Email.Focus();
                return;
            }
            if (!IsValidEmail(email))
            {
                MessageBox.Show("Định dạng email không hợp lệ. Vui lòng nhập lại",
                                "Lỗi Dữ Liệu",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                TTCN_txt_Email.Focus();
                TTCN_txt_Email.SelectAll();
                return;
            }
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string query = "UPDATE TaiKhoan SET HoTen = @HoTen, Email = @Email, SDT = @SDT, DiaChi = @DiaChi";

                    if (newImageData != null)
                    {
                        query += ", Anh = @Anh";
                    }

                    query += " WHERE MaTK = @MaTK";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@HoTen", ten);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@SDT", sdt);
                        cmd.Parameters.AddWithValue("@DiaChi", TTCN_txt_DiaChi.Text);
                        cmd.Parameters.AddWithValue("@MaTK", this.TenTK.MaTK);

                        if (newImageData != null)
                        {
                            cmd.Parameters.AddWithValue("@Anh", newImageData);
                        }

                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Cập nhật thành công!");
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

        private byte[] ImageToByteArray(Image image)
        {
            if (image == null) return null;
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, ImageFormat.Jpeg);
                return ms.ToArray();
            }
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

        private void TTCN_pc_Anhdaidien_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    Image tempImage = new Bitmap(ofd.FileName);
                    TTCN_pc_Anhdaidien.Image = tempImage;

                    newImageData = ImageToByteArray(tempImage);
                }
            }
        }
    }
}
