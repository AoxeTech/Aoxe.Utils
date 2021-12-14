using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace Zaabee.CodeTimer;

public class CodeTimer
{
    public static void Initialize()
    {
        Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;
        Thread.CurrentThread.Priority = ThreadPriority.Highest;
        Time("", 1, () => { });
    }

    public static void Time(string name, int iteration, Action action)
    {
        if (string.IsNullOrEmpty(name)) return;

        // 1.
        Trace.WriteLine(name);

        // 2.
        GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
        var gcCounts = new int[GC.MaxGeneration + 1];
        for (var i = 0; i <= GC.MaxGeneration; i++)
        {
            gcCounts[i] = GC.CollectionCount(i);
        }

        // 3.
        var watch = new Stopwatch();
        watch.Start();
        var cycleCount = GetCycleCount();
        for (var i = 0; i < iteration; i++) action();
        var cpuCycles = GetCycleCount() - cycleCount;
        watch.Stop();

        // 4.
        Trace.WriteLine("\tTime Elapsed:\t" + watch.ElapsedMilliseconds.ToString("N0") + "ms");
        Trace.WriteLine("\tCPU Cycles:\t" + cpuCycles.ToString("N0"));

        // 5.
        for (var i = 0; i <= GC.MaxGeneration; i++)
        {
            var count = GC.CollectionCount(i) - gcCounts[i];
            Trace.WriteLine("\tGen " + i + ": \t\t" + count);
        }

        Trace.WriteLine("");
    }

    private static ulong GetCycleCount()
    {
        ulong cycleCount = 0;
        QueryThreadCycleTime(GetCurrentThread(), ref cycleCount);
        return cycleCount;
    }

    [DllImport("kernel32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool QueryThreadCycleTime(IntPtr threadHandle, ref ulong cycleTime);

    [DllImport("kernel32.dll")]
    private static extern IntPtr GetCurrentThread();
}