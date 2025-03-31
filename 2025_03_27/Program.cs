using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
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
        static ServerConnection connection = new ServerConnection();

        static async Task Main(string[] args)
        {
            await start();
        }

        async static Task start()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Mit szeretnél csinálni? - Vásárolni(V) | Nézelődni(N) | Törölni(T)");
            
            string answer = Console.ReadLine().ToUpper();


            if (answer == "V")
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Neve: ");
                string answerNameV = Console.ReadLine();
                if (string.IsNullOrEmpty(answerNameV))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("A név nem lehet üres.");
                    return;
                }
                name = answerNameV;

                Console.WriteLine("Értékelése: ");
                string answerGrade = Console.ReadLine();
                if (!double.TryParse(answerGrade, out grade))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Érvénytelen értékelés. Kérlek, egy számot adj meg.");
                    return;
                }

                Console.WriteLine("Ára: ");
                string answerPrice = Console.ReadLine();
                if (!int.TryParse(answerPrice, out price))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Érvénytelen ár. Kérlek, egy egész számot adj meg.");
                    return;
                }

                bool result = await connection.createKolbi(name, (float)grade, price);
                if (result)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("A Kolbi sikeresen hozzáadva!");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Hiba történt a Kolbi hozzáadásakor.");
                }
            }


            else if (answer == "N")
            {
                List<JsonData> result = await connection.Allkolbi();

                if (result != null && result.Count > 0)
                {
                    foreach (var kolbasz in result)
                    {
                        Console.WriteLine($"Név: {kolbasz.kolbaszName}, Értékelés: {kolbasz.kolbaszGrade}, Ár: {kolbasz.kolbaszPrice}");
                    }
                }
                else
                {
                    Console.WriteLine("Nincsenek elérhető kolbászok.");
                }
            }
            else if (answer == "T")
            {
                Console.WriteLine("Neve: ");
                string answerNameT = Console.ReadLine();
                if (string.IsNullOrEmpty(answerNameT))
                {
                    Console.WriteLine("A név nem lehet üres.");
                    return;
                }
                name = answerNameT;
                bool result = await connection.deleteKolbi(name);
                if (result)
                {
                    Console.WriteLine("A Kolbi sikeresen törölve!");
                }
                else
                {
                    Console.WriteLine("Hiba történt a Kolbi kitörlésénél.");
                }
            }
            else
            {
                Console.WriteLine("Érvénytelen választás.");
            }

            Console.ReadKey();
        }
    }
}
