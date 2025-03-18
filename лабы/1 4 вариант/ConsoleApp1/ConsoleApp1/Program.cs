using System;

public class Firearm
{
    public string Name;
    public float Caliber;
    public double Range;

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
        Firearm q = new Firearm("АК-47", 7.62f, 800.0);

        q.DisplayInfo();
    }
}