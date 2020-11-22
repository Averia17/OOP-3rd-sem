using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab09
{
    class Program
    {
        private static void DisplayRedMessage(String message)
        {
            // Устанавливаем красный цвет символов
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            // Сбрасываем настройки цвета
            Console.ResetColor();
        }
        static void Main(string[] args)
        {
            List<string> FirstList = new List<string>() { "papka", "mamka", "file", "label", "image", "programm" };
            Programmer programmer = new Programmer(FirstList);
            Console.WriteLine("Первое задание \n");

            programmer.Show();

            programmer.DeleteEvent += (string message) => Console.WriteLine(message); 

            programmer.MutateEvent += (string message) => // += DisplayRedMessage(string message)
            {
                // Устанавливаем красный цвет символов
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(message);
                // Сбрасываем настройки цвета
                Console.ResetColor();
            };


            programmer.Delete();
            programmer.Show();

            programmer.Mutate();

            programmer.Show();

            programmer.Mutate();
            
            programmer.Show();

            Console.WriteLine("\nВторое задание \n");

            string str = "Д*Е!Л*Е!Г*А!Т*Ы     очень     СТРАН!!!!НА!!!!Я вещь";
            Func<string, string> A = null;
            A += SecondTask.DelZnak;
            Console.WriteLine("До: {0}\nПосле: {1}\n", str, str = A(str));
            A += SecondTask.DelProbel;
            Console.WriteLine("До: {0}\nПосле: {1}\n", str, str = A(str));
            A += SecondTask.Upper;
            Console.WriteLine("До: {0}\nПосле: {1}\n", str, str = A(str));
            A += SecondTask.Letter;
            Console.WriteLine("До: {0}\nПосле: {1}\n", str, str = A(str));

        }
    }
}
