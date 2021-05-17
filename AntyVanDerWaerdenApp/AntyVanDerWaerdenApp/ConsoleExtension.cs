using System;

namespace AntyVanDerWaerdenApp
{
    public static class ConsoleExtension
    {
        public static void ClearLine()
        {
            Console.SetCursorPosition(0, Console.CursorTop);
            for (var i = 0; i < Console.WindowWidth; i++)
                Console.Write(' ');
            Console.SetCursorPosition(0, Console.CursorTop);
        }
    }
}
