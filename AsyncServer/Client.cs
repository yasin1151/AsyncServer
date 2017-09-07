using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using AsyncServer.Interface;
using AsyncServer.Utils;

namespace AsyncServer
{
    public class Client : IClient
    {
        /// <summary>
        /// 每个客户端对应的socket
        /// </summary>
        private Socket _clientSocket;

        /// <summary>
        ///每个客户端对应的ip、端口信息
        /// </summary>
        private IPEndPoint _ipEndPoint;

        /// <summary>
        /// 对应的服务端
        /// </summary>
        private Server _server;

        /// <summary>
        /// 用于处理消息
        /// </summary>
        private IMessage _msg;


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="server"></param>
        public Client(Socket socket, Server server)
        {
            _clientSocket = socket;
            _ipEndPoint = EndPoint2IPEndPoint(socket.RemoteEndPoint);
            _server = server;
        }


        /// <summary>
        /// 获取当前所拥有的socket
        /// </summary>
        /// <returns>当前的socket</returns>
        public Socket GetSocket()
        {
            return _clientSocket;
        }

        /// <summary>
        /// 获取当前socket绑定的ip地址
        /// </summary>
        /// <returns>当前绑定的ip</returns>
        public string GetIp()
        {
            return _ipEndPoint.Address.ToString();
        }

        /// <summary>
        /// 获取当前socket绑定的端口
        /// </summary>
        /// <returns>当前绑定的端口</returns>
        public int GetPoint()
        {
            return _ipEndPoint.Port;
        }

        /// <summary>
        /// 获取对server的引用
        /// </summary>
        /// <returns>server对象</returns>
        public Server GetServer()
        {
            return _server;
        }

        /// <summary>
        /// 设置server对象
        /// </summary>
        /// <param name="server">server对象</param>
        public void SetServer(Server server)
        {
            _server = server;
        }

        /// <summary>
        /// 客户端开始接受消息
        /// </summary>
        public virtual void StartRecv()
        {
            if (_clientSocket == null || !_clientSocket.Connected)
            {
                return;
            }

            _clientSocket.BeginReceive(
                _msg.GetData(), _msg.GetStartIndex(), _msg.GetReaminSize(),
                SocketFlags.None, OnRecvMsgCallBack, _clientSocket);
        }

        /// <summary>
        /// 当接受到消息时的回调函数
        /// </summary>
        /// <param name="ar"></param>
        public virtual void OnRecvMsgCallBack(IAsyncResult ar)
        {
            int count = _clientSocket.EndReceive(ar);

            //处理消息，然后转发给回调函数
            _msg.AnalysisMsg(count, OnAnalysisMsgCallBack);

            StartRecv();
        }

        /// <summary>
        /// 处理消息的回调函数
        /// </summary>
        /// <param name="data"></param>
        public virtual void OnAnalysisMsgCallBack(string data)
        {
            Console.WriteLine("Recv [{0}] : {1}", _clientSocket.RemoteEndPoint, data);
            _clientSocket.Send(MsgUtil.PackData2UTF8("hello"));
        }


        /// <summary>
        /// EndPoint转换到IPEndPoint
        /// </summary>
        /// <param name="endPoint">EndPoint</param>
        /// <returns>IPEndPoint</returns>
        private IPEndPoint EndPoint2IPEndPoint(EndPoint endPoint)
        {
            string[] strs = endPoint.ToString().Split(':');
            return new IPEndPoint(
                    IPAddress.Parse(strs[0]),
                    int.Parse(strs[1]));
        }
    }
}
