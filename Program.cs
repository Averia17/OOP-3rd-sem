using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Lab08
{
    interface IFunction<T>
    {
        void Add(T element);
        void Remove(int pos);
        void Show();
    }
    public class CollectionType <T> : IFunction<T>
    {
        public T element;
        public List<T> collection;
        public CollectionType()
        {
            this.collection = new List<T>();
            this.element = default(T);
        }
        public CollectionType(T el)
        {
            this.collection = new List<T>();
            this.element = el;
        }

        public T Pop()
        {
            int lastElementIndex = this.collection.Count - 1;
            T lastElement = this.collection[lastElementIndex];
            this.collection.RemoveAt(lastElementIndex);
            return lastElement;
        }
        public void Add(T el)
        {
            if (el.Equals(0))
            {
                throw new Exception("You cannot add element with a value of 0");
            }
            collection.Add(el);
        }
        public void Show()
        {
            if (collection.Count == 0)
            {
                throw new Exception("Empty collection");
            }
            for (int i = 0; i < collection.Count; i++)
            {
                Console.WriteLine((i + 1) + " element of list: " + collection[i]);
            }
        }
        public void Remove(int pos)
        {
            this.collection.RemoveAt(pos);
        }
      
        public void ToFile( string path)
        {
            int index = collection.Count;
            string[] text = new string[index];
            for (int i = 0; i < index; i++)
            {
                text[i] = Convert.ToString(this.Pop());
            }
            File.WriteAllLines(path, text);
        }
        //Запись в json
        public void WriteToJson(string path)
        {
            string json = JsonConvert.SerializeObject(this.ToArray(), Formatting.Indented);
            System.IO.File.WriteAllText(path, json);
            Console.WriteLine("Data has been saved to file");
        }
        public void FromFile(string path)
        {
            Console.WriteLine(File.ReadAllText(path));
        }


        public T[] ToArray()
        {
            T[] lst = new T[collection.Count];
            for (int i = 0; i < collection.Count; i++)
            {
                lst[i] = collection[i];
            }

            return lst;

        }
    }
    public class Document
    {
        private readonly int id;
        private string title;
        private DateTime dateOfSignature; //дата подписи
        private string client;
        private string organization;
        public Document(string title, DateTime dateOfSignature, string client, string organization)
        {
            id = (int)title.GetHashCode() + (int)dateOfSignature.GetHashCode();
            this.title = title;
            this.dateOfSignature = dateOfSignature;
            this.client = client;
            this.organization = organization;
        }
        public string Client
        {
            get => client;
            set => client = value;

        }
       
        public string Organization
        {
            get => organization;
            set => organization = value;
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

    }
    class Program
    {
        public static object JsonSerializer { get; private set; }

        static async Task Main(string[] args)
        {
            string path = @"D:\ТРЕТИЙ СЕМЕСТР\ООП\lab 8\Lab08\Lab08\in.txt";
            string jpath = @"D:\ТРЕТИЙ СЕМЕСТР\ООП\lab 8\Lab08\Lab08\in.json";

            try
            {
                CollectionType<int> list = new CollectionType<int>();
                CollectionType<short> list1 = new CollectionType<short>(2);
                
                BinaryFormatter formatter = new BinaryFormatter();
                //list.Show();              //exception
                list.Add(9);
                //list.Add(0);              //exception
                list.Add(78);
                list.Add(22);
                list.Add(54);
                list.Add(20);
                list.Add(1);
                list.Add(58);
                list.Add(77);
                list.Show();
                CollectionType<Document> tom = new CollectionType<Document>();

                Document a = new Document("Check", new DateTime(2020 / 10 / 10), "Tabolich Artyom", "KFC");
                Document b = new Document("Check", new DateTime(2020 / 12 / 30), "Tabolich Artyom", "MacDonalds");
                Document c = new Document("Check", new DateTime(2020 / 10 / 10), "Tabolich Artyom", "KFC");
                tom.Add(a);
                tom.Add(b);
                tom.Add(c);

                list.ToFile(path);
                list.FromFile(path);

                tom.WriteToJson(jpath);

                Console.WriteLine("Without exceptions!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                Console.WriteLine("Block finally");
            }
        
        }
    }
}
