using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Client;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {

            List<Client> list = new List<Client>();


            for (int i = 0; i < 1000; ++i)
            {
                Socket socket = new Socket(
                        AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                Client client = new Client(socket);
                client.Connect("127.0.0.1", 10086);
                client.StartRecv();
                list.Add(client);
            }



            while (true)
            {
                for (int i = 0; i < list.Count; ++i)
                {
                    list[i].SendMsg("i m client " + i);
                }
                
                Thread.Sleep(100);
            }
        }
    }
}
