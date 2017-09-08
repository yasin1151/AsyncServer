using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Client.Interface;
using Client.Utils;

namespace Client
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
        /// 用于处理消息
        /// </summary>
        private IMessage _msg;


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="server"></param>
        public Client(Socket socket)
        {
            _clientSocket = socket;
            //_ipEndPoint = EndPoint2IPEndPoint(socket.RemoteEndPoint);
            _msg = new Message();
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
        public int GetPort()
        {
            return _ipEndPoint.Port;
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
            try
            {
                int count = _clientSocket.EndReceive(ar);

                //处理消息，然后转发给回调函数
                _msg.AnalysisMsg(count, OnAnalysisMsgCallBack);

                StartRecv();
            }
            catch (Exception)
            {
                Close();
            }

        }

        /// <summary>
        /// 处理消息的回调函数
        /// </summary>
        /// <param name="data"></param>
        public virtual void OnAnalysisMsgCallBack(string data)
        {
            Console.WriteLine("Recv [{0}] : {1}", _clientSocket.RemoteEndPoint, data);
        }

        /// <summary>
        /// 连接服务端
        /// </summary>
        /// <param name="ip">服务端ip</param>
        /// <param name="port">服务端端口</param>
        public void Connect(string ip, int port)
        {
            if (_clientSocket.Connected)
            {
                return;
            }

            _ipEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);

            try
            {
                _clientSocket.Connect(ip, port);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        /// <summary>
        /// 发送消息给服务端
        /// </summary>
        /// <param name="msg">消息</param>
        public void SendMsg(string msg)
        {
            _clientSocket.Send(MsgUtil.PackData2UTF8(msg));
        }

        /// <summary>
        /// 关闭客户端
        /// </summary>
        public void Close()
        {
            if (_clientSocket != null)
            {
                _clientSocket.Close();
            }
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
