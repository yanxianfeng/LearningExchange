using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServerSocket
{
    public class ServerSocket
    {
        Socket soverSocket;

        List<Socket> Connetsockets = new List<Socket>();

        Thread sover_Thread;
        public void CreaterServer(string _ip,int _point)
        {
            IPAddress ip = IPAddress.Parse(_ip);
            IPEndPoint ip_end_point = new IPEndPoint(ip, _point);
            soverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            soverSocket.Bind(ip_end_point);
            soverSocket.Listen(10);

            sover_Thread = new Thread(ClientConnetListen);
            //sover_Thread.IsBackground = true;
            sover_Thread.Start(soverSocket);
        }
        /// <summary>
        /// 等待 客户端连接
        /// </summary>
        /// <param name="o"></param>
        private void ClientConnetListen(object o)
        {
            Socket Socket_sover = (Socket)o;
            try
            {
                while (true)
                {
                    Socket connetSocket = Socket_sover.Accept();//连接客户端
                    Console.WriteLine("客户端{0}链接成功", connetSocket.RemoteEndPoint);
                    string str = "你好客户端，我是服务端。";
                    byte[] bytes = System.Text.Encoding.Default.GetBytes(str);
                    connetSocket.Send(bytes);//向客户端发送消息
                    Thread thread = new Thread(Send);//发消息线程
                    thread.IsBackground = true;
                    thread.Start(connetSocket);
                    Thread thread1 = new Thread(Receive);//收消息线程
                    thread1.IsBackground = true;
                    thread1.Start(connetSocket);
                }
            }
            catch (Exception stringt)
            {
                Console.WriteLine(stringt);
                throw;
            }    
        }
        public void Send(object o)
        {
            Socket socket = (Socket)o;
            while (true)
            {
                string str = Console.ReadLine();
                byte[] bytes = System.Text.Encoding.Default.GetBytes(str);
                socket.Send(bytes);
                Thread.Sleep(100);

            }
        }

        public void Receive(object o)
        {
            Socket socket = (Socket)o;
            while (true)
            {
                byte[] bytes = new byte[1024];
                int len = socket.Receive(bytes);
                byte[] bytes1 = new byte[len];

                Array.Copy(bytes, 0, bytes1, 0, len);
                string str = System.Text.Encoding.Default.GetString(bytes1);
                Console.WriteLine("客户端说："+str);
            }
        }
        public void ExitServer()
        {
            if (soverSocket!=null)
            {
                soverSocket.Close();
            }
        }
    }
}
