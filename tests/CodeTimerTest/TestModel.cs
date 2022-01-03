namespace CodeTimerTest;

public class TestModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public DateTime CreateTime { get; set; }
    public List<string> Tags { get; set; } = new();
}