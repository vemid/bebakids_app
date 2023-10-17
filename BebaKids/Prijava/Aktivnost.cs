using System;
using System.Windows.Forms;

namespace BebaKids.Prijava
{
    public partial class Aktivnost : Form
    {
        public Aktivnost()
        {
            InitializeComponent();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            Form1 Form1 = new Form1();
            Form1.Show();
        }

        private void objektiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Prijava.Objekat.Objekti objekti = new Objekat.Objekti();
            objekti.Show();
        }

        private void radniciToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Prijava.Radnici.Radnici radnici = new Radnici.Radnici();
            radnici.Show();
        }

        private void izvestajPoRadnikuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Prijava.Reports.ReportByRadnik izvestaj = new Reports.ReportByRadnik();
            izvestaj.Show();
        }
    }

}
