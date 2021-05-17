using System.Collections.Generic;

namespace AntyVanDerWaerdenApp.Strategies
{
    public interface IStrategy
    {
        (int number, int color) MakeMove(IReadOnlyList<int> numbers);
        void Update(int number, int color);
        void Reset();
    }
}
