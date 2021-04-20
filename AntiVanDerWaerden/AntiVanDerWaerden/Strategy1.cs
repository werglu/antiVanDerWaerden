using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntiVanDerWaerden
{
    public class Strategy1 : IStrategy
    {
        int n;
        int k; //długość podciągu
        int c; //liczba dostępnych kolorów
        bool demo; //tryb demo
        List<int[]> subsequences = new List<int[]>(); //wszytskie podciągi o długości k
        int[] numbers; //kolor liczby
        int[,] TArray; // tablica T
        Random random = new Random();
        int maxColor = 1;
        int finish = -1;

        public Strategy1(int n, int k, int c, bool demo)
        {
            this.n = n;
            this.k = k;
            this.c = c;
            this.demo = demo;
            this.numbers = new int[n];

            this.subsequences = FindAllSubsequences();
            this.TArray = new int[subsequences.Count, c+1];
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

        public void DoFirstMovePlayer1()
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

        public void PlayPlayer1()
        {
            ChooseFringeElement(ChooseBestSubsequenceIndex());
        }

        public void PlayPlayer2()
        {
            int maxElements = 0;
            int maxIndex = 0;

            for (int i=0; i<TArray.GetLength(0); i++)
            {
                if (TArray[i,c] > maxElements)
                {
                    maxElements = TArray[i, c];
                    maxIndex = i;
                }
            }

            ChooseMiddleElement(maxIndex);
        }

        public int ChooseBestSubsequenceIndex() //jeśli jest kilka podciągów największą liczbą pokolorowanych elementów, losowo wybieramy jeden
        {   
            List<int> bestIndexes = new List<int>();
            int maxElements = 0;
            int maxIndex = 0;

            for (int i = 0; i < TArray.GetLength(0); i++)
            {
                if (TArray[i, c] > maxElements)
                {
                    maxElements = TArray[i, c];
                    maxIndex = i;
                }
            }

            for (int i = 0; i < TArray.GetLength(0); i++)
            {
                if (TArray[i, c] == maxElements)
                {
                    bestIndexes.Add(i);
                }
            }

            maxIndex = random.Next(0, bestIndexes.Count);
            return  bestIndexes.ElementAt(maxIndex);
        }
        
        public bool CheckIfPlayer1HasChance()
        {
            for (int i=0; i<TArray.GetLength(0); i++)
            {
                if (TArray[i, c] > -1)
                {
                    return true;
                }
            }
            return false;
        }

        public bool CheckIfAllNumbersCollored()
        {
            for (int i=0; i<n; i++)
            {
                if (numbers[i]==0)
                {
                    return false;
                }    
            }
            return true;
        }
        public void ChooseFringeElement(int elementIndex)
        {
            int fringeIndex = k - 1;
            int i = 0;
            while (true)
            {
                if (i < k && numbers[subsequences.ElementAt(elementIndex)[i] - 1] == 0)
                {
                    int color = ChooseNotUsedColor(elementIndex);
                    ColorNumber(subsequences.ElementAt(elementIndex)[i], color);
                    DisplayMove(1, subsequences.ElementAt(elementIndex)[i], color);
                    return;
                }
                if (fringeIndex - i >= 0 && numbers[subsequences.ElementAt(elementIndex)[fringeIndex - i] - 1] == 0)
                {
                    int color = ChooseNotUsedColor(elementIndex);
                    ColorNumber(subsequences.ElementAt(elementIndex)[fringeIndex - i], color);
                    DisplayMove(1, subsequences.ElementAt(elementIndex)[fringeIndex - i], color);
                    return;
                }
                i++;
            }
        }

        public int ChooseNotUsedColor(int index)
        {
            maxColor++;
            return maxColor;
        }

        public int ChooseRandomColor(int index) // wybiera losowy kolor, użyty juz wcześniej
        {
            List<int> colors = new List<int>();

            for (int i=0; i<c; i++)
            {
                if (TArray[index, i] >0)
                {
                    colors.Add(i+1);
                }
            }

            int rnd = random.Next(0, colors.Count);
            return colors.ElementAt(rnd);
        }

        public void ChooseMiddleElement(int maxElementIndex)
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
                if (middleIndex-i >= 0 && numbers[subsequences.ElementAt(maxElementIndex)[middleIndex-i] -1] == 0 )
                {
                    int color = ChooseRandomColor(maxElementIndex);
                    ColorNumber(subsequences.ElementAt(maxElementIndex)[middleIndex - i], color);
                    DisplayMove(2, subsequences.ElementAt(maxElementIndex)[middleIndex - i], color);
                    return;
                }
                if (middleIndex+i < k && numbers[subsequences.ElementAt(maxElementIndex)[middleIndex+i] -1] == 0)
                {
                    int color = ChooseRandomColor(maxElementIndex);
                    ColorNumber(subsequences.ElementAt(maxElementIndex)[middleIndex + i], color);
                    DisplayMove(2, subsequences.ElementAt(maxElementIndex)[middleIndex + i], color);
                    return;
                }
                i++;
            }

        }

        public void DisplayState()
        {
            if (demo)
            {
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
        }

        public void DisplayMove(int gracz, int number, int color)
        {
            if (demo)
            {
                Console.Write("Gracz" + gracz.ToString() +" wybiera liczbę " + number.ToString() + " i kolor ");
                Console.ForegroundColor = (ConsoleColor)color;
                Console.Write(color.ToString());
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public void ColorNumber(int number, int color)
        {
            numbers[number-1] = color;
            int index = 0;

            foreach(var element in subsequences)
            {
                if (element.Contains(number))
                {
                    if (TArray[index, color-1] > 0)
                    {
                        TArray[index, color - 1] = color;
                        TArray[index, c] = -1; //nie może być już tęczowy
                    }
                    else
                    {
                        TArray[index, color-1] = color;
                        if (TArray[index, c] > -1)
                        {
                            TArray[index, c]++;
                        }
                        if (TArray[index, c] == k)
                        {
                            finish = index;
                        }
                    }
                }
                index++;
            }
        }


        public void DisplayFinish(int index)
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

        public List<int[]> FindAllSubsequences()
        {
            var subsequences = new List<int[]>();
            int r = n; //różnica
            for (int i=r; i>0; i--)
            {
                if (1+(k-1)*r <= n)
                {
                    int start = 1;
                    while (start + (k - 1) * r <= n)
                    {
                        var array = new int[k];
                        for (int j=0; j<k;j++)
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
