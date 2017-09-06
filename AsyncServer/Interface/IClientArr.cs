using System;
using System.Collections.Generic;
using System.Text;

namespace AsyncServer.Interface
{
    /// <summary>
    /// 用于定义客户端集合的行为
    /// </summary>
    public interface IClientArr
    {
        /// <summary>
        /// 获取客户端
        /// </summary>
        /// <returns></returns>
        List<IClient> GetClientList();

        
        /// <summary>
        /// 获取客户端数量
        /// </summary>
        /// <returns>客户端数量</returns>
        int GetClientNum();


        /// <summary>
        /// 移除客户端
        /// </summary>
        /// <param name="client">client的引用</param>
        /// <returns>是否移除成功</returns>
        bool RemoveClient(IClient client);


        /// <summary>
        /// 添加客户端
        /// </summary>
        /// <param name="client">client的引用</param>
        /// <returns>是否添加成功</returns>
        bool AddClient(IClient client);
    }
}
