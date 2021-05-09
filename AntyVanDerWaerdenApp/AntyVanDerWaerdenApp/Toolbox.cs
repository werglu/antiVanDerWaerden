﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntyVanDerWaerdenApp
{
    public static class Toolbox
    {
        public static List<int[]> FindAllSubsequences(int n, int k)
        {
            var subsequences = new List<int[]>();
            var r = n; // różnica
            for (var i = r; i > 0; i--)
            {
                if (1 + (k - 1) * r <= n)
                {
                    var start = 1;
                    while (start + (k - 1) * r <= n)
                    {
                        var array = new int[k];
                        for (var j = 0; j < k; j++)
                            array[j] = start + j * r;

                        start++;
                        subsequences.Add(array);
                    }
                }
                r--;
            }

            return subsequences;
            //for (int i=0; i<subsequences.Count; i++)
            //{
            //    for (int j=0;j<k;j++)
            //    {
            //        Console.Write(subsequences.ElementAt(i)[j] + " ");
            //    }
            //    Console.WriteLine();
            //}
        }
    }
}
