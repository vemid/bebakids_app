namespace BebaKids
{


    class Konekcija
    {
        public static string konString = "Dsn=bebakids;uid=sa;Pwd=adminabc123;";
    }

    class SqlConekcija
    {
        public static string sqlKonekcija = Properties.Settings.Default.ConnectionString;
    }

    class MysqlKonekcija
    {
        public static string myConnectionString = "server=192.168.100.11;database=interno;uid=root;pwd=710412;";
    }

    class MysqlB2B
    {
        public static string myConnectionString = "server=192.168.100.11;database=b2b;uid=root;pwd=710412;";
    }
}
