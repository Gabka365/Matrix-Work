using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MatrixWork
{
    internal static class ArraySupport
    {
        public static void SizeInput(out int[] Size)
        {
            Size = new int[2];

            Console.WriteLine(" Введите количество строк и количество столбцов матрицы:");

            while (true)
            {
                try
                {
                    for (int i = 0; i < Size.Length; i++)
                    {
                        Console.WriteLine($"{i + 1}-й параметр : ");
                        Size[i] = Convert.ToInt32(Console.ReadLine());
                    }
                    break;
                }
                catch
                {
                    Console.WriteLine(" Ошибка ввода. Вам следует повторить ввод данных. ");
                }

            }

        }

        public static void MatrixFilling (in int[] Size, out int[,] matrix)
        {
            matrix = new int[Size[0], Size[1]];

            while (true)
            {
                try
                {
                    for (int i = 0; i < Size[0]; i++)
                    {
                        Console.WriteLine($"\n{i}-ая строка матрицы\n");
                        for (int j = 0; j < Size[1]; j++)
                            matrix[i, j] = Convert.ToInt32(Console.ReadLine());
                    }
                    break;
                }
                catch
                {
                    Console.WriteLine(" Ошибка ввода. Вам следует повторить ввод данных. ");
                }
            }

        }

        public static void Print(in int[] Size, in int[,] matrix)
        {
            Console.WriteLine("\n Матрица:\n");

            for (int i = 0; i < Size[0]; i++)
            {
                for (int j = 0; j < Size[1]; j++)
                {
                    Console.Write($"\t{matrix[i, j]}");
                }
                Console.WriteLine("\n");
            }
        }

        public static void ChooseOperation(out int NumberOfOperation)
        {
            Console.WriteLine("\n Введите номер операции, которую хотите совершить:" +
                "\n 1-я: Нахождение количества положительных/отрицательных переменных" +
                "\n 2-я: Сортировка строк массива" +
                "\n 3-я: Инверсия строк массива\n");

            while (true)
            {
                NumberOfOperation = Convert.ToInt32(Console.ReadLine());
                if (NumberOfOperation > 0 && NumberOfOperation < 4)
                    break;
                else
                {
                    Console.WriteLine(" Ошибка ввода. Повторите заново:");
                    continue;
                }
            }   
            
        }

        public static void Implement(in int[] Size, in int NumberOfOperation, ref int[,] matrix)
        {
            switch ((MatrixOperations)NumberOfOperation)
            {
                case (MatrixOperations.FindElements):
                    {
                        Console.WriteLine("\n Выберите количество каких элементов вы хотите подсчитать:"
                            + "\n4 - положительныx"
                            + "\n5 - отрицательныx\n");

                        int TypeOfOperation;

                        while (true)
                        {
                            TypeOfOperation = Convert.ToInt32(Console.ReadLine());
                            if (TypeOfOperation > 3 && TypeOfOperation < 6)
                            {
                                FindElements(TypeOfOperation, matrix, out int Count);
                                break;
                            }
                            else
                            {
                                Console.WriteLine(" Ошибка ввода. Повторите заново:");
                                continue;
                            }
                        }
                        break;
                    }
                case (MatrixOperations.SortStrings):
                    {
                        Console.WriteLine(" Нажмите на клавиатуре стрелку вправо или влево для выбора направления сортировки.");

                        while (true)
                        {
                            ConsoleKey DiractionOfSort = Console.ReadKey().Key;
                            if (DiractionOfSort == ConsoleKey.LeftArrow || DiractionOfSort == ConsoleKey.RightArrow)
                            {
                                SortStrings(matrix, DiractionOfSort);
                                break;
                            }
                            else
                            {
                                Console.WriteLine("\n Допускаются только стрелки вправо или влево. Повторите.");
                                continue;
                            }

                        }

                        Print(in Size, in matrix);


                        break;
                    }

                case (MatrixOperations.InverseString):
                    {
                        Console.WriteLine("\n Выберите какую инверсию вы хотите совершить" +
                            "\n 6 - всех строк матрицы" +
                            "\n 7 - одной строки");

                        int TypeOfOperation;

                        while (true)
                        {
                            TypeOfOperation = Convert.ToInt32(Console.ReadLine());
                            if (TypeOfOperation > 5 && TypeOfOperation < 8)
                            {
                                InverseString(TypeOfOperation, ref matrix);
                                break;
                            }
                            else
                            {
                                Console.WriteLine(" Ошибка ввода. Повторите заново:");
                                continue;
                            }
                        }

                        Print(in Size, matrix);
                        break;
                    }
            }
        }

        private static void FindElements(in int TypeOfOperation, int[,] matrix, out int Count)
        {
            Count = 0;
            
            for (int i = 0; i < matrix.GetLength(0); i++) 
            { 
                for (int j = 0;  j < matrix.GetLength(1); j++)
                {
                    switch ((MatrixOperations)TypeOfOperation)
                    {
                        case (MatrixOperations.FindPlus):
                            if (matrix[i, j] > 0)
                                Count++;
                            break;
                        case (MatrixOperations.FindMinus):
                            if (matrix[i, j] < 0)
                                Count++;
                            break;
                    }
                }
            }

            Console.WriteLine($" Количество {((MatrixOperations)TypeOfOperation == MatrixOperations.FindPlus? 
                " положительных" : "отрицательных")} элементов равняется {Count}") ;

        }

        private static void SortStrings(int[,] matrix, ConsoleKey Diraction)
        {
            int[] arr = new int[matrix.GetLength(1)];

            for (int i = 0; i < matrix.GetLength (0); i++)
            {
                for ( int j = 0; j < matrix.GetLength(1); j++)
                    arr[j] = matrix[i,j];

                QuickSort(arr, matrix.GetLength(1), Diraction);

                for (int j = 0; j < matrix.GetLength(1); j++)
                    matrix[i, j] = arr[j];

            }
        }


        private static void QuickSort(int[] arr, int size, ConsoleKey Diraction)
        {
            int temp = 0;

            if (arr.Length <= 2)
            {
                if ((size == 2 && arr[0] > arr[1] && Diraction is ConsoleKey.LeftArrow) 
                    || (size == 2 && arr[0] < arr[1] && Diraction is ConsoleKey.RightArrow))
                {
                    temp = arr[0];
                    arr[0] = arr[1];
                    arr[1] = temp;
                }
                return;
            }
            else
            {
                int lead = arr[size / 2], CountOfLess = 0, CountOfLarger = 0;

                for (int i = 0; i < size; i++)
                {
                    if (i == size / 2)
                        continue;
                    if (arr[i] > lead)
                        CountOfLarger++;
                    if (arr[i] <= lead)
                        CountOfLess++;
                }

                int[] ArrayOfLessElem = new int[CountOfLess];
                int[] ArrayOfLargerElem = new int[CountOfLarger];
                int j = 0, k = 0;



                for (int i = 0; i < size; i++)
                {
                    if (i == size / 2)
                        continue;
                    if (arr[i] > lead)
                    {
                        ArrayOfLargerElem[j] = arr[i];
                        j++;
                    }
                    if (arr[i] <= lead)
                    {
                        ArrayOfLessElem[k] = arr[i];
                        k++;
                    }
                }


                QuickSort(ArrayOfLessElem, CountOfLess, Diraction);
                QuickSort(ArrayOfLargerElem, CountOfLarger, Diraction);

                switch (Diraction)
                {
                    case ConsoleKey.LeftArrow:
                        for (int i = 0; i < k; i++)
                            arr[i] = ArrayOfLessElem[i];
                        arr[k] = lead;
                        for (int i = 0; i < j; i++)
                            arr[i + k + 1] = ArrayOfLargerElem[i];
                        break;
                    case ConsoleKey.RightArrow:
                        for (int i = 0; i < j; i++)
                            arr[i] = ArrayOfLargerElem[i];
                        arr[j] = lead;
                        for (int i = 0; i < k; i++)
                            arr[i + j + 1] = ArrayOfLessElem[i];
                        break;
                }    

                

            }
        }

        private static void InverseString(in int TypeOfOperation, ref int[,] matrix)
        {
            int i = 0, height = matrix.GetLength(0), temp = 0;

            if ((MatrixOperations)TypeOfOperation == MatrixOperations.InverseOne)
            {
                Console.WriteLine("  Введите номер строки, которую хотите инвертировать:");
                while(true)
                {
                    i = Convert.ToInt32(Console.ReadLine());
                    if (i < 0 || i >= matrix.GetLength(0))
                    {
                        Console.WriteLine(" Ошибка ввода. Повторите заново");
                        continue;
                    }
                    else
                    {
                        height = i + 1;
                        break;
                    }
                }
            }

            for (;i < height; i++)
            {
                for (int j = 0; j <  matrix.GetLength(1)/2; j++) 
                { 
                    temp = matrix[i, matrix.GetLength(1) - 1 - j];
                    matrix[i, matrix.GetLength(1) - 1 - j] = matrix[i, j];
                    matrix[i, j] = temp;
                }
            }

        }

        private enum MatrixOperations
        {
            FindElements = 1,
            SortStrings,
            InverseString,
            FindPlus,
            FindMinus,
            InverseAllStrings,
            InverseOne
        }

    }
}
