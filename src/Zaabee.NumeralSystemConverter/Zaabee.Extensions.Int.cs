namespace Zaabee.NumeralSystemConverter;

public static partial class ZaabeeExtension
{
    public static string ToString(this int dec, NumeralSystem numerationSystem, bool inverted = false) =>
        dec.ToString((byte)numerationSystem, inverted);

    public static string ToString(this int dec, byte toBase, bool inverted = false)
    {
        if (toBase > (byte)NumeralSystem.Base62) throw new ArgumentOutOfRangeException(nameof(toBase));
        var stack = new Stack<byte>();
        var sb = new StringBuilder();

        if (dec < 0)
        {
            sb.Append('-');
            dec = Math.Abs(dec);
        }

        while (dec > 0)
        {
            stack.Push((byte)(dec % toBase));
            dec /= toBase;
        }

        var charSet = inverted ? Consts.InvertedCharacterSet : Consts.DefaultCharacterSet;
        while (stack.Count > 0) sb.Append(charSet[stack.Pop()]);
        return sb.ToString();
    }
}