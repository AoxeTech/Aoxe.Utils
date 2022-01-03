namespace CodeTimerTest;

public class UnitTest
{
    [Fact]
    public void Test()
    {
        const int quantity = 100;
        const int iteration = 10000;
        var targetFrameworkAttribute = Assembly.GetExecutingAssembly()
            .GetCustomAttributes(typeof(TargetFrameworkAttribute), false)
            .SingleOrDefault() as TargetFrameworkAttribute;
        Trace.Listeners.Add(new ConsoleTraceListener());

        var models = CreateModels(quantity);

        Trace.WriteLine($"The target framework is {targetFrameworkAttribute?.FrameworkName}.");
        Trace.WriteLine($"Quantity is {quantity}.");
        Trace.WriteLine($"Iteration is {iteration}.");

        CodeTimer.Initialize();
        Trace.WriteLine($"CodeTimer has been initialized on {DateTime.Now}.");

        CodeTimer.Time("Lambda", iteration, () =>
        {
            var results = models.Where(p => p.Name is "Name").Select(ConvertToDto).ToList();
        });
        CodeTimer.Time("Foreach", iteration, () =>
        {
            var results = new List<TestDto>();
            foreach (var testModel in models)
            {
                if (testModel.Name is "Name")
                    results.Add(ConvertToDto(testModel));
            }
        });
    }

    private static TestModel ConvertToModel(TestDto testDto) =>
        new()
        {
            Id = testDto.Id,
            Name = testDto.Name,
            Address = testDto.Address,
            CreateTime = testDto.CreateTime,
            Tags = testDto.Tags.ToList()
        };

    private static TestDto ConvertToDto(TestModel testModel) =>
        new()
        {
            Id = testModel.Id,
            Name = testModel.Name,
            Address = testModel.Address,
            CreateTime = testModel.CreateTime,
            Tags = testModel.Tags.ToList()
        };

    private static List<TestModel> CreateModels(int quantity) =>
        Enumerable.Range(0, quantity).Select(_ => CreateModel()).ToList();

    private static TestModel CreateModel() =>
        new()
        {
            Id = Guid.NewGuid(),
            Name = "Name",
            Address = "Address",
            CreateTime = DateTime.Now,
            Tags = new List<string>
            {
                "Apple", "Banana", "Pear"
            }
        };
}