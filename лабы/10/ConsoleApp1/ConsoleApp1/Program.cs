using System;
using System.Collections;
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

public class FilesCollection : IList<files>, IEnumerator<files>, IComparer<files>
{
    private List<files> filesList = new List<files>();
    private int position = -1;

    // Перечисление для направления сортировки
    public enum SortDirection
    {
        Ascending,
        Descending
    }

    // Перечисление для поля сортировки
    public enum SortField
    {
        FileName,
        FileSize,
        CreationDate,
        CreationTime
    }

    // Реализация IList<files>
    public files this[int index]
    {
        get => filesList[index];
        set => filesList[index] = value;
    }

    public int Count => filesList.Count;

    public bool IsReadOnly => false;

    public void Add(files item)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item));

        if (filesList.Exists(f => f.FileName == item.FileName))
            throw new ArgumentException("File with this name already exists.");

        filesList.Add(item);
    }

    public void Clear()
    {
        filesList.Clear();
    }

    public bool Contains(files item)
    {
        return filesList.Contains(item);
    }

    public void CopyTo(files[] array, int arrayIndex)
    {
        filesList.CopyTo(array, arrayIndex);
    }

    public IEnumerator<files> GetEnumerator()
    {
        return this;
    }

    public int IndexOf(files item)
    {
        return filesList.IndexOf(item);
    }

    public void Insert(int index, files item)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item));

        if (filesList.Exists(f => f.FileName == item.FileName))
            throw new ArgumentException("File with this name already exists.");

        filesList.Insert(index, item);
    }

    public bool Remove(files item)
    {
        return filesList.Remove(item);
    }

    public void RemoveAt(int index)
    {
        filesList.RemoveAt(index);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this;
    }

    // Реализация IEnumerator<files>
    public files Current => filesList[position];

    object IEnumerator.Current => Current;

    public void Dispose()
    {
        Reset();
    }

    public bool MoveNext()
    {
        position++;
        return position < filesList.Count;
    }

    public void Reset()
    {
        position = -1;
    }

    // Реализация IComparer<files>
    public int Compare(files x, files y)
    {
        throw new NotImplementedException("Use the overload with SortField and SortDirection");
    }

    public int Compare(files x, files y, SortField field, SortDirection direction)
    {
        if (x == null && y == null) return 0;
        if (x == null) return (direction == SortDirection.Ascending) ? -1 : 1;
        if (y == null) return (direction == SortDirection.Ascending) ? 1 : -1;

        int result;
        switch (field)
        {
            case SortField.FileName:
                result = string.Compare(x.FileName, y.FileName, StringComparison.Ordinal);
                break;
            case SortField.FileSize:
                result = x.FileSize.CompareTo(y.FileSize);
                break;
            case SortField.CreationDate:
                result = x.CreationDate.CompareTo(y.CreationDate);
                break;
            case SortField.CreationTime:
                result = x.CreationTime.CompareTo(y.CreationTime);
                break;
            default:
                throw new ArgumentException("Invalid sort field");
        }

        return (direction == SortDirection.Ascending) ? result : -result;
    }

    public void Sort(SortField field, SortDirection direction)
    {
        filesList.Sort((x, y) => Compare(x, y, field, direction));
    }

    // Дополнительный метод для удобного вывода
    public void PrintAll()
    {
        Console.WriteLine($"{"Имя файла",-30} {"Размер",10} {"Дата",10} {"Время",5}");
        Console.WriteLine(new string('-', 62));
        foreach (var file in filesList)
        {
            Console.WriteLine(file);
        }
    }
}

class Program
{
    static void Main()
    {
        // Создание коллекции
        var collection = new FilesCollection();

        // Добавление файлов
        collection.Add(new files("document.txt", 1024, new DateTime(2023, 1, 15), new TimeSpan(10, 30, 0)));
        collection.Add(new files("image.jpg", 204800, new DateTime(2023, 2, 20), new TimeSpan(15, 45, 0)));
        collection.Add(new files("report.pdf", 512000, new DateTime(2023, 1, 10), new TimeSpan(9, 15, 0)));
        collection.Add(new files("presentation.pptx", 307200, new DateTime(2023, 3, 5), new TimeSpan(14, 0, 0)));

        Console.WriteLine("Исходная коллекция:");
        collection.PrintAll();
        Console.WriteLine();

        // Сортировка по имени (по возрастанию)
        collection.Sort(FilesCollection.SortField.FileName, FilesCollection.SortDirection.Ascending);
        Console.WriteLine("Sorted by FileName (Ascending):");
        collection.PrintAll();
        Console.WriteLine();

        // Сортировка по размеру (по убыванию)
        collection.Sort(FilesCollection.SortField.FileSize, FilesCollection.SortDirection.Descending);
        Console.WriteLine("Sorted by FileSize (Descending):");
        collection.PrintAll();
        Console.WriteLine();

        // Сортировка по дате создания (по возрастанию)
        collection.Sort(FilesCollection.SortField.CreationDate, FilesCollection.SortDirection.Ascending);
        Console.WriteLine("Sorted by CreationDate (Ascending):");
        collection.PrintAll();
        Console.WriteLine();

        // Сортировка по времени создания (по убыванию)
        collection.Sort(FilesCollection.SortField.CreationTime, FilesCollection.SortDirection.Descending);
        Console.WriteLine("Sorted by CreationTime (Descending):");
        collection.PrintAll();
        Console.WriteLine();

        // Демонстрация других методов
        Console.WriteLine($"Contains 'document.txt': {collection.Contains(new files("document.txt", 0, DateTime.Now, TimeSpan.Zero))}");
        Console.WriteLine($"Index of 'image.jpg': {collection.IndexOf(collection.FirstOrDefault(f => f.FileName == "image.jpg"))}");
        Console.WriteLine();

        // Удаление файла
        collection.RemoveAt(0);
        Console.WriteLine("After removing first item:");
        collection.PrintAll();
    }
}