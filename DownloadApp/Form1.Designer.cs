namespace DownloadApp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            txtQuery = new TextBox();
            numLimit = new NumericUpDown();
            txtSaveDir = new TextBox();
            label1 = new Label();
            label3 = new Label();
            btnPickDir = new Button();
            btnSearch = new Button();
            btnDownload = new Button();
            flow = new FlowLayoutPanel();
            progressBar = new ProgressBar();
            lblStatus = new Label();
            btnCancel = new Button();
            exclude = new TextBox();
            label2 = new Label();
            advsearch = new Button();
            chkSelectAll = new CheckBox();
            label4 = new Label();
            label5 = new Label();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)numLimit).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // txtQuery
            // 
            txtQuery.Location = new Point(118, 88);
            txtQuery.Name = "txtQuery";
            txtQuery.Size = new Size(299, 31);
            txtQuery.TabIndex = 1;
            // 
            // numLimit
            // 
            numLimit.Location = new Point(584, 89);
            numLimit.Maximum = new decimal(new int[] { 80, 0, 0, 0 });
            numLimit.Name = "numLimit";
            numLimit.Size = new Size(62, 31);
            numLimit.TabIndex = 2;
            numLimit.Value = new decimal(new int[] { 30, 0, 0, 0 });
            // 
            // txtSaveDir
            // 
            txtSaveDir.Location = new Point(119, 126);
            txtSaveDir.Name = "txtSaveDir";
            txtSaveDir.Size = new Size(819, 31);
            txtSaveDir.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(24, 88);
            label1.Name = "label1";
            label1.Size = new Size(88, 25);
            label1.TabIndex = 4;
            label1.Text = "Tìm kiếm:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(32, 128);
            label3.Name = "label3";
            label3.Size = new Size(79, 25);
            label3.TabIndex = 6;
            label3.Text = "Lưu vào:";
            // 
            // btnPickDir
            // 
            btnPickDir.Location = new Point(944, 124);
            btnPickDir.Name = "btnPickDir";
            btnPickDir.Size = new Size(184, 34);
            btnPickDir.TabIndex = 7;
            btnPickDir.Text = "Chọn thư mục";
            btnPickDir.UseVisualStyleBackColor = true;
            btnPickDir.Click += btnPickDir_Click;
            // 
            // btnSearch
            // 
            btnSearch.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSearch.Location = new Point(1163, 86);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(336, 71);
            btnSearch.TabIndex = 8;
            btnSearch.Text = "Tìm ảnh";
            btnSearch.UseVisualStyleBackColor = true;
            // 
            // btnDownload
            // 
            btnDownload.Location = new Point(201, 165);
            btnDownload.Name = "btnDownload";
            btnDownload.Size = new Size(184, 34);
            btnDownload.TabIndex = 9;
            btnDownload.Text = "Tải ảnh đã chọn";
            btnDownload.UseVisualStyleBackColor = true;
            btnDownload.Click += btnDownload_Click;
            // 
            // flow
            // 
            flow.AutoScroll = true;
            flow.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flow.BackColor = Color.Silver;
            flow.Location = new Point(12, 255);
            flow.Name = "flow";
            flow.Size = new Size(1795, 1030);
            flow.TabIndex = 10;
            // 
            // progressBar
            // 
            progressBar.Location = new Point(581, 165);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(1226, 34);
            progressBar.TabIndex = 11;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(581, 213);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(28, 25);
            lblStatus.TabIndex = 12;
            lblStatus.Text = "....";
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(391, 166);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(184, 34);
            btnCancel.TabIndex = 13;
            btnCancel.Text = "Hủy";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // exclude
            // 
            exclude.Location = new Point(829, 88);
            exclude.Name = "exclude";
            exclude.Size = new Size(299, 31);
            exclude.TabIndex = 14;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(676, 88);
            label2.Name = "label2";
            label2.Size = new Size(147, 25);
            label2.TabIndex = 15;
            label2.Text = "Loại trừ từ khóa: ";
            // 
            // advsearch
            // 
            advsearch.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            advsearch.Location = new Point(1511, 88);
            advsearch.Name = "advsearch";
            advsearch.Size = new Size(296, 69);
            advsearch.TabIndex = 16;
            advsearch.Text = "Tìm ảnh nâng cao";
            advsearch.UseVisualStyleBackColor = true;
            // 
            // chkSelectAll
            // 
            chkSelectAll.AutoSize = true;
            chkSelectAll.Location = new Point(24, 170);
            chkSelectAll.Name = "chkSelectAll";
            chkSelectAll.Size = new Size(162, 29);
            chkSelectAll.TabIndex = 17;
            chkSelectAll.Text = "Chọn tất cả ảnh";
            chkSelectAll.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(440, 94);
            label4.Name = "label4";
            label4.Size = new Size(123, 25);
            label4.TabIndex = 18;
            label4.Text = "Số lượng ảnh:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.Red;
            label5.Location = new Point(248, 9);
            label5.Name = "label5";
            label5.Size = new Size(1559, 65);
            label5.TabIndex = 19;
            label5.Text = "TẢI ẢNH TỪ PEXELS THÔNG QUA API - MADE BY: BÙI QUANG THÁI";
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(23, 1);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(88, 84);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 20;
            pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1816, 1297);
            Controls.Add(pictureBox1);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(chkSelectAll);
            Controls.Add(advsearch);
            Controls.Add(label2);
            Controls.Add(exclude);
            Controls.Add(btnCancel);
            Controls.Add(lblStatus);
            Controls.Add(progressBar);
            Controls.Add(flow);
            Controls.Add(btnDownload);
            Controls.Add(btnSearch);
            Controls.Add(btnPickDir);
            Controls.Add(label3);
            Controls.Add(label1);
            Controls.Add(txtSaveDir);
            Controls.Add(numLimit);
            Controls.Add(txtQuery);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Tải ảnh từ Pexels";
            ((System.ComponentModel.ISupportInitialize)numLimit).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox txtQuery;
        private NumericUpDown numLimit;
        private TextBox txtSaveDir;
        private Label label1;
        private Label label3;
        private Button btnPickDir;
        private Button btnSearch;
        private Button btnDownload;
        private FlowLayoutPanel flow;
        private ProgressBar progressBar;
        private Label lblStatus;
        private Button btnCancel;
        private TextBox exclude;
        private Label label2;
        private Button advsearch;
        private CheckBox chkSelectAll;
        private Label label4;
        private Label label5;
        private PictureBox pictureBox1;
    }
}
