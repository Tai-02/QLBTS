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
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtLichSu = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(280, 30);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(209, 81);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Lịch sử";
            // 
            // txtLichSu
            // 
            this.txtLichSu.BackColor = System.Drawing.Color.White;
            this.txtLichSu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLichSu.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtLichSu.Location = new System.Drawing.Point(55, 140);
            this.txtLichSu.Multiline = true;
            this.txtLichSu.Name = "txtLichSu";
            this.txtLichSu.ReadOnly = true;
            this.txtLichSu.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLichSu.Size = new System.Drawing.Size(670, 360);
            this.txtLichSu.TabIndex = 1;
            // 
            // LichSuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(780, 540);
            this.Controls.Add(this.txtLichSu);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "LichSuForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lịch sử - MOMOCHA";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtLichSu;
    }
}