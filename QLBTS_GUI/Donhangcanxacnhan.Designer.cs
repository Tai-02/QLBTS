namespace QLBTS_GUI
{
    partial class Donhangcanxacnhan
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            Namedau = new Label();
            dataGridView1 = new DataGridView();
            MaDonhang = new DataGridViewTextBoxColumn();
            Gia = new DataGridViewTextBoxColumn();
            btnNhan = new DataGridViewButtonColumn();
            btnChiTiet = new DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // Namedau
            // 
            Namedau.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Namedau.AutoSize = true;
            Namedau.Font = new Font("Times New Roman", 27.75F, FontStyle.Bold, GraphicsUnit.Point, 163);
            Namedau.ForeColor = SystemColors.ControlText;
            Namedau.Location = new Point(290, 137);
            Namedau.Name = "Namedau";
            Namedau.Size = new Size(393, 42);
            Namedau.TabIndex = 0;
            Namedau.Text = "Đơn hàng cần xác nhận";
            Namedau.Click += label1_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.BackgroundColor = SystemColors.ButtonFace;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.ActiveCaptionText;
            dataGridViewCellStyle1.Font = new Font("Times New Roman", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 163);
            dataGridViewCellStyle1.ForeColor = SystemColors.Window;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { MaDonhang, Gia, btnNhan, btnChiTiet });
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = SystemColors.Window;
            dataGridViewCellStyle4.Font = new Font("Times New Roman", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 163);
            dataGridViewCellStyle4.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.False;
            dataGridView1.DefaultCellStyle = dataGridViewCellStyle4;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.GridColor = Color.Cyan;
            dataGridView1.Location = new Point(89, 194);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(844, 329);
            dataGridView1.TabIndex = 1;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // MaDonhang
            // 
            MaDonhang.HeaderText = "Mã đơn hàng";
            MaDonhang.Name = "MaDonhang";
            MaDonhang.ReadOnly = true;
            // 
            // Gia
            // 
            Gia.HeaderText = "Giá";
            Gia.Name = "Gia";
            Gia.ReadOnly = true;
            // 
            // btnNhan
            // 
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.NullValue = "Nhận";
            btnNhan.DefaultCellStyle = dataGridViewCellStyle2;
            btnNhan.HeaderText = "Nhận Đơn";
            btnNhan.Name = "btnNhan";
            btnNhan.ReadOnly = true;
            btnNhan.UseColumnTextForButtonValue = true;
            // 
            // btnChiTiet
            // 
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.NullValue = "Chi Tiết";
            btnChiTiet.DefaultCellStyle = dataGridViewCellStyle3;
            btnChiTiet.HeaderText = "Chi Tiết";
            btnChiTiet.Name = "btnChiTiet";
            btnChiTiet.ReadOnly = true;
            btnChiTiet.UseColumnTextForButtonValue = true;
            // 
            // Nhanviengiaohang
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(984, 561);
            Controls.Add(dataGridView1);
            Controls.Add(Namedau);
            Name = "Nhanviengiaohang";
            Text = "Nhanviengiaohang";
            Load += Nhanviengiaohang_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label Namedau;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn MaDonhang;
        private DataGridViewTextBoxColumn Gia;
        private DataGridViewButtonColumn btnNhan;
        private DataGridViewButtonColumn btnChiTiet;
    }
}