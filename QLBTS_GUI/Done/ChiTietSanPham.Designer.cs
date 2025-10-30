namespace QLBTS_GUI
{
    partial class ChiTietSanPham
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            leftPanel = new Panel();
            productImage = new PictureBox();
            rightPanel = new Panel();
            btnBuyNow = new Guna.UI2.WinForms.Guna2Button();
            btnAddToCart = new Guna.UI2.WinForms.Guna2Button();
            pnlQuantity = new Panel();
            btnPlus = new Guna.UI2.WinForms.Guna2Button();
            txtQuantity = new Guna.UI2.WinForms.Guna2TextBox();
            btnMinus = new Guna.UI2.WinForms.Guna2Button();
            lblQuantityLabel = new Label();
            rdoSizeL = new RadioButton();
            rdoSizeM = new RadioButton();
            lblSizeLabel = new Label();
            lblPrice = new Label();
            lblProductName = new Label();
            leftPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)productImage).BeginInit();
            rightPanel.SuspendLayout();
            pnlQuantity.SuspendLayout();
            SuspendLayout();
            // 
            // leftPanel
            // 
            leftPanel.Controls.Add(productImage);
            leftPanel.Dock = DockStyle.Left;
            leftPanel.Location = new Point(0, 0);
            leftPanel.Name = "leftPanel";
            leftPanel.Size = new Size(400, 453);
            leftPanel.TabIndex = 0;
            // 
            // productImage
            // 
            productImage.Location = new Point(50, 50);
            productImage.Name = "productImage";
            productImage.Size = new Size(300, 400);
            productImage.SizeMode = PictureBoxSizeMode.Zoom;
            productImage.TabIndex = 0;
            productImage.TabStop = false;
            // 
            // rightPanel
            // 
            rightPanel.Controls.Add(btnBuyNow);
            rightPanel.Controls.Add(btnAddToCart);
            rightPanel.Controls.Add(pnlQuantity);
            rightPanel.Controls.Add(lblQuantityLabel);
            rightPanel.Controls.Add(rdoSizeL);
            rightPanel.Controls.Add(rdoSizeM);
            rightPanel.Controls.Add(lblSizeLabel);
            rightPanel.Controls.Add(lblPrice);
            rightPanel.Controls.Add(lblProductName);
            rightPanel.Dock = DockStyle.Fill;
            rightPanel.Location = new Point(400, 0);
            rightPanel.Name = "rightPanel";
            rightPanel.Padding = new Padding(30);
            rightPanel.Size = new Size(382, 453);
            rightPanel.TabIndex = 1;
            // 
            // btnBuyNow
            // 
            btnBuyNow.BackColor = Color.Transparent;
            btnBuyNow.BorderRadius = 12;
            btnBuyNow.Cursor = Cursors.Hand;
            btnBuyNow.CustomizableEdges = customizableEdges1;
            btnBuyNow.DisabledState.BorderColor = Color.DarkGray;
            btnBuyNow.DisabledState.CustomBorderColor = Color.DarkGray;
            btnBuyNow.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnBuyNow.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnBuyNow.FillColor = Color.Red;
            btnBuyNow.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 163);
            btnBuyNow.ForeColor = Color.White;
            btnBuyNow.HoverState.FillColor = Color.FromArgb(220, 0, 0);
            btnBuyNow.Location = new Point(30, 380);
            btnBuyNow.Name = "btnBuyNow";
            btnBuyNow.ShadowDecoration.Color = Color.Red;
            btnBuyNow.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnBuyNow.ShadowDecoration.Depth = 10;
            btnBuyNow.ShadowDecoration.Enabled = true;
            btnBuyNow.Size = new Size(160, 50);
            btnBuyNow.TabIndex = 8;
            btnBuyNow.Text = "MUA NGAY";
            btnBuyNow.Click += btnBuyNow_Click;
            // 
            // btnAddToCart
            // 
            btnAddToCart.BackColor = Color.Transparent;
            btnAddToCart.BorderRadius = 12;
            btnAddToCart.Cursor = Cursors.Hand;
            btnAddToCart.CustomizableEdges = customizableEdges3;
            btnAddToCart.DisabledState.BorderColor = Color.DarkGray;
            btnAddToCart.DisabledState.CustomBorderColor = Color.DarkGray;
            btnAddToCart.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnAddToCart.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnAddToCart.FillColor = Color.Red;
            btnAddToCart.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnAddToCart.ForeColor = Color.White;
            btnAddToCart.HoverState.FillColor = Color.FromArgb(180, 60, 60);
            btnAddToCart.Location = new Point(210, 380);
            btnAddToCart.Name = "btnAddToCart";
            btnAddToCart.ShadowDecoration.Color = Color.FromArgb(200, 80, 80);
            btnAddToCart.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnAddToCart.ShadowDecoration.Depth = 10;
            btnAddToCart.ShadowDecoration.Enabled = true;
            btnAddToCart.Size = new Size(160, 50);
            btnAddToCart.TabIndex = 7;
            btnAddToCart.Text = "THÊM VÀO GIỎ";
            btnAddToCart.Click += btnAddToCart_Click;
            // 
            // pnlQuantity
            // 
            pnlQuantity.BackColor = Color.Transparent;
            pnlQuantity.Controls.Add(btnPlus);
            pnlQuantity.Controls.Add(txtQuantity);
            pnlQuantity.Controls.Add(btnMinus);
            pnlQuantity.Location = new Point(30, 300);
            pnlQuantity.Name = "pnlQuantity";
            pnlQuantity.Size = new Size(150, 40);
            pnlQuantity.TabIndex = 6;
            // 
            // btnPlus
            // 
            btnPlus.BorderRadius = 8;
            btnPlus.Cursor = Cursors.Hand;
            btnPlus.CustomizableEdges = customizableEdges5;
            btnPlus.DisabledState.BorderColor = Color.DarkGray;
            btnPlus.DisabledState.CustomBorderColor = Color.DarkGray;
            btnPlus.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnPlus.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnPlus.FillColor = Color.DarkGray;
            btnPlus.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 163);
            btnPlus.ForeColor = Color.Black;
            btnPlus.Location = new Point(110, 0);
            btnPlus.Name = "btnPlus";
            btnPlus.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnPlus.Size = new Size(40, 40);
            btnPlus.TabIndex = 7;
            btnPlus.Text = "+";
            btnPlus.Click += btnPlus_Click;
            // 
            // txtQuantity
            // 
            txtQuantity.BorderRadius = 8;
            txtQuantity.CustomizableEdges = customizableEdges7;
            txtQuantity.DefaultText = "1";
            txtQuantity.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtQuantity.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtQuantity.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtQuantity.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtQuantity.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txtQuantity.Font = new Font("Segoe UI", 14F);
            txtQuantity.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txtQuantity.Location = new Point(45, 0);
            txtQuantity.Margin = new Padding(5, 6, 5, 6);
            txtQuantity.Name = "txtQuantity";
            txtQuantity.PlaceholderText = "";
            txtQuantity.SelectedText = "";
            txtQuantity.ShadowDecoration.CustomizableEdges = customizableEdges8;
            txtQuantity.Size = new Size(60, 40);
            txtQuantity.TabIndex = 0;
            txtQuantity.TextAlign = HorizontalAlignment.Center;
            // 
            // btnMinus
            // 
            btnMinus.BorderRadius = 8;
            btnMinus.Cursor = Cursors.Hand;
            btnMinus.CustomizableEdges = customizableEdges9;
            btnMinus.DisabledState.BorderColor = Color.DarkGray;
            btnMinus.DisabledState.CustomBorderColor = Color.DarkGray;
            btnMinus.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnMinus.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnMinus.FillColor = Color.DarkGray;
            btnMinus.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 163);
            btnMinus.ForeColor = Color.Black;
            btnMinus.Location = new Point(0, 0);
            btnMinus.Name = "btnMinus";
            btnMinus.ShadowDecoration.CustomizableEdges = customizableEdges10;
            btnMinus.Size = new Size(40, 40);
            btnMinus.TabIndex = 7;
            btnMinus.Text = "-";
            btnMinus.Click += btnMinus_Click;
            // 
            // lblQuantityLabel
            // 
            lblQuantityLabel.AutoSize = true;
            lblQuantityLabel.Font = new Font("Segoe UI", 14F);
            lblQuantityLabel.Location = new Point(30, 260);
            lblQuantityLabel.Name = "lblQuantityLabel";
            lblQuantityLabel.Size = new Size(115, 32);
            lblQuantityLabel.TabIndex = 5;
            lblQuantityLabel.Text = "Số lượng:";
            // 
            // rdoSizeL
            // 
            rdoSizeL.AutoSize = true;
            rdoSizeL.Cursor = Cursors.Hand;
            rdoSizeL.Font = new Font("Segoe UI", 12F);
            rdoSizeL.Location = new Point(165, 210);
            rdoSizeL.Name = "rdoSizeL";
            rdoSizeL.Size = new Size(42, 32);
            rdoSizeL.TabIndex = 4;
            rdoSizeL.Text = "L";
            rdoSizeL.UseVisualStyleBackColor = true;
            rdoSizeL.CheckedChanged += rdoSizeL_CheckedChanged;
            // 
            // rdoSizeM
            // 
            rdoSizeM.AutoSize = true;
            rdoSizeM.Checked = true;
            rdoSizeM.Cursor = Cursors.Hand;
            rdoSizeM.Font = new Font("Segoe UI", 12F);
            rdoSizeM.Location = new Point(30, 210);
            rdoSizeM.Name = "rdoSizeM";
            rdoSizeM.Size = new Size(51, 32);
            rdoSizeM.TabIndex = 3;
            rdoSizeM.TabStop = true;
            rdoSizeM.Text = "M";
            rdoSizeM.UseVisualStyleBackColor = true;
            rdoSizeM.CheckedChanged += rdoSizeM_CheckedChanged;
            // 
            // lblSizeLabel
            // 
            lblSizeLabel.AutoSize = true;
            lblSizeLabel.Font = new Font("Segoe UI", 14F);
            lblSizeLabel.Location = new Point(30, 170);
            lblSizeLabel.Name = "lblSizeLabel";
            lblSizeLabel.Size = new Size(132, 32);
            lblSizeLabel.TabIndex = 2;
            lblSizeLabel.Text = "Kích thước:";
            // 
            // lblPrice
            // 
            lblPrice.AutoSize = true;
            lblPrice.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 163);
            lblPrice.ForeColor = Color.Red;
            lblPrice.Location = new Point(30, 100);
            lblPrice.Name = "lblPrice";
            lblPrice.Size = new Size(192, 41);
            lblPrice.TabIndex = 1;
            lblPrice.Text = "Giá: 40.000đ";
            // 
            // lblProductName
            // 
            lblProductName.AutoSize = true;
            lblProductName.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 163);
            lblProductName.Location = new Point(23, 30);
            lblProductName.Name = "lblProductName";
            lblProductName.Size = new Size(369, 54);
            lblProductName.TabIndex = 0;
            lblProductName.Text = "Trà sữa khoai môn";
            // 
            // ChiTietSanPham
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(782, 453);
            Controls.Add(rightPanel);
            Controls.Add(leftPanel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "ChiTietSanPham";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Chi tiết sản phẩm - MOMOCHA";
            Load += ChiTietSanPham_Load;
            leftPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)productImage).EndInit();
            rightPanel.ResumeLayout(false);
            rightPanel.PerformLayout();
            pnlQuantity.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel leftPanel;
        private PictureBox productImage;
        private Panel rightPanel;
        private Label lblSizeLabel;
        private Label lblPrice;
        private Label lblProductName;
        private RadioButton rdoSizeL;
        private RadioButton rdoSizeM;
        private Label lblQuantityLabel;
        private Guna.UI2.WinForms.Guna2Button btnMinus;
        private Panel pnlQuantity;
        private Guna.UI2.WinForms.Guna2TextBox txtQuantity;
        private Guna.UI2.WinForms.Guna2Button btnAddToCart;
        private Guna.UI2.WinForms.Guna2Button btnPlus;
        private Guna.UI2.WinForms.Guna2Button btnBuyNow;
    }
}