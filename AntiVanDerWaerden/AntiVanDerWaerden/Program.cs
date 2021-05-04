using System;

namespace AntiVanDerWaerden
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            //Console.ForegroundColor = ConsoleColor.Red;
            //Console.ForegroundColor = (ConsoleColor)0;
            
            Console.WriteLine("Anty-van der Waerden\n");

            int n;
            int k;
            int c;

            while (true)
            {
                Console.WriteLine("Podaj dane wejściowe: n k c");
                
                var data = Console.ReadLine();
                var splittedData = data.Split(' ');
                if (splittedData.Length != 3)
                {
                    Console.WriteLine("Nieprawidłowa liczba argumentów.");
                    continue;
                }

                if (!int.TryParse(splittedData[0], out n) || n < 1)
                {
                    Console.WriteLine("Nieprawidłowa wartość parametru n\n");
                    continue;
                }
                if (!int.TryParse(splittedData[1], out k) || k < 1)
                {
                    Console.WriteLine("Nieprawidłowa wartość parametru k\n");
                    continue;
                }
                if (!int.TryParse(splittedData[2], out c) || c < 1)
                {
                    Console.WriteLine("Nieprawidłowa wartość parametru c\n");
                    continue;
                }

                break;
            }
            
            Console.WriteLine("Wybierz tryb gry:");
            Console.WriteLine("1) demo");
            Console.WriteLine("2) testowy");
            
            var mode = Console.ReadLine();
            if (mode == "1" || mode == "demo" || mode == "1)")
            {
                Console.WriteLine("--------START--------");
                for (var i = 1; i <= n; i++)
                    Console.Write(i.ToString() + ' ');
                Console.WriteLine();

                var strategy = new Strategy1(n, k, c, true);
                strategy.Play();
                Console.WriteLine("--------END--------");

            }
        }
    }
}
