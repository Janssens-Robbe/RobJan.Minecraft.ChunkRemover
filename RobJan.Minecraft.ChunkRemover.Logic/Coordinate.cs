namespace RobJan.Minecraft.ChunkRemover.Logic;

public struct Coordinate
{
    public Coordinate(int x, int z)
    {
        X = x;
        Z = z;
        Chunk = Chunk.FromCoodinates(x, z);
    }

    public int X { get; }
    public int Z { get; }

    public Chunk Chunk { get; }
}
