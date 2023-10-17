using System;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Windows.Forms;

namespace BebaKids
{
    public partial class FrmProveraFakture : Form
    {
        private OdbcDataAdapter barkod = null;
        public FrmProveraFakture()
        {
            InitializeComponent();
            lbNaziv.Text = "";
            lbSifra.Text = "";
            lbVelicina.Text = "";
            this.ActiveControl = tbBarkod;
        }

        public static string prijemnica = "";
        public static string vrsta = "";


        private DataTable citajBarkod()
        {
            OdbcConnection konekcija = new OdbcConnection(Konekcija.konString);

            DataSet ds = new DataSet();// kreiranje DataSet objekta
            barkod = new OdbcDataAdapter("select * from ean_kod2", konekcija);//punjenje objekta sqladaptera sa podacima iz tab. users
            barkod.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            OdbcCommandBuilder builder = new OdbcCommandBuilder(barkod);//sqldataadapter komanda cita iz sqlbuildera
            barkod.Fill(ds, "barkod");//punjenj objekta
            DataTable tabelaBarkodova = ds.Tables["barkod"];//kreiraanje tabele koja prestavlja kopiju
            return tabelaBarkodova;
        }

        private void textBoxTest_KeyDown(object sender, KeyEventArgs e)
        {

            Classes.Application sound = new Classes.Application();
            vrsta = Form1.vrsta.ToString();

            if (e.KeyCode == Keys.Enter)
            {
                string BARKOD = Convert.ToString(tbBarkod.Text);
                string dokument = Convert.ToString(tbPrijemnica.Text);
                var MyIni = new IniFile(@"C:\bkapps\config.ini");
                var objekat = MyIni.Read("sif_obj_mp", "ProveraDokumenta").ToString();
                var SifPar = MyIni.Read("sif_par", "ProveraDokumenta").ToString();

                DataTable barkodovi = citajBarkod();
                //dataGridView1.DataSource = barkodovi;
                //DataRow checkBarkod = barkodovi.Rows.Find(BARKOD.ToString());
                var result = barkodovi.Rows.IndexOf(barkodovi.AsEnumerable().Where(c => c.Field<String>(3) == BARKOD).FirstOrDefault());

                if (result == -1)
                {
                    //tbBarkod.Clear();
                    //this.ActiveControl = tbSifra;
                    //MessageBox.Show("Ne postoji barkod, molimo unesite rucno sifru i velicinu");
                    //groupBox1.Visible = true;
                    //tbKolicina.Text = "1";

                    sound.playSound("errorSifra");

                    frmRucniUnos frmRucniUnos = new frmRucniUnos();
                    if (frmRucniUnos.ShowDialog() == DialogResult.OK)
                    {
                        int iKolicina = frmRucniUnos.tkolicina;
                        string iSifra = frmRucniUnos.tsifra;
                        string iVelicina = frmRucniUnos.tvelicina;
                        Save cuvanje = new Save();
                        cuvanje.insert(dokument, vrsta, objekat, iSifra, iVelicina, Convert.ToInt32(iKolicina));

                        lbVelicina.Text = iVelicina.ToString();
                        lbSifra.Text = iSifra.ToString();
                        //var sifra = barkodovi.Rows.IndexOf(barkodovi.AsEnumerable().Where(c => c.Field<String>(2) == iSifra.ToString()).FirstOrDefault());
                        //lbNaziv.Text = barkodovi.Rows[sifra].ItemArray[1].ToString();
                        tbBarkod.Clear();
                    }


                }
                else
                {
                    if (barkodovi.Rows[result].ItemArray[4].ToString() == "")
                    {

                        sound.playSound("error");
                        frmVelicina frmVelicina = new frmVelicina();
                        if (frmVelicina.ShowDialog() == DialogResult.OK || !string.IsNullOrEmpty(frmVelicina.tbvelicina.ToString()))
                        {
                            lbVelicina.Text = frmVelicina.tbvelicina.ToString();
                            lbSifra.Text = barkodovi.Rows[result].ItemArray[2].ToString();
                            lbNaziv.Text = barkodovi.Rows[result].ItemArray[1].ToString();
                            tbBarkod.Clear();

                            string iVelicina = frmVelicina.tbvelicina.ToString();
                            string iSifra = barkodovi.Rows[result].ItemArray[2].ToString();
                            int iKolicina = 1;

                            Save cuvanje = new Save();
                            cuvanje.insert(dokument, vrsta, objekat, iSifra, iVelicina, Convert.ToInt32(iKolicina));
                        }
                        else { MessageBox.Show("Niste uneli velicinu"); }
                    }
                    else
                    {
                        lbSifra.Text = barkodovi.Rows[result].ItemArray[2].ToString();
                        lbNaziv.Text = barkodovi.Rows[result].ItemArray[1].ToString();
                        lbVelicina.Text = barkodovi.Rows[result].ItemArray[4].ToString();
                        //MessageBox.Show(barkodovi.Rows[result].ItemArray[2].ToString());
                        tbBarkod.Clear();

                        string iVelicina = barkodovi.Rows[result].ItemArray[4].ToString();
                        string iSifra = barkodovi.Rows[result].ItemArray[2].ToString();
                        int iKolicina = 1;

                        Save cuvanje = new Save();
                        cuvanje.insert1(dokument, vrsta, SifPar, iSifra, iVelicina, Convert.ToInt32(iKolicina));
                    }
                }

            }
        }

        private void tbPrijemnica_Leave(object sender, EventArgs e)
        {
            var MyIni = new IniFile(@"C:\bkapps\config.ini");
            //var objekat = MyIni.Read("sif_obj_mp", "ProveraDokumenta").ToString();
            var SifPar = MyIni.Read("sif_par", "ProveraDokumenta").ToString();
            vrsta = Form1.vrsta.ToString();
            //MessageBox.Show(vrsta.ToString());

            Save cuvanje = new Save();
            var provera = cuvanje.proveraDokumenta1(tbPrijemnica.Text.ToString(), vrsta, SifPar);
            //MessageBox.Show(tbPrijemnica.Text.ToString() + " " + vrsta.ToString() + " " + objekat.ToString());
            if (provera == true)
            {
            }

            else
            {
                MessageBox.Show("Ne postoji otpremnica");
            }
        }

        public void btn_Proveri(object sender, EventArgs e)
        {
            vrsta = Form1.vrsta.ToString();
            Save brisi = new Save();

            this.Hide();
            prijemnica = tbPrijemnica.Text.ToString();

            //brisi.brisiDokument(prijemnica,vrsta);
            frmProveraPrijemnice1 frm = new frmProveraPrijemnice1();
            frm.Show();

        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Save save = new Save();
            save.completedDokument(tbPrijemnica.Text.ToString());

            this.Hide();
            Form1 Form1 = new Form1();
            Form1.Show();
        }

        private void btnUporedi_Click(object sender, EventArgs e)
        {
            vrsta = Form1.vrsta.ToString();
            Save brisi = new Save();

            this.Hide();
            prijemnica = tbPrijemnica.Text.ToString();

            //brisi.brisiDokument(prijemnica,vrsta);
            frmProveraPrijemnice1 frm = new frmProveraPrijemnice1();
            frm.Show();
        }
    }
}
