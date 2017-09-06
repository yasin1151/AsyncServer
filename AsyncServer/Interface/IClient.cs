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
    }
}
