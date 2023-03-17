namespace Zaabee.NumeralSystemConverter.Test;

public class StringExtensionTest
{
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
    public void ToIntTest(int value, NumeralSystem numerationSystem, bool inverted)
    {
        var str = value.ToString(numerationSystem, inverted);
        var result = str.ToInt(numerationSystem, inverted);
        Debug.WriteLine($"The value is: {value}");
        Debug.WriteLine($"The numerationSystem is: {numerationSystem}");
        Debug.WriteLine($"The str is: {str}");
        Debug.WriteLine($"The result is: {result}");
        Assert.Equal(value, result);
        Assert.Equal(0, "".ToInt(numerationSystem, inverted));
        Assert.Throws<ArgumentException>(() => "!@#".ToInt(numerationSystem, inverted));
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
    public void ToLongTest(long value, NumeralSystem numerationSystem, bool inverted)
    {
        var str = value.ToString(numerationSystem, inverted);
        var result = str.ToLong(numerationSystem, inverted);
        Debug.WriteLine($"The value is: {value}");
        Debug.WriteLine($"The numerationSystem is: {numerationSystem}");
        Debug.WriteLine($"The str is: {str}");
        Debug.WriteLine($"The result is: {result}");
        Assert.Equal(value, result);
        Assert.Equal(0, "".ToLong(numerationSystem, inverted));
        Assert.Throws<ArgumentException>(() => "!@#".ToLong(numerationSystem, inverted));
    }
}