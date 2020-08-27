namespace Igtampe.LBL.GUI.Forms {
    partial class ServerContactForm {
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
            this.Image = new System.Windows.Forms.PictureBox();
            this.HeaderLabel = new System.Windows.Forms.Label();
            this.SubtitleLabel = new System.Windows.Forms.Label();
            this.MainProgBar = new System.Windows.Forms.ProgressBar();
            this.CancelBTN = new System.Windows.Forms.Button();
            this.MainBWorker = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.Image)).BeginInit();
            this.SuspendLayout();
            // 
            // Image
            // 
            this.Image.Image = global::Igtampe.LBL.GUI.Properties.Resources.LBLLogo;
            this.Image.Location = new System.Drawing.Point(12, 12);
            this.Image.Name = "Image";
            this.Image.Size = new System.Drawing.Size(77, 77);
            this.Image.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Image.TabIndex = 0;
            this.Image.TabStop = false;
            // 
            // HeaderLabel
            // 
            this.HeaderLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HeaderLabel.Location = new System.Drawing.Point(95, 12);
            this.HeaderLabel.Name = "HeaderLabel";
            this.HeaderLabel.Size = new System.Drawing.Size(379, 33);
            this.HeaderLabel.TabIndex = 1;
            this.HeaderLabel.Text = "Contacting Server";
            // 
            // SubtitleLabel
            // 
            this.SubtitleLabel.Location = new System.Drawing.Point(98, 45);
            this.SubtitleLabel.Name = "SubtitleLabel";
            this.SubtitleLabel.Size = new System.Drawing.Size(376, 18);
            this.SubtitleLabel.TabIndex = 2;
            this.SubtitleLabel.Text = "Please Wait";
            // 
            // MainProgBar
            // 
            this.MainProgBar.Location = new System.Drawing.Point(101, 66);
            this.MainProgBar.Name = "MainProgBar";
            this.MainProgBar.Size = new System.Drawing.Size(292, 23);
            this.MainProgBar.TabIndex = 3;
            // 
            // CancelBTN
            // 
            this.CancelBTN.Location = new System.Drawing.Point(399, 67);
            this.CancelBTN.Name = "CancelBTN";
            this.CancelBTN.Size = new System.Drawing.Size(75, 23);
            this.CancelBTN.TabIndex = 4;
            this.CancelBTN.Text = "Cancel";
            this.CancelBTN.UseVisualStyleBackColor = true;
            this.CancelBTN.Click += new System.EventHandler(this.CancelBTN_Click);
            // 
            // MainBWorker
            // 
            MainBWorker.WorkerReportsProgress = true;
            MainBWorker.WorkerSupportsCancellation = true;
            MainBWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.MainBWorker_DoWork);
            MainBWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.MainBWorker_ProgressChanged);
            MainBWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.MainBWorker_RunWorkerCompleted);
            // 
            // ServerContactForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(486, 102);
            this.Controls.Add(this.CancelBTN);
            this.Controls.Add(this.MainProgBar);
            this.Controls.Add(this.SubtitleLabel);
            this.Controls.Add(this.HeaderLabel);
            this.Controls.Add(this.Image);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ServerContactForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Contacting the Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ServerContactForm_FormClosing);
            this.Shown += new System.EventHandler(this.ServerContactForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.Image)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.PictureBox Image;
        protected System.Windows.Forms.Label HeaderLabel;
        protected System.Windows.Forms.Label SubtitleLabel;
        protected System.Windows.Forms.ProgressBar MainProgBar;
        protected System.Windows.Forms.Button CancelBTN;
        protected System.ComponentModel.BackgroundWorker MainBWorker;
    }
}