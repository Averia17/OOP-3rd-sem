using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab13
{
    class Program
    {
        static void Main(string[] args)
        {
       
            TASDiskInfo diskInfo = new TASDiskInfo();
            diskInfo.DiskInfo();

            TASFileInfo fileInfo = new TASFileInfo();
            fileInfo.FileData(@"D:\ТРЕТИЙ СЕМЕСТР\ООП\lab 13\Lab13\Lab13\Class.cs");

            TASDirInfo dirInfo = new TASDirInfo();
            dirInfo.DirInfo(@"D:\ТРЕТИЙ СЕМЕСТР\ООП");

            TASFileManager fileManager = new TASFileManager();
            fileManager.FileManager("D:");

            TASLog.SearchByString("FileInfo:");
        }
    }
}
