using System;
using System.Collections.Generic;
using System.Linq;

namespace AntyVanDerWaerdenApp.Strategies.Strategy1
{
    public abstract class Strategy1Base : IStrategy
    {
        private readonly int n;
        private readonly int k;
        private readonly int c;
        
        protected Random Random { get; } = new Random();
        protected List<int[]> Subsequences { get; private set; }
        protected List<int[]> T { get; } = new List<int[]>();
        
        protected Strategy1Base(int n, int k, int c)
        {
            this.n = n;
            this.k = k;
            this.c = c;
            
            Subsequences = Toolbox.GetAllSubsequences(n, k);
            for (var i = 0; i < Subsequences.Count; i++)
                T.Add(new int[c + 1]);
        }

        public virtual void Reset()
        {
            Subsequences = Toolbox.GetAllSubsequences(n, k);
            for (var i = 0; i < Subsequences.Count; i++)
                T.Add(new int[c + 1]);
        }

        public abstract (int number, int color) MakeMove(IReadOnlyList<int> numbers);

        public void Update(int number, int color)
        {
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
    }
}
