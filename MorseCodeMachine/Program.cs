using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace MorseCodeMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Gray;

                string output = "";

                Console.WriteLine("Enter message");
                Console.Write(": ");
                string message = Console.ReadLine().ToLower();

                string[] words = message.Split(' ');

                foreach (string word in words)
                {
                    foreach (char letter in word)
                    {
                        string cs = @"Data Source=.\MorseCode.db";
                        string sql = $"SELECT Code FROM MorseCode WHERE Character == '{letter}'";

                        using (var connection = new SQLiteConnection(cs))
                        {

                            output += $"{connection.Query<string>(sql).FirstOrDefault()}";

                        }

                        output += "    ";
                    }
                    output += "    ";
                }

                Console.ForegroundColor = ConsoleColor.Green;

                Console.WriteLine(output);

                foreach (var item in output)
                {
                    if (item != ' ')
                    {
                        if (item == '.')
                        {
                            Console.Beep(700, 200);
                            Task.Delay(240);
                        }
                        else if (item == '-')
                        {
                            Console.Beep(700, 700);
                            Task.Delay(240);

                        }
                    }
                }

                Console.ReadKey(); 
            }
        }
    }
}
