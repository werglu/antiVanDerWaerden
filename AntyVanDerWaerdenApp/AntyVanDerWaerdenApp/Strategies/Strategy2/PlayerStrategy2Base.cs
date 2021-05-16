using System;
using System.Collections.Generic;
using System.Linq;

namespace AntyVanDerWaerdenApp.Strategies.Strategy2
{
    public abstract class PlayerStrategy2Base : PlayerStrategyBase
    {
        /// <summary>
        /// Kolorowany ciąg (kolory poszczególnych liczb).
        /// </summary>
        protected readonly int[] Numbers;
        
        /// <summary>
        /// Wszystkie podciągi o długości k.
        /// </summary>
        protected readonly List<int[]> Subsequences;
        
        protected int[] A;
        protected readonly List<int[]> T = new List<int[]>();
        protected readonly Random Random = new Random();

        public PlayerStrategy2Base(int n, int k, int c, bool demo) : base(n, k, c, demo)
        {
            Numbers = new int[n];
            Subsequences = Toolbox.FindAllSubsequences(n, k);
            for (var i = 0; i < Subsequences.Count; i++)
                T.Add(new int[k + 1]);
            A = new int[n];
        }

        public abstract override void MakeFirstMove();

        public abstract override void MakeMove();

        protected int FindMaxSubsequence()
        {
            return T.Max(x => x[K]);
        }

        protected void UpdateAArray()
        {
            A = new int[N];
            var max = FindMaxSubsequence();
            
            for (var i = 0; i < Subsequences.Count; i++)
            {
                if (T[i][K] == max)
                {
                    for (var j = 0; j < Subsequences[i].Length; j++)
                    {
                        if (Numbers[Subsequences[i][j] - 1] != 0)
                            A[Subsequences[i][j] - 1] = -1;
                        else
                            A[Subsequences[i][j] - 1]++;
                    }
                }
            }
        }

        protected int FindNumberToColor()
        {
            var max = A.Max();
            var maxIndexes = new List<int>();
            
            for (var i = 0; i < A.Length; i++)
                if (A[i] == max)
                    maxIndexes.Add(i);
            
            return maxIndexes[Random.Next(maxIndexes.Count)];
        }

        protected void ColorNumber(int num, int color)
        {
            Numbers[num] = color;
            num++;
            for (var i = 0; i < Subsequences.Count; i++)
            {
                for (var j = 0; j < Subsequences[i].Length; j++)
                {
                    if (Subsequences[i][j] == num)
                    {
                        if (T[i].Contains(color))
                        {
                            T.RemoveAt(i);
                            Subsequences.RemoveAt(i);
                            i--;
                            break;
                        }
                        T[i][K]++;
                        T[i][j] = color;
                    }
                }
            }

        }
    }
}
