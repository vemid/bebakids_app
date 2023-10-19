using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Net;
using System.Collections.Specialized;
using MySql.Data.MySqlClient;
using System.Net.Http;
using System.Net.Http.Headers;

namespace BebaKids.Loyalti
{
    public partial class DodavanjeUceniska : Form
    {
        string baseUrl = "http://sms.vemid.net";
        string api = "/v1/messages/test";
        string clientId = "vemid";
        string clientSecret = "root";
        string sender = "BebaKids";

        private string apiUrl = "http://sms.vemid.net/v1/messages/test";
        private string username = "vemid";
        private string password = "root";
        private string token;

        public DodavanjeUceniska()
        {
            InitializeComponent();

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g;

            g = e.Graphics;
            int width = ClientRectangle.Width;
            int height = ClientRectangle.Height;
            Color color = System.Drawing.ColorTranslator.FromHtml("#3fb0ac");
            Pen myPen = new Pen(color);
            myPen.Width = 5;
            //g.DrawLine(myPen, 50, 300, 50, 300);fgh ffghfg
            Point tacka12 = new Point(30, 50);
            Point tacka22 = new Point(width - 30, 50);
            g.DrawLine(myPen, tacka12, tacka22);

            Pen litlePen = new Pen(color);
            litlePen.Width = 5;
            Point tackal1 = new Point(30, height - 20);
            Point tackal2 = new Point(width - 30, height - 20);
            g.DrawLine(litlePen, tackal1, tackal2);
        }

        public void clearAll()
        {
            foreach (TextBox tb in this.Controls.OfType<TextBox>())
            {
                tb.Clear();
            }

            foreach (MaskedTextBox tb in this.Controls.OfType<MaskedTextBox>())
            {
                tb.Clear();
            }

            foreach (CheckBox tb in this.Controls.OfType<CheckBox>())
            {
                tb.Checked = false;
            }
        }

        private void tbCard_Leave(object sender, EventArgs e)
        {

            Save cuvanje = new Save();
            List<Dictionary<string, object>> lista = cuvanje.checkLoyaltiCardExists(tbCard.Text.ToString());
            var provera = true;
            var test = "";
            if (lista.Count > 0)
            {
                tbLastName.Enabled = false;
                tbName.Enabled = false;
                tbId.Enabled = false;
                tbPhone.Enabled = false;
                tbGender.Enabled = false;
                tbLastName.Text = lista[0]["last_name"].ToString();
                tbName.Text = lista[0]["first_name"].ToString();
                tbPhone.Text = lista[0]["phone"].ToString();
                tbId.Text = lista[0]["id"].ToString();
                test = "Nije uneto";
                if (lista[0]["gender"].ToString() == "M")
                {
                    test = "Muski";
                }
                if (lista[0]["gender"].ToString() == "Z")
                {
                    test = "Zenski";
                }
                tbGender.Text = test.ToString();
                if (lista[0]["phone"].ToString().Count() > 10)
                {
                    btnAdd.Visible = true;
                }
                tbPhone.Enabled = true;
                btnChangePhone.Visible = true;
            }
            else
            {
                MessageBox.Show("Kartica nije validna, ili je vec otvorena !", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbLastName.Enabled = false;
                tbName.Enabled = false;

            }
        }

        protected async void btnAdd_Click(object sender, EventArgs e)
        {
            string name = tbName.Text.ToLower();
            string lastName = tbLastName.Text.ToLower();
            string phone = tbPhone.Text.ToString();
            string gender = tbGender.Text.ToString();
            string card = tbCard.Text.ToString();

            string trimPhone = tbPhone.Text.Replace(" ", "");
            trimPhone = trimPhone.Replace(")", "");
            trimPhone = trimPhone.Replace("(", "");


            string infinitiv = "Postovani/a ";

            if (gender == "Muski")
            {
                infinitiv = "Postovani ";
            }
            if (gender == "Zenski")
            {
                infinitiv = "Postovana ";
            }


            string smsName = name[0].ToString().ToUpper() + name.Substring(1);

            string[] array = { "a", "e", "i", "o", "u" };

            char lastChar = smsName[smsName.Length - 1];

            if (!array.Contains(lastChar.ToString()))
            {
                // Replace the last character with a hyphen.
                smsName = smsName + "e";
            }

            WebClient client = new WebClient();

            string url = "http://bulk.mobile-gw.com:9000";

            // Set the content type to application/x-www-form-urlencoded
            client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";


            // Create a collection to hold the request parameters
            NameValueCollection parameters = new NameValueCollection();

            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var code = new StringBuilder(4);

            for (int i = 0; i < 4; i++)
            {
                code.Append(chars[random.Next(chars.Length)]);
            }

            var MyIni = new IniFile(@"C:\bkapps\config.ini");
            var objekat = MyIni.Read("sif_obj_mp", "ProveraDokumenta");

            DateTime thisDay = DateTime.Now;
            string dateFormat = "yyyy-MM-dd";
            string timeFormat = "HH:mm:ss";

            MySqlConnection mysql = new MySqlConnection(MysqlB2B.myConnectionString);
            MySqlCommand command = mysql.CreateCommand();
            command.CommandText = "insert into campaing_participations (sif_obj_mp, datum, vreme, card,code,article_number) VALUES ('" + objekat + "','" + thisDay.ToString("yyyy-MM-dd") + "','" + thisDay.ToString(timeFormat) + "','" + card + "','" + code + "','" + tbArticleNumber.Text.ToString() + "')"; ;

            mysql.Open();
            int insertResult = command.ExecuteNonQuery();
            mysql.Close();

            string messageString = infinitiv + smsName + " , Hvala Vam sto ste postali deo naseg pojekta DECA SA VELIKIM D. Poklanjamo Vam 25% popusta na BEBAKIDS ne snizene artikle.Vas ID broj je : " + code.ToString();

            if (insertResult == 1)
            {

                await getBearerTokenAsync("00" + trimPhone, messageString);

                MessageBox.Show("Uspesno dodat ucesnik !", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clearAll();

                this.Hide();
                Form1 Form1 = new Form1();
                Form1.Show();


            }

        }

        private void button1_Click(object sender, EventArgs e)
        {

            string id = tbId.Text.ToString();

            MySqlConnection mysql = new MySqlConnection(MysqlB2B.myConnectionString);
            MySqlCommand command = mysql.CreateCommand();
            command.CommandText = "update loyalti set phone = '" + tbPhone.Text.ToString() + "' where id = '" + id + "'"; ;

            mysql.Open();
            command.ExecuteNonQuery();
            mysql.Close();
            MessageBox.Show("Uspesno ste promeni telefon  ! ", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (tbPhone.Text.ToString().Count() > 10)
            {
                btnAdd.Visible = true;
            }
            else
            {
                btnAdd.Visible = false;
            }

        }

        private void addNewParticipations_Load(object sender, EventArgs e)
        {
            System.ComponentModel.ComponentResourceManager resources =
    new System.ComponentModel.ComponentResourceManager(typeof(DodavanjeUceniska));

            this.Icon = Properties.Resources.main_favicon;
        }

        private async Task getBearerTokenAsync(string recipient, string text)
        {
            HttpClient client = new HttpClient();

            Uri baseUri = new Uri(baseUrl);

            client.BaseAddress = baseUri;
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.ConnectionClose = true;


            var values = new List<KeyValuePair<string, string>>();
            values.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
            values.Add(new KeyValuePair<string, string>("sender", sender));
            values.Add(new KeyValuePair<string, string>("recipient", recipient));
            values.Add(new KeyValuePair<string, string>("text", text));
            var content = new FormUrlEncodedContent(values);

            var authenticationString = $"{clientId}:{clientSecret}";
            var base64EncodedAuthenticationString = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(authenticationString));

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, api);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);
            requestMessage.Content = content;

            //make the request
            var task = await client.SendAsync(requestMessage);


        }

    }
}
