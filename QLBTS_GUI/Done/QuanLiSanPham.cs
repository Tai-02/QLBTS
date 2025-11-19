using QLBTS_BLL;
using QLBTS_DAL;
using QLBTS_DTO;
using System;
using System.Data;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using Guna.UI2.WinForms.Enums;

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
            Loadcbb();
        }

        private void Loadcbb()
        {
            QLSP_cbb_loai.Items.Clear();

            var loaiSPList = sanPhamBLL.GetDistinctLoaiSP();
            foreach (var loai in loaiSPList)
            {
                QLSP_cbb_loai.Items.Add(loai);
            }

            QLSP_cbb_loai.Items.Add("...");
            QLSP_cbb_loai.SelectedIndex = 0;

            QLSP_cbb_loai.SelectedIndexChanged -= QLSP_cbb_loai_SelectedIndexChanged;
            QLSP_cbb_loai.SelectedIndexChanged += QLSP_cbb_loai_SelectedIndexChanged;
        }


        private void LoadData()
        {
            dataGridView1.DataSource = sanPhamBLL.GetAllSanPham();
            dataGridView1.ClearSelection();
        }

        // Lam moi form nhap lieu
        private void ClearForm()
        {
            QLSP_txt_tensp.Clear();
            Loadcbb();
            QLSP_txt_giaM.Clear();
            QLSP_txt_giaL.Clear();
            QLSP_txt_soluong.Clear();
            QLSP_txt_khuyenmaiM.Clear();
            QLSP_txt_khuyenmaiL.Clear();
            pictureBox1.Image = null;
            dataGridView1.ClearSelection();
        }

        // Khi click vao 1 dong trong bang
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dataGridView1.Rows[e.RowIndex];

                // Gán dữ liệu vào các control
                QLSP_txt_tensp.Text = row.Cells["TenSP"].Value.ToString();
                QLSP_cbb_loai.SelectedItem = row.Cells["LoaiSP"].Value.ToString();

                // Giá và khuyến mãi cho size M, L
                QLSP_txt_giaM.Text = row.Cells["GiaM"].Value.ToString();
                QLSP_txt_giaL.Text = row.Cells["GiaL"].Value.ToString();
                QLSP_txt_khuyenmaiM.Text = row.Cells["KhuyenMaiM"].Value.ToString();
                QLSP_txt_khuyenmaiL.Text = row.Cells["KhuyenMaiL"].Value.ToString();

                // Số lượng
                QLSP_txt_soluong.Text = row.Cells["SoLuong"].Value.ToString();

                // Ảnh sản phẩm
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
                    TenSP = QLSP_txt_tensp.Text.ToString(),
                    LoaiSP = QLSP_cbb_loai.Text.Trim(),
                    SoLuong = int.Parse(QLSP_txt_soluong.Text),
                    GiaM = int.Parse(QLSP_txt_giaM.Text),
                    GiaL = int.Parse(QLSP_txt_giaL.Text),
                    KhuyenMaiM = int.Parse(QLSP_txt_khuyenmaiM.Text),
                    KhuyenMaiL = int.Parse(QLSP_txt_khuyenmaiL.Text),
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

                if (string.IsNullOrWhiteSpace(QLSP_txt_tensp.Text) ||
                    string.IsNullOrWhiteSpace(QLSP_txt_giaM.Text) ||
                    string.IsNullOrWhiteSpace(QLSP_txt_giaL.Text) ||
                    string.IsNullOrWhiteSpace(QLSP_txt_soluong.Text) ||
                    string.IsNullOrWhiteSpace(QLSP_txt_khuyenmaiM.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin sản phẩm!", "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int maSP = Convert.ToInt32(dataGridView1.CurrentRow.Cells["MaSP"].Value);

                SanPhamDTO sp = new SanPhamDTO
                {
                    MaSP = maSP,
                    TenSP = QLSP_txt_tensp.Text,
                    LoaiSP = QLSP_cbb_loai.Text.Trim(),
                    GiaM = int.Parse(QLSP_txt_giaM.Text),
                    GiaL = int.Parse(QLSP_txt_giaL.Text),
                    SoLuong = int.Parse(QLSP_txt_soluong.Text),
                    KhuyenMaiM = int.Parse(QLSP_txt_khuyenmaiM.Text),
                    KhuyenMaiL = int.Parse(QLSP_txt_khuyenmaiL.Text),
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

        private string ShowInputForm(string title, string labelText)
        {
            string result = null;

            Form form = new Form();
            form.Text = title;
            form.StartPosition = FormStartPosition.CenterParent;
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.Width = 300;
            form.Height = 150;

            Label label = new Label() { Left = 10, Top = 10, Text = labelText, AutoSize = true };
            TextBox textBox = new TextBox() { Left = 10, Top = 35, Width = 260 };
            Button btnOK = new Button() { Text = "OK", Left = 50, Width = 80, Top = 70, DialogResult = DialogResult.OK };
            Button btnCancel = new Button() { Text = "Cancel", Left = 150, Width = 80, Top = 70, DialogResult = DialogResult.Cancel };

            form.Controls.Add(label);
            form.Controls.Add(textBox);
            form.Controls.Add(btnOK);
            form.Controls.Add(btnCancel);

            form.AcceptButton = btnOK;
            form.CancelButton = btnCancel;

            if (form.ShowDialog() == DialogResult.OK)
            {
                result = textBox.Text.Trim();
            }

            form.Dispose();
            return result;
        }

        private void QLSP_cbb_loai_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (QLSP_cbb_loai.SelectedItem != null && QLSP_cbb_loai.SelectedItem.ToString() == "...")
            {
                string loaiMoi = ShowInputForm("Nhập loại mới", "Loại sản phẩm:");

                if (!string.IsNullOrEmpty(loaiMoi))
                {
                    QLSP_cbb_loai.Items.Insert(QLSP_cbb_loai.Items.Count - 1, loaiMoi);
                    QLSP_cbb_loai.SelectedItem = loaiMoi;
                }
                else
                {
                    QLSP_cbb_loai.SelectedIndex = 0;
                }
            }
        }

        private void QLSP_txt_khuyenmaiM_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Chan ky tu khong hop le
            }
        }

        private void QLSP_txt_khuyenmaiL_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Chan ky tu khong hop le
            }
        }

        private void QLSP_txt_soluong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Chan ky tu khong hop le
            }
        }

        private void QLSP_txt_giaL_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Chan ky tu khong hop le
            }
        }
    }
}
