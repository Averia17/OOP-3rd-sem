using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab04
{
    public static class StatisticOperation
    {
        public static string Max(MyList list)
        {
            string str = list.GetList().First();
            foreach (string item in list.GetList())
            {
                if (str.Length < item.Length)
                {
                    str = item;
                }
            }
            return str;
        }

        public static string Min(MyList list)
        {
            string str = list.GetList().First();
            foreach (string item in list.GetList())
            {
                if (str.Length > item.Length)
                {
                    str = item;
                }
            }
            return str;
        }

        public static int CountOfWords(this string str)
        {
            int count = 0;
            string[] words = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string word in words)
                if(char.IsUpper(word[0]))
                    count += 1;
            return count;
        }

        public static bool IsHasDuplicates(MyList list)
        {
            return list.GetList().Distinct().Count() != list.GetList().Count();
        }
    }
}
