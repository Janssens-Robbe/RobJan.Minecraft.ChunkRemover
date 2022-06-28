namespace RobJan.Minecraft.ChunkRemover;

[Verb("remove", isDefault: true, HelpText = "Removes data from a world.")]
internal class Options : BaseOptions
{
    private const string _couldNotPraseCoordsMessage = "Could not parse coodinate `{0}`. Correct format is `x,z` or `x,z,r`";

    public Options(string worldPath, int range, IEnumerable<string> coordinates)
    {
        WorldPath = worldPath;
        Range = range;
        Coordinates = coordinates;
    }

    [Option('w', "world", Default = ".", HelpText = "Path to the world folder.")]
    public string WorldPath { get; }

    [Option('r', "range", Default = 32, HelpText = "Range (in chunks) from any specified coordinates where no data will be removed.")]
    public int Range { get; }

    [Option('c', "coordinates", Required = true, HelpText = "List of coordinates to not remove any chunks around within the range.Format `x,z`. Optinally the reange can be overriden for each coordinate by using the format `x,z,r` where `r` is the range.")]
    public IEnumerable<string> Coordinates { get; }

    public override RegionRemoverConfig ToRegionRemoverConfig()
    {
        return new(WorldPath, ParseCoodinates(), Range);
    }

    private IEnumerable<ChunkRange> ParseCoodinates()
    {
        foreach (var coords in Coordinates)
        {
            var split = coords.Split(",");
            yield return split.Length switch
            {
                2 => ParseCoordinateWithoutRange(split),
                3 => ParseCoordinateWithRange(split),
                _ => throw new ArgumentException(string.Format(_couldNotPraseCoordsMessage, coords))
            };

        }
    }

    private ChunkRange ParseCoordinateWithoutRange(string[] coords)
    {
        if (!int.TryParse(coords[0], out int x)
            || !int.TryParse(coords[1], out int z))
        {
            throw new ArgumentException(string.Format(_couldNotPraseCoordsMessage, string.Join(",", coords)));
        }
        return new ChunkRange(x, z, Range);
    }

    private ChunkRange ParseCoordinateWithRange(string[] coords)
    {
        if (!int.TryParse(coords[0], out int x)
            || !int.TryParse(coords[1], out int z)
            || !int.TryParse(coords[2], out int r))
        {
            throw new ArgumentException(string.Format(_couldNotPraseCoordsMessage, string.Join(",", coords)));
        }
        return new ChunkRange(x, z, r);
    }
}
