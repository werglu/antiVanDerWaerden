using System.Collections.Generic;
using System.Linq;

namespace AntyVanDerWaerdenApp.Strategies.Strategy1
{
    public class Player2Strategy1 : PlayerStrategy1Base
    {
        public Player2Strategy1(int n, int k, int c, bool demo) : base(n, k, c, demo)
        { }

        public override void MakeFirstMove() => MakeMove();

        public override void MakeMove()
        {
            var maxElements = 0;
            var maxIndex = 0;

            for (var i = 0; i < T.GetLength(0); i++)
            {
                if (T[i, C] > maxElements)
                {
                    maxElements = T[i, C];
                    maxIndex = i;
                }
            }

            ChooseMiddleElement(maxIndex);
        }

        private void ChooseMiddleElement(int maxElementIndex)
        {
            var middleIndex = K / 2;
            if (Numbers[Subsequences.ElementAt(maxElementIndex)[middleIndex] - 1] == 0)
            {
                var color = ChooseRandomColor(maxElementIndex);
                ColorNumber(Subsequences.ElementAt(maxElementIndex)[middleIndex], color);
                DisplayMove(2, Subsequences.ElementAt(maxElementIndex)[middleIndex], color);
                return;
            }

            var i = 1;
            while (true)
            {
                if (middleIndex - i >= 0 && Numbers[Subsequences.ElementAt(maxElementIndex)[middleIndex - i] - 1] == 0)
                {
                    var color = ChooseRandomColor(maxElementIndex);
                    ColorNumber(Subsequences.ElementAt(maxElementIndex)[middleIndex - i], color);
                    DisplayMove(2, Subsequences.ElementAt(maxElementIndex)[middleIndex - i], color);
                    return;
                }
                if (middleIndex + i < K && Numbers[Subsequences.ElementAt(maxElementIndex)[middleIndex + i] - 1] == 0)
                {
                    var color = ChooseRandomColor(maxElementIndex);
                    ColorNumber(Subsequences.ElementAt(maxElementIndex)[middleIndex + i], color);
                    DisplayMove(2, Subsequences.ElementAt(maxElementIndex)[middleIndex + i], color);
                    return;
                }
                i++;
            }
        }

        private int ChooseRandomColor(int index) // wybiera losowy kolor, użyty juz wcześniej
        {
            var colors = new List<int>();
            for (var i = 0; i < C; i++)
                if (T[index, i] > 0)
                    colors.Add(i + 1);

            if (colors.Count == 0)
                return 1;

            return colors.ElementAt(Random.Next(0, colors.Count));
        }
    }
}
