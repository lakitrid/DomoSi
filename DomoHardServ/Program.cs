using DomoHard;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace DomoHardServ
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Start DomoHard Server");
            SensorWatcher watcher = new SensorWatcher();

            Console.ReadLine();
            watcher.Dispose();
            Console.WriteLine("Stop DomoHard Server");
        }
    }
}
