namespace ChessUI.DashboardForm
{
    partial class ChoosePVEMode
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChoosePVEMode));
            button1 = new System.Windows.Forms.Button();
            label2 = new System.Windows.Forms.Label();
            comboBox1 = new System.Windows.Forms.ComboBox();
            label1 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            comboBox2 = new System.Windows.Forms.ComboBox();
            pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.BackColor = System.Drawing.SystemColors.Info;
            button1.Font = new System.Drawing.Font("Showcard Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            button1.ForeColor = System.Drawing.SystemColors.ControlText;
            button1.Location = new System.Drawing.Point(442, 337);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(184, 71);
            button1.TabIndex = 13;
            button1.Text = "Start game";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Showcard Gothic", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label2.Location = new System.Drawing.Point(405, 162);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(82, 35);
            label2.TabIndex = 9;
            label2.Text = "Time";
            // 
            // comboBox1
            // 
            comboBox1.BackColor = System.Drawing.SystemColors.Info;
            comboBox1.Font = new System.Drawing.Font("Showcard Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new System.Drawing.Point(519, 171);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new System.Drawing.Size(151, 26);
            comboBox1.TabIndex = 8;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Showcard Gothic", 19.8000011F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            label1.ForeColor = System.Drawing.Color.DarkOrange;
            label1.Location = new System.Drawing.Point(329, 75);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(418, 43);
            label1.TabIndex = 7;
            label1.Text = "Choose your setting";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("Showcard Gothic", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            label4.Location = new System.Drawing.Point(352, 255);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(186, 35);
            label4.TabIndex = 14;
            label4.Text = "Difficulty";
            // 
            // comboBox2
            // 
            comboBox2.BackColor = System.Drawing.SystemColors.Info;
            comboBox2.Font = new System.Drawing.Font("Showcard Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new System.Drawing.Point(563, 255);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new System.Drawing.Size(146, 34);
            comboBox2.TabIndex = 15;
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImage = (System.Drawing.Image)resources.GetObject("pictureBox1.BackgroundImage");
            pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            pictureBox1.Location = new System.Drawing.Point(676, 121);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new System.Drawing.Size(249, 266);
            pictureBox1.TabIndex = 16;
            pictureBox1.TabStop = false;
            // 
            // ChoosePVEMode
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(920, 450);
            Controls.Add(comboBox2);
            Controls.Add(label4);
            Controls.Add(button1);
            Controls.Add(label2);
            Controls.Add(comboBox1);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            Name = "ChoosePVEMode";
            Text = "ChoosePVEMode";
            Load += ChoosePVEMode_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}