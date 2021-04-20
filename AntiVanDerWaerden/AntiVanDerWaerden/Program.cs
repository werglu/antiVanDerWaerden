using System;

namespace AntiVanDerWaerden
{
    class Program
    {
        static void Main(string[] args)
        {
            IStrategy strategy;
            //Console.ForegroundColor = ConsoleColor.Red;
            //Console.ForegroundColor = (ConsoleColor)0;
            Console.WriteLine("Anty-van der Waerden");
            Console.WriteLine();
            Console.WriteLine("Podaj dane wejściowe: n k c");
            var data = Console.ReadLine();
            var splittedData = data.Split(' ');
            Console.WriteLine("Wybierz tryb gry:");
            Console.WriteLine("1) demo");
            Console.WriteLine("2) testowy");
            var tryb = Console.ReadLine();
            if (tryb == "1" || tryb == "demo" || tryb == "1)")
            {
                Console.WriteLine("--------START--------");
                for (int i = 1; i <= int.Parse(splittedData[0]); i++)
                {
                    Console.Write(i.ToString() + ' ');
                }
                Console.WriteLine();

                strategy = new Strategy1(int.Parse(splittedData[0]), int.Parse(splittedData[1]), int.Parse(splittedData[2]), true);
                strategy.Play();
                Console.WriteLine("--------END--------");

            }
        }
    }
}
