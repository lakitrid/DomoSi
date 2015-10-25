using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DomoDispatcher
{
    internal class SocketService
    {
        private Task task;
        private CancellationTokenSource tokenSource;

        internal SocketService()
        {
            this.tokenSource = new CancellationTokenSource();
        }

        internal void Start()
        {
            this.task = new Task(Process, this.tokenSource.Token);
            this.task.Start();
        }

        internal void Stop()
        {
            this.task.Wait();
        }

        private void Process()
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPHostEntry ipHostInfo = Dns.GetHostEntry("localhost");
            IPAddress ipAddress = ipHostInfo.AddressList[1];
            socket.Bind(new IPEndPoint(ipAddress, 9000));
            socket.Listen(100);
            socket.Accept();
            
            SocketAsyncEventArgs socketValue = new SocketAsyncEventArgs();
            socketValue.Completed += SocketValue_Completed;

            if (!socket.ReceiveAsync(socketValue))
            {
                Console.WriteLine($"Negative response from socket receiveAsync");    
            }
        }

        private void SocketValue_Completed(object sender, SocketAsyncEventArgs e)
        {
            Console.WriteLine(ASCIIEncoding.UTF8.GetString(e.Buffer));
        }
    }
}
