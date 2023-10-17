using System;
using System.Data.Odbc;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BebaKids.Proizvodnja
{
    public partial class ProveraCene : Form
    {
        public ProveraCene()
        {
            InitializeComponent();
            nazivArt.Font = new Font("Times New Roman", 12, FontStyle.Bold);
            sifra.Font = new Font("Times New Roman", 12, FontStyle.Bold);
            this.ActiveControl = tbSifra;

        }

        string sirovinski;
        string sifraArt;

        private void btnProveraCene_Click(object sender, EventArgs e)
        {


            if (String.IsNullOrEmpty(tbSifra.Text))
            {
                MessageBox.Show("Polje sifra artikla mora biti popunjeno");
            }
            else
            {

                if (tbSifra.Text.Length < 13)
                {
                    Classes.Proizvodnja pr = new Classes.Proizvodnja();
                    sifraArt = pr.getSifraArt(tbSifra.Text);

                }
                else
                {
                    sifraArt = tbSifra.Text;
                }
                //string sirovinski = "";
                string connString = "Dsn=ifx;uid=informix";
                OdbcConnection conn = new OdbcConnection(connString);
                StringBuilder komanda = new StringBuilder();
                komanda.Append("select trim(r.naz_rob) naziv,nvl(round(c1.mal_cen,2),0) srb,nvl(round(c2.mal_cen,2),0) cg,nvl(round(c3.mal_cen,2),0) bih,nvl(d.sastav,'') sirovinski from roba r ");
                komanda.Append("left join proiz_cen_st c1 on c1.sif_rob = r.sif_rob and c1.sta_cen_st = 'A' and c1.ozn_cen = '01/140000001' ");
                komanda.Append("left join proiz_cen_st c2 on c2.sif_rob = r.sif_rob and c2.sta_cen_st = 'A' and c2.ozn_cen = '03/160000001' ");
                komanda.Append("left join proiz_cen_st c3 on c3.sif_rob = r.sif_rob and c3.sta_cen_st = 'A' and c3.ozn_cen = '60/160000001' ");
                komanda.Append("left join deklaracija d on d.sif_rob = r.sif_rob ");
                komanda.Append("where r.sif_rob = '" + sifraArt + "' ");
                OdbcCommand komandaGetCene = new OdbcCommand(komanda.ToString(), conn);
                conn.Open();
                OdbcDataReader dr = komandaGetCene.ExecuteReader();

                if (dr.Read())
                {
                    sifra.Text = sifraArt;
                    nazivArt.Text = dr.GetString(0).ToString();
                    gbCene.Visible = true;
                    rthSirovinski.Visible = true;
                    btSirovinski.Visible = true;
                    tbSrb.Text = dr.GetString(1).ToString();
                    tbCg.Text = dr.GetString(2).ToString();
                    tbBih.Text = dr.GetString(3).ToString();
                    sirovinski = rthSirovinski.Text = dr.GetString(4).ToString();
                }
                else
                {
                    MessageBox.Show("Ne postoji takva sifra", "Obavestenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    gbCene.Visible = false;
                    rthSirovinski.Visible = false;
                    btSirovinski.Visible = false;
                }
                if (sirovinski.Length > 0)
                    btSirovinski.Text = "Izmeni Sirovinski";
                else btSirovinski.Text = "Dodaj Sirovinski";

                conn.Close();
                tbSifra.Clear();
                this.ActiveControl = tbSifra;
            }
        }

        private void btSirovinski_Click(object sender, EventArgs e)
        {
            string textSirovinski = rthSirovinski.Text.ToString();

            if (sirovinski.Length != textSirovinski.Length)
            {
                Classes.Proizvodnja pr = new Classes.Proizvodnja();
                pr.insertSirovinski(sifra.Text, textSirovinski);
                MessageBox.Show("Uspeno uneta sirovinski sastav", "Obavestenje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                gbCene.Visible = false;
                rthSirovinski.Visible = false;
                btSirovinski.Visible = false;
            }
        }

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnProveraCene.PerformClick();
                // these last two lines will stop the beep sound
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }

        private void ProveraCene_Load(object sender, EventArgs e)
        {

        }
    }
}
