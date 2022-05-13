namespace CodeTimerTest;

public class UnitTest
{
    [Theory]
    [InlineData(100, 10000)]
    public void Test(int quantity, int iteration)
    {
        var targetFrameworkAttribute = Assembly.GetExecutingAssembly()
            .GetCustomAttributes(typeof(TargetFrameworkAttribute), false)
            .SingleOrDefault() as TargetFrameworkAttribute;
        Trace.Listeners.Add(new ConsoleTraceListener());

        var models = TestHelper.CreateModels(quantity);

        Trace.WriteLine($"The target framework is {targetFrameworkAttribute?.FrameworkName}.");
        Trace.WriteLine($"Quantity is {quantity}.");
        Trace.WriteLine($"Iteration is {iteration}.");

        Runner.Initialize();
        Trace.WriteLine($"CodeTimer has been initialized on {DateTime.Now}.");

        var lambdaSummary = Runner.Time("Lambda", iteration, () =>
        {
            var results = models.Where(p => p.Name is "Name")
                .Select(testModel => testModel.ToDto().ToModel())
                .ToList();
        });
        var foreachSummary = Runner.Time("Foreach", iteration, () =>
        {
            var results = new List<TestModel>();
            foreach (var testModel in models)
            {
                if (testModel.Name is "Name")
                    results.Add(testModel.ToDto().ToModel());
            }
        });
        Trace.WriteLine(lambdaSummary);
        Trace.WriteLine(foreachSummary);
    }
}