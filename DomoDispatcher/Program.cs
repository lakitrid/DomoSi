using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomoDispatcher
{
    class Program
    {
        static void Main(string[] args)
        {
            var socket = new SocketService();
            socket.Start();

            Console.ReadLine();
            socket.Stop();
        }
    }
}
