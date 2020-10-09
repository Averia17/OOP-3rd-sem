﻿using System;
using System.Collections.Generic;

namespace Lab06
{
    enum Doc
    {
        Receipt = 1, Waybill, Check
    }
    struct Information
    {
        public string document;
        public string client;
        public string organization;
    }
    interface IDocument
    {
        void Info();
    }
    public abstract class Document
    {
        private readonly int id;
        private string title;
        private DateTime dateOfSignature; //дата подписи
        private Client client;
        private Organization organization;
        public Document(string title, DateTime dateOfSignature, Client client, Organization organization)
        {
            id = (int)title.GetHashCode() + (int)dateOfSignature.GetHashCode();
            this.title = title;
            this.dateOfSignature = dateOfSignature;
            this.client = client;
            this.organization = organization;
        }
        public string Name
        {
            get => client.Name;
            set => client.Name = value;

        }
        public string Lastname
        {
            get => client.Lastname;
            set => client.Lastname = value;

        }
        public string NameOfOrganization
        {
            get => organization.NameOfOrganization;
            set => organization.NameOfOrganization = value;
        }
        public string Title
        {
            get => title;
            set => title = value;
        }
        public DateTime DateOfSignature
        {
            get => dateOfSignature;
            set => dateOfSignature = value;
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (obj.GetType() != this.GetType())
                return false;
            Document odin = (Document)obj;
            return this.Title == odin.Title;
        }
        //переопределение GetHashCode
        public override int GetHashCode()
        {
            int hash = 47, d = 32;
            string a = Convert.ToString(Title);
            hash = string.IsNullOrEmpty(a) ? 0 : Title.GetHashCode();
            hash = (hash * 47) + d.GetHashCode();
            return hash;
        }
        public abstract void Info();
        virtual public int GetTotalPrice() { return 0; }
    }
    sealed public class Receipt : Document, IDocument //квитанция
    {
        private int servicePrice;

        public Receipt(string title, DateTime dateOfSignature, Client client, Organization organization, int servicePrice)
            : base(title, dateOfSignature, client, organization)
        {
            this.servicePrice = servicePrice;
        }
        public override string ToString()   //переопределение метода(во всех классах)
        {
            return Title + " " + DateOfSignature.ToString("MM/dd/yyyy") + " " + Name + " " + Lastname + " " + NameOfOrganization + " " + servicePrice + "\n";
        }
        public override void Info()
        {
            Console.WriteLine("\t" + Title + "\n" + "Дата заключения: " + DateOfSignature.ToString("MM/dd/yyyy") + "\n" + "Клиент: " + Name + Lastname + "\n" + "Организация: " + NameOfOrganization + "\n" + "Итоговая стоимость: " + servicePrice);
        }
        override public int GetTotalPrice()
        {
            return servicePrice;
        }
    }
    sealed public class Waybill : Document, IDocument //накладная
    {
        private int servicePrice;
        public Waybill(string title, DateTime dateOfSignature, Client client, Organization organization, int servicePrice)
           : base(title, dateOfSignature, client, organization)
        {
            this.servicePrice = servicePrice;
        }
        public override string ToString()   //переопределение метода(во всех классах)
        {
            return Title + " " + DateOfSignature.ToString("MM/dd/yyyy") + " " + Name + " " + Lastname + " " + NameOfOrganization + " " + servicePrice + "\n";
        }
        public override void Info()
        {
            Console.WriteLine("\t" + Title + "\n" + "Дата заключения: " + DateOfSignature.ToString("MM/dd/yyyy") + "\n" + "Клиент: " + Name + Lastname + "\n" + "Организация: " + NameOfOrganization + "\n" + "Итоговая стоимость: " + servicePrice);
        }
        override public int GetTotalPrice()
        {
            return servicePrice;
        }
    }
    sealed public class Check : Document, IDocument //бесплодный класс - нельзя наследовать
    {
        private int totalPrice;
        public Check(string title, DateTime dateOfSignature, Client client, Organization organization, int totalPrice)
               : base(title, dateOfSignature, client, organization)
        {
            this.totalPrice = totalPrice;
        }
        public override string ToString()   //переопределение метода(во всех классах)
        {
            return Title + " " + DateOfSignature.ToString("MM/dd/yyyy") + " " + Name + " " + Lastname + " " + NameOfOrganization + " " + totalPrice + "\n";
        }
        public override void Info()
        {
            Console.WriteLine("\t" + Title + "\n" + "Дата заключения: " + DateOfSignature.ToString("MM/dd/yyyy") + "\n" + "Клиент: " + Name + Lastname + "\n" + "Организация: " + NameOfOrganization + "\n" + "Итоговая стоимость: " + totalPrice);
        }
        override public int GetTotalPrice()
        {
            return totalPrice;
        }

    }
    public class Client
    {
        private string name;
        private string lastname;

        public Client(string name, string lastname)
        {
            this.name = name;
            this.lastname = lastname;

        }
        public string Name
        {
            get => name;
            set => name = value;
        }
        public string Lastname
        {
            get => lastname;
            set => lastname = value;
        }
    }
    public class Organization
    {
        private string nameOfOrganization;
        public Organization(string nameOfOrganization)
        {
            this.nameOfOrganization = nameOfOrganization;
        }
        public string NameOfOrganization
        {
            get => nameOfOrganization;
            set => nameOfOrganization = value;
        }
    }
    public static class Printer
    {
        public static void IAmPrinting(Document document)
        {
            Console.WriteLine(document.GetType());
            Console.WriteLine(document.ToString());
        }
    }


    public class Bookkeeping
    {
        List<Receipt> receipts = new List<Receipt>();
        List<Waybill> waybills = new List<Waybill>();
        List<Check> checks = new List<Check>();

        public Receipt this[int index]
        {
            get
            {
                if (index > receipts.Count)
                {
                    Console.WriteLine("Превышен максимальный индекс списка квитанций");
                    return null;
                }
                return receipts[index];
            }
            set
            {
                if (index > receipts.Count)
                    Console.WriteLine("Элемента с таким индексом не существует");
                else
                    receipts[index] = value;
            }
        }
        public void AddReceipt(Receipt a) { receipts.Add(a); }
        public void AddWaybill(Waybill a) { waybills.Add(a); }
        public void AddCheck(Check a) { checks.Add(a); }
        public void DelReceipt(Receipt a) { receipts.Remove(a); }
        public void DelWaybill(Waybill a) { waybills.Remove(a); }
        public void DelCheck(Check a) { checks.Remove(a); }

        public void ShowList()
        {
            Console.WriteLine("List of Receipts: ");
            foreach(Receipt item in receipts)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine("List of Waybills: ");
            foreach (Waybill item in waybills)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine("List of Checks: ");
            foreach (Check item in checks)
            {
                Console.WriteLine(item.ToString());
            }
        }
        public int GetWaybillPrice(string name)
        {
            int price = 0;
            foreach(Waybill item in waybills)
            {
                if(item.Title == name)
                    price += item.GetTotalPrice();
            }    
            return price;
        }
        public int GetCheckCount()
        {
            return checks.Count;
        }
        public void GetDocuments(DateTime firstDate, DateTime lastDate)
        {
            foreach (Receipt item in receipts)
                if (item.DateOfSignature > firstDate && item.DateOfSignature < lastDate)
                    Console.WriteLine(item.ToString());
            foreach (Waybill item in waybills)
                if (item.DateOfSignature > firstDate && item.DateOfSignature < lastDate)
                    Console.WriteLine(item.ToString());
            foreach (Check item in checks)
                if (item.DateOfSignature > firstDate && item.DateOfSignature < lastDate)
                    Console.WriteLine(item.ToString());
        }

        public class BookkeepingController
        {
            public void Show(Bookkeeping lib) { lib.ShowList(); }
            public int Price(Bookkeeping lib, string name) { int a = lib.GetWaybillPrice(name); return a; }
            public int Count(Bookkeeping lib) { int a = lib.GetCheckCount(); return a; }
            public void Get(Bookkeeping lib, DateTime firstDate, DateTime lastDate) { lib.GetDocuments(firstDate, lastDate);}
        }
    }
}
