using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TestServer
{
    class StaticTcpServer
    {
        public static void RunServer()
        {
            const int port = 4200;
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            
            TcpListener server = new TcpListener(ip, port);
            server.Start();
            
            Console.WriteLine("starting server ( 127.0.0.1 : 4200 )");

            while (true)
            {
                using TcpClient client = server.AcceptTcpClient();
                var stream = client.GetStream();
                
                byte[] buffer = new byte[1024];
                var data = stream.Read(buffer, 0, buffer.Length);
                string request = Encoding.UTF8.GetString(buffer, 0, buffer.Length);

                string url = "";
                
                try
                {
                    var lines = request.Split("\r\n");
                    var requestLine = lines[0].Split(" ");
                    url = requestLine[1];
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                try
                {
                    var prefix = "/echo/".Length; 
                    var bodyStr = url.Substring(prefix);
            
                    var echoBody = Encoding.UTF8.GetBytes($"{bodyStr}");
                    var echoHeader = Encoding.UTF8.GetBytes($"HTTP/1.1 200 OK\r\nContent-Type: text/plain\r\nContent-Length: {bodyStr.Length}\r\n\r\n");

                    try
                    {
                        stream.Write(echoHeader);
                        stream.Write(echoBody);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Error replaying echo");
                    }
            
                    Console.WriteLine($"Echo Command (message: {bodyStr})");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
    }

    class AsyncTcpServer
    {
        async void RunServer()
        {
            Console.WriteLine("starting server ( 127.0.0.1 : 4200 )");
        }
    }
}