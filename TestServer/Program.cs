namespace TestServer
{
    class Program
    {
        static void Main(string[] args)
        {
            StaticUdpServer.RunServer();
        }

        async void AsyncMain(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}