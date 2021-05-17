using System;
using System.Collections.Generic;
using System.Linq;

namespace AntyVanDerWaerdenApp.Strategies.Strategy2
{
    public abstract class Strategy2Base : IStrategy
    {
        private readonly int n;
        private readonly int k;
        
        protected Random Random { get; } = new Random();
        protected List<int[]> Subsequences { get; }
        protected List<int[]> T { get; }
        protected int[] A { get; private set; }
        
        protected Strategy2Base(int n, int k, int c)
        {
            this.n = n;
            this.k = k;
        }
        
        public abstract (int number, int color) MakeMove(IReadOnlyList<int> numbers);

        public void Update(int number, int color)
        {
            for (var i = 0; i < Subsequences.Count; i++)
            {
                for (var j = 0; j < Subsequences[i].Length; j++)
                {
                    if (Subsequences[i][j] == number)
                    {
                        if (T[i].Contains(color))
                        {
                            T.RemoveAt(i);
                            Subsequences.RemoveAt(i);
                            i--;
                        }
                        else
                        {
                            T[i][k]++;
                            T[i][j] = color;
                        }
                    }
                }
            }
        }

        protected void UpdateAArray(IReadOnlyList<int> numbers)
        {
            var max = FindMaxSubsequence();
            A = new int[n];

            for (var i = 0; i < Subsequences.Count; i++)
            {
                if (T[i][k] != max)
                    continue;
                
                for (var j = 0; j < Subsequences[i].Length; j++)
                {
                    if (numbers[Subsequences[i][j] - 1] != 0)
                        A[Subsequences[i][j] - 1] = -1;
                    else
                        A[Subsequences[i][j] - 1]++;
                }
            }
        }

        protected int FindMaxSubsequence() => T.Select(t => t[k]).Max();

        protected int GetNumberToColor()
        {
            var max = A.Max();
            var maxIndexes = A.Select((x, index) => (x, index)).Where(x => x.x == max).Select(x => x.index).ToList();

            return maxIndexes[Random.Next(0, maxIndexes.Count)];
        }
    }
}
