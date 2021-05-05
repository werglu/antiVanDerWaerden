using System;
using System.Globalization;

namespace AntiVanDerWaerden
{
    public static class Program
    {
        private static void Main(string[] args)
        {
         
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
                Console.WriteLine("Wybierz strategie");
                Console.WriteLine("1)");
                Console.WriteLine("2)");
                var strategyReadLine = Console.ReadLine();
                Console.WriteLine("--------START--------");
                for (var i = 1; i <= n; i++)
                    Console.Write(i.ToString() + ' ');
                Console.WriteLine();
                IStrategy strategy;
                if(strategyReadLine == "1")
                    strategy = new Strategy1(n, k, c, true);
                else if (strategyReadLine == "2")
                    strategy = new Strategy2(n, k, c, true);
                else
                {
                    Console.WriteLine("Źle wybrana strategia");
                    return;
                }
                strategy.Play();
                Console.WriteLine("--------END--------");

            }
        }
    }
}
