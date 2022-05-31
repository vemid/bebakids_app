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

namespace BebaKids
{
    public partial class frmRucniUnos : Form
    {
        public frmRucniUnos()
        {
            InitializeComponent();
        }
        public static string tsifra = "";
        public static string tvelicina = "";
        public static int tkolicina = 0;

        private void rucni_Click(object sender, EventArgs e)
        {
            tsifra = tbSifra.Text.ToString();
            tvelicina = tbVelicina.Text.ToString();
            tkolicina = Convert.ToInt32(tbKolicina.Text);
        }

        private void tbSifra_Leave(object sender, EventArgs e)
        {
            OdbcConnection konekcija = new OdbcConnection(Konekcija.konString);
            string sifra = tbSifra.Text.ToString();
            OdbcCommand komanda = new OdbcCommand("select * from ean_kod2 where sif_rob = '"+sifra+"'", konekcija);
            
            try
            {
                konekcija.Open();
                OdbcDataReader dr = komanda.ExecuteReader();
                

                if (dr.Read())
                {
                    //SendKeys.Send("{TAB}");
                }
                else
                {
                    MessageBox.Show("Ne postoji sifra");
                    tbSifra.Clear();
                    tbSifra.Focus();
                }
                
            
                konekcija.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //return ex.Message.ToString;
            }


            
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            //
            // Detect the KeyEventArg's key enumerated constant.
            //
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }
    }
}
