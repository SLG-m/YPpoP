using System;
using System.Collections;
using System.Collections.Generic;

// Перечисление для типа диска
public enum DiskType
{
    CD,
    DVD,
    BluRay,
    HDDVD
}

// Перечисление для типа данных на диске с данными
public enum DataType
{
    Software,
    Games,
    Video,
    Audio,
    Data
}

// Перечисление для направления сортировки
public enum SortDirection
{
    Ascending,
    Descending
}

// Перечисление для поля сортировки
public enum SortField
{
    RecordingDate,
    Artist,
    AlbumTitle,
    Duration,
    Description,
    DataSize
}

// Базовый класс Disk
public abstract class Disk
{
    public DiskType Type { get; set; }
    public DateTime RecordingDate { get; set; }

    protected Disk(DiskType type, DateTime recordingDate)
    {
        Type = type;
        RecordingDate = recordingDate;
    }

    // Виртуальные методы
    public virtual void DisplayInfo()
    {
        Console.WriteLine($"Тип диска: {Type}");
        Console.WriteLine($"Дата записи: {RecordingDate.ToShortDateString()}");
    }

    public virtual string GetSummary()
    {
        return $"{Type} диск, записан {RecordingDate.ToShortDateString()}";
    }
}

// Класс для музыкального диска
public class MusicDisk : Disk
{
    public string Artist { get; set; }
    public string AlbumTitle { get; set; }
    public int DurationMinutes { get; set; }

    public MusicDisk(DiskType type, DateTime recordingDate, string artist, string albumTitle, int durationMinutes)
        : base(type, recordingDate)
    {
        Artist = artist;
        AlbumTitle = albumTitle;
        DurationMinutes = durationMinutes;
    }

    public override void DisplayInfo()
    {
        base.DisplayInfo();
        Console.WriteLine($"Исполнитель: {Artist}");
        Console.WriteLine($"Название альбома: {AlbumTitle}");
        Console.WriteLine($"Продолжительность: {DurationMinutes} минут");
    }

    public override string GetSummary()
    {
        return $"{base.GetSummary()}, музыкальный альбом {Artist} - {AlbumTitle}";
    }
}

// Класс для диска с данными
public class DataDisk : Disk
{
    public string Description { get; set; }
    public double DataSizeMB { get; set; }
    public DataType DataType { get; set; }

    public DataDisk(DiskType type, DateTime recordingDate, string description, double dataSizeMB, DataType dataType)
        : base(type, recordingDate)
    {
        Description = description;
        DataSizeMB = dataSizeMB;
        DataType = dataType;
    }

    public override void DisplayInfo()
    {
        base.DisplayInfo();
        Console.WriteLine($"Описание: {Description}");
        Console.WriteLine($"Объем данных: {DataSizeMB} MB");
        Console.WriteLine($"Тип данных: {DataType}");
    }

    public override string GetSummary()
    {
        return $"{base.GetSummary()}, диск с данными ({DataType}), {DataSizeMB} MB";
    }
}

// Класс для сравнения дисков
public class DiskComparer : IComparer<Disk>
{
    public SortField SortField { get; set; }
    public SortDirection SortDirection { get; set; }

    public DiskComparer(SortField sortField, SortDirection sortDirection)
    {
        SortField = sortField;
        SortDirection = sortDirection;
    }

    public int Compare(Disk x, Disk y)
    {
        if (x == null && y == null) return 0;
        if (x == null) return SortDirection == SortDirection.Ascending ? -1 : 1;
        if (y == null) return SortDirection == SortDirection.Ascending ? 1 : -1;

        int result = 0;

        switch (SortField)
        {
            case SortField.RecordingDate:
                result = x.RecordingDate.CompareTo(y.RecordingDate);
                break;
            case SortField.Artist:
                var mx = x as MusicDisk;
                var my = y as MusicDisk;
                result = (mx?.Artist ?? "").CompareTo(my?.Artist ?? "");
                break;
            case SortField.AlbumTitle:
                var mxa = x as MusicDisk;
                var mya = y as MusicDisk;
                result = (mxa?.AlbumTitle ?? "").CompareTo(mya?.AlbumTitle ?? "");
                break;
            case SortField.Duration:
                var mxd = x as MusicDisk;
                var myd = y as MusicDisk;
                result = (mxd?.DurationMinutes ?? 0).CompareTo(myd?.DurationMinutes ?? 0);
                break;
            case SortField.Description:
                var dx = x as DataDisk;
                var dy = y as DataDisk;
                result = (dx?.Description ?? "").CompareTo(dy?.Description ?? "");
                break;
            case SortField.DataSize:
                var ddx = x as DataDisk;
                var ddy = y as DataDisk;
                result = (ddx?.DataSizeMB ?? 0).CompareTo(ddy?.DataSizeMB ?? 0);
                break;
        }

        return SortDirection == SortDirection.Ascending ? result : -result;
    }
}

// Класс-список для хранения коллекции дисков
public class DiskCollection : IEnumerable<Disk>
{
    private List<Disk> disks = [];

    // Добавление диска в коллекцию
    public void Add(Disk disk)
    {
        disks.Add(disk);
    }

    // Индексатор
    public Disk this[int index]
    {
        get
        {
            if (index < 0 || index >= disks.Count)
                throw new IndexOutOfRangeException("Индекс находится вне границ коллекции");
            return disks[index];
        }
        set
        {
            if (index < 0 || index >= disks.Count)
                throw new IndexOutOfRangeException("Индекс находится вне границ коллекции");
            disks[index] = value;
        }
    }

    // Определение типа элемента по индексу
    public string GetDiskType(int index)
    {
        if (index < 0 || index >= disks.Count)
            return "Неверный индекс";

        return disks[index] switch
        {
            MusicDisk => "Музыкальный диск",
            DataDisk => "Диск с данными",
            _ => "Неизвестный тип диска"
        };
    }

    // Вывод коллекции с фильтром
    public void DisplayCollection(string filter = "all")
    {
        if (!disks.Any())
        {
            Console.WriteLine("Коллекция пуста.");
            return;
        }

        foreach (var disk in disks)
        {
            if (filter == "all" ||
                (filter == "music" && disk is MusicDisk) ||
                (filter == "data" && disk is DataDisk))
            {
                disk.DisplayInfo();
                Console.WriteLine(new string('-', 15));
            }
        }
    }

    // Сортировка коллекции
    public void Sort(SortField sortField, SortDirection sortDirection)
    {
        var comparer = new DiskComparer(sortField, sortDirection);
        disks.Sort(comparer);
    }

    // Реализация интерфейса IEnumerable
    public IEnumerator<Disk> GetEnumerator() => disks.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public int Count => disks.Count;
}

// Основной класс программы
class Program
{
    static void Main()
    {
        DiskCollection collection = [];
        bool running = true;

        while (running)
        {
            Console.WriteLine("\nМеню:");
            Console.WriteLine("1. Добавить музыкальный диск");
            Console.WriteLine("2. Добавить диск с данными");
            Console.WriteLine("3. Показать все диски");
            Console.WriteLine("4. Показать только музыкальные диски");
            Console.WriteLine("5. Показать только диски с данными");
            Console.WriteLine("6. Отсортировать коллекцию");
            Console.WriteLine("7. Определить тип диска по индексу");
            Console.WriteLine("8. Выход");
            Console.Write("Выберите действие: ");

            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("Ошибка: введите число от 1 до 8.");
                continue;
            }

            try
            {
                switch (choice)
                {
                    case 1:
                        AddMusicDisk(collection);
                        break;
                    case 2:
                        AddDataDisk(collection);
                        break;
                    case 3:
                        Console.WriteLine("\nВсе диски:");
                        collection.DisplayCollection();
                        break;
                    case 4:
                        Console.WriteLine("\nМузыкальные диски:");
                        collection.DisplayCollection("music");
                        break;
                    case 5:
                        Console.WriteLine("\nДиски с данными:");
                        collection.DisplayCollection("data");
                        break;
                    case 6:
                        SortCollection(collection);
                        break;
                    case 7:
                        GetDiskType(collection);
                        break;
                    case 8:
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Неверный выбор. Введите число от 1 до 8.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
            }
        }
    }

    static void AddMusicDisk(DiskCollection collection)
    {
        Console.WriteLine("\nДобавление музыкального диска:");

        DiskType type;
        while (true)
        {
            Console.Write("Тип диска (0-CD, 1-DVD, 2-BluRay, 3-HDDVD): ");
            if (Enum.TryParse(Console.ReadLine(), out type) && Enum.IsDefined(typeof(DiskType), type))
                break;
            Console.WriteLine("Ошибка: введите число от 0 до 3.");
        }

        DateTime date;
        while (true)
        {
            Console.Write("Дата записи (гггг-мм-дд): ");
            if (DateTime.TryParse(Console.ReadLine(), out date))
                break;
            Console.WriteLine("Ошибка: введите дату в правильном формате.");
        }

        Console.Write("Исполнитель: ");
        string artist = Console.ReadLine();
        while (string.IsNullOrWhiteSpace(artist))
        {
            Console.WriteLine("Исполнитель не может быть пустым.");
            Console.Write("Исполнитель: ");
            artist = Console.ReadLine();
        }

        Console.Write("Название альбома: ");
        string album = Console.ReadLine();
        while (string.IsNullOrWhiteSpace(album))
        {
            Console.WriteLine("Название альбома не может быть пустым.");
            Console.Write("Название альбома: ");
            album = Console.ReadLine();
        }

        int duration;
        while (true)
        {
            Console.Write("Продолжительность (минут): ");
            if (int.TryParse(Console.ReadLine(), out duration) && duration > 0)
                break;
            Console.WriteLine("Ошибка: введите положительное целое число.");
        }

        collection.Add(new MusicDisk(type, date, artist, album, duration));
        Console.WriteLine("Музыкальный диск добавлен.");
    }

    static void AddDataDisk(DiskCollection collection)
    {
        Console.WriteLine("\nДобавление диска с данными:");

        DiskType type;
        while (true)
        {
            Console.Write("Тип диска (0-CD, 1-DVD, 2-BluRay, 3-HDDVD): ");
            if (Enum.TryParse(Console.ReadLine(), out type) && Enum.IsDefined(typeof(DiskType), type))
                break;
            Console.WriteLine("Ошибка: введите число от 0 до 3.");
        }

        DateTime date;
        while (true)
        {
            Console.Write("Дата записи (гггг-мм-дд): ");
            if (DateTime.TryParse(Console.ReadLine(), out date))
                break;
            Console.WriteLine("Ошибка: введите дату в правильном формате.");
        }

        Console.Write("Описание: ");
        string description = Console.ReadLine();
        while (string.IsNullOrWhiteSpace(description))
        {
            Console.WriteLine("Описание не может быть пустым.");
            Console.Write("Описание: ");
            description = Console.ReadLine();
        }

        double size;
        while (true)
        {
            Console.Write("Объем данных (MB): ");
            if (double.TryParse(Console.ReadLine(), out size) && size > 0)
                break;
            Console.WriteLine("Ошибка: введите положительное число.");
        }

        DataType dataType;
        while (true)
        {
            Console.Write("Тип данных (0-ПО, 1-игры, 2-видео, 3-аудио, 4-данные): ");
            if (Enum.TryParse(Console.ReadLine(), out dataType) && Enum.IsDefined(typeof(DataType), dataType))
                break;
            Console.WriteLine("Ошибка: введите число от 0 до 4.");
        }

        collection.Add(new DataDisk(type, date, description, size, dataType));
        Console.WriteLine("Диск с данными добавлен.");
    }

    static void SortCollection(DiskCollection collection)
    {
        if (collection.Count == 0)
        {
            Console.WriteLine("Коллекция пуста. Нечего сортировать.");
            return;
        }

        SortField field;
        while (true)
        {
            Console.WriteLine("\nПоля для сортировки:");
            Console.WriteLine("0 - Дата записи");
            Console.WriteLine("1 - Исполнитель (для музыкальных дисков)");
            Console.WriteLine("2 - Название альбома (для музыкальных дисков)");
            Console.WriteLine("3 - Продолжительность (для музыкальных дисков)");
            Console.WriteLine("4 - Описание (для дисков с данными)");
            Console.WriteLine("5 - Объем данных (для дисков с данными)");
            Console.Write("Выберите поле: ");
            if (Enum.TryParse(Console.ReadLine(), out field) && Enum.IsDefined(typeof(SortField), field))
                break;
            Console.WriteLine("Ошибка: введите число от 0 до 5.");
        }

        SortDirection direction;
        while (true)
        {
            Console.Write("Направление сортировки (0-по возрастанию, 1-по убыванию): ");
            if (Enum.TryParse(Console.ReadLine(), out direction) && Enum.IsDefined(typeof(SortDirection), direction))
                break;
            Console.WriteLine("Ошибка: введите 0 или 1.");
        }

        collection.Sort(field, direction);
        Console.WriteLine("Коллекция отсортирована.");
    }

    static void GetDiskType(DiskCollection collection)
    {
        if (collection.Count == 0)
        {
            Console.WriteLine("Коллекция пуста.");
            return;
        }

        int index;
        while (true)
        {
            Console.Write($"Введите индекс диска (0-{collection.Count - 1}): ");
            if (int.TryParse(Console.ReadLine(), out index) && index >= 0 && index < collection.Count)
                break;
            Console.WriteLine($"Ошибка: введите число от 0 до {collection.Count - 1}.");
        }

        Console.WriteLine($"Тип диска: {collection.GetDiskType(index)}");
    }
}