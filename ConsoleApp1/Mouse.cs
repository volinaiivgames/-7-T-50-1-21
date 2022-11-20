using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ConsoleApp1
{
    class Mouse
    {
        public static Thread ThreadMouse = new Thread(new ThreadStart(Start));
        public static bool Active = false;
        public static int MousePosition = 0;
        public static int MousePositionMin = 0;
        public static int MousePositionMax = 0;

        public static void Start()
        {
            while (true)
            {
                if (Active)
                {
                    Console.SetCursorPosition(0, MousePosition);
                    Console.Write("->");
                }
                Thread.Sleep(100);
            }
        }

        public static void Update(ConsoleKey key)
        {
            if (key == ConsoleKey.UpArrow && MousePositionMin != MousePosition)
            {
                Console.SetCursorPosition(0, MousePosition);
                Console.Write("  ");
                MousePosition--;
            }
            else if (key == ConsoleKey.DownArrow && MousePositionMin + MousePositionMax - 1 != MousePosition)
            {
                Console.SetCursorPosition(0, MousePosition);
                Console.Write("  ");
                MousePosition++;
            }
        }
    }
}
