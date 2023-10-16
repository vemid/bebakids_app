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
using System.Net.Sockets;
using System.Threading;

namespace BebaKids
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
var MyIni = new IniFile(@"C:\bkapps\config.ini");
            var sifraObjekta = MyIni.Read("naziv", "ProveraDokumenta");
            var objekat = MyIni.Read("sif_obj_mp", "ProveraDokumenta");
            label2.Text = sifraObjekta.ToString();
        }
        public static string vrsta = "";
        public static string SifraObjekta = "";

        private bool testKonekcija()
        {
            TcpClient tcpClient = new TcpClient();

            try
            {
                tcpClient.Connect("192.168.100.12", 1526);
                return true;
            }
            catch (Exception)
            {
                return false;
                
            }
        }

        public void btnBarkodovi_Click(object sender, EventArgs e)
        {

            this.Hide();
            frmInsertBarkod Form2 = new frmInsertBarkod();
            Form2.Show();


        }

        public void btnPrijemnica_Click(object sender, EventArgs e)
        {
            vrsta = "P9";
            this.Hide();
            frmPrijemnica Form2 = new frmPrijemnica();
            Form2.Show();


        }
        public void btnOtpremnica_Click(object sender, EventArgs e)
        {
            vrsta = "OM";
            this.Hide();
            frmPrijemnica Form2 = new frmPrijemnica();
            Form2.Show();


        }
        public void btnMagacin_Click(object sender, EventArgs e)
        {
            vrsta = "MP";
            this.Hide();
            frmPrijemnica Form2 = new frmPrijemnica();
            Form2.Show();


        }

        public void btnPrijvaRadnika_Click(object sender, EventArgs e)
        {
            vrsta = "REGULAR";
            this.Hide();
            Prijava.PrijavaRadnika prijavaRadnika = new Prijava.PrijavaRadnika();
            prijavaRadnika.Show();
            
        }

        public void btnPopis_Click(object sender, EventArgs e)
        {
            vrsta = "POPIS";
            this.Hide();
            Prijava.PrijavaRadnika prijavaRadnika = new Prijava.PrijavaRadnika();
            prijavaRadnika.Show();
        }



        public void formLoad_Load(object sender, EventArgs e)
        {
            var MyIni = new IniFile(@"C:\bkapps\config.ini");
            var provera = MyIni.Read("magacin", "ProveraDokumenta");
            var franzisa = MyIni.Read("fransiza", "ProveraDokumenta");
            var proizvodnja = MyIni.Read("proizvodnja", "Proizvodnja");
            var trziste = MyIni.Read("trziste", "ProveraDokumenta");
            var system = MyIni.Read("system", "ProveraDokumenta");

            if (provera == "DA")
            {
                btnPrijemnica.Visible = false;
                btnPrenosnica.Visible = false;
                btnPrijavaRadnika.Visible = false;
                btnPrijavaOdsustva.Visible = false;
                btnPrijavaPopisa.Visible = false;
                if (proizvodnja == "DA")
                {
                    proizvodnjaToolStripMenuItem.Visible = true;
                }
                dnevniPrometToolStripMenuItem.Visible = false;
                
            }
            else
            {
                btnMagacin.Visible = false;
                izvestajiToolStripMenuItem.Visible = true;
                radniciToolStripMenuItem.Visible = false;
                racunovodstvoToolStripMenuItem.Visible = false;
                izvestajFransizePoDanuToolStripMenuItem.Visible = false;
                if (franzisa == "DA")
                {
                    izvestajFransizePoDanuToolStripMenuItem.Visible = true;
                    btnPrijemnica.Visible = false;
                    btnPrenosnica.Visible = false;
                }
                else
                { btnProveraFakture.Visible = false; }
                //pregledDnevnogIzveštajaToolStripMenuItem.Visible = false;

            }
            if (trziste == "CG")
            {
                unosNovogClanaCGToolStripMenuItem.Visible = true;
                unosNovogClanaToolStripMenuItem.Visible = false;

            }
            else { unosNovogClanaCGToolStripMenuItem.Visible = false; }

            
            
            if (testKonekcija())
            { }
            else
            {
                pictureBox2.Visible = true;
                label3.Visible = true;

            }
            if (system == "watch")
            {
                pictureBox1.Image = BebaKids.Properties.Resources.watch_logo1;
                this.Icon = Properties.Resources.watch_icon;
            }

        }

        private void otvoriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Prijava.Login login = new Prijava.Login();
            login.Show();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            vrsta = "HOUR";
            this.Hide();
            Prijava.PrijavaRadnika prijavaRadnika = new Prijava.PrijavaRadnika();
            prijavaRadnika.Show();
        }

        private void btnPrijavaOdsustva_Click(object sender, EventArgs e)
        {
            vrsta = "ODSUSTVO";
            this.Hide();
            Prijava.PrijavaOdsustva PrijavaOdsustva = new Prijava.PrijavaOdsustva();
            PrijavaOdsustva.Show();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            var MyIni = new IniFile(@"C:\bkapps\config.ini");
            var provera = MyIni.Read("magacin", "ProveraDokumenta");
            if (provera == "NE")
            {
                if (testKonekcija())
                {
                    Save save = new Save();
                    save.prenosPodataka();
                    Classes.Application app = new Classes.Application();
                    app.prenosPazara();
                }
                else
                {
                    MessageBox.Show("Nemate vezu prema serveru, molim Vas proverite konekciju pa pokusajte ponovo kasnije!", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            //Application.Exit();
        }

        private void btnRealizacijaCekova_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmCekovi Form2 = new frmCekovi();
            Form2.Show();

        }

        private void unosPopisaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            PopisMp.PopisMpUnos popisMp = new PopisMp.PopisMpUnos();
            popisMp.Show();
        }

        private void pregledPrenosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            PopisMp.PrenosPopis prenosPopis = new PopisMp.PrenosPopis();
            prenosPopis.Show();
        }

        private void izvestajFransizePoDanuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Fransiza.PregledDnevneProvizije frn = new Fransiza.PregledDnevneProvizije();
            frn.Show();
        }

        private void prenosSifaraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Proizvodnja.PrenosSifara frm = new Proizvodnja.PrenosSifara();
            frm.Show();
        }

        private void kalkulacijaArtiklaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Proizvodnja.Kalkulacija frm = new Proizvodnja.Kalkulacija();
            frm.Size = new Size(SystemInformation.VirtualScreen.Width,SystemInformation.VirtualScreen.Height);
            frm.Show();
        }

        private void proveraCeneArtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Proizvodnja.ProveraCene newWindows = new Proizvodnja.ProveraCene();
            newWindows.Show();
        }

        private void unosPrometaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(testKonekcija())
            { 
            this.Hide();
            tbPSPresek frm = new tbPSPresek();
            frm.Show();
            }
            else
            {
                noConnection nc = new noConnection();
                nc.Show();
            }
}

        private void pregledDnevnogIzveštajaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Backend.frmPrometiPoRadnjama frm = new Backend.frmPrometiPoRadnjama();
            frm.Show();
        }

        private void unosNovogClanaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            if (testKonekcija())
            {
                Loyalti.addNewCard frm = new Loyalti.addNewCard();
                frm.Show();
            }
            else
            {
                noConnection nc = new noConnection();
                nc.Show();
            }
        }

        private void unosNovogClanaCGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            if (testKonekcija())
            {
                Loyalti.addNewCardCG frm = new Loyalti.addNewCardCG();
                frm.Show();
            }
            else
            {
                noConnection nc = new noConnection();
                nc.Show();
            }
        }


        private void btnProveraFakture_Click(object sender, EventArgs e)
        {
            vrsta = "FK";
            this.Hide();
            FrmProveraFakture Form2 = new FrmProveraFakture();
            Form2.Show();
        }

        private void osvezavanjeIzvodaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();

                Backend.eFiscal frm = new Backend.eFiscal();
                frm.Show();
        }

        private void dokumentOtpremeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();

            MIS.dodajDokumentMP frm = new MIS.dodajDokumentMP();
            frm.Show();
        }
    }
}
