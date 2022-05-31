namespace BebaKids.Racunovodstvo
{
    partial class izvodi
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(izvodi));
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.btnOsveziIzvod = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(24, 61);
            this.comboBox1.MinimumSize = new System.Drawing.Size(300, 0);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(300, 21);
            this.comboBox1.TabIndex = 0;
            // 
            // btnOsveziIzvod
            // 
            this.btnOsveziIzvod.Location = new System.Drawing.Point(24, 249);
            this.btnOsveziIzvod.Name = "btnOsveziIzvod";
            this.btnOsveziIzvod.Size = new System.Drawing.Size(300, 23);
            this.btnOsveziIzvod.TabIndex = 1;
            this.btnOsveziIzvod.Text = "Osvezi";
            this.btnOsveziIzvod.UseVisualStyleBackColor = true;
            this.btnOsveziIzvod.Click += new System.EventHandler(this.btnOsveziIzvod_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(261, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Iz padajuce liste odaberite izvod koji zelite da osvezite";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(287, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "** napomena : prikazuju se samo izvodi snimljeni na \"zuto\" !";
            // 
            // izvodi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(352, 284);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOsveziIzvod);
            this.Controls.Add(this.comboBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(300, 39);
            this.Name = "izvodi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Osvezavanje Izvoda";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button btnOsveziIzvod;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}