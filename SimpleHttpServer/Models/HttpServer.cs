namespace SimpleHttpServer.Models
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading;

    public class HttpServer
    {
        #region Constructor
        public HttpServer(int port, IEnumerable<Route> routes)
        {
            this.Port = port;
            this.Processor = new HttpProcessor(routes);
            this.IsActive = true;
        }
        #endregion

        #region Properties
        public TcpListener Listener { get; private set; }

        public int Port { get; private set; }

        public bool IsActive { get; private set; }

        public HttpProcessor Processor { get; private set; }
        #endregion

        #region Methods
        public void Listen()
        {
            this.Listener = new TcpListener(IPAddress.Any, this.Port);
            this.Listener.Start();

            while (this.IsActive)
            {
                // Perform a blocking call to accept requests.
                // You could also user server.AcceptSocket() here.
                TcpClient client = this.Listener.AcceptTcpClient();
                Console.WriteLine("Connected!");

                Thread thread = new Thread(() => 
                {
                    this.Processor.HandleClient(client);
                });

                thread.Start();
                Thread.Sleep(1);
            }
        }
        #endregion
    }
}