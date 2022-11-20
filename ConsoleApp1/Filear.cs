using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace ConsoleApp1
{
    class Filear
    {
        public static void CreateFile(string dir)
        {
            if(!Directory.Exists(Idriver.Dir + "\\" + dir)) File.CreateText(Idriver.Dir + "\\" + dir);
            Console.Clear();
            Idriver.openDir(Idriver.Dir);
        }
        public static void CreateDir(string dir)
        {
            if (!Directory.Exists(Idriver.Dir + "\\" + dir)) Directory.CreateDirectory(Idriver.Dir + "\\" + dir); 
            Console.Clear();
            Idriver.openDir(Idriver.Dir);
        }
        public static void Open(string dir) => Process.Start(new ProcessStartInfo(dir) { UseShellExecute = true }); 
    }
}
