namespace ServerCore;

class SpinLock
{
    volatile int _locked = 0; // 0 = unlock, 1 = lock

    public void Acquire()
    {
        while (true)
        {
            //// 경합되지 않는 스택이기 때문에 if문이 제대로 동작
            //int original = Interlocked.Exchange(ref _locked, 1); // 변경된 부분
            //if (original == 0) break;

            // CAS Compare-And-Swap
            int expected = 0;
            int desired = 1;
            
            if (Interlocked.CompareExchange(ref _locked, desired, expected) == expected)
                break;
        }
    }

    public void Release()
    {
        _locked = 0;
    }
}

class Program
{
    static int _num = 0;
    static SpinLock _lock = new SpinLock();

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