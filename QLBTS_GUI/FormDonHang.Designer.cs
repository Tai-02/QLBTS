namespace QLBTS_GUI
{
    partial class QLDonHangForm
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

        private void InitializeComponent()
        {
            dgvOrders = new DataGridView();
            lblTitle = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvOrders).BeginInit();
            SuspendLayout();
            // 
            // dgvOrders
            // 
            dgvOrders.AllowUserToAddRows = false;
            dgvOrders.AllowUserToDeleteRows = false;
            dgvOrders.BackgroundColor = Color.White;
            dgvOrders.BorderStyle = BorderStyle.None;
            dgvOrders.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvOrders.Location = new Point(71, 100);
            dgvOrders.Name = "dgvOrders";
            dgvOrders.RowHeadersVisible = false;
            dgvOrders.RowHeadersWidth = 51;
            dgvOrders.RowTemplate.Height = 40;
            dgvOrders.Size = new Size(756, 411);
            dgvOrders.TabIndex = 0;
            dgvOrders.CellFormatting += dgvOrders_CellFormatting;
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Cambria", 20F, FontStyle.Bold);
            lblTitle.Location = new Point(0, 30);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(889, 40);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "Quản lý đơn hàng";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // QLDonHangForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(882, 553);
            Controls.Add(lblTitle);
            Controls.Add(dgvOrders);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "QLDonHangForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản lý đơn hàng";
            Load += QLDonHangForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvOrders).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvOrders;
        private System.Windows.Forms.Label lblTitle;
    }
}
