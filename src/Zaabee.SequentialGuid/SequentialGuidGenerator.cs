namespace Zaabee.SequentialGuid;

/// <summary>
/// Sequential guid generator
/// http://www.codeproject.com/Articles/388157/GUIDs-as-fast-primary-keys-under-multiple-database
/// </summary>
public static class SequentialGuidHelper
{
    public static Guid GenerateComb(SequentialGuidType guidType = SequentialGuidType.AsString)
    {
        var randomBytes = new byte[10];
        using (var rng = RandomNumberGenerator.Create())
            rng.GetBytes(randomBytes);

        var timestampBytes = BitConverter.GetBytes(DateTime.UtcNow.Ticks / 10000L);

        if (BitConverter.IsLittleEndian)
            Array.Reverse(timestampBytes);

        var guidBytes = new byte[16];

        switch (guidType)
        {
            case SequentialGuidType.AsString:
            case SequentialGuidType.AsBinary:
                Buffer.BlockCopy(timestampBytes, 2, guidBytes, 0, 6);
                Buffer.BlockCopy(randomBytes, 0, guidBytes, 6, 10);
                // If formatting as a string, we have to reverse the order
                // of the Data1 and Data2 blocks on little-endian systems.
                if (guidType is SequentialGuidType.AsString && BitConverter.IsLittleEndian)
                {
                    Array.Reverse(guidBytes, 0, 4);
                    Array.Reverse(guidBytes, 4, 2);
                }

                break;
            case SequentialGuidType.AtEnd:
                Buffer.BlockCopy(randomBytes, 0, guidBytes, 0, 10);
                Buffer.BlockCopy(timestampBytes, 2, guidBytes, 10, 6);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(guidType), guidType, null);
        }

        return new Guid(guidBytes);
    }
}