using System.Collections.Generic;
using System.Linq;

namespace AntyVanDerWaerdenApp.Strategies.Strategy2
{
    public class Strategy2Player1 : Strategy2Base
    {
        private readonly int k;
        private readonly int c;

        public Strategy2Player1(int n, int k, int c) : base(n, k, c)
        {
            this.k = k;
            this.c = c;
        }

        public override (int number, int color) MakeMove(IReadOnlyList<int> numbers)
        {
            UpdateAArray(numbers);
            
            var number = GetNumberToColor();
            var color = GetLeastUsedColor(numbers);
            
            Update(number, color);
            return (number, color);
        }

        private int GetLeastUsedColor(IReadOnlyList<int> numbers)
        {
            var colors = new int[c + 1];
            colors[0] = int.MaxValue;

            var maxSubsequence = FindMaxSubsequence();
            for (var i = 0; i < Subsequences.Count; i++)
                 if (T[i][k] == maxSubsequence)
                     foreach (var element in Subsequences[i])
                         if (numbers[element - 1] != 0)
                             colors[numbers[element - 1]]++;

            var min = colors.Min();
            var indexes = colors.Select((x, index) => (x, index)).Where(x => x.x == min).Select(x => x.index).ToList();

            return indexes[Random.Next(0, indexes.Count)];
        }
    }
}
