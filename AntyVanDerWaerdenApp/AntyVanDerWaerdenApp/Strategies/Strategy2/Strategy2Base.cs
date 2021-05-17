using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AntyVanDerWaerdenApp.Strategies.Strategy2
{
    public abstract class Strategy2Base : IStrategy
    {
        private readonly int n;
        private readonly int k;
        private readonly int c;
        
        protected Random Random { get; } = new Random();
        protected List<int[]> Subsequences { get; }
        protected List<int[]> T { get; } = new List<int[]>();
        protected int[] A { get; private set; }
        
        protected Strategy2Base(int n, int k, int c)
        {
            this.n = n;
            this.k = k;
            this.c = c;
            
            Subsequences = Toolbox.GetAllSubsequences(n, k);
            for (var i = 0; i < Subsequences.Count; i++)
                // T.Add(new int[k + 1]);
                T.Add(new int[c + 1]);
        }
        
        public abstract (int number, int color) MakeMove(IReadOnlyList<int> numbers);

        public void Update(int number, int color)
        {
            // previous way, fixed
            // for (var i = 0; i < Subsequences.Count; i++)
            // {
            //     for (var j = 0; j < Subsequences[i].Length; j++)
            //     {
            //         if (Subsequences[i][j] == number)
            //         {
            //             if (T[i].Reverse().Skip(1).Contains(color))
            //             {
            //                 T.RemoveAt(i);
            //                 Subsequences.RemoveAt(i);
            //                 i--;
            //                 break;
            //             }
            //             
            //             T[i][k]++;
            //             T[i][j] = color;
            //         }
            //     }
            // }

            for (var i = 0; i < Subsequences.Count; i++)
            {
                if (!Subsequences[i].Contains(number + 1))
                    continue;
            
                if (T[i][color - 1] == 1)
                {
                    T.RemoveAt(i);
                    Subsequences.RemoveAt(i);
                    i--;
                }
                else
                {
                    T[i][color - 1] = 1;
                    T[i][c]++;
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
            
            for (var i = 0; i < A.Length; i++)
                if (numbers[i] != 0)
                    A[i] = -1;
        }

        protected int FindMaxSubsequence() => T.Select(t => t[k]).Max();

        protected int GetNumberToColor()
        {
            var max = A.Max();
            var maxIndexes = A.Select((x, index) => (x, index)).Where(x => x.x == max).Select(x => x.index).ToList();
            var maxIndex = Random.Next(0, maxIndexes.Count);

            return maxIndexes[maxIndex];
        }
    }
}
