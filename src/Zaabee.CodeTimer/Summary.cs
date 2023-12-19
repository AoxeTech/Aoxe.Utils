namespace Zaabee.CodeTimer;

public class Summary
{
    public string Name { get; set; } = string.Empty;
    public long ElapsedMilliseconds { get; set; }
    public ulong CpuCycle { get; set; }
    public List<GenCount> GenCounts { get; set; } = new();

    public override string ToString() =>
        $@"
Name:   {Name}
Time Elapsed:   {ElapsedMilliseconds:N0}ms
CPU Cycles: {CpuCycle:N0}
{string.Join("\r\n", GenCounts.Select(genCount => $"Gen {genCount.Gen}\t\t{genCount.Count}"))}";
}

public class GenCount
{
    public int Gen { get; set; }
    public int Count { get; set; }
}
