namespace BebaKids.Prijava
{
    partial class Aktivnost
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Aktivnost));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.aktivnostToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.radniciToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.objektiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pregledToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.izvestajPoRadnikuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.izvestajPoObjektuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aktivnostToolStripMenuItem,
            this.pregledToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(747, 28);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // aktivnostToolStripMenuItem
            // 
            this.aktivnostToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.radniciToolStripMenuItem,
            this.objektiToolStripMenuItem});
            this.aktivnostToolStripMenuItem.Name = "aktivnostToolStripMenuItem";
            this.aktivnostToolStripMenuItem.Size = new System.Drawing.Size(82, 24);
            this.aktivnostToolStripMenuItem.Text = "Aktivnost";
            // 
            // radniciToolStripMenuItem
            // 
            this.radniciToolStripMenuItem.Name = "radniciToolStripMenuItem";
            this.radniciToolStripMenuItem.Size = new System.Drawing.Size(133, 26);
            this.radniciToolStripMenuItem.Text = "Radnici";
            this.radniciToolStripMenuItem.Click += new System.EventHandler(this.radniciToolStripMenuItem_Click);
            // 
            // objektiToolStripMenuItem
            // 
            this.objektiToolStripMenuItem.Name = "objektiToolStripMenuItem";
            this.objektiToolStripMenuItem.Size = new System.Drawing.Size(133, 26);
            this.objektiToolStripMenuItem.Text = "Objekti";
            this.objektiToolStripMenuItem.Click += new System.EventHandler(this.objektiToolStripMenuItem_Click);
            // 
            // pregledToolStripMenuItem
            // 
            this.pregledToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.izvestajPoRadnikuToolStripMenuItem,
            this.izvestajPoObjektuToolStripMenuItem});
            this.pregledToolStripMenuItem.Name = "pregledToolStripMenuItem";
            this.pregledToolStripMenuItem.Size = new System.Drawing.Size(72, 24);
            this.pregledToolStripMenuItem.Text = "Pregled";
            // 
            // izvestajPoRadnikuToolStripMenuItem
            // 
            this.izvestajPoRadnikuToolStripMenuItem.Name = "izvestajPoRadnikuToolStripMenuItem";
            this.izvestajPoRadnikuToolStripMenuItem.Size = new System.Drawing.Size(211, 26);
            this.izvestajPoRadnikuToolStripMenuItem.Text = "Izvestaj po radniku";
            this.izvestajPoRadnikuToolStripMenuItem.Click += new System.EventHandler(this.izvestajPoRadnikuToolStripMenuItem_Click);
            // 
            // izvestajPoObjektuToolStripMenuItem
            // 
            this.izvestajPoObjektuToolStripMenuItem.Name = "izvestajPoObjektuToolStripMenuItem";
            this.izvestajPoObjektuToolStripMenuItem.Size = new System.Drawing.Size(211, 26);
            this.izvestajPoObjektuToolStripMenuItem.Text = "Izvestaj po Objektu";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::BebaKids.Properties.Resources.logo;
            this.pictureBox1.Location = new System.Drawing.Point(255, 95);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(285, 66);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // Aktivnost
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(747, 249);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Aktivnost";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Aktivnost";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem aktivnostToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem radniciToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem objektiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pregledToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem izvestajPoRadnikuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem izvestajPoObjektuToolStripMenuItem;
    }
}