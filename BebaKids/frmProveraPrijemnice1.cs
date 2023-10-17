using System;
using System.Data;
using System.Windows.Forms;


namespace BebaKids
{
    public partial class frmProveraPrijemnice1 : Form
    {
        public frmProveraPrijemnice1()
        {
            InitializeComponent();
        }
        string prijemnica = FrmProveraFakture.prijemnica;
        string vrsta = FrmProveraFakture.vrsta;
        DataTable Excel = new DataTable();

        public void frmProveraPrijemnice1_Load(object sender, EventArgs e)
        {
            //string prijemnica = frmPrijemnica.prijemnica;
            MessageBox.Show("Postovane Koleginice\n Molim Vas proverite sve stavke u tabeli\n da li su zaista ne slaganja u pakovanju robe!!\nTek nakon toga kliknite na dugme posalji!", "Obavestenje", MessageBoxButtons.OK, MessageBoxIcon.Information);

            var MyIni = new IniFile(@"C:\bkapps\config.ini");
            //var objekat = MyIni.Read("sif_obj_mp", "ProveraDokumenta").ToString();
            var SifPar = MyIni.Read("sif_par", "ProveraDokumenta").ToString();

            Save save = new Save();
            save.getTable1(vrsta, SifPar, prijemnica);
            Excel = save.table(prijemnica);

            dataGridView1.DataSource = save.table(prijemnica);
        }

        public void btnExcel_Click(object sender, EventArgs e)
        {
            var MyIni = new IniFile(@"C:\bkapps\config.ini");
            string prijemnica = FrmProveraFakture.prijemnica;
            string vrsta = FrmProveraFakture.vrsta.ToString();
            //MessageBox.Show(vrsta);
            string objekat = MyIni.Read("objekat", "ProveraDokumenta").ToString();



            Classes.Application export = new Classes.Application();
            export.createExcel(vrsta, objekat, prijemnica, Excel);

            MessageBox.Show("Uspesno poslati podaci", "Obavestenje", MessageBoxButtons.OK, MessageBoxIcon.Information);


            this.Hide();
            Form1 frm = new Form1();
            frm.Show();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Save save = new Save();
            save.completedDokument(prijemnica);

            this.Hide();
            Form1 Form1 = new Form1();
            Form1.Show();


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


            DialogResult result = MessageBox.Show("Da li ste sigurno proverili kolicinu sa dokumenta sa skeniranom kolicinom??", "Obavestenje", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
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
