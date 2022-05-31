namespace BebaKids
{
    partial class frmInsertBarkod
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInsertBarkod));
            this.btnInsertBarkod = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // btnInsertBarkod
            // 
            this.btnInsertBarkod.Location = new System.Drawing.Point(215, 63);
            this.btnInsertBarkod.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnInsertBarkod.Name = "btnInsertBarkod";
            this.btnInsertBarkod.Size = new System.Drawing.Size(167, 28);
            this.btnInsertBarkod.TabIndex = 0;
            this.btnInsertBarkod.Text = "Prebaci Barkodove";
            this.btnInsertBarkod.UseVisualStyleBackColor = true;
            this.btnInsertBarkod.Click += new System.EventHandler(this.btnPrebaciBarkodove_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(173, 134);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(240, 28);
            this.progressBar1.TabIndex = 1;
            // 
            // frmInsertBarkod
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(613, 177);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnInsertBarkod);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "frmInsertBarkod";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Prebacivanje Barkodova";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnInsertBarkod;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}