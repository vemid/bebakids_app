using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BebaKids.Loyalti
{
    public partial class addNewCardCG : Form
    {
        public addNewCardCG()
        {
            InitializeComponent();
            //tbDate.CustomFormat = " ";
            tbDate.Format = DateTimePickerFormat.Custom;
            tbCard.Focus();
        }
        bool vrednost = false;

        private void DatePicker_Changed(object sender, EventArgs e)
        {
            tbDate.CustomFormat = "dd.MM.yyyy";
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g;

            g = e.Graphics;
            int width = ClientRectangle.Width;
            int height = ClientRectangle.Height;
            Color color = System.Drawing.ColorTranslator.FromHtml("#3fb0ac");
            Pen myPen = new Pen(color);
            myPen.Width = 5;
            //g.DrawLine(myPen, 50, 300, 50, 300);fgh ffghfg
            Point tacka12 = new Point(30, 50);
            Point tacka22 = new Point(width - 30, 50);
            g.DrawLine(myPen, tacka12, tacka22);

            Pen litlePen = new Pen(color);
            litlePen.Width = 5;
            Point tackal1 = new Point(30, height - 20);
            Point tackal2 = new Point(width - 30, height - 20);
            g.DrawLine(litlePen, tackal1, tackal2);
        }

        private void tbCard_Leave(object sender, EventArgs e)
        {

            Save cuvanje = new Save();
            var provera = cuvanje.checkLoyaltiCardCG(tbCard.Text.ToString());
            if (provera == true)
            {
                tbAddress.Enabled = true;
                tbDate.Enabled = true;
                tbEmail.Enabled = true;
                tbLastName.Enabled = true;
                tbName.Enabled = true;
                tbPhone.Enabled = true;
                tbPostalCode.Enabled = true;
                btnAdd.Visible = true;
            }
            else
            {
                MessageBox.Show("Kartica nije validna, ili je vec otvorena !", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbAddress.Enabled = false;
                tbDate.Enabled = false;
                tbEmail.Enabled = false;
                tbLastName.Enabled = false;
                tbName.Enabled = false;
                tbPhone.Enabled = false;
                tbPostalCode.Enabled = false;
            }
        }

        public void clearAll()
        {
            foreach (TextBox tb in this.Controls.OfType<TextBox>())
            {
                tb.Clear();
            }

            foreach (MaskedTextBox tb in this.Controls.OfType<MaskedTextBox>())
            {
                tb.Clear();
            }

            foreach (CheckBox tb in this.Controls.OfType<CheckBox>())
            {
                tb.Checked = false;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbCard.Text.ToString()) || string.IsNullOrEmpty(tbName.Text.ToString()) || string.IsNullOrEmpty(tbLastName.Text.ToString()))
            {
                MessageBox.Show("Morate popuniti obavezna polja !", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                var MyIni = new IniFile(@"C:\bkapps\config.ini");
                var objekat = MyIni.Read("sif_obj_mp", "ProveraDokumenta");

                DateTime thisDay = DateTime.Now;
                string dateFormat = "yyyy-MM-dd";
                string timeFormat = "HH:mm:ss";

                string card = tbCard.Text.ToString();
                string name = tbName.Text.ToString();
                string lastName = tbLastName.Text.ToString();
                string totalName = name + " " + lastName;
                string pol;
                if (cbMale.Checked)
                {
                    pol = "M";
                }
                else if (cbFemale.Checked)
                {
                    pol = "Z";
                }
                else pol = "";
                DateTime d = tbDate.Value.Date;
                //d.ToString("yyyy-MM-dd")
                string address = tbAddress.Text.ToString();
                string postalCode = tbPostalCode.Text.ToString();
                string email = tbEmail.Text.ToString();
                string phone = tbPhone.Text.ToString();

                string connString = "Dsn=ifx;uid=informix";
                OdbcConnection conn = new OdbcConnection(connString);
                int returnInformation;

                OdbcCommand komandaPosPar = new OdbcCommand("update pos_par set sif_del = 'M', naz_par = '" + totalName + "', uli_bro = '" + address + "', telefon = '" + phone + "', e_mail = '" + email + "', sta_pos_par = '1' where sif_par = '" + card + "'", conn);

                MySqlConnection mysql = new MySqlConnection(MysqlB2B.myConnectionString);
                MySqlCommand command = mysql.CreateCommand();
                command.CommandText = "insert into loyalti (sif_obj_mp, datum, vreme, card,first_name,last_name,address,ptt,phone,e_mail,date_of_birth,gender) VALUES ('" + objekat + "','" + thisDay.ToString("yyyy-MM-dd") + "','" + thisDay.ToString(timeFormat) + "','" + card + "','" + name + "','" + lastName + "','" + address + "','" + postalCode + "','" + phone + "','" + email + "','" + d.ToString("yyyy-MM-dd") + "','" + pol + "')"; ;

                conn.Open();
                int posPar = komandaPosPar.ExecuteNonQuery();
                conn.Close();
                if (posPar == 1)
                {                  
                        mysql.Open();
                        command.ExecuteNonQuery();
                        mysql.Close();
                        MessageBox.Show("Uspesno otvorena kartica !", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clearAll();

                }
                else
                {
                    MessageBox.Show("Niste otvorili karticu, pokusajte ponovo !", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                    Form1 frm = new Form1();
                    frm.Show();
                }

            }
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            Form1 Form1 = new Form1();
            Form1.Show();
        }
    }
}
