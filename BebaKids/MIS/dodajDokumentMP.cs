using ClosedXML.Excel;
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
using System.Data.SqlClient;
using System.ComponentModel;
using BebaKids.Models;
using BebaKids.Models.Controllers;

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
                new ItemWithTextAndValue("Nalog otpreme MP", "NO"),
                new ItemWithTextAndValue("Narudzbenica", "N1"),

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

            dc = new DataColumn("id", typeof(string));
            dt.Columns.Add(dc);

            return dt;

        }

        DataTable tabela = new DataTable();
        int i = 0;
        Classes.ErrorLogger errorLogger = new Classes.ErrorLogger();
        EanKodController eanKodController = new EanKodController();
        PopisController popisController = new PopisController();

        IniFile MyIni = new IniFile(@"C:\bkapps\config.ini");

        public void btnNoviPopis_Click(object sender, EventArgs e)
        {
            deleteOldRecords();

            Classes.Application test = new Classes.Application();
            if (test.testKonekcija())
            {

                if (comboBoxMagacin.SelectedIndex != -1 && comboBox1.SelectedIndex != -1 && cbDocumentType.SelectedIndex != -1)
                {
                    var MyIni = new IniFile(@"C:\bkapps\config.ini");
                    string objekatToMp = comboBox1.SelectedValue.ToString();
                    if (objekatToMp.Length == 1)
                    {
                        objekatToMp = "0" + objekatToMp;
                    }
                    var objekat = comboBoxMagacin.SelectedValue.ToString();
                    DateTime d = datumDokumenta.Value.Date;
                    var organizacija = MyIni.Read("organizacija", "ProveraDokumenta");
                    string connString = "Dsn=ifx;uid=informix";
                    string cmd = "execute procedure test_get_oznaka_dokumenta('" + cbDocumentType.SelectedValue.ToString() + "','" + objekatToMp + "','" + organizacija + "','" + objekat + "','" + d.ToString("dd.MM.yyyy") + "')";
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
                    cbDocumentType.Enabled = false;
                    comboBox1.Enabled = false;
                    comboBoxMagacin.Enabled = false;
                    tbBarkod.Enabled = true;
                    btnExport.Visible = true;
                    btnPrenesi.Visible = true;
                    progressBar1.Visible = true;
                    tbBarkod.Focus();
                    tabela = popisMP();
                }
                else
                {
                    MessageBox.Show("Nisu popunjena sva polja", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            if (e.KeyCode == Keys.Enter)
            {
                var oznakaDokumenta = textBox1.Text.ToString();
                DateTime d = DateTime.ParseExact(datumDokumenta.Value.ToShortDateString(), "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture);
                Classes.Application sound = new Classes.Application();

                if (e.KeyCode == Keys.Enter && !string.IsNullOrEmpty(tbBarkod.Text.ToString()))
                {
                    string BARKOD = Convert.ToString(tbBarkod.Text);
                    int objekat = Convert.ToInt32(comboBox1.SelectedValue.ToString());
                    var product = searchForBarKod(BARKOD);

                    if (product == null)
                    {
                        EanKod tProdukt = new EanKod();
                        sound.playSound("errorSifra");

                        frmRucniUnos frmRucniUnos = new frmRucniUnos();
                        if (frmRucniUnos.ShowDialog() == DialogResult.OK)
                        {
                            int iKolicina = frmRucniUnos.tkolicina;
                            string sifra = frmRucniUnos.tsifra.ToString();
                            string velicina = frmRucniUnos.tvelicina.ToString();
                            tProdukt.sifraRobe = sifra;
                            tProdukt.VelicinaRobe = velicina;
                            eanKodController.AddRelatedItem(tProdukt);
                            DataRow dr = tabela.NewRow();
                            dr[0] = "";
                            dr[1] = tProdukt.sifraRobe;
                            dr[2] = tProdukt.VelicinaRobe;
                            dr[3] = Convert.ToInt32(iKolicina);
                            dr[4] = 1;
                            tabela.Rows.Add(dr);
                            i++;


                            lbVelicina.Text = tProdukt.VelicinaRobe.ToString();
                            lbSifra.Text = tProdukt.sifraRobe.ToString();

                            tbBarkod.Clear();
                        }


                    }
                    else
                    {
                        if (product.VelicinaRobe == "")
                        {

                            sound.playSound("error");
                            frmVelicina frmVelicina = new frmVelicina();
                            if (frmVelicina.ShowDialog() == DialogResult.OK || !string.IsNullOrEmpty(frmVelicina.tbvelicina.ToString()))
                            {
                                lbVelicina.Text = frmVelicina.tbvelicina.ToString();
                                lbSifra.Text = product.sifraRobe;
                                lbNaziv.Text = product.naziv;
                                tbBarkod.Clear();

                                product.VelicinaRobe = frmVelicina.tbvelicina.ToString();
                                int iKolicina = 1;

                                eanKodController.AddRelatedItem(product);
                                DataRow dr = tabela.NewRow();
                                dr[0] = product.naziv;
                                dr[1] = product.sifraRobe;
                                dr[2] = product.VelicinaRobe;
                                dr[3] = Convert.ToInt32(iKolicina);
                                dr[4] = 1;
                                tabela.Rows.Add(dr);
                                i++;
                            }
                            else { MessageBox.Show("Niste uneli velicinu"); }
                        }
                        else
                        {
                            lbSifra.Text = product.sifraRobe;
                            lbNaziv.Text = product.naziv;
                            lbVelicina.Text = product.VelicinaRobe;
                            tbBarkod.Clear();

                            int iKolicina = 1;

                            eanKodController.AddRelatedItem(product);
                            DataRow dr = tabela.NewRow();
                            dr[0] = product.naziv;
                            dr[1] = product.sifraRobe;
                            dr[2] = product.VelicinaRobe;
                            dr[3] = Convert.ToInt32(iKolicina);
                            dr[4] = 1;
                            tabela.Rows.Add(dr);
                            i++;
                        }
                    }
                }

                dataGridView1.DataSource = tabela;
                dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.RowCount - 1;
                dataGridView1.AutoResizeColumns();

            }
        }
        /*
        private void textBoxTest_KeyDown(object sender, KeyEventArgs e)
        {

            var oznakaDokumenta = textBox1.Text.ToString();
            DateTime d = DateTime.ParseExact(datumDokumenta.Value.ToShortDateString(), "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture);
            Classes.Application sound = new Classes.Application();

            if (e.KeyCode == Keys.Enter && !string.IsNullOrEmpty(tbBarkod.Text.ToString()))
            {
                string BARKOD = Convert.ToString(tbBarkod.Text);
                int objekat = Convert.ToInt32(comboBox1.SelectedValue.ToString());
                Save saveBarkod = new Save();
                DataTable barkodovi = saveBarkod.barkodovi(BARKOD);
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
                        int id = cuvanje.insertPopis(oznakaDokumenta, objekat, d, iSifra, iVelicina, Convert.ToInt32(iKolicina));
                        DataRow dr = tabela.NewRow();
                        dr[0] = "";
                        dr[1] = iSifra;
                        dr[2] = iVelicina;
                        dr[3] = Convert.ToInt32(iKolicina);
                        dr[4] = id;
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
                            int id = cuvanje.insertPopis(oznakaDokumenta, objekat, d, iSifra, iVelicina, Convert.ToInt32(iKolicina));
                            DataRow dr = tabela.NewRow();
                            dr[0] = barkodovi.Rows[result].ItemArray[1].ToString();
                            dr[1] = iSifra;
                            dr[2] = iVelicina;
                            dr[3] = Convert.ToInt32(iKolicina);
                            dr[4] = id;
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
                        tbBarkod.Clear();

                        string iVelicina = barkodovi.Rows[result].ItemArray[4].ToString();
                        string iSifra = barkodovi.Rows[result].ItemArray[2].ToString();
                        int iKolicina = 1;

                        Save cuvanje = new Save();
                        int id = cuvanje.insertPopis(oznakaDokumenta, objekat, d, iSifra, iVelicina, Convert.ToInt32(iKolicina));
                        DataRow dr = tabela.NewRow();
                        dr[0] = barkodovi.Rows[result].ItemArray[1].ToString();
                        dr[1] = iSifra;
                        dr[2] = iVelicina;
                        dr[3] = Convert.ToInt32(iKolicina);
                        dr[4] = id;
                        tabela.Rows.Add(dr);
                        i++;
                    }
                }

            }

            Save save = new Save();

            dataGridView1.DataSource = save.popisTable(textBox1.Text.ToString(), "insert");
            dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.RowCount - 1;
            dataGridView1.AutoResizeColumns();
            dataGridView1.ReadOnly = true;
        }
        */
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            OdbcConnection conn = new OdbcConnection(Konekcija.konString);
            if (dataGridView1.CurrentCell.ColumnIndex == 0)
            {
                dataGridView1.Rows.RemoveAt(e.RowIndex);
                /*
                if (dataGridView1.Rows.Count > 2)
                {
                    int rowID = e.RowIndex;
                    string idCell = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();

                    OdbcCommand komanda = new OdbcCommand("delete from pop_sta_mp_st where ozn_pop_sta = '" + textBox1.Text.ToString() + "' and id = '" + idCell + "'", conn);
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
                    dataGridView1.DataSource = save.popisTable(textBox1.Text.ToString(), "insert");

                    int lastIndex = dataGridView1.Rows.Count - 1;
                    dataGridView1.FirstDisplayedScrollingRowIndex = lastIndex;

                }
                */

                dataGridView1.DataSource = tabela;
                int lastIndex = dataGridView1.Rows.Count - 1;
                dataGridView1.FirstDisplayedScrollingRowIndex = lastIndex;
                dataGridView1.AutoResizeColumns();
                dataGridView1.ReadOnly = true;
                tbBarkod.Focus();

            }
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text.ToString()))
            {
                string objekatToMp = comboBox1.SelectedValue.ToString();
                if (objekatToMp.Length == 1)
                {
                    objekatToMp = "0" + objekatToMp;
                }

                DateTime d = DateTime.ParseExact(datumDokumenta.Value.ToShortDateString(), "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture);
                saveToPopis(textBox1.Text.ToString(), d, MyIni.Read("sif_obj_mp", "ProveraDokumenta"), tabela);
                sendRequest(textBox1.Text.ToString(), comboBoxMagacin.SelectedValue.ToString(), objekatToMp, cbDocumentType.SelectedValue.ToString());
            }
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
            string objekatToMp = comboBox1.SelectedValue.ToString();
            if (objekatToMp.Length == 1)
            {
                objekatToMp = "0" + objekatToMp;
            }
            DateTime d = DateTime.ParseExact(datumDokumenta.Value.ToShortDateString(), "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture);

            saveToPopis(dokument, d, objekatToMp, tabela);
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
                    errorLogger.LogException(ex);
                    MessageBox.Show(ex.Message);

                }
                progressBar1.Value = i;
            }
            MessageBox.Show("Uspesno preneti podaci", "Obavestenje", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                
                try
                {
                    HttpResponseMessage response = await client.PostAsync(servisUrl(documentType), content);
                    string responseContent = await response.Content.ReadAsStringAsync();
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(responseContent);
                    XmlNode errorMessageNode = xmlDoc.SelectSingleNode("//errorMessage");
                    if (errorMessageNode != null)
                    {
                        string log = "Za dokument " + document + " postoji greska : " + errorMessageNode.InnerText;
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
                url = urlServis + "/OptremnicaServisPort?wsdl";
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

            string cmd = "select trim(p.sif_rob) sifra,trim(p.sif_ent_rob) velicina,obc.ozn_cen cenovnik," +
                            "nvl(z.cen_zal, 0) nabavna,nvl(c.vel_cen, 0) vp,nvl(c.mal_cen, 0) mp,round(nvl(c.mal_cen, 0) * 0.8333, 3) mp_bpdv,trim(nvl(o.sif_par_obj,'000000')) partner,sum(p.kol_pop) kolicina from pop_sta_mp_st p " +
                            "left join zal_robe_mag z on z.sif_rob = p.sif_rob and z.sif_mag = '" + objekatFrom + "' " +
                            "left join proiz_cen_obj_mp obc on obc.sif_obj_mp = '" + objekatTo + "' " +
                            "left join proiz_cen_st c on c.sif_rob = p.sif_rob and c.sta_cen_st = 'A' and c.ozn_cen = obc.ozn_cen " +
                            "left join obj_mp o on obc.sif_obj_mp = o.sif_obj_mp " +
                            "where p.ozn_pop_sta = '" + document + "' group by 1,2,3,4,5,6,7,8";

            List<DataRecord> dataCollection = new List<DataRecord>();

            using (OdbcCommand command = new OdbcCommand(cmd, conn))
            {
                conn.Open();
                using (OdbcDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DataRecord dataRecord = new DataRecord
                        {
                            sifra = reader["sifra"].ToString(), 
                            velicina = reader["velicina"].ToString(),
                            cenovnik = reader["cenovnik"].ToString(),
                            partner = reader["partner"].ToString(),
                            nabavna = double.Parse(reader["nabavna"].ToString()),
                            kolicina = double.Parse(reader["kolicina"].ToString()),
                            vp = double.Parse(reader["vp"].ToString()),
                            mp = double.Parse(reader["mp"].ToString()),
                            mp_bpdv = double.Parse(reader["mp_bpdv"].ToString()),
                        };

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
                returnXml = generateDodajNalogZaIzdavanje(document, dataCollection, objekatFrom, objekatTo);
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
            DateTime d = datumDokumenta.Value.Date;

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
                                <datumDokumenta>{d.ToString("yyyy-MM-dd")}</datumDokumenta>
                                <dpo>{d.ToString("yyyy-MM-dd")}</dpo>
                                <logname>elektronska.servis</logname>
                                <oznakaCenovnika>{items[0].cenovnik}</oznakaCenovnika>
                                <oznakaDokumenta>{document}</oznakaDokumenta>
                                <sifraMagacina>{objekatFrom}</sifraMagacina>
                                <sifraObjekta>{objekatTo}</sifraObjekta>
                                <sifraOrganizacioneJedinice>{organizacija}</sifraOrganizacioneJedinice>
                                <status>0</status>
                                {stavkeXml}
                                <storno>N</storno>
                                <napomena>{tbComment.Text.ToString()}</napomena>
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
            DateTime d = datumDokumenta.Value.Date;

            List<string> stavkeElements = new List<string>();
            foreach (var dataItem in items)
            {
                string stavkeElement = $@"
                <stavke>
                    <akcijskaStopaRabata>0</akcijskaStopaRabata>
                    <cenaZalihe>{dataItem.nabavna}</cenaZalihe>
                    <kolicina>{dataItem.kolicina}</kolicina>
                    <osnovnaCena>{dataItem.mp}</osnovnaCena>
                    <prodajnaCena>{dataItem.mp}</prodajnaCena>
                    <prodajnaCenaBezPoreza>{dataItem.mp_bpdv}</prodajnaCenaBezPoreza>
                    <prodajnaCenaSaRabatom>{dataItem.mp}</prodajnaCenaSaRabatom>
                    <sifraObelezja>{dataItem.velicina}</sifraObelezja>
                    <sifraRobe>{dataItem.sifra}</sifraRobe>
                    <sifraTarifneGrupePoreza>100</sifraTarifneGrupePoreza>
                    <stopaPDV>20</stopaPDV>
                    <stopaRabata>0</stopaRabata>
                    <maloprodajnaMarza>0</maloprodajnaMarza>
                    <posebnaStopaRabata>0</posebnaStopaRabata>
                    <brojPakovanja>{dataItem.kolicina}</brojPakovanja>
                </stavke>";

                i++;

                stavkeElements.Add(stavkeElement);
            }

            string stavkeXml = string.Join("", stavkeElements);


            string soapRequest = $@"<?xml version='1.0' encoding='utf-8'?>
                <soap:Envelope xmlns:soap='http://schemas.xmlsoap.org/soap/envelope/' xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema'>
                    <soap:Body>
                        <dodajOtpremnicuUMaloprodaju xmlns='http://sifarnici.servis.mis.com/'>
                            <otpremnicaUMaloprodaju xmlns=''>
                                <datumDokumenta>{d.ToString("yyyy-MM-dd")}</datumDokumenta>
                                <logname>elektronska.servis</logname>
                                <oznakaCenovnika>{items[0].cenovnik}</oznakaCenovnika>
                                <oznakaDokumenta>{document}</oznakaDokumenta>
                                <sifraMagacina>{objekatFrom}</sifraMagacina>
                                <sifraObjektaMaloprodaje>{objekatTo}</sifraObjektaMaloprodaje>
                                {stavkeXml}
                                <storno>N</storno>
                                <vrstaKnjizenja>2</vrstaKnjizenja>
                                <napomena>{tbComment.Text.ToString()}</napomena>
                            </otpremnicaUMaloprodaju>
                        </dodajOtpremnicuUMaloprodaju>
                    </soap:Body>
                </soap:Envelope>";

            return Regex.Replace(soapRequest, @"\t|\n|\r", "");
        }

        private string generateDodajNalogZaIzdavanje(string document, List<DataRecord> items, string objekatFrom, string objekatTo)
        {

            var MyIni = new IniFile(@"C:\bkapps\config.ini");
            var organizacija = MyIni.Read("organizacija", "ProveraDokumenta");
            DateTime d = datumDokumenta.Value.Date;

            List<string> stavkeElements = new List<string>();
            foreach (var dataItem in items)
            {
                string stavkeElement = $@"
                <stavke>
                    <akcijskaStopaRabata>0</akcijskaStopaRabata>
                    <brojPakovanja>0</brojPakovanja>
                    <bruto>0</bruto>
                    <cenaSaRabatom>{dataItem.vp}</cenaSaRabatom>
                    <deviznaCena>{dataItem.vp}</deviznaCena>
                    <dodatniRabat>0</dodatniRabat>
                    <iznosAkcize>0</iznosAkcize>
                    <iznosManipulativnihTroskova>0</iznosManipulativnihTroskova>
                    <iznosTakse>0</iznosTakse>
                    <kolicina>{dataItem.kolicina}</kolicina>
                    <osnovnaCena>{dataItem.vp}</osnovnaCena>
                    <posebnaStopaRabata>0</posebnaStopaRabata>
                    <prodajnaCena>{dataItem.mp_bpdv}</prodajnaCena>
                    <rabatBezAkciza>0</rabatBezAkciza>
                    <sifraObelezja>{dataItem.velicina}</sifraObelezja>
                    <sifraRobe>{dataItem.sifra}</sifraRobe>
                    <stopaManipulativnihTroskova>0</stopaManipulativnihTroskova>
                    <stopaPoreza>20</stopaPoreza>
                    <stopaRabata>0</stopaRabata>
                    <zahtevanaKolicina>{dataItem.kolicina}</zahtevanaKolicina>
                </stavke>";

                i++;

                stavkeElements.Add(stavkeElement);
            }

            string stavkeXml = string.Join("", stavkeElements);


            string soapRequest = $@"<?xml version='1.0' encoding='utf-8'?>
                <soap:Envelope xmlns:soap='http://schemas.xmlsoap.org/soap/envelope/' xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema'>
                    <soap:Body>
                        <dodajNalogZaIzdavanje xmlns='http://dokumenti.servis.mis.com/'>
                            <dokumenti xmlns=''>
                                <akcijskiRabatVrednost>0</akcijskiRabatVrednost>
                                <brojRata>0</brojRata>
                                <cenaPrevoza>0</cenaPrevoza>
                                <datumDokumenta>{d.ToString("yyyy-MM-dd")}</datumDokumenta>
                                <datumOtpreme>{d.ToString("yyyy-MM-dd")}</datumOtpreme>
                                <dpo>{d.ToString("yyyy-MM-dd")}</dpo>
                                <valutaPlacanja>{DateTime.Now.ToString("yyyy-MM-dd")}</valutaPlacanja>
                                <logname>elektronska.servis</logname>
                                <napomena>123</napomena>
                                <oznakaCenovnika>{items[0].cenovnik}</oznakaCenovnika>
                                <oznakaDokumenta>{document}</oznakaDokumenta>
                                <realizovano>N</realizovano>
                                <sifraNacinaPlacanja>1</sifraNacinaPlacanja>
                                <sifraOrganizacioneJednice>{organizacija}</sifraOrganizacioneJednice>
                                <sifraMagacina>{objekatFrom}</sifraMagacina>
                                <status>0</status>
                                <stopaManipulativnihTroskova>0</stopaManipulativnihTroskova>
                                <iznosManipulativnihTroskova>0</iznosManipulativnihTroskova>
                                <iznosKasaSkonto>0</iznosKasaSkonto>
                                <iznosManipulativnihTroskova>0</iznosManipulativnihTroskova>
                                <kasaSkonto>0</kasaSkonto>
                                <marza>0</marza>
                                {stavkeXml}
                                <storno>N</storno>
                                <vrstaFakturisanja>1</vrstaFakturisanja>
                                <vrstaIzjave>2</vrstaIzjave>
                                <sifraRacuna>RB</sifraRacuna>
                                <sifraPartnera>{items[0].partner}</sifraPartnera>
                                <sifraPartneraKorisnika>{items[0].partner}</sifraPartneraKorisnika>
                                <napomena>{tbComment.Text.ToString()}</napomena>
                            </dokumenti>
                        </dodajNalogZaIzdavanje>
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
            public string partner { get; set; }
            public double kolicina { get; set; }
            public double nabavna { get; set; }
            public double vp { get; set; }
            public double mp { get; set; }
            public double mp_bpdv { get; set; }

        }

        private void PopisMpUnos_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
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

        private EanKod searchForBarKod(string barcode)
        {
            using (var context = new Models.MyContext())
            {
                var product = context.EanKod
                    .FirstOrDefault(p => p.barKod == barcode);

                return product;
            }
        }

        private void saveToPopis(string dokument, DateTime datumDokumenta, string objekatDokumenta, DataTable tableData)
        {

            foreach (DataRow row in tableData.Rows)
            {
                Popis popis = new Popis()
                {
                    oznakaDokumenta = dokument,
                    datum = datumDokumenta,
                    objekat = objekatDokumenta,
                    sifra = row["sifra"].ToString(),
                    velicina = row["velicina"].ToString(),
                    kolic = Convert.ToInt32(row["kolicina"].ToString()),
                    preneto = 0
                };

                popisController.AddRelatedItem(popis);
            }

            popisController.saveItemsToDatabase();
        }

        private void deleteOldRecords()
        {
            string query = $"DELETE FROM pop_sta_mp_st WHERE DATEDIFF(day, dat_pop, GETDATE()) > 30";
            
            var MyIni = new IniFile(@"C:\bkapps\config.ini");
            string Localbaza = MyIni.Read("konekcija", "Baza").ToString();

            using (SqlConnection connection = new SqlConnection(Localbaza))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        //errorLogger.LogStringException("Obrisano iz tabele popisa broj rekorda: " + rowsAffected);
                    }
                    catch (Exception ex)
                    {
                        errorLogger.LogException(ex);
                    }
                }
            }
        }
    }
}

