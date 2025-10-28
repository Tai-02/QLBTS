using System.Drawing;
using System.Windows.Forms;

namespace QLBTS_GUI
{
    partial class QLDonHangForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitle;
        private DataGridView dgvOrders;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            dgvOrders = new DataGridView();
            lblTitle = new Label();

            ((System.ComponentModel.ISupportInitialize)dgvOrders).BeginInit();
            SuspendLayout();

            // ====== lblTitle ======
            lblTitle.Font = new Font("Cambria", 20F, FontStyle.Bold, GraphicsUnit.Point);
            lblTitle.ForeColor = Color.Black;
            lblTitle.Location = new Point(0, 30);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(900, 50);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Quản lý đơn hàng";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;

            // ====== dgvOrders ======
            dgvOrders.AllowUserToAddRows = false;
            dgvOrders.AllowUserToDeleteRows = false;
            dgvOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvOrders.BackgroundColor = Color.White;
            dgvOrders.BorderStyle = BorderStyle.None;
            dgvOrders.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

            dgvOrders.ColumnHeadersDefaultCellStyle.BackColor = Color.Black;
            dgvOrders.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvOrders.ColumnHeadersDefaultCellStyle.Font = new Font("Times New Roman", 12F, FontStyle.Bold);
            dgvOrders.ColumnHeadersHeight = 40;
            dgvOrders.EnableHeadersVisualStyles = false;

            dgvOrders.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            dgvOrders.DefaultCellStyle.SelectionBackColor = Color.White;
            dgvOrders.DefaultCellStyle.SelectionForeColor = Color.Black;

            dgvOrders.GridColor = Color.Gray;
            dgvOrders.Location = new Point(60, 100);
            dgvOrders.Name = "dgvOrders";
            dgvOrders.RowHeadersVisible = false;
            dgvOrders.RowTemplate.Height = 45;
            dgvOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvOrders.Size = new Size(780, 400);
            dgvOrders.TabIndex = 1;
            dgvOrders.CellContentClick += dgvOrders_CellContentClick;

            // ====== QLDonHangForm ======
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(900, 550);
            Controls.Add(dgvOrders);
            Controls.Add(lblTitle);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            Name = "QLDonHangForm";
            Text = "Quản lý đơn hàng";
            Load += QLDonHangForm_Load;

            ((System.ComponentModel.ISupportInitialize)dgvOrders).EndInit();
            ResumeLayout(false);
        }
    }
}
