﻿using System.Diagnostics;

namespace Multithreading;

class Program
{
    static void Main(string[] args)
    {
        Matrix m1 = new(200, 200, 10);
        Matrix m2 = new(200, 200);
        Stopwatch stopwatch = new();
        
        MatrixMultiplier mult = new(m1, m2);

        // Console.WriteLine(mult.m1);
        // Console.WriteLine(mult.m2);

        Console.WriteLine("=========== Parallel ===========");
        stopwatch.Restart();
        mult.MultiplyParallel(1);
        stopwatch.Stop();
        Console.WriteLine($"1 thread time: {stopwatch.Elapsed}");
        // Console.WriteLine(mult.result);

        stopwatch.Restart();
        mult.MultiplyParallel(3);
        stopwatch.Stop();
        Console.WriteLine($"3 thread time: {stopwatch.Elapsed}");
        // Console.WriteLine(mult.result);

        stopwatch.Restart();
        mult.MultiplyParallel(6);
        stopwatch.Stop();
        Console.WriteLine($"6 thread time: {stopwatch.Elapsed}");
        // Console.WriteLine(mult.result);

        stopwatch.Restart();
        mult.MultiplyParallel(12);
        stopwatch.Stop();
        Console.WriteLine($"12 thread time: {stopwatch.Elapsed}");
        // Console.WriteLine(mult.result);



        Console.WriteLine(" =========== Threads ===========");
        stopwatch.Restart();
        mult.MultiplyThreads(1);
        stopwatch.Stop();
        Console.WriteLine($"1 thread time: {stopwatch.Elapsed}");
        //Console.WriteLine(mult.result);

        stopwatch.Restart();
        mult.MultiplyThreads(3);
        stopwatch.Stop();
        Console.WriteLine($"3 thread time: {stopwatch.Elapsed}");
        // Console.WriteLine(mult.result);

        stopwatch.Restart();
        mult.MultiplyThreads(6);
        stopwatch.Stop();
        Console.WriteLine($"6 thread time: {stopwatch.Elapsed}");
        // Console.WriteLine(mult.result);

        stopwatch.Restart();
        mult.MultiplyThreads(12);
        stopwatch.Stop();
        Console.WriteLine($"12 thread time: {stopwatch.Elapsed}");
        // Console.WriteLine(mult.result);

    }
}
