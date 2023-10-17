using System;
using System.Data.Odbc;
using System.Windows.Forms;

namespace BebaKids.Prijava.Radnici
{
    public partial class Radnici : Form
    {
        public Radnici()
        {
            InitializeComponent();
            button2.Visible = true;
            Save save = new Save();
            Classes.Application objekti = new Classes.Application();

            comboBox1.DataSource = objekti.objekti();
            comboBox1.DisplayMember = "naz_obj_mp";
            comboBox1.ValueMember = "sif_obj_mp";
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void frmObjekti_Load(object sender, EventArgs e)
        {


            Save save = new Save();
            Classes.Application radnici = new Classes.Application();

            dataGridView1.DataSource = radnici.radnici();
        }

        private void btnDodajObjekat_Click(object sender, EventArgs e)
        {
            string prijavaKonString = "Dsn=prijava;uid=sa;Pwd=adminabc123";

            MessageBox.Show(comboBox1.SelectedValue.ToString());
            MessageBox.Show(comboBox2.SelectedItem.ToString());

            if (!String.IsNullOrEmpty(tbSifraRadnika.Text.ToString()) && tbImePrezime.Text.ToString() != "" && comboBox1.SelectedValue.ToString() != "" && comboBox2.SelectedItem.ToString() != "")
            {
                int sifraRadnika = Convert.ToInt32(tbSifraRadnika.Text.ToString());
                int sifraObjekta = Convert.ToInt32(comboBox1.SelectedValue.ToString());
                OdbcConnection konekcija = new OdbcConnection(prijavaKonString);
                OdbcCommand komanda = new OdbcCommand("insert into radnici (sifra,ime_i_prezime,sif_obj_mp,status) values ('" + sifraRadnika + "','" + tbImePrezime.Text.ToString() + "','" + sifraObjekta + "','" + comboBox2.Text.ToString() + "')", konekcija);
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
                dataGridView1.DataSource = null;
                dataGridView1.Rows.Clear();
                Save save = new Save();
                Classes.Application radnici = new Classes.Application();

                dataGridView1.DataSource = radnici.radnici();

                tbImePrezime.Clear();
                tbSifraRadnika.Clear();
                comboBox1.SelectedIndex = -1;
                comboBox2.SelectedIndex = -1;

            }
            else
            {
                MessageBox.Show("Morate uneti sve podatke", "Informacija", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void izmeniToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            button2.Visible = true;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

                string sifra = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                string imePrezime = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                string objekat = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                string status = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                //string role = dataGridView1.CurrentRow.Cells[4].Value.ToString();


                tbSifraRadnika.Text = sifra;
                tbSifraRadnika.ReadOnly = true;
                tbImePrezime.Text = imePrezime;
                comboBox2.SelectedIndex = -1;
                comboBox2.SelectedText = status;
                comboBox1.SelectedItem = objekat;
            }
        }
        private void btnSacuvaj_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(tbSifraRadnika.Text.ToString()) && tbImePrezime.Text.ToString() != "" && comboBox1.Text.ToString() != "" && comboBox2.Text.ToString() != "")
            {
                string prijavaKonString = "Dsn=prijava;uid=sa;Pwd=adminabc123";
                int sifraRadnika = Convert.ToInt32(tbSifraRadnika.Text.ToString());
                int sifraObjekta = Convert.ToInt32(comboBox1.SelectedValue.ToString());
                OdbcConnection konekcija = new OdbcConnection(prijavaKonString);
                OdbcCommand komanda = new OdbcCommand("update radnici set ime_i_prezime= '" + tbImePrezime.Text.ToString() + "',sif_obj_mp = '" + Convert.ToInt32(comboBox1.SelectedValue.ToString()) + "',status = '" + comboBox2.Text.ToString() + "' where sifra = '" + sifraRadnika + "'", konekcija);
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

                dataGridView1.DataSource = null;
                dataGridView1.Rows.Clear();
                Save save = new Save();
                Classes.Application radnici = new Classes.Application();

                dataGridView1.DataSource = radnici.radnici();

                button1.Visible = true;
                //button2.Visible = false;
                tbSifraRadnika.ReadOnly = false;
            }
            else
            {
                MessageBox.Show("Morate uneti sve podatke", "Informacija", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            Prijava.Aktivnost aktivnost = new Aktivnost();
            aktivnost.Show();
        }

    }
}
