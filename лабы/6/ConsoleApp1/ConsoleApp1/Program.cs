using System;

class Program
{
    static void Main()
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
}