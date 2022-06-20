namespace RobJan.Minecraft.ChunkRemover.Logic;

public struct Chunk
{
    public Chunk(int x, int z)
    {
        X = x;
        Z = z;
        Region = Region.FromChunk(x, z);
    }

    public int X { get; }
    public int Z { get; }

    public Region Region { get; }

    public static Chunk FromCoodinates(int x, int z)
    {
        return new Chunk(
            x < 0 ? x / 16 - 1 : x / 16,
            z < 0 ? z / 16 - 1 : z / 16);
    }
}
