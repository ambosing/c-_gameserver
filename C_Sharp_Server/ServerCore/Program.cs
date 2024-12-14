namespace ServerCore;


class Program
{

    static ThreadLocal<string> ThreadName = new ThreadLocal<string>(() => { return $"My Name Is {Thread.CurrentThread.ManagedThreadId}"; });
    
    static void WhoAmI()
    {
        bool repeat = ThreadName.IsValueCreated; // true라면 위의 람다함수로 만들지 않음.
        if (repeat)
        {
            Console.WriteLine(ThreadName.Value + "(repeat)");
        } else
            Console.WriteLine(ThreadName.Value);
    }

    static void Main(string[] args)
    {
        ThreadPool.SetMinThreads(1, 1);
        ThreadPool.SetMaxThreads(3, 3);

        Parallel.Invoke(WhoAmI, WhoAmI, WhoAmI, WhoAmI, WhoAmI, WhoAmI, WhoAmI, WhoAmI );

        ThreadName.Dispose(); // 이렇게 쓰레드로컬을 날릴 수 있음
    }
}