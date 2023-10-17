using System;
using System.Data.Odbc;
using System.Windows.Forms;

namespace BebaKids.Prijava.Objekat
{
    public partial class Objekti : Form
    {
        public Objekti()
        {
            InitializeComponent();
            button2.Visible = false;
        }

        public void frmObjekti_Load(object sender, EventArgs e)
        {


            Save save = new Save();
            Classes.Application objekti = new Classes.Application();

            dataGridView1.DataSource = objekti.objekti();
        }

        private void btnDodajObjekat_Click(object sender, EventArgs e)
        {
            string prijavaKonString = "Dsn=prijava;uid=sa;Pwd=adminabc123";

            if (!String.IsNullOrEmpty(textBox2.Text.ToString()) && textBox1.Text.ToString() != "")
            {
                int status = 0;
                if (comboBox1.SelectedText.ToString() == "Aktivan")
                {
                    status = 1;
                }
                else
                {
                    status = 0;
                }

                int sifraObjekta = Convert.ToInt32(textBox1.Text.ToString());
                OdbcConnection konekcija = new OdbcConnection(prijavaKonString);
                OdbcCommand komanda = new OdbcCommand("insert into sif_obj_mp (sif_obj_mp,naz_obj_mp,e_mail,status) values ('" + sifraObjekta + "','" + textBox2.Text.ToString() + "','" + textBox3.Text.ToString() + "','" + status + "')", konekcija);
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
                Classes.Application objekti = new Classes.Application();

                dataGridView1.DataSource = objekti.objekti();

                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                comboBox1.SelectedIndex = -1;

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
                string nazivObjekta = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                string email = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                string status = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                //string role = dataGridView1.CurrentRow.Cells[4].Value.ToString();


                textBox1.Text = sifra;
                //textBox1.ReadOnly = true;
                textBox2.Text = nazivObjekta;
                textBox3.Text = email;
                comboBox1.SelectedItem = status;
            }
        }
        private void btnSacuvaj_Click(object sender, EventArgs e)
        {
            string prijavaKonString = "Dsn=prijava;uid=sa;Pwd=adminabc123";
            int sifraObjekta = Convert.ToInt32(textBox1.Text.ToString());
            OdbcConnection konekcija = new OdbcConnection(prijavaKonString);
            OdbcCommand komanda = new OdbcCommand("update sif_obj_mp set naz_obj_mp= '" + textBox2.Text.ToString() + "',e_mail = '" + textBox3.Text.ToString() + "',status = '" + comboBox1.SelectedValue.ToString() + "' where sif_obj_mp = '" + textBox1.Text.ToString() + "'", konekcija);
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
            Classes.Application objekti = new Classes.Application();

            dataGridView1.DataSource = objekti.objekti();

            button1.Visible = true;
            button2.Visible = false;
            textBox1.ReadOnly = false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            Prijava.Aktivnost aktivnost = new Aktivnost();
            aktivnost.Show();
        }


    }
}
