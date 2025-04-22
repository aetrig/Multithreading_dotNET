using System.Diagnostics;

namespace Multithreading;

class Program
{
    static void Main(string[] args)
    {
        Matrix m1 = new(1000, 901, 10);
        Matrix m2 = new(900, 700);
        Stopwatch stopwatch = new();
        
        MatrixMultiplier mult = new(m1, m2);

        stopwatch.Start();
        mult.MultiplyParallel(1);
        stopwatch.Stop();
        Console.WriteLine($"1 thread time: {stopwatch.Elapsed}");

        stopwatch.Restart();
        mult.MultiplyParallel(3);
        stopwatch.Stop();
        Console.WriteLine($"3 thread time: {stopwatch.Elapsed}");

        stopwatch.Restart();
        mult.MultiplyParallel(6);
        stopwatch.Stop();
        Console.WriteLine($"6 thread time: {stopwatch.Elapsed}");

        stopwatch.Restart();
        mult.MultiplyParallel(100);
        stopwatch.Stop();
        Console.WriteLine($"100 thread time: {stopwatch.Elapsed}");
    }
}
