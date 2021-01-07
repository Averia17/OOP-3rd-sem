using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization.Formatters.Soap;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Xml;

namespace Lab14
{
    [Serializable]  //объект доступен для служб сериализации
    [DataContract]  //контракт данных - тип, объект которого описывает информационный фрагмент
    public abstract class Document
    {
        private readonly int id;
        private string title;
        private DateTime dateOfSignature; //дата подписи
        public Client client;
        public Organization organization;
        public Document(string title, DateTime dateOfSignature, Client client, Organization organization)
        {
            id = (int)title.GetHashCode() + (int)dateOfSignature.GetHashCode();
            this.title = title;
            this.dateOfSignature = dateOfSignature;
            this.client = client;
            this.organization = organization;
        }
        public Document()
        {

        }
        [DataMember]
        public string Title
        {
            get => title;
            set => title = value;
        }
        [DataMember]
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
    [Serializable]
    [DataContract]
    sealed public class Receipt : Document //квитанция
    {
        [NonSerialized]
        private int servicePrice;

        public Receipt(string title, DateTime dateOfSignature, Client client, Organization organization, int servicePrice)
            : base(title, dateOfSignature, client, organization)
        {
            this.servicePrice = servicePrice;
        }
        public override string ToString()   //переопределение метода(во всех классах)
        {
            return Title + " " + DateOfSignature.ToString("MM/dd/yyyy") + " " + client.Name + " " + client.Lastname + " " + organization.NameOfOrganization + " " + servicePrice + "\n";
        }
        public override void Info()
        {
            Console.WriteLine("\t" + Title + "\n" + "Дата заключения: " + DateOfSignature.ToString("MM/dd/yyyy") + "\n" + "Клиент: " + client.Name + client.Lastname + "\n" + "Организация: " + organization.NameOfOrganization + "\n" + "Итоговая стоимость: " + servicePrice);
        }
        override public int GetTotalPrice()
        {
            return servicePrice;
        }
    }
    [Serializable]
    [DataContract]
    sealed public class Waybill : Document //накладная
    {
        [NonSerialized]
        private int servicePrice;
        public Waybill(string title, DateTime dateOfSignature, Client client, Organization organization, int servicePrice)
           : base(title, dateOfSignature, client, organization)
        {
            this.servicePrice = servicePrice;
        }
        public override string ToString()   //переопределение метода(во всех классах)
        {
            return Title + " " + DateOfSignature.ToString("MM/dd/yyyy") + " " + client.Name + " " + client.Lastname + " " + organization.NameOfOrganization + " " + servicePrice + "\n";
        }
        public override void Info()
        {
            Console.WriteLine("\t" + Title + "\n" + "Дата заключения: " + DateOfSignature.ToString("MM/dd/yyyy") + "\n" + "Клиент: " + client.Name + client.Lastname + "\n" + "Организация: " + organization.NameOfOrganization + "\n" + "Итоговая стоимость: " + servicePrice);
        }
        override public int GetTotalPrice()
        {
            return servicePrice;
        }
    }
    [Serializable]
    [DataContract]
    sealed public class Check : Document //бесплодный класс - нельзя наследовать
    {
        [NonSerialized]
        private int totalPrice;
        public Check(string title, DateTime dateOfSignature, Client client, Organization organization, int totalPrice)
               : base(title, dateOfSignature, client, organization)
        {
            this.totalPrice = totalPrice;
        }
        public Check() 
        { 
        }
        public override string ToString()   //переопределение метода(во всех классах)
        {
            return Title + " " + DateOfSignature.ToString("MM/dd/yyyy") + " " + client.Name + " " + client.Lastname + " " + organization.NameOfOrganization + " " + totalPrice + "\n";
        }
        public override void Info()
        {
            Console.WriteLine("\t" + Title + "\n" + "Дата заключения: " + DateOfSignature.ToString("MM/dd/yyyy") + "\n" + "Клиент: " + client.Name + client.Lastname + "\n" + "Организация: " + organization.NameOfOrganization + "\n" + "Итоговая стоимость: " + totalPrice);
        }
        override public int GetTotalPrice()
        {
            return totalPrice;
        }

    }
    [Serializable]
    [DataContract]
    public class Client
    {
        private string name;
        private string lastname;

        public Client(string name, string lastname)
        {
            this.name = name;
            this.lastname = lastname;
        }
        public Client()
        {

        }
        [DataMember]
        public string Name
        {
            get => name;
            set => name = value;
        }
        [DataMember]
        public string Lastname
        {
            get => lastname;
            set => lastname = value;
        }
    }
    [Serializable]
    [DataContract]
    public class Organization
    {
        private string nameOfOrganization;
        public Organization(string nameOfOrganization)
        {
            this.nameOfOrganization = nameOfOrganization;
        }
        public Organization()
        { }
        [DataMember]
        public string NameOfOrganization
        {
            get => nameOfOrganization;
            set => nameOfOrganization = value;
        }
    }
}
