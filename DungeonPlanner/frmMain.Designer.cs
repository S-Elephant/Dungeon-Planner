namespace DungeonPlanner
{
    partial class frmMain
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
            this.btnRotate = new System.Windows.Forms.Button();
            this.lstRooms = new System.Windows.Forms.ListBox();
            this.chkDrawTunnel = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblversion = new System.Windows.Forms.Label();
            this.dungeonMap1 = new DungeonPlanner.DungeonMap();
            this.SuspendLayout();
            // 
            // btnRotate
            // 
            this.btnRotate.Location = new System.Drawing.Point(16, 269);
            this.btnRotate.Name = "btnRotate";
            this.btnRotate.Size = new System.Drawing.Size(120, 23);
            this.btnRotate.TabIndex = 3;
            this.btnRotate.Text = "Rotate 90 degrees cw";
            this.btnRotate.UseVisualStyleBackColor = true;
            this.btnRotate.Click += new System.EventHandler(this.btnRotate_Click);
            // 
            // lstRooms
            // 
            this.lstRooms.FormattingEnabled = true;
            this.lstRooms.Items.AddRange(new object[] {
            "Ducal Dining Room",
            "Dungeon Entrance",
            "Dungeon Exit",
            "Fortification Room",
            "Gun Room",
            "Heart Room",
            "Ignoble Pit Chamber",
            "Knights Hall",
            "Ladies Chamber",
            "Ordinary Pantry",
            "Ordinary Rock Chamber",
            "Rare Dungeon",
            "Rare Parlor",
            "Servant\'s Chamber",
            "Servant\'s Entrance",
            "Small Cavern",
            "Small Escape Chamber",
            "Small Grotto Room"});
            this.lstRooms.Location = new System.Drawing.Point(16, 25);
            this.lstRooms.Name = "lstRooms";
            this.lstRooms.Size = new System.Drawing.Size(120, 238);
            this.lstRooms.Sorted = true;
            this.lstRooms.TabIndex = 4;
            this.lstRooms.SelectedIndexChanged += new System.EventHandler(this.lstRooms_SelectedIndexChanged);
            // 
            // chkDrawTunnel
            // 
            this.chkDrawTunnel.AutoSize = true;
            this.chkDrawTunnel.Location = new System.Drawing.Point(18, 298);
            this.chkDrawTunnel.Name = "chkDrawTunnel";
            this.chkDrawTunnel.Size = new System.Drawing.Size(84, 17);
            this.chkDrawTunnel.TabIndex = 5;
            this.chkDrawTunnel.Text = "DrawTunnel";
            this.chkDrawTunnel.UseVisualStyleBackColor = true;
            this.chkDrawTunnel.CheckedChanged += new System.EventHandler(this.chkDrawTunnel_CheckedChanged);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(16, 351);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Save...";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(16, 380);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 8;
            this.btnLoad.Text = "Load...";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(16, 407);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 9;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 479);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(197, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Left mouse button: place room or tunnel.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 492);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(213, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Right mouse button: remove room or tunnel.";
            // 
            // lblversion
            // 
            this.lblversion.AutoSize = true;
            this.lblversion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblversion.Location = new System.Drawing.Point(3, 466);
            this.lblversion.Name = "lblversion";
            this.lblversion.Size = new System.Drawing.Size(72, 13);
            this.lblversion.TabIndex = 12;
            this.lblversion.Text = "Version: 0.9.4";
            // 
            // dungeonMap1
            // 
            this.dungeonMap1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.dungeonMap1.IsRoomMode = true;
            this.dungeonMap1.Location = new System.Drawing.Point(217, 25);
            this.dungeonMap1.Name = "dungeonMap1";
            this.dungeonMap1.NextRoomToAdd = null;
            this.dungeonMap1.Rotation = 0;
            this.dungeonMap1.Size = new System.Drawing.Size(508, 405);
            this.dungeonMap1.TabIndex = 6;
            this.dungeonMap1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dungeonMap1_MouseMove);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(729, 523);
            this.Controls.Add(this.lblversion);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dungeonMap1);
            this.Controls.Add(this.chkDrawTunnel);
            this.Controls.Add(this.lstRooms);
            this.Controls.Add(this.btnRotate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRotate;
        private System.Windows.Forms.ListBox lstRooms;
        private System.Windows.Forms.CheckBox chkDrawTunnel;
        private DungeonMap dungeonMap1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblversion;
    }
}

