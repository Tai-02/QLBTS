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
            LoadBestSellers();
        }

        private void LoadBestSellers()
        {
            // 1. Gom nhóm các controls để dễ dàng truy cập trong vòng lặp
            // Đảm bảo tên control khớp với tên bạn đặt trong Designer
            List<Panel> panels = new List<Panel>
            {
                pnBestSeller1, pnBestSeller2, pnBestSeller3, pnBestSeller4, pnBestSeller5
            };

            List<PictureBox> pictureBoxes = new List<PictureBox>
            {
                pcBest1, pcBest2, pcBest3, pcBest4, pcBest5
            };

            List<Label> nameLabels = new List<Label>
            {
                lbTenSPBest1, lbTenSPBest2, lbTenSPBest3, lbTenSPBest4, lbTenSPBest5
            };

            List<Label> priceLabels = new List<Label>
            {
                lbGiaSPBest1, lbGiaSPBest2, lbGiaSPBest3, lbGiaSPBest4, lbGiaSPBest5
            };

            // Ẩn tất cả các panel trước khi tải
            foreach (Panel p in panels)
            {
                p.Visible = false;
            }

            MySqlConnection conn = null;
            try
            {
                conn = new MySqlConnection(connectionString);
                conn.Open();

                // 2. Viết truy vấn SQL
                // Lấy 5 sản phẩm có số lượng bán cao nhất
                //string query = "SELECT TenSP, Gia, HinhAnh FROM SanPham ORDER BY SoLuongDaBan DESC LIMIT 5";
                string query = "SELECT MaSP, TenSP, Gia, HinhAnh FROM SanPham WHERE MaSP BETWEEN 1 AND 5";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                // 3. Dùng MySqlDataReader để đọc dữ liệu
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    int index = 0; // Biến đếm để gán vào đúng panel
                    while (reader.Read() && index < panels.Count)
                    {
                        // Lấy dữ liệu từ cột
                        string tenSP = reader["TenSP"].ToString();
                        decimal giaBan = Convert.ToDecimal(reader["Gia"]);

                        // Lấy dữ liệu hình ảnh (dạng BLOB)
                        byte[] imgData = (byte[])reader["HinhAnh"];

                        // 4. Gán dữ liệu vào controls

                        // Xử lý tên và giá
                        nameLabels[index].Text = tenSP;
                        priceLabels[index].Text = giaBan.ToString("N0") + " VNĐ"; // Định dạng tiền tệ 100,000 VNĐ

                        // Xử lý hình ảnh
                        if (imgData != null && imgData.Length > 0)
                        {
                            using (MemoryStream ms = new MemoryStream(imgData))
                            {
                                pictureBoxes[index].Image = Image.FromStream(ms);
                            }
                        }

                        // Hiển thị panel này lên
                        panels[index].Visible = true;

                        // Tăng biến đếm
                        index++;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu Best Seller: " + ex.Message);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }
    }
}
