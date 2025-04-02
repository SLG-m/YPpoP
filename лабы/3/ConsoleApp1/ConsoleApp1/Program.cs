using System;

public class Matrix2 : ICloneable
{
    private double a11, a12, a21, a22;

    public Matrix2(double a12, double a21)
    {
        this.a11 = 1;
        this.a12 = a12;
        this.a21 = a21;
        this.a22 = 1;
    }

    public Matrix2(double a11, double a12, double a21, double a22)
    {
        this.a11 = a11;
        this.a12 = a12;
        this.a21 = a21;
        this.a22 = a22;
    }

    public object Clone()
    {
        return new Matrix2(this.a11, this.a12, this.a21, this.a22);
    }

    public double Det()
    {
        return a11 * a22 - a12 * a21;
    }

    public Matrix2 Inverse()
    {
        double det = Det();
        if (det == 0)
        {
            throw new InvalidOperationException("Матрица вырожденная, обратной матрицы не существует.");
        }

        return new Matrix2(a22 / det, -a12 / det, -a21 / det, a11 / det);
    }

    public static Matrix2 operator +(Matrix2 m1, Matrix2 m2)
    {
        return new Matrix2(
            m1.a11 + m2.a11,
            m1.a12 + m2.a12,
            m1.a21 + m2.a21,
            m1.a22 + m2.a22
        );
    }

    public static Matrix2 operator *(Matrix2 m1, Matrix2 m2)
    {
        return new Matrix2(
            m1.a11 * m2.a11 + m1.a12 * m2.a21,
            m1.a11 * m2.a12 + m1.a12 * m2.a22,
            m1.a21 * m2.a11 + m1.a22 * m2.a21,
            m1.a21 * m2.a12 + m1.a22 * m2.a22
        );
    }

    public override string ToString()
    {
        return $"[{a11}, {a12}]\n[{a21}, {a22}]";
    }
}

public class MyApplication
{
    private Matrix2 matrix1;
    private Matrix2 matrix2;

    public void Run()
    {
        while (true)
        {
            Console.WriteLine("1. Создать две матрицы");
            Console.WriteLine("2. Вычислить определитель первой матрицы");
            Console.WriteLine("3. Найти обратную матрицу для первой матрицы");
            Console.WriteLine("4. Сложить две матрицы");
            Console.WriteLine("5. Умножить две матрицы");
            Console.WriteLine("6. Выйти");
            Console.Write("Выберите опцию: ");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    CreateMatrices();
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

    private void CreateMatrices()
    {
        Console.WriteLine("Введите элементы первой матрицы:");
        matrix1 = GetMatrix();
        Console.WriteLine("Первая матрица создана:\n" + matrix1);

        Console.WriteLine("Введите элементы второй матрицы:");
        matrix2 = GetMatrix();
        Console.WriteLine("Вторая матрица создана:\n" + matrix2);
    }

    private void CalculateDeterminant()
    {
        if (matrix1 == null)
        {
            Console.WriteLine("Матрицы не созданы. Сначала создайте матрицы.");
            return;
        }

        Matrix2 clonedMatrix = (Matrix2)matrix1.Clone();
        Console.WriteLine("Определитель первой матрицы: " + clonedMatrix.Det());
    }

    private void FindInverse()
    {
        if (matrix1 == null)
        {
            Console.WriteLine("Матрицы не созданы. Сначала создайте матрицы.");
            return;
        }

        try
        {
            Matrix2 clonedMatrix = (Matrix2)matrix1.Clone();
            Matrix2 inverseMatrix = clonedMatrix.Inverse();
            Console.WriteLine("Обратная матрица для первой матрицы:\n" + inverseMatrix);
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private void AddMatrices()
    {
        if (matrix1 == null || matrix2 == null)
        {
            Console.WriteLine("Матрицы не созданы. Сначала создайте матрицы.");
            return;
        }

        Matrix2 result = matrix1 + matrix2;
        Console.WriteLine("Результат сложения:\n" + result);
    }

    private void MultiplyMatrices()
    {
        if (matrix1 == null || matrix2 == null)
        {
            Console.WriteLine("Матрицы не созданы. Сначала создайте матрицы.");
            return;
        }

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
    static void Main()
    {
        MyApplication app = new MyApplication();
        app.Run();
    }
}