using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab11
{
    public partial class Product
    {
        private readonly uint ID;
        private static int productAmount;
        private string name;
        private int upc;
        private string producer;
        private double price;
        private DateTime storage_date;
        private int quantity;
        private int max_quantity = 100;

        public Product(string name, int upc, string producer, double price, DateTime storage_date, int quantity)
        {
            ID = (uint)name.GetHashCode() + (uint)producer.GetHashCode();
            this.name = name;
            this.upc = upc;
            this.producer = producer;
            this.price = price;
            this.storage_date = storage_date;
            this.quantity = quantity;

            productAmount++;
        }
        
        static Product()
        {
            productAmount = 0;
        }
        public static int getAmount()
        {
            return productAmount;
        }

        //ПРОВЕРКА 
        public string Name
        {
            get => name;
            set
            {
                if (value.Length > 3 && value.Length < 50 && !value.Contains("123456789"))
                    name = value;
                else
                    throw new Exception("Неверное наименование.");
            }

        }
        public int UPC
        {
            get => upc;
            set
            {
                if (value > 100000)
                    upc = value;
                else
                    throw new Exception("Неверный формат UPC.");
            }
        }
        public string Producer
        {
            get => producer;
            set
            {
                if (value.Length > 3 && value.Length < 50 && !value.Contains("123456789"))
                    producer = value;
                else
                    throw new Exception("Неверный producer.");
            }

        }

        public double Price
        {
            get => price;
            set
            {
                if (value > 0)
                    price = value;
                else
                    throw new Exception("Неверный формат цены.");
            }
        }
        public int Quantity
        {
            get => quantity;
            set
            {
                if (value >= 0)
                    quantity = value;
                else
                    throw new Exception("Неверный формат количества.");
            }
        }
        public DateTime Date
        {
            get => storage_date;
            private set { }
        }
        public double GetTotalPrice()
        {
            return quantity * price;
        }
        public override string ToString()
        {
            string output = $"-------- Информация о продукте -------\nD\n";
            output = $"Продукт: {Name}\n UPC: {UPC} Производитель: {Producer} \n Цена: {Price} Количество: {Quantity}\n Дата изготовления: {Date.ToString("MM/dd/yyyy")}\n";
            return output; ;
        }
        public override bool Equals(object obj)
        {
            return this.GetHashCode() == obj.GetHashCode();
        }
        public override int GetHashCode()//переопределение метода GetHashCode
        {
            // 269 или 47 простые
            int hash = 269;
            hash = string.IsNullOrEmpty(name) ? 0 : name.GetHashCode();
            hash = (hash * 47) + producer.GetHashCode();
            return hash;
        }
      

    }
    public class Person
    {
        public string Name { get; set; }
        public string Item { get; set; }
        public Person(string Name, string Item)
        {
            this.Name = Name;
            this.Item = Item;
        }
    }
}
