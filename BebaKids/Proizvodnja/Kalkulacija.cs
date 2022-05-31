using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BebaKids.Properties;
using System.IO;
using System.Net;
using System.Data.Odbc;
using MySql.Data.MySqlClient;
using System.Drawing.Printing;
using System.Printing;
using System.ServiceProcess;



namespace BebaKids.Proizvodnja
{
    public partial class Kalkulacija : Form
    {
        Image check = Resources.check;
        Image delete = Resources.delete;
        Image search = Resources.search;
        Image document = Resources.document;
        Image no_image = Resources.no_image;
        Classes.Proizvodnja pr = new Classes.Proizvodnja();
        //DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DataTable dtCopy = new DataTable();
        PrintDocument printD = new PrintDocument();


        public Kalkulacija()
        {
            InitializeComponent();

            /*ds.Tables.Add("Items");
            ds.Tables[0].Columns.Add();
            ds.Tables[0].Columns.Add();
            ds.Tables[0].Columns.Add();
            ds.Tables[0].Columns.Add();
            ds.Tables[0].Columns.Add();
            ds.Tables[0].Columns.Add();
            ds.Tables[0].Columns.Add();*/
            dt.Columns.Add("vrsta");
            dt.Columns.Add("kartica");
            dt.Columns.Add("materijal");
            dt.Columns.Add("cena");
            dt.Columns.Add("utrosak");
            dt.Columns.Add("suma");
            dt.Columns.Add("napomena");

            disableAll(true);
            groupBox1.Visible = false;

            int red = tableLayoutPanel1.RowCount;
            dt.Clear();
            for (int i = 0; i < red; i++)
            {
                Label label = new Label();
                label.Text = i + 1 + ".";
                label.Anchor = AnchorStyles.None;
                tableLayoutPanel1.Controls.Add(label, 0, i);
                CheckBox op = new CheckBox();
                op.Anchor = AnchorStyles.None;
                tableLayoutPanel1.Controls.Add(op, 1, i);
                TextBox kartica = new TextBox();
                kartica.KeyDown += new KeyEventHandler((sender, e) => textBox1_KeyDown(sender, e));
                kartica.Anchor = AnchorStyles.None;
                tableLayoutPanel1.Controls.Add(kartica, 2, i);
                TextBox materijal = new TextBox();
                materijal.KeyDown += new KeyEventHandler((sender, e) => textBox1_KeyDown(sender, e));
                materijal.Anchor = AnchorStyles.None;
                tableLayoutPanel1.Controls.Add(materijal, 3, i);
                TextBox cena = new TextBox();
                cena.KeyDown += new KeyEventHandler((sender, e) => textBox1_KeyDown(sender, e));
                cena.Anchor = AnchorStyles.None;
                tableLayoutPanel1.Controls.Add(cena, 4, i);
                cena.Text = "0".ToString();
                TextBox utrosak = new TextBox();
                utrosak.KeyDown += new KeyEventHandler((sender, e) => textBox1_KeyDown(sender, e));
                utrosak.Anchor = AnchorStyles.None;
                tableLayoutPanel1.Controls.Add(utrosak, 5, i);
                TextBox suma = new TextBox();
                suma.KeyDown += new KeyEventHandler((sender, e) => textBox1_KeyDown(sender, e));
                suma.Anchor = AnchorStyles.None;
                tableLayoutPanel1.Controls.Add(suma, 6, i);
                TextBox napomena = new TextBox();
                napomena.KeyDown += new KeyEventHandler((sender, e) => textBox1_KeyDown(sender, e));
                napomena.Anchor = AnchorStyles.None;
                tableLayoutPanel1.Controls.Add(napomena, 7, i);

                kartica.Leave += new EventHandler((sender, e) => kartica_Leave(sender, e, materijal, cena, op));
                utrosak.Leave += new EventHandler((sender, e) => utrosak_Leave(sender, e, cena, suma));
                btnOzvezi.Click += new EventHandler((sender, e) => pbCheck_Click(sender, e, op, kartica, materijal, cena, utrosak, suma, napomena));
            }
            btnOzvezi.Click += new EventHandler(btnOzvezi_Click);
            pbCheck.Click += new EventHandler(table);

        }

        public void disableAll(bool vrsta)
        {
            foreach (TextBox tb in this.groupBox2.Controls.OfType<TextBox>())
            {
                tb.Text = "";
                //tb.BackColor = Color.AntiqueWhite;
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            //
            // Detect the KeyEventArg's key enumerated constant.
            //
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }


        private void tbSifra_Leave(object sender, EventArgs e)
        {

            Stream read = pr.getImage(tbSifra.Text.ToString());
            try
            {
                var image = Image.FromStream(read);
                ptSlika.Image = image;
            }
            catch
            {
                var image = no_image;
                ptSlika.Image = image;
            }
            DataTable roba = new DataTable();
            try
            {
                roba = pr.getRoba(tbSifra.Text.ToString());
                lbNazRob.Text = roba.Rows[0].Field<string>("naz_rob");
                tbKurs.Text = roba.Rows[0].Field<float>("kurs").ToString();
            }
            catch { lbNazRob.Text = "Ne postoji sifra, ili je polje prazno"; }
        }

        private void btSearch_Click(object sender, EventArgs e)
        {
            btnObrisi.Visible = true;
            gbTotal.Visible = false;
            groupBox1.Visible = false;

            Classes.Proizvodnja pr = new Classes.Proizvodnja();
            Stream read = pr.getImage(tbSifra.Text.ToString());
            try
            {
                var image = Image.FromStream(read);
                ptSlika.Image = image;
            }
            catch
            {
                var image = no_image;
                ptSlika.Image = image;
            }

            searchButton(tbSifra.Text.ToString());

        }

        private void Kalkulacija_Load(object sender, EventArgs e)
        {
            pbCheck.Visible = false;
            gbZaglavlje.Visible = true;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            Form1 Form1 = new Form1();
            Form1.Show();
        }

        public void btNew_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            gbTotal.Visible = true;
            groupBox2.Visible = false;
            disableAll(true);


        }

        public void kartica_Leave(object sender, EventArgs e, TextBox materijal, TextBox cena, CheckBox op)
        {
            var kartica = sender as TextBox;
            Classes.Proizvodnja pr = new Classes.Proizvodnja();
            DataTable tabela = new DataTable();
            DataTable roba = new DataTable();
            if (kartica != null && kartica.Text.Length > 0)
            {

                try
                {
                    tabela = pr.getKartica(kartica.Text.ToString(), op.Checked);
                    materijal.Text = tabela.Rows[0].Field<string>("parent_fabric");
                    decimal price = tabela.Rows[0].Field<decimal>("price_material");
                    cena.Text = price.ToString();
                }
                catch (Exception ex) { MessageBox.Show("Ne postoji kartica!"); }
            }
            roba = pr.getRoba(tbSifra.Text.ToString());
            try
            { string robas = roba.Rows[0].Field<string>("naz_rob"); }
            catch { MessageBox.Show("Ne postoji roba !!!\nNe mozete nastaviti!", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }
        public void utrosak_Leave(object sender, EventArgs e, TextBox cena, TextBox suma)
        {
            suma.BackColor = Color.White;
            var utrosak = sender as TextBox;
            decimal tCena = 0;
            decimal tUtrosak = 0;
            try
            {
                tCena = Convert.ToDecimal(cena.Text);
            }
            catch { MessageBox.Show("Cena moze biti samo broj", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            try
            {
                tUtrosak = Convert.ToDecimal(utrosak.Text);
            }
            catch { MessageBox.Show("Utrosak moze biti samo broj", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            decimal tSuma = tCena * tUtrosak;
            if (tSuma <= 0)
            {
                suma.BackColor = Color.Red;
            }
            suma.Text = tSuma.ToString();


        }

        private void table(object sender, EventArgs e)
        {
            if (!cbKonacna.Checked && !cbPlanska.Checked)
            {
                MessageBox.Show("Niste odabrali vrstu kalkulacije", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (tbSifra == null && tbSifra.Text.Length == 0)
            {
                MessageBox.Show("Niste uneli sifru", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (tbKolicina == null && tbKolicina.Text.Length == 0)
            {
                MessageBox.Show("Niste uneli kolicinu", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string vrsta = "";
                if (cbKonacna.Checked)
                {
                    vrsta = "K";
                }
                else if (cbPlanska.Checked)
                {
                    vrsta = "P";
                }
                else MessageBox.Show("Niste odabrali vrstu kalkulacije", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                DataTable tabela = new DataTable();
                tabela = pr.getRoba(tbSifra.Text.ToString());
                //float kurs = tabela.Rows[0].Field<float>("kurs");

                MySqlConnection mysql = new MySqlConnection(MysqlKonekcija.myConnectionString);
                MySqlCommand command = mysql.CreateCommand();
                command.CommandText = "insert into kalkulacija (sif_rob,type,kolic) values ('" + tbSifra.Text.ToString() + "','" + vrsta + "'," + Convert.ToInt32(tbKolicina.Text.ToString()) + ")";
                mysql.Open();
                long id = 0;
                try
                {

                    command.ExecuteNonQuery();
                    id = command.LastInsertedId;


                    foreach (DataRow dr in dtCopy.Rows)
                    {
                        MySqlCommand cmd = mysql.CreateCommand();
                        cmd.CommandText = "insert into kalkulacija_items (id_kalkulacije,id_kartice,naziv_mat,cena_mat,utrosak,vrednost,napomena) values (@idKalkulacije,@idKartice,@nazivMat,@cenaMat,@utrosak,@vrednost,@napomena)";
                        cmd.Parameters.AddWithValue("@idKalkulacije", id);
                        cmd.Parameters.AddWithValue("@idKartice", dr["kartica"].ToString());
                        cmd.Parameters.AddWithValue("@nazivMat", dr["materijal"].ToString());
                        cmd.Parameters.AddWithValue("@cenaMat", Convert.ToDouble(dr["cena"].ToString()));
                        cmd.Parameters.AddWithValue("@utrosak", Convert.ToDouble(dr["utrosak"].ToString()));
                        cmd.Parameters.AddWithValue("@vrednost", Convert.ToDouble(dr["suma"].ToString()));
                        cmd.Parameters.AddWithValue("@napomena", dr["napomena"].ToString());

                        try
                        {
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }

                    double planska = Convert.ToDouble(tbPlanskaRsd.Text);
                    double planskaEur = Convert.ToDouble(tbPlanskaEur.Text);
                    double vp = Convert.ToDouble(tbVpRsd.Text);
                    double vpEur = Convert.ToDouble(tbVpEur.Text);
                    double mp = Convert.ToDouble(tbMpRsd.Text);
                    double mpEur = Convert.ToDouble(tbMpEur.Text);

                    MySqlCommand update = mysql.CreateCommand();
                    update.CommandText = "update roba set cen_kos = " + planska + ", cen_kos_eur = " + planskaEur + ",vp = " + vp + ",vp_eur = " + vpEur + ",mp=" + mp + ",mp_eur=" + mpEur + " where sif_rob = '" + tbSifra.Text.ToString() + "'";
                    update.ExecuteNonQuery();

                    mysql.Close();

                    MessageBox.Show("Uspeno uneta kalkulacija", "Obavestenje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnPrint_Click(btnPrint, null);


                    dtCopy.Clear();
                    dt.Clear();
                    tbSifra.Text = "";
                    //obrisiTabelu();
                    this.Close();
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                    MessageBox.Show("Dupli lokakni kljuc", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }


            }
        }

        private void pbCheck_Click(object sender, EventArgs e, CheckBox op, TextBox kartica, TextBox materijal, TextBox cena, TextBox utrosak, TextBox suma, TextBox napomena)
        {

            if (suma != null && suma.Text.Length > 0)
            {
                string vrsta = "";
                if (op.Checked)
                {
                    vrsta = "P";
                }
                else { vrsta = "O"; }
                string tKartica = kartica.Text.ToString();
                string tMaterijal = materijal.Text.ToString();
                string tNapomena = napomena.Text.ToString();
                double tCena = Convert.ToDouble(cena.Text);
                double tUtrosak = Convert.ToDouble(utrosak.Text);
                double tSuma = Convert.ToDouble(suma.Text);

                DataRow dr = dt.NewRow();
                dr[0] = vrsta.ToString();
                dr[1] = tKartica;
                dr[2] = tMaterijal;
                dr[3] = tCena.ToString();
                dr[4] = tUtrosak.ToString();
                dr[5] = tSuma.ToString();
                dr[6] = tNapomena;
                dt.Rows.Add(dr);
            }
        }

        private void getTotalSuma(decimal vrednost)
        {

        }

        private void btnOzvezi_Click(object sender, EventArgs e)
        {
            double totalSuma = 0;
            foreach (DataRow dr in dt.Rows)
            {
                totalSuma += Convert.ToDouble(dr[5]);
            }
            tbTotalSuma.Text = totalSuma.ToString();
            dtCopy = dt;
            //dt.Clear();

            double planska = Convert.ToDouble(tbTotalSuma.Text.ToString());


            DataTable tabela = new DataTable();
            tabela = pr.getRoba(tbSifra.Text.ToString());
            float kurs = tabela.Rows[0].Field<float>("kurs");
            double marza = tabela.Rows[0].Field<double>("marza") + 1;

            tbPlanskaEur.Text = Math.Round(planska * marza, 2).ToString();
            tbPlanskaRsd.Text = Math.Round(planska * marza * kurs, 2).ToString();
            tbVpEur.Text = 0.ToString();
            tbPlanskaMarza.Text = String.Format("{0:P2}", marza - 1);
            tbVpMarza.Text = 100.ToString();

            btnOzvezi.Enabled = false;
            btnPonisti.Visible = true;

        }

        private void obrisiTabelu()
        {
            dt.Clear();
            tbSifra.Text = "";
            this.Close();
            this.Show();
        }

        private void vpCena_Leave(object sender, EventArgs e)
        {
            DataTable tabela = new DataTable();
            tabela = pr.getRoba(tbSifra.Text.ToString());
            float kurs = tabela.Rows[0].Field<float>("kurs");
            double vp = Convert.ToDouble(tbVpEur.Text);
            double pl = Convert.ToDouble(tbPlanskaEur.Text);

            double marza = Math.Round(((vp / pl) - 1), 2);
            tbVpMarza.Text = String.Format("{0:P2}", marza);
            tbMpEur.Text = (vp * 2).ToString();
            tbVpRsd.Text = Math.Round(vp * kurs, 2).ToString();
            double mp = Math.Round((vp * 2 * kurs) / 100, 0) * 100 - 10;
            tbMpRsd.Text = mp.ToString();
            pbCheck.Visible = true;
        }

        private void searchButton(string sifra)
        {
            string vrsta = "";
            if (cbKonacna.Checked)
            {
                vrsta = "K";
            }
            else if (cbPlanska.Checked)
            {
                vrsta = "P";
            }
            if (!string.IsNullOrEmpty(tbSifra.Text.ToString()) && vrsta != "")
            {
                groupBox2.Visible = true;
                Classes.Proizvodnja pr = new Classes.Proizvodnja();

                DataTable kalkulacija = new DataTable();
                kalkulacija = pr.kalkulacijaItems(tbSifra.Text.ToString(), vrsta);
                groupBox2.Location = new Point(15, 170);
                btnPrint.DataSource = kalkulacija;
                int rowCount = kalkulacija.Rows.Count;
                if (rowCount > 0)
                {
                    kalkulacija = pr.kalkulacija(tbSifra.Text.ToString(), vrsta);
                    int kolicina = kalkulacija.Rows[0].Field<int>("kolic");
                    double cenKos = kalkulacija.Rows[0].Field<double>("cen_kos");
                    double cenKosEur = kalkulacija.Rows[0].Field<double>("cen_kos_eur");
                    double vp = kalkulacija.Rows[0].Field<double>("vp");
                    double vpEur = kalkulacija.Rows[0].Field<double>("vp_eur");
                    double marza = kalkulacija.Rows[0].Field<double>("marza");
                    double mp = kalkulacija.Rows[0].Field<double>("mp");
                    double mpEur = kalkulacija.Rows[0].Field<double>("mp_eur");

                    double marzaVp = Math.Round(((vpEur / cenKosEur) - 1), 2);

                    tbKolicina.Text = kolicina.ToString();
                    tb1PlanskaRsd.Text = cenKos.ToString();
                    tb1PlanskaEur.Text = cenKosEur.ToString();
                    tb1MarzlaPlanska.Text = String.Format("{0:P2}", marza - 1);
                    tb1VpRsd.Text = vp.ToString();
                    tb1VpEur.Text = vpEur.ToString();
                    tb1VpMarza.Text = String.Format("{0:P2}", marzaVp);
                    tb1Mp.Text = mpEur.ToString();
                    tb1MpRsd.Text = mp.ToString();

                    buttonPrint.Visible = true;

                }
                else { MessageBox.Show("Ne postoji sifra sa izabranom vrstom", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
            else { MessageBox.Show("Niste uneli sifru ili vrstu", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }

        private void btnPrint_Click(object sender, EventArgs e)
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

        }

        private void printDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Classes.Proizvodnja pr = new Classes.Proizvodnja();
            Image artikal;
            string vrsta = "";
            if (cbKonacna.Checked)
            {
                vrsta = "K";
            }
            else if (cbPlanska.Checked)
            {
                vrsta = "P";
            }
            Stream read = pr.getImage(tbSifra.Text.ToString());
            try
            {
                artikal = Image.FromStream(read);

            }
            catch
            {
                artikal = Resources.no_image;
            }

            DataTable kalkulacijaItems = new DataTable();

            DataTable kalkulacija = new DataTable();
            kalkulacija = pr.kalkulacijaItems(tbSifra.Text.ToString(), vrsta);

            double cenKos = 0;
            double cenKosEur = 0;
            double vp = 0;
            double vpEur = 0;
            double marza = 0;
            double mp = 0;
            double mpEur = 0;
            double marzaVp = 0;
            string naziv = "";
            string kolekcija = "";
            string sifra = "";

            int rowCount = kalkulacija.Rows.Count;
            if (rowCount > 0)
            {
                kalkulacija = pr.kalkulacija(tbSifra.Text.ToString(), vrsta);
                int kolicina = kalkulacija.Rows[0].Field<int>("kolic");
                cenKos = kalkulacija.Rows[0].Field<double>("cen_kos");
                cenKosEur = kalkulacija.Rows[0].Field<double>("cen_kos_eur");
                vp = kalkulacija.Rows[0].Field<double>("vp");
                vpEur = kalkulacija.Rows[0].Field<double>("vp_eur");
                marza = kalkulacija.Rows[0].Field<double>("marza");
                mp = kalkulacija.Rows[0].Field<double>("mp");
                mpEur = kalkulacija.Rows[0].Field<double>("mp_eur");
                //krus = kalkulacija.Rows[0].Field<double>("kurs");
                naziv = kalkulacija.Rows[0].Field<string>("naz_rob");
                kolekcija = kalkulacija.Rows[0].Field<string>("kolekcija");
                sifra = kalkulacija.Rows[0].Field<string>("sif_rob");


                marzaVp = Math.Round(((vpEur / cenKosEur) - 1), 2);
            }

            kalkulacijaItems = pr.kalkulacijaItems(sifra.ToString(), vrsta);

            Image logo = Resources.logoHeader;
            int sirina = logo.Width;
            int visina = 0;
            visina += logo.Height;
            e.Graphics.DrawImage(logo, 25, 25, logo.Width, logo.Height);

            e.Graphics.DrawString("Kalkulacija utroska materijala", new Font("Arial", 18, FontStyle.Bold), Brushes.Black, new Point(100, visina + 50));
            e.Graphics.DrawImage(artikal, logo.Width - 210, visina + 50, 200, 310);
            int pocetakItema = visina + 50 + 330;
            visina = 250;
            int pocetak = 100;
            e.Graphics.DrawString("Artikal : " + sifra, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(pocetak, visina));
            visina += 30;
            e.Graphics.DrawString("Naziv : " + naziv, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(pocetak, visina));
            visina += 30;
            e.Graphics.DrawString("Kolekcija : " + kolekcija, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(pocetak, visina));
            visina += 30;
            int cene = pocetak;
            e.Graphics.DrawString("Planska cena rsd: " + cenKos, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(pocetak, visina));
            cene += 220;
            e.Graphics.DrawString("Planska cena \u20AC : " + cenKosEur, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(cene, visina));
            visina += 30;
            e.Graphics.DrawString("Planska marza \u0025 : " + String.Format("{0:P2}", marza), new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(pocetak, visina));
            e.Graphics.DrawString("Vp marza \u0025 : " + String.Format("{0:P2}", marzaVp), new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(pocetak + 220, visina));
            visina += 30;
            e.Graphics.DrawString("Vp cena rsd : " + vp, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(pocetak, visina));
            e.Graphics.DrawString("VP cena \u20AC : " + vpEur, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(pocetak + 220, visina));
            visina += 30;
            e.Graphics.DrawString("MP cena rsd : " + mp, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(pocetak, visina));
            e.Graphics.DrawString("MP cena \u20AC : " + mpEur, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(pocetak + 220, visina));
            Pen blackPen = new Pen(Color.Black, 3);
            Point tacka1 = new Point(0, pocetakItema);
            Point tacka2 = new Point(1000, pocetakItema);
            e.Graphics.DrawLine(blackPen, tacka1, tacka2);
            int items = pocetakItema + 10;
            int startItems = pocetak - 20;
            e.Graphics.DrawString("Rbr", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(startItems, items));
            e.Graphics.DrawString("Kartica", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(startItems + 50, items));
            e.Graphics.DrawString("Materijal", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(startItems + 140, items));
            e.Graphics.DrawString("Cena", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(startItems + 290, items));
            e.Graphics.DrawString("Utrosak", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(startItems + 350, items));
            e.Graphics.DrawString("Suma", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(startItems + 425, items));
            e.Graphics.DrawString("Napomena", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(startItems + 490, items));
            Pen line = new Pen(Color.Black, 1);
            Point tacka12 = new Point(0, items + 30);
            Point tacka22 = new Point(1000, items + 30);
            e.Graphics.DrawLine(line, tacka12, tacka22);
            double totalSuma = 0;
            int rbr = 1;
            int foreachItems = items + 30;
            foreach (DataRow dr in kalkulacijaItems.Rows)
            {
                e.Graphics.DrawString(rbr.ToString() + ".", new Font("Arial", 11, FontStyle.Regular), Brushes.Black, new Point(startItems, foreachItems + 3));
                e.Graphics.DrawString(dr["kartica"].ToString(), new Font("Arial", 11, FontStyle.Regular), Brushes.Black, new Point(startItems + 50, foreachItems + 4));
                e.Graphics.DrawString(dr["materijal"].ToString(), new Font("Arial", 11, FontStyle.Regular), Brushes.Black, new Point(startItems + 140, foreachItems + 3));
                e.Graphics.DrawString(dr["cena_mat"].ToString(), new Font("Arial", 11, FontStyle.Regular), Brushes.Black, new Point(startItems + 290, foreachItems + 3));
                e.Graphics.DrawString(dr["utrosak"].ToString(), new Font("Arial", 11, FontStyle.Regular), Brushes.Black, new Point(startItems + 350, foreachItems + 3));
                e.Graphics.DrawString(dr["vrednost"].ToString(), new Font("Arial", 11, FontStyle.Regular), Brushes.Black, new Point(startItems + 425, foreachItems + 3));
                e.Graphics.DrawString(dr["napomena"].ToString(), new Font("Arial", 11, FontStyle.Regular), Brushes.Black, new Point(startItems + 490, foreachItems + 3));
                e.Graphics.DrawLine(new Pen(Color.Black, 1), new Point(0, foreachItems + 22), new Point(1000, foreachItems + 22));
                foreachItems += 22;
                rbr += 1;
                totalSuma += Convert.ToDouble(dr["vrednost"].ToString());
            }

            e.Graphics.DrawString("Total: " + totalSuma.ToString(), new Font("Arial", 11, FontStyle.Regular), Brushes.Black, new Point(startItems + 425, foreachItems + 20));

        }

        void printingDocument_Click(object sender, EventArgs e)
        {
            PrintDialog printdlg = new PrintDialog();
            PrinterSettings settings = new PrinterSettings();
            string printerName = settings.PrinterName;
            //printdlg.ShowDialog();
            //printD.DefaultPageSettings.PrinterSettings.PrintToFile = true;
            //printD.DefaultPageSettings.PrinterSettings.PrintFileName = "d:\\ddd112.pdf";
            printD.PrintController = new StandardPrintController();
            if (printdlg.ShowDialog() == DialogResult.OK)
            {
                printD.Print();
            }
            //var fileStream = new FileStream("d:\\ddd112.pdf",FileMode.Open,FileAccess.Read);

            try { System.IO.File.Delete("d:\\ddd112.pdf"); }
            catch { }

        }

        private void btnObrisi_Click(object sender, EventArgs e)
        {
            string vrsta = "";
            if (cbKonacna.Checked)
            {
                vrsta = "K";
            }
            else if (cbPlanska.Checked)
            {
                vrsta = "P";
            }
            Classes.Proizvodnja pr = new Classes.Proizvodnja();
            if (tbSifra != null && tbSifra.Text.Length > 0)
            {
                pr.btnObrisi(tbSifra.Text.ToString(), vrsta);
                MessageBox.Show("Uspesno ste obrisali sifru", "Obavestenje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void btnPonisti_Click(object sender, EventArgs e)
        {
            dt.Clear();
            btnPonisti.Visible = false;
            btnOzvezi.Enabled = true;
        }
    }


}
