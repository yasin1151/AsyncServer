using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace AsyncServer.Interface
{
    /// <summary>
    /// 服务器端接口
    /// </summary>
    public interface IServer : IHaveSocketable, IClientArr
    {

        /// <summary>
        /// 启动服务器，开始监听
        /// </summary>
        void StartServer();

        /// <summary>
        /// 当有新连接上线时的回调函数
        /// </summary>
        void OnNewConnCallBack( IAsyncResult ar );
    }

}
