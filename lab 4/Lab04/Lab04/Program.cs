using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab04
{
    public class MyList
    {

        private readonly Owner owner;
        private readonly Date date;
        private List<string> collection;

        public MyList(int ownerID, string ownerFIO)
        {
            this.owner = new Owner(ownerID, ownerFIO);
            this.date = new Date();
            this.collection = new List<string>();
        }

        public void AddItem(string item)
        {
            collection.Add(item);
        }

        public List<string> GetList()
        {
            return collection;
        }
        public int GetSize()
        {
            return collection.Count();
        }
        public string OutList()
        {
            string output = "";
            foreach (var item in collection)
            {
                output += item;
                output += '\n';
            }
            return output;
        }
       
        public Owner GetOwner()
        {
            return owner;
        }

        public static MyList operator + (MyList list, string item)
        {
            list.collection.Insert(0, item);
            return list;
        }

        public static MyList operator --(MyList list)
        {
            list.collection.RemoveAt(0);
            return list;
        }
        public static bool operator !=(MyList listOne, MyList listTwo)
        {
            foreach (string item1 in listOne.collection)
            {
                foreach (string item2 in listTwo.collection)
                {
                    if (item1 != item2)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool operator ==(MyList listOne, MyList listTwo)
        {
            foreach (string item1 in listOne.collection)
            {
                foreach (string item2 in listTwo.collection)
                {
                    if (item1 != item2)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public static MyList operator *(MyList listOne, MyList listTwo)
        {
            MyList new_list = listOne;
            foreach (string item in listTwo.collection)
            {
                new_list.AddItem(item);
            }
            return new_list;
        }

        

        public class Owner
        {
            private readonly int id;
            private readonly string fio;

            public Owner(int id, string fio)
            {
                this.id = id;
                this.fio = fio;
            }

            public void GetInfo()
            {
                Console.WriteLine($"Owner – ID: {id}, FIO: {fio}.");
            }
        }
        public class Date
        {
            public readonly DateTime time;

            public Date()
            {
                time = DateTime.Now;
            }
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            MyList list = new MyList(1, "aaa");
            list.AddItem("Tabolich");
            list.AddItem("Artyom");
            list.AddItem("Sergeevich");

            list = list + "Andrew";
            Console.WriteLine("");
            Console.WriteLine(list.OutList());
            --list;
            Console.WriteLine("");

            MyList list2 = new MyList(1, "bbb");
            list2.AddItem("Student");
            list2.AddItem("of");
            list2.AddItem("BSTU");
            MyList concat_list = list * list2;

            Console.WriteLine(concat_list.OutList());
            Console.WriteLine(concat_list != list2);
            Console.WriteLine(list.GetSize());

            string abc = "aaa Bbb ccc  DDDD ccc";
            Console.WriteLine($"Count of words with first upper letter: {abc.CountOfWords()}");
            Console.WriteLine($"Is list has dublicates? {StatisticOperation.IsHasDuplicates(concat_list)}");
            list.GetOwner().GetInfo();

        }
    }
}
