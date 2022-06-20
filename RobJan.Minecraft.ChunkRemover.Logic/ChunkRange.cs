namespace RobJan.Minecraft.ChunkRemover.Logic;

public struct ChunkRange
{
    public ChunkRange(int x, int z, int range)
        : this(new(x, z), range) { }

    public ChunkRange(Coordinate coordinate, int range)
    {
        if (range < 0)
            throw new ArgumentOutOfRangeException(nameof(range));

        Coordinate = coordinate;
        Range = range;
    }

    public Coordinate Coordinate { get; }
    public int Range { get; }
}
