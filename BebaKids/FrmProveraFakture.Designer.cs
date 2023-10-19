namespace BebaKids
{
    partial class FrmProveraFakture
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmProveraFakture));
            this.btnUporedi = new System.Windows.Forms.Button();
            this.tbPrijemnica = new System.Windows.Forms.TextBox();
            this.tbBarkod = new System.Windows.Forms.TextBox();
            this.lbNaziv = new System.Windows.Forms.Label();
            this.lbVelicina = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lbSifra = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbObjekat = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnUporedi
            // 
            this.btnUporedi.Location = new System.Drawing.Point(290, 420);
            this.btnUporedi.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnUporedi.Name = "btnUporedi";
            this.btnUporedi.Size = new System.Drawing.Size(112, 36);
            this.btnUporedi.TabIndex = 13;
            this.btnUporedi.Text = "Uporedi";
            this.btnUporedi.UseVisualStyleBackColor = true;
            this.btnUporedi.Click += new System.EventHandler(this.btnUporedi_Click);
            // 
            // tbPrijemnica
            // 
            this.tbPrijemnica.Location = new System.Drawing.Point(290, 80);
            this.tbPrijemnica.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tbPrijemnica.Name = "tbPrijemnica";
            this.tbPrijemnica.Size = new System.Drawing.Size(294, 31);
            this.tbPrijemnica.TabIndex = 11;
            this.tbPrijemnica.Leave += new System.EventHandler(this.tbPrijemnica_Leave);
            // 
            // tbBarkod
            // 
            this.tbBarkod.Location = new System.Drawing.Point(290, 162);
            this.tbBarkod.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tbBarkod.Name = "tbBarkod";
            this.tbBarkod.Size = new System.Drawing.Size(294, 31);
            this.tbBarkod.TabIndex = 12;
            this.tbBarkod.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxTest_KeyDown);
            // 
            // lbNaziv
            // 
            this.lbNaziv.AutoSize = true;
            this.lbNaziv.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.lbNaziv.Location = new System.Drawing.Point(146, 336);
            this.lbNaziv.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lbNaziv.Name = "lbNaziv";
            this.lbNaziv.Size = new System.Drawing.Size(72, 25);
            this.lbNaziv.TabIndex = 3;
            this.lbNaziv.Text = "Naziv:";
            // 
            // lbVelicina
            // 
            this.lbVelicina.AutoSize = true;
            this.lbVelicina.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.lbVelicina.Location = new System.Drawing.Point(146, 292);
            this.lbVelicina.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lbVelicina.Name = "lbVelicina";
            this.lbVelicina.Size = new System.Drawing.Size(94, 25);
            this.lbVelicina.TabIndex = 4;
            this.lbVelicina.Text = "Velicina:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(80, 336);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 25);
            this.label5.TabIndex = 5;
            this.label5.Text = "Naziv:";
            // 
            // lbSifra
            // 
            this.lbSifra.AutoSize = true;
            this.lbSifra.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.lbSifra.Location = new System.Drawing.Point(146, 242);
            this.lbSifra.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lbSifra.Name = "lbSifra";
            this.lbSifra.Size = new System.Drawing.Size(62, 25);
            this.lbSifra.TabIndex = 6;
            this.lbSifra.Text = "Sifra:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(58, 292);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 25);
            this.label4.TabIndex = 7;
            this.label4.Text = "Velicina:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(92, 242);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 25);
            this.label3.TabIndex = 8;
            this.label3.Text = "Sifra:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(189, 177);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 25);
            this.label2.TabIndex = 9;
            this.label2.Text = "Barkod:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(58, 80);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(223, 25);
            this.label1.TabIndex = 10;
            this.label1.Text = "Unesi Broj Prijemnice:";
            // 
            // tbObjekat
            // 
            this.tbObjekat.Location = new System.Drawing.Point(32, 502);
            this.tbObjekat.Name = "tbObjekat";
            this.tbObjekat.Size = new System.Drawing.Size(100, 31);
            this.tbObjekat.TabIndex = 14;
            this.tbObjekat.Visible = false;
            // 
            // FrmProveraFakture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 564);
            this.Controls.Add(this.tbObjekat);
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
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmProveraFakture";
            this.Text = "Provera Fakture";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnUporedi;
        private System.Windows.Forms.TextBox tbPrijemnica;
        private System.Windows.Forms.TextBox tbBarkod;
        private System.Windows.Forms.Label lbNaziv;
        private System.Windows.Forms.Label lbVelicina;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbSifra;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbObjekat;
    }
}