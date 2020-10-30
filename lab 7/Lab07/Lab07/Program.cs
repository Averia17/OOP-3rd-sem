using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Lab06
{

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                /*int y = 10;
                int x = y / 0; //ex*/

                Client first_client = new Client("Артём", "Таболич");
                Waybill document = new Waybill("fd" ,new DateTime(2012, 05, 12), first_client, new Organization("Пограничная служба"), 2600);
                Waybill first_waybill = new Waybill("Накладная на машину", new DateTime(2012, 05, 12), first_client, new Organization("Пограничная служба"), 7800);
                Receipt first_receipt = new Receipt("Квитанция об оплате интернета", new DateTime(2020, 05, 12), first_client, new Organization("Zala"), 49);
                Check first_check = new Check("Чек за купон 1330", new DateTime(2020, 10, 15), first_client, new Organization("KFC"), 5);
                
                Waybill second_waybill = new Waybill("Накладная на машину", new DateTime(2013, 03, 15), first_client, new Organization("Пограничная служба"), 12200);
                Waybill third_waybill = new Waybill("Накладная на машину", new DateTime(2014, 02, 21), first_client, new Organization("Пограничная служба"), 9000);
                Waybill fours_waybill = new Waybill("Накладная на машину", new DateTime(2020, 07, 30), first_client, new Organization("Пограничная служба"), 11000);
                Waybill last_waybill = new Waybill("Накладная на мебель", new DateTime(2012, 05, 12), first_client, new Organization("Окраска мебели"), 800);


                Bookkeeping bkkeeping = new Bookkeeping();
                BookkeepingController bkkeepingcontroll = new BookkeepingController();
                bkkeeping.AddReceipt(first_receipt);
                bkkeeping.AddWaybill(first_waybill);
                bkkeeping.AddCheck(first_check);

                bkkeeping.AddWaybill(second_waybill);
                bkkeeping.AddWaybill(third_waybill);
                bkkeeping.AddWaybill(fours_waybill);
                bkkeeping.AddWaybill(last_waybill);
                bkkeeping.ShowList();
                Console.WriteLine();
                Console.WriteLine(bkkeeping.waybills[4]); //ex

                Console.WriteLine("Суммарную стоимость продукции заданного наименования по всем накладным = {0}", bkkeeping.GetWaybillPrice("Накладная на машину"));
                Console.WriteLine();

                bkkeeping.GetDocuments(new DateTime(2020, 01, 01), new DateTime(2021, 01, 01));
                Console.WriteLine();
                Console.WriteLine("Количество чеков: {0}", bkkeepingcontroll.Count(bkkeeping));

                int index;

                index = 40;

                Debug.Assert(index > -1, "Значение должно быть больше -1");
            }
            
            catch (EmptyException ex)
            {
                ex.GetInfo();
            }
            catch (PriceException ex)
            {
                ex.GetInfo();
            }
            catch (RangeException ex)
            {
                ex.GetInfo();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.TargetSite);
            }
            
            catch (DivideByZeroException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.TargetSite);
            }
            catch
            {
                Console.WriteLine("Unknown error");
            }
            finally
            {
                Console.WriteLine("Finally block");
            }
        }
    }

}
