﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Odbc;
using ClosedXML.Excel;

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
                
                var MyIni = new IniFile(@"C:\bkapps\config.ini");
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
            var objekat = textBox2.Text.ToString();
            DateTime d = DateTime.ParseExact(datumDokumenta.Value.ToShortDateString(), "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture);
            //Save cuvanje = new Save();
            //cuvanje.savePopis(tabela, dokument, objekat, d);

            
            OdbcConnection konekcija = new OdbcConnection(Konekcija.konString);
            string connString = "Dsn=ifx;uid=informix";
            OdbcConnection conn = new OdbcConnection(connString);

            string cmd = ("select * from pop_sta_mp_st where ozn_pop_sta = '"+dokument+"' and preneto = 0");
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
                insertPopis.Append("insert into pop_sta_mp_st (ozn_pop_sta,rbr,sif_rob,sif_ent_rob,kol_pop) values ('"+ozn_pop_sta+"','"+id+"','"+sif_rob+"','"+sif_ent_rob+"','"+kol_pop+"')");
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
            table = save.popisTable(textBox1.Text.ToString(),"insert");

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

        private void PopisMpUnos_Load(object sender, EventArgs e)
        {

        }
    }
}