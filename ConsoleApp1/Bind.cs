using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ConsoleApp1
{
    class Bind
    {
        public static Thread ThreadBinds = new Thread(new ThreadStart(Start));
        public static bool Active = false;
        public static void Start()
        {
            while (true)
            {
                ConsoleKeyInfo keys = Console.ReadKey();
                if (Active)
                {
                    switch (keys.Key)
                    {
                        case ConsoleKey.UpArrow: Mouse.Update(ConsoleKey.UpArrow); break;
                        case ConsoleKey.DownArrow: Mouse.Update(ConsoleKey.DownArrow); break;
                        case ConsoleKey.Enter: Idriver.SetEnterDir(); break;
                        case ConsoleKey.Backspace: Idriver.SetBackDir(); break;
                        case ConsoleKey.F1: Idriver.CreateDir(); break;
                        case ConsoleKey.F2: Idriver.CreateFile(); break;
                        case ConsoleKey.F3: Idriver.DeleteDir(); break;
                        default: break;
                    }
                }
            }
        }
    }
}
