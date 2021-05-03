using System;
using System.Collections.Generic;
using System.Linq;

namespace AntiVanDerWaerden
{
    public class Strategy1 : IStrategy
    {
        private int n;
        private int k; // długość podciągu
        private int c; // liczba dostępnych kolorów
        private bool demo; // tryb demo
        private List<int[]> subsequences; // wszystkie podciągi o długości k
        private int[] numbers; // kolor liczby
        private int[,] T; // tablica T
        private Random random = new Random();
        private int maxColor = 1;
        private int finish = -1;

        public Strategy1(int n, int k, int c, bool demo)
        {
            this.n = n;
            this.k = k;
            this.c = c;
            this.demo = demo;
            numbers = new int[n];
            
            subsequences = Toolbox.FindAllSubsequences(n, k);
            T = new int[subsequences.Count, c+1];
        }


        public void Play()
        {
            DoFirstMovePlayer1();
            if (demo)
            {
                DisplayState();
            }
            while(finish == -1)
            {
                PlayPlayer2();
                DisplayState();
                if (CheckIfAllNumbersCollored() || !CheckIfPlayer1HasChance())
                {
                    finish = -2;
                    break;
                }
                PlayPlayer1();
                DisplayState();
                if (CheckIfAllNumbersCollored() || !CheckIfPlayer1HasChance())
                {
                    finish = -2;
                }
            }
            DisplayFinish(finish);       
        }

        private void DoFirstMovePlayer1()
        {
            int number = (int)Math.Ceiling(n / 2.0);
            ColorNumber(number, 1);
            if (demo)
            {
                Console.Write("Gracz1 wybiera liczbę " + number.ToString() + " i kolor ");
                Console.ForegroundColor = (ConsoleColor)1;
                Console.Write("1");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        private void PlayPlayer1()
        {
            ChooseFringeElement(ChooseBestSubsequenceIndex());
        }

        private void PlayPlayer2()
        {
            int maxElements = 0;
            int maxIndex = 0;

            for (int i = 0; i < T.GetLength(0); i++)
            {
                if (T[i,c] > maxElements)
                {
                    maxElements = T[i, c];
                    maxIndex = i;
                }
            }

            ChooseMiddleElement(maxIndex);
        }

        private int ChooseBestSubsequenceIndex() //jeśli jest kilka podciągów największą liczbą pokolorowanych elementów, losowo wybieramy jeden
        {   
            List<int> bestIndexes = new List<int>();
            int maxElements = 0;
            int maxIndex = 0;

            for (int i = 0; i < T.GetLength(0); i++)
            {
                if (T[i, c] > maxElements)
                {
                    maxElements = T[i, c];
                    maxIndex = i;
                }
            }

            for (int i = 0; i < T.GetLength(0); i++)
            {
                if (T[i, c] == maxElements)
                {
                    bestIndexes.Add(i);
                }
            }

            maxIndex = random.Next(0, bestIndexes.Count);
            return bestIndexes.ElementAt(maxIndex);
        }
        
        private bool CheckIfPlayer1HasChance()
        {
            for (int i = 0; i < T.GetLength(0); i++)
            {
                if (T[i, c] > -1)
                {
                    return true;
                }
            }
            return false;
        }

        private bool CheckIfAllNumbersCollored()
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
        private void ChooseFringeElement(int elementIndex)
        {
            int fringeIndex = k - 1;
            int i = 0;
            while (true)
            {
                if (i < k && numbers[subsequences.ElementAt(elementIndex)[i] - 1] == 0)
                {
                    int color = ChooseNotUsedColor();
                    ColorNumber(subsequences.ElementAt(elementIndex)[i], color);
                    DisplayMove(1, subsequences.ElementAt(elementIndex)[i], color);
                    return;
                }
                if (fringeIndex - i >= 0 && numbers[subsequences.ElementAt(elementIndex)[fringeIndex - i] - 1] == 0)
                {
                    int color = ChooseNotUsedColor();
                    ColorNumber(subsequences.ElementAt(elementIndex)[fringeIndex - i], color);
                    DisplayMove(1, subsequences.ElementAt(elementIndex)[fringeIndex - i], color);
                    return;
                }
                i++;
            }
        }

        private int ChooseNotUsedColor()
        {
            return ++maxColor;
        }

        private int ChooseRandomColor(int index) // wybiera losowy kolor, użyty juz wcześniej
        {
            var colors = new List<int>();

            for (int i = 0; i < c; i++)
            {
                if (T[index, i] > 0)
                {
                    colors.Add(i+1);
                }
            }

            return colors.ElementAt(random.Next(0, colors.Count));
        }

        private void ChooseMiddleElement(int maxElementIndex)
        {
            int middleIndex = k / 2;
            int i = 1;
            if (numbers[subsequences.ElementAt(maxElementIndex)[middleIndex] - 1] == 0)
            {
                int color = ChooseRandomColor(maxElementIndex);
                ColorNumber(subsequences.ElementAt(maxElementIndex)[middleIndex], color);
                DisplayMove(2, subsequences.ElementAt(maxElementIndex)[middleIndex], color);
                return;
            }

            while (true)
            {
                if (middleIndex - i >= 0 && numbers[subsequences.ElementAt(maxElementIndex)[middleIndex-i] - 1] == 0 )
                {
                    int color = ChooseRandomColor(maxElementIndex);
                    ColorNumber(subsequences.ElementAt(maxElementIndex)[middleIndex - i], color);
                    DisplayMove(2, subsequences.ElementAt(maxElementIndex)[middleIndex - i], color);
                    return;
                }
                if (middleIndex + i < k && numbers[subsequences.ElementAt(maxElementIndex)[middleIndex+i] - 1] == 0)
                {
                    int color = ChooseRandomColor(maxElementIndex);
                    ColorNumber(subsequences.ElementAt(maxElementIndex)[middleIndex + i], color);
                    DisplayMove(2, subsequences.ElementAt(maxElementIndex)[middleIndex + i], color);
                    return;
                }
                i++;
            }

        }

        private void DisplayState()
        {
            if (demo)
            {
                for (int i = 0; i < n; i++)
                {
                    if (numbers[i] == 0)
                    {
                        Console.Write($"{i + 1} ");
                    }
                    else
                    {
                        Console.ForegroundColor = (ConsoleColor)numbers[i];
                        Console.Write($"{i + 1} ");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
                Console.WriteLine();
            }
        }

        private void DisplayMove(int player, int number, int color)
        {
            if (demo)
            {
                Console.Write($"Gracz {player} wybiera liczbę {number} i kolor ");
                Console.ForegroundColor = (ConsoleColor)color;
                Console.Write($"{color}\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        private void ColorNumber(int number, int color)
        {
            numbers[number - 1] = color;
            int index = 0;

            foreach(var element in subsequences)
            {
                if (element.Contains(number))
                {
                    if (T[index, color - 1] > 0)
                    {
                        T[index, color - 1] = color;
                        T[index, c] = -1; // nie może być już tęczowy
                    }
                    else
                    {
                        T[index, color - 1] = color;
                        if (T[index, c] > -1)
                        {
                            T[index, c]++;
                        }
                        if (T[index, c] == k)
                        {
                            finish = index;
                        }
                    }
                }
                index++;
            }
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
                Console.Write("Zwyciężył Gracz1! Znaleziono tęczowy podciag ");
                for (int i = 0; i < k; i++)
                {
                    Console.ForegroundColor = (ConsoleColor)numbers[subsequences.ElementAt(index)[i] - 1];
                    Console.Write($"{subsequences.ElementAt(index)[i]} ");
                }
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
