using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Windows;

namespace _2025_03_27
{
    class Program
    {
        static string name;
        static double grade;
        static int price;
        static ServerConnection connection; //érték kell
        static void Main(string[] args)
        {
            start();
        }
        async static void start()
        {
            

            Console.WriteLine("Mit szeretnél csinálni? - Vásárolni(V) | Nézelődni(N) | Törölni(T)");
            string answer = Console.ReadLine();

            if(answer == "V")
            {
                Console.WriteLine("Neve: ");
                string answerName = Console.ReadLine();
                name = answerName;
                if(answerName != null)
                {
                    Console.WriteLine("Értékelése: ");
                    string answerGrade = Console.ReadLine();
                    grade = Convert.ToDouble(answerGrade);
                    if(answerGrade != null)
                    {
                        Console.WriteLine("Ára: ");
                        string anserPrice = Console.ReadLine();
                        price = Convert.ToInt32(anserPrice);

                        bool result = await connection.createKolbi(name, (float)grade, Convert.ToInt32(price));
                        if(result)
                        {
                            Console.WriteLine(result);
                        }
                    }
                }
                else if(answer == "N")
                {

                }
                else if (answer == "T")
                {

                }
            }

            Console.ReadKey();
        }
    }
}
