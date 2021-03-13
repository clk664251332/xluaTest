using System;

namespace MyServer.Core
{
    internal class ServerMain
    {
        static void Main(string[] args)
        {
            Server server = new Server();
            server.Start("127.0.0.1", 1234);

            while (true)
            {
                string str = Console.ReadLine();
                if (str == "quit")
                {
                    return;
                }
            }
        }
    }
}
