using System;

namespace AsyncServer.Interface
{
    /// <summary>
    /// 定义客户端行为
    /// </summary>
    public interface IClient : IHaveSocketable
    {
        /// <summary>
        /// 获取对server的引用
        /// </summary>
        /// <returns>server对象</returns>
        Server GetServer();


        /// <summary>
        /// 设置server对象
        /// </summary>
        /// <param name="server">server对象</param>
        void SetServer(Server server);


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
    }
}
