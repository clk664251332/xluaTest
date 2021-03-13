using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace MyServer.Core
{
    /// <summary>
    /// Socket就像是代理人，服务器有一个Socket，同时可能与多个客户端打交道，所以会有多个客户端的代理人Socket
    /// 为了方便管理多个客户端，我们抽象出了Connection
    /// </summary>
    internal class Server
    {
        //监听套接字
        public Socket serverSocket;
        //客户端连接
        public Connection[] conns;
        //最大连接数
        public int maxConnCount = 50;
        //心跳机制
        Timer timer = new Timer(1000);
        public long heartBeatTime = 180;


        //获取连接池索引
        public int GetNewConnIndex()
        {
            if (conns.Length == 0) return -1;

            for (int i = 0; i < conns.Length; i++)
            {
                if (conns[i] == null)
                {
                    conns[i] = new Connection();
                    return i;
                }
                else if (!conns[i].inUse)
                {
                    return i;
                }
            }
            return -1;
        }

        //开启服务器
        public void Start(string host, int port)
        {
            conns = new Connection[maxConnCount];
            for (int i = 0; i < maxConnCount; i++)
            {
                conns[i] = new Connection();
            }
            //创建socket
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress iPAddress = IPAddress.Parse(host);
            EndPoint point = new IPEndPoint(iPAddress, port);//IPEndPoint是对IP和端口做了一层封装的类
            serverSocket.Bind(point);
            serverSocket.Listen(maxConnCount);
            //开始监听
            serverSocket.BeginAccept(AcceptCB, null);
            Console.WriteLine("[开启服务器]");
            //开启心跳
            timer.Elapsed += new ElapsedEventHandler(HandleMainTimer);
            timer.AutoReset = false;
            timer.Enabled = true;
        }

        private void HandleMainTimer(object sender, ElapsedEventArgs e)
        {
            HeartBeat();
            timer.Start();
        }

        //连接回调
        private void AcceptCB(IAsyncResult ar)
        {
            //连接的回调，创建与客户端的连接对象
            try
            {
                Socket clientSocket = serverSocket.EndAccept(ar);
                int index = GetNewConnIndex();
                if (index < 0)
                {
                    Console.WriteLine("[警告]连接池已满");
                }
                else
                {
                    Connection conn = conns[index];
                    conn.Init(clientSocket);
                    Console.WriteLine("[开启连接]" + conn.GetAddress());
                    //监听客户端消息
                    conn.StartRecieve(RecieveCB);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("[连接失败]" + e.ToString());
            }
        }

        //收消息回调
        private void RecieveCB(IAsyncResult ar)
        {
            Connection conn = (Connection)ar.AsyncState;
            lock (conn)
            {
                try
                {
                    //count是收到客户端消息的字节数
                    int count = conn.socket.EndReceive(ar);
                    //接收到客户端的关闭信号
                    if (count <= 0)
                    {
                        Console.WriteLine("收到客户端" + conn.GetAddress() + " [断开连接]");
                        conn.Close();
                        return;
                    }
                    else
                    {
                        conn.buffCount += count;
                        ProcessData(conn);
                        //继续接收
                        conn.StartRecieve(RecieveCB);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("收到客户端" + conn.GetAddress() + " [断开连接]");
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 数据粘包处理
        /// </summary>
        /// <param name="conn"></param>
        void ProcessData(Connection conn)
        {
            //每条消息包含4字节的消息长度和消息体
            if (conn.buffCount < sizeof(Int32))
            {
                //一定不是完整消息，return不处理
                return;
            }
            Array.Copy(conn.readBuffer, conn.lenBytes, sizeof(Int32));
            conn.msgLength = BitConverter.ToInt32(conn.lenBytes, 0);
            if (conn.buffCount < conn.msgLength + sizeof(Int32))
            {
                //消息的实际长度与消息附带长度不符，return不处理
                return;
            }
            //处理消息，广播分发

            //清除掉已经处理的消息
            int count = conn.buffCount - conn.msgLength - sizeof(Int32);
            Array.Copy(conn.readBuffer, sizeof(Int32) + conn.msgLength, conn.readBuffer, 0, count);
            conn.buffCount = count;
            if (conn.buffCount > 0)
            {
                ProcessData(conn);
            }


            //string str = System.Text.Encoding.UTF8.GetString(conn.readBuffer, 0, count);
            //Console.WriteLine("收到客户端消息" + conn.GetAddress() + str);
            //byte[] bytes = System.Text.Encoding.UTF8.GetBytes(str);
            ////将该消息广播给其他玩家
            //for (int i = 0; i < conns.Length; i++)
            //{
            //    if (conns[i] == null) continue;
            //    if (!conns[i].inUse) continue;
            //    if (conns[i] == conn) continue;

            //    conns[i].socket.Send(bytes);
            //}
        }

        /// <summary>
        /// 心跳，检测客户端是否断线
        /// </summary>
        void HeartBeat()
        {
            long timeNow = CommonHelper.GetTimeStamp();
            for (int i = 0; i < conns.Length; i++)
            {
                Connection conn = conns[i];
                if (conn == null) continue;
                if (!conn.inUse) continue;

                if (timeNow - conn.lastTimeTick > heartBeatTime)
                {
                    Console.WriteLine("[客户端断线]" + conn.GetAddress());
                    lock (conn)
                    {
                        conn.Close();
                    }
                }

            }
        }

        public void Close()
        {
            for (int i = 0; i < conns.Length; i++)
            {
                Connection conn = conns[i];
                if (conn == null) continue;
                if (!conn.inUse) continue;
                lock (conn)
                {
                    conn.Close();
                }

            }
        }
    }
}
