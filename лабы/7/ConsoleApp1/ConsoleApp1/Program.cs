using System;
using System.Linq;

class Program
{
    static void Main()
    {
        Console.WriteLine("Введите предложение:");
        string sentence = Console.ReadLine();

        Console.WriteLine("Введите номер K (начинается с 1):");
        int K = int.Parse(Console.ReadLine());

        Console.WriteLine("Введите номер N (начинается с 1):");
        int N = int.Parse(Console.ReadLine());

        // Разбиваем предложение на слова (учитываем несколько пробелов)
        string[] words = sentence.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        if (words.Length == 0)
        {
            Console.WriteLine("Предложение не содержит слов!");
            return;
        }

        // Проверяем корректность введенных K и N
        if (K < 1 || K > words.Length || N < 1 || N > words.Length)
        {
            Console.WriteLine($"Ошибка: K и N должны быть от 1 до {words.Length}");
            return;
        }

        // Меняем первое слово (индекс 0) с K-ым (индекс K-1)
        if (K != 1) // Если K не равно 1, иначе менять не нужно
        {
            string temp = words[0];
            words[0] = words[K - 1];
            words[K - 1] = temp;
        }

        // Меняем последнее слово (индекс words.Length-1) с N-ым (индекс N-1)
        if (N != words.Length) // Если N не равно последнему индексу, иначе менять не нужно
        {
            string temp = words[words.Length - 1];
            words[words.Length - 1] = words[N - 1];
            words[N - 1] = temp;
        }

        // Собираем слова обратно в предложение
        string result = string.Join(" ", words);

        Console.WriteLine("Результат:");
        Console.WriteLine(result);
    }
}