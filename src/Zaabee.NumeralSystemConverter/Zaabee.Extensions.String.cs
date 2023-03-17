namespace Zaabee.NumeralSystemConverter;

public static partial class ZaabeeExtension
{
    public static int ToInt(this string value, NumeralSystem numeralSystem, bool inverted = false) =>
        value.ToInt((byte)numeralSystem, inverted);

    public static long ToLong(this string value, NumeralSystem numeralSystem, bool inverted = false) =>
        value.ToLong((byte)numeralSystem, inverted);

    public static int ToInt(this string value, byte fromBase, bool inverted = false)
    {
        if (string.IsNullOrWhiteSpace(value)) return default;

        var isMinus = false;
        if (value[0] is '-')
        {
            isMinus = true;
            value = new string(value.Skip(1).ToArray());
        }

        if (value.Any(c => !char.IsLetterOrDigit(c)))
            throw new ArgumentException("The string can only contain letter or digit.", nameof(value));

        var charSet = inverted ? Consts.InvertedCharacterSet : Consts.DefaultCharacterSet;

        var result = value
            .Select((t, i) => charSet.IndexOf(t) * (int)Math.Pow(fromBase, value.Length - i - 1))
            .Sum();

        result = isMinus ? 0 - result : result;
        return result;
    }

    public static long ToLong(this string value, byte fromBase, bool inverted = false)
    {
        if (string.IsNullOrWhiteSpace(value)) return default;

        var isMinus = false;
        if (value[0] is '-')
        {
            isMinus = true;
            value = new string(value.Skip(1).ToArray());
        }

        if (value.Any(c => !char.IsLetterOrDigit(c)))
            throw new ArgumentException("The string can only contain letter or digit.", nameof(value));

        var charSet = inverted ? Consts.InvertedCharacterSet : Consts.DefaultCharacterSet;

        var result = value
            .Select((t, i) => charSet.IndexOf(t) * (long)Math.Pow(fromBase, value.Length - i - 1))
            .Sum();

        result = isMinus ? 0 - result : result;
        return result;
    }
}