using System;

namespace LevgeProxy
{

    internal class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server();
            server.LocalAddress = "0.0.0.0";
            server.RemoteAddress = "212.64.211.117";
            server.RemotePort = 27001;
            server.LocalPort = 27001;

            server.Start();

            Console.WriteLine("Press ESC to stop");
            do
            {
                while (!Console.KeyAvailable)
                {
                    // Do something
                }
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }
    }
}
