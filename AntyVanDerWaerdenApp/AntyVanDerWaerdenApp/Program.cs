using AntyVanDerWaerdenApp.Strategies;
using AntyVanDerWaerdenApp.Strategies.Strategy1;
using AntyVanDerWaerdenApp.Strategies.Strategy2;
using System;

namespace AntyVanDerWaerdenApp
{
    class Program
    {
        private enum Mode
        {
            Demo,
            Test
        }
        
        public static void Main(string[] args)
        {
            Console.WriteLine("Anty-van der Waerden\n");

            int n;
            int k;
            int c;

            while (true)
            {
                Console.WriteLine("Podaj dane wejściowe: n k c");

                var data = Console.ReadLine();
                var splittedData = data?.Split(' ');
                if (splittedData?.Length != 3)
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
            
            IStrategy player1Strategy;
            IStrategy player2Strategy;
            
            // choosing player 1 strategy
            while (true)
            {
                Console.WriteLine("Wybierz strategię gracza 1:");
                Console.WriteLine("1)");
                Console.WriteLine("2)");

                var strategyReadLine = Console.ReadLine();
                switch (strategyReadLine)
                {
                    // first strategy
                    case "1":
                    case "1)":
                        player1Strategy = new Strategy1Player1(n, k, c);
                        break;
                    
                    // second strategy
                    case "2":
                    case "2)":
                        player1Strategy = new Strategy2Player1(n, k, c);
                        break;
                    
                    // invalid input
                    default:
                        Console.WriteLine("Źle wybrana strategia");
                        continue;
                }

                break;
            }
            
            // choosing player 2 strategy
            while(true)
            {
                Console.WriteLine("Wybierz strategię gracza 2:");
                Console.WriteLine("1)");
                Console.WriteLine("2)");

                var strategyReadLine = Console.ReadLine();
                switch (strategyReadLine)
                {
                    // first strategy
                    case "1":
                    case "1)":
                        player2Strategy = new Strategy1Player2(n, k, c);
                        break;
                    
                    // second strategy
                    case "2":
                    case "2)":
                        player2Strategy = new Strategy2Player2(n, k, c);
                        break;
                    
                    // invalid input
                    default:
                        Console.WriteLine("Źle wybrana strategia");
                        continue;
                }

                break;
            }

            // choose mode
            Mode mode;
            while (true)
            {
                Console.WriteLine("Wybierz tryb gry:");
                Console.WriteLine("1) demo");
                Console.WriteLine("2) testowy");
                
                var modeReadLine = Console.ReadLine();
                switch (modeReadLine)
                {
                    // first mode (demo)
                    case "1":
                    case "demo":
                    case "1)":
                        mode = Mode.Demo;
                        break;
                    
                    // second mode (test)
                    case "2":
                    case "test":
                    case "testowy":
                    case "2)":
                        mode = Mode.Test;
                        break;
                    
                    // invalid input
                    default:
                        Console.WriteLine("Źle wybrany tryb");
                        continue;
                }

                break;
            }

            var game = new Game(n, k, c, player1Strategy, player2Strategy);
            switch (mode)
            {
                case Mode.Demo:
                    game.PlayDemo();
                    break;
                case Mode.Test:
                    int testCount;
                    while (true)
                    {
                        Console.WriteLine("Podaj liczbę testów:");
                        var line = Console.ReadLine();
                        if (!int.TryParse(line, out testCount) || testCount <= 0)
                        {
                            Console.WriteLine("Nieprawidłowa liczba testów");
                            continue;
                        }

                        break;
                    }
                    game.PlayTest(testCount);
                    break;
            }
        }
    }
}
