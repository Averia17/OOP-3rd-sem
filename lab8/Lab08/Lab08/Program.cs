using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Lab08
{
    interface IFunction<T>
    {
        void Add(T element);
        void Remove(int pos);
        void Show();
    }

    public class CollectionType<T> :IFunction<T>
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
                Console.WriteLine((i + 1) + " element of list: " + collection[i].ToString());
            }
        }
      
        public void Remove(int pos)
        {
            this.collection.RemoveAt(pos);
        }
        public void Save(string CurrentFile)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream fs = new FileStream(CurrentFile, FileMode.OpenOrCreate))
            {
                bf.Serialize(fs, collection);
                fs.Close();
            }
        }

        public void Upload(string CurrentFile)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream fs = new FileStream(CurrentFile, FileMode.OpenOrCreate))
            {
                List<T> deser = (List<T>)bf.Deserialize(fs);
                foreach (T p in deser)
                {
                    if (p == null)
                        continue;
                    this.Add(p);
                }
                fs.Close();
            }
        }
        
        //Запись в json
        public void WriteToJson(string path)
        {
            string json = JsonConvert.SerializeObject(this.ToArray(), Formatting.Indented);
            System.IO.File.WriteAllText(path, json);
            Console.WriteLine("Data has been saved to file");
        }
        public void LoadJson(string path)
        {
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                collection = JsonConvert.DeserializeObject<List<T>>(json);
            }
        }
        //запись в .txt
        public void ToFile(string path)
        {
            int index = collection.Count;
            string[] text = new string[index];
            for (int i = 0; i < index; i++)
            {
                text[i] = Convert.ToString(this.Pop());
            }
            File.WriteAllLines(path, text);
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
        public override string ToString()   //переопределение метода(во всех классах)
        {
            return Title + " " + DateOfSignature.ToString("MM/dd/yyyy") + " " + Title + " " + Client + " " + Organization + "\n";
        }
    }
    class Program
    {
        public static object JsonSerializer { get; private set; }

        static async Task Main(string[] args)
        {
            string path = @"D:\ТРЕТИЙ СЕМЕСТР\ООП\lab8\Lab08\Lab08\in.txt";
            string jpath = @"D:\ТРЕТИЙ СЕМЕСТР\ООП\lab8\Lab08\Lab08\in.json";

            try
            {
                CollectionType<int> list = new CollectionType<int>();
                CollectionType<short> list1 = new CollectionType<short>(2);

                BinaryFormatter formatter = new BinaryFormatter();
                //list.Show();              //exception
/*                list.Add(9);
                //list.Add(0);              //exception
                list.Add(78);
                list.Add(22);
                list.Add(54);
                list.Add(20);
                list.Add(1);
                list.Add(58);
                list.Add(77);
                list.Show();*/

                /*list.ToFile(path);
                list.FromFile(path);*/
                CollectionType<Document> tom = new CollectionType<Document>();
                CollectionType<Document> lst = new CollectionType<Document>();

                Document a = new Document("Check", new DateTime(2020, 05, 12), "Tabolich Artyom", "KFC");
                Document b = new Document("Check", new DateTime(2020, 12, 30), "Tabolich Artyom", "MacDonalds");
                Document c = new Document("Check", new DateTime(2020, 10, 10), "Tabolich Artyom", "KFC");
                tom.Add(a);
                tom.Add(b);
                tom.Add(c);

                list.Save(path);
                list.Upload(path);
                tom.WriteToJson(jpath);
                lst.LoadJson(jpath);
                lst.Show();

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
