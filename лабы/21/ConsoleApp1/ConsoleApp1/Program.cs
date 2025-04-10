using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;

class Program
{
    static double[,] matrix;
    static int firstMaxColumn;
    static int secondMaxColumn;
    static double k;

    static void Main(string[] args)
    {
        try
        {
            Random rand = new Random();

            // Ввод размеров матрицы с обработкой ошибок
            int N = 0, M = 0;
            bool validInput = false;
            while (!validInput)
            {
                try
                {
                    Console.Write("Введите количество строк N: ");
                    N = int.Parse(Console.ReadLine());
                    Console.Write("Введите количество столбцов M: ");
                    M = int.Parse(Console.ReadLine());
                    if (N <= 0 || M <= 0) throw new ArgumentException("Размеры матрицы должны быть положительными числами");
                    validInput = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка ввода: {ex.Message}. Попробуйте снова.");
                }
            }

            // Генерация матрицы
            matrix = new double[N, M];
            Parallel.For(0, N, i =>
            {
                Parallel.For(0, M, j =>
                {
                    matrix[i, j] = rand.Next(1, 10);
                });
            });

            Console.WriteLine("\nИсходная матрица:");
            PrintMatrix(matrix);

            // Вычисление сумм столбцов с использованием PLINQ
            var columnSums = new ConcurrentBag<(int column, double sum)>();

            Parallel.For(0, M, j =>
            {
                double sum = 0;
                for (int i = 0; i < N; i++)
                {
                    sum += matrix[i, j];
                }
                columnSums.Add((j, sum));
            });

            // Поиск двух столбцов с максимальной суммой
            var sortedColumns = columnSums.OrderByDescending(x => x.sum).Take(2).ToList();
            firstMaxColumn = sortedColumns[0].column;
            secondMaxColumn = sortedColumns[1].column;

            Console.WriteLine($"\nСтолбцы с максимальной суммой: {firstMaxColumn} и {secondMaxColumn}");

            // Ввод K с обработкой ошибок
            validInput = false;
            while (!validInput)
            {
                try
                {
                    Console.Write("\nВведите значение K: ");
                    k = double.Parse(Console.ReadLine());
                    validInput = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка ввода: {ex.Message}. Попробуйте снова.");
                }
            }

            // Создание и выполнение задачи для обработки матрицы
            var processTask = Task.Run(() =>
            {
                try
                {
                    Parallel.For(0, N, i =>
                    {
                        Console.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}] Поток {Task.CurrentId}: Обработка строки {i}");
                        Thread.Sleep(150); // Имитация работы
                        matrix[i, firstMaxColumn] -= k;
                        matrix[i, secondMaxColumn] -= k;
                    });
                }
                catch (AggregateException ae)
                {
                    foreach (var e in ae.InnerExceptions)
                    {
                        Console.WriteLine($"Ошибка при обработке матрицы: {e.Message}");
                    }             
                }
            });

            // Ожидание завершения задачи и вывод результата
            processTask.Wait();

            if (processTask.IsFaulted)
            {
                Console.WriteLine("\nПри обработке матрицы произошли ошибки:");
                foreach (var ex in processTask.Exception.InnerExceptions)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                Console.WriteLine("\nРезультирующая матрица:");
                PrintMatrix(matrix);
            }

            Console.WriteLine("Нажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Критическая ошибка: {ex.Message}");
            Console.ReadKey();
        }
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