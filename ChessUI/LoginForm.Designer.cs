namespace ChessUI
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            tabControl2 = new System.Windows.Forms.TabControl();
            tabPage3 = new System.Windows.Forms.TabPage();
            label8 = new System.Windows.Forms.Label();
            tabPage1 = new System.Windows.Forms.TabPage();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            loginBtn = new System.Windows.Forms.Button();
            label3 = new System.Windows.Forms.Label();
            pwLogin = new System.Windows.Forms.TextBox();
            label10 = new System.Windows.Forms.Label();
            emailLogin = new System.Windows.Forms.TextBox();
            label11 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            button1 = new System.Windows.Forms.Button();
            label6 = new System.Windows.Forms.Label();
            textBox1 = new System.Windows.Forms.TextBox();
            label7 = new System.Windows.Forms.Label();
            textBox2 = new System.Windows.Forms.TextBox();
            tabPage2 = new System.Windows.Forms.TabPage();
            label13 = new System.Windows.Forms.Label();
            tabControl1 = new System.Windows.Forms.TabControl();
            tabControl2.SuspendLayout();
            tabPage3.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            tabControl1.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl2
            // 
            tabControl2.Controls.Add(tabPage3);
            tabControl2.Location = new System.Drawing.Point(-7, -46);
            tabControl2.Name = "tabControl2";
            tabControl2.SelectedIndex = 0;
            tabControl2.Size = new System.Drawing.Size(901, 549);
            tabControl2.TabIndex = 9;
            // 
            // tabPage3
            // 
            tabPage3.BackgroundImage = (System.Drawing.Image)resources.GetObject("tabPage3.BackgroundImage");
            tabPage3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            tabPage3.Controls.Add(label8);
            tabPage3.Location = new System.Drawing.Point(4, 29);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new System.Windows.Forms.Padding(3);
            tabPage3.Size = new System.Drawing.Size(893, 516);
            tabPage3.TabIndex = 0;
            tabPage3.Text = "tabPage3";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new System.Drawing.Font("Segoe UI", 19.8000011F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            label8.Location = new System.Drawing.Point(87, 39);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(240, 46);
            label8.TabIndex = 0;
            label8.Text = "WELCOME TO";
            // 
            // tabPage1
            // 
            tabPage1.BackColor = System.Drawing.Color.OldLace;
            tabPage1.Controls.Add(label1);
            tabPage1.Controls.Add(label2);
            tabPage1.Controls.Add(loginBtn);
            tabPage1.Controls.Add(label3);
            tabPage1.Controls.Add(pwLogin);
            tabPage1.Controls.Add(label10);
            tabPage1.Controls.Add(emailLogin);
            tabPage1.Controls.Add(label11);
            tabPage1.Location = new System.Drawing.Point(4, 4);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new System.Windows.Forms.Padding(3);
            tabPage1.Size = new System.Drawing.Size(470, 526);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "tabPage1";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = System.Drawing.SystemColors.Highlight;
            label1.Location = new System.Drawing.Point(265, 399);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(70, 20);
            label1.TabIndex = 23;
            label1.Text = "Tạo ngay";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(110, 399);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(139, 20);
            label2.TabIndex = 22;
            label2.Text = "Chưa có tài khoản ?";
            // 
            // loginBtn
            // 
            loginBtn.Location = new System.Drawing.Point(174, 325);
            loginBtn.Name = "loginBtn";
            loginBtn.Size = new System.Drawing.Size(112, 45);
            loginBtn.TabIndex = 21;
            loginBtn.Text = "Đăng nhập";
            loginBtn.UseVisualStyleBackColor = true;
            loginBtn.Click += loginBtn_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Showcard Gothic", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            label3.ForeColor = System.Drawing.SystemColors.Desktop;
            label3.Location = new System.Drawing.Point(55, 53);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(380, 35);
            label3.TabIndex = 20;
            label3.Text = "Login to your account";
            // 
            // pwLogin
            // 
            pwLogin.Location = new System.Drawing.Point(107, 266);
            pwLogin.Name = "pwLogin";
            pwLogin.Size = new System.Drawing.Size(255, 27);
            pwLogin.TabIndex = 19;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new System.Drawing.Point(110, 108);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(75, 20);
            label10.TabIndex = 16;
            label10.Text = "Username";
            // 
            // emailLogin
            // 
            emailLogin.Location = new System.Drawing.Point(107, 154);
            emailLogin.Name = "emailLogin";
            emailLogin.Size = new System.Drawing.Size(255, 27);
            emailLogin.TabIndex = 18;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new System.Drawing.Point(110, 216);
            label11.Name = "label11";
            label11.Size = new System.Drawing.Size(70, 20);
            label11.TabIndex = 17;
            label11.Text = "Password";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            label4.Location = new System.Drawing.Point(241, 396);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(118, 20);
            label4.TabIndex = 36;
            label4.Text = "Đăng nhập ngay";
            label4.Click += label4_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(96, 396);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(124, 20);
            label5.TabIndex = 35;
            label5.Text = "Đã có tài khoản ?";
            // 
            // button1
            // 
            button1.Location = new System.Drawing.Point(164, 320);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(116, 48);
            button1.TabIndex = 34;
            button1.Text = "Signup";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(113, 110);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(75, 20);
            label6.TabIndex = 33;
            label6.Text = "Username";
            // 
            // textBox1
            // 
            textBox1.Location = new System.Drawing.Point(114, 140);
            textBox1.Name = "textBox1";
            textBox1.Size = new System.Drawing.Size(246, 27);
            textBox1.TabIndex = 32;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(117, 205);
            label7.Name = "label7";
            label7.RightToLeft = System.Windows.Forms.RightToLeft.No;
            label7.Size = new System.Drawing.Size(70, 20);
            label7.TabIndex = 31;
            label7.Text = "Password";
            // 
            // textBox2
            // 
            textBox2.Location = new System.Drawing.Point(114, 239);
            textBox2.Name = "textBox2";
            textBox2.Size = new System.Drawing.Size(250, 27);
            textBox2.TabIndex = 29;
            // 
            // tabPage2
            // 
            tabPage2.BackColor = System.Drawing.Color.OldLace;
            tabPage2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            tabPage2.Controls.Add(label4);
            tabPage2.Controls.Add(label5);
            tabPage2.Controls.Add(button1);
            tabPage2.Controls.Add(label6);
            tabPage2.Controls.Add(textBox1);
            tabPage2.Controls.Add(label7);
            tabPage2.Controls.Add(textBox2);
            tabPage2.Controls.Add(label13);
            tabPage2.Location = new System.Drawing.Point(4, 4);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new System.Windows.Forms.Padding(3);
            tabPage2.Size = new System.Drawing.Size(470, 526);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "tabPage2";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new System.Drawing.Font("Showcard Gothic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            label13.Location = new System.Drawing.Point(57, 53);
            label13.Name = "label13";
            label13.Size = new System.Drawing.Size(359, 37);
            label13.TabIndex = 27;
            label13.Text = "signup new account";
            // 
            // tabControl1
            // 
            tabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new System.Drawing.Point(416, -27);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new System.Drawing.Size(478, 559);
            tabControl1.TabIndex = 8;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(887, 498);
            Controls.Add(tabControl1);
            Controls.Add(tabControl2);
            Name = "LoginForm";
            Text = "LoginForm";
            tabControl2.ResumeLayout(false);
            tabPage3.ResumeLayout(false);
            tabPage3.PerformLayout();
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            tabControl1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button loginBtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox pwLogin;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox emailLogin;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Label label8;
    }
}