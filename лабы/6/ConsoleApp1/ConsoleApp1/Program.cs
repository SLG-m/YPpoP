using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== ЗАДАНИЕ 1: Подсчет элементов меньше среднего в вещественном массиве ===");
        Task1();

        Console.WriteLine("\n=== ЗАДАНИЕ 2: Перестановка элементов символьного массива по четным/нечетным позициям ===");
        Task2();

        Console.WriteLine("\n=== ЗАДАНИЕ 3: Работа с двумерным массивом (замена столбца и поиск минимума) ===");
        Task3();
    }

    static void Task1()
    {
        // Пример вещественного массива
        double[] array = { 1.5, 2.3, 3.7, 4.1, 5.9, 6.2, 7.4, 8.0, 9.6, 10.8 };
        Console.WriteLine("Исходный массив: " + string.Join(", ", array));

        // Вычисление среднего арифметического
        double sum = 0;
        foreach (double num in array)
        {
            sum += num;
        }
        double average = sum / array.Length;

        // Подсчет элементов, меньших среднего
        int count = 0;
        foreach (double num in array)
        {
            if (num < average)
            {
                count++;
            }
        }

        // Вывод результата
        Console.WriteLine($"Среднее арифметическое: {average:F2}");
        Console.WriteLine($"Количество элементов меньше среднего: {count}");
    }

    static void Task2()
    {
        char[] array = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };
        Console.WriteLine("Исходный массив: " + string.Join(", ", array));

        // Создаем новый массив для результата
        char[] result = new char[array.Length];
        int evenIndex = 0; // Индекс для четных позиций (1-ая половина)
        int oddIndex = array.Length / 2; // Индекс для нечетных позиций (2-ая половина)

        for (int i = 0; i < array.Length; i++)
        {
            if (i % 2 == 0) // Четная позиция (индексация с 0)
                result[evenIndex++] = array[i];
            else // Нечетная позиция
                result[oddIndex++] = array[i];
        }

        Console.WriteLine("Результат: " + string.Join(", ", result));
    }

    static void Task3()
    {
        // Пример двумерного массива 3x3
        int[,] array = {
            { 1, 2, 3 },
            { 4, 5, 6 },
            { 7, 8, 9 }
        };

        Console.WriteLine("Исходный массив:");
        PrintArray(array);

        // Ввод номера столбца для замены с проверкой
        int columnToReplace;
        while (true)
        {
            Console.Write("\nВведите номер столбца для замены (от 0 до 2): ");
            if (int.TryParse(Console.ReadLine(), out columnToReplace) && columnToReplace >= 0 && columnToReplace < array.GetLength(1))
            {
                break;
            }
            Console.WriteLine("Ошибка! Введите корректный номер столбца.");
        }

        // Ввод нового значения с проверкой
        int newValue;
        while (true)
        {
            Console.Write("Введите новое значение для замены: ");
            if (int.TryParse(Console.ReadLine(), out newValue))
            {
                break;
            }
            Console.WriteLine("Ошибка! Введите целое число.");
        }

        // Замена элементов столбца
        ReplaceColumn(array, columnToReplace, newValue);

        Console.WriteLine($"\nМассив после замены {columnToReplace}-го столбца на {newValue}:");
        PrintArray(array);

        // Поиск минимального элемента
        int minElement = FindMinElement(array);
        Console.WriteLine($"\nМинимальный элемент массива: {minElement}");
    }

    static void ReplaceColumn(int[,] array, int columnIndex, int newValue)
    {
        int rows = array.GetLength(0);
        for (int i = 0; i < rows; i++)
        {
            array[i, columnIndex] = newValue;
        }
    }

    static int FindMinElement(int[,] array)
    {
        int min = array[0, 0];
        int rows = array.GetLength(0);
        int cols = array.GetLength(1);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (array[i, j] < min)
                {
                    min = array[i, j];
                }
            }
        }
        return min;
    }

    static void PrintArray(int[,] array)
    {
        int rows = array.GetLength(0);
        int cols = array.GetLength(1);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Console.Write(array[i, j] + " ");
            }
            Console.WriteLine();
        }
    }
}