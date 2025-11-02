namespace QLBTS_GUI
{
    partial class Khung
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
            components = new System.ComponentModel.Container();
            Khung_pn = new Panel();
            timer1 = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // Khung_pn
            // 
            Khung_pn.Location = new Point(3, 2);
            Khung_pn.Name = "Khung_pn";
            Khung_pn.Size = new Size(958, 659);
            Khung_pn.TabIndex = 0;
            // 
            // timer1
            // 
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            // 
            // Khung
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(964, 664);
            Controls.Add(Khung_pn);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Khung";
            Text = "Khung";
            Load += Khung_Load;
            ResumeLayout(false);
        }

        #endregion

        public static Panel Khung_pn;
        private System.Windows.Forms.Timer timer1;
    }
}