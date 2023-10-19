namespace BebaKids.Loyalti
{
    partial class DodavanjeUceniska
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
            this.tbCard = new System.Windows.Forms.MaskedTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbLastName = new System.Windows.Forms.TextBox();
            this.tbName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tbGender = new System.Windows.Forms.TextBox();
            this.tbPhone = new System.Windows.Forms.TextBox();
            this.tbId = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbArticleNumber = new System.Windows.Forms.TextBox();
            this.btnAdd = new MaterialSkin.Controls.MaterialFlatButton();
            this.btnChangePhone = new MaterialSkin.Controls.MaterialFlatButton();
            this.SuspendLayout();
            // 
            // tbCard
            // 
            this.tbCard.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbCard.Location = new System.Drawing.Point(120, 66);
            this.tbCard.Mask = "000000";
            this.tbCard.Name = "tbCard";
            this.tbCard.Size = new System.Drawing.Size(66, 22);
            this.tbCard.TabIndex = 34;
            this.tbCard.Leave += new System.EventHandler(this.tbCard_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(10, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 16);
            this.label4.TabIndex = 39;
            this.label4.Text = "Ime :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(10, 128);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 16);
            this.label3.TabIndex = 38;
            this.label3.Text = "Prezime :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(10, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 16);
            this.label2.TabIndex = 37;
            this.label2.Text = "Broj Kartice :";
            // 
            // tbLastName
            // 
            this.tbLastName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbLastName.Location = new System.Drawing.Point(120, 122);
            this.tbLastName.Name = "tbLastName";
            this.tbLastName.Size = new System.Drawing.Size(131, 22);
            this.tbLastName.TabIndex = 36;
            // 
            // tbName
            // 
            this.tbName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbName.Location = new System.Drawing.Point(120, 94);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(131, 22);
            this.tbName.TabIndex = 35;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(8, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(256, 20);
            this.label1.TabIndex = 33;
            this.label1.Text = "Kreiranje nove prijave ucesnika";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label8.Location = new System.Drawing.Point(8, 151);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 16);
            this.label8.TabIndex = 41;
            this.label8.Text = "Telefon :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label7.Location = new System.Drawing.Point(8, 179);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(33, 16);
            this.label7.TabIndex = 42;
            this.label7.Text = "Pol :";
            // 
            // tbGender
            // 
            this.tbGender.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbGender.Location = new System.Drawing.Point(120, 173);
            this.tbGender.Name = "tbGender";
            this.tbGender.Size = new System.Drawing.Size(131, 22);
            this.tbGender.TabIndex = 44;
            // 
            // tbPhone
            // 
            this.tbPhone.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbPhone.Location = new System.Drawing.Point(120, 145);
            this.tbPhone.Name = "tbPhone";
            this.tbPhone.Size = new System.Drawing.Size(131, 22);
            this.tbPhone.TabIndex = 43;
            // 
            // tbId
            // 
            this.tbId.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbId.Location = new System.Drawing.Point(264, 173);
            this.tbId.Name = "tbId";
            this.tbId.Size = new System.Drawing.Size(131, 22);
            this.tbId.TabIndex = 46;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label5.Location = new System.Drawing.Point(8, 207);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 16);
            this.label5.TabIndex = 47;
            this.label5.Text = "Broj Artikala :";
            // 
            // tbArticleNumber
            // 
            this.tbArticleNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbArticleNumber.Location = new System.Drawing.Point(120, 201);
            this.tbArticleNumber.Name = "tbArticleNumber";
            this.tbArticleNumber.Size = new System.Drawing.Size(131, 22);
            this.tbArticleNumber.TabIndex = 48;
            // 
            // btnAdd
            // 
            this.btnAdd.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            //this.btnAdd.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnAdd.Depth = 0;
            //this.btnAdd.HighEmphasis = true;
            //this.btnAdd.Icon = null;
            this.btnAdd.Image = global::BebaKids.Properties.Resources.print;
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdd.Location = new System.Drawing.Point(120, 252);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnAdd.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnAdd.Name = "btnAdd";
            //this.btnAdd.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnAdd.Size = new System.Drawing.Size(108, 36);
            this.btnAdd.TabIndex = 49;
            this.btnAdd.Text = "      Sacuvaj       ";
            this.btnAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //this.btnAdd.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            //this.btnAdd.UseAccentColor = false;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Visible = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnChangePhone
            // 
            this.btnChangePhone.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            //this.btnChangePhone.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnChangePhone.Depth = 0;
            //this.btnChangePhone.HighEmphasis = true;
            //this.btnChangePhone.Icon = null;
            this.btnChangePhone.Image = global::BebaKids.Properties.Resources.print;
            this.btnChangePhone.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnChangePhone.Location = new System.Drawing.Point(264, 118);
            this.btnChangePhone.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnChangePhone.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnChangePhone.Name = "btnChangePhone";
            //this.btnChangePhone.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnChangePhone.Size = new System.Drawing.Size(153, 36);
            this.btnChangePhone.TabIndex = 50;
            this.btnChangePhone.Text = "Promeni Telefon";
            this.btnChangePhone.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //this.btnChangePhone.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            //this.btnChangePhone.UseAccentColor = false;
            this.btnChangePhone.UseVisualStyleBackColor = true;
            this.btnChangePhone.Click += new System.EventHandler(this.button1_Click);
            // 
            // DodavanjeUceniska
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 313);
            this.Controls.Add(this.btnChangePhone);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbArticleNumber);
            this.Controls.Add(this.tbId);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tbGender);
            this.Controls.Add(this.tbPhone);
            this.Controls.Add(this.tbCard);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbLastName);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "DodavanjeUceniska";
            this.Text = "addNewParticipations";
            this.Load += new System.EventHandler(this.addNewParticipations_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MaskedTextBox tbCard;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbLastName;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbGender;
        private System.Windows.Forms.TextBox tbPhone;
        private System.Windows.Forms.TextBox tbId;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbArticleNumber;
        private MaterialSkin.Controls.MaterialFlatButton btnAdd;
        private MaterialSkin.Controls.MaterialFlatButton btnChangePhone;
    }
}