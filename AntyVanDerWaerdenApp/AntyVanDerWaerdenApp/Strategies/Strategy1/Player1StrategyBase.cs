using System;
using System.Collections.Generic;
using System.Linq;

namespace AntyVanDerWaerdenApp.Strategies.Strategy1
{
    public abstract class PlayerStrategy1Base : PlayerStrategyBase
    {
        public int Finish { get; private set; } = -1;
        
        /// <summary>
        /// Kolorowany ciąg (kolory poszczególnych liczb).
        /// </summary>
        protected readonly int[] Numbers;
        
        /// <summary>
        /// Wszystkie podciągi o długości k.
        /// </summary>
        protected readonly List<int[]> Subsequences;
        
        protected readonly int[,] T;
        protected int MaxColor = -1;
        protected readonly Random Random = new Random();
        
        public PlayerStrategy1Base(int n, int k, int c, bool demo) : base(n, k, c, demo)
        {
            Numbers = new int[n];
            Subsequences = Toolbox.FindAllSubsequences(n, k);
            T = new int[Subsequences.Count, c + 1];
        }

        public abstract override void MakeFirstMove();

        public abstract override void MakeMove();

        protected void ColorNumber(int number, int color)
        {
            Numbers[number - 1] = color;

            var index = 0;
            foreach (var subsequence in Subsequences)
            {
                if (subsequence.Contains(number))
                {
                    if (T[index, color - 1] > 0)
                    {
                        T[index, color - 1] = color;
                        T[index, C] = -1; // nie może być już tęczowy
                    }
                    else
                    {
                        T[index, color - 1] = color;

                        if (T[index, C] > -1)
                            T[index, C]++;

                        if (T[index, C] == K)
                            Finish = index;
                    }
                }

                index++;
            }
        }
    }
}
