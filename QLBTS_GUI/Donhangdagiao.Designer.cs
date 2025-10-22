namespace QLBTS_GUI
{
    partial class Donhangdagiao
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
            Namedau = new Label();
            dataGridView1 = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // Namedau
            // 
            Namedau.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Namedau.AutoSize = true;
            Namedau.Font = new Font("Times New Roman", 27.75F, FontStyle.Bold, GraphicsUnit.Point, 163);
            Namedau.ForeColor = SystemColors.ControlText;
            Namedau.Location = new Point(234, 100);
            Namedau.Name = "Namedau";
            Namedau.Size = new Size(299, 42);
            Namedau.TabIndex = 1;
            Namedau.Text = "Đơn hàng đã giao";
            // 
            // dataGridView1
            // 
            dataGridView1.BackgroundColor = SystemColors.GradientActiveCaption;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(49, 145);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(703, 280);
            dataGridView1.TabIndex = 2;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // Donhangdagiao
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dataGridView1);
            Controls.Add(Namedau);
            Name = "Donhangdagiao";
            Text = "Donhangdagiao";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label Namedau;
        private DataGridView dataGridView1;
    }
}