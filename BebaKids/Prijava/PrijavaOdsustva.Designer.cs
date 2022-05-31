namespace BebaKids.Prijava
{
    partial class PrijavaOdsustva
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrijavaOdsustva));
            this.dateFrom = new System.Windows.Forms.DateTimePicker();
            this.dateTo = new System.Windows.Forms.DateTimePicker();
            this.cbPaidDay = new System.Windows.Forms.CheckBox();
            this.cbSickDay = new System.Windows.Forms.CheckBox();
            this.btnSacuvaj = new System.Windows.Forms.Button();
            this.comboRadnici = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // dateFrom
            // 
            this.dateFrom.Location = new System.Drawing.Point(32, 85);
            this.dateFrom.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dateFrom.Name = "dateFrom";
            this.dateFrom.Size = new System.Drawing.Size(151, 20);
            this.dateFrom.TabIndex = 0;
            // 
            // dateTo
            // 
            this.dateTo.Location = new System.Drawing.Point(223, 85);
            this.dateTo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dateTo.Name = "dateTo";
            this.dateTo.Size = new System.Drawing.Size(151, 20);
            this.dateTo.TabIndex = 0;
            // 
            // cbPaidDay
            // 
            this.cbPaidDay.AutoSize = true;
            this.cbPaidDay.Location = new System.Drawing.Point(32, 134);
            this.cbPaidDay.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbPaidDay.Name = "cbPaidDay";
            this.cbPaidDay.Size = new System.Drawing.Size(93, 17);
            this.cbPaidDay.TabIndex = 1;
            this.cbPaidDay.Text = "Placen Odmor";
            this.cbPaidDay.UseVisualStyleBackColor = true;
            // 
            // cbSickDay
            // 
            this.cbSickDay.AutoSize = true;
            this.cbSickDay.Location = new System.Drawing.Point(129, 134);
            this.cbSickDay.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbSickDay.Name = "cbSickDay";
            this.cbSickDay.Size = new System.Drawing.Size(73, 17);
            this.cbSickDay.TabIndex = 1;
            this.cbSickDay.Text = "Bolovanje";
            this.cbSickDay.UseVisualStyleBackColor = true;
            // 
            // btnSacuvaj
            // 
            this.btnSacuvaj.Location = new System.Drawing.Point(316, 210);
            this.btnSacuvaj.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSacuvaj.Name = "btnSacuvaj";
            this.btnSacuvaj.Size = new System.Drawing.Size(56, 23);
            this.btnSacuvaj.TabIndex = 2;
            this.btnSacuvaj.Text = "Sacuvaj";
            this.btnSacuvaj.UseVisualStyleBackColor = true;
            this.btnSacuvaj.Click += new System.EventHandler(this.btnSacuvaj_Click);
            // 
            // comboRadnici
            // 
            this.comboRadnici.FormattingEnabled = true;
            this.comboRadnici.Location = new System.Drawing.Point(88, 36);
            this.comboRadnici.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comboRadnici.Name = "comboRadnici";
            this.comboRadnici.Size = new System.Drawing.Size(151, 21);
            this.comboRadnici.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 36);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Radnik:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(32, 179);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(270, 65);
            this.textBox1.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(37, 162);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Napomena:";
            // 
            // PrijavaOdsustva
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 253);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboRadnici);
            this.Controls.Add(this.btnSacuvaj);
            this.Controls.Add(this.cbSickDay);
            this.Controls.Add(this.cbPaidDay);
            this.Controls.Add(this.dateTo);
            this.Controls.Add(this.dateFrom);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "PrijavaOdsustva";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Prijava Odsustva";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateFrom;
        private System.Windows.Forms.DateTimePicker dateTo;
        private System.Windows.Forms.CheckBox cbPaidDay;
        private System.Windows.Forms.CheckBox cbSickDay;
        private System.Windows.Forms.Button btnSacuvaj;
        private System.Windows.Forms.ComboBox comboRadnici;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label3;
    }
}