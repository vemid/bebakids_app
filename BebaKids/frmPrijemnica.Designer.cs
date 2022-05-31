namespace BebaKids
{
    partial class frmPrijemnica
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrijemnica));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbBarkod = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lbSifra = new System.Windows.Forms.Label();
            this.lbVelicina = new System.Windows.Forms.Label();
            this.lbNaziv = new System.Windows.Forms.Label();
            this.tbPrijemnica = new System.Windows.Forms.TextBox();
            this.btnUporedi = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 28);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Unesi Broj Prijemnice:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(120, 90);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Barkod:";
            // 
            // tbBarkod
            // 
            this.tbBarkod.Location = new System.Drawing.Point(187, 81);
            this.tbBarkod.Margin = new System.Windows.Forms.Padding(4);
            this.tbBarkod.Name = "tbBarkod";
            this.tbBarkod.Size = new System.Drawing.Size(197, 22);
            this.tbBarkod.TabIndex = 1;
            this.tbBarkod.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxTest_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(55, 132);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "Sifra:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(33, 164);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "Velicina:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(47, 192);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 17);
            this.label5.TabIndex = 0;
            this.label5.Text = "Naziv:";
            // 
            // lbSifra
            // 
            this.lbSifra.AutoSize = true;
            this.lbSifra.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.lbSifra.Location = new System.Drawing.Point(91, 132);
            this.lbSifra.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbSifra.Name = "lbSifra";
            this.lbSifra.Size = new System.Drawing.Size(41, 17);
            this.lbSifra.TabIndex = 0;
            this.lbSifra.Text = "Sifra:";
            // 
            // lbVelicina
            // 
            this.lbVelicina.AutoSize = true;
            this.lbVelicina.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.lbVelicina.Location = new System.Drawing.Point(91, 164);
            this.lbVelicina.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbVelicina.Name = "lbVelicina";
            this.lbVelicina.Size = new System.Drawing.Size(61, 17);
            this.lbVelicina.TabIndex = 0;
            this.lbVelicina.Text = "Velicina:";
            // 
            // lbNaziv
            // 
            this.lbNaziv.AutoSize = true;
            this.lbNaziv.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.lbNaziv.Location = new System.Drawing.Point(91, 192);
            this.lbNaziv.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbNaziv.Name = "lbNaziv";
            this.lbNaziv.Size = new System.Drawing.Size(47, 17);
            this.lbNaziv.TabIndex = 0;
            this.lbNaziv.Text = "Naziv:";
            // 
            // tbPrijemnica
            // 
            this.tbPrijemnica.Location = new System.Drawing.Point(187, 28);
            this.tbPrijemnica.Margin = new System.Windows.Forms.Padding(4);
            this.tbPrijemnica.Name = "tbPrijemnica";
            this.tbPrijemnica.Size = new System.Drawing.Size(197, 22);
            this.tbPrijemnica.TabIndex = 1;
            this.tbPrijemnica.Leave += new System.EventHandler(this.tbPrijemnica_Leave);
            // 
            // btnUporedi
            // 
            this.btnUporedi.Location = new System.Drawing.Point(187, 246);
            this.btnUporedi.Name = "btnUporedi";
            this.btnUporedi.Size = new System.Drawing.Size(75, 23);
            this.btnUporedi.TabIndex = 2;
            this.btnUporedi.Text = "Uporedi";
            this.btnUporedi.UseVisualStyleBackColor = true;
            this.btnUporedi.Click += new System.EventHandler(this.btn_Proveri);
            // 
            // frmPrijemnica
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 289);
            this.Controls.Add(this.btnUporedi);
            this.Controls.Add(this.tbPrijemnica);
            this.Controls.Add(this.tbBarkod);
            this.Controls.Add(this.lbNaziv);
            this.Controls.Add(this.lbVelicina);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lbSifra);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmPrijemnica";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Skeniranje dokumenta";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbBarkod;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbSifra;
        private System.Windows.Forms.Label lbVelicina;
        private System.Windows.Forms.Label lbNaziv;
        private System.Windows.Forms.TextBox tbPrijemnica;
        private System.Windows.Forms.Button btnUporedi;
    }
}