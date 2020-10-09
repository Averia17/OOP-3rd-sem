using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab05
{

    class Program
    {
        static void Main(string[] args)
        {
            Client first_client = new Client("Артём", "Таболич");

            Waybill first_waybill = new Waybill("Накладная на машину", new DateTime(2012,05,12), first_client, new Organization("Пограничная служба"), 7800);
            Receipt first_receipt = new Receipt("Квитанция об оплате интернета", new DateTime(2020,05,12), first_client, new Organization("Zala"), 49);
            Check first_check = new Check("Чек за купон 1330", new DateTime(2020,10,15), first_client, new Organization("KFC"), 5);
            first_waybill.Info();

            Console.WriteLine();

            Document firsdoc = first_waybill;
            Document seconddoc = first_receipt;

            Document thirddoc = first_check as Check;
            Console.WriteLine(thirddoc is Check);
            Console.WriteLine(firsdoc is Document);
            Console.WriteLine();

            //вызов одноименных методов
            firsdoc.Info();
            Console.WriteLine();

            ((IDocument)seconddoc).Info();
            Console.WriteLine();

            //создание массива
            Document[] mas = { firsdoc, seconddoc, thirddoc };
            for (int i = 0; i < mas.Length; i++)
            {
                Printer.IAmPrinting(mas[i]);
            }
        }
    }
}
