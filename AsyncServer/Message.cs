using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsyncServer.Interface;

namespace AsyncServer
{
    public class Message : IMessage
    {
        /// <summary>
        /// 数据缓冲区
        /// </summary>
        private byte[] _bufData;

        /// <summary>
        /// 当前可以使用的位置（当前已经使用了的大小）
        /// </summary>
        private int _beginIndex;


        /// <summary>
        /// 构造函数，可以指定缓冲区的大小
        /// </summary>
        /// <param name="bufSize">缓冲区大小，默认为1024</param>
        public Message(int bufSize = 1024)
        {
            _bufData = new byte[bufSize];
            _beginIndex = 0;
        }


        /// <summary>
        /// 获取内部的byte数组
        /// </summary>
        /// <returns></returns>
        public byte[] GetData()
        {
            return _bufData;
        }

        /// <summary>
        /// 获取可以开始存储的位置
        /// </summary>
        /// <returns>开始存储的位置</returns>
        public int GetStartIndex()
        {
            return _beginIndex;
        }

        /// <summary>
        /// 获取剩余缓冲区的大小
        /// </summary>
        /// <returns>剩余缓冲区的大小</returns>
        public int GetReaminSize()
        {
            return _bufData.Length - _beginIndex;
        }

        /// <summary>
        /// 解析消息（包头），并把数据传给回调函数
        /// </summary>
        /// <param name="msgCount">消息的大小</param>
        /// <param name="callBack">处理消息的回调函数</param>
        public void AnalysisMsg(int msgCount, Action<string> callBack)
        {
            _beginIndex += msgCount;

            //当还有数据时
            while (4 < _beginIndex)
            {
                //获取长度
                int count = BitConverter.ToInt32(_bufData, 0);

                // 如果数据没有分包
                if (count <= (_beginIndex - 4))
                {
                    string data = Encoding.UTF8.GetString(_bufData, 4, count);

                    callBack(data);

                    //数组往前移动
                    //这里这样处理不是很好，待改进
                    Array.Copy(_bufData, count + 4, _bufData, 0, _beginIndex - 4 - count);
                    _beginIndex -= (count + 4);
                }
                else
                {
                    //TODO.. 分包情况，不处理，在客户端限制包大小即可
                    break;
                }
            }
        }
    }
}
