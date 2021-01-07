using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab15
{
    class Program
    {
        private static object locker = new object();
        private static string oddAbdEvenNum = "";
        public static void ToWriteNum()
        {
            try
            {
                /*Console.WriteLine("Input n: ");
                int n = int.Parse(Console.ReadLine());*/
                int n = 10;
                for (int i = 0; i < n; i++)
                {
                    using (StreamWriter sw = new StreamWriter(@"D:\ТРЕТИЙ СЕМЕСТР\ООП\lab 15\Lab15\Lab15\nums.txt", true))
                    {
                        sw.WriteLine(i);
                        Console.WriteLine(Thread.CurrentThread.Name + " выводит " + i);
                        Thread.Sleep(100);
                    }
                }
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void OddAndEvenNumbersToConsole(object num)
        {
            // true = odd numbers, false = even numbers
            lock (locker)
            {
                ValueTuple<int, bool> n = (ValueTuple<int, bool>)num;
                for (int i = 0; i < n.Item1; i++)
                {
                    if (i % 2 == 1 && n.Item2)
                    {
                        string str = "Odd Thread: " + i + '\n';
                        Console.Write(str);
                        oddAbdEvenNum += str;
                        //Thread.Sleep(0);
                        //Thread.Sleep(100);
                    }
                    if (i % 2 == 0 && !n.Item2)
                    {
                        string str = "Even Thread: " + i + '\n';
                        Console.Write(str);
                        oddAbdEvenNum += str;
                        //Thread.Sleep(0);
                        //Thread.Sleep(100);
                    }
                }
            }

        }
        static void TimerMethod(object count)
        {
            int n = (int)count;
            for (int i = 1; i < 9; i++, n++)
                Console.WriteLine("Timer " + (n * i));
        }
        static void Main(string[] args)
        {
            using (StreamWriter sw = new StreamWriter(@"D:\ТРЕТИЙ СЕМЕСТР\ООП\lab 15\Lab15\Lab15\processes.txt"))
            {
                Process[] allProcesses = Process.GetProcesses();    //получение всех процессов
                foreach (Process p in allProcesses)          //при запуске приложения ОС создает для него процесс,
                {                                           //которому выделяется определенное адр пр-во в памяти
                    sw.WriteLine("ID: " + p.Id);
                    sw.WriteLine("Process name: " + p.ProcessName);
                    sw.WriteLine("Priority: " + p.BasePriority);
                    sw.WriteLine("Responding: " + p.Responding);        //отвечает ли пользовательский интерфейс
                    sw.WriteLine("WorkingSet64: " + p.WorkingSet64);    //объем памяти
                    //sw.WriteLine("Start at: " + p.StartTime);             //отсутствие доступа
                    //sw.WriteLine("Total processor time: " + p.TotalProcessorTime);
                    sw.WriteLine();
                }
            }
            //2
            AppDomain domain = AppDomain.CurrentDomain;               //домен приложения - отдельный логический раздел внутри процесса
            Console.WriteLine("Current domain");
            Console.WriteLine("Name: " + domain.FriendlyName);
            Console.WriteLine("Configuration: " + domain.SetupInformation);
            Console.WriteLine("Directory: " + domain.BaseDirectory);
            Console.WriteLine("Assemblies of current domain:");
            Assembly[] assemblies = domain.GetAssemblies();
            foreach (Assembly a in assemblies)
            {
                Console.WriteLine(a.GetName().Name);
            }
            //Создайте новый домен.
            AppDomain newDomain = AppDomain.CreateDomain("New domain");
            //Загрузите туда сборку.
            newDomain.Load(new AssemblyName("Lab15"));
            Console.WriteLine("Name of the new domain: " + newDomain.FriendlyName + "\nAssamblies of new domain:");
            foreach (Assembly a in newDomain.GetAssemblies())
            {
                Console.WriteLine(a.GetName().Name);
            }
            //Выгрузите домен.
            AppDomain.Unload(newDomain);

            Console.WriteLine("\nThreads");                         
            Thread thread = new Thread(ToWriteNum);                 //процесс содержит мин один поток - главный
            thread.Start();
            thread.Name = "MyThread1";
            Thread mainThread = Thread.CurrentThread;
            mainThread.Name = "MainTread";
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(Thread.CurrentThread.Name + ' ' + i);
                Thread.Sleep(50);
            }
            Thread.Sleep(1000);

            Console.WriteLine("Thread name: " + mainThread.Name);
            Thread.Sleep(100);
            //mainTread.Abort();
            Console.WriteLine("State: " + mainThread.ThreadState);
            Thread.Sleep(100);
            Console.WriteLine("Priority: " + mainThread.Priority);
            Thread.Sleep(1000);


            Thread oddThread = new Thread(OddAndEvenNumbersToConsole);
            oddThread.Name = "oddThread";
            oddThread.Priority = ThreadPriority.Lowest;
            Thread evenThread = new Thread(OddAndEvenNumbersToConsole);
            evenThread.Name = "evenThread";
            evenThread.Priority = ThreadPriority.Lowest;
            int number = 10;
            ValueTuple<int, bool> oddnum = (number, true);
            ValueTuple<int, bool> evennum = (number, false);

            oddThread.Start(oddnum);
            evenThread.Start(evennum);

            Thread.Sleep(1000);
            TimerCallback tm = new TimerCallback(TimerMethod);
            Timer timer = new Timer(tm, 10, 0, 2000);
            Console.ReadLine();
        }
    }
}
