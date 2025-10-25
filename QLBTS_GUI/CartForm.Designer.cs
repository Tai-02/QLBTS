namespace QLBTS_GUI
{
    partial class CartForm
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
            leftPanel = new Panel();
            lblTitle = new Label();
            productContainer = new FlowLayoutPanel();
            lblItemCount = new Label();
            rightPanel = new Panel();
            btnContinue = new Button();
            btnCheckout = new Button();
            lblTotalValue = new Label();
            lblTotal = new Label();
            panelLine2 = new Panel();
            lblDiscountValue = new Label();
            panelLine1 = new Panel();
            lblDiscount = new Label();
            lblTempValue = new Label();
            lblTempTotal = new Label();
            lblSummaryTitle = new Label();
            leftPanel.SuspendLayout();
            rightPanel.SuspendLayout();
            SuspendLayout();
            // 
            // leftPanel
            // 
            leftPanel.Controls.Add(lblTitle);
            leftPanel.Controls.Add(productContainer);
            leftPanel.Controls.Add(lblItemCount);
            leftPanel.Location = new Point(0, 0);
            leftPanel.Name = "leftPanel";
            leftPanel.Size = new Size(620, 600);
            leftPanel.TabIndex = 1;
            // 
            // lblTitle
            // 
            lblTitle.AllowDrop = true;
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 28F);
            lblTitle.Location = new Point(20, 9);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(395, 62);
            lblTitle.TabIndex = 2;
            lblTitle.Text = "Giỏ hàng của bạn";
            // 
            // productContainer
            // 
            productContainer.AutoScroll = true;
            productContainer.Location = new Point(20, 100);
            productContainer.Name = "productContainer";
            productContainer.Size = new Size(580, 450);
            productContainer.TabIndex = 1;
            // 
            // lblItemCount
            // 
            lblItemCount.AutoSize = true;
            lblItemCount.Font = new Font("Segoe UI", 12F);
            lblItemCount.ForeColor = Color.Gray;
            lblItemCount.Location = new Point(30, 69);
            lblItemCount.Name = "lblItemCount";
            lblItemCount.Size = new Size(112, 28);
            lblItemCount.TabIndex = 0;
            lblItemCount.Text = "0 sản phẩm";
            lblItemCount.Click += lblItemCount_Click;
            // 
            // rightPanel
            // 
            rightPanel.BackColor = Color.FromArgb(232, 244, 248);
            rightPanel.Controls.Add(btnContinue);
            rightPanel.Controls.Add(btnCheckout);
            rightPanel.Controls.Add(lblTotalValue);
            rightPanel.Controls.Add(lblTotal);
            rightPanel.Controls.Add(panelLine2);
            rightPanel.Controls.Add(lblDiscountValue);
            rightPanel.Controls.Add(panelLine1);
            rightPanel.Controls.Add(lblDiscount);
            rightPanel.Controls.Add(lblTempValue);
            rightPanel.Controls.Add(lblTempTotal);
            rightPanel.Controls.Add(lblSummaryTitle);
            rightPanel.Location = new Point(622, 0);
            rightPanel.Name = "rightPanel";
            rightPanel.Size = new Size(360, 600);
            rightPanel.TabIndex = 3;
            // 
            // btnContinue
            // 
            btnContinue.Font = new Font("Segoe UI", 13F);
            btnContinue.Location = new Point(30, 475);
            btnContinue.Name = "btnContinue";
            btnContinue.Size = new Size(320, 60);
            btnContinue.TabIndex = 10;
            btnContinue.Text = "Tiếp tục mua hàng";
            btnContinue.UseVisualStyleBackColor = true;
            btnContinue.Click += btnContinue_Click;
            // 
            // btnCheckout
            // 
            btnCheckout.BackColor = Color.FromArgb(33, 150, 243);
            btnCheckout.Cursor = Cursors.Hand;
            btnCheckout.FlatAppearance.BorderSize = 0;
            btnCheckout.FlatStyle = FlatStyle.Flat;
            btnCheckout.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 163);
            btnCheckout.ForeColor = Color.White;
            btnCheckout.Location = new Point(30, 400);
            btnCheckout.Name = "btnCheckout";
            btnCheckout.Size = new Size(320, 60);
            btnCheckout.TabIndex = 9;
            btnCheckout.Text = "Thanh toán";
            btnCheckout.UseVisualStyleBackColor = false;
            btnCheckout.Click += btnCheckout_Click;
            // 
            // lblTotalValue
            // 
            lblTotalValue.AutoSize = true;
            lblTotalValue.Font = new Font("Segoe UI", 19.8000011F, FontStyle.Italic, GraphicsUnit.Point, 163);
            lblTotalValue.Location = new Point(198, 312);
            lblTotalValue.Name = "lblTotalValue";
            lblTotalValue.Size = new Size(153, 46);
            lblTotalValue.TabIndex = 8;
            lblTotalValue.Text = "150.000đ";
            lblTotalValue.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Font = new Font("Segoe UI", 22.2F, FontStyle.Bold, GraphicsUnit.Point, 163);
            lblTotal.Location = new Point(30, 260);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(208, 50);
            lblTotal.TabIndex = 7;
            lblTotal.Text = "Tổng cộng";
            // 
            // panelLine2
            // 
            panelLine2.BackColor = Color.Gray;
            panelLine2.Location = new Point(30, 230);
            panelLine2.Name = "panelLine2";
            panelLine2.Size = new Size(320, 3);
            panelLine2.TabIndex = 6;
            // 
            // lblDiscountValue
            // 
            lblDiscountValue.AutoSize = true;
            lblDiscountValue.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 163);
            lblDiscountValue.Location = new Point(220, 175);
            lblDiscountValue.Name = "lblDiscountValue";
            lblDiscountValue.Size = new Size(61, 41);
            lblDiscountValue.TabIndex = 5;
            lblDiscountValue.Text = "0%";
            lblDiscountValue.TextAlign = ContentAlignment.MiddleRight;
            // 
            // panelLine1
            // 
            panelLine1.BackColor = Color.LightGray;
            panelLine1.Location = new Point(30, 100);
            panelLine1.Name = "panelLine1";
            panelLine1.Size = new Size(320, 2);
            panelLine1.TabIndex = 4;
            // 
            // lblDiscount
            // 
            lblDiscount.AutoSize = true;
            lblDiscount.Font = new Font("Segoe UI", 18F);
            lblDiscount.ForeColor = Color.Black;
            lblDiscount.Location = new Point(30, 175);
            lblDiscount.Name = "lblDiscount";
            lblDiscount.Size = new Size(135, 41);
            lblDiscount.TabIndex = 3;
            lblDiscount.Text = "Giảm giá";
            // 
            // lblTempValue
            // 
            lblTempValue.AutoSize = true;
            lblTempValue.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 163);
            lblTempValue.ForeColor = Color.Black;
            lblTempValue.Location = new Point(220, 130);
            lblTempValue.Name = "lblTempValue";
            lblTempValue.Size = new Size(54, 41);
            lblTempValue.TabIndex = 2;
            lblTempValue.Text = "0đ";
            lblTempValue.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblTempTotal
            // 
            lblTempTotal.AutoSize = true;
            lblTempTotal.Font = new Font("Segoe UI", 18F);
            lblTempTotal.ForeColor = Color.Black;
            lblTempTotal.Location = new Point(30, 130);
            lblTempTotal.Name = "lblTempTotal";
            lblTempTotal.Size = new Size(134, 41);
            lblTempTotal.TabIndex = 1;
            lblTempTotal.Text = "Tạm tính";
            // 
            // lblSummaryTitle
            // 
            lblSummaryTitle.AutoSize = true;
            lblSummaryTitle.Font = new Font("Segoe UI", 32F, FontStyle.Bold);
            lblSummaryTitle.ForeColor = Color.Black;
            lblSummaryTitle.Location = new Point(30, 25);
            lblSummaryTitle.Name = "lblSummaryTitle";
            lblSummaryTitle.Size = new Size(256, 72);
            lblSummaryTitle.TabIndex = 0;
            lblSummaryTitle.Text = "Tổng kết";
            lblSummaryTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // CartForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(982, 553);
            Controls.Add(rightPanel);
            Controls.Add(leftPanel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "CartForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "CartForm";
            leftPanel.ResumeLayout(false);
            leftPanel.PerformLayout();
            rightPanel.ResumeLayout(false);
            rightPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel leftPanel;
        private FlowLayoutPanel productContainer;
        private Label lblItemCount;
        private Panel rightPanel;
        private Button btnContinue;
        private Button btnCheckout;
        private Label lblTotalValue;
        private Label lblTotal;
        private Panel panelLine2;
        private Label lblDiscountValue;
        private Panel panelLine1;
        private Label lblDiscount;
        private Label lblTempValue;
        private Label lblTempTotal;
        private Label lblSummaryTitle;
        private Label lblTitle;
    }
}