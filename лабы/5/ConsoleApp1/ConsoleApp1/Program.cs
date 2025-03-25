using System;
using System.Collections.Generic;

public class files
{
    public string FileName { get; set; }
    public int FileSize { get; set; }
    public DateTime CreationDate { get; set; }
    public TimeSpan CreationTime { get; set; }

    public files(string fileName, int fileSize, DateTime creationDate, TimeSpan creationTime)
    {
        FileName = fileName;
        FileSize = fileSize;
        CreationDate = creationDate;
        CreationTime = creationTime;
    }

    public override string ToString()
    {
        return $"{FileName,-30} {FileSize,10} {CreationDate:dd.MM.yyyy} {CreationTime:hh\\:mm}";
    }
}

class filesList
{
    private List<files> list = new List<files>();

    // Методы сравнения для сортировки
    private static int CompareByName(files x, files y)
    {
        return x.FileName.CompareTo(y.FileName);
    }

    private static int CompareByNameDesc(files x, files y)
    {
        return y.FileName.CompareTo(x.FileName);
    }

    private static int CompareBySize(files x, files y)
    {
        return x.FileSize.CompareTo(y.FileSize);
    }

    private static int CompareBySizeDesc(files x, files y)
    {
        return y.FileSize.CompareTo(x.FileSize);
    }

    private static int CompareByDate(files x, files y)
    {
        int dateCompare = x.CreationDate.CompareTo(y.CreationDate);
        if (dateCompare != 0) return dateCompare;
        return x.CreationTime.CompareTo(y.CreationTime);
    }

    private static int CompareByDateDesc(files x, files y)
    {
        int dateCompare = y.CreationDate.CompareTo(x.CreationDate);
        if (dateCompare != 0) return dateCompare;
        return y.CreationTime.CompareTo(x.CreationTime);
    }

    public void AddFile(files file)
    {
        // Проверка на уникальность имени файла
        foreach (files f in list)
        {
            if (f.FileName.Equals(file.FileName, StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException("Файл с таким именем уже существует.");
            }
        }
        list.Add(file);
    }

    public void DisplayAllFiles()
    {
        if (list.Count == 0)
        {
            Console.WriteLine("Список файлов пуст.");
            return;
        }

        Console.WriteLine($"{"Имя файла",-30} {"Размер",10} {"Дата создания",12} {"Время",8}");
        Console.WriteLine(new string('-', 62));
        foreach (var file in list)
        {
            Console.WriteLine(file);
        }
    }

    public int GetCount()
    {
        return list.Count;
    }

    public void SortFiles(int fieldChoice, bool ascending = true)
    {
        switch (fieldChoice)
        {
            case 1: // По имени
                if (ascending)
                    list.Sort(CompareByName);
                else
                    list.Sort(CompareByNameDesc);
                break;
            case 2: // По размеру
                if (ascending)
                    list.Sort(CompareBySize);
                else
                    list.Sort(CompareBySizeDesc);
                break;
            case 3: // По дате создания
                if (ascending)
                    list.Sort(CompareByDate);
                else
                    list.Sort(CompareByDateDesc);
                break;
            default:
                throw new ArgumentException("Неверный выбор поля для сортировки.");
        }
    }

    public files GetFileByIndex(int index)
    {
        if (index < 0 || index >= list.Count)
        {
            throw new IndexOutOfRangeException("Индекс выходит за пределы списка.");
        }
        return list[index];
    }

    public void UpdateFileByIndex(int index, files newFile)
    {
        if (index < 0 || index >= list.Count)
        {
            throw new IndexOutOfRangeException("Индекс выходит за пределы списка.");
        }

        // Проверка на уникальность имени файла (кроме текущего файла)
        for (int i = 0; i < list.Count; i++)
        {
            if (i != index && list[i].FileName.Equals(newFile.FileName, StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException("Файл с таким именем уже существует.");
            }
        }

        list[index] = newFile;
    }
}

public class MyApplication
{
    private filesList fileList = new filesList();

    public void Run()
    {
        while (true)
        {
            try
            {
                Console.WriteLine("\nМеню:");
                Console.WriteLine("1. Добавить файл");
                Console.WriteLine("2. Показать все файлы");
                Console.WriteLine("3. Сортировать файлы");
                Console.WriteLine("4. Получить файл по индексу");
                Console.WriteLine("5. Обновить файл по индексу");
                Console.WriteLine("6. Выход");
                Console.Write("Выберите опцию: ");

                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        AddFile();
                        break;
                    case 2:
                        fileList.DisplayAllFiles();
                        break;
                    case 3:
                        SortFiles();
                        break;
                    case 4:
                        GetFileByIndex();
                        break;
                    case 5:
                        UpdateFileByIndex();
                        break;
                    case 6:
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Ошибка: Введено нечисловое значение, когда ожидалось число.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }

    private void AddFile()
    {
        Console.WriteLine("\nДобавление нового файла:");
        Console.Write("Введите имя файла: ");
        string fileName = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(fileName) || fileName.Length > 30)
        {
            throw new ArgumentException("Имя файла не может быть пустым или превышать 30 символов.");
        }

        Console.Write("Введите размер файла: ");
        int fileSize = int.Parse(Console.ReadLine());
        if (fileSize <= 0)
        {
            throw new ArgumentException("Размер файла должен быть положительным числом.");
        }

        Console.Write("Введите дату создания (дд.мм.гггг): ");
        DateTime creationDate = DateTime.ParseExact(Console.ReadLine(), "dd.MM.yyyy", null);

        Console.Write("Введите время создания (чч:мм): ");
        TimeSpan creationTime = TimeSpan.ParseExact(Console.ReadLine(), "hh\\:mm", null);

        files newFile = new files(fileName, fileSize, creationDate, creationTime);
        fileList.AddFile(newFile);
        Console.WriteLine("Файл успешно добавлен.");
    }

    private void SortFiles()
    {
        if (fileList.GetCount() == 0)
        {
            Console.WriteLine("Список файлов пуст. Нечего сортировать.");
            return;
        }

        Console.WriteLine("\nПоля для сортировки:");
        Console.WriteLine("1. По имени");
        Console.WriteLine("2. По размеру");
        Console.WriteLine("3. По дате создания");
        Console.Write("Выберите поле для сортировки: ");
        int fieldChoice = int.Parse(Console.ReadLine());

        Console.Write("Сортировать по возрастанию? (y/n): ");
        bool ascending = Console.ReadLine().ToLower() == "y";

        fileList.SortFiles(fieldChoice, ascending);
        Console.WriteLine("Файлы отсортированы.");
        fileList.DisplayAllFiles();
    }

    private void GetFileByIndex()
    {
        if (fileList.GetCount() == 0)
        {
            Console.WriteLine("Список файлов пуст.");
            return;
        }

        Console.Write($"Введите индекс файла (0-{fileList.GetCount() - 1}): ");
        int index = int.Parse(Console.ReadLine());

        files file = fileList.GetFileByIndex(index);
        Console.WriteLine("\nНайденный файл:");
        Console.WriteLine($"{"Имя файла",-30} {"Размер",10} {"Дата создания",12} {"Время",8}");
        Console.WriteLine(new string('-', 62));
        Console.WriteLine(file);
    }

    private void UpdateFileByIndex()
    {
        if (fileList.GetCount() == 0)
        {
            Console.WriteLine("Список файлов пуст. Нечего обновлять.");
            return;
        }

        Console.Write($"Введите индекс файла для обновления (0-{fileList.GetCount() - 1}): ");
        int index = int.Parse(Console.ReadLine());

        // Получаем текущий файл для отображения текущих значений
        files currentFile = fileList.GetFileByIndex(index);
        Console.WriteLine("\nТекущие данные файла:");
        Console.WriteLine(currentFile);

        Console.WriteLine("\nВведите новые данные:");
        Console.Write("Введите имя файла: ");
        string fileName = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(fileName) || fileName.Length > 30)
        {
            throw new ArgumentException("Имя файла не может быть пустым или превышать 30 символов.");
        }

        Console.Write("Введите размер файла: ");
        int fileSize = int.Parse(Console.ReadLine());
        if (fileSize <= 0)
        {
            throw new ArgumentException("Размер файла должен быть положительным числом.");
        }

        Console.Write("Введите дату создания (дд.мм.гггг): ");
        DateTime creationDate = DateTime.ParseExact(Console.ReadLine(), "dd.MM.yyyy", null);

        Console.Write("Введите время создания (чч:мм): ");
        TimeSpan creationTime = TimeSpan.ParseExact(Console.ReadLine(), "hh\\:mm", null);

        files updatedFile = new files(fileName, fileSize, creationDate, creationTime);
        fileList.UpdateFileByIndex(index, updatedFile);
        Console.WriteLine("Файл успешно обновлен.");
    }
}

class Program
{
    static void Main()
    {
        MyApplication app = new MyApplication();
        app.Run();
    }
}