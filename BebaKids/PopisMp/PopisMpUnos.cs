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
using System.Data.SqlClient;
using System.ComponentModel;
using BebaKids.Models;
using BebaKids.Models.Controllers;

namespace BebaKids.PopisMp
{
    public partial class PopisMpUnos : Form
    {
        private OdbcDataAdapter barkod = null;
        public PopisMpUnos()
        {
            InitializeComponent();
            tbBarkod.Enabled = false;

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

                var sifraObjekta = MyIni.Read("naziv", "ProveraDokumenta");
                var objekat = MyIni.Read("sif_obj_mp", "ProveraDokumenta");
                DateTime d = datumDokumenta.Value.Date;
                var organizacija = MyIni.Read("organizacija", "ProveraDokumenta");
                string connString = "Dsn=ifx;uid=informix";
                string cmd = "execute procedure test_popis('ST','" + objekat + "','" + organizacija + "','" + d.ToString("dd.MM.yyyy") + "')";
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
                string getOznaka = "select trim(ozn_pop_sta) from tOznaka";
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
                    int objekat = Convert.ToInt32(textBox2.Text.ToString());
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
                DateTime d = DateTime.ParseExact(datumDokumenta.Value.ToShortDateString(), "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture);
                saveToPopis(textBox1.Text.ToString(), d, MyIni.Read("sif_obj_mp", "ProveraDokumenta"), tabela);
            }
            this.Hide();
            Form1 Form1 = new Form1();
            Form1.Show();
        }

        private void btnPrenesi_Click(object sender, EventArgs e)
        {
            string dokument = textBox1.Text.ToString();
            var objekat = textBox2.Text.ToString();
            DateTime d = DateTime.ParseExact(datumDokumenta.Value.ToShortDateString(), "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture);

            saveToPopis(dokument, d, objekat, tabela);
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
            progressBar1.Value = 0;
            this.Hide();
            Form1 frm = new Form1();
            frm.Show();
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
        }

        private void PopisMpUnos_Load(object sender, EventArgs e)
        {

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
                        errorLogger.LogStringException("Obrisano iz tabele popisa broj rekorda: " + rowsAffected);
                    }
                    catch (Exception ex)
                    {
                        errorLogger.LogException(ex);
                    }
                }
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
    }
}
