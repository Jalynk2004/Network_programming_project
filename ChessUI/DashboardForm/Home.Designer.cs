namespace ChessUI.DashboardForm
{
    partial class Home
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Home));
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            PvEButton = new System.Windows.Forms.Button();
            PvPButton = new System.Windows.Forms.Button();
            pictureBox1 = new System.Windows.Forms.PictureBox();
            label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // label2
            // 
            label2.Font = new System.Drawing.Font("Showcard Gothic", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            label2.Location = new System.Drawing.Point(661, 158);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(264, 118);
            label2.TabIndex = 21;
            label2.Text = "choose your play mode to begin";
            label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Showcard Gothic", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            label1.ForeColor = System.Drawing.Color.Peru;
            label1.Location = new System.Drawing.Point(261, 9);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(384, 98);
            label1.TabIndex = 20;
            label1.Text = "Chess.ai";
            // 
            // PvEButton
            // 
            PvEButton.BackColor = System.Drawing.Color.Gray;
            PvEButton.BackgroundImage = (System.Drawing.Image)resources.GetObject("PvEButton.BackgroundImage");
            PvEButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            PvEButton.FlatAppearance.BorderSize = 0;
            PvEButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            PvEButton.Font = new System.Drawing.Font("Showcard Gothic", 22.2F, System.Drawing.FontStyle.Bold);
            PvEButton.Location = new System.Drawing.Point(681, 288);
            PvEButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            PvEButton.Name = "PvEButton";
            PvEButton.Size = new System.Drawing.Size(222, 58);
            PvEButton.TabIndex = 19;
            PvEButton.Text = "🤖 PvE 🤖 ";
            PvEButton.UseVisualStyleBackColor = false;
            PvEButton.Click += PvEButton_Click;
            // 
            // PvPButton
            // 
            PvPButton.BackColor = System.Drawing.Color.IndianRed;
            PvPButton.BackgroundImage = (System.Drawing.Image)resources.GetObject("PvPButton.BackgroundImage");
            PvPButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            PvPButton.FlatAppearance.BorderSize = 0;
            PvPButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            PvPButton.Font = new System.Drawing.Font("Showcard Gothic", 22.2F, System.Drawing.FontStyle.Bold);
            PvPButton.ForeColor = System.Drawing.Color.Sienna;
            PvPButton.Location = new System.Drawing.Point(681, 380);
            PvPButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            PvPButton.Name = "PvPButton";
            PvPButton.Size = new System.Drawing.Size(222, 58);
            PvPButton.TabIndex = 18;
            PvPButton.Text = "⚔ PvP ⚔️";
            PvPButton.UseVisualStyleBackColor = false;
            PvPButton.Click += PvPButton_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImage = (System.Drawing.Image)resources.GetObject("pictureBox1.BackgroundImage");
            pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            pictureBox1.Location = new System.Drawing.Point(262, 125);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new System.Drawing.Size(383, 339);
            pictureBox1.TabIndex = 22;
            pictureBox1.TabStop = false;
            // 
            // label3
            // 
            label3.Font = new System.Drawing.Font("Showcard Gothic", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label3.ForeColor = System.Drawing.Color.DarkTurquoise;
            label3.Location = new System.Drawing.Point(695, 26);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(250, 119);
            label3.TabIndex = 23;
            label3.Text = "label3";
            label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            label3.Click += label3_Click;
            // 
            // Home
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.SystemColors.ButtonHighlight;
            ClientSize = new System.Drawing.Size(1108, 518);
            Controls.Add(label3);
            Controls.Add(pictureBox1);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(PvEButton);
            Controls.Add(PvPButton);
            Name = "Home";
            Text = "Home";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button PvEButton;
        private System.Windows.Forms.Button PvPButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label3;
    }
}