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
    public partial class frmProveraPrijemnice : Form
    {
        public frmProveraPrijemnice()
        {
            InitializeComponent();
        }
        string prijemnica = frmPrijemnica.prijemnica;
        string vrsta = frmPrijemnica.vrsta;
        DataTable Excel = new DataTable();

        public void frmProveraPrijemnice_Load(object sender, EventArgs e)
        {
            Save save = new Save();

            //string prijemnica = frmPrijemnica.prijemnica;
            MessageBox.Show("Postovane Koleginice\n Molim Vas proverite sve stavke u tabeli\n da li su zaista ne slaganja u pakovanju robe!!\nTek nakon toga kliknite na dugme posalji!", "Obavestenje", MessageBoxButtons.OK, MessageBoxIcon.Information);

            var MyIni = new IniFile(@"C:\bkapps\config.ini");
            var objekat = MyIni.Read("sif_obj_mp", "ProveraDokumenta").ToString();



            save.getTable(vrsta, objekat, prijemnica);
            Excel = save.table(prijemnica);

            dataGridView1.DataSource = save.table(prijemnica);
        }

        public void btnExport_Click(object sender, EventArgs e)
        {
            if (Excel.Rows.Count > 0)
            {
                string prijemnica = frmPrijemnica.prijemnica;
                string vrsta = frmPrijemnica.vrsta.ToString();
                //MessageBox.Show(vrsta);
                string cmd = "";
                //var MyIni = new IniFile(@"C:\bkapps\config.ini");
                //var objekat = MyIni.Read("sif_obj_mp", "ProveraDokumenta").ToString();
                if (vrsta == "P9")
                {
                    cmd = " select o.e_mail from pren_mp p " +
                                 " left join obj_mp o on p.sif_obj_izl = o.sif_obj_mp " +
                                 " where p.ozn_pre_mp = '" + prijemnica + "'";
                }
                if (vrsta == "OM")
                {
                    cmd = " select o.e_mail from otprem_mp p " +
                                 " left join obj_mp o on p.sif_obj_mp = o.sif_obj_mp " +
                                 " where p.ozn_otp_mal = '" + prijemnica + "'";
                }
                if (vrsta == "MP")
                {
                    cmd = " select o.e_mail from povrat_mp p " +
                                 " left join obj_mp o on p.sif_obj_mp = o.sif_obj_mp " +
                                 " where p.ozn_pov_mp = '" + prijemnica + "'";
                }

                string connString = "Dsn=ifx;uid=informix";
                OdbcConnection conn = new OdbcConnection(connString);
                OdbcCommand komanda = new OdbcCommand(cmd, conn);
                conn.Open();
                OdbcDataReader dr = komanda.ExecuteReader();
                dr.Read();

                string email = dr.GetString(0);

                Classes.Application export = new Classes.Application();
                export.createExcel(vrsta, email, prijemnica, Excel);

                MessageBox.Show("Uspesno poslati podaci", "Obavestenje", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            this.Hide();
            Form1 frm = new Form1();
            frm.Show();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            Form1 Form1 = new Form1();
            Form1.Show();

            Save save = new Save();
            save.completedDokument(prijemnica);

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


            DialogResult result = MessageBox.Show("Da li ste sigruno proverili kolicinu sa dokumenta sa skeniranom kolicinom??", "Obavestenje", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                if (dataGridView1.CurrentCell.ColumnIndex == 0)
                {
                    {
                        int row = dataGridView1.CurrentCell.RowIndex;
                        string id = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                        Excel.Rows[row].Delete();
                        Excel.AcceptChanges();

                        dataGridView1.DataSource = null;
                        dataGridView1.Rows.Clear();
                        dataGridView1.DataSource = Excel;

                    }
                }
            }
        }
    }
}
