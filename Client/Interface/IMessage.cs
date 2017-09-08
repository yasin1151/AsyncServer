using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Interface
{
    /// <summary>
    /// 用于处理消息的消息类接口
    /// </summary>
    public interface IMessage
    { 
        /// <summary>
        /// 获取内部的byte数组
        /// </summary>
        /// <returns></returns>
        byte[] GetData();

        /// <summary>
        /// 获取可以开始存储的位置
        /// </summary>
        /// <returns>开始存储的位置</returns>
        int GetStartIndex();

        /// <summary>
        /// 获取剩余缓冲区的大小
        /// </summary>
        /// <returns>剩余缓冲区的大小</returns>
        int GetReaminSize();

        /// <summary>
        /// 解析消息（包头），并把数据传给回调函数
        /// </summary>
        /// <param name="msgCount">消息的大小</param>
        /// <param name="callBack">处理消息的回调函数</param>
        void AnalysisMsg(int msgCount, Action<string> callBack);

    }
}
