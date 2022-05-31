using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BebaKids.Fransiza
{
    public partial class PregledDnevneProvizije : Form
    {
        public void disableAll(bool vrsta)
        {
            foreach (TextBox tb in this.tableLayoutPanel1.Controls.OfType<TextBox>())
            {
                tb.ReadOnly = vrsta;
                tb.BackColor = Color.AntiqueWhite;
            }
            foreach (TextBox tb in this.tableLayoutPanel2.Controls.OfType<TextBox>())
            {
                tb.ReadOnly = vrsta;
                tb.BackColor = Color.AntiqueWhite;
            }
            foreach (TextBox tb in this.tableLayoutPanel3.Controls.OfType<TextBox>())
            {
                tb.ReadOnly = vrsta;
                tb.BackColor = Color.AntiqueWhite;
            }
            foreach (TextBox tb in this.Controls.OfType<TextBox>())
            {
                tb.ReadOnly = vrsta;
                tb.BackColor = Color.AntiqueWhite;
            }

        }

        public PregledDnevneProvizije()
        {
            InitializeComponent();

            disableAll(true);
            DateTime thisDay = DateTime.Now;
            string dateFormat = "dd";
            int date = Convert.ToInt32(thisDay.ToString(dateFormat));
            if (date < 6)
            {
                dateFrom.MinDate = thisDay.AddDays(-67);
                dateTo.MaxDate = thisDay.AddDays(30);
            }
            else
            {
                dateFrom.MinDate = thisDay.AddDays(date*(-1));
                dateTo.MaxDate = thisDay.AddDays(30);
            }
            //MessageBox.Show(date.ToString());
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            Form1 Form1 = new Form1();
            Form1.Show();
        }

        private void btnPrikazi_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            DateTime date = dateFrom.Value.Date;
            DateTime dateDo = dateTo.Value.Date;

            var MyIni = new IniFile(@"C:\bkapps\config.ini");
            var Sifraobjekat = MyIni.Read("sif_obj_mp", "ProveraDokumenta");
            var trziste = MyIni.Read("trziste", "ProveraDokumenta");
            int objekat = Convert.ToInt32(Sifraobjekat.ToString());

            string tTrziste;

            if (trziste == "CG")
            {
                tTrziste = "CG";
            }
            else if (trziste == "BIH")
            {
                tTrziste = "BIH";
            }
            else tTrziste = "SRB";

                Save save = new Save();
            DataTable report = save.reportProvizija(Sifraobjekat, date.ToShortDateString(), dateDo.ToShortDateString(), tTrziste);

            double bkLoyalti = 0;
            double bkSnizeno = 0;
            double bkNeSnizeno = 0;
            double trLoyalti = 0;
            double trSnizeno = 0;
            double trNeSnizeno = 0;
            DataRow dbkSnizeno = report.Select("vrsta = 'BebaKids' AND grupa = 'Snizen'").FirstOrDefault();
            DataRow dbkNeSnizeno = report.Select("vrsta = 'BebaKids' AND grupa = 'Ne Snizen'").FirstOrDefault();
            DataRow dbkLoyalti = report.Select("vrsta = 'BebaKids' AND grupa = 'Loyalti'").FirstOrDefault();
            DataRow dtrSnizeno = report.Select("vrsta = 'Trgovacka' AND grupa = 'Snizen'").FirstOrDefault();
            DataRow dtrNeSnizeno = report.Select("vrsta = 'Trgovacka' AND grupa = 'Ne Snizen'").FirstOrDefault();
            DataRow dtrLoyalti = report.Select("vrsta = 'Trgovacka' AND grupa = 'Loyalti'").FirstOrDefault();

            //string ttbkSnizeno = dbkSnizeno["vred_pro"].ToString();


            if (dbkSnizeno != null)
                bkSnizeno = Convert.ToDouble(dbkSnizeno["vred_pro"].ToString());
            //bkSnizeno = ConvertToInt32(dbkSnizeno["vred_pro"], 0);
            if (dbkNeSnizeno != null)
                bkNeSnizeno = Convert.ToDouble(dbkNeSnizeno["vred_pro"].ToString());
                //bkNeSnizeno = ConvertToInt32(dbkNeSnizeno["vred_pro"], 0);
            if (dbkLoyalti != null)
                bkLoyalti = Convert.ToDouble(dbkLoyalti["vred_pro"].ToString());
            //bkLoyalti = ConvertToInt32(dbkLoyalti["vred_pro"], 0);
            if (dtrSnizeno != null)
                trSnizeno = Convert.ToDouble(dtrSnizeno["vred_pro"].ToString());
            //trSnizeno = ConvertToInt32(dtrSnizeno["vred_pro"], 0);
            if (dtrNeSnizeno != null)
                trNeSnizeno = Convert.ToDouble(dtrNeSnizeno["vred_pro"].ToString());
            //trNeSnizeno = ConvertToInt32(dtrNeSnizeno["vred_pro"], 0);
            if (dtrLoyalti != null)
                trLoyalti = Convert.ToDouble(dtrLoyalti["vred_pro"].ToString());
            //trLoyalti = ConvertToInt32(dtrLoyalti["vred_pro"], 0);


            tbBkNs.Text = (bkNeSnizeno + bkLoyalti).ToString();
            tbBkS.Text = bkSnizeno.ToString();
            tbTrNs.Text = (trLoyalti + trNeSnizeno).ToString();
            tbTrS.Text = trSnizeno.ToString();

            tbTotalNs.Text = (bkNeSnizeno + bkLoyalti + trLoyalti + trNeSnizeno).ToString();
            tbTotalS.Text = (trSnizeno + bkSnizeno).ToString();
            tbTotal.Text = (Convert.ToDouble(tbTotalNs.Text.ToString()) + (Convert.ToDouble(tbTotalS.Text.ToString()))).ToString();

            double pdv ;
            double pdv2;
            double neSnizeno ;
            double snizeno ;

            if (trziste == "CG")
            {
                pdv = 0.8265;
                pdv2 = 1.21;
                neSnizeno = 0.36;
                snizeno = 0.26;

            }
            else if (trziste == "BIH")
            {
                pdv = 0.8547;
                pdv2 = 1.17;
                neSnizeno = 0.36;
                snizeno = 0.26;

            }
            else
            {
                pdv = 0.833333333333;
                pdv2 = 1.2;
                neSnizeno = 0.33;
                snizeno = 0.23;
            }

            
            double pdvNs = Math.Round(Convert.ToDouble(tbBkNs.Text) * pdv, 2)+ Math.Round(Convert.ToDouble(tbTrNs.Text) * pdv, 2);
            double pdvS = Math.Round(Convert.ToDouble(tbBkS.Text) * pdv, 2)+ Math.Round(Convert.ToDouble(tbTrS.Text) * pdv, 2);
            double pdvBkNs = Math.Round(Convert.ToDouble(tbBkNs.Text) * pdv, 2);
            double pdvBkS = Math.Round(Convert.ToDouble(tbBkS.Text) * pdv, 2);
            double pdvTrNs = Math.Round(Convert.ToDouble(tbTrNs.Text) * pdv, 2);
            double pdvTrS = Math.Round(Convert.ToDouble(tbTrS.Text) * pdv, 2);

            double proBkNs = Math.Round(pdvBkNs * neSnizeno,2);
            double proBkS = Math.Round(pdvBkS * snizeno,2);
            double proTrNs = Math.Round(pdvTrNs * neSnizeno,2);
            double proTrS = Math.Round(pdvTrS * snizeno,2);


            tbpdvBkNs.Text = pdvBkNs.ToString();
            tbpdvBkS.Text = pdvBkS.ToString();
            tbpdvTrNs.Text = pdvTrNs.ToString();
            tbpdvTrS.Text = pdvTrS.ToString();
            double vrednost = pdvNs + pdvS;

            tbpdvTotalNs.Text = pdvNs.ToString();
            tbpdvTotalS.Text = pdvS.ToString();
            tbpdvTotal.Text = (pdvNs + pdvS).ToString();

            tbProBkNs.Text = proBkNs.ToString();
            tbProBkS.Text = proBkS.ToString();
            tbProTrNs.Text = proTrNs.ToString();
            tbProTrS.Text = proTrS.ToString();
            tbProTotalNs.Text = (proBkNs + proTrNs).ToString();
            tbProTotalS.Text = (proBkS + proTrS).ToString();
            double vrednostPro = proBkNs + proBkS + proTrNs + proTrS;

            tbProTotal.Text = vrednostPro.ToString();

            double rabat;
            rabat = 0;
            /*
            if (vrednost < 800000)
            {
                rabat = 0;
            }
            else if (vrednost > 800000 && vrednost <= 1000000)
            {
                rabat = 0.02;
            }  
            else if (vrednost > 1000000 && vrednost <= 1500000)
            {
                rabat = 0.03;
            }
            else if (vrednost > 1500000 && vrednost <= 2000000)
            {
                rabat = 0.04;
            }
            else
            {
                rabat = 0.05;
            }
            */
            double vreRab = Math.Round((rabat * vrednost), 2);
            double suma = vreRab + vrednostPro;
            double sumaPdv = Math.Round(suma * pdv2,2);

            tbRab.Text = String.Format("{0:P2}.", rabat);
            tbVreRab.Text = vreRab.ToString();
            tbSuma.Text = suma.ToString();
            tbSumaPdv.Text = sumaPdv.ToString();
            Cursor.Current = Cursors.Default;
        }
        public static int ConvertToInt32(object value, int defaultValue)
        {
            if (value == null)
                return defaultValue;
            return Convert.ToInt32(value);


        }

        private void label11_Click(object sender, EventArgs e)
        {

        }
    }


}
