using System;

// Интерфейсы
public interface IEmployeeInfo
{
    string ShowInfo();
}

public interface IWorkable
{
    void Work();
}

public interface ITrainable
{
    void AttendTraining();
}

// Базовый абстрактный класс
public abstract class Employee : IEmployeeInfo
{
    public string Name { get; set; }
    public string Position { get; set; }

    public event Action<string> OnTaskCompleted;

    public Employee(string name, string position)
    {
        Name = name;
        Position = position;
    }

    public virtual string ShowInfo()
    {
        return $"Имя: {Name}\nДолжность: {Position}";
    }

    protected void NotifyTaskCompleted(string task)
    {
        OnTaskCompleted?.Invoke($"{Position} {Name} выполнил задачу: {task}");
    }
}

// Класс Рабочий
public class Worker : Employee, IWorkable
{
    public string Department { get; set; }

    public Worker(string name, string department) : base(name, "Рабочий")
    {
        Department = department;
    }

    public override string ShowInfo()
    {
        return base.ShowInfo() + $"\nОтдел: {Department}";
    }

    public void Work()
    {
        Console.WriteLine($"Рабочий {Name} выполняет работу в отделе {Department}");
        NotifyTaskCompleted("Выполнение производственного задания");
    }
}

// Класс Инженер
public class Engineer : Employee, IWorkable, ITrainable
{
    public string Specialization { get; set; }

    public Engineer(string name, string specialization) : base(name, "Инженер")
    {
        Specialization = specialization;
    }

    public override string ShowInfo()
    {
        return base.ShowInfo() + $"\nСпециализация: {Specialization}";
    }

    public void Work()
    {
        Console.WriteLine($"Инженер {Name} работает над проектом в области {Specialization}");
        NotifyTaskCompleted("Проектирование системы");
    }

    public void AttendTraining()
    {
        Console.WriteLine($"Инженер {Name} проходит обучение по {Specialization}");
        NotifyTaskCompleted("Прохождение тренинга");
    }
}

class Program
{
    static void Main()
    {
        Worker worker = new Worker("Иван Петров", "Производство");
        Engineer engineer = new Engineer("Алексей Сидоров", "Электротехника");

        worker.OnTaskCompleted += message => Console.WriteLine($"[Событие] {message}");
        engineer.OnTaskCompleted += message => Console.WriteLine($"[Событие] {message}");

        worker.Work();
        engineer.Work();
        engineer.AttendTraining();
    }
}