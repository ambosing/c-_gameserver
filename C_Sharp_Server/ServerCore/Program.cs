namespace ServerCore;


class Program
{

    // 상호배제

    // 내부적으로 Monitor 사용
    static object _lock = new object();
    static SpinLock _lock2 = new SpinLock(); // SpinLock을 하다가 너무 오래걸리면 양보
    static Mutex _lcok3 = new Mutex(); // 커널에게 명령어 요청해야 해서 느림

    // 하나만 들어갈 수 있는 상호배제가 단점일 수 있음
    // [ ] [ ] [ ] [ ] [ ]
    // 운영 툴에서 보상을 줘야하는 로직
    class Reward
    {

    }

    // 99.999999%
    // 그래서 Get에서는 필요없을 가능성
    // 그래서 필요한 것이 RWLock ReaderWriterLock
    static ReaderWriterLockSlim _lock3 = new ReaderWriterLockSlim();
    static Reward GetRewardById(int id)
    {
        // Write가 없으면 자유롭게 넘나들 수 있음
        _lock3.EnterReadLock();
        
        _lock3.ExitReadLock();
        
        
        lock (_lock)
        {

        }
        return null;
    }
    
    // 0.00000001%
    void AddReward(Reward reward)
    {
        _lock3.EnterWriteLock();

        _lock3.ExitWriteLock();
        lock (_lock3)
        {
        }
    }

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