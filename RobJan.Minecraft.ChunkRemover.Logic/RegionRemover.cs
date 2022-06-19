namespace RobJan.Minecraft.ChunkRemover.Logic;

public class RegionRemover
{
    private readonly Queue<Region> _regionsToRemove = new();
    private readonly List<Region> _regionsToKeep = new();
    private List<Region>? _allRegions;

    public RegionRemover(string worldPath, IEnumerable<Coordinate> placesToKeep, int range)
    {
        WorldPath = worldPath;
        RegionPath = Path.Combine(worldPath, "region");
        PlacesToKeep = placesToKeep;
        Range = range;

        if (!Directory.Exists(WorldPath))
            throw new ArgumentException($"Folder \"{WorldPath}\" does not exist.", nameof(worldPath));
        if (!Directory.Exists(RegionPath))
            throw new ArgumentException($"Folder \"{RegionPath}\" does not exist.", nameof(worldPath));
    }

    public string WorldPath { get; }
    public string RegionPath { get; }
    public IEnumerable<Coordinate> PlacesToKeep { get; }
    public int Range { get; }
    public int RegionsToRemoveCount => _regionsToRemove.Count;
    public int RegionsToKeepCount => _regionsToKeep.Count;
    public int TotalRegionsCount => _allRegions?.Count ?? 0;

    public void LoadRegions()
    {
        _allRegions = Directory.GetFiles(RegionPath)
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
        foreach (var place in PlacesToKeep)
        {
            if (region.IsInRangeOf(place.Chunk, Range))
                return true;
        }

        return false;
    }

    public void Remove()
    {
        while (_regionsToRemove.TryDequeue(out var region))
        {
            File.Delete(Path.Combine(RegionPath, region.FileName));
        }
    }
}
