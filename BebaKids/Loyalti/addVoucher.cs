using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Odbc;
using MySql.Data.MySqlClient;

namespace BebaKids.Loyalti
{
    public partial class addVoucher : Form
    {
        public addVoucher()
        {
            InitializeComponent();
            tbDateIskoristivost.CustomFormat = " ";
            tbDateIskoristivost.Format = DateTimePickerFormat.Custom;
            tbDateIskoristivost.Focus();
        }
        bool vrednost = false;

        private void DatePicker_Changed(object sender, EventArgs e)
        {
            tbDateIskoristivost.CustomFormat = "dd.MM.yyyy";
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

        private void tbCard_Leave(object sender, EventArgs e)
        {
            var MyIni = new IniFile(@"C:\bkapps\config.ini");
            var objekat = MyIni.Read("sif_obj_mp", "ProveraDokumenta");
            var baza = MyIni.Read("baza", "ProveraDokumenta");
            var tCard = tbVoucher.Text.ToString();

            Save cuvanje = new Save();
            var provera = cuvanje.checkVoucher(tCard, baza.ToString());
            if (provera == true)
            {
                tbDateIskoristivost.Enabled = true;
                tbValue.Enabled = true;
                tbValue.Enabled = true;
                tbVoucher.Enabled = true;
                btnAddVoucer.Visible = true;
            }
            else
            {
                MessageBox.Show("Kartica nije validna, ili je vec otvorena !", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbDateIskoristivost.Enabled = false;
                tbValue.Enabled = false;
                tbValue.Enabled = false;
                tbVoucher.Enabled = false;
                btnAddVoucer.Visible = false;
            }


        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbVoucher.Text.ToString()))
            {
                MessageBox.Show("Morate popuniti obavezna polja !", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                var MyIni = new IniFile(@"C:\bkapps\config.ini");
                var objekat = MyIni.Read("sif_obj_mp", "ProveraDokumenta");
                var baza = MyIni.Read("baza", "ProveraDokumenta");

                DateTime thisDay = DateTime.Now;
                string dateFormat = "yyyy-MM-dd";
                string timeFormat = "HH:mm:ss";

                string card = tbVoucher.Text.ToString();
                string value = tbValue.Text.ToString();

                DateTime d = tbDateIskoristivost.Value.Date;

                string connString = "Dsn=ifx;uid=informix";
                OdbcConnection conn = new OdbcConnection(connString);
                int returnInformation;

                OdbcCommand komandaPlatnaKartica = new OdbcCommand("update " + baza + ":platna_kartica set sta_pla_kar = 'A',iznos_vaucer ='" + value + "',rok_vaz = '" + d.ToString("dd.MM.yyyy") + "' where ozn_pla_kar = '" + card + "'", conn);

                conn.Open();
                int posPar = komandaPlatnaKartica.ExecuteNonQuery();
                conn.Close();
                if (posPar == 1)
                {
                    MessageBox.Show("Uspesno otvorena kartica !", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clearAll();
                }
                else
                {
                    MessageBox.Show("Niste otvorili karticu, pokusajte pono !", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void addNewVoucher_Load(object sender, EventArgs e)
        {
            tbDateIskoristivost.Value = DateTime.Now;
        }

        protected void OnValueChanged(EventArgs eventargs)
        {
            DateTime d = tbDateIskoristivost.Value.Date;
            tbDateIskoristivost.Value = d;
        }
    }
}
