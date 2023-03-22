namespace Zaabee.SequentialGuid.TestProject;

public class UnitTest
{
    private const int Quantity = 1000;

    [Theory]
    [InlineData(SequentialGuidType.AsBinary)]
    [InlineData(SequentialGuidType.AsString)]
    [InlineData(SequentialGuidType.AtEnd)]
    public void TestSequential(SequentialGuidType sequentialGuidType)
    {
        var guids = new Guid[Quantity];
        Enumerable.Range(0, Quantity).AsParallel().ForAll(p =>
            guids[p] = SequentialGuidHelper.GenerateComb(sequentialGuidType));
        Assert.Equal(guids, guids.Distinct());
    }

    [Fact]
    public void TestSequentialOutOfRange()
    {
        Assert.Throws<ArgumentOutOfRangeException>(
            () => SequentialGuidHelper.GenerateComb((SequentialGuidType)5)
            );
    }
}