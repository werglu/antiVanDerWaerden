using System;
using System.Collections.Generic;
using System.Linq;

namespace AntyVanDerWaerdenApp
{
    public class Game
    {
        private readonly int c;
        private readonly int k;
        private readonly int n;

        private readonly Strategies.IStrategy player1Strategy;
        private readonly Strategies.IStrategy player2Strategy;

        private  int[] numbers;
        private  List<int[]> subsequences;
        private  List<int[]> T;

        private enum MakeMoveResult
        {
            NoOneWon,
            Player1Won,
            Player2Won
        };

        public Game(int n, int k, int c, Strategies.IStrategy player1Strategy, Strategies.IStrategy player2Strategy)
        {
            this.n = n;
            this.k = k;
            this.c = c;

            this.player1Strategy = player1Strategy;
            this.player2Strategy = player2Strategy;

            numbers = new int[n];
            subsequences = Toolbox.GetAllSubsequences(n, k);

            T = new List<int[]>();
            for (var i = 0; i < subsequences.Count; i++)
                T.Add(new int[c + 1]);
        }

        public void PlayDemo()
        {
            DisplayState();
            while (true)
            {
                Console.Write("Ruch gracza 1. Naciśnij dowolny klawisz aby kontynuować...");
                Console.ReadKey(true);
                ConsoleExtension.ClearLine();
                if (MakeMove(player1Strategy, 1, player2Strategy, true) != MakeMoveResult.NoOneWon)
                    return;
                
                Console.Write("Ruch gracza 2. Naciśnij dowolny klawisz aby kontynuować...");
                Console.ReadKey(true);
                ConsoleExtension.ClearLine();
                if (MakeMove(player2Strategy, 2, player1Strategy, true) != MakeMoveResult.NoOneWon)
                    return;
            }
        }

        public void PlayTest(int testCount)
        {
            var player1WinCount = 0;
            var player2WinCount = 0;
            
            for (var i = 0; i < testCount; i++)
            {
                var broke = false;
                while (true)
                {
                    switch (MakeMove(player1Strategy, 1, player2Strategy, false))
                    {
                        case MakeMoveResult.Player1Won:
                            player1WinCount++;
                            broke = true;
                            break;
                        case MakeMoveResult.Player2Won:
                            player2WinCount++;
                            broke = true;
                            break;
                    }
                    if (broke)
                        break;

                    switch (MakeMove(player2Strategy, 2, player1Strategy, false))
                    {
                        case MakeMoveResult.Player1Won:
                            player1WinCount++;
                            broke = true;
                            break;
                        case MakeMoveResult.Player2Won:
                            player2WinCount++;
                            broke = true;
                            break;
                    }
                    if (broke)
                        break;
                }
                
                Reset();
                player1Strategy.Reset();
                player2Strategy.Reset();
            }

            Console.WriteLine($"Wykonano {testCount} gier.");
            Console.WriteLine($"Gracz 1 wygrał {player1WinCount} razy ({(double)player1WinCount / testCount * 100}% wszystkich gier).");
            Console.WriteLine($"Gracz 2 wygrał {player2WinCount} razy ({(double)player2WinCount / testCount * 100}% wszystkich gier).");
        }

        private void Reset()
        {
            numbers = new int[n];
            subsequences = Toolbox.GetAllSubsequences(n, k);

            T = new List<int[]>();
            for (var i = 0; i < subsequences.Count; i++)
                T.Add(new int[c + 1]);
        }
        
        private MakeMoveResult MakeMove(Strategies.IStrategy playingStrategy, int playingPlayer, Strategies.IStrategy notPlayingStrategy, bool demo)
        {
            var (number, color) = playingStrategy.MakeMove(numbers);
            if (numbers[number] != 0)
                throw new InvalidOperationException($"Tried to recolor number {number} from color {numbers[number]} to color {color}.");
            
            notPlayingStrategy.Update(number, color);
                
            Update(number, color);
            
            if (demo)
            {
                DisplayMove(playingPlayer, number, color);
                DisplayState();
            }
                
            if (Player1Won())
            {
                if (demo)
                {
                    Console.Write("Zwyciężył Gracz1! Znaleziono tęczowy podciąg ");

                    var subsequence = subsequences[T.Select((x, index) => (x, index)).First(x => x.x[c] == k).index];
                    foreach (var element in subsequence)
                    {
                        Console.ForegroundColor = (ConsoleColor)numbers[element - 1];
                        Console.Write($"{element} ");
                    }
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine();
                }

                return MakeMoveResult.Player1Won;
            }
            if (Player2Won())
            {
                if (demo)
                    Console.Write("Zwyciężył Gracz2! Nie znaleziono tęczowego podciagu.");
                
                return MakeMoveResult.Player2Won;
            }

            return MakeMoveResult.NoOneWon;
        }

        private void Update(int number, int color)
        {
            numbers[number] = color;
            for (var i = 0; i < subsequences.Count; i++)
            {
                if (!subsequences[i].Contains(number + 1))
                    continue;
                
                if (T[i][color - 1] == 1)
                {
                    T.RemoveAt(i);
                    subsequences.RemoveAt(i);
                    i--;
                }
                else
                {
                    T[i][color - 1] = 1;
                    T[i][c]++;
                }
            }
        }

        private void DisplayMove(int player, int number, int color)
        {
            Console.Write($"Gracz {player} wybiera liczbę {number + 1} i kolor ");
            Console.ForegroundColor = (ConsoleColor)color;
            Console.Write($"{color}\n");
            Console.ForegroundColor = ConsoleColor.White;
        }

        private void DisplayState()
        {
            for (var i = 0; i < n; i++)
            {
                if (numbers[i] == 0)
                    Console.Write($"{i + 1} ");
                else
                {
                    Console.ForegroundColor = (ConsoleColor)numbers[i];
                    Console.Write($"{i + 1} ");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }

            Console.WriteLine();
        }

        private bool Player1Won() => T.Any(x => x[c] == k);
        private bool Player2Won() => T.Count == 0;
    }
}
