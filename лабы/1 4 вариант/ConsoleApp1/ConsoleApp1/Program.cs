using System;

public class Firearm
{
    public string Name { get; set; }
    public float Caliber { get; set; }
    public double Range { get; set; }

    public Firearm(string name, float caliber, double range)
    {
        Name = name;
        Caliber = caliber;
        Range = range;
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Название: {Name}");
        Console.WriteLine($"Калибр: {Caliber} мм");
        Console.WriteLine($"Дальность: {Range} м");
    }
}

class Program
{
    static void Main(string[] args)
    {

    }
}