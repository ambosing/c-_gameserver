﻿namespace ServerCore;

class Lock
{
    // 커널 단게의 bool이라고 생각해도 됨
    AutoResetEvent _available = new AutoResetEvent(true); // true => available, false => not available

    public void Acquire()
    {
        _available.WaitOne(); // 입장 시도
        // _available.Reset(); 이 포함되어 있음
        // => bool = false와 같음
    }

    public void Release()
    {
       _available.Set(); // bool = true
    }
}

class Program
{
    static int _num = 0;
    static Lock _lock = new Lock();

    static void Thread1()
    {
        for (int i = 0; i < 100000; i++)
        {
            _lock.Acquire();
            _num++;
            _lock.Release();
        }
    }
    static void Thread2()
    {
        for (int i = 0; i < 100000; i++)
        {
            _lock.Acquire();
            _num--;
            _lock.Release();
        }
    }

    static void Main(string[] args)
    {
        Task t1 = new Task(Thread1);
        Task t2 = new Task(Thread2);

        t1.Start();
        t2.Start();

        Task.WaitAll(t1, t2);

        Console.WriteLine(_num);
    }
}