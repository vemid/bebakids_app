using System;
using System.Data;
using System.Data.Odbc;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;

namespace BebaKids.Prijava
{
    public partial class PrijavaRadnika : Form
    {
        private OdbcDataAdapter users = null;
        public PrijavaRadnika()
        {
            InitializeComponent();
            label2.Text = "";
        }

        private async void sendRequestButton_Click(object sender, EventArgs e)
        {
            string url = "http://192.168.100.236/ServisMisWeb/services/NalogZaIzdavanjeMpServisPort?wsdl"; // Replace with the actual URL of your SOAP service.
            string soapRequest = GenerateSoapRequest(); // Create your SOAP request here.

            using (HttpClient client = new HttpClient())
            {
                // Set the content type and SOAP action header
                //client.DefaultRequestHeaders.Add("Content-Type", "text/xml; charset=utf-8");
                //client.DefaultRequestHeaders.Add("SOAPAction", "YourSOAPAction"); // Replace with your SOAP action.

                // Create the HTTP request content
                HttpContent content = new StringContent(soapRequest, Encoding.UTF8, "text/xml");

                try
                {
                    HttpResponseMessage response = await client.PostAsync(url, content);
                    string responseContent = await response.Content.ReadAsStringAsync();
                    textBox2.Text = responseContent;
                }
                catch (Exception ex)
                {
                    textBox2.Text = "Error: " + ex.Message;
                }
            }
        }

        // Generate your SOAP request here
        private string GenerateSoapRequest()
        {
            // Construct your SOAP request XML string here
            // Example:
            string soapRequest = @"<?xml version=""1.0"" encoding=""utf-8""?>
        <soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
            <soap:Body>
                <dodajNalogZaIzdavanjeMp xmlns=""http://dokumenti.servis.mis.com/"">
                    <nalozi xmlns="""">
                        <datumDokumenta>2023-10-16</datumDokumenta>
                        <dpo>2023-10-16</dpo>
                        <logname>mis</logname>
                        <oznakaDokumenta>M12</oznakaDokumenta>
                        <sifraMagacina>MP</sifraMagacina>
                        <sifraObjekta>02</sifraObjekta>
                        <sifraOrganizacioneJedinice>01</sifraOrganizacioneJedinice>
                        <status>0</status>
                        <stavke>
                            <kolicina>1</kolicina>
                            <osnovnaCenabezPoreza>83.3333</osnovnaCenabezPoreza>
                            <prodajnaCenaSaPopustom>100</prodajnaCenaSaPopustom>
                            <prodajnaCenaSaPorezom>100</prodajnaCenaSaPorezom>
                            <redniBroj>1</redniBroj>
                            <sifraOblezja>XX</sifraOblezja>
                            <sifraRobe>01032040</sifraRobe>
                            <sifraTarifneGrupe>100</sifraTarifneGrupe>
                            <stopaPopusta>0</stopaPopusta>
                            <stopaPoreza>20</stopaPoreza>
                            <zonaMagacina>DEF_ZON</zonaMagacina>
                        </stavke>
                        <stavke>
                            <kolicina>1</kolicina>
                            <osnovnaCenabezPoreza>83.3333</osnovnaCenabezPoreza>
                            <prodajnaCenaSaPopustom>100</prodajnaCenaSaPopustom>
                            <prodajnaCenaSaPorezom>100</prodajnaCenaSaPorezom>
                            <redniBroj>1</redniBroj>
                            <sifraOblezja>XX</sifraOblezja>
                            <sifraRobe>04315060F</sifraRobe>
                            <sifraTarifneGrupe>100</sifraTarifneGrupe>
                            <stopaPopusta>0</stopaPopusta>
                            <stopaPoreza>20</stopaPoreza>
                            <zonaMagacina>DEF_ZON</zonaMagacina>
                        </stavke>
                        <storno>N</storno>
                    </nalozi>
                </dodajNalogZaIzdavanjeMp>
            </soap:Body>
        </soap:Envelope>";

            return soapRequest;
        }

        public static string vrsta = "";
        private DataTable citajUsere()
        {
            OdbcConnection konekcija = new OdbcConnection(Konekcija.konString);

            DataSet ds = new DataSet();// kreiranje DataSet objekta
            users = new OdbcDataAdapter("select * from radnici", konekcija);//punjenje objekta sqladaptera sa podacima iz tab. users
            users.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            OdbcCommandBuilder builder = new OdbcCommandBuilder(users);//sqldataadapter komanda cita iz sqlbuildera
            users.Fill(ds, "radnici");//punjenj objekta
            DataTable tabelaUsera = ds.Tables["radnici"];//kreiraanje tabele koja prestavlja kopiju
            return tabelaUsera;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            Form1 Form1 = new Form1();
            Form1.Show();
        }

        private void textBoxTest_KeyDown(object sender, KeyEventArgs e)
        {
            var MyIni = new IniFile(@"C:\bkapps\config.ini");
            var Sifraobjekat = MyIni.Read("sif_obj_mp", "ProveraDokumenta");
            int objekat = Convert.ToInt32(Sifraobjekat.ToString());

            OdbcConnection konekcija = new OdbcConnection(Konekcija.konString);
            if (e.KeyCode == Keys.Enter)
            {
                DateTime thisDay = DateTime.Now;
                string dateFormat = "yyyy-MM-dd";
                string timeFormat = "HH:mm:ss";

                string date = thisDay.ToString(dateFormat);
                string time = thisDay.ToString(timeFormat);

                //MessageBox.Show(time);
                int test;
                if (!string.IsNullOrEmpty(textBox1.Text) && int.TryParse(textBox1.Text, out test))
                {
                    string ID = textBox1.Text;
                    DataTable tableUsera = citajUsere();

                    DataRow checkUser = tableUsera.Rows.Find(ID);

                    if (checkUser != null)
                    {
                        vrsta = Form1.vrsta.ToString();
                        //MessageBox.Show(vrsta);

                        OdbcCommand checkCheckin = new OdbcCommand("Select * from prijava where sifra ='" + checkUser["sifra"] + "' and date = '" + thisDay.ToString(dateFormat) + "' and sif_obj_mp = '" + objekat + "' and vrsta = '" + vrsta + "'", konekcija);
                        konekcija.Open();
                        OdbcDataAdapter adapt = new OdbcDataAdapter(checkCheckin);
                        DataSet ds = new DataSet();
                        adapt.Fill(ds);
                        konekcija.Close();
                        int count = ds.Tables[0].Rows.Count;
                        Save cuvanje = new Save();

                        if (count == 0)
                        {

                            cuvanje.insertPrijava(Convert.ToInt32(checkUser["sifra"].ToString()), objekat, vrsta);
                            label2.Text = "Radnik: " + checkUser["ime_i_prezime"] + " prijavljen u : " + time;
                        }
                        else
                        {

                            if (count == 1 && String.IsNullOrEmpty(ds.Tables[0].Rows[0]["check_out"].ToString()))
                            {
                                DateTime checkIn = Convert.ToDateTime(ds.Tables[0].Rows[0]["check_in"]);
                                TimeSpan difference = thisDay.Subtract(checkIn);
                                int hours = (thisDay - checkIn).Hours;
                                int minutes = (thisDay - checkIn).Minutes;
                                //MessageBox.Show(hours.ToString());
                                if (hours > 0)
                                {

                                    bool isRegular = true;
                                    int status = Convert.ToInt32(ds.Tables[0].Rows[0]["status"].ToString());

                                    if (hours <= 10)
                                    {
                                        isRegular = true;
                                        cuvanje.updatePrijava(Convert.ToInt32(ds.Tables[0].Rows[0]["id"].ToString()), isRegular, status, textBox2.Text.ToString());
                                        //MessageBox.Show(ds.Tables[0].Rows[0]["id"].ToString());
                                        label2.Text = "Radnik: " + checkUser["ime_i_prezime"] + " odjavljen u : " + time;
                                    }
                                    else
                                    {
                                        isRegular = false;
                                        cuvanje.updatePrijava(Convert.ToInt32(ds.Tables[0].Rows[0]["id"].ToString()), isRegular, status, textBox2.Text.ToString());
                                        label2.Text = "Radnik: " + checkUser["ime_i_prezime"] + " odjavljen u : " + time;
                                        //MessageBox.Show("dupla smena");
                                    }
                                }
                            }
                        }
                        if (count == 1 && !String.IsNullOrEmpty(ds.Tables[0].Rows[0]["check_out"].ToString()))
                        {
                            MessageBox.Show("Postovani, već ste prijavljeni !", "Informacija", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                    }
                    else
                    {
                        MessageBox.Show("Ne postoji takav korisnik !", "Informacija", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                textBox1.Clear();
            }

        }
    }
}
