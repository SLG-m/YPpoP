using System;

class Program
{
    static void Main()
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

    // Метод для замены элементов столбца
    static void ReplaceColumn(int[,] array, int columnIndex, int newValue)
    {
        int rows = array.GetLength(0);
        for (int i = 0; i < rows; i++)
        {
            array[i, columnIndex] = newValue;
        }
    }

    // Метод для поиска минимального элемента
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

    // Метод для вывода массива
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