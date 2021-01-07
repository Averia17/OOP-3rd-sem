using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Lab16
{
    class Program
    {
        private const long maxNum = 100000;
        static BlockingCollection<string> products;
        static int productNumber = 0;
        private static CancellationTokenSource source = new CancellationTokenSource();
        private static CancellationToken token = source.Token;
        public static void SearchSimpleNumber()
        {
            for (long i = 2; i <= maxNum; i++)
            {
                if (isSimpleNumber(i))
                    Console.Write(i + ", ");
                if (token.IsCancellationRequested)
                {
                    Console.WriteLine("CancellationRequested\n");
                    return;
                }
            }
        }
        private static bool isSimpleNumber(long num)
        {
            for (long i = 2; i <= (num / 2); i++)
                if (num % i == 0)
                    return false;
            return true;
        }
        static void Main(string[] args)
        {
            int iteration = 3;
            Stopwatch stopwatch = new Stopwatch();
            /*while (iteration > 0)
            {
                stopwatch.Start();
                Task task = Task.Factory.StartNew(SearchSimpleNumber);
                Console.WriteLine($"\nЗадача {iteration}. ID: {task.Id.ToString()}");
                Console.WriteLine($"\nЗадача {iteration}. статус: {task.Status.ToString()}");
                task.Wait();
                stopwatch.Stop();
                Console.WriteLine($"\nВремя выполнения задачи {iteration}: {stopwatch.Elapsed.TotalMilliseconds.ToString()}\n");
                stopwatch.Reset();
                iteration--;
                Console.WriteLine("--------------------");
            }*/

           /* Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Task task = Task.Factory.StartNew(SearchSimpleNumber);
            Console.WriteLine($"\nID задачи: {task.Id.ToString()}");
            Console.WriteLine($"\nСтатус задачи: {task.Status.ToString()}");

            Console.WriteLine("q - для выхода ");
            if (Console.ReadKey().KeyChar == 'q')
                source.Cancel();

            task.Wait();
            stopwatch.Stop();
            Console.WriteLine($"\nВремя выполнения задачи: {stopwatch.Elapsed.TotalMilliseconds.ToString()}\n");
*/

            Tasks.Task3();
            Tasks.Task5();
            Tasks.Task6();


            products = new BlockingCollection<string>();
            Task[] cTasks = new Task[5];
            for (int i = 1; i <= 5; i++)
            {
                Task.Factory.StartNew(() => 
                    new Provider("продавец" + i, i * 1000).Provide(token)
                );
                Thread.Sleep(300);
                cTasks[i - 1] = Task.Factory.StartNew(() =>
                    new Customer("покупатель" + i, i * 125).Buy()
                );
                Thread.Sleep(300);
            }
            Thread.Sleep(1000);
            Task.WaitAll(cTasks);
            Console.WriteLine("\nПокупатели покинули магазин");
            source.Cancel();
        }
        class Customer
        {
            string name;
            int waitingTime;
            string product;
            public void Buy()
            {
                while (true)
                {
                    if (products.TryTake(out product))
                    {
                        Console.WriteLine(name + " купил " + product);
                    }
                    else
                    {
                        if (products.TryTake(out product, TimeSpan.FromMilliseconds(waitingTime)))
                        {
                            Console.WriteLine(name + " купил " + product);
                        }
                        else
                        {
                            Console.WriteLine(name + " покинул");
                            break;
                        }
                    }
                    Thread.Sleep(100);
                }
            }
            public Customer(string name, int waitingTime)
            {
                this.name = name;
                this.waitingTime = waitingTime;
            }
        }
        class Provider
        {
            string name;
            int providingTime;
            string product = "продукт";
            public void Provide(CancellationToken token)
            {
                while (true)
                {
                    Thread.Sleep(providingTime);
                    products.Add($"{product} {++productNumber}");
                    Console.WriteLine(name + " добавляет " + product + productNumber);
                    Thread.Sleep(100);
                    if (token.IsCancellationRequested) break;
                }
            }
            public Provider(string name, int providingTime)
            {
                this.name = name;
                this.providingTime = providingTime;
            }
        }
    }
}
