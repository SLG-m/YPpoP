using System;

public class Account<T>
{
    public decimal Amount { get; private set; }
    public T Currency { get; private set; }

    public Account(decimal amount, T currency)
    {
        Amount = amount;
        Currency = currency;
    }

    public Account()
    {
        Amount = 0;
        Currency = default;
    }

    public void Input()
    {
        Console.Write("Введите сумму: ");
        decimal amount;
        while (!decimal.TryParse(Console.ReadLine(), out amount))
        {
            Console.Write("Ошибка. Введите сумму: ");
        }
        Amount = amount;

        Console.Write("Введите валюту: ");
        string currencyInput = Console.ReadLine();

        try
        {
            if (typeof(T) == typeof(string))
            {
                Currency = (T)Convert.ChangeType(currencyInput, typeof(T));
            }
            else if (typeof(T).IsEnum)
            {
                Currency = (T)Enum.Parse(typeof(T), currencyInput, true);
            }
            else
            {
                T converted = (T)Convert.ChangeType(currencyInput, typeof(T));
                Currency = converted;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка ввода валюты: {ex.Message}");
            Currency = default;
        }
    }

    public void Output()
    {
        Console.WriteLine($"Сумма: {Amount}, Валюта: {Currency}");
    }

    public static Account<T> operator +(Account<T> a, Account<T> b)
    {
        if (a is null || b is null)
            throw new ArgumentNullException();
        if (!a.Currency.Equals(b.Currency))
            throw new InvalidOperationException("Валюты не совпадают.");
        return new Account<T>(a.Amount + b.Amount, a.Currency);
    }

    public static Account<T> operator +(Account<T> account, decimal amount)
    {
        return new Account<T>(account.Amount + amount, account.Currency);
    }

    public static Account<T> operator -(Account<T> account, decimal amount)
    {
        return new Account<T>(account.Amount - amount, account.Currency);
    }

    public static Account<T> operator ++(Account<T> account)
    {
        account.Amount *= 1.05m;
        return account;
    }

    public static bool operator >(Account<T> a, Account<T> b)
    {
        if (a is null || b is null)
            throw new ArgumentNullException();
        if (!a.Currency.Equals(b.Currency))
            throw new InvalidOperationException("Валюты не совпадают.");
        return a.Amount > b.Amount;
    }

    public static bool operator <(Account<T> a, Account<T> b)
    {
        if (a is null || b is null)
            throw new ArgumentNullException();
        if (!a.Currency.Equals(b.Currency))
            throw new InvalidOperationException("Валюты не совпадают.");
        return a.Amount < b.Amount;
    }
}

class Program
{
    static void Main()
    {
        Account<string> account1 = new Account<string>();
        Console.WriteLine("Ввод данных для счета 1:");
        account1.Input();
        account1.Output();

        Account<string> account2 = new Account<string>();
        Console.WriteLine("\nВвод данных для счета 2:");
        account2.Input();
        account2.Output();

        try
        {
            Account<string> mergedAccount = account1 + account2;
            Console.WriteLine("\nРезультат слияния счетов:");
            mergedAccount.Output();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nОшибка при слиянии: {ex.Message}");
        }

        Console.WriteLine("\nПополнение счета 1 на 100:");
        account1 += 100m;
        account1.Output();

        Console.WriteLine("\nСнятие 50 со счета 1:");
        account1 = account1 - 50m;
        account1.Output();

        Console.WriteLine("\nНачисление 5% на счет 1:");
        account1++;
        account1.Output();

        try
        {
            Console.WriteLine("\nСравнение счетов:");
            bool isGreater = account1 > account2;
            Console.WriteLine($"Счет 1 больше счета 2: {isGreater}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при сравнении: {ex.Message}");
        }
    }
}