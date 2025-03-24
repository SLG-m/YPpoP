using System;

    class Account
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; }

        public Account()
        {
            Amount = 0;
            Currency = "";
        }

        public Account(decimal amount, string currency)
        {
            Amount = amount;
            Currency = currency;
        }

        public void Input()
        {
            Console.Write("Введите сумму на счете: ");
            Amount = decimal.Parse(Console.ReadLine());

            Console.Write("Введите валюту счета (например, USD, EUR, RUB): ");
            Currency = Console.ReadLine().ToUpper(); 
        }

        public void Display()
        {
            Console.WriteLine($"Сумма: {Amount} {Currency}");
        }

        public static Account operator +(Account acc1, Account acc2)
        {
            if (acc1.Currency != acc2.Currency)
            {
                throw new InvalidOperationException("Валюты счетов не совпадают.");
            }

            return new Account(acc1.Amount + acc2.Amount, acc1.Currency);
        }

        public static Account operator -(Account acc, decimal amount)
        {
            if (acc.Amount < amount)
            {
                throw new InvalidOperationException("Недостаточно средств на счете.");
            }

            return new Account(acc.Amount - amount, acc.Currency);
        }

        public static Account operator +(Account acc, decimal amount)
        {
            return new Account(acc.Amount + amount, acc.Currency);
        }

        public static bool operator >(Account acc1, Account acc2)
        {
            if (acc1.Currency != acc2.Currency)
            {
                throw new InvalidOperationException("Валюты счетов не совпадают.");
            }

            return acc1.Amount > acc2.Amount;
        }

        public static bool operator <(Account acc1, Account acc2)
        {
            if (acc1.Currency != acc2.Currency)
            {
                throw new InvalidOperationException("Валюты счетов не совпадают.");
            }

            return acc1.Amount < acc2.Amount;
        }

        public static Account operator ++(Account acc)
        {
            decimal interestRate = 0.05m; // 5%
            acc.Amount += acc.Amount * interestRate;
            return acc;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Account acc1 = new Account();
            Account acc2 = new Account();

            Console.WriteLine("Введите данные для счета 1:");
            acc1.Input();

            Console.WriteLine("Введите данные для счета 2:");
            acc2.Input();

            Console.WriteLine("\nСчет 1:");
            acc1.Display();

            Console.WriteLine("Счет 2:");
            acc2.Display();

            try
            {
                Account acc3 = acc1 + acc2;
                Console.WriteLine("\nСчет 3 (слияние счетов 1 и 2):");
                acc3.Display();
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                Console.Write("\nВведите сумму для снятия со счета 1: ");
                decimal withdrawAmount = decimal.Parse(Console.ReadLine());
                acc1 = acc1 - withdrawAmount;
                Console.WriteLine("Счет 1 после снятия:");
                acc1.Display();
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            try
            {
                Console.Write("\nВведите сумму для пополнения счета 2: ");
                decimal depositAmount = decimal.Parse(Console.ReadLine());
                acc2 += depositAmount;
                Console.WriteLine("Счет 2 после пополнения:");
                acc2.Display();
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
            Console.WriteLine(ex.Message);
              }


        try
            {
                if (acc1 > acc2)
                {
                    Console.WriteLine("\nСчет 1 больше счета 2.");
                }
                else
                {
                    Console.WriteLine("\nСчет 2 больше счета 1.");
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                acc1++;
                Console.WriteLine("\nСчет 1 после начисления процентов:");
                acc1.Display();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
