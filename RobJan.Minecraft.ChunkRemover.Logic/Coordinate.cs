namespace RobJan.Minecraft.ChunkRemover.Logic;

public struct Coordinate
{
    public Coordinate(int x, int y)
    {
        X = x;
        Y = y;
        Chunk = Chunk.FromCoodinates(x, y);
    }

    public int X { get; }
    public int Y { get; }

    public Chunk Chunk { get; }
}
