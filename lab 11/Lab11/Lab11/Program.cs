using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab11
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] months = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            int n;
            Console.Write("Enter string length n:");
            n = int.Parse(Console.ReadLine());
            Console.WriteLine("1. Months with a string length of " + n + ":");
            IEnumerable<string> length = months
                .Where(p => p.Length == n)
                .Select(p => p);
            //months[2] = "Mar";
            foreach (string month in length) Console.WriteLine(month);
            Console.WriteLine();
            Console.WriteLine("2. Summer and winter months:");
            IEnumerable<string> sumwin = from p in months
                                         where p == "January" || p == "February" || p == "December" || p == "June" || p == "July" || p == "August"
                                         select p;
            foreach (string month in sumwin) Console.WriteLine(month);
            Console.WriteLine();

            Console.WriteLine("3. Months in alphabetical order:");
            IEnumerable<string> alph = from p in months orderby p select p;

            foreach (string month in alph) Console.WriteLine(month);
            Console.WriteLine();

            Console.WriteLine("4. Months containing a letter 'u' and a length >= 4:");
            IEnumerable<string> contU = from p in months where p.Contains('u') && p.Length >= 4 select p;
            foreach (string month in contU) Console.WriteLine(month);
            Console.WriteLine("\n---------------------------------------------------\n");

            List<Product> products = new List<Product>() { new Product("green tea", 222211, "India", 5.9, new DateTime(2020, 5, 8), 19),
                                                           new Product("black tea", 2332211, "USA", 9.59, new DateTime(2019, 6, 1), 12),
                                                           new Product("white tea", 111345, "Russia", 7.29, new DateTime(2020, 7, 16), 33),
                                                           new Product("tea", 667721, "UK", 10.95, new DateTime(2019, 8, 15), 12),
                                                           new Product("tea", 2332211, "Belarus", 8.9, new DateTime(2018, 9, 22), 88),
                                                           new Product("table", 111223, "Belarus", 120.5, new DateTime(2015, 10, 18), 7),
                                                           new Product("computer", 2299922, "China", 1119.9, new DateTime(2017, 11, 15), 38),
                                                           new Product("vape", 2111122, "Turkmenistan", 159.9, new DateTime(2018, 1, 30), 16),
                                                           new Product("phone", 22, "China", 599.9, new DateTime(2017, 2, 18), 7),
                                                           new Product("phone", 3232322, "Belarus", 499.9, new DateTime(2018, 6, 18), 10) };


            //список товаров для заданного наименования
            Console.Write("Enter name:");
            string name = Console.ReadLine();
            var lst1 = from prod in products where prod.Name == name select prod;
            foreach (var prod in lst1) Console.WriteLine(prod.ToString());
            //список товаров для заданного наименования, цена которых не превосходит заданную;
            Console.Write("Enter price:");
            int price = int.Parse(Console.ReadLine());
            lst1 = from prod in lst1 where prod.Price <= price select prod;
            foreach (var prod in lst1) Console.WriteLine(prod.ToString());
            //количество наименований цена которых больше 100
            Console.WriteLine($"Number of products which price is more then 100: {(from prod in products where prod.Price > 100 select prod).Count()}");
            //максимальный товар (по цене)
            Console.WriteLine($"Highest price of products: {(from prod in products select prod.Price).Max()}"); //products.Select(prod => prod).Max(prod => prod.Price)}
            var sortProd = products.OrderBy(p => p.Producer).ThenBy(p => p.Quantity).Select(p => p);
            foreach (var prod in sortProd) Console.WriteLine(prod.ToString());

            Console.WriteLine("\n---------------------------------------------------\n");

            Console.WriteLine("Own request:");
            var own = products
                .Where(p => p.Quantity < 20)
                .OrderBy(p => p.Quantity)
                .ThenByDescending(p => p.Name.Length)
                .Where(p => p.Name.Contains("tea"))
                .Select(p => p);
            foreach (var o in own) Console.WriteLine(o.ToString());

            Console.WriteLine("\n---------------------------------------------------\n");

            List<Person> owners = new List<Person>() { new Person("Artyom", "computer"), new Person("Vitalik", "black tea"), new Person("Vova", "vape"), new Person("Denis", "phone") };
            
            var result = products.Join(owners, // второй набор
                         p => p.Name, // свойство-селектор объекта из первого набора
                         o => o.Item, // свойство-селектор объекта из второго набора
                         (p, o) => new { Owner = o.Name, Name = p.Name, Producer = p.Producer, Price = p.Price }); // результат

            foreach (var item in result)
                Console.WriteLine($"{item.Owner} is owner of {item.Name} ({item.Producer}, {item.Price})");
        }
    }
}