﻿using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ServerCore;


class Program
{


    static void Main(string[] args)
    {
        // DNS (Domain Name System)
        // DNS로 등록하면 이후 ip가 변경돼도 문제가 없음
        string host = Dns.GetHostName();
        IPHostEntry ipHost = Dns.GetHostEntry(host);
        IPAddress ipAddr = ipHost.AddressList[0];
        IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777); // ip, port

        // 문지기 
        Socket listenSocket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

        // 문지기 교육
        listenSocket.Bind(endPoint);

        // 영업 시작
        // backlog : 최대 대기수
        listenSocket.Listen(10);

        while (true)
        {
            Console.WriteLine("Listening....");

            // 손님을 입장시킨다
            Socket clientSocket = listenSocket.Accept();

            try
            {
                //받는다
                byte[] recvBuff = new byte[1024];
                int recvBytes = clientSocket.Receive(recvBuff);
                string recvData = Encoding.UTF8.GetString(recvBuff, 0, recvBytes);
                Console.WriteLine($"[From Client] {recvData}");

                //보낸다
                byte[] sendBuff = Encoding.UTF8.GetBytes("Welcome to MMORPG Server !");
                clientSocket.Send(sendBuff);

                // 쫒아낸다
                clientSocket.Shutdown(SocketShutdown.Both); // 신호 끊을거라고 FIN
                clientSocket.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            
        }
    }
}