namespace RobJan.Minecraft.ChunkRemover.Logic;

public struct Chunk
{
    public Chunk(int x, int y)
    {
        X = x;
        Y = y;
        Region = Region.FromChunk(x, y);
    }

    public int X { get; }
    public int Y { get; }

    public Region Region { get; }

    public static Chunk FromCoodinates(int x, int y)
    {
        return new Chunk(
            x < 0 ? x / 16 - 1 : x / 16,
            y < 0 ? y / 16 - 1 : y / 16);
    }
}
