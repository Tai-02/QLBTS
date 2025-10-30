namespace QLBTS_GUI
{
    partial class LichSu
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblTitle = new Label();
            txtLichSu = new TextBox();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 36F, FontStyle.Bold);
            lblTitle.Location = new Point(280, 30);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(231, 81);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Lịch sử";
            // 
            // txtLichSu
            // 
            txtLichSu.BackColor = Color.White;
            txtLichSu.BorderStyle = BorderStyle.FixedSingle;
            txtLichSu.Font = new Font("Segoe UI", 11F);
            txtLichSu.Location = new Point(12, 141);
            txtLichSu.Multiline = true;
            txtLichSu.Name = "txtLichSu";
            txtLichSu.ReadOnly = true;
            txtLichSu.ScrollBars = ScrollBars.Vertical;
            txtLichSu.Size = new Size(924, 360);
            txtLichSu.TabIndex = 1;
            // 
            // LichSu
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(948, 513);
            Controls.Add(txtLichSu);
            Controls.Add(lblTitle);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "LichSu";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Lịch sử - MOMOCHA";
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtLichSu;
    }
}