using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;


class Ship
{
    public string Name { get; set; }
    public double Displacement { get; set; }
    public string Type { get; set; }

    public Ship(string name, double displacement, string type)
    {
        Name = name;
        Displacement = displacement;
        Type = type;
    }

    public override string ToString()
    {
        return $"Корабль: {Name}, Водоизмещение: {Displacement} тонн, Тип: {Type}";
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Создание коллекции кораблей
        List<Ship> ships = new List<Ship>()
        {
            new Ship("Титаник", 52310, "Пассажирский"),
            new Ship("50 лет Победы", 25000,"Ледокол"),
            new Ship("Адмирал Кузнецов", 61390, "Авианосец"),
            new Ship("Пётр Великий", 25860, "Крейсер"),
            new Ship("AIDAblu", 71290, "Круизный"),
            new Ship("Москва", 12500, "Ракетный крейсер"),
            new Ship("Севморпуть", 61880, "Контейнеровоз"),
            new Ship("Ямал", 23455, "Ледокол")
        };

        // Проверка на корректность данных
        bool isValid = ships.All(s => s.Displacement > 0 && !string.IsNullOrEmpty(s.Name) && !string.IsNullOrEmpty(s.Type));
        Console.WriteLine($"Все данные кораблей корректны: {isValid}\n");

        // Извлечение корабля
        Ship extracted = ships.FirstOrDefault(s => s.Name == "Титаник");
        if (extracted != null)
        {
            Console.WriteLine($"Извлеченный корабль: {extracted}\n");
        }

        // Вывод всех кораблей
        Console.WriteLine("Все корабли:");
        Print(ships);

        // LINQ операции

        // Фильтрация (водоизмещение больше 30000 тонн)
        Console.WriteLine("\nКорабли с водоизмещением > 30000 тонн:");
        var query1 = from ship in ships
                        where ship.Displacement > 30000
                        select ship;
        Print(query1);

        // Проекция (только имена кораблей)
        Console.WriteLine("\nИмена кораблей:");
        var query2 = from ship in ships
                        select ship.Name;
        Print(query2);

        // Сортировка по водоизмещению (по возрастанию)
        Console.WriteLine("\nСортировка по водоизмещению (по возрастанию):");
        var query3 = from ship in ships
                        orderby ship.Displacement
                        select ship;
        Print(query3);

        // Сортировка по водоизмещению (по убыванию)
        Console.WriteLine("\nСортировка по водоизмещению (по убыванию):");
        query3 = from ship in ships
                    orderby ship.Displacement descending
                    select ship;
        Print(query3);

        // Агрегатные операции
        Console.WriteLine($"\nСреднее водоизмещение: {ships.Average(s => s.Displacement):F2} тонн");
        Console.WriteLine($"Максимальное водоизмещение: {ships.Max(s => s.Displacement)} тонн");
        Console.WriteLine($"Минимальное водоизмещение: {ships.Min(s => s.Displacement)} тонн");
        Console.WriteLine($"Суммарное водоизмещение: {ships.Sum(s => s.Displacement)} тонн");

        // Группировка по типу корабля
        var shipGroups = ships.GroupBy(s => s.Type);
        Console.WriteLine("\nГруппировка по типу корабля:");
        foreach (var group in shipGroups)
        {
            Console.WriteLine($"Тип: {group.Key}");
            Print(group);
        }

        // Соединение коллекций
        string[] russianShips = { "Адмирал Кузнецов", "Пётр Великий", "Москва", "Севморпуть", "Ямал", "50 лет Победы" };

        var russianFleet = from ship in ships
                            join name in russianShips on ship.Name equals name
                            select ship;

        Console.WriteLine("\nРоссийские корабли:");
        Print(russianFleet);

        // Сохранение результатов в файл
        string filePath = "ships_report.txt";
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.WriteLine("Отчет по кораблям:");
            foreach (var ship in ships)
            {
                writer.WriteLine(ship);
            }

            writer.WriteLine("\nСтатистика:");
            writer.WriteLine($"Среднее водоизмещение: {ships.Average(s => s.Displacement):F2} тонн");
            writer.WriteLine($"Максимальное водоизмещение: {ships.Max(s => s.Displacement)} тонн");
            writer.WriteLine($"Минимальное водоизмещение: {ships.Min(s => s.Displacement)} тонн");
            writer.WriteLine($"Суммарное водоизмещение: {ships.Sum(s => s.Displacement)} тонн");

            writer.WriteLine("\nКорабли с водоизмещением > 30000 тонн:");
            foreach (var ship in query1)
            {
                writer.WriteLine(ship);
            }

            writer.WriteLine("\nРоссийские корабли:");
            foreach (var ship in russianFleet)
            {
                writer.WriteLine(ship);
            }
        }

        Console.WriteLine($"\nРезультаты сохранены в файл: {filePath}");
    }

    // Методы для вывода коллекций
    public static void Print(List<Ship> elements)
    {
        foreach (var element in elements)
        {
            Console.WriteLine(element);
        }
    }

    public static void Print(IEnumerable<Ship> elements)
    {
        foreach (var element in elements)
        {
            Console.WriteLine(element);
        }
    }

    public static void Print(IEnumerable<string> elements)
    {
        foreach (var element in elements)
        {
            Console.WriteLine(element);
        }
    }
}
