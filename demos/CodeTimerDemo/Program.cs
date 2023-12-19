// See https://aka.ms/new-console-template for more information

const int iteration = 100_000_000;
byte[] srcBytes = { 1, 2, 3, 4, 5 };

Console.WriteLine("Begin!");

Runner.Initialize();

Console.WriteLine(
    Runner.Time(
        "for",
        iteration,
        () =>
        {
            var dest = new byte[srcBytes.Length];
            for (var i = 0; i < srcBytes.Length; i++)
            {
                dest[i] = srcBytes[i];
            }
        }
    )
);

Console.WriteLine(
    Runner.Time(
        "CopyTo",
        iteration,
        () =>
        {
            var dest = new byte[srcBytes.Length];
            srcBytes.CopyTo(dest, 0);
        }
    )
);

Console.WriteLine(
    Runner.Time(
        "Array.Copy",
        iteration,
        () =>
        {
            var dest = new byte[srcBytes.Length];
            Array.Copy(srcBytes, dest, srcBytes.Length);
        }
    )
);

Console.WriteLine(
    Runner.Time(
        "Clone",
        iteration,
        () =>
        {
            var dest = (byte[])srcBytes.Clone();
        }
    )
);

Console.WriteLine(
    Runner.Time(
        "Buffer.BlockCopy",
        iteration,
        () =>
        {
            var dest = new byte[srcBytes.Length];
            Buffer.BlockCopy(srcBytes, 0, dest, 0, srcBytes.Length);
        }
    )
);

Console.WriteLine(
    Runner.Time(
        "Span.ToArray",
        iteration,
        () =>
        {
            var dest = new Span<byte>(srcBytes, 0, srcBytes.Length).ToArray();
        }
    )
);

Console.WriteLine(
    Runner.Time(
        "ToArray",
        iteration,
        () =>
        {
            var dest = srcBytes.ToArray();
        }
    )
);

Console.WriteLine("Complete!");

Console.ReadLine();
