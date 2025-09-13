using System.Net;
using System.Text;

namespace Server
{
    class AsyncHttpServer
    {
        public async static Task RunServer()
        {
            using (HttpListener server = new HttpListener())
            {
                server.Prefixes.Add("http://localhost:4200/");
                server.Start();
                
                Console.WriteLine("\nServer started ( URL : http://localhost:4200/ )\n");

                while (true)
                {
                    try
                    {
                        HttpListenerContext context = await server.GetContextAsync();

                        try
                        {
                            string request = context.Request.RawUrl;

                            if (request.StartsWith("/echo/"))
                            {
                                Console.WriteLine("Echo endpoint received request");

                                var prefix = "/echo/".Length;
                                var message = request.Substring(prefix);

                                using (HttpListenerResponse response = context.Response)
                                {
                                    response.StatusCode = 200;
                                    response.ContentType = "text/plain";
                                    
                                    byte[] buffer = Encoding.UTF8.GetBytes(message);
                                    response.ContentLength64 = buffer.Length;

                                    using (Stream output = response.OutputStream)
                                    {
                                       await output.WriteAsync(buffer, 0, buffer.Length);
                                    }
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            throw;
                        }

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        throw;
                    }
                }
            }
        }
    }
}

