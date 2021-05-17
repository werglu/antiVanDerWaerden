using System.Collections.Generic;
using System.Linq;

namespace AntyVanDerWaerdenApp.Strategies.Strategy1
{
    public class Strategy1Player2 : Strategy1Base
    {
        private readonly int k;
        private readonly int c;

        public Strategy1Player2(int n, int k, int c) : base(n, k, c)
        {
            this.k = k;
            this.c = c;
        }

        public override (int number, int color) MakeMove(IReadOnlyList<int> numbers)
        {
            var maxColoredCount = T.Select(x => x[c]).Max();
            var maxColoredCountSubsequenceIndex = T.Select((x, index) => (x, index)).First(x => x.x[c] == maxColoredCount).index;
            
            var (number, color) = GetMiddleElement(numbers, maxColoredCountSubsequenceIndex);
            
            Update(number, color);
            return (number, color);
        }

        private (int number, int color) GetMiddleElement(IReadOnlyList<int> numbers, int subsequenceIndex)
        {
            var middleIndex = k / 2;
            var color = GetRandomColor(subsequenceIndex);
            var subsequence = Subsequences[subsequenceIndex];

            if (numbers[subsequence[middleIndex] - 1] == 0)
                return (numbers[subsequence[middleIndex] - 1], color);

            for (var i = 1; i < k - k / 2; i++)
            {
                if (middleIndex - i >= 0 && numbers[subsequence[middleIndex - i] - 1] == 0)
                    return (numbers[subsequence[middleIndex - i] - 1], color);
                
                if (middleIndex + i >= 0 && numbers[subsequence[middleIndex - i] + 1] == 0)
                    return (numbers[subsequence[middleIndex + i] - 1], color);
            }
            
            // all numbers colored
            return (-1, -1);
        }

        /// <summary>
        /// Returns random color used in a given subsequence or 1 if no color used.
        /// </summary>
        /// <param name="subsequenceIndex">Index of a subsequence in which used colors will be searched.</param>
        private int GetRandomColor(int subsequenceIndex)
        {
            var usedColors = T[subsequenceIndex].Where(x => x > 0).Select(x => x + 1).ToList();
            return usedColors.Count == 0 ? 1 : usedColors[Random.Next(0, usedColors.Count)];
        }
    }
}
