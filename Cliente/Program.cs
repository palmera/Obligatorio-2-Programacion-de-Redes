using Cliente.Interface;
using Protocols;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Cliente
{
    class Program
    {   
        public static ClientInterface clientInterface { get; set; }

        static void Main(string[] args)
        {
            ClientInterface clientInterface = new ClientInterface();
            clientInterface.start();
            Console.ReadLine();
            //tcpClient.ReceiveBufferSize = 256;
            //int BufferSize = tcpClient.ReceiveBufferSize;
            //NetworkStream nws = tcpClient.GetStream();

            //FileStream fs;
            //fs = new FileStream(filename, FileMode.Open,
            //    FileAccess.Read);
            //byte[] bytesToSend = new byte[fs.Length];
            //int numBytesRead = fs.Read(bytesToSend, 0,
            //    bytesToSend.Length);
            //int totalBytes = 0;
            ////byte[] protocolaa= Encoding.ASCII.GetBytes("abc");
            ////nws.Write(protocol, 0, protocol.Length);
            //for (int i = 0; i <= fs.Length / BufferSize; i++)
            //{
            //    //---send the file---
            //    if (fs.Length - (i * BufferSize) > BufferSize)
            //    {
            //        nws.Write(bytesToSend, i * BufferSize,
            //            BufferSize);
            //        totalBytes += BufferSize;
            //    }
            //    else
            //    {
            //        nws.Write(bytesToSend, i * BufferSize,
            //            (int)fs.Length - (i * BufferSize));
            //        totalBytes += (int)fs.Length - (i * BufferSize);
            //    }
            //}
            //fs.Close();
            //Console.ReadLine();
        }
        
    }
}
