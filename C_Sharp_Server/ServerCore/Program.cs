﻿namespace ServerCore;

class Program
{
    private static volatile bool _stop = false;

    static void ThreadMain()
    {
        Console.WriteLine("쓰레드 시작!");

        while (_stop == false)
        {
            //누군가가 stop 신호를 해주기를 기다린다.
        }

        Console.WriteLine("쓰레드 종료!");
        
    }
    static void Main(string[] args)
    {
        Task task = new Task(ThreadMain);
        task.Start();
        
        Thread.Sleep(1000);
        
        _stop = true;

        Console.WriteLine("Stop 호출");
        Console.WriteLine("종료 대기중");
        task.Wait(); // Task 끝날 때까지 대기
        Console.WriteLine("종료 성공");
    }
}