namespace ChessUI.DashboardForm
{
    partial class ChoosePVPMode
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChoosePVPMode));
            button1 = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            button3 = new System.Windows.Forms.Button();
            pictureBox1 = new System.Windows.Forms.PictureBox();
            label2 = new System.Windows.Forms.Label();
            textBox1 = new System.Windows.Forms.TextBox();
            button2 = new System.Windows.Forms.Button();
            groupBox1 = new System.Windows.Forms.GroupBox();
            label4 = new System.Windows.Forms.Label();
            groupBox2 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // button1
            // 
            button1.BackColor = System.Drawing.SystemColors.Info;
            button1.Font = new System.Drawing.Font("Showcard Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            button1.Location = new System.Drawing.Point(405, 207);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(231, 37);
            button1.TabIndex = 3;
            button1.Text = "Host a game";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Showcard Gothic", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            label1.ForeColor = System.Drawing.Color.DarkOrange;
            label1.Location = new System.Drawing.Point(341, 26);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(430, 50);
            label1.TabIndex = 8;
            label1.Text = "Choose your mode";
            // 
            // button3
            // 
            button3.BackColor = System.Drawing.SystemColors.Info;
            button3.Font = new System.Drawing.Font("Showcard Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            button3.ForeColor = System.Drawing.Color.Black;
            button3.Location = new System.Drawing.Point(94, 60);
            button3.Name = "button3";
            button3.Size = new System.Drawing.Size(306, 72);
            button3.TabIndex = 10;
            button3.Text = "JOIN AUTOMATICALLY";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImage = (System.Drawing.Image)resources.GetObject("pictureBox1.BackgroundImage");
            pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            pictureBox1.Location = new System.Drawing.Point(700, 155);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new System.Drawing.Size(365, 369);
            pictureBox1.TabIndex = 11;
            pictureBox1.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Showcard Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label2.Location = new System.Drawing.Point(357, 157);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(32, 26);
            label2.TabIndex = 12;
            label2.Text = "IP";
            // 
            // textBox1
            // 
            textBox1.Location = new System.Drawing.Point(405, 156);
            textBox1.Name = "textBox1";
            textBox1.Size = new System.Drawing.Size(231, 27);
            textBox1.TabIndex = 13;
            // 
            // button2
            // 
            button2.Font = new System.Drawing.Font("Showcard Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            button2.ForeColor = System.Drawing.Color.MediumTurquoise;
            button2.Location = new System.Drawing.Point(655, 155);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(94, 29);
            button2.TabIndex = 14;
            button2.Text = "Connect";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label4);
            groupBox1.Font = new System.Drawing.Font("Showcard Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            groupBox1.ForeColor = System.Drawing.Color.MediumTurquoise;
            groupBox1.Location = new System.Drawing.Point(283, 108);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(488, 179);
            groupBox1.TabIndex = 15;
            groupBox1.TabStop = false;
            groupBox1.Text = "LAN MODE";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = System.Drawing.Color.Black;
            label4.Location = new System.Drawing.Point(65, 103);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(51, 29);
            label4.TabIndex = 0;
            label4.Text = "Or ";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(button3);
            groupBox2.Font = new System.Drawing.Font("Showcard Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            groupBox2.ForeColor = System.Drawing.Color.MediumTurquoise;
            groupBox2.Location = new System.Drawing.Point(283, 340);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new System.Drawing.Size(488, 184);
            groupBox2.TabIndex = 19;
            groupBox2.TabStop = false;
            groupBox2.Text = "ONLINE MODE";
            // 
            // ChoosePVPMode
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1091, 557);
            Controls.Add(button2);
            Controls.Add(textBox1);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(button1);
            Controls.Add(groupBox1);
            Controls.Add(groupBox2);
            Controls.Add(pictureBox1);
            Name = "ChoosePVPMode";
            Text = "ChoosePVPMode";
            Load += ChoosePVPMode_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
    }
}