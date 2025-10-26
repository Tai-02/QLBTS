using QLBTS_BLL;
using QLBTS_DAL;
using QLBTS_DTO;
using System;
using System.Data;
using System.Windows.Forms;

namespace QLBTS_GUI
{
    public partial class FormSanPham : Form
    {
        private bool isEditing = false;
        private SanPhamBLL sanPhamBLL = new SanPhamBLL();

        public FormSanPham()
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
            isEditing = false;
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
                isEditing = true;
            }
        }

        // Them san pham moi
        private void them_btn_Click(object sender, EventArgs e)
        {
            if (isEditing)
            {
                MessageBox.Show("Bạn đang ở chế độ chỉnh sửa.\nDouble-click vùng trống để quay lại chế độ thêm mới!");
                return;
            }

            try
            {
                SanPham sp = new SanPham
                {
                    TenSP = textBox1.Text,
                    Gia = decimal.Parse(textBox2.Text),
                    Size = textBox3.Text,
                    SoLuong = int.Parse(textBox4.Text),
                    KhuyenMai = 0,
                    HinhAnh = null
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
            if (!isEditing)
            {
                MessageBox.Show("Bạn chưa chọn sản phẩm nào để cập nhật!");
                return;
            }

            try
            {
                int maSP = (int)dataGridView1.CurrentRow.Cells["MaSP"].Value;
                SanPham sp = new SanPham
                {
                    MaSP = maSP,
                    TenSP = textBox1.Text,
                    Gia = decimal.Parse(textBox2.Text),
                    Size = textBox3.Text,
                    SoLuong = int.Parse(textBox4.Text),
                    KhuyenMai = 0,
                    HinhAnh = null
                };

                if (sanPhamBLL.CapNhatSanPham(sp))
                {
                    MessageBox.Show("Cập nhật thành công!");
                    LoadData();
                    ClearForm();
                }
                else
                {
                    MessageBox.Show("Không có thay đổi nào được cập nhật!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật: " + ex.Message);
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
    }
}
