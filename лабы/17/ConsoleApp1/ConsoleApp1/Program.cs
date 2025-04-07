using System;
using System.Collections.Generic;
using System.IO;

public class Ship
{
    public string Name;
    public double Vodoizmesc;
    public string Type;

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
            Console.WriteLine("14. Выйти");

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