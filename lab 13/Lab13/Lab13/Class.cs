using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab13
{
    public static class TASLog
    {
        public const string sourceFile = @"D:\ТРЕТИЙ СЕМЕСТР\ООП\lab 13\Lab13\Lab13\taslogfile.txt";
        static TASLog() //вызывается в самом начале и очищает файл
        {
            using (StreamWriter w = new StreamWriter(sourceFile, false)) { }
        }
        public static void WriteLine(string str)
        {
            try
            {
                using (StreamWriter w = new StreamWriter(sourceFile, true))
                {
                    w.WriteLine(str);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
        }
        public static void SearchByString(string str)
        {
            using (StreamReader sr = new StreamReader(sourceFile, false))
            {
                while (!sr.EndOfStream)
                {
                    if (sr.ReadLine().StartsWith(str))
                        Console.WriteLine(sr.ReadLine());
                }
            }
        }

    }
    public class TASDiskInfo
    {
        public void DiskInfo()
        {
            TASLog.WriteLine("DiskInfo:");
            DriveInfo[] drives = DriveInfo.GetDrives(); //получение массива дисков
            foreach (DriveInfo drive in drives)
            {
                TASLog.WriteLine("\tName: " + drive.Name);
                TASLog.WriteLine("\tType: " + drive.DriveType);
                if (drive.IsReady)
                {
                    TASLog.WriteLine("\tFileSystem: " + drive.DriveFormat);
                    TASLog.WriteLine($"\tFreeSpace: total - {drive.TotalFreeSpace / 1000 / 1000 / 1000} GB, available - { drive.AvailableFreeSpace / 1024 / 1024 / 1024} GB");
                    TASLog.WriteLine($"\tTotalSize: {drive.TotalSize / 1024 / 1024 / 1024} GB");
                    TASLog.WriteLine("\tVolumeLabel: " + drive.VolumeLabel);
                }
                TASLog.WriteLine("");
            }
        }
    }
    public class TASFileInfo
    {
        public void FileData(string path)
        {
            TASLog.WriteLine("FileInfo:");
            FileInfo fileInf = new FileInfo(path);
            if (fileInf.Exists)
            {
                TASLog.WriteLine($"\tAll way : {fileInf.DirectoryName}");
                TASLog.WriteLine($"\tName: {fileInf.Name}");
                TASLog.WriteLine($"\tCapacity: {fileInf.Length}\n\tExtension: {fileInf.Extension}\n\tCreationTime: {fileInf.CreationTime}");
            }
            else
            {
                TASLog.WriteLine("This file doesn't exists");
            }
        }
    }
    public class TASDirInfo
    {
        public void DirInfo(string dirName)
        { 
            DirectoryInfo dirInfo = new DirectoryInfo(dirName);
            TASLog.WriteLine("\nDirInfo:");

            TASLog.WriteLine($"\tFilesCount: {dirInfo.GetFiles().Count()}");
            TASLog.WriteLine($"\tCreateon time: {dirInfo.CreationTime}");
            TASLog.WriteLine($"\tSubDirectories: {dirInfo.GetDirectories("*", SearchOption.AllDirectories).Count()}");
            TASLog.WriteLine($"\tParents: {dirInfo.Parent}");
        }
    }
    public class TASFileManager
    {
        public void FileManager(string path)
        {
            try
            {
                DriveInfo driveInfo = new DriveInfo(path);
                DirectoryInfo dirInfo = Directory.CreateDirectory(driveInfo.Name + "TASInspect");
                using (StreamWriter writer = File.CreateText(dirInfo.FullName + "\\tasdirinfo.txt"))
                {
                    writer.WriteLine("This is information");
                }
                File.Copy(dirInfo.FullName + "\\tasdirinfo.txt", dirInfo.FullName + "\\copied.txt");
                File.Delete(dirInfo.FullName + "\\tasdirinfo.txt");

                DirectoryInfo dirInfo1 = Directory.CreateDirectory(driveInfo.Name + "TASFiles");
                DirectoryInfo currentDirectory = new DirectoryInfo("./");
                foreach (var item in currentDirectory.GetFiles())
                    item.CopyTo(dirInfo1.FullName + "\\" + item.Name, true);
                dirInfo1.MoveTo(dirInfo.FullName + "\\" + dirInfo1.Name);

                DirectoryInfo dirInfo2 = new DirectoryInfo(dirInfo.FullName + "\\" + dirInfo1.Name);
                ZipFile.CreateFromDirectory(dirInfo2.FullName, dirInfo.FullName + "\\archieve.zip");

                DirectoryInfo exInfo = Directory.CreateDirectory(dirInfo.FullName + "\\Extracted");
                using (ZipArchive arch = ZipFile.OpenRead(dirInfo.FullName + "\\archieve.zip"))
                {
                    foreach (ZipArchiveEntry entry in arch.Entries)
                    {
                        entry.ExtractToFile(exInfo.FullName + "\\Extracted_" + entry.Name);
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
        }
    }
}
