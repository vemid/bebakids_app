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

namespace BebaKids.Racunovodstvo
{
    public partial class izvodi : Form
    {
        public izvodi()
        {
            InitializeComponent();
            Classes.Application izvodi = new Classes.Application();

            comboBox1.DataSource = izvodi.zuti_izvodi();
            comboBox1.DisplayMember = "display";
            comboBox1.ValueMember = "ozn_izv";
            comboBox1.SelectedIndex = -1;
        }

        private void btnOsveziIzvod_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(comboBox1.SelectedValue.ToString());

            string connString = "Dsn=ifx;uid=informix";
            if (comboBox1.SelectedValue.ToString() != "")
            {
                string cmd = "execute procedure test_update_izvod('"+comboBox1.SelectedValue.ToString()+"');";
                OdbcConnection conn = new OdbcConnection(connString);
                OdbcCommand komandaProcedure = new OdbcCommand(cmd, conn);
                conn.Open();

                OdbcDataReader dr = komandaProcedure.ExecuteReader();
                dr.Read();
                if (dr.GetString(1).ToString() == "1")
                {
                    MessageBox.Show("Uspesno osvezeni izvod", "Informacija", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else {
                    MessageBox.Show(dr.GetString(0).ToString(), "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                conn.Close();

                comboBox1.SelectedIndex = -1;
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
