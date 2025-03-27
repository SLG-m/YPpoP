using System;
using System.Text;

class Program
{
    static void Main()
    {
        Console.WriteLine("Введите текст (несколько предложений):");
        string text = Console.ReadLine();

        // Разделяем текст на предложения (по знакам .!? и т.д.)
        string[] sentences = text.Split(new[] { '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);

        StringBuilder result = new StringBuilder();

        foreach (string sentence in sentences)
        {
            if (string.IsNullOrWhiteSpace(sentence))
                continue;

            // Разбиваем предложение на слова
            string[] words = sentence.Split(new[] { ' ', ',', ';', ':', '-', '(', ')' }, StringSplitOptions.RemoveEmptyEntries);

            // Переворачиваем порядок слов
            Array.Reverse(words);

            // Собираем предложение заново
            string reversedSentence = string.Join(" ", words).Trim();

            // Добавляем в результат с сохранением пунктуации
            result.Append(reversedSentence);
            result.Append(". "); // Добавляем точку и пробел после каждого предложения
        }

        Console.WriteLine("\nРезультат:");
        Console.WriteLine(result.ToString().Trim());
    }
}