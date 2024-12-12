namespace ServerCore;

class Program
{
    static int number = 0; // Race Condition

    static void Thread_1()
    {
        for (int i = 0; i < 100000; i++)
        {
            int prev = number;
            Console.WriteLine($"prev:{prev}");
            Interlocked.Increment(ref number); // 성능에서 손해 봄, 대신 원자성 보장
            int next = number;
            Console.WriteLine($"next:{next}");
        }

    }
    static void Thread_2()
    {
        for (int i = 0; i < 100000; i++)
            Interlocked.Decrement(ref number); // 원자성 보장
        // 메모리 배리어를 간접적으로 사용하고 있어서 volatile을 쓰지 않아도 됨
    }

    static void Main(string[] args)
    {
        Task t1 = new Task(Thread_1);
        Task t2 = new Task(Thread_2);

        t1.Start();
        t2.Start();

        Task.WaitAll(t1, t2);

        Console.WriteLine(number);
    }
}