namespace CodeTimerTest;

public static class TestHelper
{
    public static List<TestModel> CreateModels(int quantity) =>
        Enumerable.Range(0, quantity).Select(_ => CreateModel()).ToList();

    public static TestModel CreateModel() =>
        new()
        {
            Id = Guid.NewGuid(),
            Name = "Name",
            Address = "Address",
            CreateTime = DateTime.Now,
            Tags = new List<string> { "Apple", "Banana", "Pear" }
        };

    public static TestModel ToModel(this TestDto testDto) =>
        new()
        {
            Id = testDto.Id,
            Name = testDto.Name,
            Address = testDto.Address,
            CreateTime = testDto.CreateTime,
            Tags = testDto.Tags.ToList()
        };

    public static TestDto ToDto(this TestModel testModel) =>
        new()
        {
            Id = testModel.Id,
            Name = testModel.Name,
            Address = testModel.Address,
            CreateTime = testModel.CreateTime,
            Tags = testModel.Tags.ToList()
        };
}
