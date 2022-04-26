namespace LevgeProxy
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Windows.Forms;

    public class Server
    {
        private TcpListener m_vServer;
        private Client m_vClient;
        private ListView lbLogs;

        public Server(ListView lbLogs)
        {
            this.LocalAddress = IPAddress.Loopback.ToString();
            this.LocalPort = 7776;
            this.RemoteAddress = IPAddress.Loopback.ToString();
            this.RemotePort = 7777;
            this.lbLogs = lbLogs;
        }

        public bool Start()
        {
            try
            {
                // Cleanup any previous objects..
                this.Stop();

                // Create the new TcpListener..
                this.m_vServer = new TcpListener(IPAddress.Parse(this.LocalAddress), this.LocalPort);
                this.m_vServer.Start();

                // Setup the async handler when a client connects..
                this.m_vServer.BeginAcceptTcpClient(new AsyncCallback(OnAcceptTcpClient), this.m_vServer);
                return true;
            }
            catch (Exception ex)
            {
                this.Stop();
                MessageBox.Show("Exception caught inside of Server::Start\r\n" + ex.Message);
                return false;
            }
        }

        public void Stop()
        {
            // Cleanup the client object..
            if (this.m_vClient != null)
                this.m_vClient.Stop();
            this.m_vClient = null;

            // Cleanup the server object..
            if (this.m_vServer != null)
                this.m_vServer.Stop();
            this.m_vServer = null;
        }

        private void OnAcceptTcpClient(IAsyncResult result)
        {
            // Ensure this connection is complete and valid..
            if (result.IsCompleted == false || !(result.AsyncState is TcpListener))
            {
                this.Stop();
                return;
            }

            lbLogs.Items.Add("Bağlantı Geldi.");

            // Obtain our server instance. (YOU NEED TO USE IT LIKE THIS DO NOT USE this.m_vServer here!)
            TcpListener tcpServer = (result.AsyncState as TcpListener);
            TcpClient tcpClient = null;

            try
            {
                // End the async connection request..
                tcpClient = tcpServer.EndAcceptTcpClient(result);

                // Kill the previous client that was connected (if any)..
                if (this.m_vClient != null)
                    this.m_vClient.Stop();

                // Prepare the client and start the proxying..
                this.m_vClient = new Client(tcpClient.Client,lbLogs);
                this.m_vClient.Start(this.RemoteAddress, this.RemotePort);
            }
            catch
            {
                MessageBox.Show("Error while attempting to complete async connection.");
            }

            // Begin listening for the next client..
            tcpServer.BeginAcceptTcpClient(new AsyncCallback(OnAcceptTcpClient), tcpServer);
        }

        public String LocalAddress
        {
            get;
            set;
        }
        public Int32 LocalPort
        {
            get;
            set;
        }
        public String RemoteAddress
        {
            get;
            set;
        }
        public Int32 RemotePort
        {
            get;
            set;
        }
    }
}
