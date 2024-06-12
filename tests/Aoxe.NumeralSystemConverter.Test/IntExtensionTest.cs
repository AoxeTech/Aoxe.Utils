namespace Aoxe.NumeralSystemConverter.Test;

public class IntExtensionTest
{
    [Theory]
    [InlineData(int.MaxValue, 32, true)]
    [InlineData(int.MaxValue, 36, true)]
    [InlineData(int.MaxValue, 62, true)]
    [InlineData(int.MinValue + 1, 32, true)]
    [InlineData(int.MinValue + 1, 36, true)]
    [InlineData(int.MinValue + 1, 62, true)]
    [InlineData(int.MaxValue, 32, false)]
    [InlineData(int.MaxValue, 36, false)]
    [InlineData(int.MaxValue, 62, false)]
    [InlineData(int.MinValue + 1, 32, false)]
    [InlineData(int.MinValue + 1, 36, false)]
    [InlineData(int.MinValue + 1, 62, false)]
    public void Test(int value, byte radix, bool inverted)
    {
        var str = value.ToString(radix, inverted);
        var result = str.ToInt(radix, inverted);
        Assert.Equal(value, result);
        Assert.Throws<ArgumentOutOfRangeException>(() => value.ToString(63, false));
    }

    [Theory]
    [InlineData(int.MaxValue, NumeralSystem.Binary, true)]
    [InlineData(int.MaxValue, NumeralSystem.Decimalism, true)]
    [InlineData(int.MaxValue, NumeralSystem.Hexadecimal, true)]
    [InlineData(int.MaxValue, NumeralSystem.Octal, true)]
    [InlineData(int.MaxValue, NumeralSystem.Base62, true)]
    [InlineData(int.MaxValue, NumeralSystem.Base36, true)]
    [InlineData(int.MinValue + 1, NumeralSystem.Binary, true)]
    [InlineData(int.MinValue + 1, NumeralSystem.Decimalism, true)]
    [InlineData(int.MinValue + 1, NumeralSystem.Hexadecimal, true)]
    [InlineData(int.MinValue + 1, NumeralSystem.Octal, true)]
    [InlineData(int.MinValue + 1, NumeralSystem.Base62, true)]
    [InlineData(int.MinValue + 1, NumeralSystem.Base36, true)]
    [InlineData(int.MaxValue, NumeralSystem.Binary, false)]
    [InlineData(int.MaxValue, NumeralSystem.Decimalism, false)]
    [InlineData(int.MaxValue, NumeralSystem.Hexadecimal, false)]
    [InlineData(int.MaxValue, NumeralSystem.Octal, false)]
    [InlineData(int.MaxValue, NumeralSystem.Base62, false)]
    [InlineData(int.MaxValue, NumeralSystem.Base36, false)]
    [InlineData(int.MinValue + 1, NumeralSystem.Binary, false)]
    [InlineData(int.MinValue + 1, NumeralSystem.Decimalism, false)]
    [InlineData(int.MinValue + 1, NumeralSystem.Hexadecimal, false)]
    [InlineData(int.MinValue + 1, NumeralSystem.Octal, false)]
    [InlineData(int.MinValue + 1, NumeralSystem.Base62, false)]
    [InlineData(int.MinValue + 1, NumeralSystem.Base36, false)]
    public void TestByNumerationSystem(int value, NumeralSystem numerationSystem, bool inverted)
    {
        var str = value.ToString(numerationSystem, inverted);
        var result = str.ToInt(numerationSystem, inverted);
        Assert.Equal(value, result);
    }
}
