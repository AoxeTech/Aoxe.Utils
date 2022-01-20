// See https://aka.ms/new-console-template for more information

using Zaabee.CodeTimer;

Console.WriteLine("Begin!");

var bytes = new byte[1000000000];

CodeTimer.Initialize();

Console.WriteLine(CodeTimer.Time("Length",1000, () => { var result = bytes.Length; }));
Console.WriteLine(CodeTimer.Time("LongLength",1000, () => { var result = bytes.LongLength; }));
Console.WriteLine(CodeTimer.Time("NotAny",1000, () => { var result = !bytes.Any(); }));

Console.WriteLine("Complete!");

Console.ReadLine();