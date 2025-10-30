namespace QLBTS_GUI
{
    partial class OrderDetailForm
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
            productContainer = new FlowLayoutPanel();
            lblItemCount = new Label();
            lblTitle = new Label();
            rightPanel = new Panel();
            lblTotalValue = new Label();
            lblTotal = new Label();
            panelLine2 = new Panel();
            lblDiscountValue = new Label();
            lblDiscount = new Label();
            lblTempValue = new Label();
            lblTempTotal = new Label();
            panelLine1 = new Panel();
            lblSummaryTitle = new Label();
            leftPanel.SuspendLayout();
            rightPanel.SuspendLayout();
            SuspendLayout();
            // 
            // leftPanel
            // 
            leftPanel.Controls.Add(productContainer);
            leftPanel.Controls.Add(lblItemCount);
            leftPanel.Controls.Add(lblTitle);
            leftPanel.Dock = DockStyle.Left;
            leftPanel.Location = new Point(0, 0);
            leftPanel.Name = "leftPanel";
            leftPanel.Padding = new Padding(20);
            leftPanel.Size = new Size(550, 553);
            leftPanel.TabIndex = 0;
            // 
            // productContainer
            // 
            productContainer.AutoScroll = true;
            productContainer.Location = new Point(20, 130);
            productContainer.Name = "productContainer";
            productContainer.Size = new Size(510, 440);
            productContainer.TabIndex = 2;
            // 
            // lblItemCount
            // 
            lblItemCount.AutoSize = true;
            lblItemCount.Font = new Font("Segoe UI", 12F);
            lblItemCount.ForeColor = Color.Gray;
            lblItemCount.Location = new Point(20, 90);
            lblItemCount.Name = "lblItemCount";
            lblItemCount.Size = new Size(112, 28);
            lblItemCount.TabIndex = 1;
            lblItemCount.Text = "2 sản phẩm";
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 28.2F, FontStyle.Bold, GraphicsUnit.Point, 163);
            lblTitle.Location = new Point(20, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(409, 62);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Giỏ hàng của bạn";
            // 
            // rightPanel
            // 
            rightPanel.BackColor = Color.FromArgb(232, 244, 248);
            rightPanel.Controls.Add(lblTotalValue);
            rightPanel.Controls.Add(lblTotal);
            rightPanel.Controls.Add(panelLine2);
            rightPanel.Controls.Add(lblDiscountValue);
            rightPanel.Controls.Add(lblDiscount);
            rightPanel.Controls.Add(lblTempValue);
            rightPanel.Controls.Add(lblTempTotal);
            rightPanel.Controls.Add(panelLine1);
            rightPanel.Controls.Add(lblSummaryTitle);
            rightPanel.Dock = DockStyle.Fill;
            rightPanel.Location = new Point(550, 0);
            rightPanel.Name = "rightPanel";
            rightPanel.Padding = new Padding(30);
            rightPanel.Size = new Size(432, 553);
            rightPanel.TabIndex = 1;
            // 
            // lblTotalValue
            // 
            lblTotalValue.AutoSize = true;
            lblTotalValue.Font = new Font("Segoe UI", 19.8000011F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 163);
            lblTotalValue.Location = new Point(30, 385);
            lblTotalValue.Name = "lblTotalValue";
            lblTotalValue.Size = new Size(169, 46);
            lblTotalValue.TabIndex = 8;
            lblTotalValue.Text = "150.000đ";
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Font = new Font("Segoe UI", 22.2F, FontStyle.Bold, GraphicsUnit.Point, 163);
            lblTotal.Location = new Point(30, 330);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(208, 50);
            lblTotal.TabIndex = 7;
            lblTotal.Text = "Tổng cộng";
            // 
            // panelLine2
            // 
            panelLine2.BackColor = Color.LightGray;
            panelLine2.Location = new Point(30, 310);
            panelLine2.Name = "panelLine2";
            panelLine2.Size = new Size(290, 2);
            panelLine2.TabIndex = 6;
            // 
            // lblDiscountValue
            // 
            lblDiscountValue.AutoSize = true;
            lblDiscountValue.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 163);
            lblDiscountValue.Location = new Point(160, 266);
            lblDiscountValue.Name = "lblDiscountValue";
            lblDiscountValue.Size = new Size(61, 41);
            lblDiscountValue.TabIndex = 5;
            lblDiscountValue.Text = "0%";
            lblDiscountValue.Click += lblDiscountValue_Click;
            // 
            // lblDiscount
            // 
            lblDiscount.AutoSize = true;
            lblDiscount.Font = new Font("Segoe UI", 18F);
            lblDiscount.Location = new Point(30, 220);
            lblDiscount.Name = "lblDiscount";
            lblDiscount.Size = new Size(135, 41);
            lblDiscount.TabIndex = 4;
            lblDiscount.Text = "Giảm giá";
            // 
            // lblTempValue
            // 
            lblTempValue.AutoSize = true;
            lblTempValue.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 163);
            lblTempValue.Location = new Point(160, 179);
            lblTempValue.Name = "lblTempValue";
            lblTempValue.Size = new Size(54, 41);
            lblTempValue.TabIndex = 3;
            lblTempValue.Text = "0đ";
            lblTempValue.TextAlign = ContentAlignment.MiddleRight;
            lblTempValue.Click += lblTempValue_Click;
            // 
            // lblTempTotal
            // 
            lblTempTotal.AutoSize = true;
            lblTempTotal.Font = new Font("Segoe UI", 18F);
            lblTempTotal.Location = new Point(30, 130);
            lblTempTotal.Name = "lblTempTotal";
            lblTempTotal.Size = new Size(134, 41);
            lblTempTotal.TabIndex = 2;
            lblTempTotal.Text = "Tạm tính";
            // 
            // panelLine1
            // 
            panelLine1.BackColor = Color.LightGray;
            panelLine1.Location = new Point(30, 105);
            panelLine1.Name = "panelLine1";
            panelLine1.Size = new Size(290, 2);
            panelLine1.TabIndex = 1;
            // 
            // lblSummaryTitle
            // 
            lblSummaryTitle.AutoSize = true;
            lblSummaryTitle.Font = new Font("Segoe UI", 31.8000011F, FontStyle.Bold, GraphicsUnit.Point, 163);
            lblSummaryTitle.Location = new Point(30, 30);
            lblSummaryTitle.Name = "lblSummaryTitle";
            lblSummaryTitle.Size = new Size(256, 72);
            lblSummaryTitle.TabIndex = 0;
            lblSummaryTitle.Text = "Tổng kết";
            // 
            // OrderDetailForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(982, 553);
            Controls.Add(rightPanel);
            Controls.Add(leftPanel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "OrderDetailForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Chi tiết đơn hàng - MOMOCHA";
            leftPanel.ResumeLayout(false);
            leftPanel.PerformLayout();
            rightPanel.ResumeLayout(false);
            rightPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel leftPanel;
        private Label lblItemCount;
        private Label lblTitle;
        private FlowLayoutPanel productContainer;
        private Panel rightPanel;
        private Label lblTempTotal;
        private Panel panelLine1;
        private Label lblSummaryTitle;
        private Label lblTempValue;
        private Panel panelLine2;
        private Label lblDiscountValue;
        private Label lblDiscount;
        private Label lblTotalValue;
        private Label lblTotal;
    }
}