namespace BebaKids.PopisMp
{
    partial class PopisMpUnos
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PopisMpUnos));
            this.datumDokumenta = new System.Windows.Forms.DateTimePicker();
            this.btnNoviPopis = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbBarkod = new System.Windows.Forms.TextBox();
            this.lbNaziv = new System.Windows.Forms.Label();
            this.lbVelicina = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lbSifra = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Akcija = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btnPrenesi = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // datumDokumenta
            // 
            this.datumDokumenta.Location = new System.Drawing.Point(158, 12);
            this.datumDokumenta.Name = "datumDokumenta";
            this.datumDokumenta.Size = new System.Drawing.Size(145, 20);
            this.datumDokumenta.TabIndex = 0;
            // 
            // btnNoviPopis
            // 
            this.btnNoviPopis.Location = new System.Drawing.Point(22, 9);
            this.btnNoviPopis.Name = "btnNoviPopis";
            this.btnNoviPopis.Size = new System.Drawing.Size(130, 23);
            this.btnNoviPopis.TabIndex = 1;
            this.btnNoviPopis.Text = "Novi Dokument";
            this.btnNoviPopis.UseVisualStyleBackColor = true;
            this.btnNoviPopis.Click += new System.EventHandler(this.btnNoviPopis_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(22, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Oznaka dokumenta :";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBox1.Location = new System.Drawing.Point(158, 47);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(145, 22);
            this.textBox1.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(319, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Objekat :";
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBox2.Location = new System.Drawing.Point(386, 9);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(42, 22);
            this.textBox2.TabIndex = 3;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(94, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Barkod :";
            // 
            // tbBarkod
            // 
            this.tbBarkod.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbBarkod.Location = new System.Drawing.Point(158, 86);
            this.tbBarkod.Name = "tbBarkod";
            this.tbBarkod.Size = new System.Drawing.Size(145, 22);
            this.tbBarkod.TabIndex = 3;
            this.tbBarkod.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxTest_KeyDown);
            // 
            // lbNaziv
            // 
            this.lbNaziv.AutoSize = true;
            this.lbNaziv.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.lbNaziv.Location = new System.Drawing.Point(372, 101);
            this.lbNaziv.Name = "lbNaziv";
            this.lbNaziv.Size = new System.Drawing.Size(37, 13);
            this.lbNaziv.TabIndex = 4;
            this.lbNaziv.Text = "Naziv:";
            // 
            // lbVelicina
            // 
            this.lbVelicina.AutoSize = true;
            this.lbVelicina.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.lbVelicina.Location = new System.Drawing.Point(372, 78);
            this.lbVelicina.Name = "lbVelicina";
            this.lbVelicina.Size = new System.Drawing.Size(47, 13);
            this.lbVelicina.TabIndex = 5;
            this.lbVelicina.Text = "Velicina:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(339, 101);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Naziv:";
            // 
            // lbSifra
            // 
            this.lbSifra.AutoSize = true;
            this.lbSifra.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.lbSifra.Location = new System.Drawing.Point(372, 52);
            this.lbSifra.Name = "lbSifra";
            this.lbSifra.Size = new System.Drawing.Size(31, 13);
            this.lbSifra.TabIndex = 7;
            this.lbSifra.Text = "Sifra:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(328, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Velicina:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(345, 52);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Sifra:";
            // 
            // dataGridView1
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightSkyBlue;
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.Azure;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Akcija});
            this.dataGridView1.Location = new System.Drawing.Point(25, 150);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.PowderBlue;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(661, 477);
            this.dataGridView1.TabIndex = 10;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // Akcija
            // 
            this.Akcija.HeaderText = "Akcija";
            this.Akcija.Name = "Akcija";
            this.Akcija.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Akcija.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Akcija.Text = "Obrsi";
            this.Akcija.ToolTipText = "Obrisi";
            this.Akcija.UseColumnTextForButtonValue = true;
            // 
            // btnPrenesi
            // 
            this.btnPrenesi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnPrenesi.Location = new System.Drawing.Point(552, 15);
            this.btnPrenesi.Margin = new System.Windows.Forms.Padding(2);
            this.btnPrenesi.Name = "btnPrenesi";
            this.btnPrenesi.Size = new System.Drawing.Size(65, 24);
            this.btnPrenesi.TabIndex = 12;
            this.btnPrenesi.Text = "Prenesi";
            this.btnPrenesi.UseVisualStyleBackColor = true;
            this.btnPrenesi.Visible = false;
            this.btnPrenesi.Click += new System.EventHandler(this.btnPrenesi_Click);
            // 
            // btnExport
            // 
            this.btnExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnExport.Location = new System.Drawing.Point(633, 15);
            this.btnExport.Margin = new System.Windows.Forms.Padding(2);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(65, 24);
            this.btnExport.TabIndex = 12;
            this.btnExport.Text = "Exportuj";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Visible = false;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(518, 53);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(180, 23);
            this.progressBar1.TabIndex = 14;
            this.progressBar1.Visible = false;
            // 
            // PopisMpUnos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(706, 636);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnPrenesi);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.lbNaziv);
            this.Controls.Add(this.lbVelicina);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lbSifra);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.tbBarkod);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnNoviPopis);
            this.Controls.Add(this.datumDokumenta);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PopisMpUnos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Unos popisa u MP objektu";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.PopisMpUnos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker datumDokumenta;
        private System.Windows.Forms.Button btnNoviPopis;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbBarkod;
        private System.Windows.Forms.Label lbNaziv;
        private System.Windows.Forms.Label lbVelicina;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbSifra;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewButtonColumn Akcija;
        private System.Windows.Forms.Button btnPrenesi;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}