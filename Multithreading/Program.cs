using System.Diagnostics;

namespace Multithreading;

class Program
{
    static void Main(string[] args)
    {
        Matrix m1 = new(1000, -1, 10);
        //Console.WriteLine(m1);
        Matrix m2 = new(1000);
        //Console.WriteLine(m2);
        Stopwatch stopwatch = new();
        stopwatch.Start();
        Matrix m3 = m1.MultiplyParallel(m2, 1);
        stopwatch.Stop();
        Console.WriteLine($"1 time: {stopwatch.Elapsed}");

        stopwatch.Restart();
        Matrix m4 = m1.MultiplyParallel(m2, 3);
        stopwatch.Stop();
        Console.WriteLine($"3 time: {stopwatch.Elapsed}");

        stopwatch.Restart();
        Matrix m5 = m1.MultiplyParallel(m2, 6);
        stopwatch.Stop();
        Console.WriteLine($"6 time: {stopwatch.Elapsed}");
    }
}
