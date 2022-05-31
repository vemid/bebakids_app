namespace BebaKids.Proizvodnja
{
    partial class ProveraCene
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbSifra = new System.Windows.Forms.TextBox();
            this.btnProveraCene = new System.Windows.Forms.Button();
            this.gbCene = new System.Windows.Forms.GroupBox();
            this.tbBih = new System.Windows.Forms.TextBox();
            this.tbCg = new System.Windows.Forms.TextBox();
            this.tbSrb = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.sifra = new System.Windows.Forms.Label();
            this.nazivArt = new System.Windows.Forms.Label();
            this.SRB = new System.Windows.Forms.Label();
            this.rthSirovinski = new System.Windows.Forms.RichTextBox();
            this.btSirovinski = new System.Windows.Forms.Button();
            this.gbCene.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(47, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Sifra artikla: ";
            // 
            // tbSifra
            // 
            this.tbSifra.Location = new System.Drawing.Point(118, 12);
            this.tbSifra.Name = "tbSifra";
            this.tbSifra.Size = new System.Drawing.Size(139, 20);
            this.tbSifra.TabIndex = 1;
            this.tbSifra.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_KeyDown);
            // 
            // btnProveraCene
            // 
            this.btnProveraCene.Location = new System.Drawing.Point(276, 9);
            this.btnProveraCene.Name = "btnProveraCene";
            this.btnProveraCene.Size = new System.Drawing.Size(75, 23);
            this.btnProveraCene.TabIndex = 2;
            this.btnProveraCene.Text = "Proveri";
            this.btnProveraCene.UseVisualStyleBackColor = true;
            this.btnProveraCene.Click += new System.EventHandler(this.btnProveraCene_Click);
            // 
            // gbCene
            // 
            this.gbCene.Controls.Add(this.tbBih);
            this.gbCene.Controls.Add(this.tbCg);
            this.gbCene.Controls.Add(this.tbSrb);
            this.gbCene.Controls.Add(this.label3);
            this.gbCene.Controls.Add(this.label2);
            this.gbCene.Controls.Add(this.sifra);
            this.gbCene.Controls.Add(this.nazivArt);
            this.gbCene.Controls.Add(this.SRB);
            this.gbCene.Location = new System.Drawing.Point(69, 51);
            this.gbCene.Name = "gbCene";
            this.gbCene.Size = new System.Drawing.Size(239, 116);
            this.gbCene.TabIndex = 3;
            this.gbCene.TabStop = false;
            this.gbCene.Text = "Cene";
            this.gbCene.Visible = false;
            // 
            // tbBih
            // 
            this.tbBih.Location = new System.Drawing.Point(167, 90);
            this.tbBih.Name = "tbBih";
            this.tbBih.Size = new System.Drawing.Size(65, 20);
            this.tbBih.TabIndex = 5;
            // 
            // tbCg
            // 
            this.tbCg.Location = new System.Drawing.Point(86, 90);
            this.tbCg.Name = "tbCg";
            this.tbCg.Size = new System.Drawing.Size(65, 20);
            this.tbCg.TabIndex = 5;
            // 
            // tbSrb
            // 
            this.tbSrb.Location = new System.Drawing.Point(6, 90);
            this.tbSrb.Name = "tbSrb";
            this.tbSrb.Size = new System.Drawing.Size(65, 20);
            this.tbSrb.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(183, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "BIH";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(107, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "CG";
            // 
            // sifra
            // 
            this.sifra.AutoSize = true;
            this.sifra.Location = new System.Drawing.Point(6, 27);
            this.sifra.Name = "sifra";
            this.sifra.Size = new System.Drawing.Size(32, 13);
            this.sifra.TabIndex = 4;
            this.sifra.Text = "naziv";
            // 
            // nazivArt
            // 
            this.nazivArt.AutoSize = true;
            this.nazivArt.Location = new System.Drawing.Point(6, 49);
            this.nazivArt.Name = "nazivArt";
            this.nazivArt.Size = new System.Drawing.Size(32, 13);
            this.nazivArt.TabIndex = 4;
            this.nazivArt.Text = "naziv";
            // 
            // SRB
            // 
            this.SRB.AutoSize = true;
            this.SRB.Location = new System.Drawing.Point(24, 74);
            this.SRB.Name = "SRB";
            this.SRB.Size = new System.Drawing.Size(29, 13);
            this.SRB.TabIndex = 4;
            this.SRB.Text = "SRB";
            // 
            // rthSirovinski
            // 
            this.rthSirovinski.Location = new System.Drawing.Point(69, 185);
            this.rthSirovinski.Name = "rthSirovinski";
            this.rthSirovinski.Size = new System.Drawing.Size(239, 104);
            this.rthSirovinski.TabIndex = 4;
            this.rthSirovinski.Text = "";
            this.rthSirovinski.Visible = false;
            // 
            // btSirovinski
            // 
            this.btSirovinski.Location = new System.Drawing.Point(175, 304);
            this.btSirovinski.Name = "btSirovinski";
            this.btSirovinski.Size = new System.Drawing.Size(133, 23);
            this.btSirovinski.TabIndex = 5;
            this.btSirovinski.Text = "Dodaj Sirovnski";
            this.btSirovinski.UseVisualStyleBackColor = true;
            this.btSirovinski.Visible = false;
            this.btSirovinski.Click += new System.EventHandler(this.btSirovinski_Click);
            // 
            // ProveraCene
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 372);
            this.Controls.Add(this.btSirovinski);
            this.Controls.Add(this.rthSirovinski);
            this.Controls.Add(this.gbCene);
            this.Controls.Add(this.btnProveraCene);
            this.Controls.Add(this.tbSifra);
            this.Controls.Add(this.label1);
            this.Name = "ProveraCene";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Provera Cene";
            this.Load += new System.EventHandler(this.ProveraCene_Load);
            this.gbCene.ResumeLayout(false);
            this.gbCene.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbSifra;
        private System.Windows.Forms.Button btnProveraCene;
        private System.Windows.Forms.GroupBox gbCene;
        private System.Windows.Forms.TextBox tbBih;
        private System.Windows.Forms.TextBox tbCg;
        private System.Windows.Forms.TextBox tbSrb;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label SRB;
        private System.Windows.Forms.Label nazivArt;
        private System.Windows.Forms.Label sifra;
        private System.Windows.Forms.RichTextBox rthSirovinski;
        private System.Windows.Forms.Button btSirovinski;
    }
}