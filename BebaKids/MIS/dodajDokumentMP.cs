﻿using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
namespace BebaKids.MIS
{
    public partial class dodajDokumentMP : Form
    {
        private OdbcDataAdapter barkod = null;
        string objekat;
        public dodajDokumentMP()
        {
            InitializeComponent();
            tbBarkod.Enabled = false;

            Classes.Application objekti = new Classes.Application();
            comboBox1.DataSource = objekti.objekti();
            comboBox1.DisplayMember = "naz_obj_mp";
            comboBox1.ValueMember = "sif_obj_mp";
            comboBox1.SelectedIndex = -1;

            comboBoxMagacin.DataSource = objekti.magacini();
            comboBoxMagacin.DisplayMember = "magacin";
            comboBoxMagacin.ValueMember = "sif_mag";
            comboBoxMagacin.SelectedIndex = -1;

            var items = new List<ItemWithTextAndValue>
            {
                new ItemWithTextAndValue("Otprema u MP", "OM"),
                new ItemWithTextAndValue("Otprema Fransize", "OT"),
                new ItemWithTextAndValue("Narudzbenica", "N1"),
                new ItemWithTextAndValue("Otprema u MP", "NO"),
            };


            cbDocumentType.DisplayMember = "DisplayText";
            cbDocumentType.ValueMember = "Value";
            cbDocumentType.DataSource = items;
            cbDocumentType.SelectedIndex = -1;

        }


        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private DataTable popisMP()
        {
            DataTable dt = new DataTable();

            DataColumn dc = new DataColumn("Naziv", typeof(String));
            dt.Columns.Add(dc);

            dc = new DataColumn("sifra", typeof(String));
            dt.Columns.Add(dc);

            dc = new DataColumn("velicina", typeof(String));
            dt.Columns.Add(dc);

            dc = new DataColumn("kolicina", typeof(String));
            dt.Columns.Add(dc);

            return dt;

        }

        DataTable tabela = new DataTable();
        int i = 0;


        public DataTable citajBarkod()
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


        public void btnNoviPopis_Click(object sender, EventArgs e)
        {
            Classes.Application test = new Classes.Application();
            if (test.testKonekcija())
            {

                if (comboBoxMagacin.SelectedIndex != -1 && comboBox1.SelectedIndex != -1 && cbDocumentType.SelectedIndex != -1)
                {
                    var MyIni = new IniFile(@"C:\bkapps\config.ini");
                    var sifraObjekta = MyIni.Read("naziv", "ProveraDokumenta");
                    var objekat = comboBoxMagacin.SelectedValue.ToString();
                    DateTime d = datumDokumenta.Value.Date;
                    var organizacija = MyIni.Read("organizacija", "ProveraDokumenta");
                    string connString = "Dsn=ifx;uid=informix";
                    string cmd = "execute procedure test_get_oznaka_dokumenta('"+cbDocumentType.SelectedValue.ToString()+"','" + objekat + "','" + organizacija + "')";
                    OdbcConnection conn = new OdbcConnection(connString);
                    OdbcCommand komandaProcedure = new OdbcCommand(cmd, conn);
                    conn.Open();
                    try
                    {
                        int a = komandaProcedure.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    string getOznaka = "select trim(oznaka) from temptOznaka";
                    OdbcCommand komandaGetOznaka = new OdbcCommand(getOznaka, conn);
                    OdbcDataReader dr = komandaGetOznaka.ExecuteReader();
                    dr.Read();
                    textBox1.Text = dr.GetString(0).ToString();
                    textBox2.Text = objekat.ToString();
                    conn.Close();
                    datumDokumenta.Enabled = false;
                    textBox1.Enabled = false;
                    tbBarkod.Enabled = true;
                    btnExport.Visible = true;
                    btnPrenesi.Visible = true;
                    progressBar1.Visible = true;
                    tbBarkod.Focus();
                    tabela = popisMP();
                }
                else
                {
                    MessageBox.Show("Nisu preneti popunjena sva polja", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            else
            {
                noConnection nc = new noConnection();
                nc.Show();
            }

        }


        private void textBoxTest_KeyDown(object sender, KeyEventArgs e)
        {

            var oznakaDokumenta = textBox1.Text.ToString();
            //var objekat = textBox2.Text.ToString;
            DateTime d = DateTime.ParseExact(datumDokumenta.Value.ToShortDateString(), "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture);
            //DateTime d = datumDokumenta.Value.Date;
            Classes.Application sound = new Classes.Application();
            //MessageBox.Show(d.ToString("dd.MM.yyyy"));

            if (e.KeyCode == Keys.Enter && !string.IsNullOrEmpty(tbBarkod.Text.ToString()))
            {
                string BARKOD = Convert.ToString(tbBarkod.Text);
                int objekat = Convert.ToInt32(textBox2.Text.ToString());
                Save saveBarkod = new Save();
                DataTable barkodovi = saveBarkod.barkodovi(BARKOD);
                //MessageBox.Show(saveBarkod.barkodovi(BARKOD).ToString());
                var result = barkodovi.Rows.IndexOf(barkodovi.AsEnumerable().Where(c => c.Field<String>(3) == BARKOD).FirstOrDefault());

                if (result == -1)
                {

                    sound.playSound("errorSifra");

                    frmRucniUnos frmRucniUnos = new frmRucniUnos();
                    if (frmRucniUnos.ShowDialog() == DialogResult.OK)
                    {
                        int iKolicina = frmRucniUnos.tkolicina;
                        string iSifra = frmRucniUnos.tsifra;
                        string iVelicina = frmRucniUnos.tvelicina;
                        Save cuvanje = new Save();
                        //cuvanje.insert(dokument, vrsta, objekat, iSifra, iVelicina, Convert.ToInt32(iKolicina));
                        cuvanje.insertPopis(oznakaDokumenta, objekat, d, iSifra, iVelicina, Convert.ToInt32(iKolicina));
                        DataRow dr = tabela.NewRow();
                        dr[0] = "";
                        dr[1] = iSifra;
                        dr[2] = iVelicina;
                        dr[3] = Convert.ToInt32(iKolicina);
                        tabela.Rows.Add(dr);
                        i++;


                        lbVelicina.Text = iVelicina.ToString();
                        lbSifra.Text = iSifra.ToString();

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
                            //cuvanje.insert(dokument, vrsta, objekat, iSifra, iVelicina, Convert.ToInt32(iKolicina));
                            cuvanje.insertPopis(oznakaDokumenta, objekat, d, iSifra, iVelicina, Convert.ToInt32(iKolicina));
                            DataRow dr = tabela.NewRow();
                            dr[0] = barkodovi.Rows[result].ItemArray[1].ToString();
                            dr[1] = iSifra;
                            dr[2] = iVelicina;
                            dr[3] = Convert.ToInt32(iKolicina);
                            tabela.Rows.Add(dr);
                            i++;
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
                        //cuvanje.insert(dokument, vrsta, objekat, iSifra, iVelicina, Convert.ToInt32(iKolicina));
                        cuvanje.insertPopis(oznakaDokumenta, objekat, d, iSifra, iVelicina, Convert.ToInt32(iKolicina));
                        DataRow dr = tabela.NewRow();
                        dr[0] = barkodovi.Rows[result].ItemArray[1].ToString();
                        dr[1] = iSifra;
                        dr[2] = iVelicina;
                        dr[3] = Convert.ToInt32(iKolicina);
                        tabela.Rows.Add(dr);
                        i++;
                    }
                }

            }

            Save save = new Save();

            //dataGridView1.DataSource = save.popisTable(textBox1.Text.ToString(),"insert");
            dataGridView1.DataSource = tabela;
            dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.RowCount - 1;
            dataGridView1.AutoResizeColumns();
            //dataGridView1.Sort(dataGridView1.Columns["col1"], ListSortDirection.Descending);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            OdbcConnection conn = new OdbcConnection(Konekcija.konString);
            if (dataGridView1.CurrentCell.ColumnIndex == 0)
            {
                int row = dataGridView1.CurrentCell.RowIndex;
                /* int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[1].Value);


                 OdbcCommand komanda = new OdbcCommand("delete from pop_sta_mp_st where ozn_pop_sta = '" + textBox1.Text.ToString() + "' and id = '" + id + "'", conn);
                 try
                 {
                     conn.Open();
                     komanda.ExecuteNonQuery();
                     conn.Close();
                 }
                 catch (Exception ex)
                 {
                     MessageBox.Show(ex.Message);
                 }
                 dataGridView1.DataSource = null;
                 dataGridView1.Rows.Clear();
                 Save save = new Save();
                 DataTable tabelaCekova = new DataTable();
                 dataGridView1.DataSource = save.popisTable(textBox1.Text.ToString(),"insert");
                 */
                //tabela.Rows[row].Delete();
                dataGridView1.DataSource = tabela;
                dataGridView1.AutoResizeColumns();
                //dataGridView1.Sort(dataGridView1.Columns["col1"], ListSortDirection.Descending);
                tbBarkod.Focus();

            }
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            Form1 Form1 = new Form1();
            Form1.Show();
        }

        private void btnPrenesi_Click(object sender, EventArgs e)
        {
            string dokument = textBox1.Text.ToString();
            var objekatFrom = comboBoxMagacin.SelectedValue.ToString();
            var objekatTo = comboBox1.SelectedValue.ToString();
            var vrsta = cbDocumentType.SelectedValue.ToString();
            DateTime d = DateTime.ParseExact(datumDokumenta.Value.ToShortDateString(), "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture);
            //Save cuvanje = new Save();
            //cuvanje.savePopis(tabela, dokument, objekat, d);


            OdbcConnection konekcija = new OdbcConnection(Konekcija.konString);
            string connString = "Dsn=ifx;uid=informix";
            OdbcConnection conn = new OdbcConnection(connString);

            string cmd = ("select * from pop_sta_mp_st where ozn_pop_sta = '" + dokument + "' and preneto = 0");
            konekcija.Open();
            OdbcDataAdapter adapter = new OdbcDataAdapter(cmd, konekcija);
            DataTable table = new DataTable();
            adapter.Fill(table);
            konekcija.Close();
            var i = 0;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = table.Rows.Count;
            foreach (DataRow row in table.Rows)
            {
                string ozn_pop_sta = row["ozn_pop_sta"].ToString();
                string sif_rob = row["sif_rob"].ToString();
                string sif_ent_rob = row["sif_ent_rob"].ToString();
                int kol_pop = Convert.ToInt32(row["kolic"].ToString());
                int id = Convert.ToInt32(row["id"].ToString());

                StringBuilder insertPopis = new StringBuilder();
                insertPopis.Append("insert into pop_sta_mp_st (ozn_pop_sta,rbr,sif_rob,sif_ent_rob,kol_pop) values ('" + ozn_pop_sta + "','" + id + "','" + sif_rob + "','" + sif_ent_rob + "','" + kol_pop + "')");
                OdbcCommand komandaUpdate = new OdbcCommand(insertPopis.ToString(), conn);
                i++;

                StringBuilder updatePopis = new StringBuilder();
                updatePopis.Append("update pop_sta_mp_st set preneto = 1 where id = '" + id + "'");
                OdbcCommand komandaPreneto = new OdbcCommand(updatePopis.ToString(), konekcija);

                try
                {
                    conn.Open();
                    int a = komandaUpdate.ExecuteNonQuery();
                    conn.Close();
                    if (a == 0)
                    {
                        MessageBox.Show("Nisu preneti podaci", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        konekcija.Open();
                        komandaPreneto.ExecuteNonQuery();
                        konekcija.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                progressBar1.Value = i;
            }
            MessageBox.Show("Uspesno preneti podaci", "Obavestenje", MessageBoxButtons.OK, MessageBoxIcon.Information);

            string objekatToMp = comboBox1.SelectedValue.ToString();
            if (objekatToMp.Length == 1)
            {
                objekatToMp = "0" + objekatToMp;
            }

            sendRequest(dokument, comboBoxMagacin.SelectedValue.ToString(), objekatToMp, cbDocumentType.SelectedValue.ToString());
            progressBar1.Value = 0;
            this.Hide();
            Form1 frm = new Form1();
            frm.Show();
        }

        private async void sendRequest(string document, string objekatFrom, string objekatTo, string documentType)
        {

            string soapRequest = generateRequestForMIS(document, objekatFrom, objekatTo, documentType);

            using (HttpClient client = new HttpClient())
            {

                HttpContent content = new StringContent(soapRequest, Encoding.UTF8, "text/xml");
                Classes.ErrorLogger errorLogger = new Classes.ErrorLogger();
                try
                {
                    HttpResponseMessage response = await client.PostAsync(servisUrl(documentType), content);
                    string responseContent = await response.Content.ReadAsStringAsync();
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(responseContent);
                    XmlNode errorMessageNode = xmlDoc.SelectSingleNode("//errorMessage");
                    if (errorMessageNode != null)
                    {
                        string log = "Za dokument " + document + "postoji greska : " + errorMessageNode.InnerText;
                        errorLogger.LogStringException(log);
                        MessageBox.Show(log, "Obavestenje", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
                catch (Exception ex)
                {

                    errorLogger.LogException(ex);
                }
            }
        }

        private string servisUrl(string documentType)
        {

            var MyIni = new IniFile(@"C:\bkapps\config.ini");
            var urlServis = MyIni.Read("webservis", "Servis");
            string url = "";

            if (documentType == "NO")
            {
                url = urlServis + "/NalogZaIzdavanjeMpServisPort?wsdl";
            }

            if (documentType == "OM")
            {
                url = urlServis + "/OptremnicaServisPort";
            }

            if (documentType == "OT")
            {
                url = urlServis + "/DodajNalogZaIzdavanjePort?wsdl";
            }

            return url;


        }

        private string generateRequestForMIS(string document, string objekatFrom, string objekatTo, string documentType)
        {

            string connString = "Dsn=ifx;uid=informix";
            OdbcConnection conn = new OdbcConnection(connString);

            // string cmd = ("select sif_rob sifra,sif_ent_rob velicina from pop_sta_mp_st where ozn_pop_sta = '" + document + "'");
            string cmd = "select trim(p.sif_rob) sifra,trim(p.sif_ent_rob) velicina,p.kol_pop kolicina,obc.ozn_cen cenovnik," +
                            "nvl(z.cen_zal, 0) nabavna,nvl(c.vel_cen, 0) vp,nvl(c.mal_cen, 0) mp,round(nvl(c.mal_cen, 0) * 0.8333, 3) mp_bpdv from pop_sta_mp_st p " +
                            "left join zal_robe_mag z on z.sif_rob = p.sif_rob and z.sif_mag = '"+objekatFrom+"' " +
                            "left join proiz_cen_obj_mp obc on obc.sif_obj_mp = '"+objekatTo+"' " +
                            "left join proiz_cen_st c on c.sif_rob = p.sif_rob and c.sta_cen_st = 'A' and c.ozn_cen = obc.ozn_cen " +
                            "where p.ozn_pop_sta = '01/230300003'";

            List<DataRecord> dataCollection = new List<DataRecord>();

            using (OdbcCommand command = new OdbcCommand(cmd, conn))
            {
                conn.Open();
                using (OdbcDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Create a custom data record object to store the data
                        DataRecord dataRecord = new DataRecord
                        {
                            sifra = reader["sifra"].ToString(), // Replace with actual column names
                            velicina = reader["velicina"].ToString(),
                            cenovnik = reader["cenovnik"].ToString(),
                            nabavna = double.Parse(reader["nabavna"].ToString()),
                            kolicina = double.Parse(reader["kolicina"].ToString()),
                            vp = double.Parse(reader["vp"].ToString()),
                            mp = double.Parse(reader["mp"].ToString()),
                            mp_bpdv = double.Parse(reader["mp_bpdv"].ToString()),
                        };

                        // Add the data record to the data collection
                        dataCollection.Add(dataRecord);
                    }
                }
                conn.Close();

            }

            string returnXml = "";

            if (documentType == "NO")
            {
                returnXml = generateDodajNalogZaIzdavanjeMP(document, dataCollection, objekatFrom, objekatTo);
            }

            if (documentType == "OM")
            {
                returnXml = generateDodajOtpremnicuMP(document, dataCollection, objekatFrom, objekatTo);
            }

            if (documentType == "OT")
            {
                returnXml = generateDodajNalogZaIzdavanjeMP(document, dataCollection, objekatFrom, objekatTo);
            }

            if (documentType == "N1")
            {
                returnXml = generateDodajNalogZaIzdavanjeMP(document, dataCollection, objekatFrom, objekatTo);
            }

            return returnXml;
        }

        private string generateDodajNalogZaIzdavanjeMP(string document, List<DataRecord> items, string objekatFrom, string objekatTo)
        {

            var MyIni = new IniFile(@"C:\bkapps\config.ini");
            var organizacija = MyIni.Read("organizacija", "ProveraDokumenta");

            List<string> stavkeElements = new List<string>();
            foreach (var dataItem in items)
            {
                string stavkeElement = $@"
                    <stavke>
                        <kolicina>{dataItem.kolicina}</kolicina>
                        <osnovnaCenabezPoreza>{dataItem.mp}</osnovnaCenabezPoreza>
                        <prodajnaCenaSaPopustom>{dataItem.mp}</prodajnaCenaSaPopustom>
                        <prodajnaCenaSaPorezom>{dataItem.mp}</prodajnaCenaSaPorezom>
                        <redniBroj>{i}</redniBroj>
                        <sifraOblezja>{dataItem.velicina}</sifraOblezja>
                        <sifraRobe>{dataItem.sifra}</sifraRobe>
                        <sifraTarifneGrupe>100</sifraTarifneGrupe>
                        <stopaPopusta>0</stopaPopusta>
                        <stopaPoreza>20</stopaPoreza>
                    </stavke>";

                i++;

                stavkeElements.Add(stavkeElement);
            }

            string stavkeXml = string.Join("", stavkeElements);


            string soapRequest = $@"<?xml version='1.0' encoding='utf-8'?>
                <soap:Envelope xmlns:soap='http://schemas.xmlsoap.org/soap/envelope/' xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema'>
                    <soap:Body>
                        <dodajNalogZaIzdavanjeMp xmlns='http://dokumenti.servis.mis.com/'>
                            <nalozi xmlns=''>
                                <datumDokumenta>{DateTime.Now.ToString("yyyy-MM-dd")}</datumDokumenta>
                                <dpo>{DateTime.Now.ToString("yyyy-MM-dd")}</dpo>
                                <logname>mis</logname>
                                <oznakaCenovnika>{items[0].cenovnik}</oznakaCenovnika>
                                <oznakaDokumenta>{document}</oznakaDokumenta>
                                <sifraMagacina>{objekatFrom}</sifraMagacina>
                                <sifraObjekta>{objekatTo}</sifraObjekta>
                                <sifraOrganizacioneJedinice>{organizacija}</sifraOrganizacioneJedinice>
                                <status>0</status>
                                {stavkeXml}
                                <storno>N</storno>
                            </nalozi>
                        </dodajNalogZaIzdavanjeMp>
                    </soap:Body>
                </soap:Envelope>";

            return Regex.Replace(soapRequest, @"\t|\n|\r", "");
        }

        private string generateDodajOtpremnicuMP(string document, List<DataRecord> items, string objekatFrom, string objekatTo)
        {

            var MyIni = new IniFile(@"C:\bkapps\config.ini");
            var organizacija = MyIni.Read("organizacija", "ProveraDokumenta");

            List<string> stavkeElements = new List<string>();
            foreach (var dataItem in items)
            {
                string stavkeElement = $@"
                <stavke>
                    <akcijskaStopaRabata>0</akcijskaStopaRabata>
                    <cenaZalihe>{}</cenaZalihe>
                    <kolicina>{}</kolicina>
                    <osnovnaCena>100</osnovnaCena>
                    <prodajnaCena>100</prodajnaCena>
                    <prodajnaCenaBezPoreza>83.333</prodajnaCenaBezPoreza>
                    <prodajnaCenaSaRabatom>100</prodajnaCenaSaRabatom>
                    <sifraObelezja>XX</sifraObelezja>
                    <sifraRobe>01032040</sifraRobe>
                    <sifraTarifneGrupePoreza>100</sifraTarifneGrupePoreza>
                    <stopaPDV>20</stopaPDV>
                    <stopaRabata>0</stopaRabata>
                    <maloprodajnaMarza>0</maloprodajnaMarza>
                    <posebnaStopaRabata>0</posebnaStopaRabata>
                </stavke>";

                i++;

                stavkeElements.Add(stavkeElement);
            }

            string stavkeXml = string.Join("", stavkeElements);


            string soapRequest = $@"<?xml version='1.0' encoding='utf-8'?>
                <soap:Envelope xmlns:soap='http://schemas.xmlsoap.org/soap/envelope/' xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema'>
                    <soap:Body>
                        <dodajNalogZaIzdavanjeMp xmlns='http://dokumenti.servis.mis.com/'>
                            <nalozi xmlns=''>
                                <datumDokumenta>{DateTime.Now.ToString("yyyy-MM-dd")}</datumDokumenta>
                                <logname>mis</logname>
                                <napomena>123</napomena>
                                <oznakaCenovnika>{items[0].cenovnik}</oznakaCenovnika>
                                <oznakaDokumenta>{document}</oznakaDokumenta>
                                <sifraMagacina>{objekatFrom}</sifraMagacina>
                                <sifraObjektaMaloprodaje>{objekatTo}</sifraObjektaMaloprodaje>
                                <status>0</status>
                                {stavkeXml}
                                <storno>N</storno>
                                <vrstaKnjizenja>2</vrstaKnjizenja>
                            </nalozi>
                        </dodajNalogZaIzdavanjeMp>
                    </soap:Body>
                </soap:Envelope>";

            return Regex.Replace(soapRequest, @"\t|\n|\r", "");
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            Save save = new Save();
            DataTable table = new DataTable();
            table = save.popisTable(textBox1.Text.ToString(), "insert");

            string text = textBox1.Text.ToString().Replace("/", "-");

            XLWorkbook wb = new XLWorkbook();
            var ws = wb.Worksheets.Add(table, text);

            var saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel files|*.xlsx",
                Title = "Sacuvajte prijemnice"
            };
            saveFileDialog.InitialDirectory = "c:\\";
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.FileName = text;
            //saveFileDialog.ShowDialog();
            if (saveFileDialog.ShowDialog() == DialogResult.OK && !String.IsNullOrWhiteSpace(saveFileDialog.FileName))
            {

                wb.SaveAs(saveFileDialog.FileName);

                MessageBox.Show("Uspesno exportovani podaci", "Obavestenje", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        public class DataRecord
        {
            public string sifra { get; set; }
            public string velicina { get; set; }

            public string cenovnik { get; set; }
            public double kolicina { get; set; }
            public double nabavna { get; set; }
            public double vp { get; set; }
            public double mp { get; set; }
            public double mp_bpdv { get; set; }

            // Add more properties for other columns
        }

        private void PopisMpUnos_Load(object sender, EventArgs e)
        {

        }

        public class ItemWithTextAndValue
        {
            public string DisplayText { get; set; }
            public string Value { get; set; }

            public ItemWithTextAndValue(string displayText, string value)
            {
                DisplayText = displayText;
                Value = value;
            }
        }
    }
}
