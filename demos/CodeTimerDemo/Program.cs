// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using Zaabee.CodeTimer;

Console.WriteLine("Begin!");

var bytes = new byte[1000000000];
Trace.Listeners.Add(new ConsoleTraceListener());

CodeTimer.Initialize();

CodeTimer.Time("Length",1000, () => { var result = bytes.Length; });
CodeTimer.Time("LongLength",1000, () => { var result = bytes.LongLength; });
CodeTimer.Time("NotAny",1000, () => { var result = !bytes.Any(); });

Console.WriteLine("Complete!");

Console.ReadLine();