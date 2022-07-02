using Konsole;

namespace RobJan.Minecraft.ChunkRemover;

internal class Remover
{
    private readonly BaseOptions _options;
    private readonly RegionRemover _remover;

    public Remover(BaseOptions options)
    {
        _options = options;
        _remover = new RegionRemover(options.ToRegionRemoverConfig());
    }

    public List<ChunkRange> PlacesToKeep { get; } = new();

    public void Run()
    {
        LoadRegions();
        ScanRegions();
        if (!_options.NoConfirm)
            PromtRemoval();
        Remove();
    }

    private void LoadRegions()
    {
        Console.WriteLine("Loading regions...");
        Thread thread = new(new ThreadStart(_remover.LoadRegions));
        thread.Start();
        thread.Join();
    }

    private void ScanRegions()
    {
        Console.WriteLine($"{_remover.TotalRegionsCount} regions found. Scanning which should be removed...");
        var scanProgressBar = new ProgressBar(PbStyle.SingleLine, _remover.TotalRegionsCount);
        scanProgressBar.Refresh(0, "keep: 0; remove: 0");
        Thread thread = new(new ThreadStart(_remover.Scan));
        thread.Start();
        while (thread.IsAlive)
        {
            Thread.Sleep(50);
            scanProgressBar.Refresh(_remover.RegionsToKeepCount + _remover.RegionsToRemoveCount, $"keep: {_remover.RegionsToKeepCount,6}; remove: {_remover.RegionsToRemoveCount,6}");
        }
        thread.Join();
    }

    private void Remove()
    {
        var countToRemove = _remover.RegionsToRemoveCount;
        var deleteProgressBar = new ProgressBar(PbStyle.SingleLine, countToRemove);
        deleteProgressBar.Refresh(0, $"Removing {_remover.RegionsToRemoveCount}/{countToRemove} regions");
        Thread thread = new(new ThreadStart(_remover.Remove));
        thread.Start();
        while (thread.IsAlive)
        {
            Thread.Sleep(50);
            deleteProgressBar.Refresh(countToRemove - _remover.RegionsToRemoveCount, $"Removing {_remover.RegionsToRemoveCount} regions");
        }
        thread.Join();
        Console.WriteLine("Finished!");
    }

    private void PromtRemoval()
    {
        ConsoleKeyInfo key;
        do
        {
            Console.Write($"Found {_remover.RegionsToKeepCount} regions to keep and {_remover.RegionsToRemoveCount} regions to remove. Do you wish to proceed? (y/n) ");
            key = Console.ReadKey();
            Console.WriteLine();
        } while (key.Key != ConsoleKey.Y && key.Key != ConsoleKey.N);

        if (key.Key == ConsoleKey.N)
        {
            Console.WriteLine("Exiting...");
            Environment.Exit(0);
        }
    }
}
