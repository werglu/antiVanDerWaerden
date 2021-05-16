using System;
using System.Collections.Generic;
using System.Linq;

namespace AntyVanDerWaerdenApp.Strategies.Strategy1
{
    public class Player1Strategy1 : PlayerStrategy1Base
    {
        public Player1Strategy1(int n, int k, int c, bool demo) : base(n, k, c, demo)
        { }

        public override void MakeFirstMove()
        {
            var number = (int)Math.Ceiling(N / 2.0);
            ColorNumber(number, 1);
            
            DisplayMove(1, number, 1);
        }

        public override void MakeMove()
        {
            ChooseFringeElement(ChooseBestSubsequenceIndex());
        }

        /// <summary>
        /// Jeśli jest kilka podciągów z największą liczbą pokolorowanych elementów, losowo wybieramy jeden.
        /// </summary>
        private int ChooseBestSubsequenceIndex()
        {
            var bestIndexes = new List<int>();
            var maxElements = 0;

            for (var i = 0; i < T.GetLength(0); i++)
                if (T[i, C] > maxElements)
                    maxElements = T[i, C];

            for (var i = 0; i < T.GetLength(0); i++)
                if (T[i, C] == maxElements)
                    bestIndexes.Add(i);

            var maxIndex = Random.Next(0, bestIndexes.Count);
            return bestIndexes.ElementAt(maxIndex);
        }
        
        private void ChooseFringeElement(int elementIndex)
        {
            var fringeIndex = K - 1;
            var i = 0;
            while (true)
            {
                var color = ChooseNotUsedColor();
                if (i < K && Numbers[Subsequences.ElementAt(elementIndex)[i] - 1] == 0)
                {
                    ColorNumber(Subsequences.ElementAt(elementIndex)[i], color);
                    DisplayMove(1, Subsequences.ElementAt(elementIndex)[i], color);
                    return;
                }
                if (fringeIndex - i >= 0 && Numbers[Subsequences.ElementAt(elementIndex)[fringeIndex - i] - 1] == 0)
                {
                    ColorNumber(Subsequences.ElementAt(elementIndex)[fringeIndex - i], color);
                    DisplayMove(1, Subsequences.ElementAt(elementIndex)[fringeIndex - i], color);
                    return;
                }
                i++;
            }
        }

        private int ChooseNotUsedColor()
        {
            return ++MaxColor;
        }
    }
}
