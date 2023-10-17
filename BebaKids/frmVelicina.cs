using System;
using System.Windows.Forms;

namespace BebaKids
{
    public partial class frmVelicina : Form
    {
        public frmVelicina()
        {
            InitializeComponent();
            //button1.Visible = false;
        }
        public static string tbvelicina = "";


        private void textBoxTest_KeyDown(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) && e.KeyCode == Keys.Enter)
            {
                MessageBox.Show("Niste uneli velicinu");
                textBox1.Focus();
            }
            else
            {
                if (e.KeyCode == Keys.Enter)
                {
                    tbvelicina = textBox1.Text;
                    //DialogResult.OK;
                    SendKeys.Send("{TAB}");
                }
            }
        }
        private void button_Click(object sender, EventArgs e)
        {
            tbvelicina = textBox1.Text;
        }
    }

}
