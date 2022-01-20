// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using Zaabee.CodeTimer;

Console.WriteLine("Begin!");

var bytes = new byte[1000000000];
Trace.Listeners.Add(new ConsoleTraceListener());

var summaries = new List<Summary>();

CodeTimer.Initialize();

summaries.Add(CodeTimer.Time("Length",1000, () => { var result = bytes.Length; }));
summaries.Add(CodeTimer.Time("LongLength",1000, () => { var result = bytes.LongLength; }));
summaries.Add(CodeTimer.Time("NotAny",1000, () => { var result = !bytes.Any(); }));

Console.WriteLine($"{summaries.Count} Complete!");

Console.ReadLine();