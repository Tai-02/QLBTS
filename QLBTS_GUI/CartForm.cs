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
    public partial class CartForm : Form
    {
        public CartForm()
        {
            InitializeComponent();
            ConfigureLayout();
            LoadCartItems();
        }

        private void lblItemCount_Click(object sender, EventArgs e)
        {

        }




        private void ConfigureLayout()
        {
            //// Cấu hình Form
            //this.ClientSize = new Size(950, 550);  
            //this.BackColor = Color.White;  
            //this.FormBorderStyle = FormBorderStyle.FixedDialog;
            //this.StartPosition = FormStartPosition.CenterScreen;
            //this.MaximizeBox = false;
            //this.Text = "Giỏ hàng - MOMOCHA";

            //// leftPanel - SÁT MÉP TRÁI
            //leftPanel.Location = new Point(0, 0);  
            //leftPanel.Size = new Size(620, 660);   
            //leftPanel.BackColor = Color.White;
            //leftPanel.AutoScroll = true;

            //// rightPanel - SÁT BÊN leftPanel
            //rightPanel.Location = new Point(620, 0);  
            //rightPanel.Size = new Size(400, 660);     
            //rightPanel.BackColor = ColorTranslator.FromHtml("#E8F4F8");

            //// Đảm bảo rightPanel không bị đè
            //rightPanel.BringToFront();
        }

        private void LoadCartItems()
        {
            // Xóa sản phẩm cũ (nếu có)
            productContainer.Controls.Clear();

            // Thêm 2 sản phẩm với ảnh thật
            AddProductItem("Trà đào cam sả", "M", 50000, 2, "TraDaoCamSa.jpg");
            AddProductItem("Trà trái cây nhiệt đới", "M", 100000, 1, "TraTraiCayNhietDoi.jpg");

            // Cập nhật số lượng sản phẩm
            lblItemCount.Text = $"{productContainer.Controls.Count} sản phẩm";

            // Cập nhật tổng tiền
            UpdateTotal();
        }

        private void AddProductItem(string name, string size, decimal price, int quantity, string imagePath)
        {
            // Panel chứa 1 sản phẩm
            Panel itemPanel = new Panel
            {
                Width = productContainer.Width - 20,  // Tự động theo container
                Height = 110,
                BackColor = Color.White,
                Margin = new Padding(0, 0, 0, 10)
            };

            // ===== ẢNH SẢN PHẨM =====
            PictureBox productImage = new PictureBox
            {
                Size = new Size(80, 80),
                Location = new Point(15, 15),
                SizeMode = PictureBoxSizeMode.Zoom,
                BorderStyle = BorderStyle.None
            };

            // Load ảnh từ folder Images
            try
            {
                string fullPath = Path.Combine(Application.StartupPath, "Images", imagePath);
                if (File.Exists(fullPath))
                {
                    productImage.Image = Image.FromFile(fullPath);
                }
                else
                {
                    productImage.BackColor = Color.LightBlue;
                }
            }
            catch
            {
                productImage.BackColor = Color.LightGray;
            }

            // Làm tròn góc ảnh
            productImage.Paint += (s, e) =>
            {
                System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
                path.AddEllipse(0, 0, 80, 80);
                ((PictureBox)s).Region = new Region(path);
            };
            itemPanel.Controls.Add(productImage);

            // ===== TÊN SẢN PHẨM =====
            Label nameLabel = new Label
            {
                Text = name,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Location = new Point(110, 15),
                Size = new Size(200, 25),
                AutoSize = false,
                ForeColor = Color.Black
            };
            itemPanel.Controls.Add(nameLabel);

            // ===== SIZE =====
            Label sizeLabel = new Label
            {
                Text = $"Size    {size}",
                Font = new Font("Segoe UI", 10),
                Location = new Point(110, 45),
                AutoSize = true,
                ForeColor = Color.Gray
            };
            itemPanel.Controls.Add(sizeLabel);

            // ===== GIÁ =====
            Label priceLabel = new Label
            {
                Text = $"Giá:    {price:N0}đ",
                Font = new Font("Segoe UI", 10),
                Location = new Point(110, 70),
                AutoSize = true,
                ForeColor = Color.Black
            };
            itemPanel.Controls.Add(priceLabel);

            // ===== NÚT GIẢM (-) =====
            Button minusBtn = new Button
            {
                Text = "−",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Size = new Size(35, 35),
                Location = new Point(300, 50),
                BackColor = Color.WhiteSmoke,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            minusBtn.FlatAppearance.BorderSize = 0;

            // ===== LABEL SỐ LƯỢNG =====
            Label qtyLabel = new Label
            {
                Text = quantity.ToString(),
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Location = new Point(340, 50),
                Size = new Size(45, 30),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.Black,
                Tag = quantity
            };
            itemPanel.Controls.Add(qtyLabel);

            // Gán Tag cho nút trừ
            minusBtn.Tag = qtyLabel;
            minusBtn.Click += MinusBtn_Click;
            itemPanel.Controls.Add(minusBtn);

            // ===== NÚT TĂNG (+) =====
            Button plusBtn = new Button
            {
                Text = "+",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Size = new Size(35, 35),
                Location = new Point(390, 50),
                BackColor = Color.WhiteSmoke,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Tag = qtyLabel
            };
            plusBtn.FlatAppearance.BorderSize = 0;
            plusBtn.Click += PlusBtn_Click;
            itemPanel.Controls.Add(plusBtn);

            // ===== NÚT XÓA =====
            Button deleteBtn = new Button
            {
                Text = "🗑",
                Font = new Font("Segoe UI", 14),
                Size = new Size(40, 35),
                Location = new Point(440, 50),
                BackColor = ColorTranslator.FromHtml("#FFEBEE"),
                ForeColor = Color.Red,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Tag = itemPanel
            };
            deleteBtn.FlatAppearance.BorderSize = 0;
            deleteBtn.Click += DeleteBtn_Click;
            itemPanel.Controls.Add(deleteBtn);

            // ===== ĐƯỜNG KẺ PHÂN CÁCH =====
            Panel separator = new Panel
            {
                Size = new Size(itemPanel.Width, 1),
                Location = new Point(0, 109),
                BackColor = Color.LightGray
            };
            itemPanel.Controls.Add(separator);

            // Thêm vào container
            productContainer.Controls.Add(itemPanel);
        }

        // Sự kiện giảm số lượng
        private void MinusBtn_Click(object? sender, EventArgs e)
        {
            if (sender is Button btn && btn.Tag is Label qtyLabel)
            {
                int currentQty = Convert.ToInt32(qtyLabel.Text);

                if (currentQty > 1)
                {
                    currentQty--;
                    qtyLabel.Text = currentQty.ToString();
                    qtyLabel.Tag = currentQty;
                    UpdateTotal();
                }
            }
        }

        // Sự kiện tăng số lượng
        private void PlusBtn_Click(object? sender, EventArgs e)
        {
            if (sender is Button btn && btn.Tag is Label qtyLabel)
            {
                int currentQty = Convert.ToInt32(qtyLabel.Text);
                currentQty++;

                qtyLabel.Text = currentQty.ToString();
                qtyLabel.Tag = currentQty;
                UpdateTotal();
            }
        }

        // Sự kiện xóa sản phẩm
        private void DeleteBtn_Click(object? sender, EventArgs e)
        {
            if (sender is Button btn && btn.Tag is Panel itemPanel)
            {
                DialogResult result = MessageBox.Show(
                    "Bạn có chắc muốn xóa sản phẩm này?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    productContainer.Controls.Remove(itemPanel);
                    lblItemCount.Text = $"{productContainer.Controls.Count} sản phẩm";
                    UpdateTotal();
                }
            }
        }

        // Cập nhật tổng tiền
        private void UpdateTotal()
        {
            decimal total = 0;

            foreach (Control ctrl in productContainer.Controls)
            {
                if (ctrl is Panel itemPanel)
                {
                    // Tìm label giá và số lượng trong panel
                    Label? priceLabel = null;
                    Label? qtyLabel = null;

                    foreach (Control c in itemPanel.Controls)
                    {
                        if (c is Label lbl)
                        {
                            if (lbl.Text.StartsWith("Giá:"))
                                priceLabel = lbl;
                            else if (lbl.TextAlign == ContentAlignment.MiddleCenter)
                                qtyLabel = lbl;
                        }
                    }

                    if (priceLabel != null && qtyLabel != null)
                    {
                        // Lấy giá từ text "Giá: 50.000đ"
                        string priceText = priceLabel.Text
                            .Replace("Giá:", "")
                            .Replace("đ", "")
                            .Replace(".", "")
                            .Replace(",", "")
                            .Trim();

                        if (decimal.TryParse(priceText, out decimal price))
                        {
                            if (int.TryParse(qtyLabel.Text, out int qty))
                            {
                                total += price * qty;
                            }
                        }
                    }
                }
            }

            // Cập nhật labels trong rightPanel
            lblTempValue.Text = $"{total:N0}đ";
            lblTotalValue.Text = $"{total:N0}đ";
        }

        // Sự kiện nút Thanh toán
        private void btnCheckout_Click(object? sender, EventArgs e)
        {
            MessageBox.Show(
                "Chuyển đến trang thanh toán!",
                "Thanh toán",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        // Sự kiện link Tiếp tục mua hàng
        private void btnContinue_Click(object sender, EventArgs e)
        {

        MessageBox.Show(
                "Quay lại trang chủ!",
                "Mua hàng",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }
    
    }
}
