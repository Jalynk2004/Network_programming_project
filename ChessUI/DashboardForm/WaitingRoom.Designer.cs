namespace ChessUI.DashboardForm
{
    partial class WaitingRoom
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
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            button1 = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // label2
            // 
            label2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Segoe UI", 13F);
            label2.Location = new System.Drawing.Point(108, 127);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(374, 30);
            label2.TabIndex = 5;
            label2.Text = "To cancel request, click Cancel below!";
            // 
            // label1
            // 
            label1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Segoe UI", 23F);
            label1.Location = new System.Drawing.Point(68, 49);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(469, 52);
            label1.TabIndex = 4;
            label1.Text = "Your game will start soon!";
            // 
            // label3
            // 
            label3.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Segoe UI", 12F);
            label3.Location = new System.Drawing.Point(68, 255);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(460, 28);
            label3.TabIndex = 7;
            label3.Text = "This dialog will automatically close after game start.";
            // 
            // button1
            // 
            button1.Location = new System.Drawing.Point(214, 197);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(122, 40);
            button1.TabIndex = 8;
            button1.Text = "Cancel";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // WaitingRoom
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(584, 302);
            Controls.Add(button1);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(label3);
            Name = "WaitingRoom";
            Text = "WaitingRoom";
            Load += WaitingRoom_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
    }
}