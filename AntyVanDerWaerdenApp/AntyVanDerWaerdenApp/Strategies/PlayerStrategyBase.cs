using System;

namespace AntyVanDerWaerdenApp.Strategies
{
    public abstract class PlayerStrategyBase : IPlayerStrategy
    {
        public int N { get; }
        public int K { get; }
        public int C { get; }
        public bool Demo { get; }

        public PlayerStrategyBase(int n, int k, int c, bool demo)
        {
            N = n;
            K = k;
            C = c;
            Demo = demo;
        }

        public abstract void MakeFirstMove();
        public abstract void MakeMove();

        protected void DisplayMove(int player, int number, int color)
        {
            if (!Demo)
                return;

            Console.Write($"Gracz {player} wybiera liczbę {number} i kolor ");
            Console.ForegroundColor = (ConsoleColor)color;
            Console.Write($"{color}\n");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
