namespace DungeonPlanner
{
    partial class frmStats
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
            this.components = new System.ComponentModel.Container();
            this.txtStats = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.timRefresh = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // txtStats
            // 
            this.txtStats.Location = new System.Drawing.Point(12, 12);
            this.txtStats.Multiline = true;
            this.txtStats.Name = "txtStats";
            this.txtStats.ReadOnly = true;
            this.txtStats.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtStats.Size = new System.Drawing.Size(360, 214);
            this.txtStats.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(293, 238);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // timRefresh
            // 
            this.timRefresh.Enabled = true;
            this.timRefresh.Interval = 1000;
            this.timRefresh.Tick += new System.EventHandler(this.timRefresh_Tick);
            // 
            // frmStats
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 270);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.txtStats);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmStats";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Statistics";
            this.Load += new System.EventHandler(this.frmStats_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtStats;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Timer timRefresh;
    }
}