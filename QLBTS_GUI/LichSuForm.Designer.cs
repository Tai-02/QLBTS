namespace QLBTS_GUI
{
    partial class LichSuForm
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
            lblTitle.Location = new Point(355, 29);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(231, 81);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Lịch sử";
            lblTitle.Click += lblTitle_Click;
            // 
            // txtLichSu
            // 
            txtLichSu.BackColor = Color.White;
            txtLichSu.BorderStyle = BorderStyle.FixedSingle;
            txtLichSu.Font = new Font("Segoe UI", 11F);
            txtLichSu.Location = new Point(55, 140);
            txtLichSu.Multiline = true;
            txtLichSu.Name = "txtLichSu";
            txtLichSu.ReadOnly = true;
            txtLichSu.ScrollBars = ScrollBars.Vertical;
            txtLichSu.Size = new Size(852, 360);
            txtLichSu.TabIndex = 1;
            // 
            // LichSuForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(982, 553);
            Controls.Add(txtLichSu);
            Controls.Add(lblTitle);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "LichSuForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Lịch sử - MOMOCHA";
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtLichSu;
    }
}