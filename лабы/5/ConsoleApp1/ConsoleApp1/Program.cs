using System;


public class files
{
    public string FileName { get; set; }
    public int FileSize { get; set; }
    public DateTime CreationDate { get; set; }
    public TimeSpan CreationTime { get; set; }

    public files(string fileName, int fileSize, DateTime creationDate, TimeSpan creationTime)
    {
        FileName = fileName;
        FileSize = fileSize;
        CreationDate = creationDate;
        CreationTime = creationTime;
    }

    public override string ToString()
    {
        return $"{FileName,-30} {FileSize,10} {CreationDate:dd.MM.yyyy} {CreationTime:hh\\:mm}";
    }
}

class filesList
{
    List<files> list = new List<files>();

}
public class MyApplication 
{
    public void Run()
    {
        while (true)
        {
            Console.WriteLine("\nМеню:");
            Console.WriteLine("1. Добавить файл");
            Console.WriteLine("2. Показать все файлы");
            Console.WriteLine("3. Сортировать файлы");
            Console.WriteLine("4. Получить файл по индексу");
            Console.WriteLine("5. Обновить файл по индексу");
            Console.WriteLine("6. Выход");
            Console.Write("Выберите опцию: ");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    
                    break;
                case 2:
                    
                    break;
                case 3:
                    
                    break;
                case 4:
                   
                    break;
                case 5:
                    
                    break;
                case 6:
                    return;
                default:
                    Console.WriteLine("Неверный выбор. Попробуйте снова.");
                    break;
            }
        }
    }
}
class Program
{
    static void Main()
    {
        MyApplication app = new MyApplication();
        app.Run();
    }
}