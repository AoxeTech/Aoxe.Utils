// See https://aka.ms/new-console-template for more information

using Zaabee.CodeTimer;

Console.WriteLine("Begin!");

var bytes = new byte[1000000000];

Runner.Initialize();

Console.WriteLine(Runner.Time("Length",1000, () => { var result = bytes.Length; }));
Console.WriteLine(Runner.Time("LongLength",1000, () => { var result = bytes.LongLength; }));
Console.WriteLine(Runner.Time("NotAny",1000, () => { var result = !bytes.Any(); }));

Console.WriteLine("Complete!");

Console.ReadLine();