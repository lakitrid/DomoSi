using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Networking;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;

namespace DomoIoTCore
{
    internal class SocketServer
    {
        private Task task;

        private CancellationTokenSource tokenSource;
        private StreamSocket socket;

        public object Thread { get; private set; }

        public SocketServer()
        {
            this.socket = new StreamSocket();
            this.tokenSource = new CancellationTokenSource();
            this.task = new Task(this.Init, this.tokenSource.Token);
            this.task.Start();
        }

        private async void Init()
        {
            HostName host = new HostName("localhost");
            await this.socket.ConnectAsync(host, "9000");

            DataWriter writer = new DataWriter(this.socket.OutputStream);

            for (int i = 0; i < 100; i++)
            {
                writer.WriteString("Test !");
            }

            /*DataReader reader = new DataReader(this.socket.InputStream);

            while (!this.tokenSource.Token.IsCancellationRequested)
            {
                reader.InputStreamOptions = InputStreamOptions.Partial;
                uint unit = await reader.LoadAsync(reader.UnconsumedBufferLength);
                string result = reader.ReadString(unit);
                Debug.WriteLine(result);
            }*/
        }
    }
}
