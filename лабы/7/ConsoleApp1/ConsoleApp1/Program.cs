using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\nВыберите задание:");
            Console.WriteLine("1. Сформировать строку из 10 символов (четные позиции - цифры, нечетные - буквы)");
            Console.WriteLine("2. Поменять местами слова в предложении");
            Console.WriteLine("3. Переписать слова в предложениях в обратном порядке");
            Console.WriteLine("0. Выход");
            Console.Write("Ваш выбор: ");

            int choice;
            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Некорректный ввод. Попробуйте снова.");
                continue;
            }

            switch (choice)
            {
                case 1:
                    Task1();
                    break;
                case 2:
                    Task2();
                    break;
                case 3:
                    Task3();
                    break;
                case 0:
                    return;
                default:
                    Console.WriteLine("Неверный выбор. Попробуйте снова.");
                    break;
            }
        }
    }

    static void Task1()
    {
        Random random = new Random();
        StringBuilder result = new StringBuilder(10);

        for (int i = 0; i < 10; i++)
        {
            if (i % 2 == 0) // Нечетная позиция (индексация с 0)
            {
                char letter = (char)random.Next('a', 'z' + 1);
                result.Append(letter);
            }
            else // Четная позиция
            {
                int evenDigit = random.Next(0, 5) * 2;
                result.Append(evenDigit);
            }
        }

        Console.WriteLine("\nРезультат: " + result.ToString());
    }

    static void Task2()
    {
        Console.WriteLine("\nВведите предложение:");
        string sentence = Console.ReadLine();

        Console.WriteLine("Введите номер K (начинается с 1):");
        int K = int.Parse(Console.ReadLine());

        Console.WriteLine("Введите номер N (начинается с 1):");
        int N = int.Parse(Console.ReadLine());

        string[] words = sentence.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        if (words.Length == 0)
        {
            Console.WriteLine("Предложение не содержит слов!");
            return;
        }

        if (K < 1 || K > words.Length || N < 1 || N > words.Length)
        {
            Console.WriteLine($"Ошибка: K и N должны быть от 1 до {words.Length}");
            return;
        }

        if (K != 1)
        {
            string temp = words[0];
            words[0] = words[K - 1];
            words[K - 1] = temp;
        }

        if (N != words.Length)
        {
            string temp = words[words.Length - 1];
            words[words.Length - 1] = words[N - 1];
            words[N - 1] = temp;
        }

        string result = string.Join(" ", words);

        Console.WriteLine("\nРезультат:");
        Console.WriteLine(result);
    }

    static void Task3()
    {
        Console.WriteLine("Введите текст (несколько предложений):");
        string text = Console.ReadLine();

        string[] sentences = text.Split(new[] { '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);

        StringBuilder result = new StringBuilder();

        foreach (string sentence in sentences)
        {
            if (string.IsNullOrWhiteSpace(sentence))
                continue;

            string[] words = sentence.Split(new[] { ' ', ',', ';', ':', '-', '(', ')' }, StringSplitOptions.RemoveEmptyEntries);

            Array.Reverse(words);

            string reversedSentence = string.Join(" ", words).Trim();

            result.Append(reversedSentence);
            result.Append(". ");
        }

        Console.WriteLine("\nРезультат:");
        Console.WriteLine(result.ToString().Trim());
    }
}