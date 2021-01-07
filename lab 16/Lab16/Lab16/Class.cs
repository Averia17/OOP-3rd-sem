using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab16
{
    public class Tasks
    {
        const int capacity = 100;
        const int density = 1000;
        const int weight = 6000;

        public static int GetWeight() => capacity * density;
        public static int GetDensity() => weight / capacity;
        public static int GetCapacity() => weight / density;

        public static async void Task3()
        {
            Task<int> f1 = Task.Factory.StartNew(GetWeight);
            Task<int> f2 = Task.Factory.StartNew(GetDensity);
            Task<int> f3 = Task.Factory.StartNew(GetCapacity);

            await f1.ContinueWith((firstTask) => Console.WriteLine($"\nРезультат GetWeight: {firstTask.Result}"));
            await f2.ContinueWith((secondTask) => Console.WriteLine($"\nРезультат GetDensity: {secondTask.Result}"));
            await f3.ContinueWith((thirdTask) => Console.WriteLine($"\nРезультат GetCapacity: {thirdTask.Result}"));

            f3.ContinueWith((thirdTask) => Console.WriteLine($"\nРезультат GetCapacity с GetAwaiter(): {thirdTask.Result}")).GetAwaiter();
            f2.ContinueWith((secondTask) => Console.WriteLine($"\nРезультат GetDensity с GetAwaiter().GetResult(): {secondTask.Result}")).GetAwaiter().GetResult();

        }
        public static void Task5()
        {
            const int arrSize = 10000000;
            const int arrCount = 10;
            Stopwatch stopwatch = Stopwatch.StartNew();
            Parallel.For(0, arrCount, (Count) =>
            {
                int[] arr = new int[arrSize];
                Parallel.ForEach(arr, (el) =>
                {
                    el = arrCount * arrCount;
                });
            });
            stopwatch.Stop();
            Console.WriteLine("\nTime with Parallel.For, Parallel.ForEach: " + stopwatch.Elapsed.Milliseconds.ToString());
            stopwatch.Restart();
            for (int i = 0; i < arrCount; i++)
            {
                int[] arr = new int[arrSize];
                for (int j = 0; j < arr.Length; j++)
                    arr[j] = arrCount * arrCount;
            }
            stopwatch.Stop();
            Console.WriteLine("\nTime with two for operators: " + stopwatch.Elapsed.Milliseconds.ToString());
        }
        public static void Task6()
        {
            Parallel.Invoke(AddInArray, AddInArray, AddInArray);
        }
        private static void AddInArray()
        {
            int[] arr = new int[1000000];
            for (int i = 0; i < arr.Length; i++)
                arr[i] = i * i;
            Console.WriteLine("\nParallel.Invoke закончил работу.\n");
        }
        
    }

}