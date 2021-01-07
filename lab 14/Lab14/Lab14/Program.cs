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
using Newtonsoft.Json;

namespace Lab14
{

    class Program
    {
        static void Main(string[] args)
        {
            
            Client first_client = new Client("Артём", "Таболич");

            
            Waybill first_waybill = new Waybill("Накладная на машину", new DateTime(2012, 05, 12), first_client, new Organization("Пограничная служба"), 7800); //объект для сериализации
            Console.WriteLine("Binary:");
            BinaryFormatter formatter = new BinaryFormatter();  //создаем объект BinaryFormatter, который сериализует, используя двоичный формат
            using (FileStream fs = new FileStream(@"D:\ТРЕТИЙ СЕМЕСТР\ООП\lab 14\Lab14\Bin.dat", FileMode.OpenOrCreate))    //получем поток, куда будем записывать сериализованный объект
            {
                formatter.Serialize(fs, first_waybill);
            }
            using (FileStream fs = new FileStream(@"D:\ТРЕТИЙ СЕМЕСТР\ООП\lab 14\Lab14\Bin.dat", FileMode.OpenOrCreate))
            {
                Waybill newBook = (Waybill)formatter.Deserialize(fs);
                newBook.Info();
            }

            //b.SOAP формат
            Receipt first_receipt = new Receipt("Квитанция об оплате интернета", new DateTime(2020, 05, 12), first_client, new Organization("Zala"), 49);
            Console.WriteLine("\nSOAP:");
            SoapFormatter soapformatter = new SoapFormatter();  //сохраняет состояние объекта в виде сообщения SOAP
                                                                //(стандартный XML-формат для передачи и приема сообщений от веб - служб)
            using (Stream fs = new FileStream(@"D:\ТРЕТИЙ СЕМЕСТР\ООП\lab 14\Lab14\SOAP.dat", FileMode.OpenOrCreate))
            {
                soapformatter.Serialize(fs, first_receipt);
            }
            using (Stream fs = new FileStream(@"D:\ТРЕТИЙ СЕМЕСТР\ООП\lab 14\Lab14\SOAP.dat", FileMode.OpenOrCreate))
            {
                Receipt newBook1 = (Receipt)soapformatter.Deserialize(fs);
                newBook1.Info();
            }

            //c.JSON формат
            //DataContractJsonSerializer  не работает 
            Check first_check = new Check("Чек за купон 1330", new DateTime(2020, 10, 15), first_client, new Organization("KFC"), 5);
            Console.WriteLine("\nJSON:");
            string path = @"D:\ТРЕТИЙ СЕМЕСТР\ООП\lab 14\Lab14\check.json";
            JsonSerializer serializer = new JsonSerializer();
            using (StreamWriter sw = new StreamWriter(path))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, first_check);
            }
            Check deserializedProduct = JsonConvert.DeserializeObject<Check>(File.ReadAllText(path));
            Console.WriteLine("Object deserialized\n");
            //d.XML формат
            Check check = new Check("Чек за купон 7070", new DateTime(2019, 11, 15), first_client, new Organization("KFC"), 25);
            Console.WriteLine("\nXML:");
            XmlSerializer xml = new XmlSerializer(typeof(Check));    //ограничения: конструктор без параметров, указание типа
            using (FileStream fs = new FileStream(@"D:\ТРЕТИЙ СЕМЕСТР\ООП\lab 14\Lab14\XMLSerial.xml", FileMode.OpenOrCreate))
            {
                xml.Serialize(fs, check);
            }
            using (FileStream fs = new FileStream(@"D:\ТРЕТИЙ СЕМЕСТР\ООП\lab 14\Lab14\XMLSerial.xml", FileMode.OpenOrCreate))
            {
                Check newBook3 = xml.Deserialize(fs) as Check;
                newBook3.Info();
            }

            //2.Создайте коллекцию(массив) объектов и выполните сериализацию / десериализацию.
            Check check1 = new Check("Чек за купон 7070", new DateTime(2019, 11, 15), first_client, new Organization("KFC"), 25);
            Check check2 = new Check("Чек за купон 5050", new DateTime(2019, 11, 15), first_client, new Organization("KFC"), 25);
            Check check3 = new Check("Чек за купон 1330", new DateTime(2019, 11, 15), first_client, new Organization("KFC"), 25);
            Check[] bs = new Check[] { check1, check2, check3 };
            Console.WriteLine("\nArray:");
            path = @"D:\ТРЕТИЙ СЕМЕСТР\ООП\lab 14\Lab14\array.json";

            string json = JsonConvert.SerializeObject(bs, Newtonsoft.Json.Formatting.Indented);
            System.IO.File.WriteAllText(path, json);
            
            using (StreamReader r = new StreamReader(path))
            {
                string jsons = r.ReadToEnd();
                List<Check> checks = JsonConvert.DeserializeObject<List<Check>>(jsons);
                
            }
           

            //3.Используя XPath напишите два селектора для вашего XML документа.
            Console.WriteLine("\nXPath:");  //язык запросов к элементам XML-документа
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(@"D:\ТРЕТИЙ СЕМЕСТР\ООП\lab 14\Lab14\check.xml");
            XmlElement xRoot = xDoc.DocumentElement;    //св-во, возвр. корень док-та
            Console.WriteLine("All nodes:");
            XmlNodeList all = xRoot.SelectNodes("*");   //выбирает все узлы
            foreach (XmlNode x in all)
            {
                Console.WriteLine(x.OuterXml);         //вывод всей разметки
            }
            Console.WriteLine("Several parts of the check:");
            XmlNodeList parts = xRoot.SelectNodes("client");  //выбирает узлы client
            foreach (XmlNode x in parts)
            {
                Console.WriteLine(x.SelectSingleNode("Name").InnerText); //вывод значения первого узла Name
                Console.WriteLine(x.SelectSingleNode("Lastname").InnerText); //вывод значения первого узла Lastame

            }

            //4.Используя Linq to XML(или Linq to JSON) создайте новый xml(json) - документ и напишите несколько запросов.
            Console.WriteLine("\nLINQ to XML:");
            XDocument xdoc = new XDocument();
            XElement bookstore = new XElement("bookstore"); //первый эл
            XAttribute bs_name_attr = new XAttribute("name", "oz");
            XElement bs_country_elem = new XElement("country", "Belarus");
            XElement bs_city_elem = new XElement("city", "Minsk");
            bookstore.Add(bs_name_attr);            //заполняем аттрибутом и элем-ми
            bookstore.Add(bs_country_elem);
            bookstore.Add(bs_city_elem);

            XElement bookstore2 = new XElement("bookstore");    //второй эл
            XAttribute bs2_name_attr = new XAttribute("name", "Bookshop");
            XElement bs2_country_elem = new XElement("country", "France");
            XElement bs2_city_elem = new XElement("city", "Paris");
            bookstore2.Add(bs2_name_attr);          //заполняем аттрибутом и элем-ми
            bookstore2.Add(bs2_country_elem);
            bookstore2.Add(bs2_city_elem);

            XElement root = new XElement("root");   //корневой элемент
            root.Add(bookstore);
            root.Add(bookstore2);
            xdoc.Add(root);
            xdoc.Save(@"D:\ТРЕТИЙ СЕМЕСТР\ООП\lab 14\Lab14\linq.xml");                  //сохраняем в файл

            Console.WriteLine("Request 1: What is a bookstore in Belarus?"); //1-й запрос
            var items = xdoc.Element("root").Elements("bookstore")
                .Where(p => p.Element("country").Value == "Belarus")
                .Select(p => p);
            foreach (var item in items)
            {
                Console.WriteLine(item.Attribute("name").Value + " - " + item.Element("country").Value + " - " + item.Element("city").Value);
            }
            Console.WriteLine("Request 2: Where bookstore called 'Bookshop'? is located? ");//2-й запрос
            var coun = xdoc.Element("root").Elements("bookstore")
                .Where(p => p.Attribute("name").Value == "Bookshop")
                .Select(p => p);
            foreach (var c in coun)
            {
                Console.WriteLine(c.Attribute("name").Value + " - " + c.Element("country").Value + " - " + c.Element("city").Value);
            }
        }
    }
}
