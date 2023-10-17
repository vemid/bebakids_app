using System.Windows.Forms;

namespace BebaKids
{
    public partial class noConnection : Form
    {
        public noConnection()
        {
            InitializeComponent();
            //Thread.Sleep(3000);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            Form1 Form1 = new Form1();
            Form1.Show();
        }
    }
}
