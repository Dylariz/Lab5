using System;
using System.Collections;

namespace Level2
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Number25();
        }

        /// <summary>
        /// Поменять местами строку матрицы А размера 5 × 5 и столбец матрицы В размера 5 × 5,
        /// содержащие максимальные элементы на диагоналях. Поиск максимального элемента на
        /// диагонали осуществить в методе.
        /// </summary>
        static void Number4()
        {
            int n = 5;
            int[,] matrixA = GenerateMatrix(n, n, (-10, 10));
            int[,] matrixB = GenerateMatrix(n, n, (-10, 20));

            Console.WriteLine("Matrix A:");
            PrintMatrix(matrixA);
            Console.WriteLine("Matrix B:");
            PrintMatrix(matrixB);
            
            int maxA = MaxElementsOf(matrixA, Direction.Diagonals)[n / 2];
            int maxB = MaxElementsOf(matrixB, Direction.Diagonals)[n / 2];
            
            int indexA = 0, indexB = 0;
            for (int i = 0; i < n; i++)
            {
                if (matrixA[i, i] == maxA)
                    indexA = i;
                if (matrixB[i, i] == maxB)
                    indexB = i;
            }
            
            for (int i = 0; i < n; i++)
                (matrixA[indexA, i], matrixB[i, indexB]) = (matrixB[i, indexB], matrixA[indexA, i]);
            
            Console.WriteLine("Results:");
            Console.WriteLine("Matrix A:");
            PrintMatrix(matrixA);
            Console.WriteLine("Matrix B:");
            PrintMatrix(matrixB);
        }

        /// <summary>
        /// В двух заданных матрицах найти максимальные элементы и поменять их местами.
        /// Поиск максимального элемента матрицы оформить в виде метода.
        /// </summary>
        static void Number11()
        {
            int n = 5;
            int[,] matrixA = GenerateMatrix(n, n, (-10, 10));
            int[,] matrixB = GenerateMatrix(n, n, (-10, 20));

            Console.WriteLine("Matrix A:");
            PrintMatrix(matrixA);
            Console.WriteLine("Matrix B:");
            PrintMatrix(matrixB);
            
            int[] maxIndexA = IndexesOf(matrixA, Function.Max, Direction.All);
            int[] maxIndexB = IndexesOf(matrixB, Function.Max, Direction.All);
            (matrixA[maxIndexA[0], maxIndexA[1]], matrixB[maxIndexB[0], maxIndexB[1]]) = 
                (matrixB[maxIndexB[0], maxIndexB[1]], matrixA[maxIndexA[0], maxIndexA[1]]);
            
            Console.WriteLine("Results:");
            Console.WriteLine("Matrix A:");
            PrintMatrix(matrixA);
            Console.WriteLine("Matrix B:");
            PrintMatrix(matrixB);
        }

        /// <summary>
        /// В двух заданных квадратных матрицах упорядочить элементы главной диагонали по
        /// возрастанию. Упорядочение диагональных элементов оформить в виде метода.
        /// </summary>
        static void Number18()
        {
            int n = 5;
            int[,] matrixA = GenerateMatrix(n, n, (-10, 10));
            int[,] matrixB = GenerateMatrix(n, n, (-10, 20));

            Console.WriteLine("Matrix A:");
            PrintMatrix(matrixA);
            Console.WriteLine("Matrix B:");
            PrintMatrix(matrixB);
            
            MatrixMainDiagonalSort(matrixA);
            MatrixMainDiagonalSort(matrixB);
            
            Console.WriteLine("Results:");
            Console.WriteLine("Matrix A:");
            PrintMatrix(matrixA);
            Console.WriteLine("Matrix B:");
            PrintMatrix(matrixB);
        }

        /// <summary>
        /// В двух заданных матрицах найти строку, содержащую максимальное количество
        /// отрицательных элементов. Нахождение количества отрицательных элементов строк
        /// матрицы и поиск среди них максимального оформить в виде методов.
        /// </summary>
        static void Number25()
        {
            int n = 5;
            int[,] matrixA = GenerateMatrix(n, n, (-5, 10));
            int[,] matrixB = GenerateMatrix(n, n, (-3, 8));

            Console.WriteLine("Matrix A:");
            PrintMatrix(matrixA);
            Console.WriteLine("Matrix B:");
            PrintMatrix(matrixB);
            
            int[] countOfNegativeA = CountOf(matrixA, Sign.Negative, Direction.Rows);
            int[] countOfNegativeB = CountOf(matrixB, Sign.Negative, Direction.Rows);
            
            int maxIndexA = IndexOfMaxInArray(countOfNegativeA), maxIndexB = IndexOfMaxInArray(countOfNegativeB);

            Console.WriteLine("Results:");
            Console.WriteLine($"Matrix A: {maxIndexA} строка");
            Console.WriteLine($"Matrix B: {maxIndexB} строка");
        }

        #region FunctionalMethods

        static int[,] ApplyFuncToMatrix(int[,] matrix, Func<int, int> func, Direction direction = Direction.Rows, int index = -1)
        {
            if (index != -1)
            {
                if (direction == Direction.Rows)
                {
                    for (var i = 0; i < matrix.GetLength(1); i++)
                    {
                        matrix[index, i] = func(matrix[index, i]);
                    }
                }
                else
                {
                    for (var i = 0; i < matrix.GetLength(0); i++)
                    {
                        matrix[i, index] = func(matrix[i, index]);
                    }
                }
            }
            else
            {
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        matrix[i, j] = func(matrix[i, j]);
                    }
                }
            }

            return matrix;
        }
        
        static int[] MinElementsOf(int[,] matrix, Direction direction)
        {
            int[] result;
            switch (direction)
            {
                case Direction.Rows:
                    result = new int[matrix.GetLength(0)];
                    for (int i = 0; i < matrix.GetLength(0); i++)
                    {
                        result[i] = matrix[i, 0];
                        for (int j = 0; j < matrix.GetLength(1); j++)
                        {
                            if (matrix[i, j] < result[i])
                            {
                                result[i] = matrix[i, j];
                            }
                        }
                    }

                    return result;

                case Direction.Columns:
                    result = new int[matrix.GetLength(1)];
                    for (int i = 0; i < matrix.GetLength(1); i++)
                    {
                        result[i] = matrix[0, i];
                        for (int j = 0; j < matrix.GetLength(0); j++)
                        {
                            if (matrix[j, i] < result[i])
                            {
                                result[i] = matrix[j, i];
                            }
                        }
                    }

                    return result;
                case Direction.Diagonals:
                    int n = matrix.GetLength(0);
                    result = new int[2 * n - 1];

                    for (int t = 0; t < n; t++)
                    {
                        int min1 = matrix[0, t];
                        int min2 = matrix[t, 0];
                        for (int i = 0; i < n - t; i++)
                        {
                            if (t != 0 && matrix[i, i + t] < min1)
                                min1 = matrix[i, i + t];

                            if (matrix[i + t, i] < min2)
                                min2 = matrix[i + t, i];
                        }

                        result[result.Length / 2 + t] = min1;
                        result[result.Length / 2 - t] = min2;
                    }

                    return result;
                case Direction.All:
                    result = new int[1];
                    result[0] = matrix[0, 0];
                    for (int i = 0; i < matrix.GetLength(0); i++)
                    {
                        for (int j = 0; j < matrix.GetLength(1); j++)
                        {
                            if (matrix[i, j] < result[0])
                            {
                                result[0] = matrix[i, j];
                            }
                        }
                    }

                    return result;
            }

            return null;
        }

        static int[] MaxElementsOf(int[,] matrix, Direction direction)
        {
            int[] result;
            switch (direction)
            {
                case Direction.Rows:
                    result = new int[matrix.GetLength(0)];
                    for (int i = 0; i < matrix.GetLength(0); i++)
                    {
                        result[i] = matrix[i, 0];
                        for (int j = 0; j < matrix.GetLength(1); j++)
                        {
                            if (matrix[i, j] > result[i])
                            {
                                result[i] = matrix[i, j];
                            }
                        }
                    }

                    return result;

                case Direction.Columns:
                    result = new int[matrix.GetLength(1)];
                    for (int i = 0; i < matrix.GetLength(1); i++)
                    {
                        result[i] = matrix[0, i];
                        for (int j = 0; j < matrix.GetLength(0); j++)
                        {
                            if (matrix[j, i] > result[i])
                            {
                                result[i] = matrix[j, i];
                            }
                        }
                    }

                    return result;

                case Direction.Diagonals:
                    int n = matrix.GetLength(0);
                    result = new int[2 * n - 1];

                    for (int t = 0; t < n; t++)
                    {
                        int max1 = matrix[0, t];
                        int max2 = matrix[t, 0];
                        for (int i = 0; i < n - t; i++)
                        {
                            if (t != 0 && matrix[i, i + t] > max1)
                                max1 = matrix[i, i + t];

                            if (matrix[i + t, i] > max2)
                                max2 = matrix[i + t, i];
                        }

                        result[result.Length / 2 + t] = max1;
                        result[result.Length / 2 - t] = max2;
                    }

                    return result;
                case Direction.All:
                    result = new int[1];
                    result[0] = matrix[0, 0];
                    for (int i = 0; i < matrix.GetLength(0); i++)
                    {
                        for (int j = 0; j < matrix.GetLength(1); j++)
                        {
                            if (matrix[i, j] > result[0])
                            {
                                result[0] = matrix[i, j];
                            }
                        }
                    }

                    return result;
            }

            return null;
        }

        static int[] IndexesOf(int[,] matrix, Function function, Direction direction)
        {
            int[] arr = function == Function.Min ? MinElementsOf(matrix, direction) : MaxElementsOf(matrix, direction);
            int[] indexes;
            switch (direction)
            {
                case Direction.Rows:
                    indexes = new int[matrix.GetLength(0)];
                    for (int i = 0; i < matrix.GetLength(0); i++)
                    {
                        for (int j = 0; j < matrix.GetLength(1); j++)
                        {
                            if (matrix[i, j] == arr[i])
                            {
                                indexes[i] = j;
                                break;
                            }
                        }
                    }
                    return indexes;
                
                case Direction.Columns:
                    indexes = new int[matrix.GetLength(1)];
                    for (int i = 0; i < matrix.GetLength(1); i++)
                    {
                        for (int j = 0; j < matrix.GetLength(0); j++)
                        {
                            if (matrix[j, i] == arr[i])
                            {
                                indexes[i] = j;
                                break;
                            }
                        }
                    }

                    return indexes;
                
                case Direction.All:
                    indexes = new int[2];
                    for (int i = 0; i < matrix.GetLength(0); i++)
                    {
                        for (int j = 0; j < matrix.GetLength(1); j++)
                        {
                            if (matrix[i, j] == arr[0])
                            {
                                indexes[0] = i;
                                indexes[1] = j;
                                return indexes;
                            }
                        }
                    }
                    break;
            }
            return null;
        }

        static int[] CountOf(int[,] matrix, Sign sign, Direction direction)
        {
            if (direction == Direction.Columns || direction == Direction.Rows)
            {
                int[] count = new int[direction == Direction.Rows ? matrix.GetLength(0) : matrix.GetLength(1)];
                for (int i = 0; i < count.Length; i++)
                {
                    for (int j = 0; j < (direction == Direction.Rows ? matrix.GetLength(1) : matrix.GetLength(0)); j++)
                    {
                        if (sign == Sign.Positive &&
                            matrix[direction == Direction.Rows ? i : j, direction == Direction.Rows ? j : i] > 0)
                        {
                            count[i]++;
                        }
                        else if (sign == Sign.Negative && matrix[direction == Direction.Rows ? i : j,
                                     direction == Direction.Rows ? j : i] < 0)
                        {
                            count[i]++;
                        }
                    }
                }

                return count;
            }

            return null;
        }

        static int[,] MatrixMainDiagonalSort(int[,] matrix)
        {
            int[] arr = new int[matrix.GetLength(0)];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                arr[i] = matrix[i, i];
            }

            BubleSort(arr);
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                matrix[i, i] = arr[i];
            }

            return matrix;
        }
        
        static int[,] GenerateMatrix(int n, int m, (int, int) range)
        {
            int[,] matrix = new int[n, m];
            Random random = new Random();
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = random.Next(range.Item1, range.Item2 + 1);
                }
            }

            return matrix;
        }

        static int[,] GenerateUnitMatrix(int n)
        {
            int[,] matrix = new int[n, n];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = i == j ? 1 : 0;
                }
            }

            return matrix;
        }

        static int[,] SwapPartOfMatrix(int[,] matrix, Direction direction, int index1, int index2)
        {
            if (direction == Direction.Rows)
                for (int i = 0; i < matrix.GetLength(1); i++)
                    (matrix[index1, i], matrix[index2, i]) = (matrix[index2, i], matrix[index1, i]);
            else
                for (int i = 0; i < matrix.GetLength(0); i++)
                    (matrix[i, index1], matrix[i, index2]) = (matrix[i, index2], matrix[i, index1]);

            return matrix;
        }

        static int[,] DeletePartOfMatrix(int[,] matrix, Direction direction, int index)
        {
            int[,] newMatrix;
            if (direction == Direction.Rows)
            {
                newMatrix = new int[matrix.GetLength(0) - 1, matrix.GetLength(1)];
                for (int i = 0; i < index; i++)
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        newMatrix[i, j] = matrix[i, j];
                    }
                }

                for (int i = index; i < newMatrix.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        newMatrix[i, j] = matrix[i + 1, j];
                    }
                }
            }
            else
            {
                newMatrix = new int[matrix.GetLength(0), matrix.GetLength(1) - 1];
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < index; j++)
                    {
                        newMatrix[i, j] = matrix[i, j];
                    }
                }

                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    for (int j = index; j < newMatrix.GetLength(1); j++)
                    {
                        newMatrix[i, j] = matrix[i, j + 1];
                    }
                }
            }

            return newMatrix;
        }

        static void PrintMatrix(int[,] matrix)
        {
            int[] power = new int[matrix.GetLength(1)];
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                int currentPower = 0;

                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    int length = matrix[j, i].ToString().Length;
                    if (length > currentPower)
                        currentPower = length;
                }

                power[i] = currentPower;
            }

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                Console.Write("|");
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    int currentNumber = matrix[i, j];
                    int currentPower = matrix[i, j].ToString().Length - 1;

                    for (int k = 0; k < power[j] - currentPower; k++)
                    {
                        Console.Write(" ");
                    }

                    Console.Write(matrix[i, j]);
                }

                Console.WriteLine(" |");
            }

            Console.WriteLine();
        }
        
        static int[] GenerateArray(int i, (int, int) range)
        {
            int[] vector = new int[i];
            Random random = new Random();
            for (int j = 0; j < vector.Length; j++)
            {
                vector[j] = random.Next(range.Item1, range.Item2 + 1);
            }

            return vector;
        }

        static int IndexOfMaxInArray(int[] arr, int startIndex = 0)
        {
            int iMax = startIndex;
            for (int i = startIndex + 1; i < arr.Length; i++)
            {
                if (arr[iMax] < arr[i])
                {
                    iMax = i;
                }
            }

            return iMax;
        }
        
        static int IndexOfMinInArray(int[] arr, int startIndex = 0)
        {
            int iMin = startIndex;
            for (int i = startIndex + 1; i < arr.Length; i++)
            {
                if (arr[iMin] > arr[i])
                {
                    iMin = i;
                }
            }

            return iMin;
        }

        static void BubleSort(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = 0; j < arr.Length - 1; j++)
                {
                    if (arr[j] > arr[j + 1])
                    {
                        (arr[j], arr[j + 1]) = (arr[j + 1], arr[j]);
                    }
                }
            }
        }
        
        static void PrintArray(Array arr)
        {
            IEnumerator enumerator = arr.GetEnumerator();
            while (enumerator.MoveNext())
                Console.Write($"{enumerator.Current} ");
            Console.WriteLine("\n");
        }

        #endregion
    }

    enum Direction
    {
        Rows,
        Columns,
        Diagonals,
        All
    }

    enum Function
    {
        Min,
        Max
    }

    enum Sign
    {
        Negative,
        Positive
    }
}