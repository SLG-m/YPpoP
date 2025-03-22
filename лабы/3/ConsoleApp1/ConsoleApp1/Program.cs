using System;

public class Matrix2 : ICloneable
{
    // Закрытые поля для хранения элементов матрицы
    private double a11, a12, a21, a22;

    // Конструктор, принимающий два элемента побочной диагонали (остальные элементы равны 1)
    public Matrix2(double a12, double a21)
    {
        this.a11 = 1;
        this.a12 = a12;
        this.a21 = a21;
        this.a22 = 1;
    }

    // Конструктор, принимающий все элементы матрицы
    public Matrix2(double a11, double a12, double a21, double a22)
    {
        this.a11 = a11;
        this.a12 = a12;
        this.a21 = a21;
        this.a22 = a22;
    }

    // Метод для клонирования объекта
    public object Clone()
    {
        return new Matrix2(this.a11, this.a12, this.a21, this.a22);
    }

    // Метод для вычисления определителя матрицы
    public double Det()
    {
        return a11 * a22 - a12 * a21;
    }

    // Метод для нахождения обратной матрицы
    public Matrix2 Inverse()
    {
        double det = Det();
        if (det == 0)
        {
            throw new InvalidOperationException("Матрица вырожденная, обратной матрицы не существует.");
        }

        return new Matrix2(a22 / det, -a12 / det, -a21 / det, a11 / det);
    }

    // Перегрузка оператора сложения матриц
    public static Matrix2 operator +(Matrix2 m1, Matrix2 m2)
    {
        return new Matrix2(
            m1.a11 + m2.a11,
            m1.a12 + m2.a12,
            m1.a21 + m2.a21,
            m1.a22 + m2.a22
        );
    }

    // Перегрузка оператора умножения матриц
    public static Matrix2 operator *(Matrix2 m1, Matrix2 m2)
    {
        return new Matrix2(
            m1.a11 * m2.a11 + m1.a12 * m2.a21,
            m1.a11 * m2.a12 + m1.a12 * m2.a22,
            m1.a21 * m2.a11 + m1.a22 * m2.a21,
            m1.a21 * m2.a12 + m1.a22 * m2.a22
        );
    }

    // Переопределение метода ToString для удобного вывода матрицы
    public override string ToString()
    {
        return $"[{a11}, {a12}]\n[{a21}, {a22}]";
    }
}

public class MyApplication
{
    public void Run()
    {
        while (true)
        {
            Console.WriteLine("1. Создать матрицу");
            Console.WriteLine("2. Вычислить определитель");
            Console.WriteLine("3. Найти обратную матрицу");
            Console.WriteLine("4. Сложить две матрицы");
            Console.WriteLine("5. Умножить две матрицы");
            Console.WriteLine("6. Выйти");
            Console.Write("Выберите опцию: ");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    CreateMatrix();
                    break;
                case 2:
                    CalculateDeterminant();
                    break;
                case 3:
                    FindInverse();
                    break;
                case 4:
                    AddMatrices();
                    break;
                case 5:
                    MultiplyMatrices();
                    break;
                case 6:
                    return;
                default:
                    Console.WriteLine("Неверный выбор. Попробуйте снова.");
                    break;
            }
        }
    }

    private void CreateMatrix()
    {
        Console.Write("Введите элемент a12: ");
        double a12 = double.Parse(Console.ReadLine());
        Console.Write("Введите элемент a21: ");
        double a21 = double.Parse(Console.ReadLine());

        Matrix2 matrix = new Matrix2(a12, a21);
        Console.WriteLine("Матрица создана:\n" + matrix);
    }

    private void CalculateDeterminant()
    {
        Matrix2 matrix = GetMatrix();
        Console.WriteLine("Определитель матрицы: " + matrix.Det());
    }

    private void FindInverse()
    {
        Matrix2 matrix = GetMatrix();
        try
        {
            Matrix2 inverseMatrix = matrix.Inverse();
            Console.WriteLine("Обратная матрица:\n" + inverseMatrix);
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private void AddMatrices()
    {
        Console.WriteLine("Введите первую матрицу:");
        Matrix2 matrix1 = GetMatrix();
        Console.WriteLine("Введите вторую матрицу:");
        Matrix2 matrix2 = GetMatrix();

        Matrix2 result = matrix1 + matrix2;
        Console.WriteLine("Результат сложения:\n" + result);
    }

    private void MultiplyMatrices()
    {
        Console.WriteLine("Введите первую матрицу:");
        Matrix2 matrix1 = GetMatrix();
        Console.WriteLine("Введите вторую матрицу:");
        Matrix2 matrix2 = GetMatrix();

        Matrix2 result = matrix1 * matrix2;
        Console.WriteLine("Результат умножения:\n" + result);
    }

    private Matrix2 GetMatrix()
    {
        Console.Write("Введите элемент a12: ");
        double a12 = double.Parse(Console.ReadLine());
        Console.Write("Введите элемент a21: ");
        double a21 = double.Parse(Console.ReadLine());

        return new Matrix2(a12, a21);
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