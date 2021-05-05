using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;

namespace AntiVanDerWaerden
{
    public class Strategy2 : IStrategy
    {
        private readonly int n;
        private readonly int k;
        private readonly int c;
        private readonly bool demo;
        private readonly int[] numbers; //kolor liczby
        private List<int[]> subsequences;
        private List<int[]> TArray = new List<int[]>();
        private int[] A;
        private int finish = -1;
        private readonly Random random = new Random();
        public List<int[]> FindAllSubsequences()

        {
            var subsequences = new List<int[]>();
            int r = n; //różnica
            for (int i = r; i > 0; i--)
            {
                if (1 + (k - 1) * r <= n)
                {
                    int start = 1;
                    while (start + (k - 1) * r <= n)
                    {
                        var array = new int[k];
                        for (int j = 0; j < k; j++)
                        {
                            array[j] = start + j * r;
                        }
                        start++;
                        subsequences.Add(array);
                    }
                }
                r--;
            }
            return subsequences;
        }

        public void Play()
        {
            if (demo)
            {
               // DisplayState();
            }

            while (finish == -1)
            {
                //for (int i = 0; i < subsequences.Count; i++)
                //{
                //    for (int j = 0; j < k; j++)
                //    {
                //        Console.Write(subsequences.ElementAt(i)[j] + " ");
                //    }
                //    Console.Write("|");
                //    foreach (var co in TArray[i])
                //    {
                //        Console.Write(co + " ");
                //    }
                //    Console.WriteLine();
                //}
                //Console.WriteLine("Player1:");
                PlayPlayer1();
                DisplayState();
                finish = CheckIfPlayer1Won();
                if (finish != -1)
                    break;
                if (CheckIfAllNumbersColored() || subsequences.Count == 0)
                {
                    finish = -2;
                    break;
                }
                //Console.WriteLine("Player2:");
                PlayPlayer2();
                DisplayState();
                if (CheckIfAllNumbersColored() || subsequences.Count == 0)
                {
                    finish = -2;
                }
            }
            DisplayFinish(finish);
        }

        public int CheckIfPlayer1Won()
        {
            for (int i = 0; i < subsequences.Count; i++)
            {
                if (TArray[i][k] == k)
                    return i;
            }
            return -1;
        }

        public Strategy2(int n, int k, int c, bool demo)
        {
            this.n = n;
            this.k = k;
            this.c = c;
            this.demo = demo;
            subsequences = FindAllSubsequences();
            numbers = new int[n];
            A = new int[n];
            for (int i = 0; i < subsequences.Count; i++)
            {
                TArray.Add(new int[k + 1]);
            }
        }

        private void DisplayState()
        {
            //Console.WriteLine("Display state:");
            for (int i = 0; i < n; i++)
            {
                if (numbers[i] == 0)
                {
                    Console.Write((i + 1).ToString() + " ");
                }
                else
                {
                    Console.ForegroundColor = (ConsoleColor)numbers[i];
                    Console.Write((i + 1).ToString() + " ");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            Console.WriteLine();

        }

        private int FindLeastUsedColor()
        {
            int[] colors = new int[c+1];
            colors[0] = Int32.MaxValue;
            int maxSubseqence = FindMaxSubseqence();
            for (int i = 0; i < subsequences.Count; i++)
            {
                if (TArray[i][k] == maxSubseqence)
                {
                    foreach (var element in subsequences[i])
                    {
                        if (numbers[element - 1] != 0)
                            colors[numbers[element - 1]]++;
                    }
                }
            }
            int min = colors.Min();
            List<int> indexes = new List<int>();
            for (int i = 0; i < colors.Length; i++)
            {
                if(colors[i]==min)
                    indexes.Add(i);
            }
            return indexes[random.Next(indexes.Count)];
        }

        private int FindMaxSubseqence()
        {
            int max = 0;
            for (int i = 0; i < TArray.Count; i++)
            {
                if (TArray[i][k] > max)
                    max = TArray[i][k];
            }
            return max;
        }

        private void UpdateAAraay()
        {
            int max = FindMaxSubseqence();
            A = new int[n];
            for (int i = 0; i < subsequences.Count; i++)
            {
                if (TArray[i][k] == max)
                {
                    for (int j = 0; j < subsequences[i].Length; j++)
                    {
                        if (numbers[subsequences[i][j] - 1] != 0)
                            A[subsequences[i][j] - 1] = -1;
                        else
                            A[subsequences[i][j] - 1]++;
                    }
                }
            }
        }

        private int FindNumberToColor()
        {
            int max = A.Max();

            List<int> maxIndexes = new List<int>();
            for (int i = 0; i < A.Length; i++)
            {
                if (A[i] == max)
                    maxIndexes.Add(i);
            }
            return maxIndexes[random.Next(maxIndexes.Count)];
        }

        private void PlayPlayer1()
        {
            UpdateAAraay();
            int number = FindNumberToColor();
            int last_color = FindLeastUsedColor();
            
            DisplayMove(1, number+1, last_color);
            ColorNumber(number, last_color);
        }

        private int FindMostUsedColor()
        {
            int[] colors = new int[c+1];
            colors[0]=Int32.MinValue;
            int maxSubseqence = FindMaxSubseqence();
            for (int i = 0; i < subsequences.Count; i++)
            {
                if (TArray[i][k] == maxSubseqence)
                {
                    foreach (var element in subsequences[i])
                    {
                        if (numbers[element - 1] != 0)
                            colors[numbers[element - 1]]++;
                    }
                }
            }
            int max = colors.Max();
            List<int> indexes = new List<int>();
            for (int i = 0; i < colors.Length; i++)
            {
                if (colors[i] == max)
                    indexes.Add(i);
            }
            return indexes[random.Next(indexes.Count)];
        }

        private void PlayPlayer2()
        {
            UpdateAAraay();
            int number = FindNumberToColor();
            int last_color = FindMostUsedColor();
            DisplayMove(2, number + 1, last_color);
            ColorNumber(number, last_color);
        }

        private bool CheckIfAllNumbersColored()
        {
            for (int i = 0; i < n; i++)
            {
                if (numbers[i] == 0)
                {
                    return false;
                }
            }
            return true;
        }

        private void DisplayFinish(int index)
        {
            Console.WriteLine();
            if (index == -2)
            {
                Console.Write("Zwyciężył Gracz2! Nie znaleziono tęczowego podciagu ");
            }
            else
            {
                Console.Write("Zwyciężył Gracz1! Znaleziono teczowy podciag ");
                for (int i = 0; i < k; i++)
                {
                    Console.ForegroundColor = (ConsoleColor)numbers[subsequences.ElementAt(index)[i] - 1];
                    Console.Write(subsequences.ElementAt(index)[i] + " ");
                }
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
        }

        private void ColorNumber(int num, int color)
        {
            numbers[num] = color;
            num++;
            for (int i = 0; i < subsequences.Count; i++)
            {
                for (int j = 0; j < subsequences[i].Length; j++)
                {
                    if (subsequences[i][j] == num)
                    {
                        if (TArray[i].Contains(color))
                        {
                            TArray.RemoveAt(i);
                            subsequences.RemoveAt(i);
                            i--;
                            break;
                        }
                        TArray[i][k]++;
                        TArray[i][j] = color;
                    }
                }
            }

        }
        private void DisplayMove(int player, int number, int color)
        {
            if (!demo)
                return;

            Console.Write($"Gracz {player} wybiera liczbę {number} i kolor ");
            Console.ForegroundColor = (ConsoleColor)color;
            Console.Write($"{color}\n");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
