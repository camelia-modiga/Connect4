namespace ProiectIA
{
    partial class StartGame
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.startButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.numarRanduri = new System.Windows.Forms.NumericUpDown();
            this.numarColoane = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numarRanduri)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numarColoane)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(69, 29);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(122, 53);
            this.button1.TabIndex = 0;
            this.button1.Text = "Jucător";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(69, 88);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(122, 54);
            this.button2.TabIndex = 1;
            this.button2.Text = "Calculator";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // startButton
            // 
            this.startButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.startButton.Location = new System.Drawing.Point(56, 343);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(254, 66);
            this.startButton.TabIndex = 2;
            this.startButton.Text = "START";
            this.startButton.UseVisualStyleBackColor = false;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.pictureBox1.Image = global::ProiectIA.Properties.Resources.fundal3;
            this.pictureBox1.Location = new System.Drawing.Point(-5, 1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(814, 452);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // numarRanduri
            // 
            this.numarRanduri.Location = new System.Drawing.Point(638, 60);
            this.numarRanduri.Name = "numarRanduri";
            this.numarRanduri.Size = new System.Drawing.Size(120, 22);
            this.numarRanduri.TabIndex = 4;
            this.numarRanduri.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            // 
            // numarColoane
            // 
            this.numarColoane.Location = new System.Drawing.Point(638, 119);
            this.numarColoane.Name = "numarColoane";
            this.numarColoane.Size = new System.Drawing.Size(120, 22);
            this.numarColoane.TabIndex = 5;
            this.numarColoane.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(638, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "Rânduri";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(641, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 17);
            this.label2.TabIndex = 7;
            this.label2.Text = "Coloane";
            // 
            // StartGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numarColoane);
            this.Controls.Add(this.numarRanduri);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "StartGame";
            this.Text = "StartGame";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numarRanduri)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numarColoane)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.NumericUpDown numarRanduri;
        private System.Windows.Forms.NumericUpDown numarColoane;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}