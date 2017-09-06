using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using AsyncServer.Interface;

namespace AsyncServer
{
    public class Client : IClient
    {
        /// <summary>
        /// 每个客户端对应的socket
        /// </summary>
        private Socket _clientSocket;


        /// <summary>
        /// 获取当前所拥有的socket
        /// </summary>
        /// <returns>当前的socket</returns>
        public Socket GetSocket()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取当前socket绑定的ip地址
        /// </summary>
        /// <returns>当前绑定的ip</returns>
        public string GetIp()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取当前socket绑定的端口
        /// </summary>
        /// <returns>当前绑定的端口</returns>
        public int GetPoint()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取对server的引用
        /// </summary>
        /// <returns>server对象</returns>
        public Server GetServer()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 设置server对象
        /// </summary>
        /// <param name="server">server对象</param>
        public void SetServer(Server server)
        {
            throw new NotImplementedException();
        }
    }
}
