using System;
using System.Collections.Generic;

// Базовый абстрактный класс для аудиотехники
public abstract class AudioEquipment
{
    public string Manufacturer { get; set; }
    public string Model { get; set; }
    public int YearOfProduction { get; set; }

    protected AudioEquipment(string manufacturer, string model, int yearOfProduction)
    {
        Manufacturer = manufacturer;
        Model = model;
        YearOfProduction = yearOfProduction;
    }

    public abstract void DisplayInfo();
}

// Класс MP3-плеер 
public sealed class MP3Player : AudioEquipment, ICloneable
{
    public string PowerType { get; set; }
    public bool HasDictaphone { get; set; }
    public bool HasFMTuner { get; set; }
    public int FlashMemorySize { get; set; }

    public MP3Player(string manufacturer, string model, int yearOfProduction,
                    string powerType, bool hasDictaphone, bool hasFMTuner, int flashMemorySize)
        : base(manufacturer, model, yearOfProduction)
    {
        PowerType = powerType;
        HasDictaphone = hasDictaphone;
        HasFMTuner = hasFMTuner;
        FlashMemorySize = flashMemorySize;
    }

    public override void DisplayInfo()
    {
        Console.WriteLine("MP3-плеер:");
        Console.WriteLine($"Производитель: {Manufacturer}");
        Console.WriteLine($"Модель: {Model}");
        Console.WriteLine($"Год выпуска: {YearOfProduction}");
        Console.WriteLine($"Тип питания: {PowerType}");
        Console.WriteLine($"Наличие диктофона: {(HasDictaphone ? "Да" : "Нет")}");
        Console.WriteLine($"Наличие FM-тюнера: {(HasFMTuner ? "Да" : "Нет")}");
        Console.WriteLine($"Объем памяти: {FlashMemorySize} МБ");
        Console.WriteLine();
    }

    public object Clone()
    {
        return new MP3Player(Manufacturer, Model, YearOfProduction,
                            PowerType, HasDictaphone, HasFMTuner, FlashMemorySize);
    }
}

// Класс магнитофон 
public sealed class TapeRecorder : AudioEquipment
{
    public int CassetteDecksCount { get; set; }
    public bool HasCDDrive { get; set; }
    public bool HasFMTuner { get; set; }
    public bool HasEqualizer { get; set; }

    public TapeRecorder(string manufacturer, string model, int yearOfProduction,
                       int cassetteDecksCount, bool hasCDDrive, bool hasFMTuner, bool hasEqualizer)
        : base(manufacturer, model, yearOfProduction)
    {
        CassetteDecksCount = cassetteDecksCount;
        HasCDDrive = hasCDDrive;
        HasFMTuner = hasFMTuner;
        HasEqualizer = hasEqualizer;
    }

    public override void DisplayInfo()
    {
        Console.WriteLine("Магнитофон:");
        Console.WriteLine($"Производитель: {Manufacturer}");
        Console.WriteLine($"Модель: {Model}");
        Console.WriteLine($"Год выпуска: {YearOfProduction}");
        Console.WriteLine($"Количество кассетных деков: {CassetteDecksCount}");
        Console.WriteLine($"Наличие CD-привода: {(HasCDDrive ? "Да" : "Нет")}");
        Console.WriteLine($"Наличие FM-тюнера: {(HasFMTuner ? "Да" : "Нет")}");
        Console.WriteLine($"Наличие эквалайзера: {(HasEqualizer ? "Да" : "Нет")}");
        Console.WriteLine();
    }
}

// Класс музыкальный центр
public sealed class MusicCenter : AudioEquipment
{
    public int CassetteDecksCount { get; set; }
    public int EqualizerBands { get; set; }
    public int SpeakerPower { get; set; }

    public MusicCenter(string manufacturer, string model, int yearOfProduction,
                      int cassetteDecksCount, int equalizerBands, int speakerPower)
        : base(manufacturer, model, yearOfProduction)
    {
        CassetteDecksCount = cassetteDecksCount;
        EqualizerBands = equalizerBands;
        SpeakerPower = speakerPower;
    }

    public override void DisplayInfo()
    {
        Console.WriteLine("Музыкальный центр:");
        Console.WriteLine($"Производитель: {Manufacturer}");
        Console.WriteLine($"Модель: {Model}");
        Console.WriteLine($"Год выпуска: {YearOfProduction}");
        Console.WriteLine($"Количество кассетных деков: {CassetteDecksCount}");
        Console.WriteLine($"Количество полос эквалайзера: {EqualizerBands}");
        Console.WriteLine($"Мощность колонок: {SpeakerPower} Вт");
        Console.WriteLine();
    }
}

class Program
{
    // Делегаты для работы с коллекцией
    public delegate void AddEquipmentDelegate(AudioEquipment equipment);
    public delegate void RemoveEquipmentDelegate(int index);
    public delegate void DisplayAllDelegate();
    public delegate void DisplayByIndexDelegate(int index);

    static List<AudioEquipment> equipmentCollection = new List<AudioEquipment>();

    static void Main(string[] args)
    {
        AddEquipmentDelegate addDelegate = AddEquipment;
        RemoveEquipmentDelegate removeDelegate = RemoveEquipment;
        DisplayAllDelegate displayAllDelegate = DisplayAll;
        DisplayByIndexDelegate displayByIndexDelegate = DisplayByIndex;


        MP3Player mp3Player = new MP3Player("Sony", "NW-A105", 2019, "аккумулятор", true, true, 16);
        TapeRecorder tapeRecorder = new TapeRecorder("Panasonic", "RX-D55", 1995, 2, true, true, true);
        MusicCenter musicCenter = new MusicCenter("LG", "CM4360", 2010, 1, 5, 1000);


        addDelegate(mp3Player);
        addDelegate(tapeRecorder);
        addDelegate(musicCenter);

        // Создаем копию MP3-плеера (реализован ICloneable)
        MP3Player mp3PlayerClone = (MP3Player)mp3Player.Clone();
        mp3PlayerClone.Model = "NW-A105 (Clone)";
        addDelegate(mp3PlayerClone);

        Console.WriteLine("Все элементы коллекции:");
        displayAllDelegate();

        Console.WriteLine("\nЭлемент с индексом 1:");
        displayByIndexDelegate(1);

        Console.WriteLine("\nУдаляем элемент с индексом 2:");
        removeDelegate(2);
        displayAllDelegate();
    }

    // Методы для работы с коллекцией
    public static void AddEquipment(AudioEquipment equipment)
    {
        equipmentCollection.Add(equipment);
    }

    public static void RemoveEquipment(int index)
    {
        if (index >= 0 && index < equipmentCollection.Count)
        {
            equipmentCollection.RemoveAt(index);
        }
        else
        {
            Console.WriteLine("Неверный индекс!");
        }
    }

    public static void DisplayAll()
    {
        for (int i = 0; i < equipmentCollection.Count; i++)
        {
            Console.WriteLine($"Элемент #{i}:");
            equipmentCollection[i].DisplayInfo();
        }
    }

    public static void DisplayByIndex(int index)
    {
        if (index >= 0 && index < equipmentCollection.Count)
        {
            equipmentCollection[index].DisplayInfo();
        }
        else
        {
            Console.WriteLine("Неверный индекс!");
        }
    }
}