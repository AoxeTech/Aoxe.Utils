// See https://aka.ms/new-console-template for more information

Console.WriteLine("Begin!");

var testList = Enumerable.Range(0, 10).ToList();

Runner.Initialize();

Console.WriteLine(Runner.Time("Count()",100_000_000, () =>
{
    var result = testList.Count();
}));

Console.WriteLine(Runner.Time("Count",100_000_000, () =>
{
    var result = testList.Count;
}));

Console.WriteLine("Complete!");

Console.ReadLine();