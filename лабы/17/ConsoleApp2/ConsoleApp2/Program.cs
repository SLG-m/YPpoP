using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main()
    {
        string fileName = "numbers.txt";

        string filePath = Path.Combine(Environment.CurrentDirectory, fileName);

        Console.WriteLine("Выберите направление сортировки:");
        Console.WriteLine("1 - По возрастанию");
        Console.WriteLine("2 - По убыванию");
        int sortDirection = int.Parse(Console.ReadLine());

        try
        {
            // Чтение всех строк из файла
            string[] fileLines = File.ReadAllLines(filePath);
            List<int> numbers = new List<int>();

            // Разбор чисел из всех строк
            foreach (string line in fileLines)
            {
                string[] tokens = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string token in tokens)
                {
                    if (int.TryParse(token, out int number) && number >= 0)
                    {
                        numbers.Add(number);
                    }
                }
            }

            if (sortDirection == 2)
            {
                // Сортировка по убыванию 
                for (int i = 0; i < numbers.Count - 1; i++)
                {
                    for (int j = 0; j < numbers.Count - i - 1; j++)
                    {
                        if (numbers[j] < numbers[j + 1])
                        {
                            int temp = numbers[j];
                            numbers[j] = numbers[j + 1];
                            numbers[j + 1] = temp;
                        }
                    }
                }
            }
            else
            {
                // Сортировка по возрастанию
                for (int i = 0; i < numbers.Count - 1; i++)
                {
                    for (int j = 0; j < numbers.Count - i - 1; j++)
                    {
                        if (numbers[j] > numbers[j + 1])
                        {
                            int temp = numbers[j];
                            numbers[j] = numbers[j + 1];
                            numbers[j + 1] = temp;
                        }
                    }
                }
            }

            // Запись обратно в файл
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                for (int i = 0; i < numbers.Count; i++)
                {
                    if (i > 0) writer.Write(" ");
                    writer.Write(numbers[i]);
                }
            }

            Console.WriteLine("Файл успешно обработан!");
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Файл не найден! Убедитесь, что он находится в той же папке, что и программа.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }

        Console.ReadKey();
    }
}