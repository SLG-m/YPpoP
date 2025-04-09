using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Ship
{
    public string Name { get; set; }
    public double Vodoizmesc { get; set; }
    public string Type { get; set; }

    public Ship(string name, double vodoizmesc, string type)
    {
        Name = name;
        Vodoizmesc = vodoizmesc;
        Type = type;
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Имя: {Name}, Водоизмещение: {Vodoizmesc} тонн, Тип: {Type}");
    }
}

public class ShipList
{
    private LinkedList<Ship> ships = new LinkedList<Ship>();
    private LinkedListNode<Ship> currentElement;

    public void AddShip(Ship ship)
    {
        ships.AddLast(ship);
        if (currentElement == null) currentElement = ships.First;
    }

    public void RemoveCurrentShip()
    {
        if (currentElement == null)
        {
            Console.WriteLine("Список пустой");
            return;
        }

        LinkedListNode<Ship> nextElement;
        if (currentElement.Next != null)
        {
            nextElement = currentElement.Next;
        }
        else
        {
            nextElement = ships.First;
        }

        ships.Remove(currentElement);
        currentElement = nextElement;
    }

    public void MoveToFirst()
    {
        currentElement = ships.First;
    }

    public void MoveToLast()
    {
        currentElement = ships.Last;
    }

    public void MoveNext()
    {
        if (currentElement?.Next != null)
            currentElement = currentElement.Next;
    }

    public void MovePrevious()
    {
        if (currentElement?.Previous != null)
            currentElement = currentElement.Previous;
    }

    public Ship GetCurrentShip()
    {
        return currentElement?.Value;
    }

    public void UpdateCurrentShip(string name, double vodoizmesc, string type)
    {
        if (currentElement != null)
        {
            currentElement.Value.Name = name;
            currentElement.Value.Vodoizmesc = vodoizmesc;
            currentElement.Value.Type = type;
        }
    }

    public void SortByDisplacement(bool ascending = true)
    {
        var list = new List<Ship>(ships);

        if (ascending)
        {
            list.Sort(Asc);
        }
        else
        {
            list.Sort(Desc);
        }

        ships = new LinkedList<Ship>(list);
        currentElement = ships.First;
    }

    private int Asc(Ship x, Ship y)
    {
        return x.Vodoizmesc.CompareTo(y.Vodoizmesc);
    }

    private int Desc(Ship x, Ship y)
    {
        return y.Vodoizmesc.CompareTo(x.Vodoizmesc);
    }

    public void PrintAllShips()
    {
        foreach (var ship in ships)
        {
            ship.DisplayInfo();
        }
    }

    public void SaveToFile(string filePath)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var ship in ships)
                {
                    writer.WriteLine($"{ship.Name}|{ship.Vodoizmesc}|{ship.Type}");
                }
            }
            Console.WriteLine("Данные успешно сохранены в файл.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при сохранении в файл: {ex.Message}");
        }
    }

    public void LoadFromFile(string filePath)
    {
        try
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Файл не существует.");
                return;
            }

            LinkedList<Ship> newShips = new LinkedList<Ship>();
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var parts = line.Split('|');
                    if (parts.Length == 3 &&
                        double.TryParse(parts[1], out double displacement) &&
                        displacement > 0)
                    {
                        newShips.AddLast(new Ship(parts[0], displacement, parts[2]));
                    }
                }
            }

            ships = newShips;
            currentElement = ships.First;
            Console.WriteLine("Данные успешно загружены из файла.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при загрузке из файла: {ex.Message}");
        }
    }

    // LINQ методы для задания 2
    public void LinqQueries()
    {
        if (!ships.Any())
        {
            Console.WriteLine("Коллекция кораблей пуста.");
            return;
        }

        // Фильтрация: корабли с водоизмещением больше 10000 тонн
        var bigShips = ships.Where(s => s.Vodoizmesc > 10000);
        Console.WriteLine("\nКорабли с водоизмещением > 10000 тонн:");
        foreach (var ship in bigShips)
        {
            ship.DisplayInfo();
        }

        // Проекция: только имена и типы кораблей
        var namesAndTypes = ships.Select(s => new { s.Name, s.Type });
        Console.WriteLine("\nИмена и типы кораблей:");
        foreach (var item in namesAndTypes)
        {
            Console.WriteLine($"{item.Name} - {item.Type}");
        }

        // Сортировка по имени
        var sortedByName = ships.OrderBy(s => s.Name);
        Console.WriteLine("\nКорабли, отсортированные по имени:");
        foreach (var ship in sortedByName)
        {
            ship.DisplayInfo();
        }

        // Агрегатные операции
        Console.WriteLine("\nАгрегатные данные:");
        Console.WriteLine($"Всего кораблей: {ships.Count()}");
        Console.WriteLine($"Суммарное водоизмещение: {ships.Sum(s => s.Vodoizmesc)} тонн");
        Console.WriteLine($"Среднее водоизмещение: {ships.Average(s => s.Vodoizmesc):0.00} тонн");
        Console.WriteLine($"Максимальное водоизмещение: {ships.Max(s => s.Vodoizmesc)} тонн");
        Console.WriteLine($"Минимальное водоизмещение: {ships.Min(s => s.Vodoizmesc)} тонн");

        // Группировка по типу
        var shipsByType = ships.GroupBy(s => s.Type);
        Console.WriteLine("\nКорабли по типам:");
        foreach (var group in shipsByType)
        {
            Console.WriteLine($"\nТип: {group.Key}");
            foreach (var ship in group)
            {
                ship.DisplayInfo();
            }
        }

        // Соединение с другой коллекцией (пример)
        var shipTypes = new[]
        {
            new { Code = "Tanker", Description = "Танкер" },
            new { Code = "Cargo", Description = "Грузовой" },
            new { Code = "Military", Description = "Военный" }
        };

        var joinedData = from ship in ships
                         join type in shipTypes on ship.Type equals type.Code
                         select new { ship.Name, ship.Vodoizmesc, TypeDescription = type.Description };

        Console.WriteLine("\nСоединенные данные с описанием типов:");
        foreach (var item in joinedData)
        {
            Console.WriteLine($"{item.Name}, {item.Vodoizmesc} тонн, {item.TypeDescription}");
        }

        // Сохранение результатов LINQ в файл
        SaveLinqResultsToFile("linq_results.txt", bigShips, namesAndTypes, sortedByName, shipsByType, joinedData);
    }

    private void SaveLinqResultsToFile(string filePath, params object[] results)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var result in results)
                {
                    writer.WriteLine($"=== {result.GetType().Name} ===");

                    if (result is IEnumerable<Ship> shipEnumerable)
                    {
                        foreach (var ship in shipEnumerable)
                        {
                            writer.WriteLine($"{ship.Name}|{ship.Vodoizmesc}|{ship.Type}");
                        }
                    }
                    else if (result is IEnumerable<dynamic> dynamicEnumerable)
                    {
                        foreach (var item in dynamicEnumerable)
                        {
                            // Обработка сгруппированных данных
                            if (item is IGrouping<string, Ship> shipGroup)
                            {
                                writer.WriteLine($"\nГруппа: {shipGroup.Key}");
                                foreach (var ship in shipGroup)
                                {
                                    writer.WriteLine($"  {ship.Name}|{ship.Vodoizmesc}|{ship.Type}");
                                }
                            }
                            else
                            {
                                writer.WriteLine(item.ToString());
                            }
                        }
                    }

                    writer.WriteLine();
                }
            }
            Console.WriteLine($"Результаты LINQ запросов сохранены в {filePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при сохранении результатов LINQ: {ex.Message}");
        }
    }
}

public class MyApplication
{
    private ShipList shipList = new ShipList();
    private const string DataFilePath = "ships_data.txt";

    public void Run()
    {
        // Загрузка данных при запуске
        shipList.LoadFromFile(DataFilePath);

        while (true)
        {
            Console.WriteLine("\nМеню:");
            Console.WriteLine("1. Добавить корабль");
            Console.WriteLine("2. Удалить текущий корабль");
            Console.WriteLine("3. Перейти к началу списка");
            Console.WriteLine("4. Перейти к концу списка");
            Console.WriteLine("5. Перейти к следующему кораблю");
            Console.WriteLine("6. Перейти к предыдущему кораблю");
            Console.WriteLine("7. Получить текущий корабль");
            Console.WriteLine("8. Установить новое значение текущего корабля");
            Console.WriteLine("9. Сортировать по водоизмещению (по возрастанию)");
            Console.WriteLine("10. Сортировать по водоизмещению (по убыванию)");
            Console.WriteLine("11. Вывести все корабли");
            Console.WriteLine("12. Сохранить данные в файл");
            Console.WriteLine("13. Загрузить данные из файла");
            Console.WriteLine("14. Выполнить LINQ запросы");
            Console.WriteLine("15. Выйти");

            Console.Write("Выберите действие: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddShip();
                    break;
                case "2":
                    shipList.RemoveCurrentShip();
                    break;
                case "3":
                    shipList.MoveToFirst();
                    break;
                case "4":
                    shipList.MoveToLast();
                    break;
                case "5":
                    shipList.MoveNext();
                    break;
                case "6":
                    shipList.MovePrevious();
                    break;
                case "7":
                    var currentShip = shipList.GetCurrentShip();
                    if (currentShip != null)
                        currentShip.DisplayInfo();
                    else
                        Console.WriteLine("Список пустой или текущий элемент не выбран");
                    break;
                case "8":
                    UpdateCurrentShip();
                    break;
                case "9":
                    shipList.SortByDisplacement(true);
                    break;
                case "10":
                    shipList.SortByDisplacement(false);
                    break;
                case "11":
                    shipList.PrintAllShips();
                    break;
                case "12":
                    shipList.SaveToFile(DataFilePath);
                    break;
                case "13":
                    shipList.LoadFromFile(DataFilePath);
                    break;
                case "14":
                    shipList.LinqQueries();
                    break;
                case "15":
                    shipList.SaveToFile(DataFilePath);
                    return;
                default:
                    Console.WriteLine("Неправильный выбор");
                    break;
            }
        }
    }

    private void AddShip()
    {
        string name;
        while (true)
        {
            Console.Write("Введите название корабля: ");
            name = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(name))
            {
                break;
            }
            else
            {
                Console.WriteLine("Ошибка: Название корабля не может быть пустым.");
            }
        }

        double displacement;
        while (true)
        {
            Console.Write("Введите водоизмещение: ");
            try
            {
                displacement = Convert.ToDouble(Console.ReadLine());
                if (displacement > 0)
                    break;
                else
                    Console.WriteLine("Ошибка: Введите положительное число для водоизмещения.");
            }
            catch
            {
                Console.WriteLine("Ошибка: Введите корректное число для водоизмещения.");
            }
        }

        string type;
        while (true)
        {
            Console.Write("Введите тип: ");
            type = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(type))
            {
                break;
            }
            else
            {
                Console.WriteLine("Ошибка: Тип корабля не может быть пустым.");
            }
        }

        shipList.AddShip(new Ship(name, displacement, type));
    }

    private void UpdateCurrentShip()
    {
        string name;
        while (true)
        {
            Console.Write("Введите новое название корабля: ");
            name = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(name))
            {
                break;
            }
            else
            {
                Console.WriteLine("Ошибка: Название корабля не может быть пустым.");
            }
        }

        double displacement;
        while (true)
        {
            Console.Write("Введите новое водоизмещение: ");
            try
            {
                displacement = Convert.ToDouble(Console.ReadLine());
                if (displacement > 0)
                    break;
                else
                    Console.WriteLine("Ошибка: Введите положительное число для водоизмещения.");
            }
            catch
            {
                Console.WriteLine("Ошибка: Введите корректное число для водоизмещения.");
            }
        }

        string type;
        while (true)
        {
            Console.Write("Введите новый тип: ");
            type = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(type))
            {
                break;
            }
            else
            {
                Console.WriteLine("Ошибка: Тип корабля не может быть пустым.");
            }
        }

        shipList.UpdateCurrentShip(name, displacement, type);
    }
}

class Program
{
    static void Main(string[] args)
    {
        MyApplication app = new MyApplication();
        app.Run();
    }
}