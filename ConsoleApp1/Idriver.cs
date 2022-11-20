using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace ConsoleApp1
{
    class Idriver
    {
        public static string Dir = "";

        #region Get
        public static string getDriver(DriveInfo driver) { return $"{driver.Name} Всего: {getFormatBytes(driver.TotalSize)} Свободно: {getFormatBytes(driver.AvailableFreeSpace)}"; }
        public static string getFormatBytes(long bytes, int decimals = 2)
        {
            if (bytes > 0) {
                long k = 1024;
                int dm = decimals < 0 ? 0 : decimals;
                List<string> size = new List<string>() { "БАЙТ","КБ", "МБ", "ГБ", "ТБ" };
                int i = Convert.ToInt32(Math.Floor(Math.Log(bytes) / Math.Log(k)));
                return Convert.ToDouble((bytes / Math.Pow(k, i)).ToString("N" + dm)) + size[i];
            }
            return Convert.ToString(bytes);
        }
        #endregion

        #region Set
        public static void SetBM(bool active)
        {
            if(active)
            {
                Mouse.MousePosition = Itable.TopCursor;
                Mouse.MousePositionMin = Itable.TopCursor;
                Mouse.MousePositionMax = Itable.MaxHeight;
                Bind.Active = true;
                Mouse.Active = true;
            }
            else
            {
                Bind.Active = false;
                Mouse.Active = false;
                Console.SetCursorPosition(0, 0);
            }
        }
        public static void SetEnterDir()
        {
            string newDir = "";
            SetBM(false);

            if (Dir.Length != 0)
            {
                if (Directory.GetFileSystemEntries(Dir).Length == 0) return;
                newDir = Directory.GetFileSystemEntries(Dir)[Mouse.MousePosition - Mouse.MousePositionMin];
                if (Path.GetExtension(newDir).Length != 0)
                {
                    Filear.Open(newDir);
                    return;
                }
            }
            else
            {
                if (DriveInfo.GetDrives().Length == 0) return;
                if (DriveInfo.GetDrives().Length != Mouse.MousePositionMax - Mouse.MousePositionMin) return;
                newDir = DriveInfo.GetDrives()[Mouse.MousePosition - Mouse.MousePositionMin].Name;
            }
            Console.Clear();
            openDir(newDir.Substring(newDir.Length - 1) != "\\" ? newDir + "\\" : newDir);
        }

        public static void SetBackDir()
        {
            if (Dir.Length == 0) return;
            string[] DirList = Dir.Split("\\");

            Console.Clear();
            SetBM(false);

            if (DirList.Length == 2) Idriver.OpenDrivers();
            else openDir(Dir.Replace("\\" + DirList[DirList.Length - 2], ""));
        }

        #endregion

        #region open
        public static void OpenDrivers()
        {
            Dir = "";
            Mouse.MousePosition = 0;
            Mouse.MousePositionMin = 0;
            Mouse.MousePositionMax = 0;
            foreach (var item in DriveInfo.GetDrives())
            {
                Console.SetCursorPosition(0, Mouse.MousePositionMax);
                Console.WriteLine("  " + Idriver.getDriver(item));
                Mouse.MousePositionMax++;
            }

            Bind.Active = true;
            Mouse.Active = true;
        }

        public static void openDir(string path)
        {
            Dir = path;

            List<Itable> ListTop = new List<Itable>() { new Itable("Каталог " + Dir, 55), new Itable("Тип", 5), new Itable("Дата создания", 20), new Itable("Управление", 20) };
            List<string> Helps = new List<string>() { "F1 - Создать папку", "F2 - Создать файл", "F3 - Удалить", "Enter - Открыть", "Backspace - Назад" };
            string[] Dirs = Directory.GetFileSystemEntries(path);
            Itable.createTable(0, ListTop);

            int NextTopCursor = Itable.TopCursor;
            Itable.MaxHeight = Itable.TopCursor;

            foreach (var Dir in Dirs)
            {
                Itable.setTable(0, "L", 2, Itable.MaxHeight, Path.GetFileName(Dir));
                Itable.setTable(1, "C", 0, Itable.MaxHeight, Path.GetExtension(Dir).Length == 0 ? "Папка" : Path.GetExtension(Dir));
                Itable.setTable(2, "C", 0, Itable.MaxHeight, Convert.ToString(Directory.GetCreationTime(Dir)));
                Itable.MaxHeight++;
            }
            foreach (var Help in Helps)
            {
                Itable.setTable(3, "L", 0, NextTopCursor, Help);
                NextTopCursor++;
            }

            SetBM(true);
        }
        #endregion

        #region create / delete
        public static void CreateDir()
        {
            Bind.Active = false;
            Mouse.Active = false;

            Console.SetCursorPosition(0, Itable.MaxHeight);
            Console.Write("Введите имя папки: ");
            string dir = Console.ReadLine();
            Filear.CreateDir(dir);
        }
        public static void DeleteDir()
        {
            if (Directory.Exists(Directory.GetFileSystemEntries(Dir)[Mouse.MousePosition - Mouse.MousePositionMin]))
            {
                Directory.Delete(Directory.GetFileSystemEntries(Dir)[Mouse.MousePosition - Mouse.MousePositionMin]);
                Console.Clear();
                openDir(Dir);
            }
        }
        public static void CreateFile()
        {
            Bind.Active = false;
            Mouse.Active = false;
            Console.SetCursorPosition(0, Itable.MaxHeight);
            Console.Write("Введите имя файла: ");
            string dir = Console.ReadLine();
            Filear.CreateFile(dir);
        }
        #endregion
    }
}
