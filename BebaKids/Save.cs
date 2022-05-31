using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;

namespace BebaKids
{
    class Save
    {
        public void insert(string dokument, string vrsta, string objekat, string sifra, string velicina, int kolicina)
        {
            OdbcConnection konekcija = new OdbcConnection(Konekcija.konString);
            string tDokumnet = dokument;
            string tVrsta = vrsta;
            string tObjekat = objekat;
            string tSifra = sifra;
            string tVelicina = velicina;
            int tKolicina = kolicina*-1;

            StringBuilder sb = new StringBuilder();
            sb.Append("insert into prenos_dokumenta (vrsta,dokument,objekat,sifra,velicina,scaned_kolicina) values (" + "?" + "," + "?" + "," + "?" + "," + "?,?,?);");
            OdbcCommand komanda = new OdbcCommand(sb.ToString(), konekcija);

            OdbcParameter pVrsta = new OdbcParameter("@pvrsta", OdbcType.Char);
            OdbcParameter pDokument = new OdbcParameter("@pdokument", OdbcType.Char);
            OdbcParameter pObjekat = new OdbcParameter("@pobjekat", OdbcType.Char);
            OdbcParameter pSifra = new OdbcParameter("@psifra", OdbcType.Char);
            OdbcParameter pVelicina = new OdbcParameter("@pvelicina", OdbcType.Char);
            OdbcParameter pKolicina = new OdbcParameter("@pkolicina", OdbcType.Int);

            komanda.Parameters.Add(pVrsta);
            komanda.Parameters.Add(pDokument);
            komanda.Parameters.Add(pObjekat);
            komanda.Parameters.Add(pSifra);
            komanda.Parameters.Add(pVelicina);
            komanda.Parameters.Add(pKolicina);
            pVrsta.Value = tVrsta;
            pDokument.Value = tDokumnet;
            pObjekat.Value = tObjekat;
            pSifra.Value = tSifra;
            pVelicina.Value = tVelicina;
            pKolicina.Value = tKolicina;

            try
            {
                konekcija.Open();
                komanda.ExecuteNonQuery();
                konekcija.Close();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                //return ex.Message.ToString;
            }
        }
        public void insert1(string dokument, string vrsta, string sif_par, string sifra, string velicina, int kolicina)
        {
            OdbcConnection konekcija = new OdbcConnection(Konekcija.konString);
            string tDokumnet = dokument;
            string tVrsta = vrsta;
            string tSifPar = sif_par;
            string tSifra = sifra;
            string tVelicina = velicina;
            int tKolicina = kolicina * -1;

            StringBuilder sb = new StringBuilder();
            sb.Append("insert into prenos_dokumenta (vrsta,dokument,objekat,sifra,velicina,scaned_kolicina) values (" + "?" + "," + "?" + "," + "?" + "," + "?,?,?);");
            OdbcCommand komanda = new OdbcCommand(sb.ToString(), konekcija);

            OdbcParameter pVrsta = new OdbcParameter("@pvrsta", OdbcType.Char);
            OdbcParameter pDokument = new OdbcParameter("@pdokument", OdbcType.Char);
            OdbcParameter pSifPar = new OdbcParameter("@pobjekat", OdbcType.Char);
            OdbcParameter pSifra = new OdbcParameter("@psifra", OdbcType.Char);
            OdbcParameter pVelicina = new OdbcParameter("@pvelicina", OdbcType.Char);
            OdbcParameter pKolicina = new OdbcParameter("@pkolicina", OdbcType.Int);

            komanda.Parameters.Add(pVrsta);
            komanda.Parameters.Add(pDokument);
            komanda.Parameters.Add(pSifPar);
            komanda.Parameters.Add(pSifra);
            komanda.Parameters.Add(pVelicina);
            komanda.Parameters.Add(pKolicina);
            pVrsta.Value = tVrsta;
            pDokument.Value = tDokumnet;
            pSifPar.Value = tSifPar;
            pSifra.Value = tSifra;
            pVelicina.Value = tVelicina;
            pKolicina.Value = tKolicina;

            try
            {
                konekcija.Open();
                komanda.ExecuteNonQuery();
                konekcija.Close();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                //return ex.Message.ToString;
            }
        }
        public bool proveraDokumenta(string dokument, string vrsta,string objekat)
        {
            bool provera = false;
            string tDokumnet = dokument;
            string tObjekat = objekat;
            string tVrsta = vrsta;

            string connString = "Dsn=ifx;uid=informix";
            //OdbcCommand komanda = new OdbcCommand();

            OdbcConnection conn = new OdbcConnection(connString);

            if (tVrsta.ToString() == "P9")
            {
                OdbcCommand komanda = new OdbcCommand("select * from pren_mp where storno = 'N' and sif_obj_ula = '" + tObjekat + "' and ozn_pre_mp = '" + tDokumnet + "'", conn);
                conn.Open();
                OdbcDataReader dr = komanda.ExecuteReader();
                if (dr.Read())
                {
                    provera = true;
                }
                else { provera = false; }
                conn.Close();
            }
            
            if (tVrsta.ToString() == "OM")
            {
                OdbcCommand komanda = new OdbcCommand("select * from otprem_mp where storno = 'N' and sif_obj_mp = '" + tObjekat + "' and ozn_otp_mal = '" + tDokumnet + "'", conn);
                conn.Open();
                OdbcDataReader dr = komanda.ExecuteReader();
                if (dr.Read())
                {
                    provera = true;
                }
                else { provera = false; }
                conn.Close();
            }
            if(tVrsta.ToString()=="MP")
            {
                OdbcCommand komanda = new OdbcCommand("select * from povrat_mp where storno = 'N' and ozn_pov_mp = '" + tDokumnet + "'", conn);
                conn.Open();
                OdbcDataReader dr = komanda.ExecuteReader();
                if (dr.Read())
                {
                    provera = true;
                }
                else { provera = false; }
                conn.Close();
            }            
            return provera;
        }

        public bool proveraDokumenta1(string dokument, string vrsta, string sif_par)
        {
            bool provera = false;
            string tDokumnet = dokument;
            string tSifPar = sif_par;
            string tVrsta = vrsta;

            string connString = "Dsn=ifx;uid=informix";
            //OdbcCommand komanda = new OdbcCommand();

            OdbcConnection conn = new OdbcConnection(connString);
                        
                OdbcCommand komanda = new OdbcCommand("select * from otprem where storno = 'N' and sif_par = '" + tSifPar + "' and ozn_otp = '" + tDokumnet + "'", conn);
                conn.Open();
                OdbcDataReader dr = komanda.ExecuteReader();
                if (dr.Read())
                {
                    provera = true;
                }
                else { provera = false; }
                conn.Close();
            
            return provera;

        }

        public DataTable getCekovi(string objekat)
        {
            DataTable table = new DataTable();
            string tObjekat = objekat;
            string connString = "Dsn=ifx;uid=informix";
            OdbcConnection conn = new OdbcConnection(connString);
            string cmd = ("select trim(bro_tek_rac) Tekuci_Racun,trim(bro_cek) Broj_Ceka,dat_cek Datum,izn_cek Iznos from cek_gra where dat_obr = dat_cek and realizovan = 0 and sif_obj_mp = '"+tObjekat+"'");
            conn.Open();
            OdbcDataAdapter adapter = new OdbcDataAdapter(cmd, conn);
            adapter.Fill(table);
            conn.Close();

            return table;
        }

        public void getTable(string vrsta,string objekat,string dokument)
        {

            DataTable table = new DataTable();
            string tDokumnet = dokument;
            string tObjekat = objekat;
            string tVrsta = vrsta;
            string oznaka = "";
            string databaseTable = "";
            string databaseObjekat = "";
            if (vrsta == "P9")
            {
                oznaka = "ozn_pre_mp";
                databaseObjekat = "sif_obj_ula";
                databaseTable = "pren_mp_st";
            }
            if (vrsta == "OM")
            {
                oznaka = "ozn_otp_mal";
                databaseObjekat = "sif_obj_mp";
                databaseTable = "otprem_mp_st";
            }
            if(vrsta =="MP")
            {
                oznaka = "ozn_pov_mp";
                databaseObjekat = "sif_obj_mp";
                databaseTable = "povrat_mp_st";
            }
            
            string connString = "Dsn=ifx;uid=informix";
            OdbcConnection conn = new OdbcConnection(connString);
            string cmd = ("select "+oznaka+",trim(sif_rob) sif_rob,trim(sif_ent_rob) sif_ent_Rob,sum(kolic) kolic from "+databaseTable+" where "+oznaka+" = '" + tDokumnet + "' group by sif_rob,sif_ent_Rob,"+ oznaka + " ");
            conn.Open();
            OdbcDataAdapter adapter = new OdbcDataAdapter(cmd, conn);
            adapter.Fill(table);
            conn.Close();

            OdbcConnection konekcija = new OdbcConnection(Konekcija.konString);

            foreach (DataRow row in table.Rows)
            {

                String sif_rob = row["sif_rob"].ToString();
                int kolic = Convert.ToInt32(row["kolic"]);
                String sif_ent_rob = row["sif_ent_rob"].ToString();
                OdbcCommand komanda = new OdbcCommand("insert into prenos_dokumenta (vrsta,dokument,objekat,sifra,velicina,kolicina) values ('" + tVrsta + "','" + tDokumnet + "','" + tObjekat + "','" + sif_rob + "','" + sif_ent_rob + "','" + kolic + "')", konekcija);


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
            }

        }
        public void getTable1(string vrsta,string sif_par,string dokument)
        {

            DataTable table = new DataTable();
            string tDokumnet = dokument;
            string tSifPar = sif_par;
            string tVrsta = vrsta;
            string oznaka = "ozn_otp";
            string databaseTable = "otprem_st";
            string databaseObjekat = "sif_par";          
                      
            string connString = "Dsn=ifx;uid=informix";
            OdbcConnection conn = new OdbcConnection(connString);
            string cmd = ("select "+oznaka+",trim(sif_rob) sif_rob,trim(sif_ent_rob) sif_ent_Rob,sum(kolic) kolic from "+databaseTable+" where "+oznaka+" = '" + tDokumnet + "' group by sif_rob,sif_ent_Rob,"+ oznaka + " ");
            conn.Open();
            OdbcDataAdapter adapter = new OdbcDataAdapter(cmd, conn);
            adapter.Fill(table);
            conn.Close();

            OdbcConnection konekcija = new OdbcConnection(Konekcija.konString);

            foreach (DataRow row in table.Rows)
            {

                String sif_rob = row["sif_rob"].ToString();
                int kolic = Convert.ToInt32(row["kolic"]);
                String sif_ent_rob = row["sif_ent_rob"].ToString();
                OdbcCommand komanda = new OdbcCommand("insert into prenos_dokumenta (vrsta,dokument,objekat,sifra,velicina,kolicina) values ('" + tVrsta + "','" + tDokumnet + "','" + tSifPar + "','" + sif_rob + "','" + sif_ent_rob + "','" + kolic + "')", konekcija);


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
            }

        }
        
        public DataTable table(string dokument)  //provera razlike skeniranja i dokumenta
        {
            string tDokumnet = dokument;

            OdbcConnection konekcija = new OdbcConnection(Konekcija.konString);
            string cmd1 = " select p.dokument,p.objekat,dbo.get_naziv(p.sifra) naziv,p.sifra,p.velicina,sum(p.kolicina) poslata_kolicina, " +
                         " sum(p.scaned_kolicina)*-1 skenirana_kolicina,sum(p.kolicina+p.scaned_kolicina)*-1 razlika from prenos_dokumenta p " +
                         " where p.dokument = '" + tDokumnet + "' and p.completed = 0 " +
                         " group by dokument,objekat,sifra,velicina having sum(p.kolicina+p.scaned_kolicina) <> 0";

            OdbcDataAdapter adapter1 = new OdbcDataAdapter(cmd1, konekcija);

            konekcija.Open();

            DataTable razlika = new DataTable();
            adapter1.Fill(razlika);

            konekcija.Close();

            return razlika;
        }
      
        public DataTable popisTable(string dokument, string vrsta)  //pnjenje datagridview-a popisa
        {
            string tDokumnet = dokument;
            string tVrsta = vrsta;
            StringBuilder cmd1 = new StringBuilder();
            OdbcConnection konekcija = new OdbcConnection(Konekcija.konString);
            if (vrsta == "insert")
            {
                cmd1.Append("select id,dbo.get_naziv(sif_rob) naziv,sif_rob sifa,sif_ent_rob velicina,kolic kolicina from pop_sta_mp_st ");
                cmd1.Append(" where ozn_pop_sta = '" + tDokumnet + "' and preneto = 0 order by id desc ");
            }
            else
            {
                cmd1.Append("select id,ozn_pop_sta,dbo.get_naziv(sif_rob) naziv,sif_rob sifra,sif_ent_rob velicina,kolic kolicina from pop_sta_mp_st ");
                cmd1.Append(" where preneto = 0 order by id desc ");
            }
            OdbcDataAdapter adapter1 = new OdbcDataAdapter(cmd1.ToString(), konekcija);

            konekcija.Open();

            DataTable popisDokumenta = new DataTable();
            adapter1.Fill(popisDokumenta);

            konekcija.Close();

            return popisDokumenta;
        }
        public void brisiDokument(string dokument,string vrsta)
        {
            string tDokument = dokument;
            string tVrsta = vrsta;

            OdbcConnection konekcija = new OdbcConnection(Konekcija.konString);
            OdbcCommand komanda = new OdbcCommand("delete from prenos_dokumenta where dokument ='" + tDokument + "'", konekcija);
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


        }

        public void completedDokument(string dokument)
        {
            string tDokument = dokument;

            OdbcConnection konekcija = new OdbcConnection(Konekcija.konString);
            OdbcCommand komanda = new OdbcCommand("update prenos_dokumenta set completed = 1 where dokument ='" + tDokument + "'", konekcija);
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


        }



        // ubacivanje prijave radnika
        public void insertPrijava(int sifra, int objekat, string vrsta)
        {
            OdbcConnection konekcija = new OdbcConnection(Konekcija.konString);

            int tSifra = sifra;
            int tObjekat = objekat;
            DateTime thisDay = DateTime.Now;
            string dateFormat = "yyyy-MM-dd";
            string timeFormat = "HH:mm:ss";

            string date = thisDay.ToString(dateFormat);
            string time = thisDay.ToString(timeFormat);

            string tVrsta = vrsta;

            OdbcCommand komanda = new OdbcCommand("insert into prijava (sifra,sif_obj_mp,date,check_in,vrsta) values ('" + tSifra + "','" + tObjekat + "','" + date + "','" + time + "','"+ tVrsta+"')", konekcija);
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
        }

        //update prijave radnika
        public void updatePrijava(int idPrijave, bool isRegular,int status,string napomena)
        {
            OdbcConnection konekcija = new OdbcConnection(Konekcija.konString);
            int tIdPrijave = idPrijave;
            bool tIsRegular = isRegular;
            string tNapomena = napomena;
            DateTime thisDay = DateTime.Now;
            string dateFormat = "yyyy-MM-dd";
            string timeFormat = "HH:mm:ss";
            int tStatus = status;
            int pstatus = 0;
            if (tStatus == 1)
            {
                pstatus = 2;
            }
            else if (tStatus == 0)
            {
                pstatus = 4;
            }

            string date = thisDay.ToString(dateFormat);
            string time = thisDay.ToString(timeFormat);

            //OdbcCommand komanda = new OdbcCommand();
            if (tIsRegular == true)
            {
                OdbcCommand komanda = new OdbcCommand("update prijava set check_out = '"+time+"',status='"+ pstatus + "', regularDay = 1,doubleDay = 0,sickDay = 0,paidDay = 0,napomena = '" + napomena + "' where id ='" + tIdPrijave + "'", konekcija);
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
            }
            else
            {
                OdbcCommand komanda = new OdbcCommand("update prijava set check_out = '" + time + "',status='" + pstatus + "', regularDay = 0,doubleDay = 2,sickDay = 0,paidDay = 0,napomena ='"+napomena+"' where id ='" + tIdPrijave + "'", konekcija);
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
            }
        }

        //insert placenog odsustva
        public void insertOdsustvo(int sifra, int objekat,string date,int paidDay,int sickDay, string vrsta,string napomena)
        {
            OdbcConnection konekcija = new OdbcConnection(Konekcija.konString);

            int tSifra = sifra;
            int tObjekat = objekat;
            int tpaidDay = paidDay;
            int tsickDay = sickDay;
            string tNapomena = napomena;
            DateTime thisDay = DateTime.Now;
            //string dateFormat = "yyyy-MM-dd";
            string timeFormat = "HH:mm:ss";

            string tdate = date;
            string time = thisDay.ToString(timeFormat);

            string tVrsta = vrsta;

            OdbcCommand komanda = new OdbcCommand("insert into prijava (sifra,sif_obj_mp,date,check_in,check_out,regularDay,doubleDay,sickDay,paidDay,vrsta,status,napomena) values ('" + tSifra + "','" + tObjekat + "','" + tdate + "','" + time + "','" + time + "',0,0,'" + tsickDay + "','" + tpaidDay + "','" + tVrsta + "',4,'"+napomena+"')", konekcija);
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
        }

        public void prenosPodataka()
        {
            string prijavaKonString = "Dsn=prijava;uid=sa;Pwd=adminabc123";
            OdbcConnection prijavaKonekcija = new OdbcConnection(prijavaKonString);
            OdbcConnection konekcija = new OdbcConnection(Konekcija.konString);
            //OdbcCommand checkCheckin = new OdbcCommand("Select * from prijava where status = 0", konekcija);

            string cmd = ("select * from prijava where status not in ('3','1')");
            konekcija.Open();
            OdbcDataAdapter adapter = new OdbcDataAdapter(cmd, konekcija);
            DataTable table = new DataTable();
            adapter.Fill(table);
            konekcija.Close();

            foreach (DataRow row in table.Rows)
            {
                string dateFormat = "yyyy-MM-dd";
                string datum = row["date"].ToString();
                DateTime date = Convert.ToDateTime(datum);
                int sifra = Convert.ToInt32(row["sifra"].ToString());
                int objekat = Convert.ToInt32(row["sif_obj_mp"].ToString());
                //string date = row["date"].ToString();
                string CheckIn = row["check_in"].ToString();
                DateTime checkIn = Convert.ToDateTime(CheckIn);
                string vrsta = row["vrsta"].ToString();
                int status = Convert.ToInt32(row["status"].ToString());
                StringBuilder sbPrijava = new StringBuilder();
                StringBuilder sbBebakids = new StringBuilder();
                if (status == 0)
                {
                    sbPrijava.Append("insert into prijava (sifra,sif_obj_mp,date,check_in,vrsta,status) values ('" + sifra + "','" + objekat + "','" + date.ToString(dateFormat) + "','" + checkIn + "','" + vrsta + "',1)");
                    sbBebakids.Append("update prijava set status = 1 where id = '" + Convert.ToInt32(row["id"].ToString()) + "'");
                }
                else if (status == 2)
                {
                    string checkOut = row["check_out"].ToString();
                    string napomena = row["napomena"].ToString();
                    int regularDay = Convert.ToInt32(row["regularDay"].ToString());
                    int doubleDay = Convert.ToInt32(row["doubleDay"].ToString());
                    int sickDay = Convert.ToInt32(row["sickDay"].ToString());
                    int paidDay = Convert.ToInt32(row["paidDay"].ToString());

                    sbPrijava.Append("update prijava set check_out = '" + checkOut + "',status=3,napomena = '"+napomena+"', regularDay = '" + regularDay + "', doubleDay = '" + doubleDay + "',sickDay = '" + sickDay + "',paidDay = '" + paidDay + "',vrsta = '" + vrsta + "' where sifra = '"+sifra+"' and sif_obj_mp ='"+objekat+"' and date ='"+date.ToString(dateFormat) + "' and vrsta ='"+vrsta+ "'");
                    sbBebakids.Append("update prijava set status = 3 where id = '" + Convert.ToInt32(row["id"].ToString()) + "'");
                }
                else if (status == 4)
                {
                    string checkOut = row["check_out"].ToString();
                    string napomena = row["napomena"].ToString();
                    int regularDay = Convert.ToInt32(row["regularDay"].ToString());
                    int doubleDay = Convert.ToInt32(row["doubleDay"].ToString());
                    int sickDay = Convert.ToInt32(row["sickDay"].ToString());
                    int paidDay = Convert.ToInt32(row["paidDay"].ToString());
                    sbPrijava.Append("insert into prijava (sifra,sif_obj_mp,date,check_in,check_out,regularDay,doubleDay,sickDay,paidDay,vrsta,status,napomena) values ('" + sifra + "','" + objekat + "','" + date.ToString(dateFormat) + "','" + checkIn + "','" + checkOut + "','" + regularDay + "','" + doubleDay + "','" + sickDay + "','" + paidDay + "','" + vrsta + "',3,'"+napomena+"')");
                    sbBebakids.Append("update prijava set status = 3 where id = '" + Convert.ToInt32(row["id"].ToString()) + "'");
                }
                OdbcCommand prenosKomanda = new OdbcCommand(sbPrijava.ToString(), prijavaKonekcija);
                OdbcCommand komandaUpdate = new OdbcCommand(sbBebakids.ToString(), konekcija);

                //OdbcCommand prenosKomanda = new OdbcCommand("insert into prijava (sifra,sif_obj_mp,date,check_in,check_out,regularDay,doubleDay,sickDay,paidDay,vrsta,status) values ('" + sifra + "','" + objekat + "','" + date + "','" + checkIn + "','" + checkOut + "','" + regularDay + "','" + doubleDay + "','" + sickDay + "','" + paidDay + "','" + vrsta + "',0)", prijavaKonekcija);
                try
                {
                    prijavaKonekcija.Open();
                    int a = prenosKomanda.ExecuteNonQuery();
                    prijavaKonekcija.Close();
                    if (a == 0)
                    {
                        MessageBox.Show("Nisu preneti podaci", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        //OdbcCommand komandaUpdate = new OdbcCommand("update prijava set status = 1 where id = '" + Convert.ToInt32(row["id"].ToString()) + "'", konekcija);
                        try
                        {
                            konekcija.Open();
                            komandaUpdate.ExecuteNonQuery();
                            konekcija.Close();
                            
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        //kreiranje tabele za izvestaj prijave radnika
        public DataTable tableIzvestajRadnika(int sifra,string dateFrom,string dateTo,string vrsta)
        {
            string tVrsta = vrsta;
            string pom = "";
            if (tVrsta == "REGULAR")
            {
                pom = "p.vrsta in ('ODSUSTVO','" + tVrsta + "')";
            }
            else
            {
                pom = "p.vrsta = ('" + tVrsta + "')";
            }
            int tSifra = sifra;
            string tDateFrom = dateFrom;
            string tDateTo = dateTo;
            string prijavaKonString = "Dsn=prijava;uid=sa;Pwd=adminabc123";
            OdbcConnection konekcija = new OdbcConnection(prijavaKonString);
            string cmd1 = " select o.naz_obj_mp Objekat,r.ime_i_prezime ImePrezime,p.date, " +
                          " CONVERT(VARCHAR(8), check_in,108) prijava,CONVERT(VARCHAR(8), check_out,108) odjava,CONVERT(VARCHAR(8),check_out-check_in,108) vreme, " +
                          " regularDay RedovanDan, doubleDay DuplaSmena, sickDay Bolovanje,paidDay PlacenDan,napomena Napomena " +
                          " from prijava p " +
                          " left join sif_obj_mp o on o.sif_obj_mp = p.sif_obj_mp " +
                          " left join radnici r on r.sifra = p.sifra " +
                          " where "+pom+" and p.sifra = '" + tSifra + "' and p.date>= '" + tDateFrom+ "' and p.date <= '" + tDateTo + "'" +
                          " order by p.date";

            OdbcDataAdapter adapter1 = new OdbcDataAdapter(cmd1, konekcija);

            konekcija.Open();

            DataTable tabela = new DataTable();
            adapter1.Fill(tabela);

            konekcija.Close();

            return tabela;
        }

        public DataTable barkodovi(string eanKod) {
            string tBarkod = eanKod;
            OdbcConnection konekcija = new OdbcConnection(Konekcija.konString);

            DataSet ds = new DataSet();// kreiranje DataSet objekta
            OdbcDataAdapter barkod = new OdbcDataAdapter("select * from ean_kod2 where bar_kod = '"+tBarkod+"'", konekcija);//punjenje objekta sqladaptera sa podacima iz tab. users
            barkod.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            OdbcCommandBuilder builder = new OdbcCommandBuilder(barkod);//sqldataadapter komanda cita iz sqlbuildera
            barkod.Fill(ds, "barkod");//punjenj objekta
            DataTable tabelaBarkodova = ds.Tables["barkod"];//kreiraanje tabele koja prestavlja kopiju
            return tabelaBarkodova;
        }

        public string getTime(int sifra, string dateFrom, string dateTo, string vrsta)
        {
            string tVrsta = vrsta;
            int tSifra = sifra;
            string tDateFrom = dateFrom;
            string tDateTo = dateTo;
            string prijavaKonString = "Dsn=prijava;uid=sa;Pwd=adminabc123";
            OdbcConnection konekcija = new OdbcConnection(prijavaKonString);
            string cmd1 = " SELECT convert(varchar(8), dateadd(second, SUM(DATEDIFF(SECOND, check_in, check_out)), 0),108) from prijava p " +
                          " where p.vrsta = '" + vrsta + "' and p.sifra = '" + tSifra + "' and p.date>= '" + tDateFrom + "' and p.date <= '" + tDateTo + "'" ;

            //OdbcDataAdapter adapter1 = new OdbcDataAdapter(cmd1, konekcija);
            OdbcCommand komanda = new OdbcCommand(cmd1, konekcija);

            konekcija.Open();

            string total = komanda.ExecuteScalar().ToString();

            konekcija.Close();

            return total;
        }

        //insert popis stavka
        public void insertPopis(string oznakaPopisa, int objekat, DateTime date, string sifra, string velicina, int kolicina)
        {
            OdbcConnection konekcija = new OdbcConnection(Konekcija.konString);

            string tSifra = sifra;
            int tObjekat = objekat;
            string tOznakaPopisa = oznakaPopisa;
            int tKolicina = kolicina;
            string tVelicina = velicina;
            DateTime tdate = date;
            string dateFormat = "yyyy-MM-dd";
            string dateFormat2 = "dd.MM.yyyy";
            //IFormatProvider culture = new System.Globalization.CultureInfo("sr", true);
            //DateTime dt = DateTime.ParseExact(tdate, "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture);
            int preneto = 0;
            /*
            SqlCommand cmd = new SqlCommand("insert into pop_sta_mp_st (ozn_pop_sta,dat_pop,sif_obj_mp,sif_rob,sif_ent_rob,kolic,preneto) output INSERTED.ID values(@oznaka,@date,@objekat,@sifra,@velicina,@kolicina,$preneto)", konekcija);

            cmd.Parameters.AddWithValue("@oznaka", tOznakaPopisa);
            cmd.Parameters.AddWithValue("@date", tdate.ToString(dateFormat));
            cmd.Parameters.AddWithValue("@objekat", tObjekat);
            cmd.Parameters.AddWithValue("@sifra", tSifra);
            cmd.Parameters.AddWithValue("@velicina", tObjekat);
            cmd.Parameters.AddWithValue("@kolicina", tObjekat);
            cmd.Parameters.AddWithValue("@ve", tObjekat);*/


            OdbcCommand komanda = new OdbcCommand("insert into pop_sta_mp_st (ozn_pop_sta,dat_pop,sif_obj_mp,sif_rob,sif_ent_rob,kolic,preneto) values ('"+tOznakaPopisa+"','"+ tdate.ToString(dateFormat) + "','"+tObjekat+"','"+tSifra+"','"+tVelicina+"','"+tKolicina+"','"+preneto+"')", konekcija);
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
        }

        //FRANSIZNI OBJEKTI

        public DataTable reportProvizija(string objekat, string dateFrom, string dateTo,string trziste)
        {
            string tObjekat = objekat;
            string tDateFrom = dateFrom;
            string tDateTo = dateTo;
            string tTrziste = trziste;

            DataTable tabelaProvizija = new DataTable();
            string connString = "Dsn=ifx;uid=informix";
            OdbcConnection conn = new OdbcConnection(connString);
            string cmd = "execute procedure test_provizija_fransiza('"+tObjekat+"','"+tDateFrom+"','"+tDateTo+"','"+tTrziste+"')";
            OdbcCommand komandaProcedure = new OdbcCommand(cmd, conn);
            komandaProcedure.CommandTimeout = 0;
            conn.Open();
            komandaProcedure.ExecuteNonQuery();

            string cmd1 = "select trim(vrsta) vrsta,trim(grupa) grupa,sum(vred_rab) vred_rab,sum(vred_pro) vred_pro from obrenovac group by grupa,vrsta order by grupa;";



            OdbcDataAdapter adapter1 = new OdbcDataAdapter(cmd1, conn);
            
            adapter1.Fill(tabelaProvizija);
            conn.Close();

            return tabelaProvizija;
        }

        public string getNazivRobe(string sifra)
        {
            string tSifra = sifra;
            string naziv = "";
            OdbcConnection konekcija = new OdbcConnection(Konekcija.konString);
            OdbcCommand komanda = new OdbcCommand("select top 1 naziv from ean_kod2 where bar_kod = '"+tSifra+"'" );
            OdbcDataReader dr = komanda.ExecuteReader();
            if (dr.Read())
            {
                naziv = dr[0].ToString();
            }
            else
            {
                
            }
            return naziv;

        }

        public void savePopis(DataTable tabela, string dokument, string objekat, DateTime date)
        {
            OdbcConnection konekcija = new OdbcConnection(Konekcija.konString);
            DateTime tdate = date;
            string dateFormat = "yyyy-MM-dd";
            foreach (DataRow row in tabela.Rows)
            {
                int preneto = 0;
                string sif_rob = row["sifra"].ToString();
                string sif_ent_rob = row["velicina"].ToString();
                int kol_pop = Convert.ToInt32(row["kolicina"].ToString());

                OdbcCommand komanda = new OdbcCommand("insert into pop_sta_mp_st (ozn_pop_sta,dat_pop,sif_obj_mp,sif_rob,sif_ent_rob,kolic,preneto) values ('" + dokument + "','" + tdate.ToString(dateFormat) + "','" + objekat + "','" + sif_rob + "','" + sif_ent_rob + "','" + kol_pop + "','" + preneto + "')", konekcija);
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

            }
        }

        public bool checkLoyaltiCard(string card)
        {
            bool provera = false;

            string tCard = card;
            string connString = "Dsn=ifx;uid=informix";

            OdbcConnection conn = new OdbcConnection(connString);

            OdbcCommand komandaKartica = new OdbcCommand("select * from platna_kartica where ozn_pla_kar = '"+tCard+"' and sta_pla_kar = 'N'",conn);
            OdbcCommand komandaPartner = new OdbcCommand("select * from pos_par where sif_par = '"+tCard+"' and sta_pos_par = '0'",conn);
            conn.Open();
            OdbcDataReader dr = komandaKartica.ExecuteReader();
            
            if (dr.Read())
            {
                conn.Close();
                conn.Open();
                OdbcDataReader drPartner = komandaKartica.ExecuteReader();
                if (drPartner.Read())
                {
                    conn.Close();
                    provera = true;
                }
                else { provera = false; }
            }
            else { provera = false; }

            return provera;

        }
        public bool checkLoyaltiCardCG(string card)
        {
            bool provera = false;

            string tCard = card;
            string connString = "Dsn=ifx;uid=informix";

            OdbcConnection conn = new OdbcConnection(connString);

            //OdbcCommand komandaKartica = new OdbcCommand("select * from platna_kartica where ozn_pla_kar = '" + tCard + "' and sta_pla_kar = 'N'", conn);
            OdbcCommand komandaPartner = new OdbcCommand("select * from pos_par where sif_par = '" + tCard + "' and sta_pos_par = '0'", conn);
            conn.Open();
            OdbcDataReader dr = komandaPartner.ExecuteReader();

            if (dr.Read())
            {
                conn.Close();
                conn.Open();
                OdbcDataReader drPartner = komandaPartner.ExecuteReader();
                if (drPartner.Read())
                {
                    conn.Close();
                    provera = true;
                }
                else { provera = false; }
            }
            else { provera = false; }

            return provera;

        }

    }
}
