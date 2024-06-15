namespace ChessUI.DashboardForm
{
    partial class Account
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
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            textBox1 = new System.Windows.Forms.TextBox();
            button1 = new System.Windows.Forms.Button();
            button2 = new System.Windows.Forms.Button();
            label5 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            textBox2 = new System.Windows.Forms.TextBox();
            eloLb = new System.Windows.Forms.Label();
            winLb = new System.Windows.Forms.Label();
            loseLb = new System.Windows.Forms.Label();
            drawLb = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            button3 = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Showcard Gothic", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label1.Location = new System.Drawing.Point(462, 27);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(295, 46);
            label1.TabIndex = 0;
            label1.Text = "Your account";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Showcard Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label2.ForeColor = System.Drawing.Color.MediumTurquoise;
            label2.Location = new System.Drawing.Point(385, 109);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(130, 29);
            label2.TabIndex = 1;
            label2.Text = "Username";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Showcard Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label3.ForeColor = System.Drawing.Color.MediumTurquoise;
            label3.Location = new System.Drawing.Point(385, 165);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(234, 29);
            label3.TabIndex = 7;
            label3.Text = "Change password";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("Showcard Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label4.ForeColor = System.Drawing.Color.MediumTurquoise;
            label4.Location = new System.Drawing.Point(385, 225);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(55, 29);
            label4.TabIndex = 8;
            label4.Text = "ELO";
            label4.Click += label4_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new System.Drawing.Point(540, 109);
            textBox1.Name = "textBox1";
            textBox1.Size = new System.Drawing.Size(125, 27);
            textBox1.TabIndex = 9;
            // 
            // button1
            // 
            button1.BackColor = System.Drawing.SystemColors.Info;
            button1.Font = new System.Drawing.Font("Showcard Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            button1.Location = new System.Drawing.Point(710, 108);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(94, 29);
            button1.TabIndex = 10;
            button1.Text = "Change";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.BackColor = System.Drawing.SystemColors.Info;
            button2.Font = new System.Drawing.Font("Showcard Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            button2.ForeColor = System.Drawing.SystemColors.ControlText;
            button2.Location = new System.Drawing.Point(778, 166);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(94, 29);
            button2.TabIndex = 11;
            button2.Text = "Change";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new System.Drawing.Font("Showcard Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label5.ForeColor = System.Drawing.Color.MediumSeaGreen;
            label5.Location = new System.Drawing.Point(385, 389);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(59, 29);
            label5.TabIndex = 12;
            label5.Text = "Win";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new System.Drawing.Font("Showcard Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label6.ForeColor = System.Drawing.Color.Brown;
            label6.Location = new System.Drawing.Point(528, 389);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(91, 29);
            label6.TabIndex = 13;
            label6.Text = "Losses";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new System.Drawing.Font("Showcard Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label7.ForeColor = System.Drawing.Color.Peru;
            label7.Location = new System.Drawing.Point(691, 389);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(84, 29);
            label7.TabIndex = 14;
            label7.Text = "Draw";
            // 
            // textBox2
            // 
            textBox2.Location = new System.Drawing.Point(625, 166);
            textBox2.Name = "textBox2";
            textBox2.Size = new System.Drawing.Size(125, 27);
            textBox2.TabIndex = 15;
            // 
            // eloLb
            // 
            eloLb.AutoSize = true;
            eloLb.Font = new System.Drawing.Font("Showcard Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            eloLb.Location = new System.Drawing.Point(474, 225);
            eloLb.Name = "eloLb";
            eloLb.Size = new System.Drawing.Size(41, 29);
            eloLb.TabIndex = 16;
            eloLb.Text = "....";
            // 
            // winLb
            // 
            winLb.AutoSize = true;
            winLb.Font = new System.Drawing.Font("Showcard Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            winLb.ForeColor = System.Drawing.Color.SeaGreen;
            winLb.Location = new System.Drawing.Point(394, 345);
            winLb.Name = "winLb";
            winLb.Size = new System.Drawing.Size(44, 37);
            winLb.TabIndex = 17;
            winLb.Text = "...";
            // 
            // loseLb
            // 
            loseLb.AutoSize = true;
            loseLb.Font = new System.Drawing.Font("Showcard Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            loseLb.ForeColor = System.Drawing.Color.Brown;
            loseLb.Location = new System.Drawing.Point(552, 345);
            loseLb.Name = "loseLb";
            loseLb.Size = new System.Drawing.Size(44, 37);
            loseLb.TabIndex = 18;
            loseLb.Text = "...";
            // 
            // drawLb
            // 
            drawLb.AutoSize = true;
            drawLb.Font = new System.Drawing.Font("Showcard Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            drawLb.ForeColor = System.Drawing.Color.Peru;
            drawLb.Location = new System.Drawing.Point(713, 345);
            drawLb.Name = "drawLb";
            drawLb.Size = new System.Drawing.Size(44, 37);
            drawLb.TabIndex = 19;
            drawLb.Text = "...";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new System.Drawing.Font("Showcard Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label8.ForeColor = System.Drawing.Color.MediumTurquoise;
            label8.Location = new System.Drawing.Point(385, 287);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(198, 29);
            label8.TabIndex = 20;
            label8.Text = "Match history";
            // 
            // button3
            // 
            button3.BackColor = System.Drawing.SystemColors.Info;
            button3.Font = new System.Drawing.Font("Showcard Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            button3.Location = new System.Drawing.Point(598, 283);
            button3.Name = "button3";
            button3.Size = new System.Drawing.Size(135, 42);
            button3.TabIndex = 21;
            button3.Text = "View";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // Account
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1003, 450);
            Controls.Add(button3);
            Controls.Add(label8);
            Controls.Add(drawLb);
            Controls.Add(loseLb);
            Controls.Add(winLb);
            Controls.Add(eloLb);
            Controls.Add(textBox2);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(textBox1);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "Account";
            Text = "Account";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label eloLb;
        private System.Windows.Forms.Label winLb;
        private System.Windows.Forms.Label loseLb;
        private System.Windows.Forms.Label drawLb;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button3;
    }
}