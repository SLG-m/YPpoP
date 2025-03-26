using System;
using System.Text;

class Program
{
    static void Main()
    {
        Random random = new Random();
        StringBuilder result = new StringBuilder(10);

        for (int i = 0; i < 10; i++)
        {
            if (i % 2 == 0) // Нечетная позиция (индексация с 0)
            {
                // Добавляем случайную букву (английскую)
                char letter = (char)random.Next('a', 'z' + 1);
                result.Append(letter);
            }
            else // Четная позиция
            {
                // Добавляем случайную четную цифру
                int evenDigit = random.Next(0, 5) * 2; // 0, 2, 4, 6, 8
                result.Append(evenDigit);
            }
        }

        Console.WriteLine("Результат: " + result.ToString());
    }
}