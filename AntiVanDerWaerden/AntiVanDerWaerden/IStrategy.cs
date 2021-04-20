using System;
using System.Collections.Generic;
using System.Text;

namespace AntiVanDerWaerden
{
    interface IStrategy
    {
        public List<int[]> FindAllSubsequences();
        public void Play();
    }
}
