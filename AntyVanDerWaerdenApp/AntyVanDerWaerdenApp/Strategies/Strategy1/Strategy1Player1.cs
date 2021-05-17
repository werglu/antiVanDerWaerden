using System;
using System.Collections.Generic;
using System.Linq;

namespace AntyVanDerWaerdenApp.Strategies.Strategy1
{
    public class Strategy1Player1 : Strategy1Base
    {
        private static bool firstMove = true;

        private readonly int k;
        private readonly int c;

        public Strategy1Player1(int n, int k, int c) : base(n, k, c)
        {
            this.k = k;
            this.c = c;
        }

        public override (int number, int color) MakeMove(IReadOnlyList<int> numbers)
        {
            int number;
            int color;
            if (firstMove)
            {
                firstMove = false;
                (number, color) = MakeFirstMove(numbers);
            }
            else
                (number, color) = GetFringeElement(numbers, GetBestSubsequence());
            
            Update(number, color);
            return (number, color);
        }

        private (int number, int color) MakeFirstMove(IReadOnlyCollection<int> numbers) => ((int)Math.Ceiling(numbers.Count / 2.0), 1);

        private int[] GetBestSubsequence()
        {
            var maxColoredCount = T.Select(x => x[c]).Max();
            var bestIndexes = T.Where(x => x[c] == maxColoredCount).ToArray();
            var bestIndex = Random.Next(0, bestIndexes.Length);

            return bestIndexes[bestIndex];
        }

        private (int number, int color) GetFringeElement(IReadOnlyList<int> numbers, IReadOnlyList<int> subsequence)
        {
            var fringeIndex = k - 1;
            var color = GetUnusedColor(numbers);
            if (color == -1)
                return (-1, -1);
            
            for (var i = 0; i < k; i++)
            {
                if (i < k && numbers[subsequence[i] - 1] == 0)
                    return (numbers[subsequence[i] - 1], color);

                if (fringeIndex - i >= 0 && numbers[subsequence[fringeIndex - i] - 1] == 0)
                    return (numbers[subsequence[fringeIndex - i] - 1], color);
            }
            
            // all numbers colored
            return (-1, -1);
        }

        private int GetUnusedColor(IEnumerable<int> numbers)
        {
            var isUsed = new bool[c];
            foreach (var number in numbers)
                if (number > 0)
                    isUsed[number - 1] = true;

            for (var i = 0; i < c; i++)
                if (!isUsed[c])
                    return i;

            // all colors used
            return -1;
        }
    }
}
