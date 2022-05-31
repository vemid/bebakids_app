using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirebirdSql.Data.FirebirdClient;

namespace BebaKids.Backend
{
    class FBConection
    {
        FbConnection fbConnection;
        public void CreateConnection()
        {
            FbConnectionStringBuilder builder = new FbConnectionStringBuilder
            {
                Charset = "WIN1252",
                Database = @"C:\MisPOS\Baza\data.gdb",
                Dialect = 3,
                Role = "READ_ONLY_OTN",
                UserID = "SYSDBA",
                Password = "masterkey",
                ServerType = FbServerType.Default
            };
            fbConnection = new FbConnection(builder.ConnectionString);
            fbConnection.Open();
        }
    }
}

