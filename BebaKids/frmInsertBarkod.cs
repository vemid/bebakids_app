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
using System.Data.SqlClient;

namespace BebaKids
{
    public partial class frmInsertBarkod : Form
    {
        public frmInsertBarkod()
        {
            InitializeComponent();
        }

        public void btnPrebaciBarkodove_Click(object sender, EventArgs e)
        {

            string cmd1 = " select trim(roba.naz_rob) naziv,trim(ean_kod.bar_kod) bar_kod,trim(ean_kod.sif_rob) sif_rob,trim(ean_kod.sif_ent_rob) sif_ent_rob from ean_kod left join roba on roba.sif_rob = ean_kod.sif_rob ";

            string connString = "Dsn=ifx;uid=informix";

            OdbcConnection conn = new OdbcConnection(connString);

            OdbcDataAdapter adapter = new OdbcDataAdapter(cmd1, conn);

            conn.Open();
            DataTable table = new DataTable();
            table.Clear();
            adapter.Fill(table);

            conn.Close();

            OdbcConnection konekcija = new OdbcConnection(Konekcija.konString);

            var i = 0;

            progressBar1.Minimum = 0;
            progressBar1.Maximum = table.Rows.Count;

            try
            {
                konekcija.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append("truncate table ean_kod2;");
                OdbcCommand komanda = new OdbcCommand(sb.ToString(), konekcija);
                //SqlCommand komanda = new SqlCommand(sb.ToString(), konekcija);
                komanda.ExecuteNonQuery();
                konekcija.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            foreach (DataRow row in table.Rows)
            {
                //MessageBox.Show(row["naziv"].ToString());
                i++;

                String naziv = row["naziv"].ToString();
                String sif_rob = row["sif_rob"].ToString();
                String bar_kod = row["bar_kod"].ToString();
                String sif_ent_rob = row["sif_ent_rob"].ToString();

                StringBuilder sb = new StringBuilder();
                sb.Append("insert into ean_kod2 (naziv,bar_kod,sif_rob,sif_ent_rob) values (" + "?" + "," + "?" + "," + "?" + "," + "?);");
                OdbcCommand komanda = new OdbcCommand(sb.ToString(), konekcija);

                OdbcParameter paramNaziv = new OdbcParameter("@naziv", SqlDbType.Char);
                OdbcParameter paramBarKod = new OdbcParameter("@barKod", SqlDbType.Char);
                OdbcParameter paramSifRob = new OdbcParameter("@sifRob", SqlDbType.Char);
                OdbcParameter paramSifEntRob = new OdbcParameter("@sifEntRob", SqlDbType.Char);

                komanda.Parameters.Add(paramNaziv);
                komanda.Parameters.Add(paramBarKod);
                komanda.Parameters.Add(paramSifRob);
                komanda.Parameters.Add(paramSifEntRob);
                paramNaziv.Value = naziv;
                paramBarKod.Value = bar_kod;
                paramSifRob.Value = sif_rob;
                paramSifEntRob.Value = sif_ent_rob;

                try
                {
                    konekcija.Open();
                    komanda.ExecuteNonQuery();
                    konekcija.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                progressBar1.Value = i;

            }

            progressBar1.Value = 0;

            //Ovde krece unos korisnika za prijavu radnika

            string cmd2 = " select * from radnici ";

            string connStringRadnici = "Dsn=prijava;uid=sa;Pwd=adminabc123";

            OdbcConnection connRadnici = new OdbcConnection(connStringRadnici);

            OdbcDataAdapter adapterRadnici = new OdbcDataAdapter(cmd2, connRadnici);

            conn.Open();
            DataTable tableRadnici = new DataTable();
            tableRadnici.Clear();
            adapterRadnici.Fill(tableRadnici);

            conn.Close();

            OdbcConnection konekcijaRadnici = new OdbcConnection(Konekcija.konString);

            var iRadnici = 0;

            progressBar1.Minimum = 0;
            progressBar1.Maximum = tableRadnici.Rows.Count;

            try
            {
                konekcijaRadnici.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append("truncate table radnici;");
                OdbcCommand komandaRadnici = new OdbcCommand(sb.ToString(), konekcijaRadnici);
                //SqlCommand komanda = new SqlCommand(sb.ToString(), konekcija);
                komandaRadnici.ExecuteNonQuery();
                konekcijaRadnici.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            foreach (DataRow row in tableRadnici.Rows)
            {
                //MessageBox.Show(row["naziv"].ToString());
                iRadnici++;

                int sifra = Convert.ToInt32(row["sifra"].ToString());
                String imePrezime = row["ime_i_prezime"].ToString();
                int objekat = Convert.ToInt32(row["sif_obj_mp"].ToString());
                String status = row["status"].ToString();

                StringBuilder sb = new StringBuilder();
                sb.Append("insert into radnici (sifra,ime_i_prezime,sif_obj_mp,status) values (" + "?" + "," + "?" + "," + "?" + "," + "?);");
                OdbcCommand komanda = new OdbcCommand(sb.ToString(), konekcijaRadnici);

                OdbcParameter paramSifra = new OdbcParameter("@sifra", SqlDbType.Int);
                OdbcParameter paramImePrezime = new OdbcParameter("@imePrezime", SqlDbType.Char);
                OdbcParameter paramObjekat = new OdbcParameter("@objekat", SqlDbType.Int);
                OdbcParameter paramStatus = new OdbcParameter("@status", SqlDbType.Char);

                komanda.Parameters.Add(paramSifra);
                komanda.Parameters.Add(paramImePrezime);
                komanda.Parameters.Add(paramObjekat);
                komanda.Parameters.Add(paramStatus);
                paramSifra.Value = sifra;
                paramImePrezime.Value = imePrezime;
                paramObjekat.Value = objekat;
                paramStatus.Value = status;

                try
                {
                    konekcijaRadnici.Open();
                    komanda.ExecuteNonQuery();
                    konekcijaRadnici.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                progressBar1.Value = iRadnici;

            }
            MessageBox.Show("Uspesno uneti podaci");
            
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            Form1 Form1 = new Form1();
            Form1.Show();
        }
    }
}
