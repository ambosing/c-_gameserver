namespace ServerCore;


class Program
{
    // 내부적으로 Monitor 사용
    static object _lock = new object();
    static SpinLock _lock2 = new SpinLock(); // SpinLock을 하다가 너무 오래걸리면 양보
    static Mutex _lcok3 = new Mutex(); // 커널에게 명령어 요청해야 해서 느림


    static void Main(string[] args)
    {
        bool lockTaken = false;
        try
        {
            _lock2.Enter(ref lockTaken);
        }
        finally
        {
            if (lockTaken) _lock2.Exit();
        }
    }
}