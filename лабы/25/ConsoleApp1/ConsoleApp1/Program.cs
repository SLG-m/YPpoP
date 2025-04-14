using System;
using System.IO;

class Program
{
    static void Main()
    {
        string inputFile = "ships_data.txt";    // исходный файл
        string outputFile = "output.bin";  // бинарный файл

        // Чтение всех байтов из исходного файла
        byte[] fileBytes = File.ReadAllBytes(inputFile);

        // Запись байтов в бинарный файл
        File.WriteAllBytes(outputFile, fileBytes);

        Console.WriteLine("Файл успешно преобразован в бинарный формат.");
    }
}