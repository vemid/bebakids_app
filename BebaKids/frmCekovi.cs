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

namespace BebaKids
{
    public partial class frmCekovi : Form
    {
        public frmCekovi()
        {
            InitializeComponent();
        }

        public void frmProveraPrijemnice_Load(object sender, EventArgs e)
        {

            var MyIni = new IniFile(@"C:\bkapps\config.ini");
            var objekat = MyIni.Read("sif_obj_mp", "ProveraDokumenta").ToString();

            Save save = new Save();
            DataTable tabelaCekova = new DataTable();
            tabelaCekova = save.getCekovi(objekat);

            dataGridView1.DataSource = tabelaCekova;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var MyIni = new IniFile(@"C:\bkapps\config.ini");
            var objekat = MyIni.Read("sif_obj_mp", "ProveraDokumenta").ToString();
            string connString = "Dsn=ifx;uid=informix";
            OdbcConnection conn = new OdbcConnection(connString);
            if (dataGridView1.CurrentCell.ColumnIndex == 0)
            {
                //MessageBox.Show(dataGridView1.CurrentCell.);
                int row = dataGridView1.CurrentCell.RowIndex;
                string broCek = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                //MessageBox.Show("broj ceka je "+broCek.ToString()+" i objekat je"+objekat.ToString());
                
                OdbcCommand komanda = new OdbcCommand("update cek_gra set realizovan = 1 where bro_cek = '"+broCek+"' and sif_obj_mp = '"+objekat+"'", conn);
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
                tabelaCekova = save.getCekovi(objekat);

                dataGridView1.DataSource = tabelaCekova;

            }
            
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            Form1 Form1 = new Form1();
            Form1.Show();
        }
    }
}
