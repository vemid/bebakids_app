using System;
using System.Windows.Forms;

namespace BebaKids.Prijava
{
    public partial class PrijavaOdsustva : Form
    {
        public PrijavaOdsustva()
        {
            InitializeComponent();

            Save save = new Save();
            Classes.Application radnici = new Classes.Application();

            comboRadnici.DataSource = radnici.radniciObjekat();
            comboRadnici.DisplayMember = "ime_i_prezime";
            comboRadnici.ValueMember = "sifra";
            comboRadnici.SelectedIndex = -1;
            comboRadnici.SelectedIndex = -1;
        }

        public static string vrsta = "";

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            Form1 Form1 = new Form1();
            Form1.Show();
        }

        private void btnSacuvaj_Click(object sender, EventArgs e)
        {

            var MyIni = new IniFile(@"C:\bkapps\config.ini");
            var Sifraobjekat = MyIni.Read("sif_obj_mp", "ProveraDokumenta");
            int objekat = Convert.ToInt32(Sifraobjekat.ToString());

            vrsta = Form1.vrsta.ToString();

            int paidDay = 0;
            int sickDay = 0;

            if (cbPaidDay.Checked && !cbSickDay.Checked)
            {
                paidDay = 1;
            }
            else if (!cbPaidDay.Checked && cbSickDay.Checked)
            {
                sickDay = 1;
            }

            if (comboRadnici.SelectedIndex == -1)
            {
                MessageBox.Show("Morate odabrati radnika !", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (!cbPaidDay.Checked && !cbSickDay.Checked)
                {
                    MessageBox.Show("Morate selektovati jednu vrstu odsustva !", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (cbPaidDay.Checked && cbSickDay.Checked)
                {
                    MessageBox.Show("Morate selektovati samo jednu vrstu odsustva !", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    Save save = new Save();

                    for (DateTime d = dateFrom.Value.Date; d <= dateTo.Value.Date; d = d.AddDays(1))
                    {
                        if (d.DayOfWeek != DayOfWeek.Sunday)
                        {
                            save.insertOdsustvo(Convert.ToInt32(comboRadnici.SelectedValue.ToString()), objekat, d.ToString("yyyy-MM-dd"), paidDay, sickDay, vrsta, textBox1.Text.ToString());
                        }
                    }
                    this.Close();

                }

            }

        }
    }
}
