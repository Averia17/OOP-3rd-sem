using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab02
{
    class Program
    {
        static void Main(string[] args)
        {
            //типы
            bool bltype = true; Console.WriteLine(bltype);
            byte bttype = 244; Console.WriteLine(bttype);
            char ctype = 'j'; Console.WriteLine(ctype);
            decimal dtype = 2.1m; Console.WriteLine(dtype);
            double dbltype = 12.33333333333; Console.WriteLine(dbltype);
            float ftype = 12.3333f; Console.WriteLine(ftype);
            int itype = -1111; Console.WriteLine(itype);
            uint uitype = 111111111; Console.WriteLine(uitype);
            long ltype = -2222222; Console.WriteLine(ltype);
            ulong ultype = 2222222; Console.WriteLine(ultype);
            short stype = -2222; Console.WriteLine(stype); //short 
            ushort ustype = 33333; Console.WriteLine(ustype);
            string name = "Tom"; Console.WriteLine(name);
            dynamic d = 3; Console.WriteLine(d); // здесь x - целочисленное int  также можно присвоить другой тип
            object o = "hello"; Console.WriteLine(o); //значение любого типа
            Console.WriteLine("\nПреобразования\n");
            dbltype = ftype; //float к double
            itype = stype; // short к int
            dtype = bttype; // bttype к decimal
            ltype = itype; // int к long
            ultype = uitype; // uint к ulong
            Console.WriteLine("Введите int тип");

            int secitype = Convert.ToInt32(Console.ReadLine());

            dbltype = 12.6666663333333;
            ltype = (long)uitype; Console.WriteLine(ltype);
            ctype = (char)bttype; Console.WriteLine(ctype);
            itype = (int)dbltype; Console.WriteLine(itype);
            stype = (short)secitype; Console.WriteLine(stype);
            ftype = (float)dbltype; Console.WriteLine(ftype);
            //Convert
            Console.WriteLine("Тип float: {0}", Convert.ToString(ftype));
            //unboxing
            int i = 123;
            object obj = i;
            short sh = (short)(int)obj;

            o = (object)itype;
            itype = (int)o;

            o = (object)ftype;
            ftype = (float)o;

            o = (object)dbltype;
            dbltype = (double)o;

            o = (object)stype;
            stype = (short)o;

            o = (object)name;
            name = (string)o;

            o = (object)bltype;
            bltype = (bool)o;
            Console.WriteLine("Типы var");

            //неявная типизация var
            var nottype = "string";
            var nottype2 = 3.5;
            Console.WriteLine(nottype.GetType());
            Console.WriteLine(nottype2.GetType());

            Nullable<int> p = null; //полная форма записи
            int? q = null; //упрощенная форма записи
            bool ifEqual = p == q;
            Console.WriteLine("Имеет ли значение р? " + p.HasValue);
            Console.WriteLine("Равны ли переменные p и q? " + ifEqual);


            Console.WriteLine("\nРабота со строками\n");

            //СТРОКИ
            string name1 = "Artyom";
            string surname = "Tabolich";
            Console.WriteLine("Сравнение строк \n{0} И строки {1} = {2}", name1, surname, String.Compare(name1, surname)); //'1' - первая буква строки 1 меньше (по позиции юникода) строки 2, '-1' - первая буква строки 1 больше строки 2, '0' строки идентичны
            string fathername = "Sergeevich";
            string[] strlist = { name1, surname, fathername };
            string joinstr = String.Join(" ", strlist);
            Console.WriteLine(joinstr); // объединение строк

            string strCopy = String.Copy(name1); //копирование
            Console.WriteLine(strCopy);
            Console.WriteLine(fathername.Substring(2, 8)); //выделение подстроки

            string[] splitstr = joinstr.Split(' ');
            foreach (string s in splitstr)
            {
                Console.WriteLine(s);
            }
            Console.WriteLine(joinstr.Insert(3, surname.Substring(0, 4)));
            Console.WriteLine(fathername.Remove(2, 3));


            string sEmpty = "";
            string sNull = null;
            Console.WriteLine(sEmpty.Length);
            Console.WriteLine(sEmpty == sNull);

            StringBuilder sb = new StringBuilder("New Artist", 50); //строка и выделяемая ей память
            sb.Remove(7, 3);
            sb.Append("yom");
            sb.Insert(0, "Hi ");
            sb.Remove(3, 4);
            Console.WriteLine(sb);


            //Массивы
            Console.WriteLine("\nРабота с массивами\n");

            int[,] arr = new int[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                    Console.Write(arr[x, y] + " ");
                Console.WriteLine();
            }

            string[] arrstring = { "QQQQ", "WWWW", "EEEE", "RRRR", "TTTT", "YYYY" };
            for (i = 0; i < arrstring.Length; i++)
            {
                Console.Write(" " + arrstring[i]);
            }
            Console.WriteLine();
            Console.WriteLine($"Длина массива: {arrstring.Length}");
            Console.WriteLine("Введите позицию нового элемента массива, а затем его значение");
            arrstring[Convert.ToInt32(Console.ReadLine()) - 1] = Console.ReadLine();
            for (i = 0; i < arrstring.Length; i++)
            {
                Console.Write(" " + arrstring[i]);
            }


            int[][] jaggedArr = new int[][]
            {
                new int[2],
                new int[3],
                new int[4]
            };
            Console.WriteLine("Введите элементы ступенчатого массива");
            for (int x = 0; x < jaggedArr.Length; x++)
                for (int y = 0; y < jaggedArr[x].Length; y++)
                    jaggedArr[x][y] = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Ступенчатый массив\n");

            for (int x = 0; x < jaggedArr.Length; x++)
            {
                for (int y = 0; y < jaggedArr[x].Length; y++)
                {
                    Console.Write("{0} ", jaggedArr[x][y]);
                }
                Console.WriteLine();
            }
            var array = new object[0];
            var str = "";

            //КОРТЕЖИ
            Console.WriteLine("\nРабота с кортежами\n");

            (string, int, char, string, ulong) VarTuple = ("Tom", 25, 'A', "Person", 222);
            Console.WriteLine("Кортеж целиком: " + VarTuple);
            Console.WriteLine($"1, 3 и 4 элементы кортежа: {VarTuple.Item1}, {VarTuple.Item3}, {VarTuple.Item4}");
            string firstT = VarTuple.Item1;
            int secondT = VarTuple.Item2;
            char thirdT = VarTuple.Item3;
            string fourthT = VarTuple.Item4;
            ulong fifthT = VarTuple.Item5;

            var NewTuple = ("Tom", 25, 'A', "Person", (ulong)222);

            if (VarTuple.CompareTo(NewTuple) == 0)
                Console.WriteLine("Кортежи равны");
            else
                Console.WriteLine("Кортежи не равны");


            //ЛОКАЛЬНАЯ ФУНКЦИЯ
            Console.WriteLine("\nЛокальная функция\n");

            (int, int, int, char) LocalFunction(int[] mas, string str1)
            { 
                int max = mas.Max();
                int min = mas.Min();
                int sum = mas.Sum();
                char FL = str1[0];
                return (max, min, sum, FL);

            }
            var Array1 = new[] { 1, 2, 3, 4, 12, 20, 82, 18, 100, 18, 9, 122, 14, 5, 13 };
            string Str1 = "Tabolich Artyom";
            Console.WriteLine(LocalFunction(Array1, Str1));


            //checked unchecked

            int Checksum(int x, int y)
            {
                try
                {
                    checked
                    {
                        int result = x + y;
                        return result;
                    }
                }
                catch(OverflowException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return 0;
            }

            int Unchecksum(int x, int y)
            {
                try
                {
                    unchecked
                    {
                        int result = x + y;
                        return result;
                    }
                }
                catch (OverflowException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return 0;
            }

            int a = Int32.MaxValue;
            int b = 222222;
            Console.WriteLine(Checksum(a, b));
            Console.WriteLine(Unchecksum(a, b));

        }
    }
}
