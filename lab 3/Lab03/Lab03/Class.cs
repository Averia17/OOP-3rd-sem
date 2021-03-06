﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab03
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

         //КОНСТРУКТОРЫ
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

        public Product(int upc, double price, DateTime storage_date, int quantity, string name = "some tea", string producer = "UK")
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
        public Product(string name, int upc, double price, DateTime storage_date)
        {
            ID = (uint)name.GetHashCode() + (uint)producer.GetHashCode();
            this.name = name;
            this.upc = upc;
            this.producer = "UK";
            this.price = price;
            this.storage_date = storage_date;
            this.quantity = 0;
            productAmount++;
        }
        private Product()
        {

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
        public int Quantaty
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
            output = $"Продукт: {Name}\n UPC: {UPC}\n Производитель: {Producer} \n Цена: {Price} \n Количество: {Quantaty}\n Дата изготовления: {Date.ToString("MM/dd/yyyy")}\n";
            return output; ;
        }
        
       

    }
}
