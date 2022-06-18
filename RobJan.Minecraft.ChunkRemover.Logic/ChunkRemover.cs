namespace RobJan.Minecraft.ChunkRemover.Logic;

public class ChunkRemover
{
    private readonly List<Region> _regionsToRemove = new();
    private readonly List<Region> _regionsToKeep = new();
    private List<Region>? _allRegions;

    public ChunkRemover(string worldPath, IEnumerable<Coordinate> placesToKeep, int range)
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
    public IReadOnlyList<Region> RegionsToRemove => _regionsToRemove;
    public IReadOnlyList<Region> RegionsToKeep => _regionsToKeep;
    public int TotalRegions => _allRegions?.Count ?? 0;

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
                _regionsToRemove.Add(region);
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

    public int Remove()
    {
        foreach (var region in _regionsToRemove)
        {
            File.Delete(Path.Combine(RegionPath, region.FileName));
        }

        var count = _regionsToRemove.Count;
        _regionsToRemove.Clear();
        return count;
    }
}
