using System;
using System.Text.RegularExpressions;
class Program
{
    static void Main(string[] args)
    {
        Console.BackgroundColor = ConsoleColor.White;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.Clear();

        try
        {
            string filePath = "text.txt";
            string text = File.ReadAllText(filePath);

            Console.WriteLine("Исходный текст:");
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(text);

            Console.WriteLine("\nОбработанный текст:");
            ProcessText(text);
        }
        catch (FileNotFoundException)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Файл text.txt не найден в корневой папке приложения.");
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Произошла ошибка: {ex.Message}");
        }
        finally
        {
            Console.ResetColor();
        }
        Console.WriteLine("\nНажмите любую клавишу для выхода...");
        Console.ReadKey();
    }

    static void ProcessText(string text)
    {
        Regex acronymRegex = new Regex(@"\b[A-ZА-Я]{2,}\b");
        Regex capitalizedRegex = new Regex(@"\b[A-ZА-Я][a-zа-я]+\b");
        Regex numberRegex = new Regex(@"\b\d+\.?\d*\b");

        int lastPos = 0;

        var matches = acronymRegex.Matches(text).Cast<Match>()
            .Concat(capitalizedRegex.Matches(text).Cast<Match>())
            .Concat(numberRegex.Matches(text).Cast<Match>())
            .OrderBy(m => m.Index)
            .ToList();

        foreach (var match in matches)
        {
            if (match.Index > lastPos)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write(text.Substring(lastPos, match.Index - lastPos));
            }

            if (acronymRegex.IsMatch(match.Value))
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else if (capitalizedRegex.IsMatch(match.Value))
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else if (numberRegex.IsMatch(match.Value))
            {
                Console.ForegroundColor = ConsoleColor.Blue;
            }
            Console.Write(match.Value);
            lastPos = match.Index + match.Length;
        }

        if (lastPos < text.Length)
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(text.Substring(lastPos));
        }
    }
}