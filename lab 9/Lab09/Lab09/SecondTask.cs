using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab09
{
    class SecondTask
    {
        public static string DelZnak(string str)//удаление знаков
        {
            char[] znak = { '.', ',', '!', '?', '-', ':', '*' };
            for (int i = 0; i < str.Length; i++)
            {
                if (znak.Contains(str[i]))
                {
                    str = str.Remove(i, 1);
                    i--;
                }
            }
            return str;
        }

        public static string DelProbel(string str)//удаление пробелов
        {
            return str.Replace(" ", string.Empty);//возвр. изменённую строку
        }

        public static string Zaglav(string str)
        {
            for (int i = 0; i < str.Length; i++)
                str = str.Replace(str[i], Char.ToUpper(str[i]));
            return str;
        }

        public static string Letter(string str)
        {
            for (int i = 0; i < str.Length; i++)
                str = str.Replace(str[i], Char.ToLower(str[i]));
            return str;
        }
    }
}
