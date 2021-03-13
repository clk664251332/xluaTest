using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Core
{
    /// <summary>
    /// 管理客户的Socket，通过该Socket与客户进行通信
    /// </summary>
    internal class Connection
    {
        //缓冲区大小
        public const int BUFFER_SIZE = 1024;
        public Socket socket;
        public bool inUse = false;
        //BUFFER
        public byte[] readBuffer = new byte[BUFFER_SIZE];
        public int buffCount = 0;
        //粘包分包，四字节，用于临时存储消息的长度Int
        public byte[] lenBytes = new byte[(sizeof(UInt32))];
        public Int32 msgLength = 0;
        //心跳时间
        public long lastTimeTick = 0;
        //Player
        public Player player;

        public void Init(Socket soc)
        {
            socket = soc;
            inUse = true;
            buffCount = 0;
            lastTimeTick = CommonHelper.GetTimeStamp();
        }

        public int GetBuffRemain()
        {
            return BUFFER_SIZE - buffCount;
        }

        public string GetAddress()
        {
            if (!inUse) return "[无法获取地址]";
            return socket.RemoteEndPoint.ToString();
        }

        public void Close()
        {
            if (!inUse) return;
            Console.WriteLine("[断开连接]" + GetAddress());
            socket.Close();
            inUse = false;
        }

        public void StartRecieve(AsyncCallback cb)
        {
            //该客户端连接收到消息就会触发Server的RecieveCB函数
            socket.BeginReceive(readBuffer, buffCount, GetBuffRemain(), SocketFlags.None, cb, this);
        }

        public void Send(ProtocolBase protocol)
        {

        }
    }
}
