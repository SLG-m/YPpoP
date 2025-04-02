using System;
using System.Collections.Generic;

namespace MobilePhoneCollection
{
    enum Country
    {
        China,
        USA,
        SouthKorea,
        Vietnam,
        Japan,
        Germany,
        Other
    }

     struct MobilePhone
    {
        public string Model;
        public int Year;
        public decimal Price;
        public Country ManufacturerCountry;

        public override string ToString()
        {
            return $"{Model} ({Year}), {Price:N2} BYN , Made in {ManufacturerCountry}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Stack<MobilePhone> phones = new Stack<MobilePhone>();

            phones.Push(new MobilePhone { Model = "iPhone 13", Year = 2021, Price = 999, ManufacturerCountry = Country.USA });
            phones.Push(new MobilePhone { Model = "Samsung Galaxy S21", Year = 2021, Price = 799, ManufacturerCountry = Country.SouthKorea });
            phones.Push(new MobilePhone { Model = "Xiaomi Mi 11", Year = 2021, Price = 749, ManufacturerCountry = Country.China });
            phones.Push(new MobilePhone { Model = "Bphone B86", Year = 2021, Price = 1200, ManufacturerCountry = Country.Vietnam });
            phones.Push(new MobilePhone { Model = "VinSmart Vsmart", Year = 2020, Price = 200, ManufacturerCountry = Country.Vietnam });
            phones.Push(new MobilePhone { Model = "Nokia 8.3", Year = 2020, Price = 699, ManufacturerCountry = Country.Other });

            Console.WriteLine("Исходная коллекция телефонов:");
            PrintCollection(phones);

            // 1. Вставка нового телефона перед заданным
            string targetModel = "Samsung Galaxy S21";
            MobilePhone newPhone = new MobilePhone { Model = "Google Pixel 6", Year = 2021, Price = 899, ManufacturerCountry = Country.USA };
            phones = InsertBefore(phones, targetModel, newPhone);

            Console.WriteLine("\nКоллекция после вставки нового телефона:");
            PrintCollection(phones);

            // 2. Удаление вьетнамских телефонов дороже указанной цены
            decimal maxPrice = 1000;
            phones = RemoveVietnamesePhonesAbovePrice(phones, maxPrice);

            Console.WriteLine($"\nКоллекция после удаления вьетнамских телефонов дороже {maxPrice:N2} BYN:");
            PrintCollection(phones);

            // 3. Подсчет телефонов указанного года выпуска
            int targetYear = 2021;
            int count = CountPhonesByYear(phones, targetYear);

            Console.WriteLine($"\nКоличество телефонов {targetYear} года выпуска: {count}");
        }

        static void PrintCollection(Stack<MobilePhone> phones)
        {
            foreach (var phone in phones)
            {
                Console.WriteLine(phone);
            }
        }

        // 1. Метод для вставки нового телефона перед заданным
        static Stack<MobilePhone> InsertBefore(Stack<MobilePhone> original, string targetModel, MobilePhone newPhone)
        {
            Stack<MobilePhone> temp = new Stack<MobilePhone>();
            Stack<MobilePhone> result = new Stack<MobilePhone>();
            bool found = false;

            while (original.Count > 0)
            {
                var phone = original.Pop();
                if (phone.Model == targetModel && !found)
                {
                    result.Push(newPhone);
                    found = true;
                }
                result.Push(phone);
            }

            while (result.Count > 0)
            {
                temp.Push(result.Pop());
            }

            return temp;
        }

        // 2. Метод для удаления вьетнамских телефонов дороже указанной цены
        static Stack<MobilePhone> RemoveVietnamesePhonesAbovePrice(Stack<MobilePhone> original, decimal maxPrice)
        {
            Stack<MobilePhone> result = new Stack<MobilePhone>();

            foreach (var phone in original)
            {
                if (!(phone.ManufacturerCountry == Country.Vietnam && phone.Price > maxPrice))
                {
                    result.Push(phone);
                }
            }

            Stack<MobilePhone> reversed = new Stack<MobilePhone>();
            while (result.Count > 0)
            {
                reversed.Push(result.Pop());
            }

            return reversed;
        }

        // 3. Метод для подсчета телефонов указанного года выпуска
        static int CountPhonesByYear(Stack<MobilePhone> phones, int year)
        {
            int count = 0;
            foreach (var phone in phones)
            {
                if (phone.Year == year)
                {
                    count++;
                }
            }
            return count;
        }
    }
}