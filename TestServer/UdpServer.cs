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
        public static async Task RunServer(int port)
        {

            using (var udpClient = new UdpClient(port)) 
            {
                while (true)
                {
                    try
                    {
                        UdpReceiveResult result = await udpClient.ReceiveAsync();
                        string message = Encoding.UTF8.GetString(result.Buffer);

                        Console.WriteLine($"received packet (message : {message})");

                        await udpClient.SendAsync(result.Buffer, result.Buffer.Length, result.RemoteEndPoint);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Runtime Exception(message : {e.Message})");
                    }
                }
            }
            
        }
    }
}