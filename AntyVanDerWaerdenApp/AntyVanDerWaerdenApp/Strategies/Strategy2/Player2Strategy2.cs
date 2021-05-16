using System.Collections.Generic;
using System.Linq;

namespace AntyVanDerWaerdenApp.Strategies.Strategy2
{
    public class Player2Strategy2 : PlayerStrategy2Base
    {
        public Player2Strategy2(int n, int k, int c, bool demo) : base(n, k, c, demo)
        { }

        public override void MakeFirstMove() => MakeMove();

        public override void MakeMove()
        {
            UpdateAArray();

            var number = FindNumberToColor();
            var color = FindMostUsedColor();

            DisplayMove(2, number + 1, color);
            ColorNumber(number, color);
        }

        private int FindMostUsedColor()
        {
            var colors = new int[C + 1];
            colors[0] = int.MinValue;
            
            var maxSubsequence = FindMaxSubsequence();
            for (var i = 0; i < Subsequences.Count; i++)
            {
                if (T[i][K] != maxSubsequence)
                    continue;
                
                foreach (var element in Subsequences[i])
                    if (Numbers[element - 1] != 0)
                        colors[Numbers[element - 1]]++;
            }
            
            var max = colors.Max();
            var indexes = new List<int>();
            for (var i = 0; i < colors.Length; i++)
                if (colors[i] == max)
                    indexes.Add(i);
            
            return indexes[Random.Next(indexes.Count)];
        }
    }
}
