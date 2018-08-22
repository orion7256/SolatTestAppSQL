using MySql.Data.MySqlClient;

namespace SolatTestAppSQL
{
    public class server
    {
        public MySqlConnectionStringBuilder conn_string = new MySqlConnectionStringBuilder();
        public server()
        {
            conn_string.Server = "localhost";
            conn_string.UserID = "root";
            conn_string.Password = "root";
            conn_string.Database = "solar";
            conn_string.Port = 3306;
            //conn_string.

            err = "";
        }
        public string dbname { get { return conn_string.Database; } }
        public string datasource { get { return conn_string.Server; } }
        public string ID { get { return conn_string.UserID; } }
        public string pas { get { return conn_string.Password; } }
        public string err { get; set; }
    }
    public static class Sql
    {
        public static bool isopen = false;
        public static server s = new server();
        public static MySqlConnection con = new MySqlConnection(s.conn_string.ToString());

        public static void open()
        {
            try
            {
                con.Open();
                isopen = true;
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        s.err = ex.ToString();
                        s.err += "Cannot connect to server.  Contact administrator";
                        break;
                    case 1045:
                        s.err = "Invalid username/password, please try again";
                        break;
                    default: s.err = ex.ToString(); break;
                }
            }
            if (isopen)
                s.err = ("DB: " + s.dbname + " Host: " + s.datasource);
        }

        public static void exit()
        {
            con.Close();
        }
    }
}
