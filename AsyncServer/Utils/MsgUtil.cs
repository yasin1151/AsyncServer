using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;


namespace AsyncServer.Utils
{
    /// <summary>
    /// 消息工具类
    /// </summary>
    public class MsgUtil
    {
        /// <summary>
        /// 用于封装消息，加上包头(大小)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] PackData2UTF8(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return null;
            }

            int dataSize = data.Length;

            byte[] retBuf = BitConverter.GetBytes(dataSize);

            byte[] dataBytes = Encoding.UTF8.GetBytes(data);

            return retBuf.Concat(dataBytes).ToArray<byte>();

        }

        /// <summary>
        /// 将int转换成byte数组
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns>byte数组</returns>
        public static byte[] Int2Bytes(int data)
        {
            return BitConverter.GetBytes(data);
        }

    }
}
