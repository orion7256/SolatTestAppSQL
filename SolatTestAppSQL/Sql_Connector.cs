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
            status = "";
        }
        public string dbname { get { return conn_string.Database; } }
        public string datasource { get { return conn_string.Server; } }
        public string ID { get { return conn_string.UserID; } }
        public string pas { get { return conn_string.Password; } }
        public string status { get; set; }
    }
    public static class Sql_Connector
    {
        public static bool isopen = false;
        public static server srv = new server();
        public static MySqlConnection con = new MySqlConnection(srv.conn_string.ToString());

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
                        srv.status = ex.ToString();
                        srv.status += "Cannot connect to server.  Contact administrator";
                        break;
                    case 1045:
                        srv.status = "Invalid username/password, please try again";
                        break;
                    default: srv.status = ex.ToString(); break;
                }
            }
            if (isopen)
                srv.status = ("DB: " + srv.dbname + " Host: " + srv.datasource);
        }

        public static void exit()
        {
            con.Close();
        }
    }
}
