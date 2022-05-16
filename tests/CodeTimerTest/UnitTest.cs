using System.Text;

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

    [Theory]
    [InlineData(32, 100000)]
    public void StringConcatenationTest(int strLength, int iteration)
    {
        var targetFrameworkAttribute = Assembly.GetExecutingAssembly()
            .GetCustomAttributes(typeof(TargetFrameworkAttribute), false)
            .SingleOrDefault() as TargetFrameworkAttribute;
        Trace.Listeners.Add(new ConsoleTraceListener());
        Trace.WriteLine($"The target framework is {targetFrameworkAttribute?.FrameworkName}.");
        
        var random = new Random();
        const string charSet = "abcdefghigklmnopqrstuvwxyz0123456789";

        Runner.Initialize();
        Trace.WriteLine($"CodeTimer has been initialized on {DateTime.Now}.");

        var lambdaSummary = Runner.Time("Lambda", iteration, () =>
        {
            var outStr = new string(
                Enumerable.Range(0, strLength)
                    .Select(i => charSet[random.Next(0, charSet.Length)])
                    .ToArray()
            );
        });
        var stringBuilderSummary = Runner.Time("stringBuilder", iteration, () =>
        {
            var stringBuilder = new StringBuilder();

            for (var i = 0; i < strLength; i++)
            {
                stringBuilder.Append(charSet.Substring(random.Next(0, charSet.Length), 1));
            }

            var outStr = stringBuilder.ToString();
        });
        var foreachSummary = Runner.Time("foreach", iteration, () =>
        {
            var outStr = string.Empty;

            for (var i = 0; i < strLength; i++)
            {
                outStr += charSet.Substring(random.Next(0, charSet.Length), 1);
            }
        });
        Trace.WriteLine(lambdaSummary);
        Trace.WriteLine(stringBuilderSummary);
        Trace.WriteLine(foreachSummary);
    }
}