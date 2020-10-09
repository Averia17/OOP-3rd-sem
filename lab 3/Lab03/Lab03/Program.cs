using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Lab03
{
    public partial class Product
    {
        public int GetMethod(ref int x, out int y)
        {
            x++;
            y = x;
            return y;
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            Product first_tea = new Product("tea", 222211,"India", 5.9, new DateTime(2020, 5, 8), 21);
            Product second_tea = new Product("tea", 2332211, "UK", 9.59, new DateTime(2019, 6, 1), 21);
            Product third_tea = new Product("tea", 111345, "Russia", 7.29, new DateTime(2020, 7, 16), 21);
            Product fours_tea = new Product("tea", 667721, "UK", 10.95, new DateTime(2019, 8, 15), 21);
            Product fives_tea = new Product("tea", 2332211, "UK", 8.9, new DateTime(2018, 9, 22), 21);
            Product table = new Product("table", 111223, "belarus", 120.5, new DateTime(2015, 10, 18), 11);
            Product computer = new Product("computer", 2299922, "china", 1119.9, new DateTime(2017, 11, 15), 12);
            Product vape = new Product("vape", 2111122, "turkmenistan", 159.9, new DateTime(2018, 1, 30), 21);
            Product phone = new Product("phone", 22, "china", 599.9, new DateTime(2017, 2, 18), 7);

            Product []Tea_List = { first_tea, second_tea, third_tea, fours_tea, fives_tea, table, computer, vape, phone };



            Console.WriteLine("Введите цену: ");
            int price = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите наименование: ");
            string name = Console.ReadLine();

            foreach (Product item in Tea_List)
            {
                if (item.Name == name)
                    Console.WriteLine(item.ToString());
            }

            foreach (Product item in Tea_List)
            {
                if (item.Price <= price && item.Name == name)
                    Console.WriteLine("Цену меньше {0}, имеет продукт {1}, цена которого = {2}, производителем которого является {3}", price, item.Name, item.Price, item.Producer);
            }
        
            var car = new Product("car", 2333332, "russia", 24599.9, new DateTime(2017, 2, 18), 2 );

            Console.WriteLine("\nАнонимный тип: \n{0}\n Общая стоимость {1} = {2}", car.ToString(), car.Name, car.GetTotalPrice());
            //Console.WriteLine(first_tea.GetTotalPrice());



        }
    }
}
