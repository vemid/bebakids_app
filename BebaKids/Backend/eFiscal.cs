using FirebirdSql.Data.FirebirdClient;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BebaKids.Backend
{
    public partial class eFiscal : Form
    {
        public eFiscal()
        {
            InitializeComponent();
            FbConnection fbConnection;

            FbConnectionStringBuilder builder = new FbConnectionStringBuilder
            {
                Charset = "WIN1252",
                DataSource = "localhost",
                Database = @"C:\MisPOS\Baza\data.gdb",
                Dialect = 3,
                Role = "READ_ONLY_OTN",
                UserID = "SYSDBA",
                Password = "masterkey",
                ServerType = FbServerType.Default
            };
            fbConnection = new FbConnection(builder.ConnectionString);
            fbConnection.Open();

            var command = fbConnection.CreateCommand();
            var sql = "SELECT trim(USER_R.PASS) FROM USER_R where R_USER = 'mis'";

            command.CommandText = sql;

            var reader = command.ExecuteReader();

            var tt = "";
            List<MyClass> results = new List<MyClass>();

            while (reader.Read())
            {
                MyClass newItem = new MyClass();

                newItem.TypeName = reader.GetString(0);

                results.Add(newItem);
            }

            foreach (var result in results)
            {
                MessageBox.Show(result.TypeName.ToString());
            }



        }
    }
}
