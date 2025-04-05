using System;
using System.Threading;

class Program
{
    static double[,] matrix;
    static int firstMaxColumn;
    static int secondMaxColumn;
    static double k;

    static void Main(string[] args)
    {
        Random rand = new Random();

        // Ввод размеров матрицы
        Console.Write("Введите количество строк N: ");
        int N = int.Parse(Console.ReadLine());
        Console.Write("Введите количество столбцов M: ");
        int M = int.Parse(Console.ReadLine());

        // Генерация матрицы
        matrix = new double[N, M];
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < M; j++)
            {
                matrix[i, j] = rand.Next(1, 10);
            }
        }

        Console.WriteLine("\nИсходная матрица:");
        PrintMatrix(matrix);

        // Вычисление сумм столбцов
        double[] columnSums = new double[M];
        for (int j = 0; j < M; j++)
        {
            for (int i = 0; i < N; i++)
            {
                columnSums[j] += matrix[i, j];
            }
        }

        // Поиск двух столбцов с максимальной суммой
        int maxCol1 = 0;
        int maxCol2 = 1;

        if (columnSums[maxCol2] > columnSums[maxCol1])
        {
            (maxCol1, maxCol2) = (maxCol2, maxCol1);
        }

        for (int j = 2; j < M; j++)
        {
            if (columnSums[j] > columnSums[maxCol1])
            {
                maxCol2 = maxCol1;
                maxCol1 = j;
            }
            else if (columnSums[j] > columnSums[maxCol2])
            {
                maxCol2 = j;
            }
        }

        firstMaxColumn = maxCol1;
        secondMaxColumn = maxCol2;

        Console.WriteLine($"\nСтолбцы с максимальной суммой: {firstMaxColumn} и {secondMaxColumn}");

        // Ввод K
        Console.Write("\nВведите значение K: ");
        k = double.Parse(Console.ReadLine());

        // Создание и запуск потоков
        Thread[] threads = new Thread[N];
        for (int i = 0; i < N; i++)
        {
            int row = i;
            threads[i] = new Thread(() => ProcessRow(row));
            threads[i].Name = $"Поток_{row + 1}";
            threads[i].Priority = (row == 0) ? ThreadPriority.Highest : ThreadPriority.Normal;
            threads[i].Start();
        }

        // Ожидание завершения всех потоков
        foreach (Thread thread in threads)
        {
            thread.Join();
        }

        Console.WriteLine("\nРезультирующая матрица:");
        PrintMatrix(matrix);
        Console.ReadKey();
    }

    static void ProcessRow(int row)
    {
        Thread currentThread = Thread.CurrentThread;
        Console.WriteLine(
            $"[{DateTime.Now:HH:mm:ss.fff}] {currentThread.Name}: " +
            $"Состояние: {currentThread.ThreadState}, " +
            $"Приоритет: {currentThread.Priority}"
        );

        Thread.Sleep(150);

        matrix[row, firstMaxColumn] -= k;
        matrix[row, secondMaxColumn] -= k;
    }

    static void PrintMatrix(double[,] matrix)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                Console.Write($"{matrix[i, j]:F1}\t");
            }
            Console.WriteLine();
        }
    }
}