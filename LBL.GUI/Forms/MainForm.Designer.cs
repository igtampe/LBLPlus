namespace Igtampe.LBL.GUI.Forms {
    partial class MainForm {
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
            this.FolderBox = new System.Windows.Forms.GroupBox();
            this.RootBTN = new System.Windows.Forms.Button();
            this.UpLevelBTN = new System.Windows.Forms.Button();
            this.FolderListBox = new System.Windows.Forms.ListBox();
            this.FileBox = new System.Windows.Forms.GroupBox();
            this.UploadBTN = new System.Windows.Forms.Button();
            this.FilesListBox = new System.Windows.Forms.ListBox();
            this.DownloadBTN = new System.Windows.Forms.Button();
            this.DisconnectBTN = new System.Windows.Forms.Button();
            this.FolderBox.SuspendLayout();
            this.FileBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // FolderBox
            // 
            this.FolderBox.Controls.Add(this.RootBTN);
            this.FolderBox.Controls.Add(this.UpLevelBTN);
            this.FolderBox.Controls.Add(this.FolderListBox);
            this.FolderBox.Location = new System.Drawing.Point(12, 12);
            this.FolderBox.Name = "FolderBox";
            this.FolderBox.Size = new System.Drawing.Size(175, 279);
            this.FolderBox.TabIndex = 0;
            this.FolderBox.TabStop = false;
            this.FolderBox.Text = "Folders";
            // 
            // RootBTN
            // 
            this.RootBTN.Location = new System.Drawing.Point(87, 250);
            this.RootBTN.Name = "RootBTN";
            this.RootBTN.Size = new System.Drawing.Size(40, 23);
            this.RootBTN.TabIndex = 5;
            this.RootBTN.Text = "Root";
            this.RootBTN.UseVisualStyleBackColor = true;
            this.RootBTN.Click += new System.EventHandler(this.RootBTN_Click);
            // 
            // UpLevelBTN
            // 
            this.UpLevelBTN.Location = new System.Drawing.Point(133, 250);
            this.UpLevelBTN.Name = "UpLevelBTN";
            this.UpLevelBTN.Size = new System.Drawing.Size(36, 23);
            this.UpLevelBTN.TabIndex = 4;
            this.UpLevelBTN.Text = "Up";
            this.UpLevelBTN.UseVisualStyleBackColor = true;
            this.UpLevelBTN.Click += new System.EventHandler(this.UpLevelBTN_Click);
            // 
            // FolderListBox
            // 
            this.FolderListBox.FormattingEnabled = true;
            this.FolderListBox.Location = new System.Drawing.Point(6, 19);
            this.FolderListBox.Name = "FolderListBox";
            this.FolderListBox.Size = new System.Drawing.Size(163, 225);
            this.FolderListBox.TabIndex = 1;
            this.FolderListBox.SelectedIndexChanged += new System.EventHandler(this.FolderListbox_SelectedIndexChanged);
            this.FolderListBox.DoubleClick += new System.EventHandler(this.FolderListBox_DoubleClick);
            // 
            // FileBox
            // 
            this.FileBox.Controls.Add(this.UploadBTN);
            this.FileBox.Controls.Add(this.FilesListBox);
            this.FileBox.Controls.Add(this.DownloadBTN);
            this.FileBox.Location = new System.Drawing.Point(193, 12);
            this.FileBox.Name = "FileBox";
            this.FileBox.Size = new System.Drawing.Size(258, 279);
            this.FileBox.TabIndex = 2;
            this.FileBox.TabStop = false;
            this.FileBox.Text = "Files";
            // 
            // UploadBTN
            // 
            this.UploadBTN.Location = new System.Drawing.Point(96, 250);
            this.UploadBTN.Name = "UploadBTN";
            this.UploadBTN.Size = new System.Drawing.Size(75, 23);
            this.UploadBTN.TabIndex = 3;
            this.UploadBTN.Text = "Upload";
            this.UploadBTN.UseVisualStyleBackColor = true;
            this.UploadBTN.Click += new System.EventHandler(this.UploadBTN_Click);
            // 
            // FilesListBox
            // 
            this.FilesListBox.FormattingEnabled = true;
            this.FilesListBox.Location = new System.Drawing.Point(6, 19);
            this.FilesListBox.Name = "FilesListBox";
            this.FilesListBox.Size = new System.Drawing.Size(246, 225);
            this.FilesListBox.TabIndex = 1;
            this.FilesListBox.SelectedIndexChanged += new System.EventHandler(this.FilesListBox_SelectedIndexChanged);
            // 
            // DownloadBTN
            // 
            this.DownloadBTN.Location = new System.Drawing.Point(177, 250);
            this.DownloadBTN.Name = "DownloadBTN";
            this.DownloadBTN.Size = new System.Drawing.Size(75, 23);
            this.DownloadBTN.TabIndex = 2;
            this.DownloadBTN.Text = "Download";
            this.DownloadBTN.UseVisualStyleBackColor = true;
            this.DownloadBTN.Click += new System.EventHandler(this.DownloadBTN_Click);
            // 
            // DisconnectBTN
            // 
            this.DisconnectBTN.Location = new System.Drawing.Point(370, 297);
            this.DisconnectBTN.Name = "DisconnectBTN";
            this.DisconnectBTN.Size = new System.Drawing.Size(75, 23);
            this.DisconnectBTN.TabIndex = 3;
            this.DisconnectBTN.Text = "Disconnect";
            this.DisconnectBTN.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(461, 331);
            this.Controls.Add(this.DisconnectBTN);
            this.Controls.Add(this.FileBox);
            this.Controls.Add(this.FolderBox);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.FolderBox.ResumeLayout(false);
            this.FileBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox FolderBox;
        private System.Windows.Forms.ListBox FolderListBox;
        private System.Windows.Forms.GroupBox FileBox;
        private System.Windows.Forms.ListBox FilesListBox;
        private System.Windows.Forms.Button DownloadBTN;
        private System.Windows.Forms.Button UploadBTN;
        private System.Windows.Forms.Button UpLevelBTN;
        private System.Windows.Forms.Button RootBTN;
        private System.Windows.Forms.Button DisconnectBTN;
    }
}