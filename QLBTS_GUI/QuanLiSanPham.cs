using QLBTS_BLL;
using QLBTS_DAL;
using QLBTS_DTO;
using System;
using System.Data;
using System.Windows.Forms;

namespace QLBTS_GUI
{
    public partial class QuanLiSanPham : Form
    {
        private QuanLiSanPhamBLL sanPhamBLL = new QuanLiSanPhamBLL();
        private byte[] tempAnh = null;

        public QuanLiSanPham()
        {
            InitializeComponent();
        }

        private void FormSanPham_Load(object sender, EventArgs e)
        {
            LoadData();
            ClearForm();
        }

        // Load toan bo san pham
        private void LoadData()
        {
            dataGridView1.DataSource = sanPhamBLL.GetAllSanPham();
            dataGridView1.ClearSelection();
        }

        // Lam moi form nhap lieu
        private void ClearForm()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            pictureBox1.Image = null;
            dataGridView1.ClearSelection();
        }

        // Khi click vao 1 dong trong bang
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dataGridView1.Rows[e.RowIndex];
                textBox1.Text = row.Cells["TenSP"].Value.ToString();
                textBox2.Text = row.Cells["Gia"].Value.ToString();
                textBox3.Text = row.Cells["Size"].Value.ToString();
                textBox4.Text = row.Cells["SoLuong"].Value.ToString();
                textBox5.Text = row.Cells["KhuyenMai"].Value.ToString();

                if (row.Cells["HinhAnh"] != null)
                {
                    object value = row.Cells["HinhAnh"].Value;

                    if (value != DBNull.Value && value != null)
                    {
                        try
                        {
                            byte[] imageData = (byte[])value;
                            using (MemoryStream ms = new MemoryStream(imageData))
                            {
                                pictureBox1.Image = Image.FromStream(ms);
                                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                            }
                            tempAnh = imageData;
                        }
                        catch
                        {
                            pictureBox1.Image = null;
                            tempAnh = null;
                        }
                    }
                    else
                    {
                        pictureBox1.Image = null;
                        tempAnh = null;
                    }
                }
                else
                {
                    pictureBox1.Image = null;
                    tempAnh = null;
                }
            }
        }


        // Them san pham moi
        private void them_btn_Click(object sender, EventArgs e)
        {
            try
            {
                SanPhamDTO sp = new SanPhamDTO
                {
                    TenSP = textBox1.Text.ToString(),
                    Size = textBox3.Text.ToString(),
                    SoLuong = int.Parse(textBox4.Text),
                    Gia = int.Parse(textBox2.Text),
                    KhuyenMai = int.Parse(textBox5.Text),
                    HinhAnh = tempAnh,
                };

                if (sanPhamBLL.ThemSanPham(sp))
                {
                    MessageBox.Show("Thêm sản phẩm thành công!");
                    LoadData();
                    ClearForm();
                }
                else
                {
                    MessageBox.Show("Lỗi khi thêm sản phẩm!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        // Xoa san pham
        private void xoa_btn_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int maSP = (int)dataGridView1.CurrentRow.Cells["MaSP"].Value;

                var confirm = MessageBox.Show("Bạn có chắc chắn muốn xóa sản phẩm này?",
                    "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (confirm == DialogResult.Yes)
                {
                    if (sanPhamBLL.XoaSanPham(maSP))
                    {
                        MessageBox.Show("Đã xóa sản phẩm!");
                        LoadData();
                        ClearForm();
                    }
                    else
                    {
                        MessageBox.Show("Xóa thất bại!");
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần xóa!");
            }
        }

        // Cap nhat san pham
        private void capnhat_btn_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow == null)
                {
                    MessageBox.Show("Vui lòng chọn một sản phẩm để cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                    string.IsNullOrWhiteSpace(textBox2.Text) ||
                    string.IsNullOrWhiteSpace(textBox3.Text) ||
                    string.IsNullOrWhiteSpace(textBox4.Text) ||
                    string.IsNullOrWhiteSpace(textBox5.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin sản phẩm!", "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int maSP = Convert.ToInt32(dataGridView1.CurrentRow.Cells["MaSP"].Value);

                SanPhamDTO sp = new SanPhamDTO
                {
                    MaSP = maSP,
                    TenSP = textBox1.Text,
                    Gia = int.Parse(textBox2.Text),
                    Size = textBox3.Text,
                    SoLuong = int.Parse(textBox4.Text),
                    KhuyenMai = int.Parse(textBox5.Text),
                    HinhAnh = tempAnh 
                };
                if (sanPhamBLL.CapNhatSanPham(sp))
                {
                    MessageBox.Show("Cập nhật thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    ClearForm();
                }
                else
                {
                    MessageBox.Show("Không có thay đổi nào được cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // Double click de lam moi form
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                ClearForm();
                MessageBox.Show("Form đã được làm mới!", "Làm mới", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            var me = (MouseEventArgs)e;
            var hit = dataGridView1.HitTest(me.X, me.Y);
            if (hit.RowIndex == -1)
            {
                ClearForm();
                MessageBox.Show("Form đã được làm mới!", "Làm mới", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Ảnh (*.jpg; *.jpeg; *.png)|*.jpg;*.jpeg;*.png";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;

                pictureBox1.Image = Image.FromFile(filePath);

                tempAnh = File.ReadAllBytes(filePath);

                MessageBox.Show("Ảnh đã được chọn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

    }
}
