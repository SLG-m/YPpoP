using System;

class Program
{
    static void Main()
    {
        // Пример вещественного массива
        double[] array = { 1.5, 2.3, 3.7, 4.1, 5.9, 6.2, 7.4, 8.0, 9.6, 10.8 };

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
        Console.WriteLine($"Среднее арифметическое: {average}");
        Console.WriteLine($"Количество элементов меньше среднего: {count}");
    }
}