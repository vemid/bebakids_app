namespace BebaKids
{
    partial class frmRucniUnos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRucniUnos));
            this.btnInsertManual = new System.Windows.Forms.Button();
            this.tbKolicina = new System.Windows.Forms.TextBox();
            this.tbVelicina = new System.Windows.Forms.TextBox();
            this.tbSifra = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnInsertManual
            // 
            this.btnInsertManual.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnInsertManual.Location = new System.Drawing.Point(279, 116);
            this.btnInsertManual.Margin = new System.Windows.Forms.Padding(4);
            this.btnInsertManual.Name = "btnInsertManual";
            this.btnInsertManual.Size = new System.Drawing.Size(100, 28);
            this.btnInsertManual.TabIndex = 3;
            this.btnInsertManual.Text = "Unesi";
            this.btnInsertManual.UseVisualStyleBackColor = true;
            this.btnInsertManual.Click += new System.EventHandler(this.rucni_Click);
            // 
            // tbKolicina
            // 
            this.tbKolicina.Location = new System.Drawing.Point(118, 107);
            this.tbKolicina.Margin = new System.Windows.Forms.Padding(4);
            this.tbKolicina.Name = "tbKolicina";
            this.tbKolicina.Size = new System.Drawing.Size(53, 22);
            this.tbKolicina.TabIndex = 2;
            this.tbKolicina.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // tbVelicina
            // 
            this.tbVelicina.Location = new System.Drawing.Point(118, 71);
            this.tbVelicina.Margin = new System.Windows.Forms.Padding(4);
            this.tbVelicina.Name = "tbVelicina";
            this.tbVelicina.Size = new System.Drawing.Size(53, 22);
            this.tbVelicina.TabIndex = 1;
            this.tbVelicina.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // tbSifra
            // 
            this.tbSifra.Location = new System.Drawing.Point(118, 38);
            this.tbSifra.Margin = new System.Windows.Forms.Padding(4);
            this.tbSifra.Name = "tbSifra";
            this.tbSifra.Size = new System.Drawing.Size(173, 22);
            this.tbSifra.TabIndex = 0;
            this.tbSifra.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            this.tbSifra.Leave += new System.EventHandler(this.tbSifra_Leave);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(40, 116);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 17);
            this.label8.TabIndex = 3;
            this.label8.Text = "Kolicina:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(40, 80);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 17);
            this.label7.TabIndex = 4;
            this.label7.Text = "Velicina:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(62, 48);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 17);
            this.label6.TabIndex = 5;
            this.label6.Text = "Sifra:";
            // 
            // frmRucniUnos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 171);
            this.Controls.Add(this.btnInsertManual);
            this.Controls.Add(this.tbKolicina);
            this.Controls.Add(this.tbVelicina);
            this.Controls.Add(this.tbSifra);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmRucniUnos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Rucni unos sifre";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnInsertManual;
        private System.Windows.Forms.TextBox tbKolicina;
        private System.Windows.Forms.TextBox tbVelicina;
        private System.Windows.Forms.TextBox tbSifra;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
    }
}