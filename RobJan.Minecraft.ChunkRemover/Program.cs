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

ChunkRemover remover;
try
{
    remover = new ChunkRemover(worldPath, coordsToSave, range);
}
catch (ArgumentException e)
{
    Console.WriteLine(e.Message);
    return;
}

Console.WriteLine("Loading regions...");
remover.LoadRegions();
Console.WriteLine($"{remover.TotalRegions} regions found. Scanning which should be removed...");
remover.Scan();

ConsoleKeyInfo key;
do
{
    Console.Write($"Found {remover.RegionsToKeep.Count} regions to keep and {remover.RegionsToRemove.Count} regions to remove. Do you wish to proceed? (y/n) ");
    key = Console.ReadKey();
    Console.WriteLine();
} while (key.Key != ConsoleKey.Y && key.Key != ConsoleKey.N);

if (key.Key == ConsoleKey.N)
{
    Console.WriteLine("Exiting...");
    return;
}

Console.WriteLine($"Removing {remover.RegionsToRemove.Count} regions...");
remover.Remove();
Console.WriteLine("Finished!");