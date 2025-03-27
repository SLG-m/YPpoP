using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        // Устанавливаем белый фон консоли
        Console.BackgroundColor = ConsoleColor.White;
        Console.Clear();

        try
        {
            // Чтение файла из корневой папки
            string filePath = "text.txt";
            string text = File.ReadAllText(filePath);

            Console.WriteLine("Исходный текст:");
            Console.WriteLine(text);
            Console.WriteLine("\nОбработанный текст:");

            // Обработка текста
            ProcessText(text);
        }
        catch (FileNotFoundException)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Файл text.txt не найден в корневой папке приложения.");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Произошла ошибка: {ex.Message}");
            Console.ResetColor();
        }

        Console.ResetColor();
        Console.WriteLine("\nНажмите любую клавишу для выхода...");
        Console.ReadKey();
    }

    static void ProcessText(string text)
    {
        // Регулярное выражение для аббревиатур (заглавные буквы и цифры, длина 2+ символа)
        Regex acronymRegex = new Regex(@"\b[A-ZА-Я0-9]{2,}\b");
        // Регулярное выражение для слов с заглавной буквы
        Regex capitalizedRegex = new Regex(@"\b[A-ZА-Я][a-zа-я]+\b");
        // Регулярное выражение для чисел (целых и дробных)
        Regex numberRegex = new Regex(@"\b\d+\.?\d*\b");

        int lastPos = 0;

        // Находим все совпадения всех регулярных выражений
        var matches = acronymRegex.Matches(text)
            .Cast<Match>()
            .Concat(capitalizedRegex.Matches(text).Cast<Match>())
            .Concat(numberRegex.Matches(text).Cast<Match>())
            .OrderBy(m => m.Index)
            .ToList();

        foreach (var match in matches)
        {
            // Выводим текст до совпадения обычным цветом
            if (match.Index > lastPos)
            {
                Console.ResetColor();
                Console.Write(text.Substring(lastPos, match.Index - lastPos));
            }

            // Определяем тип совпадения и устанавливаем соответствующий цвет
            if (acronymRegex.IsMatch(match.Value))
            {
                Console.ForegroundColor = ConsoleColor.Red; // Аббревиатуры - красным
            }
            else if (capitalizedRegex.IsMatch(match.Value))
            {
                Console.ForegroundColor = ConsoleColor.Green; // Слова с заглавной буквы - зеленым
            }
            else if (numberRegex.IsMatch(match.Value))
            {
                Console.ForegroundColor = ConsoleColor.Blue; // Числа - синим
            }

            // Выводим совпадение
            Console.Write(match.Value);
            lastPos = match.Index + match.Length;
        }

        // Выводим оставшийся текст обычным цветом
        if (lastPos < text.Length)
        {
            Console.ResetColor();
            Console.Write(text.Substring(lastPos));
        }
    }
}