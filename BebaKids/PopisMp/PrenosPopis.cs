using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Odbc;

namespace BebaKids.PopisMp
{
    public partial class PrenosPopis : Form
    {
        public PrenosPopis()
        {
            InitializeComponent();
        }
        private void btnPrenesi_Click(object sender, EventArgs e)
        {
            OdbcConnection konekcija = new OdbcConnection(Konekcija.konString);
            string connString = "Dsn=ifx;uid=informix";
            OdbcConnection conn = new OdbcConnection(connString);
            Save save = new Save();
            
            DataTable table = new DataTable();
            table = save.popisTable("", "prenos");
            var i = 0;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = table.Rows.Count;
            foreach (DataRow row in table.Rows)
            {
                i++;
                string ozn_pop_sta = row["ozn_pop_sta"].ToString();
                string sif_rob = row["sifra"].ToString();
                string sif_ent_rob = row["velicina"].ToString();
                int kol_pop = Convert.ToInt32(row["kolicina"].ToString());
                int id = Convert.ToInt32(row["id"].ToString());

                StringBuilder insertPopis = new StringBuilder();
                insertPopis.Append("insert into pop_sta_mp_st (ozn_pop_sta,rbr,sif_rob,sif_ent_rob,kol_pop) values ('" + ozn_pop_sta + "','" + id + "','" + sif_rob + "','" + sif_ent_rob + "','" + kol_pop + "')");
                OdbcCommand komandaUpdate = new OdbcCommand(insertPopis.ToString(), conn);
                

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
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            Form1 Form1 = new Form1();
            Form1.Show();
        }

        private void PrenosPopis_Load(object sender, EventArgs e)
        {
            Save save = new Save();
            dataGridView1.DataSource = save.popisTable("", "popis");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            OdbcConnection conn = new OdbcConnection(Konekcija.konString);
            if (dataGridView1.CurrentCell.ColumnIndex == 0)
            {
                int row = dataGridView1.CurrentCell.RowIndex;
                string id = dataGridView1.CurrentRow.Cells[1].Value.ToString();

                OdbcCommand komanda = new OdbcCommand("delete from pop_sta_mp_st where  id = '" + id + "'", conn);
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
                dataGridView1.DataSource = save.popisTable("popis", "popis");

            }
        }
    }
}
