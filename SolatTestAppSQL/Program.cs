using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SolatTestAppSQL
{
    class Program
    {
        static void Main(string[] args)
        {
            Sql_Connector.open();
            Interface I = new Interface();
        }
    }
}
