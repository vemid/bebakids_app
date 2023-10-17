using BebaKids.Properties;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BebaKids.Backend
{
    public partial class frmPrometiPoRadnjama : Form
    {
        PrintDocument printD = new PrintDocument();
        DateTime date1;
        string objekat;
        public frmPrometiPoRadnjama()
        {
            InitializeComponent();
            datumPrometa.Value = DateTime.Today.AddDays(-1);
            Classes.Application objekti = new Classes.Application();

            comboBox1.DataSource = objekti.objekti();
            comboBox1.DisplayMember = "naz_obj_mp";
            comboBox1.ValueMember = "sif_obj_mp";
            comboBox1.SelectedIndex = -1;



        }





        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g;

            g = e.Graphics;
            int width = ClientRectangle.Width;
            Color color = System.Drawing.ColorTranslator.FromHtml("#3fb0ac");
            Pen myPen = new Pen(color);
            myPen.Width = 5;
            //g.DrawLine(myPen, 50, 300, 50, 300);
            Point tacka12 = new Point(30, 100);
            Point tacka22 = new Point(width - 30, 100);
            g.DrawLine(myPen, tacka12, tacka22);

            Pen litlePen = new Pen(color);
            litlePen.Width = 3;
            Point tackal1 = new Point(30, 230);
            Point tackal2 = new Point(width - 30, 230);
            g.DrawLine(litlePen, tackal1, tackal2);

            Point tackal11 = new Point(30, 410);
            Point tackal21 = new Point(width - 30, 410);
            g.DrawLine(litlePen, tackal11, tackal21);

            Point tackal13 = new Point(30, 535);
            Point tackal24 = new Point(width - 30, 535);
            g.DrawLine(litlePen, tackal13, tackal24);

            Point tackal15 = new Point(30, 635);
            Point tackal26 = new Point(width - 30, 635);
            g.DrawLine(litlePen, tackal15, tackal26);

            Pen redPen = new Pen(Color.Red);
            redPen.Width = 2;
            //g.DrawRectangle(redPen,new Rectangle (40,60,175,115));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //DateTime datum = datumPrometa.Value.Date;
            var MyIni = new IniFile(@"C:\bkapps\config.ini");
            var provera = MyIni.Read("magacin", "ProveraDokumenta");
            var baza = MyIni.Read("baza", "ProveraDokumenta").ToString();
            var database = MyIni.Read("database", "ProveraDokumenta").ToString();
            var system = MyIni.Read("system", "ProveraDokumenta").ToString();
            if (provera == "DA")
            {
                objekat = comboBox1.SelectedValue.ToString();
            }
            else
            {
                objekat = MyIni.Read("sif_obj_mp", "ProveraDokumenta");
                label39.Visible = false;
                if (system == "watch")
                {
                    objekat = "1" + objekat;
                }
            }
            string date = datumPrometa.Value.ToString("yyyy-MM-dd");
            date1 = Convert.ToDateTime(date);
            string Localbaza = MyIni.Read("konekcija_server", "Baza").ToString();


            SqlConnection sqlConn = new SqlConnection(Localbaza);

            StringBuilder sb = new StringBuilder();
            sb.Append("select * from pazari as p left join pazari_card as pc on pc.oznaka = p.oznaka left join pazari_ni as pn on pn.oznaka = p.oznaka where date = '" + date + "' and sif_obj_mp = '" + objekat + "'");
            SqlCommand komanda = new SqlCommand(sb.ToString(), sqlConn);

            try
            {
                sqlConn.Open();
                SqlDataReader reader = komanda.ExecuteReader();
                if (reader.Read())
                {
                    button1.Visible = true;
                    tbDIGotovina.Text = reader.GetDecimal(4).ToString();
                    tbDIKartice.Text = reader.GetDecimal(5).ToString();
                    tbDICek.Text = reader.GetDecimal(6).ToString();
                    tbPresek.Text = reader.GetDecimal(7).ToString();
                    tbPSPdv.Text = reader.GetDecimal(8).ToString();
                    tbACRedovni.Text = reader.GetDecimal(9).ToString();
                    tbACOdlozeni.Text = reader.GetDecimal(10).ToString();
                    tbACAdmimistrativne.Text = reader.GetDecimal(11).ToString();
                    tbBankaBI.Text = reader.GetDecimal(19).ToString();
                    tbDinersRedovan.Text = reader.GetDecimal(21).ToString();
                    tbBankaUni.Text = reader.GetDecimal(20).ToString();
                    tbDiners1.Text = reader.GetDecimal(22).ToString();
                    tbDiners2.Text = reader.GetDecimal(23).ToString();
                    tbDiners3.Text = reader.GetDecimal(24).ToString();
                    tbDiners4.Text = reader.GetDecimal(25).ToString();
                    tbNi1.Text = reader.GetDecimal(29).ToString();
                    tbNi2.Text = reader.GetDecimal(30).ToString();
                    tbNi3.Text = reader.GetDecimal(31).ToString();
                    tbNi4.Text = reader.GetDecimal(32).ToString();
                    tbNi5.Text = reader.GetDecimal(33).ToString();
                    tbNi6.Text = reader.GetDecimal(34).ToString();
                    tbNi7.Text = reader.GetDecimal(35).ToString();
                    tbNi8.Text = reader.GetDecimal(36).ToString();
                    tbNi9.Text = reader.GetDecimal(37).ToString();
                    tbNi10.Text = reader.GetDecimal(38).ToString();
                    tbNapomena.Text = reader.GetString(14).ToString();
                    tbKonzul.Text = reader.GetDecimal(12).ToString();
                    tbVauceri.Text = reader.GetDecimal(13).ToString();

                }

            }
            catch { }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(printDocument_PrintPage);
            PrintDialog printdlg = new PrintDialog();
            PrintPreviewDialog printPrvDlg = new PrintPreviewDialog();
            printPrvDlg.Document = pd;
            printD = pd;
            ToolStripButton b = new ToolStripButton();
            b.Image = Properties.Resources.print;
            b.DisplayStyle = ToolStripItemDisplayStyle.Image;
            b.Click += printingDocument_Click;
            ((ToolStrip)(printPrvDlg.Controls[1])).Items.RemoveAt(0);
            ((ToolStrip)(printPrvDlg.Controls[1])).Items.Insert(0, b);

            printdlg.Document = pd;
            ((Form)printPrvDlg).WindowState = FormWindowState.Maximized;
            printPrvDlg.ShowDialog();
            foreach (TextBox tb in this.Controls.OfType<TextBox>())
            {
                tb.Text = "";
            }

        }

        void printingDocument_Click(object sender, EventArgs e)
        {
            PrintDialog printdlg = new PrintDialog();
            PrinterSettings settings = new PrinterSettings();
            string printerName = settings.PrinterName;
            printD.PrintController = new StandardPrintController();
            if (printdlg.ShowDialog() == DialogResult.OK)
            {
                printD.Print();
            }

            try { System.IO.File.Delete("d:\\ddd112.pdf"); }
            catch { }

            foreach (TextBox tb in this.Controls.OfType<TextBox>())
            {
                tb.Text = "";
            }

        }

        private void printDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            var MyIni = new IniFile(@"C:\bkapps\config.ini");
            var racun = MyIni.Read("racun", "ProveraDokumenta");
            var objekat = MyIni.Read("sif_obj_mp", "ProveraDokumenta");
            var provera = MyIni.Read("magacin", "ProveraDokumenta");
            var system = MyIni.Read("system", "ProveraDokumenta").ToString();

            Image logo = Resources.logoHeader;
            string text1 = "Preduzece za trgovinu KIDS BEBA doo";
            string text2 = "Ignjata Joba 37 Beograd";

            if (system == "watch")
            {
                text1 = "Watch is Watch doo";
                text2 = "Zaplanjska 32 Beograd";
                logo = Resources.watch_header;
            }



            int sirina = logo.Width;
            int visina = 0;
            visina += logo.Height;
            e.Graphics.DrawImage(logo, 25, 25, logo.Width, logo.Height);

            e.Graphics.DrawString("Pregled dnevnog prometa na kasi", new Font("Arial", 18, FontStyle.Bold), Brushes.Black, new Point(100, visina + 50));
            e.Graphics.DrawString("Objekat: " + comboBox1.SelectedValue + " " + comboBox1.Text, new Font("Arial", 16, FontStyle.Bold), Brushes.Black, new Point(100, visina + 80));
            e.Graphics.DrawString("Datum: " + date1.ToString("dd.MM.yyyy"), new Font("Arial", 16, FontStyle.Bold), Brushes.Black, new Point(100, visina + 110));
            int width = ClientRectangle.Width;
            Color color = System.Drawing.ColorTranslator.FromHtml("#3fb0ac");
            Pen myPen = new Pen(color);
            myPen.Width = 5;
            Point tacka1 = new Point(30, visina + 150);
            Point tacka2 = new Point(800, visina + 150);
            e.Graphics.DrawLine(myPen, tacka1, tacka2);
            int visinaPromet = visina + 170;
            int levo = 100;
            int centar = 330;
            int desno = 530;
            e.Graphics.DrawString("Dnevni izvestaj", new Font("Arial", 16, FontStyle.Bold), Brushes.Black, new Point(levo - 20, visinaPromet));
            e.Graphics.DrawString("Gotovina: ", new Font("Arial", 13, FontStyle.Regular), Brushes.Black, new Point(levo, visinaPromet + 40));
            e.Graphics.DrawString(tbDIGotovina.Text, new Font("Arial", 13, FontStyle.Bold), Brushes.Black, new Point(levo + 90, visinaPromet + 40));
            e.Graphics.DrawString("Kartica: ", new Font("Arial", 13, FontStyle.Regular), Brushes.Black, new Point(levo, visinaPromet + 65));
            e.Graphics.DrawString(tbDIKartice.Text, new Font("Arial", 13, FontStyle.Bold), Brushes.Black, new Point(levo + 90, visinaPromet + 65));
            e.Graphics.DrawString("Cek: ", new Font("Arial", 13, FontStyle.Regular), Brushes.Black, new Point(levo, visinaPromet + 90));
            e.Graphics.DrawString(tbDICek.Text, new Font("Arial", 13, FontStyle.Bold), Brushes.Black, new Point(levo + 90, visinaPromet + 90));

            e.Graphics.DrawString("Presek Stanja", new Font("Arial", 16, FontStyle.Bold), Brushes.Black, new Point(centar - 20, visinaPromet));
            e.Graphics.DrawString("Gotovina: ", new Font("Arial", 13, FontStyle.Regular), Brushes.Black, new Point(centar, visinaPromet + 40));
            e.Graphics.DrawString(tbPresek.Text, new Font("Arial", 13, FontStyle.Bold), Brushes.Black, new Point(centar + 90, visinaPromet + 40));
            e.Graphics.DrawString("Kartica: ", new Font("Arial", 13, FontStyle.Regular), Brushes.Black, new Point(centar, visinaPromet + 65));
            e.Graphics.DrawString(tbPSPdv.Text, new Font("Arial", 13, FontStyle.Bold), Brushes.Black, new Point(centar + 90, visinaPromet + 65));

            e.Graphics.DrawString("Administrativne i cekovi", new Font("Arial", 16, FontStyle.Bold), Brushes.Black, new Point(desno - 20, visinaPromet));
            e.Graphics.DrawString("Redovni cekovi: ", new Font("Arial", 13, FontStyle.Regular), Brushes.Black, new Point(desno, visinaPromet + 40));
            e.Graphics.DrawString(tbACRedovni.Text, new Font("Arial", 13, FontStyle.Bold), Brushes.Black, new Point(desno + 145, visinaPromet + 40));
            e.Graphics.DrawString("Odlozeni cekovi: ", new Font("Arial", 13, FontStyle.Regular), Brushes.Black, new Point(desno, visinaPromet + 65));
            e.Graphics.DrawString(tbACOdlozeni.Text, new Font("Arial", 13, FontStyle.Bold), Brushes.Black, new Point(desno + 145, visinaPromet + 65));
            e.Graphics.DrawString("Administrativne: ", new Font("Arial", 13, FontStyle.Regular), Brushes.Black, new Point(desno, visinaPromet + 90));
            e.Graphics.DrawString(tbACAdmimistrativne.Text, new Font("Arial", 13, FontStyle.Bold), Brushes.Black, new Point(desno + 145, visinaPromet + 90));
            int visinaDrugi = visinaPromet + 145;
            Point tacka21 = new Point(30, visinaPromet + 125);
            Point tacka22 = new Point(800, visinaPromet + 125);
            e.Graphics.DrawLine(myPen, tacka21, tacka22);


            e.Graphics.DrawString("Promet po karticama", new Font("Arial", 16, FontStyle.Bold), Brushes.Black, new Point(levo - 20, visinaDrugi));
            e.Graphics.DrawString("BancaIntesa: ", new Font("Arial", 13, FontStyle.Regular), Brushes.Black, new Point(levo, visinaDrugi + 40));
            e.Graphics.DrawString(tbBankaBI.Text, new Font("Arial", 13, FontStyle.Bold), Brushes.Black, new Point(levo + 110, visinaDrugi + 40));
            e.Graphics.DrawString("Unicredit: ", new Font("Arial", 13, FontStyle.Regular), Brushes.Black, new Point(levo, visinaDrugi + 65));
            e.Graphics.DrawString(tbBankaUni.Text, new Font("Arial", 13, FontStyle.Bold), Brushes.Black, new Point(levo + 110, visinaDrugi + 65));

            e.Graphics.DrawString("Redovan Diners: ", new Font("Arial", 13, FontStyle.Regular), Brushes.Black, new Point(centar - 20, visinaDrugi + 40));
            e.Graphics.DrawString(tbDinersRedovan.Text, new Font("Arial", 13, FontStyle.Bold), Brushes.Black, new Point(centar + 120, visinaDrugi + 40));
            e.Graphics.DrawString("Diners 1.rata: ", new Font("Arial", 13, FontStyle.Regular), Brushes.Black, new Point(centar - 20, visinaDrugi + 65));
            e.Graphics.DrawString(tbDiners1.Text, new Font("Arial", 13, FontStyle.Bold), Brushes.Black, new Point(centar + 120, visinaDrugi + 65));
            e.Graphics.DrawString("Diners 2.rata: ", new Font("Arial", 13, FontStyle.Regular), Brushes.Black, new Point(centar - 20, visinaDrugi + 90));
            e.Graphics.DrawString(tbDiners2.Text, new Font("Arial", 13, FontStyle.Bold), Brushes.Black, new Point(centar + 120, visinaDrugi + 90));

            e.Graphics.DrawString("Diners 1.rata: ", new Font("Arial", 13, FontStyle.Regular), Brushes.Black, new Point(desno + 20, visinaDrugi + 40));
            e.Graphics.DrawString(tbDiners3.Text, new Font("Arial", 13, FontStyle.Bold), Brushes.Black, new Point(desno + 140, visinaDrugi + 40));
            e.Graphics.DrawString("Diners 2.rata: ", new Font("Arial", 13, FontStyle.Regular), Brushes.Black, new Point(desno + 20, visinaDrugi + 65));
            e.Graphics.DrawString(tbDiners4.Text, new Font("Arial", 13, FontStyle.Bold), Brushes.Black, new Point(desno + 140, visinaDrugi + 65));
            int visinatreci = visinaDrugi + 140;
            Point tacka31 = new Point(30, visinaDrugi + 125);
            Point tacka32 = new Point(800, visinaDrugi + 125);
            e.Graphics.DrawLine(myPen, tacka31, tacka32);


            e.Graphics.DrawString("Pregled NI obrazaca", new Font("Arial", 16, FontStyle.Bold), Brushes.Black, new Point(levo - 20, visinatreci));
            e.Graphics.DrawString("NI 1: ", new Font("Arial", 13, FontStyle.Regular), Brushes.Black, new Point(levo, visinatreci + 40));
            e.Graphics.DrawString(tbNi1.Text, new Font("Arial", 13, FontStyle.Bold), Brushes.Black, new Point(levo + 45, visinatreci + 40));
            e.Graphics.DrawString("NI 6: ", new Font("Arial", 13, FontStyle.Regular), Brushes.Black, new Point(levo, visinatreci + 65));
            e.Graphics.DrawString(tbNi6.Text, new Font("Arial", 13, FontStyle.Bold), Brushes.Black, new Point(levo + 45, visinatreci + 65));
            int levo1 = levo + 135;
            e.Graphics.DrawString("NI 2: ", new Font("Arial", 13, FontStyle.Regular), Brushes.Black, new Point(levo1, visinatreci + 40));
            e.Graphics.DrawString(tbNi2.Text, new Font("Arial", 13, FontStyle.Bold), Brushes.Black, new Point(levo1 + 40, visinatreci + 40));
            e.Graphics.DrawString("NI 7: ", new Font("Arial", 13, FontStyle.Regular), Brushes.Black, new Point(levo1, visinatreci + 65));
            e.Graphics.DrawString(tbNi7.Text, new Font("Arial", 13, FontStyle.Bold), Brushes.Black, new Point(levo1 + 40, visinatreci + 65));
            levo1 += 135;
            e.Graphics.DrawString("NI 3: ", new Font("Arial", 13, FontStyle.Regular), Brushes.Black, new Point(levo1, visinatreci + 40));
            e.Graphics.DrawString(tbNi3.Text, new Font("Arial", 13, FontStyle.Bold), Brushes.Black, new Point(levo1 + 40, visinatreci + 40));
            e.Graphics.DrawString("NI 8: ", new Font("Arial", 13, FontStyle.Regular), Brushes.Black, new Point(levo1, visinatreci + 65));
            e.Graphics.DrawString(tbNi8.Text, new Font("Arial", 13, FontStyle.Bold), Brushes.Black, new Point(levo1 + 40, visinatreci + 65));
            levo1 += 135;
            e.Graphics.DrawString("NI 4: ", new Font("Arial", 13, FontStyle.Regular), Brushes.Black, new Point(levo1, visinatreci + 40));
            e.Graphics.DrawString(tbNi4.Text, new Font("Arial", 13, FontStyle.Bold), Brushes.Black, new Point(levo1 + 40, visinatreci + 40));
            e.Graphics.DrawString("NI 9: ", new Font("Arial", 13, FontStyle.Regular), Brushes.Black, new Point(levo1, visinatreci + 65));
            e.Graphics.DrawString(tbNi9.Text, new Font("Arial", 13, FontStyle.Bold), Brushes.Black, new Point(levo1 + 40, visinatreci + 65));
            levo1 += 135;
            e.Graphics.DrawString("NI 5: ", new Font("Arial", 13, FontStyle.Regular), Brushes.Black, new Point(levo1, visinatreci + 40));
            e.Graphics.DrawString(tbNi5.Text, new Font("Arial", 13, FontStyle.Bold), Brushes.Black, new Point(levo1 + 40, visinatreci + 40));
            e.Graphics.DrawString("NI 10: ", new Font("Arial", 13, FontStyle.Regular), Brushes.Black, new Point(levo1, visinatreci + 65));
            e.Graphics.DrawString(tbNi10.Text, new Font("Arial", 13, FontStyle.Bold), Brushes.Black, new Point(levo1 + 40, visinatreci + 65));
            levo1 += 135;

            //double niObrasci = Convert.ToDouble(tbNi1.Text) + Convert.ToDouble(tbNi2.Text) + Convert.ToDouble(tbNi3.Text) + Convert.ToDouble(tbNi4.Text) +
            //  Convert.ToDouble(tbNi5.Text) + Convert.ToDouble(tbNi6.Text) + Convert.ToDouble(tbNi7.Text) + Convert.ToDouble(tbNi8.Text) + Convert.ToDouble(tbNi9.Text) + Convert.ToDouble(tbNi10.Text);
            Double pazar = Convert.ToDouble(tbDIGotovina.Text);

            int visinaCetvrti = visinatreci + 120;
            Point tacka41 = new Point(30, visinatreci + 105);
            Point tacka42 = new Point(800, visinatreci + 105);
            e.Graphics.DrawLine(myPen, tacka41, tacka42);


            e.Graphics.DrawString("Refakcije i vauceri", new Font("Arial", 16, FontStyle.Bold), Brushes.Black, new Point(levo - 20, visinaCetvrti));
            e.Graphics.DrawString("Iznos refakcija i konzularnih predsavnistva: ", new Font("Arial", 13, FontStyle.Regular), Brushes.Black, new Point(levo, visinaCetvrti + 40));
            e.Graphics.DrawString(tbKonzul.Text, new Font("Arial", 13, FontStyle.Bold), Brushes.Black, new Point(levo + 360, visinaCetvrti + 40));
            e.Graphics.DrawString("Vauceri: ", new Font("Arial", 13, FontStyle.Regular), Brushes.Black, new Point(levo + 520, visinaCetvrti + 40));
            e.Graphics.DrawString(tbVauceri.Text, new Font("Arial", 13, FontStyle.Bold), Brushes.Black, new Point(levo + 585, visinaCetvrti + 40));

            int visinaPeti = visinaCetvrti + 90;
            /*
            
            Point tacka51 = new Point(30, visinaCetvrti + 80);
            Point tacka52 = new Point(800, visinaCetvrti + 80);
            e.Graphics.DrawLine(myPen, tacka51, tacka52);

            StringFormat format = new StringFormat();
            format.FormatFlags = StringFormatFlags.FitBlackBox;
            e.Graphics.DrawString("Napomena", new Font("Arial", 16, FontStyle.Bold), Brushes.Black, new Point(levo - 20, visinaPeti));
            e.Graphics.DrawString(SpliceText(tbNapomena.Text,80), new Font("Arial", 13, FontStyle.Regular), Brushes.Black, new Point(levo, visinaPeti+40),format);

            
            */

            Color blackColor = System.Drawing.ColorTranslator.FromHtml("#000000");
            Pen blackPen = new Pen(blackColor);
            blackPen.Width = 2;

            Point tacka51 = new Point(30, visinaCetvrti + 90);
            Point tacka52 = new Point(800, visinaCetvrti + 90);
            float[] dashValues = { 5, 2, 15, 4 };
            Pen blackPen2 = new Pen(Color.Black, 2);
            blackPen2.DashPattern = dashValues;
            e.Graphics.DrawLine(blackPen2, tacka51, tacka52);

            int pocetakNaloga = visinaPeti - 30;

            int visinaNalog11 = pocetakNaloga + 90;
            e.Graphics.DrawString("Platilac", new Font("Arial", 9, FontStyle.Bold), Brushes.Black, new Point(levo - 70, pocetakNaloga + 65));
            Point tacka61 = new Point(30, pocetakNaloga + 80);
            Point tacka62 = new Point(300, pocetakNaloga + 80);
            e.Graphics.DrawLine(blackPen, tacka61, tacka62);

            e.Graphics.DrawString(text1, new Font("Arial", 9, FontStyle.Bold), Brushes.Black, new Point(levo - 60, pocetakNaloga + 87));
            e.Graphics.DrawString(text2, new Font("Arial", 9, FontStyle.Bold), Brushes.Black, new Point(levo - 60, pocetakNaloga + 100));

            int visinaNalog12 = visinaNalog11 + 0;
            Point tacka121 = new Point(30, visinaNalog11 - 10);
            Point tacka122 = new Point(30, visinaNalog11 + 30);
            e.Graphics.DrawLine(blackPen, tacka121, tacka122);

            int visinaNalog13 = visinaNalog11 + 0;
            Point tacka131 = new Point(300, visinaNalog11 - 10);
            Point tacka132 = new Point(300, visinaNalog11 + 30);
            e.Graphics.DrawLine(blackPen, tacka131, tacka132);

            int visinaNalog14 = visinaNalog11 + 30;
            Point tacka141 = new Point(30, visinaNalog11 + 30);
            Point tacka142 = new Point(300, visinaNalog11 + 30);
            e.Graphics.DrawLine(blackPen, tacka141, tacka142);

            /*gornja margina svrha uplate*/
            int visinaNalog21 = visinaNalog14 + 30;
            e.Graphics.DrawString("Svrha Uplate", new Font("Arial", 9, FontStyle.Bold), Brushes.Black, new Point(levo - 70, visinaNalog14 + 5));
            Point tacka211 = new Point(30, visinaNalog14 + 20);
            Point tacka212 = new Point(300, visinaNalog14 + 20);
            e.Graphics.DrawLine(blackPen, tacka211, tacka212);

            /*leva margina svrha uplate*/
            int visinaNalog22 = visinaNalog11 + 0;
            Point tacka221 = new Point(30, visinaNalog21 - 10);
            Point tacka222 = new Point(30, visinaNalog21 + 30);
            e.Graphics.DrawLine(blackPen, tacka221, tacka222);

            /*desna margina svrha uplate*/
            int visinaNalog23 = visinaNalog11 + 0;
            Point tacka231 = new Point(300, visinaNalog21 - 10);
            Point tacka232 = new Point(300, visinaNalog21 + 30);
            e.Graphics.DrawLine(blackPen, tacka231, tacka232);

            /*donja margina svrha uplate*/
            int visinaNalog24 = visinaNalog14 + 30;
            e.Graphics.DrawLine(blackPen, tacka222, tacka232);
            e.Graphics.DrawString("Pazar na dan " + date1.ToString("dd.MM.yyyy"), new Font("Arial", 9, FontStyle.Bold), Brushes.Black, new Point(levo - 60, visinaNalog24 + 5));

            /* primalac */
            int visinaNalog31 = visinaNalog24 + 30;
            e.Graphics.DrawString("Primalac", new Font("Arial", 9, FontStyle.Bold), Brushes.Black, new Point(levo - 70, visinaNalog31 + 5));
            Point tacka331 = new Point(30, visinaNalog31 + 20);
            Point tacka332 = new Point(300, visinaNalog31 + 20);
            Point tacka333 = new Point(30, visinaNalog31 + 60);
            Point tacka334 = new Point(300, visinaNalog31 + 60);

            e.Graphics.DrawLine(blackPen, tacka331, tacka332);
            e.Graphics.DrawLine(blackPen, tacka331, tacka333);
            e.Graphics.DrawLine(blackPen, tacka332, tacka334);
            e.Graphics.DrawLine(blackPen, tacka334, tacka333);

            e.Graphics.DrawString(text1, new Font("Arial", 9, FontStyle.Bold), Brushes.Black, new Point(levo - 60, visinaNalog31 + 27));
            e.Graphics.DrawString(text2, new Font("Arial", 9, FontStyle.Bold), Brushes.Black, new Point(levo - 60, visinaNalog31 + 40));

            /* potpis platioca */
            int visinaNalog41 = visinaNalog31 + 80;
            Point tacka411 = new Point(30, visinaNalog41 + 20);
            Point tacka412 = new Point(200, visinaNalog41 + 20);
            e.Graphics.DrawLine(blackPen, tacka411, tacka412);
            e.Graphics.DrawString("Potpis platioca", new Font("Arial", 9, FontStyle.Bold), Brushes.Black, new Point(levo - 40, visinaNalog41 + 25));


            /* mesto i datum prijema */
            int visinaNalog51 = visinaNalog41 + 20;
            Point tacka511 = new Point(200, visinaNalog51 + 20);
            Point tacka512 = new Point(360, visinaNalog51 + 20);
            e.Graphics.DrawLine(blackPen, tacka511, tacka512);
            e.Graphics.DrawString("Mesto i datum prijema", new Font("Arial", 9, FontStyle.Bold), Brushes.Black, new Point(levo + 120, visinaNalog51 + 25));

            /* linija presek */
            int krajNaloga = visinaNalog41 + 20;
            Point tacka611 = new Point(360, pocetakNaloga + 80);
            Point tacka612 = new Point(360, krajNaloga);
            e.Graphics.DrawLine(blackPen, tacka611, tacka612);


            /* desni deo */
            int visinaNalogD1 = pocetakNaloga;
            int paddingNalogD = 410;
            e.Graphics.DrawString("Sifra", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Point(paddingNalogD, pocetakNaloga + 62));
            e.Graphics.DrawString("Placanja", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Point(paddingNalogD, pocetakNaloga + 72));
            e.Graphics.DrawString("Valuta", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Point(paddingNalogD + 80, pocetakNaloga + 72));
            e.Graphics.DrawString("Iznos", new Font("Arial", 9, FontStyle.Bold), Brushes.Black, new Point(paddingNalogD + 160, pocetakNaloga + 70));

            Point tackaD111 = new Point(paddingNalogD, visinaNalogD1 + 85);
            Point tackaD112 = new Point(paddingNalogD + 60, visinaNalogD1 + 85);
            Point tackaD113 = new Point(paddingNalogD, visinaNalogD1 + 110);
            Point tackaD114 = new Point(paddingNalogD + 60, visinaNalogD1 + 110);

            e.Graphics.DrawLine(blackPen, tackaD111, tackaD112);
            e.Graphics.DrawLine(blackPen, tackaD111, tackaD113);
            e.Graphics.DrawLine(blackPen, tackaD113, tackaD114);
            e.Graphics.DrawLine(blackPen, tackaD112, tackaD114);

            Point tackaD121 = new Point(paddingNalogD + 80, visinaNalogD1 + 85);
            Point tackaD122 = new Point(paddingNalogD + 140, visinaNalogD1 + 85);
            Point tackaD123 = new Point(paddingNalogD + 80, visinaNalogD1 + 110);
            Point tackaD124 = new Point(paddingNalogD + 140, visinaNalogD1 + 110);

            e.Graphics.DrawLine(blackPen, tackaD121, tackaD122);
            e.Graphics.DrawLine(blackPen, tackaD121, tackaD123);
            e.Graphics.DrawLine(blackPen, tackaD122, tackaD124);
            e.Graphics.DrawLine(blackPen, tackaD123, tackaD124);

            Point tackaD131 = new Point(paddingNalogD + 160, visinaNalogD1 + 85);
            Point tackaD132 = new Point(paddingNalogD + 320, visinaNalogD1 + 85);
            Point tackaD133 = new Point(paddingNalogD + 160, visinaNalogD1 + 110);
            Point tackaD134 = new Point(paddingNalogD + 320, visinaNalogD1 + 110);

            e.Graphics.DrawLine(blackPen, tackaD131, tackaD132);
            e.Graphics.DrawLine(blackPen, tackaD131, tackaD133);
            e.Graphics.DrawLine(blackPen, tackaD132, tackaD134);
            e.Graphics.DrawLine(blackPen, tackaD133, tackaD134);
            e.Graphics.DrawString(pazar.ToString("N2", CultureInfo.InvariantCulture), new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(paddingNalogD + 180, pocetakNaloga + 90));
            e.Graphics.DrawString("165", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(paddingNalogD + 10, pocetakNaloga + 90));

            /* racun */
            int visinaNalogD2 = visinaNalogD1 + 110;
            e.Graphics.DrawString("Racun platioca", new Font("Arial", 9, FontStyle.Bold), Brushes.Black, new Point(paddingNalogD, visinaNalogD2));
            Point tackaD211 = new Point(paddingNalogD, visinaNalogD2 + 15);
            Point tackaD212 = new Point(paddingNalogD + 320, visinaNalogD2 + 15);
            Point tackaD213 = new Point(paddingNalogD, visinaNalogD2 + 45);
            Point tackaD214 = new Point(paddingNalogD + 320, visinaNalogD2 + 45);

            e.Graphics.DrawLine(blackPen, tackaD211, tackaD212);
            e.Graphics.DrawLine(blackPen, tackaD211, tackaD213);
            e.Graphics.DrawLine(blackPen, tackaD212, tackaD214);
            e.Graphics.DrawLine(blackPen, tackaD213, tackaD214);

            e.Graphics.DrawString(racun, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(paddingNalogD + 10, visinaNalogD2 + 25));

            /* racun */
            int visinaNalogD3 = visinaNalogD2 + 50;
            e.Graphics.DrawString("Model i poziv na broj", new Font("Arial", 9, FontStyle.Bold), Brushes.Black, new Point(paddingNalogD, visinaNalogD3));
            Point tackaD311 = new Point(paddingNalogD, visinaNalogD3 + 15);
            Point tackaD312 = new Point(paddingNalogD + 60, visinaNalogD3 + 15);
            Point tackaD313 = new Point(paddingNalogD, visinaNalogD3 + 45);
            Point tackaD314 = new Point(paddingNalogD + 60, visinaNalogD3 + 45);

            e.Graphics.DrawLine(blackPen, tackaD311, tackaD312);
            e.Graphics.DrawLine(blackPen, tackaD311, tackaD313);
            e.Graphics.DrawLine(blackPen, tackaD312, tackaD314);
            e.Graphics.DrawLine(blackPen, tackaD313, tackaD314);

            Point tackaD321 = new Point(paddingNalogD + 80, visinaNalogD3 + 15);
            Point tackaD322 = new Point(paddingNalogD + 80 + 240, visinaNalogD3 + 15);
            Point tackaD323 = new Point(paddingNalogD + 80, visinaNalogD3 + 45);
            Point tackaD324 = new Point(paddingNalogD + 80 + 240, visinaNalogD3 + 45);

            e.Graphics.DrawLine(blackPen, tackaD321, tackaD322);
            e.Graphics.DrawLine(blackPen, tackaD321, tackaD323);
            e.Graphics.DrawLine(blackPen, tackaD322, tackaD324);
            e.Graphics.DrawLine(blackPen, tackaD323, tackaD324);

            string pozivNaBroj = "";
            if (provera == "DA")
            {
                string objekatString = "";
                int count = comboBox1.SelectedValue.ToString().Length;
                if (count < 2)
                {
                    objekatString = "0" + comboBox1.SelectedValue.ToString();
                }
                else
                {
                    objekatString = comboBox1.SelectedValue.ToString();
                }
                pozivNaBroj = "MP" + objekatString + date1.ToString("yyyyMMdd");
            }
            else
            {
                pozivNaBroj = "MP" + objekat + date1.ToString("yyyyMMdd");
            }
            e.Graphics.DrawString(pozivNaBroj, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(paddingNalogD + 120, visinaNalogD3 + 20));

            /* mesto i datum izvrsenja */
            int visinaD411 = visinaNalog41 + 20;
            Point tackaD511 = new Point(paddingNalogD, visinaNalog51 + 20);
            Point tackaD512 = new Point(paddingNalogD + 160, visinaNalog51 + 20);
            e.Graphics.DrawLine(blackPen, tackaD511, tackaD512);
            e.Graphics.DrawString("Datum izvrsenja", new Font("Arial", 9, FontStyle.Bold), Brushes.Black, new Point(paddingNalogD + 30, visinaNalog51 + 25));
        }

        public static string SpliceText(string text, int lineLength)
        {
            var charCount = 0;
            var lines = text.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                            .GroupBy(w => (charCount += w.Length + 1) / lineLength)
                            .Select(g => string.Join(" ", g));

            return String.Join("\n", lines.ToArray());
        }

        private void frmPrometiPoRadnjama_Load(object sender, EventArgs e)
        {
            var MyIni = new IniFile(@"C:\bkapps\config.ini");
            var provera = MyIni.Read("magacin", "ProveraDokumenta");
            var system = MyIni.Read("system", "ProveraDokumenta");

            if (provera == "DA")
            {
                comboBox1.Visible = true;
            }
            else { comboBox1.Visible = false; }

            System.ComponentModel.ComponentResourceManager resources =
    new System.ComponentModel.ComponentResourceManager(typeof(frmPrometiPoRadnjama));
            this.Icon = Properties.Resources.main_favicon;
            if (system == "watch")
            {
                this.Icon = Properties.Resources.watch_icon;
            }


        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            Form1 Form1 = new Form1();
            Form1.Show();
        }
    }
}
