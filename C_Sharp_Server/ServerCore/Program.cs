using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ServerCore;


class Program
{
    static Listener _listener = new Listener();

    static void OnAcceptHandler(Socket clientSocket)
    {

        try
        {            
            Session session = new Session();
            session.Start(clientSocket);
            byte[] sendBuff = Encoding.UTF8.GetBytes("Welcome to MMORPG Server !");
            session.Send(sendBuff);

            Thread.Sleep(1000);

            session.Disconnect();
            session.Disconnect();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    static void Main(string[] args)
    {
        // DNS (Domain Name System)
        // DNS로 등록하면 이후 ip가 변경돼도 문제가 없음
        string host = Dns.GetHostName();
        IPHostEntry ipHost = Dns.GetHostEntry(host);
        IPAddress ipAddr = ipHost.AddressList[0];
        IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777); // ip, port


        _listener.Init(endPoint, OnAcceptHandler);
        Console.WriteLine("Listening...");

        // 프로그램이 끝나지만 않게끔 함
        while (true)
        {
            ; 
        }
    }



}