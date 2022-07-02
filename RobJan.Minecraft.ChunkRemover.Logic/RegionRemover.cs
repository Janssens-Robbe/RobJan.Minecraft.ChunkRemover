namespace RobJan.Minecraft.ChunkRemover.Logic;

public class RegionRemover
{
    private readonly Queue<Region> _regionsToRemove = new();
    private readonly List<Region> _regionsToKeep = new();
    private List<Region>? _allRegions;

    public RegionRemover(RegionRemoverConfig config)
    {
        Config = config;
    }

    public RegionRemoverConfig Config { get; }
    public int RegionsToRemoveCount => _regionsToRemove.Count;
    public int RegionsToKeepCount => _regionsToKeep.Count;
    public int TotalRegionsCount => _allRegions?.Count ?? 0;

    public void LoadRegions()
    {
        _allRegions = Directory.GetFiles(Config.RegionPath)
            .Select(x => Path.GetFileName(x))
            .Where(x => Region.FileNameRegex.IsMatch(x))
            .Select(x => Region.FromFileName(x))
            .ToList();
    }

    public void Scan()
    {
        if (_allRegions is null)
            throw new InvalidOperationException("Regions not loaded");

        foreach (var region in _allRegions)
        {
            if (IsRegionProtected(region))
                _regionsToKeep.Add(region);
            else
                _regionsToRemove.Enqueue(region);
        }
    }

    public bool IsRegionProtected(Region region)
    {
        foreach (var range in Config.PlacesToKeep)
        {
            if (region.IsIn(range))
                return true;
        }

        return false;
    }

    public void Remove()
    {
        while (_regionsToRemove.TryDequeue(out var region))
        {
            File.Delete(Path.Combine(Config.RegionPath, region.FileName));
        }
    }
}
