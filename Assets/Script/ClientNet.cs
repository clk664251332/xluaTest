using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;

public class ClientNet : MonoBehaviour
{
    public Text messageText;
    public Button connetBtn;

    public string IP;
    public int Port;

    Socket socket;
    public const int BUFFER_SIZE = 1024;
    public byte[] readBuffer = new byte[BUFFER_SIZE];

    string RecvStr;
    void Start()
    {
        connetBtn.onClick.AddListener(Connet);
    }

    void Update()
    {
        messageText.text = RecvStr;
    }

    void Connet()
    {
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        IPAddress iPAddress = IPAddress.Parse(IP);
        EndPoint point = new IPEndPoint(iPAddress, Port);//IPEndPoint是对IP和端口做了一层封装的类
        socket.Connect(point);
        socket.BeginReceive(readBuffer, 0, BUFFER_SIZE, SocketFlags.None, RecieveCB, null);
    }

    private void RecieveCB(IAsyncResult ar)
    {
        try
        {
            int count = socket.EndReceive(ar);
            string str = System.Text.Encoding.UTF8.GetString(readBuffer, 0, count);
            RecvStr = str;
        }
        catch (Exception e)
        {
            
            socket.Close();
        }
    }
}
