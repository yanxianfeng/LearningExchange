using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerSocket
{
    class Program
    {
        static void Main(string[] args)
        {
            ServerSocket serverSocket = new ServerSocket();
            serverSocket.CreaterServer("127.0.0.1",6000);
            Console.Write("服务端打开");
        }
    }


}
