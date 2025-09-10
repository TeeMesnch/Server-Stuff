using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TestServer
{
    class StaticUdpServer
    {
        public static void RunServer()
        {
            const int port = 4200;
            var ip = IPAddress.Parse("127.0.0.1");
            
            Console.WriteLine($"Server started (port: {port}) (ip: {ip})");
            
            IPEndPoint endPoint = new IPEndPoint(ip, port);
            UdpClient udpClient = new UdpClient(endPoint);
            
            IPEndPoint remoteEndPoint = new IPEndPoint(ip, port);

            while (true)
            {
                try
                {
                    byte[] data = udpClient.Receive(ref remoteEndPoint);
                    
                    udpClient.Send(data, data.Length, remoteEndPoint);
                    Console.WriteLine("Send message to client");

                    Console.WriteLine(Encoding.ASCII.GetString(data));
                }
                catch (Exception)
                {
                    Console.WriteLine("FATAL ERROR");
                }
            }
        }
    }

    class AsyncUdpServer
    {
        async void RunServer()
        {
            Console.WriteLine("HELLO");
        }
    }
}