namespace AntyVanDerWaerdenApp.Strategies
{
    public interface IPlayerStrategy
    {
        /// <summary>
        /// Długość ciągu.
        /// </summary>;
        int N { get; }
        
        /// <summary>
        /// Liczba kolorów.
        /// </summary>
        int K { get; }
        
        /// <summary>
        /// Liczba kolorów.
        /// </summary>
        int C { get; }

        /// <summary>
        /// true = tryb demo.
        /// </summary>
        bool Demo { get; }

        void MakeFirstMove();
        void MakeMove();
    }
}
