using System;
using System.Data;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BebaKids
{
    public partial class tbPSPresek : Form
    {
        public tbPSPresek()
        {
            InitializeComponent();
            button1.Visible = true;
        }

        private void frmDnevniPromet_Load(object sender, EventArgs e)
        {
            var MyIni = new IniFile(@"C:\bkapps\config.ini");
            var objekat = MyIni.Read("sif_obj_mp", "ProveraDokumenta").ToString();
            var baza = MyIni.Read("baza", "ProveraDokumenta").ToString();
            var database = MyIni.Read("database", "ProveraDokumenta").ToString();
            var system = MyIni.Read("system", "ProveraDokumenta").ToString();

            this.Icon = Properties.Resources.main_favicon;
            if (system == "watch")
            {
                this.Icon = Properties.Resources.watch_icon;
            }

            string cmd = "select trim(kbk.sif_inst) banka,round(sum(iznos),2) vrednost from kasa_blok_kartice kbk " +
                         "left join inst k on k.sif_inst = kbk.sif_inst where kbk.sif_obj_mp = '" + objekat + "' and kbk.datum = today group by banka ";


            if (database == "DA")
            {
                cmd = "select trim(kbk.sif_inst) banka,round(sum(iznos),2) vrednost from " + baza + ":kasa_blok_kartice kbk " +
                         "left join inst k on k.sif_inst = kbk.sif_inst where kbk.sif_obj_mp = '" + objekat + "' and kbk.datum = today group by banka ";
            }





            string connString = "Dsn=ifx;uid=informix";

            OdbcConnection conn = new OdbcConnection(connString);
            DataTable table = new DataTable();
            conn.Open();
            OdbcDataAdapter adapter = new OdbcDataAdapter(cmd, conn);
            adapter.Fill(table);
            conn.Close();

            vrednostBI.Text = (from DataRow dr in table.Rows where (string)dr["banka"] == "160" select (decimal)dr["vrednost"]).FirstOrDefault().ToString() + " rsd";
            vrednostUni.Text = (from DataRow dr in table.Rows where (string)dr["banka"] == "170" select (decimal)dr["vrednost"]).FirstOrDefault().ToString() + " rsd";

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
            Point tacka12 = new Point(30, 50);
            Point tacka22 = new Point(width - 30, 50);
            g.DrawLine(myPen, tacka12, tacka22);

            Pen litlePen = new Pen(color);
            litlePen.Width = 3;
            Point tackal1 = new Point(30, 180);
            Point tackal2 = new Point(width - 30, 180);
            g.DrawLine(litlePen, tackal1, tackal2);

            Point tackal11 = new Point(30, 360);
            Point tackal21 = new Point(width - 30, 360);
            g.DrawLine(litlePen, tackal11, tackal21);

            Point tackal13 = new Point(30, 485);
            Point tackal24 = new Point(width - 30, 485);
            g.DrawLine(litlePen, tackal13, tackal24);

            Point tackal15 = new Point(30, 585);
            Point tackal26 = new Point(width - 30, 585);
            g.DrawLine(litlePen, tackal15, tackal26);

            Pen redPen = new Pen(Color.Red);
            redPen.Width = 2;
            //g.DrawRectangle(redPen,new Rectangle (40,60,175,115));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var MyIni = new IniFile(@"C:\bkapps\config.ini");
            string baza = MyIni.Read("konekcija", "Baza").ToString();
            SqlConnection sqlConn = new SqlConnection(baza);

            StringBuilder sb = new StringBuilder();
            sb.Append("insert into pazari (sif_obj_mp,date,cache,card,cek,presek,presek_pdv,cek_redovni,cek_odlozeni,administrativna,konzul,vaucer,napomena) values");
            sb.Append("(@objekat,@date,@cache,@card,@cek,@presek,@presekPdv,@cekRedovni,@cekOdlozeni,@administrativna,@konzul,@vaucer,@napomena)SELECT SCOPE_IDENTITY();");
            SqlCommand komanda = new SqlCommand(sb.ToString(), sqlConn);
            String sif_obj_mp = MyIni.Read("sif_obj_mp", "ProveraDokumenta").ToString();
            String database = MyIni.Read("database", "ProveraDokumenta").ToString();
            if (database == "DA")
            {
                sif_obj_mp = "1" + sif_obj_mp;
            }
            DateTime thisDay = DateTime.Now;
            string dateFormat = "yyyy-MM-dd";
            Decimal presek = Decimal.TryParse(tbPresek.Text.ToString(), out presek) ? presek : 0;
            Decimal presek_pdv = Decimal.TryParse(tbPSPdv.Text.ToString(), out presek_pdv) ? presek_pdv : 0;
            Decimal cache = Decimal.TryParse(tbDIGotovina.Text.ToString(), out cache) ? cache : 0;
            Decimal card = Decimal.TryParse(tbDIKartice.Text.ToString(), out card) ? card : 0;
            Decimal cek = Decimal.TryParse(tbDICek.Text.ToString(), out cek) ? cek : 0;
            Decimal cek_redovni = Decimal.TryParse(tbACRedovni.Text.ToString(), out cek_redovni) ? cek_redovni : 0;
            Decimal cek_odlozeni = Decimal.TryParse(tbACOdlozeni.Text.ToString(), out cek_odlozeni) ? cek_odlozeni : 0;
            Decimal administrativna = Decimal.TryParse(tbACAdmimistrativne.Text.ToString(), out administrativna) ? administrativna : 0;
            Decimal konzul = Decimal.TryParse(tbKonzul.Text.ToString(), out konzul) ? konzul : 0;
            Decimal vaucer = Decimal.TryParse(tbVauceri.Text.ToString(), out vaucer) ? vaucer : 0;
            String napomena = tbNapomena.Text.ToString();
            //komanda.Parameters.Add("@oznaka", SqlDbType.Char).Value = id + "-" + sif_obj_mp + "-" + thisDay.ToString(dateFormat);

            SqlParameter paramObjekat = new SqlParameter("@objekat", SqlDbType.Char);
            SqlParameter paramDate = new SqlParameter("@date", SqlDbType.Date);
            SqlParameter paramCache = new SqlParameter("@cache", SqlDbType.Decimal);
            SqlParameter paramCard = new SqlParameter("@card", SqlDbType.Decimal);
            SqlParameter paramCek = new SqlParameter("@cek", SqlDbType.Decimal);
            SqlParameter paramPresek = new SqlParameter("@presek", SqlDbType.Decimal);
            SqlParameter paramPresekPdv = new SqlParameter("@presekPdv", SqlDbType.Decimal);
            SqlParameter paramCekRedovni = new SqlParameter("@cekRedovni", SqlDbType.Decimal);
            SqlParameter paramCekOdlozeni = new SqlParameter("@cekOdlozeni", SqlDbType.Decimal);
            SqlParameter paramAdministrativna = new SqlParameter("@administrativna", SqlDbType.Decimal);
            SqlParameter paramKonzul = new SqlParameter("@konzul", SqlDbType.Decimal);
            SqlParameter paramVaucer = new SqlParameter("@vaucer", SqlDbType.Decimal);
            SqlParameter paramNapomena = new SqlParameter("@napomena", SqlDbType.Text);

            komanda.Parameters.Add(paramObjekat);
            komanda.Parameters.Add(paramDate);
            komanda.Parameters.Add(paramCache);
            komanda.Parameters.Add(paramCard);
            komanda.Parameters.Add(paramCek);
            komanda.Parameters.Add(paramPresek);
            komanda.Parameters.Add(paramPresekPdv);
            komanda.Parameters.Add(paramCekRedovni);
            komanda.Parameters.Add(paramCekOdlozeni);
            komanda.Parameters.Add(paramAdministrativna);
            komanda.Parameters.Add(paramKonzul);
            komanda.Parameters.Add(paramVaucer);
            komanda.Parameters.Add(paramNapomena);

            paramObjekat.Value = sif_obj_mp;
            paramDate.Value = thisDay.ToString(dateFormat);
            paramCache.Value = cache;
            paramCard.Value = card;
            paramCek.Value = cek;
            paramCekRedovni.Value = cek_redovni;
            paramCekOdlozeni.Value = cek_odlozeni;
            paramPresek.Value = presek;
            paramPresekPdv.Value = presek_pdv;
            paramAdministrativna.Value = administrativna;
            paramKonzul.Value = konzul;
            paramVaucer.Value = vaucer;
            paramNapomena.Value = napomena;

            int id = 0;
            try
            {
                sqlConn.Open();
                id = Convert.ToInt32(komanda.ExecuteScalar());
                //MessageBox.Show(id.ToString());
                sqlConn.Close();

                StringBuilder sbKartice = new StringBuilder();
                sbKartice.Append("insert into pazari_card (id_pazar,banca_intesa,unicredit,diners_redovan,diners_1,diners_2,diners_3,diners_4,oznaka) values ");
                sbKartice.Append("(@id,@bancaIntesa,@unicredit,@dinersRedovan,@diners1,@diners2,@diners3,@diners4,@oznaka)");

                Decimal banca_intesa = Decimal.TryParse(tbBankaBI.Text.ToString(), out banca_intesa) ? banca_intesa : 0;
                Decimal unicredit = Decimal.TryParse(tbBankaUni.Text.ToString(), out unicredit) ? unicredit : 0;
                Decimal diners_redovan = Decimal.TryParse(tbDinersRedovan.Text.ToString(), out diners_redovan) ? diners_redovan : 0;
                Decimal diners_1 = Decimal.TryParse(tbDiners1.Text.ToString(), out diners_1) ? diners_1 : 0;
                Decimal diners_2 = Decimal.TryParse(tbDiners2.Text.ToString(), out diners_2) ? diners_2 : 0;
                Decimal diners_3 = Decimal.TryParse(tbDiners3.Text.ToString(), out diners_3) ? diners_3 : 0;
                Decimal diners_4 = Decimal.TryParse(tbDiners4.Text.ToString(), out diners_4) ? diners_4 : 0;

                SqlCommand komandaKartice = new SqlCommand(sbKartice.ToString(), sqlConn);

                komandaKartice.Parameters.Add("@id", SqlDbType.Int).Value = id;
                komandaKartice.Parameters.Add("@bancaIntesa", SqlDbType.Decimal).Value = banca_intesa;
                komandaKartice.Parameters.Add("@unicredit", SqlDbType.Decimal).Value = unicredit;
                komandaKartice.Parameters.Add("@dinersRedovan", SqlDbType.Decimal).Value = diners_redovan;
                komandaKartice.Parameters.Add("@diners1", SqlDbType.Decimal).Value = diners_1;
                komandaKartice.Parameters.Add("@diners2", SqlDbType.Decimal).Value = diners_2;
                komandaKartice.Parameters.Add("@diners3", SqlDbType.Decimal).Value = diners_3;
                komandaKartice.Parameters.Add("@diners4", SqlDbType.Decimal).Value = diners_4;
                komandaKartice.Parameters.Add("@oznaka", SqlDbType.Char).Value = id + "-" + sif_obj_mp + "-" + thisDay.ToString(dateFormat);

                try
                {
                    sqlConn.Open();
                    komandaKartice.ExecuteNonQuery();
                    sqlConn.Close();
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message); }

                //unos ni obrazaca

                StringBuilder sbObrasci = new StringBuilder();
                sbObrasci.Append("insert into pazari_ni (id_pazar,ni_1,ni_2,ni_3,ni_4,ni_5,ni_6,ni_7,ni_8,ni_9,ni_10,oznaka) values ");
                sbObrasci.Append("(@id,@ni1,@ni2,@ni3,@ni4,@ni5,@ni6,@ni7,@ni8,@ni9,@ni10,@oznaka)");

                Decimal ni1 = Decimal.TryParse(tbNi1.Text.ToString(), out ni1) ? ni1 : 0;
                Decimal ni2 = Decimal.TryParse(tbNi2.Text.ToString(), out ni2) ? ni2 : 0;
                Decimal ni3 = Decimal.TryParse(tbNi3.Text.ToString(), out ni3) ? ni3 : 0;
                Decimal ni4 = Decimal.TryParse(tbNi4.Text.ToString(), out ni4) ? ni4 : 0;
                Decimal ni5 = Decimal.TryParse(tbNi5.Text.ToString(), out ni5) ? ni5 : 0;
                Decimal ni6 = Decimal.TryParse(tbNi6.Text.ToString(), out ni6) ? ni6 : 0;
                Decimal ni7 = Decimal.TryParse(tbNi7.Text.ToString(), out ni7) ? ni7 : 0;
                Decimal ni8 = Decimal.TryParse(tbNi8.Text.ToString(), out ni8) ? ni8 : 0;
                Decimal ni9 = Decimal.TryParse(tbNi9.Text.ToString(), out ni9) ? ni9 : 0;
                Decimal ni10 = Decimal.TryParse(tbNi10.Text.ToString(), out ni10) ? ni10 : 0;

                SqlCommand komandaObrasci = new SqlCommand(sbObrasci.ToString(), sqlConn);

                komandaObrasci.Parameters.Add("@id", SqlDbType.Int).Value = id;
                komandaObrasci.Parameters.Add("@ni1", SqlDbType.Decimal).Value = ni1;
                komandaObrasci.Parameters.Add("@ni2", SqlDbType.Decimal).Value = ni2;
                komandaObrasci.Parameters.Add("@ni3", SqlDbType.Decimal).Value = ni3;
                komandaObrasci.Parameters.Add("@ni4", SqlDbType.Decimal).Value = ni4;
                komandaObrasci.Parameters.Add("@ni5", SqlDbType.Decimal).Value = ni5;
                komandaObrasci.Parameters.Add("@ni6", SqlDbType.Decimal).Value = ni6;
                komandaObrasci.Parameters.Add("@ni7", SqlDbType.Decimal).Value = ni7;
                komandaObrasci.Parameters.Add("@ni8", SqlDbType.Decimal).Value = ni8;
                komandaObrasci.Parameters.Add("@ni9", SqlDbType.Decimal).Value = ni9;
                komandaObrasci.Parameters.Add("@ni10", SqlDbType.Decimal).Value = ni10;
                komandaObrasci.Parameters.Add("@oznaka", SqlDbType.Char).Value = id + "-" + sif_obj_mp + "-" + thisDay.ToString(dateFormat);


                try
                {
                    sqlConn.Open();
                    komandaObrasci.ExecuteNonQuery();
                    sqlConn.Close();

                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message); }

            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }

            AutoClosingMessageBox.Show("Uspesno ste uneli pazar", "Promet", 5000);
            this.Hide();
            Form1 Form1 = new Form1();
            Form1.Show();

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            Form1 Form1 = new Form1();
            Form1.Show();
        }
    }
}
