﻿using System;
using System.IO;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Idriver.OpenDrivers();
            Mouse.ThreadMouse.Start();
            Bind.ThreadBinds.Start();
        }
    }
}
