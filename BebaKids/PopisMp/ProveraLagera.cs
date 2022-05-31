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
using System.Data.SqlClient;

namespace BebaKids.PopisMp
{
    public partial class ProveraLagera : Form
    {
        public ProveraLagera()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.main_favicon;
            this.ActiveControl = tBarkod;

        }

        private void textBoxTest_KeyDown(object sender, KeyEventArgs e)
        {
            string BARKOD = Convert.ToString(tBarkod.Text);
            string connString = "Dsn=ifx;uid=informix";

            string cmd1 = " select trim(o.naz_obj_mp) objekat,z.sif_rob sifra,trim(r.naz_rob) naziv,z.sif_ent_rob velicina,z.kolic-z.rez_kol kolicina from zal_robe_mp_zon z   " +
                                 " left join ean_kod b on b.sif_rob = z.sif_rob left join roba r on r.sif_rob = z.sif_rob left join obj_mp o on o.sif_obj_mp = z.sif_obj_mp  " +
                                 " where z.sif_obj_mp in (select sif_obj_mp from cg_radnje) and b.bar_kod = '" + BARKOD + "' " +
                                 " and z.kolic-z.rez_kol >0 order by z.sif_obj_mp,z.sif_Rob,z.sif_ent_Rob";

            OdbcConnection conn = new OdbcConnection(connString);
            OdbcDataAdapter adapter = new OdbcDataAdapter(cmd1, conn);

            conn.Open();
            DataTable table = new DataTable();
            table.Clear();
            adapter.Fill(table);


            conn.Close();

            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = table;
            dataGridView1.AutoResizeColumns();
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            Form1 Form1 = new Form1();
            Form1.Show();
        }
    }
}
