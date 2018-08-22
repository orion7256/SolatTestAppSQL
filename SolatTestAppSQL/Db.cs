using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolatTestAppSQL
{
    static class Db
    {
        private static MySqlDataAdapter da = new MySqlDataAdapter();
        private static DataTable dt = new DataTable();
        public static void Add()
        {
            Console.WriteLine("Record adding: " + DateTime.Today.ToShortDateString() + "\n");
            string desc = Console.ReadLine();
            if (desc != "")
            {
                da.SelectCommand = new MySqlCommand(@"SELECT MAX(idtasks) FROM solar.tasks;", Sql_Connector.con);
                dt.Clear();
                da.Fill(dt);
                int i = (Int32.Parse(dt.Rows[0].ItemArray[3].ToString()));
                i++;
                string com = "insert into solar.tasks values(" + i + ", '" + string.Format("{0:yyyy-MM-dd}", DateTime.Today) + "', '" + desc +  "' );";
                Console.WriteLine(com);
                MySqlCommand comand = new MySqlCommand(com, Sql_Connector.con);
                comand.ExecuteNonQuery();
                Console.ReadKey();
            }
        }
        public static void Edit()
        {
            View(false);
            Console.Write("Enter record ID number to edit:");
            int num;
            Int32.TryParse(Console.ReadLine(), out num);

            Console.WriteLine("New description: ");
            string desc = Console.ReadLine();
            if (desc != "")
            {
                string com = @"UPDATE solar.tasks SET `description`= '" + desc + "' WHERE `idtasks` = " + num + ";";
                Console.WriteLine(com);
                MySqlCommand comand = new MySqlCommand(com, Sql_Connector.con);
                comand.ExecuteNonQuery();
                Console.ReadKey();
            }
        }
        public static void Remove()
        {
            View(false);
            Console.WriteLine("Enter record ID number to remove:\n");
            int num;
            Int32.TryParse(Console.ReadLine(), out num);
            string com = "DELETE FROM solar.tasks WHERE `idtasks`=" + num + ";";
            Console.WriteLine(com);
            MySqlCommand comand = new MySqlCommand(com, Sql_Connector.con);
            comand.ExecuteNonQuery();
            Console.ReadKey();
        }
        public static void View(bool wait_flag = true)
        {
            string com = "";
            com = "SELECT * FROM solar.tasks;";
            da.SelectCommand = new MySqlCommand(com, Sql_Connector.con);
            dt.Clear();
            da.Fill(dt);
            Show_top(com);
            for (int i = 0; i< dt.Rows.Count; i++)
                Console.WriteLine(dt.Rows[i].ItemArray[0].ToString() + "  " + dt.Rows[i].ItemArray[1].ToString() + "  " + dt.Rows[i].ItemArray[2].ToString());
            if (wait_flag)
                Console.ReadKey();
        }
        private static void Show_top(string com = "")
        {
            string ret = "";
            ret += "[ " + com + "]\n";
            ret += "|ID|Date               |Description\n";
            ret += "=======================================\n";
            Console.WriteLine(ret);
        }
    }
}
