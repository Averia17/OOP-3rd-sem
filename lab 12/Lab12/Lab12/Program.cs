using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lab12
{
    public interface Show
    {
        void Show();
    }
    public class Test: Show
    {
        public string name;
        public Test(string name)
        {
            this.name = name;
        }
        public Test()
        {
            this.name = null;
        }
        public void Show()
        {
            Console.WriteLine(name);
        }
        public void ToConsole(List<string> arr)
        {
            foreach(string str in arr)
            {
                Console.WriteLine(str);
            }
        }
        public override string ToString()
        {
            return "Object of class Test with name " + name;
        }
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
    }
    static class Reflector
    {
        static public void AssName(string classname) //Определение имени сборки, в которой определен класс
        {
            Assembly currentAssembly = Assembly.GetExecutingAssembly(); 
            Type t = currentAssembly.GetType(classname);
            Assembly assem = t.Assembly;

            Console.WriteLine("Assembly Full Name:");
            Console.WriteLine(assem.FullName);

            Console.WriteLine("\nAssembly CodeBase:");
            Console.WriteLine(assem.CodeBase);
        }
        static public void GetConstructor(string classname) //есть ли публичные конструкторы
        {
            ConstructorInfo[] p = Type.GetType(classname).GetConstructors();
            Console.WriteLine("\tDoes the class has constructors?");
            if (p.Length > 0)
                Console.WriteLine($"Yes, {p.Length}");
            else
                Console.WriteLine("No");
        }

        static public void Pub(string classname) //извлекает все общедоступные публичные методы классa
        {
            Type t = Type.GetType(classname);
            Console.WriteLine("\tList of methods:");

            foreach (MethodInfo cMethod in t.GetMethods()) //GetMethods возвращает все public методы
            {
                 Console.WriteLine(cMethod.Name);
            }
        }
        static public void Field(string classname) //получает информацию о полях и свойствах класса
        {
            Type t = Type.GetType(classname);
            Console.WriteLine("\tList of fields:");
            foreach (FieldInfo fInfo in t.GetFields())
            {
                Console.WriteLine(fInfo.FieldType.Name + " " + fInfo.Name);
            }
            Console.WriteLine("\tList of properties:");
            foreach (PropertyInfo pInfo in t.GetProperties())
            {
                Console.WriteLine(pInfo.PropertyType.Name + " " + pInfo.Name);
            }
        }
        static public void Interface(string classname) // получает все реализованные классом интерфейсы
        {
            Type t = Type.GetType(classname);
            Console.WriteLine("\tList of interfaces:");
            foreach (Type tp in t.GetInterfaces())
            {
                Console.WriteLine(tp.Name);
            }
        }
        static public void MethodForType(string classname, string parametr) //выводит по имени класса имена методов, которые содержат заданный тип параметра
        {
            Type t = Type.GetType(classname);
            MethodInfo[] methods = t.GetMethods();
            Console.WriteLine("\tMethods of class {0} with args type {1}:", classname, parametr);
            for (int i = 0; i < methods.Length; i++)
            {
                ParameterInfo[] param = methods[i].GetParameters();
                for (int j = 0; j < param.Length; j++)
                {
                    if (parametr == param[j].ParameterType.Name)
                    {
                        Console.WriteLine(methods[i].Name);
                    }
                }
            }
        }
        public static void CallMethod(string className, string methodName) //вызывает метод класса, параметры метода берутся из файла
        {
            Type type = Type.GetType(className);
            List<string> FirstParam = File.ReadAllLines(@"D:\ТРЕТИЙ СЕМЕСТР\ООП\lab 12\Lab12\Lab12\Parameters.txt").ToList();
            List<string>[] parametrs = new List<string>[] { FirstParam };
            try
            {
                object obj = Activator.CreateInstance(type);
                MethodInfo method = type.GetMethod(methodName);
                Console.WriteLine("   Result of execution of method:");
                Console.WriteLine(method.Invoke(obj, parametrs));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static public void Create(string classname, string name) //2 задание, создает объект переданного типа
        {
            Type t = Type.GetType(classname);
            ConstructorInfo[] p = Type.GetType(classname).GetConstructors();
            object obj = Activator.CreateInstance(t, args: name);
            Console.WriteLine(obj.ToString());
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Reflector.AssName("Lab12.Test");
            Reflector.GetConstructor("Lab12.Test");
            Reflector.Pub("Lab12.Test");
            Reflector.Field("Lab12.Test");
            Reflector.Interface("Lab12.Test");
            Reflector.MethodForType("Lab12.Test", "Int32");
            Reflector.CallMethod("Lab12.Test", "ToConsole");

            Reflector.Create("Lab12.Test", "ARTYOM");
        }
    }
}
