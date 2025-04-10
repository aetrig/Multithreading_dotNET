using System.Diagnostics;

namespace Multithreading;

class Program
{
    static void Main(string[] args)
    {
        Matrix m1 = new(1000, -1, 10);
        Matrix m2 = new(1000);
        Stopwatch stopwatch = new();
        Matrix m3 = new(0);
        
        stopwatch.Start();
        m3 = m1.MultiplyParallel(m2, 1);
        stopwatch.Stop();
        Console.WriteLine($"1 time: {stopwatch.Elapsed}");

        stopwatch.Restart();
        m3 = m1.MultiplyParallel(m2, 3);
        stopwatch.Stop();
        Console.WriteLine($"3 time: {stopwatch.Elapsed}");

        stopwatch.Restart();
        m3 = m1.MultiplyParallel(m2, 6);
        stopwatch.Stop();
        Console.WriteLine($"6 time: {stopwatch.Elapsed}");

        stopwatch.Restart();
        m3 = m1.MultiplyParallel(m2, 100);
        stopwatch.Stop();
        Console.WriteLine($"100 time: {stopwatch.Elapsed}");
    }
}
