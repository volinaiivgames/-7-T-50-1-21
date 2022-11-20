using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class Itable
    {
        public static List<Itable> Tables = new List<Itable>();
        public static int TopCursor = 0;
        public static int MaxHeight = 0;
        public string Str { get; set; }
        public int Width { get; set; }
        public Itable(string str, int width)
        {
            Str = str;
            Width = width;
        }

        public static async void createTable(int top, List<Itable> list)
        {
            TopCursor = top+1;
            Tables = list;
            int maxWidth = Console.WindowWidth;
            int size = 0;
            foreach (var item in list)
            {
                int value = Convert.ToInt32(Math.Round((double)(item.Width * maxWidth / 100)));
                size += value;

                int next = value / 2;
                Console.SetCursorPosition(size - next - (item.Str.Length / 2), top);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(item.Str);
                Console.ResetColor();
            }
            Console.WriteLine("");
        }

        public static void setTable(int i, string type, int left, int top, string str)
        {
            int maxWidth = Console.WindowWidth;
            int size = 0;
            int num = 0;
            foreach (var item in Tables)
            {
                int value = Convert.ToInt32(Math.Round((double)(item.Width * maxWidth / 100)));
                size += value;
                if (num == i)
                {
                    int next = 0;
                    if (type == "L") next = size - value;
                    else if (type == "C") next = size - (value/2) - (str.Length/2);
                    Console.SetCursorPosition(next + left, top);
                    
                    Console.WriteLine(str);
                }
                num++;
            }
        }
    }
}
