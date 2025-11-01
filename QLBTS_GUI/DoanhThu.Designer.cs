namespace QLBTS_GUI
{
    partial class DoanhThu
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            chartThongKe = new System.Windows.Forms.DataVisualization.Charting.Chart();
            dtpNgayThangNam = new DateTimePicker();
            cbbTieuChi = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)chartThongKe).BeginInit();
            SuspendLayout();
            // 
            // chartThongKe
            // 
            chartArea1.Name = "ChartArea1";
            chartThongKe.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            chartThongKe.Legends.Add(legend1);
            chartThongKe.Location = new Point(12, 12);
            chartThongKe.Name = "chartThongKe";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            chartThongKe.Series.Add(series1);
            chartThongKe.Size = new Size(776, 375);
            chartThongKe.TabIndex = 0;
            chartThongKe.Text = "chart1";
            // 
            // dtpNgayThangNam
            // 
            dtpNgayThangNam.Location = new Point(12, 403);
            dtpNgayThangNam.Name = "dtpNgayThangNam";
            dtpNgayThangNam.Size = new Size(250, 27);
            dtpNgayThangNam.TabIndex = 1;
            dtpNgayThangNam.ValueChanged += dtpNgayThangNam_ValueChanged;
            // 
            // cbbTieuChi
            // 
            cbbTieuChi.FormattingEnabled = true;
            cbbTieuChi.Location = new Point(298, 403);
            cbbTieuChi.Name = "cbbTieuChi";
            cbbTieuChi.Size = new Size(151, 28);
            cbbTieuChi.TabIndex = 2;
            cbbTieuChi.SelectedIndexChanged += cbbTieuChi_SelectedIndexChanged;
            // 
            // DoanhThu
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(cbbTieuChi);
            Controls.Add(dtpNgayThangNam);
            Controls.Add(chartThongKe);
            Name = "DoanhThu";
            Text = "DoanhThu";
            Load += DoanhThu_Load;
            ((System.ComponentModel.ISupportInitialize)chartThongKe).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartThongKe;
        private DateTimePicker dtpNgayThangNam;
        private ComboBox cbbTieuChi;
    }
}