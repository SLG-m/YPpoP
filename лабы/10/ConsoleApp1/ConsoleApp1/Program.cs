using System;
using System.Collections;
using System.Collections.Generic;

public class FileItem : IEquatable<FileItem>
{
    public string FileName { get; set; }
    public int FileSize { get; set; }
    public DateTime CreationDate { get; set; }
    public TimeSpan CreationTime { get; set; }

    public FileItem(string fileName, int fileSize, DateTime creationDate, TimeSpan creationTime)
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

    public bool Equals(FileItem other)
    {
        if (other == null) return false;
        return FileName == other.FileName;
    }

    public override bool Equals(object obj) => Equals(obj as FileItem);
    public override int GetHashCode() => FileName.GetHashCode();
}

public class FileCollection : IList<FileItem>, IEnumerator<FileItem>, IComparer<FileItem>
{
    private List<FileItem> files = new List<FileItem>();
    private int position = -1;

    public enum SortDirection 
    {
        Ascending,
        Descending 
    }
    public enum SortField 
    { 
        FileName, 
        FileSize, 
        CreationDate, 
        CreationTime 
    }

    // IList implementation
    public FileItem this[int index] { get => files[index]; set => files[index] = value; }
    public int Count => files.Count;
    public bool IsReadOnly => false;

    public void Add(FileItem item)
    {
        if (item == null) throw new ArgumentNullException("Элемент не может быть null");
        if (files.Contains(item)) throw new ArgumentException("Файл с таким именем уже существует.");
        files.Add(item);
    }

    public void Clear() => files.Clear();
    public bool Contains(FileItem item) => files.Contains(item);
    public void CopyTo(FileItem[] array, int arrayIndex) => files.CopyTo(array, arrayIndex);
    public IEnumerator<FileItem> GetEnumerator() => this;
    public int IndexOf(FileItem item) => files.IndexOf(item);

    public void Insert(int index, FileItem item)
    {
        if (item == null) throw new ArgumentNullException("Элемент не может быть null");
        if (files.Contains(item)) throw new ArgumentException("Файл с таким именем уже существует.");
        files.Insert(index, item);
    }

    public bool Remove(FileItem item) => files.Remove(item);
    public void RemoveAt(int index) => files.RemoveAt(index);
    IEnumerator IEnumerable.GetEnumerator() => this;

    // IEnumerator implementation
    public FileItem Current => files[position];
    object IEnumerator.Current => Current;
    public void Dispose() => Reset();
    public bool MoveNext() => ++position < files.Count;
    public void Reset() => position = -1;

    // IComparer implementation
    public int Compare(FileItem x, FileItem y) => Compare(x, y, SortField.FileName, SortDirection.Ascending);

    public int Compare(FileItem x, FileItem y, SortField field, SortDirection direction)
    {
        if (x == null && y == null) return 0;
        if (x == null) return direction == SortDirection.Ascending ? -1 : 1;
        if (y == null) return direction == SortDirection.Ascending ? 1 : -1;

        int result = field switch
        {
            SortField.FileName => string.Compare(x.FileName, y.FileName, StringComparison.Ordinal),
            SortField.FileSize => x.FileSize.CompareTo(y.FileSize),
            SortField.CreationDate => x.CreationDate.CompareTo(y.CreationDate),
            SortField.CreationTime => x.CreationTime.CompareTo(y.CreationTime),
            _ => 0
        };

        return direction == SortDirection.Ascending ? result : -result;
    }

    public void Sort(SortField field, SortDirection direction) => files.Sort((x, y) => Compare(x, y, field, direction));

    public void PrintAll()
    {
        Console.WriteLine($"{"Имя файла",-30} {"Размер",10} {"Дата",10} {"Время",5}");
        Console.WriteLine(new string('-', 60));
        foreach (var file in files)
        {
            Console.WriteLine(file);
        }
    }
}

class Program
{
    static void Main()
    {
        var collection = new FileCollection();

        // Добавление тестовых данных
        collection.Add(new FileItem("document.txt", 1024, new DateTime(2023, 1, 15), new TimeSpan(10, 30, 0)));
        collection.Add(new FileItem("image.jpg", 204800, new DateTime(2023, 2, 20), new TimeSpan(15, 45, 0)));
        collection.Add(new FileItem("report.pdf", 512000, new DateTime(2023, 1, 10), new TimeSpan(9, 15, 0)));
        collection.Add(new FileItem("presentation.pptx", 307200, new DateTime(2023, 3, 5), new TimeSpan(14, 0, 0)));

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Текущая коллекция:");
            collection.PrintAll();

            Console.WriteLine("\nМеню:");
            Console.WriteLine("1. Добавить файл");
            Console.WriteLine("2. Удалить файл");
            Console.WriteLine("3. Сортировать коллекцию");
            Console.WriteLine("4. Выход");
            Console.Write("Выберите действие: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddFile(collection);
                    break;
                case "2":
                    RemoveFile(collection);
                    break;
                case "3":
                    SortCollection(collection);
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Неверный выбор! Нажмите любую клавишу для продолжения...");
                    Console.ReadKey();
                    break;
            }
        }
    }

    static void AddFile(FileCollection collection)
    {
        try
        {
            Console.Write("Введите имя файла: ");
            var name = Console.ReadLine();

            Console.Write("Введите размер файла: ");
            var size = int.Parse(Console.ReadLine());

            Console.Write("Введите дату создания (дд.мм.гггг): ");
            var date = DateTime.ParseExact(Console.ReadLine(), "dd.MM.yyyy", null);

            Console.Write("Введите время создания (чч:мм): ");
            var time = TimeSpan.ParseExact(Console.ReadLine(), "hh\\:mm", null);

            collection.Add(new FileItem(name, size, date, time));
            Console.WriteLine("Файл успешно добавлен!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
        Console.WriteLine("Нажмите любую клавишу для продолжения...");
        Console.ReadKey();
    }

    static void RemoveFile(FileCollection collection)
    {
        Console.Write("Введите индекс файла для удаления: ");
        if (int.TryParse(Console.ReadLine(), out int index) && index >= 0 && index < collection.Count)
        {
            collection.RemoveAt(index);
            Console.WriteLine("Файл успешно удален!");
        }
        else
        {
            Console.WriteLine("Неверный индекс!");
        }
        Console.WriteLine("Нажмите любую клавишу для продолжения...");
        Console.ReadKey();
    }

    static void SortCollection(FileCollection collection)
    {
        Console.WriteLine("\nВыберите поле для сортировки:");
        Console.WriteLine("1. По имени");
        Console.WriteLine("2. По размеру");
        Console.WriteLine("3. По дате создания");
        Console.WriteLine("4. По времени создания");
        Console.Write("Ваш выбор: ");

        if (!Enum.TryParse(Console.ReadLine(), out FileCollection.SortField field))
        {
            field = FileCollection.SortField.FileName;
        }

        Console.WriteLine("\nВыберите направление сортировки:");
        Console.WriteLine("1. По возрастанию");
        Console.WriteLine("2. По убыванию");
        Console.Write("Ваш выбор: ");

        var directionChoice = Console.ReadLine();
        var direction = directionChoice == "2" ? FileCollection.SortDirection.Descending : FileCollection.SortDirection.Ascending;

        collection.Sort(field, direction);
        Console.WriteLine("\nКоллекция отсортирована!");
        Console.WriteLine("Нажмите любую клавишу для продолжения...");
        Console.ReadKey();
    }
}