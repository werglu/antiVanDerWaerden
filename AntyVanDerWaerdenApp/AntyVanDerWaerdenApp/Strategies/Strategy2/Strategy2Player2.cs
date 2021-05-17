using System.Collections.Generic;
using System.Linq;

namespace AntyVanDerWaerdenApp.Strategies.Strategy2
{
    public class Strategy2Player2 : Strategy2Base
    {
        private readonly int k;
        private readonly int c;

        public Strategy2Player2(int n, int k, int c) : base(n, k, c)
        {
            this.k = k;
            this.c = c;
        }

        public override (int number, int color) MakeMove(IReadOnlyList<int> numbers)
        {
            UpdateAArray(numbers);

            var number = GetNumberToColor();
            var color = GetMostUsedColor(numbers);
            
            Update(number, color);
            return (number, color);
        }

        private int GetMostUsedColor(IReadOnlyList<int> numbers)
        {
            var colors = new int[c + 1];
            colors[0] = int.MinValue;
            
            var maxSubseqence = FindMaxSubsequence();
            for (var i = 0; i < Subsequences.Count; i++)
                if (T[i][k] == maxSubseqence)
                    foreach (var element in Subsequences[i])
                        if (numbers[element - 1] != 0)
                            colors[numbers[element - 1]]++;
            
            var max = colors.Max();
            var indexes = colors.Select((x, index) => (x, index)).Where(x => x.x == max).Select(x => x.index).ToList();
            
            return indexes[Random.Next(indexes.Count)];
        }
    }
}
