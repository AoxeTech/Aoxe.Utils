using System;
using System.Diagnostics;
using System.Linq;
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

    public static Summary Time(string name, int iteration, Action action)
    {
        if (string.IsNullOrEmpty(name)) return new Summary();

        // 1.
        GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
        var gcCounts = new int[GC.MaxGeneration + 1];
        for (var i = 0; i <= GC.MaxGeneration; i++)
        {
            gcCounts[i] = GC.CollectionCount(i);
        }

        // 2.
        var watch = new Stopwatch();
        watch.Start();
        var cycleCount = GetCycleCount();
        for (var i = 0; i < iteration; i++) action();
        watch.Stop();
        var cpuCycles = GetCycleCount() - cycleCount;

        // 3.
        return new Summary
        {
            Name = name,
            ElapsedMilliseconds = watch.ElapsedMilliseconds,
            CpuCycle = cpuCycles,
            GenCounts = Enumerable.Range(0, GC.MaxGeneration + 1)
                .Select(p => new GenCount
                {
                    Gen = p,
                    Count = GC.CollectionCount(p) - gcCounts[p]
                }).ToList()
        };
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