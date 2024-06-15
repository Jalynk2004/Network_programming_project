namespace ChessUI.DashboardForm
{
    partial class MatchHistory
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
            listView1 = new System.Windows.Forms.ListView();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Showcard Gothic", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label1.ForeColor = System.Drawing.Color.Peru;
            label1.Location = new System.Drawing.Point(266, 24);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(317, 46);
            label1.TabIndex = 1;
            label1.Text = "Match history";
            // 
            // listView1
            // 
            listView1.BackColor = System.Drawing.SystemColors.Info;
            listView1.Location = new System.Drawing.Point(71, 102);
            listView1.Name = "listView1";
            listView1.Size = new System.Drawing.Size(732, 362);
            listView1.TabIndex = 2;
            listView1.UseCompatibleStateImageBehavior = false;
            // 
            // MatchHistory
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(852, 497);
            Controls.Add(listView1);
            Controls.Add(label1);
            Name = "MatchHistory";
            Text = "MatchHistory";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView listView1;
    }
}