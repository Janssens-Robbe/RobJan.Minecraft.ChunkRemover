namespace RobJan.Minecraft.ChunkRemover;

[Verb("remove", isDefault: true, HelpText = "Removes data from a world.")]
internal class Options
{
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
}
