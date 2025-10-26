namespace QLBTS_GUI
{
    partial class Donhangdanhan
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
            SuspendLayout();
            // 
            // Donhangdanhan
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(875, 562);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "Donhangdanhan";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Đơn hàng cần xác nhận";
            Load += Donhangdanhan_Load;
            ResumeLayout(false);
        }
    }
}