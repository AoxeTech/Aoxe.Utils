using System.Collections.Generic;

namespace Zaabee.CodeTimer;

public class Summary
{
    public string Name { get; set; } = string.Empty;
    public long ElapsedMilliseconds { get; set; }
    public ulong CpuCycle { get; set; }
    public List<GenCount> GenCounts { get; set; } = new();
}

public class GenCount
{
    public int Gen { get; set; }
    public int Count { get; set; }
}