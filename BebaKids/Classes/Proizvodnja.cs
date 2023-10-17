using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Data.Odbc;
using System.IO;
using System.Net;
using System.Text;

namespace BebaKids.Classes
{
    class Proizvodnja
    {

        public string getNazivKolekcije(string oznaka)
        {
            string nazivKolekcije = "";
            string tOznaka = oznaka;
            string connString = "Dsn=ifx;uid=informix";
            OdbcConnection conn = new OdbcConnection(connString);
            string cmd = "select trim(naz_kla) from klas_robe where kla_ozn = '" + tOznaka + "'";
            OdbcCommand komandaGetKolekciija = new OdbcCommand(cmd, conn);
            conn.Open();
            OdbcDataReader dr = komandaGetKolekciija.ExecuteReader();

            if (dr.Read())
            {
                nazivKolekcije = dr.GetString(0).ToString();
            }
            else
            {
                nazivKolekcije = "Ne postoji kolekcija";
            }
            conn.Close();
            return nazivKolekcije;
        }

        public bool proveraSifreUBazi(string sifra)
        {
            bool provera = false;
            string tSifra = sifra;
            string connMysql = "Dsn=interno;uid=root;Pwd=710412";
            //OdbcConnection mysql = new OdbcConnection(connMysql);
            MySqlConnection mysql = new MySqlConnection(MysqlKonekcija.myConnectionString);
            MySqlCommand komanda = mysql.CreateCommand();
            komanda.CommandText = "select * from roba where sif_rob = '" + tSifra + "'";
            mysql.Open();
            MySqlDataReader dr = komanda.ExecuteReader(); ;
            if (dr.Read())
            {
                provera = false;
            }
            else { provera = true; }
            mysql.Close();

            return provera;
        }
        public Stream getImage(string sifRob)
        {
            string tSifRob = sifRob;
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://192.168.100.9/Razmena/skice/" + tSifRob + ".jpg");
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.Credentials = new NetworkCredential("bebakids", "bk");
            //FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            try
            {
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                return responseStream;
            }
            catch (WebException ex)
            {
                FtpWebResponse response = (FtpWebResponse)ex.Response;
                if (response.StatusCode ==
                    FtpStatusCode.ActionNotTakenFileUnavailable)
                {
                    //Does not exist
                }
                Stream responseStream = response.GetResponseStream();
                return responseStream;
            }

            //StreamReader reader = new StreamReader(responseStream);


        }

        public DataTable getRoba(string sif_rob)
        {
            DataTable tabela = new DataTable();
            string tSifra = sif_rob;

            MySqlConnection mysql = new MySqlConnection(MysqlKonekcija.myConnectionString);
            //MySqlCommand komanda = mysql.CreateCommand();
            StringBuilder komanda = new StringBuilder();
            komanda.Append("select * from roba where sif_rob = '" + tSifra + "'");
            MySqlDataAdapter adapter1 = new MySqlDataAdapter(komanda.ToString(), mysql);
            mysql.Open();
            adapter1.Fill(tabela);
            mysql.Close();
            return tabela;
        }

        public DataTable getKartica(string kartica, bool op)
        {
            DataTable tabela = new DataTable();
            string tSifra = kartica;

            MySqlConnection mysql = new MySqlConnection(MysqlKonekcija.myConnectionString);
            StringBuilder command = new StringBuilder(); ;

            if (!op)
            {
                command.Append("select number_of_card, parent_fabric,price_material from factories where number_of_card = '" + tSifra + "' and vrsta = 'O' order by id_factory_card desc limit 1 ");
            }
            else
            {
                command.Append("select number_of_card, parent_fabric,price_material from factories where fabric_code = '" + tSifra + "' and vrsta = 'P' order by id_factory_card desc limit 1 ");
            }
            MySqlDataAdapter adapter1 = new MySqlDataAdapter(command.ToString(), mysql);
            mysql.Open();
            adapter1.Fill(tabela);
            mysql.Close();
            return tabela;
        }

        public DataTable kalkulacijaItems(string sifra, string vrsta)
        {
            DataTable kalkulacijaItems = new DataTable();
            MySqlConnection mysql = new MySqlConnection(MysqlKonekcija.myConnectionString);
            StringBuilder command = new StringBuilder();
            command.Append("select ki.id_kartice Kartica,ki.naziv_mat Materijal,ki.cena_mat,ki.utrosak utrosak,ki.vrednost, ki.napomena Napomena from kalkulacija_items ki ");
            command.Append("left join kalkulacija k on k.id = ki.id_kalkulacije where k.sif_rob = '" + sifra.ToString() + "' and type = '" + vrsta.ToString() + "'");
            MySqlDataAdapter adapter1 = new MySqlDataAdapter(command.ToString(), mysql);
            try
            {
                mysql.Open();
                adapter1.Fill(kalkulacijaItems);
                mysql.Close();
            }
            catch (Exception ex)
            {

            }

            return kalkulacijaItems;
        }

        public DataTable kalkulacija(string sifra, string vrsta)
        {

            DataTable kalkulacijaItems = new DataTable();
            MySqlConnection mysql = new MySqlConnection(MysqlKonekcija.myConnectionString);
            StringBuilder command = new StringBuilder();
            command.Append("select r.*, k.kolic,k.type from roba r left join kalkulacija k on k.sif_rob = r.sif_rob where k.sif_rob = '" + sifra + "' and type = '" + vrsta + "'");
            MySqlDataAdapter adapter1 = new MySqlDataAdapter(command.ToString(), mysql);
            try
            {
                mysql.Open();
                adapter1.Fill(kalkulacijaItems);
                mysql.Close();
            }
            catch (Exception ex)
            {

            }

            return kalkulacijaItems;
        }

        public void btnObrisi(string sifra, string vrsta)
        {
            MySqlConnection mysql = new MySqlConnection(MysqlKonekcija.myConnectionString);
            MySqlCommand command = mysql.CreateCommand();
            command.CommandText = "select id from kalkulacija where sif_rob = '" + sifra + "' and type = '" + vrsta + "'";
            int id = 0;
            try
            {
                mysql.Open();
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    id = reader.GetInt32(0);
                }
                mysql.Close();
            }
            catch { }
            if (id > 0)
            {
                MySqlCommand delKalkulacija = mysql.CreateCommand();
                delKalkulacija.CommandText = "delete from kalkulacija where id = " + id + "";
                try
                {
                    mysql.Open();
                    delKalkulacija.ExecuteNonQuery();
                    mysql.Close();
                }
                catch (Exception ex) { }

                MySqlCommand delItems = mysql.CreateCommand();
                delItems.CommandText = "delete from kalkulacija_items where id_kalkulacije = " + id + "";
                try
                {
                    mysql.Open();
                    delItems.ExecuteNonQuery();
                    mysql.Close();
                }
                catch (MySqlException ex) { }
            }


        }
        public void kreiranjeCene(string sifra, decimal nabcen, decimal velcen, decimal malcen, decimal eur, decimal vpeur)
        {
            string connString = "Dsn=ifx;uid=informix";
            string cmd = "execute procedure test_kreiranje_cene('" + sifra + "'," + nabcen + "," + velcen + "," + malcen + "," + eur + "," + vpeur + ")";
            OdbcConnection conn = new OdbcConnection(connString);
            OdbcCommand komandaProcedure = new OdbcCommand(cmd, conn);
            conn.Open();
            try
            {
                int a = komandaProcedure.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
        }

        public void insertSirovinski(string sifra, string sirovinski)
        {
            string connString = "Dsn=ifx;uid=informix";
            string cmd = "execute procedure test_sirovinski('" + sifra + "','" + sirovinski + "')";
            OdbcConnection conn = new OdbcConnection(connString);
            OdbcCommand komandaProcedure = new OdbcCommand(cmd, conn);
            conn.Open();
            try
            {
                int a = komandaProcedure.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
        }

        public string getSifraArt(string rn)
        {

            string sifra = "";
            string connString = "Dsn=ifx;uid=informix";
            OdbcConnection conn = new OdbcConnection(connString);
            string cmd = "select trim(obj_ide) from rad_nal where nal_ozn = '" + rn + "'";
            OdbcCommand komandaGetKolekciija = new OdbcCommand(cmd, conn);
            conn.Open();
            OdbcDataReader dr = komandaGetKolekciija.ExecuteReader();

            if (dr.Read())
            {
                sifra = dr.GetString(0).ToString();
            }
            else
            {
                sifra = "Ne postoji radni nalog";
            }
            conn.Close();
            return sifra;
        }
    }
}
