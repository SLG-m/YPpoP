using System;

/// <summary>
/// Базовый абстрактный класс для аудиотехники
/// </summary>
public abstract class AudioEquipment
{
    /// <summary>
    /// Производитель оборудования
    /// </summary>
    public string Manufacturer { get; set; }
    /// <summary>
    /// Модель оборудования
    /// </summary>
    public string Model { get; set; }
    /// <summary>
    /// Год производства
    /// </summary>
    public int YearOfProduction { get; set; }

    /// <summary>
    /// Конструктор базового класса
    /// </summary>
    /// <param name="manufacturer"></param>
    /// <param name="model"></param>
    /// <param name="yearOfProduction"></param>
    protected AudioEquipment(string manufacturer, string model, int yearOfProduction)
    {
        Manufacturer = manufacturer;
        Model = model;
        YearOfProduction = yearOfProduction;
    }
    /// <summary>
    /// Абстрактный метод для вывода информации об оборудовании
    /// </summary>
    public abstract void DisplayInfo();
}

/// <summary>
/// Класс MP3-плеер (запрещено наследование)
/// </summary>
public sealed class MP3Player : AudioEquipment, ICloneable
{
    /// <summary>
    /// Тип питания (батарейки/аккумулятор)
    /// </summary>
    public string PowerType { get; set; }
    /// <summary>
    /// Наличие диктофона
    /// </summary>
    public bool HasDictaphone { get; set; }
    /// <summary>
    /// Наличие FM-тюнера
    /// </summary>
    public bool HasFMTuner { get; set; }
    /// <summary>
    /// Объем флеш-памяти в МБ
    /// </summary>
    public int FlashMemorySize { get; set; }
    /// <summary>
    /// Конструктор MP3-плеера
    /// </summary>
    /// <param name="manufacturer"></param>
    /// <param name="model"></param>
    /// <param name="yearOfProduction"></param>
    /// <param name="powerType"></param>
    /// <param name="hasDictaphone"></param>
    /// <param name="hasFMTuner"></param>
    /// <param name="flashMemorySize"></param>
    public MP3Player(string manufacturer, string model, int yearOfProduction,
                    string powerType, bool hasDictaphone, bool hasFMTuner, int flashMemorySize)
        : base(manufacturer, model, yearOfProduction)
    {
        PowerType = powerType;
        HasDictaphone = hasDictaphone;
        HasFMTuner = hasFMTuner;
        FlashMemorySize = flashMemorySize;
    }

    /// <summary>
    /// Вывод информации MP3-плеера
    /// </summary>
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
    /// <summary>
    /// Создает копию объекта MP3Player
    /// </summary>
    /// <returns>Копия объекта</returns>
    public object Clone()
    {
        return new MP3Player(Manufacturer, Model, YearOfProduction,
                            PowerType, HasDictaphone, HasFMTuner, FlashMemorySize);
    }
}

/// <summary>
/// Класс магнитофон (запрещено наследование)
/// </summary>
public sealed class TapeRecorder : AudioEquipment
{
    /// <summary>
    /// Количество кассетных деков
    /// </summary>
    public int CassetteDecksCount { get; set; }
    /// <summary>
    /// Наличие CD-привода
    /// </summary>
    public bool HasCDDrive { get; set; }
    /// <summary>
    /// Наличие FM-тюнера
    /// </summary>
    public bool HasFMTuner { get; set; }
    /// <summary>
    /// Наличие эквалайзера
    /// </summary>
    public bool HasEqualizer { get; set; }
    /// <summary>
    /// Конструктор магнитофона 
    /// </summary>
    /// <param name="manufacturer"></param>
    /// <param name="model"></param>
    /// <param name="yearOfProduction"></param>
    /// <param name="cassetteDecksCount"></param>
    /// <param name="hasCDDrive"></param>
    /// <param name="hasFMTuner"></param>
    /// <param name="hasEqualizer"></param>
    public TapeRecorder(string manufacturer, string model, int yearOfProduction, int cassetteDecksCount, bool hasCDDrive, bool hasFMTuner, bool hasEqualizer)
        : base(manufacturer, model, yearOfProduction)
    {
        CassetteDecksCount = cassetteDecksCount;
        HasCDDrive = hasCDDrive;
        HasFMTuner = hasFMTuner;
        HasEqualizer = hasEqualizer;
    }
    /// <summary>
    /// Вывод информации о магнитафоне
    /// </summary>
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

/// <summary>
/// Класс музыкальный центр (запрещено наследование)
/// </summary>
public sealed class MusicCenter : AudioEquipment
{
    /// <summary>
    ///  Количество кассетных деков
    /// </summary>
    public int CassetteDecksCount { get; set; }
    /// <summary>
    /// Количество полос эквалайзера
    /// </summary>
    public int EqualizerBands { get; set; }
    /// <summary>
    /// Мощность колонок в ваттах
    /// </summary>
    public int SpeakerPower { get; set; }
    /// <summary>
    /// Конструктор музыкального центра
    /// </summary>
    /// <param name="manufacturer"></param>
    /// <param name="model"></param>
    /// <param name="yearOfProduction"></param>
    /// <param name="cassetteDecksCount"></param>
    /// <param name="equalizerBands"></param>
    /// <param name="speakerPower"></param>
    public MusicCenter(string manufacturer, string model, int yearOfProduction,
                      int cassetteDecksCount, int equalizerBands, int speakerPower)
        : base(manufacturer, model, yearOfProduction)
    {
        CassetteDecksCount = cassetteDecksCount;
        EqualizerBands = equalizerBands;
        SpeakerPower = speakerPower;
    }
    /// <summary>
    /// Выводит информации о музыкальном центре
    /// </summary>
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
    /// <summary>
    /// Точка входа в программу
    /// </summary>
    static void Main()
    {
        // Создаем объекты всех производных классов
        MP3Player mp3Player = new MP3Player("Sony", "NW-A105", 2019, "аккумулятор", true, true, 16);
        TapeRecorder tapeRecorder = new TapeRecorder("Panasonic", "RX-D55", 1995, 2, true, true, true);
        MusicCenter musicCenter = new MusicCenter("LG", "CM4360", 2010, 1, 5, 1000);

        // Создаем копию MP3-плеера (реализован ICloneable)
        MP3Player mp3PlayerClone = (MP3Player)mp3Player.Clone();
        mp3PlayerClone.Model = "NW-A105 (Clone)";

        // Выводим информацию обо всех объектах
        mp3Player.DisplayInfo();
        mp3PlayerClone.DisplayInfo();
        tapeRecorder.DisplayInfo();
        musicCenter.DisplayInfo();
    }
}