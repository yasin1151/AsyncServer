using System;
using System.Collections.Generic;
using System.Text;

namespace AsyncServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server("127.0.0.1", 10086);
            server.StartServer();


            Console.WriteLine("服务器启动成功,正在监听中...");
            Console.ReadKey();
        }
    }
}
