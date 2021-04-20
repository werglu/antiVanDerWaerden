using System;
using System.Collections.Generic;
using System.Text;

namespace AntiVanDerWaerden
{
    public class Strategy1 : IStrategy
    {
        int n;
        int k; //długość podciągu
        int c; //liczba dostępnych kolorów
        bool demo; //tryb demo

        public Strategy1(int n, int k, int c, bool demo)
        {
            this.n = n;
            this.k = k;
            this.c = c;
            this.demo = demo;
        }
    }
}
