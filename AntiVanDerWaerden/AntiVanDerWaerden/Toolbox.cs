using System.Collections.Generic;

namespace AntiVanDerWaerden
{
    public static class Toolbox
    {
        public static List<int[]> FindAllSubsequences(int n, int k)
        {
            var subsequences = new List<int[]>();
            int r = n; // różnica
            for (int i = r; i > 0; i--)
            {
                if (1 + (k - 1) * r <= n)
                {
                    int start = 1;
                    while (start + (k - 1) * r <= n)
                    {
                        var array = new int[k];
                        for (int j = 0; j < k;j++)
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
