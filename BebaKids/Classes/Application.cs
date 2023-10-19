using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using System.Net.Mail;
using System.Net;
using System.Data;
using System.Data.SqlClient;
using System.Data.Odbc;
using System.Net.Sockets;
using System.Windows.Forms;

namespace BebaKids.Classes
{
    class Application
    {
        public void playSound(string typeOfSound)
        {
            string sound = typeOfSound;
            if (sound == "error")
            {
                string path = "C:\\bkapps\\error.wav";
                System.Media.SoundPlayer player = new System.Media.SoundPlayer();
                player.SoundLocation = path;
                player.Load();
                player.Play();
            }
            if (sound == "errorSifra")
            {
                string path = "C:\\bkapps\\errorSifra.wav";
                System.Media.SoundPlayer player = new System.Media.SoundPlayer();
                player.SoundLocation = path;
                player.Load();
                player.Play();
            }
        }

        public void createExcel(string vrsta, string emeilTo, string dokument, DataTable ExcelTabela)
        {
            string tVrsta = vrsta;
            string tObjekat = emeilTo;
            string tDokument = dokument;
            string email = "";
            string ccEmail = "";
            Save tabela = new Save();


            var MyIni = new IniFile(@"C:\bkapps\config.ini");
            var objekatFrom = MyIni.Read("e_mail", "ProveraDokumenta").ToString();
            var objekatTo = MyIni.Read("e_mail_magacin", "ProveraDokumenta").ToString();
            var naziv = MyIni.Read("naziv", "ProveraDokumenta").ToString();

            if (vrsta == "P9")
            {
                email = tObjekat;
                ccEmail = objekatFrom;
            }
            if (vrsta == "OM")
            {
                email = objekatTo;
                ccEmail = tObjekat;
            }
            if (vrsta == "FK")
            {
                email = tabela.getInvoiceEmail(dokument, vrsta);
                ccEmail = objekatTo;
            }
            else
            {
                ccEmail = tabela.getInvoiceEmail(dokument, vrsta);
                email = tObjekat;

            }

            DataTable excel = new DataTable();
            excel = tabela.table(tDokument);

            XLWorkbook wb = new XLWorkbook();
            var ws = wb.Worksheets.Add(ExcelTabela, "Razlika");

            const string chars = "123456789";
            var random = new Random();
            var code = new StringBuilder(4);

            for (int i = 0; i < 4; i++)
            {
                code.Append(chars[random.Next(chars.Length)]);
            }

            string fileName = "Razlika_" + code + ".xlsx";

            wb.SaveAs(fileName);

            MailMessage mail = new MailMessage();

            mail.From = new MailAddress("server@bebakids.com", "Server BebaKids");
            mail.To.Add(email);
            mail.CC.Add(ccEmail);
            mail.Bcc.Add("bojan.draganic@bebakids.com");
            mail.Subject = "Razlika robe prema dokumentu " + tDokument.ToString();

            StringBuilder poruka = new StringBuilder();

            poruka.Append("Postovani, u prilogu su razlike provere prema dokumentu " + tDokument.ToString() + " \n");

            mail.Body = poruka.ToString();

            System.Net.Mail.Attachment attachment;
            attachment = new System.Net.Mail.Attachment(fileName);
            mail.Attachments.Add(attachment);
            //SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            SmtpClient SmtpServer = new SmtpClient();
            SmtpServer.UseDefaultCredentials = true;
            SmtpServer.Host = "smtp.office365.com";
            SmtpServer.Port = 587;
            SmtpServer.EnableSsl = true;
            SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
            SmtpServer.Credentials = new NetworkCredential("server@bebakids.com", "Migracija123");


            SmtpServer.Send(mail);


        }

        public DataTable objekti()
        {
            string prijavaKonString = "Dsn=prijava;uid=sa;Pwd=adminabc123";

            OdbcConnection konekcija = new OdbcConnection(prijavaKonString);
            string cmd1 = "select * from sif_obj_mp where status = 1 order by sif_obj_mp";

            var MyIni = new IniFile(@"C:\bkapps\config.ini");
            var baza = MyIni.Read("baza", "ProveraDokumenta").ToString();
            var database = MyIni.Read("database", "ProveraDokumenta").ToString();

            if (database == "DA")
            {
                cmd1 = "select * from sif_obj_mp where status = 1 and left(naz_obj_mp,5)='Watch' order by sif_obj_mp";
            }


            OdbcDataAdapter adapter1 = new OdbcDataAdapter(cmd1, konekcija);

            konekcija.Open();

            DataTable objekti = new DataTable();
            adapter1.Fill(objekti);

            konekcija.Close();

            return objekti;
        }

        public DataTable magacini()
        {
            string connString = "Dsn=ifx;uid=informix";
            OdbcConnection conn = new OdbcConnection(connString);

            // string cmd = ("select sif_rob sifra,sif_ent_rob velicina from pop_sta_mp_st where ozn_pop_sta = '" + document + "'");
            string cmd = ("select trim(sif_mag) sif_mag,trim(sif_mag)||'-'||trim(naz_mag) magacin,trim(napomena) e_mail from magacin where left(sif_org_jed,3) = ('012')");

            OdbcCommand command = new OdbcCommand(cmd, conn);

            OdbcDataAdapter adapter1 = new OdbcDataAdapter(cmd, conn);

            conn.Open();

            DataTable magacini = new DataTable();
            adapter1.Fill(magacini);

            conn.Close();

            return magacini;
        }

        public DataTable zuti_izvodi()
        {
            string informixKonString = "Dsn=ifx;uid=informix";

            OdbcConnection konekcija = new OdbcConnection(informixKonString);
            string cmd1 = "select 'Datum: '||dat_izv||' | Izvod: '||bro_izv||' | Racun: '||trim(sif_rac) display ,ozn_izv from izvod where status = 0 and storno = 'N' and dat_izv >= '01.01.2019' order by dat_izv,sif_rac,bro_izv";
            OdbcDataAdapter adapter1 = new OdbcDataAdapter(cmd1, konekcija);

            DataTable izvodi = new DataTable();
            adapter1.Fill(izvodi);

            konekcija.Close();

            return izvodi;
        }

        public DataTable pregledPrometa()
        {
            var MyIni = new IniFile(@"C:\bkapps\config.ini");
            var vCharset = MyIni.Read("charset", "Firebird");
            var vDataSource = MyIni.Read("dataSource", "Firebird").ToString();
            var vDatabase = MyIni.Read("database", "Firebird").ToString();
            var vRole = MyIni.Read("role", "Firebird").ToString();
            var vUserID = MyIni.Read("user", "Firebird").ToString();
            var vPassword = MyIni.Read("password", "Firebird").ToString();

            string informixKonString = "Dsn=ifx;uid=informix";

            OdbcConnection konekcija = new OdbcConnection(informixKonString);
            string cmd1 = "select 'Datum: '||dat_izv||' | Izvod: '||bro_izv||' | Racun: '||trim(sif_rac) display ,ozn_izv from izvod where status = 0 and storno = 'N' and dat_izv >= '01.01.2019' order by dat_izv,sif_rac,bro_izv";
            OdbcDataAdapter adapter1 = new OdbcDataAdapter(cmd1, konekcija);

            DataTable izvodi = new DataTable();
            adapter1.Fill(izvodi);

            konekcija.Close();

            return izvodi;
        }

        public DataTable radnici()
        {
            string prijavaKonString = "Dsn=prijava;uid=sa;Pwd=adminabc123";

            OdbcConnection konekcija = new OdbcConnection(prijavaKonString);
            string cmd1 = "select radnici.sifra SifraRadnika,radnici.ime_i_prezime ImePrezime,sif_obj_mp.naz_obj_mp Objekat,radnici.status Status from radnici left join sif_obj_mp on sif_obj_mp.sif_obj_mp = radnici.sif_obj_mp";
            OdbcDataAdapter adapter1 = new OdbcDataAdapter(cmd1, konekcija);

            konekcija.Open();

            DataTable radnici = new DataTable();
            adapter1.Fill(radnici);

            konekcija.Close();

            return radnici;
        }


        public DataTable radniciObjekat()
        {
            var MyIni = new IniFile(@"C:\bkapps\config.ini");
            var Sifraobjekat = MyIni.Read("sif_obj_mp", "ProveraDokumenta");
            int objekat = Convert.ToInt32(Sifraobjekat.ToString());

            OdbcConnection konekcija = new OdbcConnection(Konekcija.konString);
            string cmd1 = "select * from radnici where radnici.sif_obj_mp = '" + objekat + "'";
            OdbcDataAdapter adapter1 = new OdbcDataAdapter(cmd1, konekcija);

            konekcija.Open();

            DataTable radnici = new DataTable();
            adapter1.Fill(radnici);

            konekcija.Close();

            return radnici;
        }

        public bool testKonekcija()
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

        public void prenosPazara()
        {

            var MyIni = new IniFile(@"C:\bkapps\config.ini");
            string baza = MyIni.Read("konekcija_server", "Baza").ToString();
            string Localbaza = MyIni.Read("konekcija", "Baza").ToString();
            SqlConnection sqlConn = new SqlConnection(baza);
            SqlConnection sqlConnLocal = new SqlConnection(Localbaza);

            StringBuilder sb = new StringBuilder();
            sb.Append("select * from pazari p left join pazari_card pc on pc.id_pazar = p.id left join pazari_ni pn on pn.id_pazar =p.id where p.preneto =0");
            sqlConnLocal.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(sb.ToString(), sqlConnLocal);
            DataTable table = new DataTable();
            adapter.Fill(table);
            sqlConnLocal.Close();

            foreach (DataRow row in table.Rows)
            {
                int status = Convert.ToInt32(row["preneto"].ToString());
                //MessageBox.Show(status.ToString());
                if (status == 0)
                {

                    StringBuilder prenosPazara = new StringBuilder();
                    StringBuilder sbKartice = new StringBuilder();
                    StringBuilder sbObrasci = new StringBuilder();

                    prenosPazara.Append("insert into pazari (id_pazar,sif_obj_mp,date,cache,card,cek,presek,presek_pdv,cek_redovni,cek_odlozeni,administrativna,konzul,vaucer,napomena,oznaka) values");
                    prenosPazara.Append("(@id,@objekat,@date,@cache,@card,@cek,@presek,@presekPdv,@cekRedovni,@cekOdlozeni,@administrativna,@konzul,@vaucer,@napomena,@oznaka)");
                    SqlCommand komanda = new SqlCommand(prenosPazara.ToString(), sqlConn);

                    sbKartice.Append("insert into pazari_card (id_pazar,banca_intesa,unicredit,diners_redovan,diners_1,diners_2,diners_3,diners_4,oznaka) values ");
                    sbKartice.Append("(@id,@bancaIntesa,@unicredit,@dinersRedovan,@diners1,@diners2,@diners3,@diners4,@oznaka)");
                    SqlCommand komandaKartice = new SqlCommand(sbKartice.ToString(), sqlConn);


                    sbObrasci.Append("insert into pazari_ni (id_pazar,ni_1,ni_2,ni_3,ni_4,ni_5,ni_6,ni_7,ni_8,ni_9,ni_10,oznaka) values ");
                    sbObrasci.Append("(@id,@ni1,@ni2,@ni3,@ni4,@ni5,@ni6,@ni7,@ni8,@ni9,@ni10,@oznaka)");
                    SqlCommand komandaObrasci = new SqlCommand(sbObrasci.ToString(), sqlConn);

                    int id = Convert.ToInt32(row["id"].ToString());
                    String objekat = row["sif_obj_mp"].ToString();
                    string dateFormat = "yyyy-MM-dd";
                    string datum = row["date"].ToString();
                    DateTime date = Convert.ToDateTime(datum);
                    Decimal cache = Decimal.TryParse(row["cache"].ToString(), out cache) ? cache : 0;
                    Decimal card = Decimal.TryParse(row["card"].ToString(), out card) ? card : 0;
                    Decimal cek = Decimal.TryParse(row["cek"].ToString(), out cek) ? cek : 0;
                    Decimal presek = Decimal.TryParse(row["presek"].ToString(), out presek) ? presek : 0;
                    Decimal presek_pdv = Decimal.TryParse(row["presek_pdv"].ToString(), out presek_pdv) ? presek_pdv : 0;
                    Decimal cek_redovni = Decimal.TryParse(row["cek_redovni"].ToString(), out cek_redovni) ? cek_redovni : 0;
                    Decimal cek_odlozeni = Decimal.TryParse(row["cek_odlozeni"].ToString(), out cek_odlozeni) ? cek_odlozeni : 0;
                    Decimal administrativna = Decimal.TryParse(row["administrativna"].ToString(), out administrativna) ? administrativna : 0;
                    Decimal konzul = Decimal.TryParse(row["konzul"].ToString(), out konzul) ? konzul : 0;
                    Decimal vaucer = Decimal.TryParse(row["vaucer"].ToString(), out vaucer) ? vaucer : 0;
                    String napomena = row["napomena"].ToString();
                    Decimal banca_intesa = Decimal.TryParse(row["banca_intesa"].ToString(), out banca_intesa) ? banca_intesa : 0;
                    Decimal unicredit = Decimal.TryParse(row["unicredit"].ToString(), out unicredit) ? unicredit : 0;
                    Decimal diners_redovan = Decimal.TryParse(row["diners_redovan"].ToString(), out diners_redovan) ? diners_redovan : 0;
                    Decimal diners_1 = Decimal.TryParse(row["diners_1"].ToString(), out diners_1) ? diners_1 : 0;
                    Decimal diners_2 = Decimal.TryParse(row["diners_2"].ToString(), out diners_2) ? diners_2 : 0;
                    Decimal diners_3 = Decimal.TryParse(row["diners_3"].ToString(), out diners_3) ? diners_3 : 0;
                    Decimal diners_4 = Decimal.TryParse(row["diners_4"].ToString(), out diners_4) ? diners_4 : 0;
                    Decimal ni1 = Decimal.TryParse(row["ni_1"].ToString(), out ni1) ? ni1 : 0;
                    Decimal ni2 = Decimal.TryParse(row["ni_2"].ToString(), out ni2) ? ni2 : 0;
                    Decimal ni3 = Decimal.TryParse(row["ni_3"].ToString(), out ni3) ? ni3 : 0;
                    Decimal ni4 = Decimal.TryParse(row["ni_4"].ToString(), out ni4) ? ni4 : 0;
                    Decimal ni5 = Decimal.TryParse(row["ni_5"].ToString(), out ni5) ? ni5 : 0;
                    Decimal ni6 = Decimal.TryParse(row["ni_6"].ToString(), out ni6) ? ni6 : 0;
                    Decimal ni7 = Decimal.TryParse(row["ni_7"].ToString(), out ni7) ? ni7 : 0;
                    Decimal ni8 = Decimal.TryParse(row["ni_8"].ToString(), out ni8) ? ni8 : 0;
                    Decimal ni9 = Decimal.TryParse(row["ni_9"].ToString(), out ni9) ? ni9 : 0;
                    Decimal ni10 = Decimal.TryParse(row["ni_10"].ToString(), out ni10) ? ni10 : 0;




                    komanda.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    komanda.Parameters.Add("@objekat", SqlDbType.Char).Value = objekat;
                    komanda.Parameters.Add("@date", SqlDbType.Date).Value = date.ToString(dateFormat); SqlParameter paramCache = new SqlParameter("@cache", SqlDbType.Decimal);
                    komanda.Parameters.Add("@cache", SqlDbType.Decimal).Value = cache;
                    komanda.Parameters.Add("@card", SqlDbType.Decimal).Value = card;
                    komanda.Parameters.Add("@cek", SqlDbType.Decimal).Value = cek;
                    komanda.Parameters.Add("@presek", SqlDbType.Decimal).Value = presek;
                    komanda.Parameters.Add("@presekPdv", SqlDbType.Decimal).Value = presek_pdv;
                    komanda.Parameters.Add("@cekRedovni", SqlDbType.Decimal).Value = cek_redovni;
                    komanda.Parameters.Add("@cekOdlozeni", SqlDbType.Decimal).Value = cek_odlozeni;
                    komanda.Parameters.Add("@administrativna", SqlDbType.Decimal).Value = administrativna;
                    komanda.Parameters.Add("@konzul", SqlDbType.Decimal).Value = konzul;
                    komanda.Parameters.Add("@vaucer", SqlDbType.Decimal).Value = vaucer;
                    komanda.Parameters.Add("@napomena", SqlDbType.Text).Value = napomena;
                    komanda.Parameters.Add("@oznaka", SqlDbType.Char).Value = id + "-" + objekat + "-" + date.ToString(dateFormat);


                    komandaKartice.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    komandaKartice.Parameters.Add("@bancaIntesa", SqlDbType.Decimal).Value = banca_intesa;
                    komandaKartice.Parameters.Add("@unicredit", SqlDbType.Decimal).Value = unicredit;
                    komandaKartice.Parameters.Add("@dinersRedovan", SqlDbType.Decimal).Value = diners_redovan;
                    komandaKartice.Parameters.Add("@diners1", SqlDbType.Decimal).Value = diners_1;
                    komandaKartice.Parameters.Add("@diners2", SqlDbType.Decimal).Value = diners_2;
                    komandaKartice.Parameters.Add("@diners3", SqlDbType.Decimal).Value = diners_3;
                    komandaKartice.Parameters.Add("@diners4", SqlDbType.Decimal).Value = diners_4;
                    komandaKartice.Parameters.Add("@oznaka", SqlDbType.Char).Value = id + "-" + objekat + "-" + date.ToString(dateFormat);

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
                    komandaObrasci.Parameters.Add("@oznaka", SqlDbType.Char).Value = id + "-" + objekat + "-" + date.ToString(dateFormat);

                    try
                    {
                        int a, b, c;
                        sqlConn.Open();
                        a = komanda.ExecuteNonQuery();
                        if (a == 0)
                        {
                            MessageBox.Show("Nisu preneti podaci pazara", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            b = komandaKartice.ExecuteNonQuery();
                            if (b == 0)
                            {
                                MessageBox.Show("Nisu preneti podaci kartica", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
                            {
                                c = komandaObrasci.ExecuteNonQuery();
                                if (c == 0)
                                {
                                    MessageBox.Show("Nisu preneti podaci NI obrazaca", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                else
                                {
                                    StringBuilder update = new StringBuilder();
                                    update.Append("update pazari set preneto = 1 where id = '" + id + "'");
                                    SqlCommand updateKomanda = new SqlCommand(update.ToString(), sqlConnLocal);
                                    sqlConnLocal.Open();
                                    updateKomanda.ExecuteNonQuery();
                                    sqlConnLocal.Close();

                                }
                            }
                        }
                        sqlConn.Close();

                        //MessageBox.Show("Uspesno uneti podaci", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }


                }



            }



        }
    }

}
