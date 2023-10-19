namespace BebaKids.Loyalti
{
    partial class addVoucher
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
            this.tbDateIskoristivost = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.tbVoucher = new System.Windows.Forms.MaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tbValue = new System.Windows.Forms.TextBox();
            this.btnAddVoucer = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(12, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(201, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Kreiranje novog vaučera";
            // 
            // tbDateIskoristivost
            // 
            this.tbDateIskoristivost.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.tbDateIskoristivost.Location = new System.Drawing.Point(124, 111);
            this.tbDateIskoristivost.MaxDate = new System.DateTime(2030, 12, 31, 0, 0, 0, 0);
            this.tbDateIskoristivost.MinDate = new System.DateTime(1950, 1, 1, 0, 0, 0, 0);
            this.tbDateIskoristivost.Name = "tbDateIskoristivost";
            this.tbDateIskoristivost.Size = new System.Drawing.Size(170, 20);
            this.tbDateIskoristivost.TabIndex = 29;
            this.tbDateIskoristivost.Value = new System.DateTime(2022, 12, 1, 0, 0, 0, 0);
            this.tbDateIskoristivost.ValueChanged += new System.EventHandler(this.DatePicker_Changed);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label6.Location = new System.Drawing.Point(13, 113);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(92, 16);
            this.label6.TabIndex = 28;
            this.label6.Text = "Iskoristivo do :";
            // 
            // tbVoucher
            // 
            this.tbVoucher.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbVoucher.Location = new System.Drawing.Point(124, 73);
            this.tbVoucher.Name = "tbVoucher";
            this.tbVoucher.Size = new System.Drawing.Size(170, 22);
            this.tbVoucher.TabIndex = 30;
            this.tbVoucher.Leave += new System.EventHandler(this.tbCard_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(14, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 16);
            this.label2.TabIndex = 31;
            this.label2.Text = "Broj Vaucera :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label9.Location = new System.Drawing.Point(14, 153);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(44, 16);
            this.label9.TabIndex = 32;
            this.label9.Text = "Iznos :";
            // 
            // tbValue
            // 
            this.tbValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbValue.Location = new System.Drawing.Point(125, 147);
            this.tbValue.Name = "tbValue";
            this.tbValue.Size = new System.Drawing.Size(131, 22);
            this.tbValue.TabIndex = 33;
            // 
            // btnAddVoucer
            // 
            this.btnAddVoucer.Location = new System.Drawing.Point(329, 14);
            this.btnAddVoucer.Name = "btnAddVoucer";
            this.btnAddVoucer.Size = new System.Drawing.Size(92, 32);
            this.btnAddVoucer.TabIndex = 34;
            this.btnAddVoucer.Text = "Sacuvaj";
            this.btnAddVoucer.UseVisualStyleBackColor = true;
            this.btnAddVoucer.Visible = false;
            this.btnAddVoucer.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // addVoucher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 215);
            this.Controls.Add(this.btnAddVoucer);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.tbValue);
            this.Controls.Add(this.tbVoucher);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbDateIskoristivost);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label1);
            this.Name = "addVoucher";
            this.Text = "addVoucher";
            this.Load += new System.EventHandler(this.addNewVoucher_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker tbDateIskoristivost;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.MaskedTextBox tbVoucher;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbValue;
        private System.Windows.Forms.Button btnAddVoucer;
    }
}