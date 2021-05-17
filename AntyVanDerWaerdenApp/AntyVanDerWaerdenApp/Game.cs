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

        private readonly int[] numbers;
        private readonly List<int[]> subsequences;
        private readonly List<int[]> T;

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
                if (MakeMove(player1Strategy, 1, player2Strategy))
                    return;
                
                Console.Write("Ruch gracza 2. Naciśnij dowolny klawisz aby kontynuować...");
                Console.ReadKey(true);
                ConsoleExtension.ClearLine();
                if (MakeMove(player2Strategy, 2, player1Strategy))
                    return;
            }
        }

        public void PlayTest()
        {
            throw new NotImplementedException();
        }
        
        /// <summary>
        /// Returns boolean value indicating whether game has ended.
        /// </summary>
        private bool MakeMove(Strategies.IStrategy playingStrategy, int playingPlayer, Strategies.IStrategy notPlayingStrategy)
        {
            var (number, color) = playingStrategy.MakeMove(numbers);
            if (numbers[number] != 0)
                throw new InvalidOperationException($"Tried to recolor number {number} from color {numbers[number]} to color {color}.");
            
            notPlayingStrategy.Update(number, color);
                
            Update(number, color);
            DisplayMove(playingPlayer, number, color);
            DisplayState();
                
            if (Player1Won())
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
                return true;
            }
            if (Player2Won())
            {
                Console.Write("Zwyciężył Gracz2! Nie znaleziono tęczowego podciagu.");
                return true;
            }

            return false;
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
