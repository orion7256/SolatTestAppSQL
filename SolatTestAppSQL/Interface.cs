using System;
using System.Threading;

namespace SolatTestAppSQL
{
    class Interface
    {
        public string source = "";
        public Interface()
        {
            Console.Clear();
            Console.Title = "Tasklist SQL Korotkov A.A.";
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            main_menu();
        }
        private void main_menu()
        {
            int Item = 0;
            string[] mark = new string[5];
            for (int f = 0; f < 5; f++) mark[f] = "  ";//init

            while (true)
            {
                Console.Clear();
                mark[Item] = "[>";
                Console.WriteLine(        "+-+-----------------------------+");
                Console.WriteLine(        "| |            Меню             |");
                Console.WriteLine(        "+-+-----------------------------+");
                Console.WriteLine(        "| | " + Sql_Connector.srv.status +         "   |");
                Console.WriteLine(        "+-+-----------------------------+");
                Console.WriteLine(mark[0] + "|  1-Просмотр записей         |");
                Console.WriteLine(mark[1] + "|  2-Добавление записи        |");
                Console.WriteLine(mark[2] + "|  3-Удаление записи          |");
                Console.WriteLine(mark[3] + "|  4-Редактирование записи    |");
                Console.WriteLine(mark[4] + "|  0-Выход (Esc)              |");
                Console.WriteLine("+-+-----------------------------+");

                ConsoleKeyInfo ReadKey = Console.ReadKey(true);
                switch (ReadKey.Key)
                {
                    case ConsoleKey.DownArrow:
                        {
                            mark[Item] = "  ";
                            if (Item < 4) Item++;
                            else Item = 0;
                            break;
                        }
                    case ConsoleKey.UpArrow:
                        {
                            mark[Item] = "  ";
                            if (Item > 0) Item--;
                            else Item = 4;
                            break;
                        }
                    case ConsoleKey.Enter:
                        {
                            Console.WriteLine("Doing " + Item);
                            DoTask(Item);

                            break;
                        }
                    case ConsoleKey.Escape:
                        {
                            Console.WriteLine("Выход");
                            Sql_Connector.exit();
                            Thread.Sleep(500);
                            Environment.Exit(0);
                            break;
                        }//esc
                    default: break;
                }
            }
        }
        public void DoTask(int task_num = -1)
        {
            if (task_num != -1)
                switch (task_num)
                {
                    case 0: Db.View(); break;
                    case 1: Db.Add(); break;
                    case 2: Db.Remove(); break;
                    case 3: Db.Edit(); break;
                    case 4: Sql_Connector.exit(); Environment.Exit(0); break;
                    default: break;
                }

        }
    }
}