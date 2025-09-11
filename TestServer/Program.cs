namespace TestServer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\n1. Async UDP Server");
                Console.WriteLine("2. Sync UDP Server");
                Console.WriteLine("3. Async TCP Server");
                Console.WriteLine("4. Sync TCP Server\n");
                
                ConsoleKeyInfo input = Console.ReadKey();
                char option = input.KeyChar;
                
                switch (option)
                {
                    case '1':
                        await AsyncUdpServer.RunServer(4200);
                        break;
                    case '2':
                        StaticUdpServer.RunServer();
                        break;
                    case '3':
                        await AsyncTcpServer.RunServer();
                        break;
                    case '4':
                        StaticTcpServer.RunServer();
                        break;
                }
            }
        }
    }
}