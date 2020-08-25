namespace Igtampe.LBL.Server.Forms {
    partial class SettingsForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.VerLabel = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.LBLDirectorySelector = new System.Windows.Forms.FolderBrowserDialog();
            this.DownloadPLevelSelector = new System.Windows.Forms.NumericUpDown();
            this.UploadPLevelSelector = new System.Windows.Forms.NumericUpDown();
            this.LBLDirectoryBox = new System.Windows.Forms.TextBox();
            this.BrowseBTN = new System.Windows.Forms.Button();
            this.OKBTN = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DownloadPLevelSelector)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UploadPLevelSelector)).BeginInit();
            this.SuspendLayout();
            // 
            // VerLabel
            // 
            this.VerLabel.Location = new System.Drawing.Point(12, 127);
            this.VerLabel.Name = "VerLabel";
            this.VerLabel.Size = new System.Drawing.Size(255, 23);
            this.VerLabel.TabIndex = 0;
            this.VerLabel.Text = "Line By Line Protocol Extension Plus Version 1.0";
            this.VerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Igtampe.LBL.Server.Properties.Resources.LBL_Standalone;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(255, 112);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 166);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Download PLevel";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 192);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Upload PLevel";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 220);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "LBL Directory:";
            // 
            // DownloadPLevelSelector
            // 
            this.DownloadPLevelSelector.Location = new System.Drawing.Point(147, 164);
            this.DownloadPLevelSelector.Name = "DownloadPLevelSelector";
            this.DownloadPLevelSelector.Size = new System.Drawing.Size(120, 20);
            this.DownloadPLevelSelector.TabIndex = 3;
            // 
            // UploadPLevelSelector
            // 
            this.UploadPLevelSelector.Location = new System.Drawing.Point(147, 190);
            this.UploadPLevelSelector.Name = "UploadPLevelSelector";
            this.UploadPLevelSelector.Size = new System.Drawing.Size(120, 20);
            this.UploadPLevelSelector.TabIndex = 4;
            // 
            // LBLDirectoryBox
            // 
            this.LBLDirectoryBox.Location = new System.Drawing.Point(92, 217);
            this.LBLDirectoryBox.Name = "LBLDirectoryBox";
            this.LBLDirectoryBox.ReadOnly = true;
            this.LBLDirectoryBox.Size = new System.Drawing.Size(175, 20);
            this.LBLDirectoryBox.TabIndex = 5;
            // 
            // BrowseBTN
            // 
            this.BrowseBTN.Location = new System.Drawing.Point(12, 252);
            this.BrowseBTN.Name = "BrowseBTN";
            this.BrowseBTN.Size = new System.Drawing.Size(93, 23);
            this.BrowseBTN.TabIndex = 6;
            this.BrowseBTN.Text = "Browse";
            this.BrowseBTN.UseVisualStyleBackColor = true;
            this.BrowseBTN.Click += new System.EventHandler(this.BrowseBTN_Click);
            // 
            // OKBTN
            // 
            this.OKBTN.Location = new System.Drawing.Point(192, 252);
            this.OKBTN.Name = "OKBTN";
            this.OKBTN.Size = new System.Drawing.Size(75, 23);
            this.OKBTN.TabIndex = 8;
            this.OKBTN.Text = "OK";
            this.OKBTN.UseVisualStyleBackColor = true;
            this.OKBTN.Click += new System.EventHandler(this.OKBtn_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(279, 287);
            this.Controls.Add(this.OKBTN);
            this.Controls.Add(this.BrowseBTN);
            this.Controls.Add(this.LBLDirectoryBox);
            this.Controls.Add(this.UploadPLevelSelector);
            this.Controls.Add(this.DownloadPLevelSelector);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.VerLabel);
            this.Name = "SettingsForm";
            this.Text = "SettingForm";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DownloadPLevelSelector)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UploadPLevelSelector)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label VerLabel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.FolderBrowserDialog LBLDirectorySelector;
        private System.Windows.Forms.NumericUpDown DownloadPLevelSelector;
        private System.Windows.Forms.NumericUpDown UploadPLevelSelector;
        private System.Windows.Forms.TextBox LBLDirectoryBox;
        private System.Windows.Forms.Button BrowseBTN;
        private System.Windows.Forms.Button OKBTN;
    }
}