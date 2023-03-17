namespace Zaabee.NumeralSystemConverter.Test;

public class LongExtensionTest
{
    [Theory]
    [InlineData(long.MaxValue, 32, true)]
    [InlineData(long.MaxValue, 36, true)]
    [InlineData(long.MaxValue, 62, true)]
    [InlineData(long.MinValue + 1, 32, true)]
    [InlineData(long.MinValue + 1, 36, true)]
    [InlineData(long.MinValue + 1, 62, true)]
    [InlineData(long.MaxValue, 32, false)]
    [InlineData(long.MaxValue, 36, false)]
    [InlineData(long.MaxValue, 62, false)]
    [InlineData(long.MinValue + 1, 32, false)]
    [InlineData(long.MinValue + 1, 36, false)]
    [InlineData(long.MinValue + 1, 62, false)]
    public void Test(long value, byte radix, bool inverted)
    {
        var str = value.ToString(radix, inverted);
        var result = str.ToLong(radix, inverted);
        Assert.Equal(value, result);
        Assert.Throws<ArgumentOutOfRangeException>(() => value.ToString(63, false));
    }

    [Theory]
    [InlineData(long.MaxValue, NumeralSystem.Binary, true)]
    [InlineData(long.MaxValue, NumeralSystem.Decimalism, true)]
    [InlineData(long.MaxValue, NumeralSystem.Hexadecimal, true)]
    [InlineData(long.MaxValue, NumeralSystem.Octal, true)]
    [InlineData(long.MaxValue, NumeralSystem.Base62, true)]
    [InlineData(long.MaxValue, NumeralSystem.Base36, true)]
    [InlineData(long.MinValue + 1, NumeralSystem.Binary, true)]
    [InlineData(long.MinValue + 1, NumeralSystem.Decimalism, true)]
    [InlineData(long.MinValue + 1, NumeralSystem.Hexadecimal, true)]
    [InlineData(long.MinValue + 1, NumeralSystem.Octal, true)]
    [InlineData(long.MinValue + 1, NumeralSystem.Base62, true)]
    [InlineData(long.MinValue + 1, NumeralSystem.Base36, true)]
    [InlineData(long.MaxValue, NumeralSystem.Binary, false)]
    [InlineData(long.MaxValue, NumeralSystem.Decimalism, false)]
    [InlineData(long.MaxValue, NumeralSystem.Hexadecimal, false)]
    [InlineData(long.MaxValue, NumeralSystem.Octal, false)]
    [InlineData(long.MaxValue, NumeralSystem.Base62, false)]
    [InlineData(long.MaxValue, NumeralSystem.Base36, false)]
    [InlineData(long.MinValue + 1, NumeralSystem.Binary, false)]
    [InlineData(long.MinValue + 1, NumeralSystem.Decimalism, false)]
    [InlineData(long.MinValue + 1, NumeralSystem.Hexadecimal, false)]
    [InlineData(long.MinValue + 1, NumeralSystem.Octal, false)]
    [InlineData(long.MinValue + 1, NumeralSystem.Base62, false)]
    [InlineData(long.MinValue + 1, NumeralSystem.Base36, false)]
    public void TestByNumerationSystem(long value, NumeralSystem numerationSystem, bool inverted)
    {
        var str = value.ToString(numerationSystem, inverted);
        var result = str.ToLong(numerationSystem, inverted);
        Assert.Equal(value, result);
    }
}