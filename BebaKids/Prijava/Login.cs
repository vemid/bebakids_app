using System.Windows.Forms;

namespace BebaKids.Prijava
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void textBoxTest_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox1.Text.ToString() == "7104")
                {
                    this.Hide();
                    Prijava.Aktivnost aktivnost = new Aktivnost();
                    aktivnost.Show();
                }
                else
                {
                    MessageBox.Show("Netacna loznika");
                    textBox1.Clear();
                }
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
