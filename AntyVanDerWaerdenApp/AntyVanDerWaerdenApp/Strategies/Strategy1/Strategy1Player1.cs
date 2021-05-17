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
                (number, color) = GetMiddleElement(numbers, GetBestSubsequence());
            
            Update(number, color);
            return (number, color);
        }

        public override void Reset()
        {
            ResetBase();
            firstMove = true;
        }

        private (int number, int color) MakeFirstMove(IReadOnlyCollection<int> numbers) => ((int)Math.Ceiling(numbers.Count / 2.0), 1);

        private int[] GetBestSubsequence()
        {
            var maxColoredCount = T.Select(x => x[c]).Max();
            var bestIndexes = T.Select((x, index) => (x, index)).Where(x => x.x[c] == maxColoredCount).Select(x => x.index).ToList();
            var bestIndex = Random.Next(0, bestIndexes.Count);

            return Subsequences[bestIndexes[bestIndex]];
        }

        private (int number, int color) GetMiddleElement(IReadOnlyList<int> numbers, IReadOnlyList<int> subsequence)
        {
            var middleIndex = k / 2;
            var color = GetUnusedColor(numbers, subsequence);

            if (numbers[subsequence[middleIndex] - 1] == 0)
                return (subsequence[middleIndex] - 1, color);

            for (var i = 1; i <= k - k / 2; i++)
            {
                if (middleIndex - i >= 0 && numbers[subsequence[middleIndex - i] - 1] == 0)
                    return (subsequence[middleIndex - i] - 1, color);
                
                if (middleIndex + i < numbers.Count && numbers[subsequence[middleIndex + i] - 1] == 0)
                    return (subsequence[middleIndex + i] - 1, color);
            }
            
            // all numbers colored
            return (-1, -1);
        }

        private int GetUnusedColor(IReadOnlyList<int> numbers, IReadOnlyList<int> subsequence)
        {
            var isUsed = new bool[c];
            foreach (var number in subsequence)
                if (numbers[number - 1] > 0)
                    isUsed[numbers[number - 1] - 1] = true;

            for (var i = 0; i < c; i++)
                if (!isUsed[i])
                    return i + 1;
            
            // all colors used
            return -1;
        }
    }
}
