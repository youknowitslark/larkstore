using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;

namespace QueueAtStore
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Магазин у дома";
            Console.CursorVisible = false;

            Random randomGenerate = new Random();

            string[] names = File.ReadAllLines("names.txt");
            string[] products = File.ReadAllLines("products.txt");
            string[] productPrices = File.ReadAllLines("prices.txt");

            int mixNamesIndex = 0;
            int maxNamesIndex = names.Length;
            int namesIndex;
            int minCartSize = 1;
            int maxCartSize = 10;
            int cartSize;
            int minQueueSize = 5;
            int maxQueueSize = 15;
            int queueSize = randomGenerate.Next(minQueueSize, maxQueueSize);
            int minProductsIndex = 0;
            int maxProductsIndex = products.Length;
            int productsIndex;
            int cartPrice = 0;
            int revenue = 0;

            Queue<string> clients = new Queue<string>();

            for (int i = 0; i < queueSize; i++) 
            {
                namesIndex = randomGenerate.Next(mixNamesIndex, maxNamesIndex);
                clients.Enqueue(names[namesIndex]);
            }

            for (int i = 0; i < queueSize; i++)
            {
                Console.WriteLine($"Выручка: {revenue} руб.");
                Console.WriteLine($"Осталось человек в очереди: {clients.Count}");
                Console.Write($"Текущий клиент: {clients.Dequeue()} ");

                if (clients.Count != 0)
                    Console.Write($"(следующий клиент: {clients.Peek()})");

                Console.WriteLine("\n");
                Console.WriteLine("Корзина:");

                cartSize = randomGenerate.Next(minCartSize, maxCartSize);

                for (int j = 0; j < cartSize; j++)
                {
                    productsIndex = randomGenerate.Next(minProductsIndex, maxProductsIndex);
                    Console.Write($"{j + 1}. {products[productsIndex]} - {productPrices[productsIndex]} руб.");
                    cartPrice += int.Parse(productPrices[productsIndex]);
                    Console.WriteLine();
                }

                Console.Write("\n");
                Console.WriteLine($"Итого: {cartPrice} руб.\n");

                Console.Write("Для обслуживания клиента нажмите на любую клавишу . . .");
                Console.ForegroundColor = ConsoleColor.Black;
                Console.ReadKey();
                Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine("\nПробиваем товар . . .");
                Thread.Sleep(1000);
                Console.WriteLine("Принимаем оплату . . .");
                Thread.Sleep(1000);

                revenue += cartPrice;
                cartPrice = 0;

                Console.WriteLine("Успешно!");
                Thread.Sleep(1000);
                Console.Clear();
            }

            Console.WriteLine($"Выручка: {revenue} руб.");
            Console.WriteLine("Нажмите на любую клавишу для выхода . . .");
            Console.ReadKey();
        }
    }
}
