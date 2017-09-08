using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using AsyncServer.Interface;

namespace AsyncServer
{
    /// <summary>
    /// 服务器
    /// </summary>
    public class Server : IServer
    {
        /// <summary>
        /// 服务器的socket
        /// </summary>
        protected Socket _serverSocket;

        /// <summary>
        /// 用于保存端口、ip信息
        /// </summary>
        protected IPEndPoint _ipEndPoint;

        /// <summary>
        /// 用于保存当前所有在线的客户端
        /// </summary>
        protected List<IClient> _listClient;


        /// <summary>
        /// 构造函数
        /// </summary>
        public Server(IPEndPoint ipEndPoint)
        {
            _ipEndPoint = ipEndPoint;
            CreateSocketAndBind();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ip">ip地址</param>
        /// <param name="point">绑定的端口号</param>
        public Server(string ip, int point)
        {
            _ipEndPoint = new IPEndPoint(IPAddress.Parse(ip), point);
            CreateSocketAndBind();
        }


        /// <summary>
        /// 用于创建socket并且绑定IPEndPoint
        /// </summary>
        private void CreateSocketAndBind()
        {
            _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            if (_ipEndPoint == null)
            {
                throw new Exception("Server.CreatSocketAndBind 发生异常, ipEndPoint为空");
            }

            _serverSocket.Bind(_ipEndPoint);

            _listClient = new List<IClient>();
        }

        /// <summary>
        /// 获取当前所拥有的socket
        /// </summary>
        /// <returns>当前的socket</returns>
        public Socket GetSocket()
        {
            return _serverSocket;
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
        /// 获取IPEndPoint对象
        /// </summary>
        /// <returns>IPEndPoint对象</returns>
        public IPEndPoint GetIpAndPoint()
        {
            return _ipEndPoint;
        }

        /// <summary>
        /// 获取客户端
        /// </summary>
        /// <returns></returns>
        public List<IClient> GetClientList()
        {
            return _listClient;
        }

        /// <summary>
        /// 获取客户端数量
        /// </summary>
        /// <returns>客户端数量</returns>
        public int GetClientNum()
        {
            return _listClient.Count;
        }

        /// <summary>
        /// 移除客户端
        /// </summary>
        /// <param name="client">client的引用</param>
        /// <returns>是否移除成功</returns>
        public bool RemoveClient(IClient client)
        {
            bool ret =  _listClient.Remove(client);
            LogClientOffline(client.GetIpAndPoint());
            return ret;
        }

        /// <summary>
        /// 添加客户端
        /// </summary>
        /// <param name="client">client的引用</param>
        /// <returns>是否添加成功</returns>
        public bool AddClient(IClient client)
        {
            _listClient.Add(client);
            return true;
        }

        /// <summary>
        /// 启动服务器，开始监听
        /// </summary>
        public virtual void StartServer()
        {
            try
            {
                _serverSocket.Listen(0);
                _serverSocket.BeginAccept(OnNewConnCallBack, null);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error : 在[{0}] 函数中发生异常 : {1}", "OnNewConnCallBack", e);
            }

        }

        /// <summary>
        /// 当有新连接上线时的回调函数
        /// </summary>
        public virtual void OnNewConnCallBack(IAsyncResult ar)
        {
            try
            {
                Socket clientSocket = _serverSocket.EndAccept(ar);
                //TODO...
                IClient client = new Client(clientSocket, this);

                _listClient.Add(client);

                LogClientOnline(client.GetIpAndPoint());

                //开启客户端的监听
                client.StartRecv();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error : 在[{0}] 函数中发生异常 : {1}", "OnNewConnCallBack", e);
            }
            finally
            {
                //继续启动监听
                _serverSocket.BeginAccept(OnNewConnCallBack, null);
            }

        }

        /// <summary>
        /// 打印客户端下线消息
        /// </summary>
        /// <param name="ipEndPoint"></param>
        private void LogClientOffline(IPEndPoint ipEndPoint)
        {
            Console.WriteLine("客户端 [{0}] 下线, 当前在线人数 : {1}", ipEndPoint, _listClient.Count);
        }

        /// <summary>
        /// 打印客户端上线消息
        /// </summary>
        /// <param name="ipEndPoint"></param>
        private void LogClientOnline(IPEndPoint ipEndPoint)
        {
            Console.WriteLine("客户端 [{0}] 上线, 当前在线人数 : {1}", ipEndPoint, _listClient.Count);
        }
    }
}
