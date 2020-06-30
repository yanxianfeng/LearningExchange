using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Threading;

namespace ConnetCocket
{
    class Program
    {
        Socket connetSocket;
        static void Main(string[] args)
        {
            Program program = new Program();
            program.connetSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPAddress ip = IPAddress.Parse("127.0.0.1");
            IPEndPoint pEndPoint = new IPEndPoint(ip, 6000);
            try
            {
                program.connetSocket.Connect(pEndPoint);
                Thread thread = new Thread(program.Send);
                thread.IsBackground = true;
                thread.Start(program.connetSocket);
                Thread thread1 = new Thread(program.Receive);
                thread1.IsBackground = true;
                thread1.Start(program.connetSocket);
                Console.WriteLine("连接成功");
            }
            catch (Exception)
            {
                Console.WriteLine("连接失败,重新连接");
                throw;
            }
            Console.Write("客户端打开");
            while (true)
            {

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
                Console.WriteLine("服务端说："+str);
            }
        }
    }
}
