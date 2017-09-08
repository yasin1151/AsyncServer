using System;

namespace Client.Interface
{
    /// <summary>
    /// 定义客户端行为
    /// </summary>
    public interface IClient : IHaveSocketable
    {

        /// <summary>
        /// 客户端开始接受消息
        /// </summary>
        void StartRecv();

        /// <summary>
        /// 当接受到消息时的回调函数
        /// </summary>
        /// <param name="ar"></param>
        void OnRecvMsgCallBack(IAsyncResult ar);

        /// <summary>
        /// 处理消息的回调函数
        /// </summary>
        /// <param name="data"></param>
        void OnAnalysisMsgCallBack(string data);

        /// <summary>
        /// 连接服务端，有自动重连机制
        /// </summary>
        /// <param name="ip">服务端ip</param>
        /// <param name="port">服务端端口</param>
        void Connect(string ip, int port);

        /// <summary>
        /// 发送消息给服务端
        /// </summary>
        /// <param name="msg">消息</param>
        void SendMsg(string msg);


        /// <summary>
        /// 关闭客户端
        /// </summary>
        void Close();

    }
}
