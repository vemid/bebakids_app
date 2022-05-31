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

namespace BebaKids.Proizvodnja
{
    public partial class PrenosSifara : Form
    {
        public PrenosSifara()
        {
            InitializeComponent();
            lbNazivKolekcije.Text = "";           
        }

        private void tbOznakaKolekcije_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbKolekcija.Text))
            {
                MessageBox.Show("Niste uneli oznaku kolekcije", "Obavestenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string oznaka = tbKolekcija.Text.ToString();
                Classes.Proizvodnja provera = new Classes.Proizvodnja();
                lbNazivKolekcije.Text = provera.getNazivKolekcije(oznaka);
            }
        }

        private void tbKurs_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbEurKolekcije.Text))
            {
                MessageBox.Show("Niste uneli kurs eura kolekcije", "Obavestenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (tbEurKolekcije.Text.ToString().Contains(",") == true)
                {
                    MessageBox.Show("Koleginice \nmatematicka decimala se pise sa tackom \na ne zarezom", "Obavestenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            Form1 Form1 = new Form1();
            Form1.Show();
        }

        private void btnPrebaciSifre_Click(object sender, EventArgs e)
        {
            string oznaka = tbKolekcija.Text.ToString();
            float kurs = float.Parse(tbEurKolekcije.Text);
            float marza = float.Parse(tbMarza.Text);
            StringBuilder cmd = new StringBuilder();
            cmd.Append("select r.sif_rob,trim(r.naz_rob) naz_rob,trim(r.kla_ozn) kla_ozn,trim(get_kolekcija(r.sif_rob)) kolekcija,trim(rk.kla_ozn) kla_ozn_07 from roba r ");
            cmd.Append("left join roba_klas_robe rk on rk.sif_rob = r.sif_rob and rk.sif_kri_kla = '07' where rk.kla_ozn = '" + oznaka + "' ");
            string connString = "Dsn=ifx;uid=informix";
            OdbcConnection conn = new OdbcConnection(connString);
            OdbcDataAdapter adapter = new OdbcDataAdapter(cmd.ToString(), conn);
            conn.Open();
            DataTable table = new DataTable();
            table.Clear();
            adapter.Fill(table);
            conn.Close();

            MySqlConnection mysql = new MySqlConnection(MysqlKonekcija.myConnectionString);
            


            var i = 0;

            progressBar1.Minimum = 0;
            progressBar1.Maximum = table.Rows.Count;
            Classes.Proizvodnja pr = new Classes.Proizvodnja();
            foreach (DataRow row in table.Rows)
            {
                i++;
                if (pr.proveraSifreUBazi(row["sif_rob"].ToString()))
                {
                    String sif_rob = row["sif_rob"].ToString();
                    String naziv = row["naz_rob"].ToString();
                    String kla_ozn = row["kla_ozn"].ToString();
                    String kolekcija = row["kolekcija"].ToString();
                    String kla_ozn_07 = row["kla_ozn_07"].ToString();
                    MySqlCommand komanda = mysql.CreateCommand();
                    StringBuilder sb = new StringBuilder();
                    komanda.CommandText = "insert into roba (sif_rob,naz_rob,kla_ozn,kolekcija,kurs,marza,kla_ozn_07) values (@PsifRob,@PnazRob,@PklaOzn,@Pkolekcija,@Pkurs,@Pmarza,@PklaOzn07);";
                    
                    komanda.Parameters.AddWithValue("@PsifRob", sif_rob);
                    komanda.Parameters.AddWithValue("@PnazRob", naziv);
                    komanda.Parameters.AddWithValue("@PklaOzn", kla_ozn);
                    komanda.Parameters.AddWithValue("@Pkolekcija",kolekcija);
                    komanda.Parameters.AddWithValue("@PklaOzn07", kla_ozn_07);
                    komanda.Parameters.AddWithValue("@Pkurs", kurs);
                    komanda.Parameters.AddWithValue("@Pmarza", marza);

                    try
                    {
                        mysql.Open();
                        komanda.ExecuteNonQuery();
                        mysql.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

                progressBar1.Value = i;
            }
            MessageBox.Show("Uspeno uneti artikli", "Obavestenje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            progressBar1.Value = 0;
        }
    }
}
