namespace Igtampe.LBL.GUI.Forms {
    partial class LoginForm {
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.ServerGroupBox = new System.Windows.Forms.GroupBox();
            this.PortBox = new System.Windows.Forms.TextBox();
            this.IPBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.LoginBox = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.LoginButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.ServerGroupBox.SuspendLayout();
            this.LoginBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Igtampe.LBL.GUI.Properties.Resources.LBLLogo;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(125, 81);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // ServerGroupBox
            // 
            this.ServerGroupBox.Controls.Add(this.PortBox);
            this.ServerGroupBox.Controls.Add(this.IPBox);
            this.ServerGroupBox.Controls.Add(this.label2);
            this.ServerGroupBox.Controls.Add(this.label1);
            this.ServerGroupBox.Location = new System.Drawing.Point(143, 12);
            this.ServerGroupBox.Name = "ServerGroupBox";
            this.ServerGroupBox.Size = new System.Drawing.Size(163, 81);
            this.ServerGroupBox.TabIndex = 1;
            this.ServerGroupBox.TabStop = false;
            this.ServerGroupBox.Text = "Server";
            // 
            // PortBox
            // 
            this.PortBox.Location = new System.Drawing.Point(43, 49);
            this.PortBox.Name = "PortBox";
            this.PortBox.Size = new System.Drawing.Size(100, 20);
            this.PortBox.TabIndex = 3;
            this.PortBox.Text = "909";
            // 
            // IPBox
            // 
            this.IPBox.Location = new System.Drawing.Point(43, 23);
            this.IPBox.Name = "IPBox";
            this.IPBox.Size = new System.Drawing.Size(100, 20);
            this.IPBox.TabIndex = 2;
            this.IPBox.Text = "127.0.0.1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Port";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP";
            // 
            // LoginBox
            // 
            this.LoginBox.Controls.Add(this.textBox1);
            this.LoginBox.Controls.Add(this.textBox2);
            this.LoginBox.Controls.Add(this.label3);
            this.LoginBox.Controls.Add(this.label4);
            this.LoginBox.Location = new System.Drawing.Point(312, 12);
            this.LoginBox.Name = "LoginBox";
            this.LoginBox.Size = new System.Drawing.Size(200, 81);
            this.LoginBox.TabIndex = 4;
            this.LoginBox.TabStop = false;
            this.LoginBox.Text = "Login";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(81, 49);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 3;
            this.textBox1.Text = "909";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(81, 23);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 2;
            this.textBox2.Text = "127.0.0.1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Password";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Username";
            // 
            // LoginButton
            // 
            this.LoginButton.Location = new System.Drawing.Point(437, 99);
            this.LoginButton.Name = "LoginButton";
            this.LoginButton.Size = new System.Drawing.Size(75, 23);
            this.LoginButton.TabIndex = 5;
            this.LoginButton.Text = "Login";
            this.LoginButton.UseVisualStyleBackColor = true;
            this.LoginButton.Click += new System.EventHandler(this.LoginButton_Click);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 134);
            this.Controls.Add(this.LoginButton);
            this.Controls.Add(this.LoginBox);
            this.Controls.Add(this.ServerGroupBox);
            this.Controls.Add(this.pictureBox1);
            this.MaximumSize = new System.Drawing.Size(540, 173);
            this.MinimumSize = new System.Drawing.Size(540, 173);
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LBL";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ServerGroupBox.ResumeLayout(false);
            this.ServerGroupBox.PerformLayout();
            this.LoginBox.ResumeLayout(false);
            this.LoginBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox ServerGroupBox;
        private System.Windows.Forms.TextBox PortBox;
        private System.Windows.Forms.TextBox IPBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox LoginBox;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button LoginButton;
    }
}

