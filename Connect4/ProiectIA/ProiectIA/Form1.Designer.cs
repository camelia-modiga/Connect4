namespace ProiectIA
{
    partial class Connect4
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
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.jocToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.jocNouToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ieșireToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ajutorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.despreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel = new System.Windows.Forms.Panel();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.Color.White;
            this.menuStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.jocToolStripMenuItem,
            this.ajutorToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(699, 28);
            this.menuStrip.TabIndex = 2;
            this.menuStrip.Text = "menuStrip1";
            // 
            // jocToolStripMenuItem
            // 
            this.jocToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.jocNouToolStripMenuItem,
            this.ieșireToolStripMenuItem});
            this.jocToolStripMenuItem.Name = "jocToolStripMenuItem";
            this.jocToolStripMenuItem.Size = new System.Drawing.Size(44, 26);
            this.jocToolStripMenuItem.Text = "Joc";
            // 
            // jocNouToolStripMenuItem
            // 
            this.jocNouToolStripMenuItem.Name = "jocNouToolStripMenuItem";
            this.jocNouToolStripMenuItem.Size = new System.Drawing.Size(142, 26);
            this.jocNouToolStripMenuItem.Text = "Joc nou";
            // 
            // ieșireToolStripMenuItem
            // 
            this.ieșireToolStripMenuItem.Name = "ieșireToolStripMenuItem";
            this.ieșireToolStripMenuItem.Size = new System.Drawing.Size(142, 26);
            this.ieșireToolStripMenuItem.Text = "Ieșire";
            // 
            // ajutorToolStripMenuItem
            // 
            this.ajutorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.despreToolStripMenuItem});
            this.ajutorToolStripMenuItem.Name = "ajutorToolStripMenuItem";
            this.ajutorToolStripMenuItem.Size = new System.Drawing.Size(64, 26);
            this.ajutorToolStripMenuItem.Text = "Ajutor";
            // 
            // despreToolStripMenuItem
            // 
            this.despreToolStripMenuItem.Name = "despreToolStripMenuItem";
            this.despreToolStripMenuItem.Size = new System.Drawing.Size(139, 26);
            this.despreToolStripMenuItem.Text = "Despre";
            // 
            // panel
            // 
            this.panel.Location = new System.Drawing.Point(12, 47);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(679, 360);
            this.panel.TabIndex = 3;
            this.panel.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_Paint);
            // 
            // Connect4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(699, 412);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.menuStrip);
            this.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "Connect4";
            this.Text = "Connect4";
            this.Load += new System.EventHandler(this.Connect4_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem jocToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem jocNouToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ieșireToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ajutorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem despreToolStripMenuItem;
        private System.Windows.Forms.Panel panel;
    }
}

