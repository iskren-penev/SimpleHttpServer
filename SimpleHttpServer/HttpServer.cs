namespace SimpleHttpServer
{
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading;
    using SimpleHttpServer.Models;

    public class HttpServer
    {
        private int port;
        private bool isActive;
        private TcpListener tcpListener;
        private HttpProcessor httpProcessor;

        public HttpServer(int port, IEnumerable<Route> routes)
        {
            this.Port = port;
            this.Processor = new HttpProcessor(routes);
            this.IsActive = true;
        }
        public int Port
        {
            get { return this.port; }
            private set { this.port = value; }
        }

        public bool IsActive
        {
            get { return this.isActive; }
            private set { this.isActive = value; }
        }

        public TcpListener Listener
        {
            get { return this.tcpListener; }
            private set { this.tcpListener = value; }
        }

        public HttpProcessor Processor
        {
            get { return this.httpProcessor; }
            private set { this.httpProcessor = value; }
        }

        public void Listen()
        {
            this.Listener = new TcpListener(IPAddress.Any, this.Port);
            this.Listener.Start();
            while (IsActive)
            {
                TcpClient client = this.Listener.AcceptTcpClient();
                Thread thread = new Thread(() =>
                {
                    this.Processor.HandleClient(client);
                });
                thread.Start();
                Thread.Sleep(1);
            }

        }
    }
}
