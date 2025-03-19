using System;
using System.Collections.Generic;


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
            Console.WriteLine("CПисок пустой");
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
        if (currentElement.Next != null)
            currentElement = currentElement.Next;
    }

    public void MovePrevious()
    {
        if (currentElement.Previous != null)
            currentElement = currentElement.Previous;
    }

    public Ship GetCurrentShip()
    {
        return currentElement.Value;
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
}

public class MyApplication
{
    private ShipList shipList = new ShipList();

    public void Run()
    {
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
            Console.WriteLine("12. Выйти");

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
                        Console.WriteLine("Список пустой");
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
                    return;
                default:
                    Console.WriteLine("Неправильно");
                    break;
            }
        }
    }

    private void AddShip()
    {
        Console.Write("Введите название корабля: ");
        string name = Console.ReadLine();
        Console.Write("Введите водоизмещение: ");
        double displacement = double.Parse(Console.ReadLine());
        Console.Write("Введите тип: ");
        string type = Console.ReadLine();

        shipList.AddShip(new Ship(name, displacement, type));
    }

    private void UpdateCurrentShip()
    {
        Console.Write("Введите новое название корабля: ");
        string name = Console.ReadLine();
        Console.Write("Введите новое водоизмещение: ");
        double displacement = double.Parse(Console.ReadLine());
        Console.Write("Введите новый тип: ");
        string type = Console.ReadLine();

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