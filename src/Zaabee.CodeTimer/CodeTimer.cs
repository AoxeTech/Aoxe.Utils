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

    public static Summary Time(string name, int iteration, Action action)
    {
        if (string.IsNullOrEmpty(name)) return new Summary();

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
        watch.Stop();
        var cpuCycles = GetCycleCount() - cycleCount;
        
        // 4.
        var summary = new Summary
        {
            Name = name,
            ElapsedMilliseconds = watch.ElapsedMilliseconds,
            CpuCycle = cpuCycles
        };
        for (var i = 0; i <= GC.MaxGeneration; i++)
        {
            var count = GC.CollectionCount(i) - gcCounts[i];
            summary.GenCounts.Add(new GenCount
            {
                Gen = i,
                Count = count
            });
        }

        // 5.
        Trace.WriteLine("\tTime Elapsed:\t" + summary.ElapsedMilliseconds.ToString("N0") + "ms");
        Trace.WriteLine("\tCPU Cycles:\t" + summary.CpuCycle.ToString("N0"));
        foreach (var genCount in summary.GenCounts)
            Trace.WriteLine("\tGen " + genCount.Gen + ": \t\t" + genCount.Count);
        Trace.WriteLine("");

        return summary;
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