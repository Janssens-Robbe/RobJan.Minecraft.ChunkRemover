using Konsole;
using RobJan.Minecraft.ChunkRemover.Logic;

if (args.Length != 3)
{
    Console.WriteLine("You need to spesify 3 arguments: worldPath, range, and coords.");
    return;
}

string worldPath = Path.GetFullPath(args[0]);
if (!int.TryParse(args[1], out int range))
{
    Console.WriteLine("Could not parse range.");
    return;
}
var coordsToSave = new List<Coordinate>();
foreach (var coords in args[2].Split("|"))
{
    var split = coords.Split(",");
    if (!int.TryParse(split[0], out int x)
        || !int.TryParse(split[1], out int y))
    {
        Console.WriteLine("Could not parse coords.");
        return;
    }
    coordsToSave.Add(new(x, y));
}

RegionRemover remover;
try
{
    remover = new RegionRemover(worldPath, coordsToSave, range);
}
catch (ArgumentException e)
{
    Console.WriteLine(e.Message);
    return;
}

Console.WriteLine("Loading regions...");
Thread thread = new(new ThreadStart(remover.LoadRegions));
thread.Start();
thread.Join();

Console.WriteLine($"{remover.TotalRegionsCount} regions found. Scanning which should be removed...");
var scanProgressBar = new ProgressBar(PbStyle.SingleLine, remover.TotalRegionsCount);
scanProgressBar.Refresh(0, "keep: 0; remove: 0");
thread = new(new ThreadStart(remover.Scan));
thread.Start();
while (thread.IsAlive)
{
    Thread.Sleep(50);
    scanProgressBar.Refresh(remover.RegionsToKeepCount + remover.RegionsToRemoveCount, $"keep: {remover.RegionsToKeepCount,6}; remove: {remover.RegionsToRemoveCount,6}");
}
thread.Join();

ConsoleKeyInfo key;
do
{
    Console.Write($"Found {remover.RegionsToKeepCount} regions to keep and {remover.RegionsToRemoveCount} regions to remove. Do you wish to proceed? (y/n) ");
    key = Console.ReadKey();
    Console.WriteLine();
} while (key.Key != ConsoleKey.Y && key.Key != ConsoleKey.N);

if (key.Key == ConsoleKey.N)
{
    Console.WriteLine("Exiting...");
    return;
}

var countToRemove = remover.RegionsToRemoveCount;
var deleteProgressBar = new ProgressBar(PbStyle.SingleLine, countToRemove);
scanProgressBar.Refresh(0, $"Removing {remover.RegionsToRemoveCount} regions");
thread = new(new ThreadStart(remover.Remove));
thread.Start();
while (thread.IsAlive)
{
    Thread.Sleep(50);
    deleteProgressBar.Refresh(countToRemove - remover.RegionsToRemoveCount, $"Removing {remover.RegionsToRemoveCount} regions");
}
thread.Join();
Console.WriteLine("Finished!");