using System.Text.RegularExpressions;

namespace RobJan.Minecraft.ChunkRemover.Logic;

public struct Region
{
    public Region(int x, int y)
    {
        X = x;
        Z = y;
        MinChunkX = X * 32;
        MaxChunkX = MinChunkX + 31;
        MinChunkZ = Z * 32;
        MaxChunkZ = MinChunkZ + 31;
    }

    public int X { get; }
    public int Z { get; }

    public int MinChunkX { get; }
    public int MaxChunkX { get; }
    public int MinChunkZ { get; }
    public int MaxChunkZ { get; }

    public string FileName => $"r.{X}.{Z}.mca";
    public static Regex FileNameRegex { get; } = new(@"r(\.-?\d+){2}\.mca", RegexOptions.Compiled);

    public static Region FromFileName(string fileName)
    {
        if (!FileNameRegex.IsMatch(fileName))
            throw new ArgumentException($"Invalid file name: {fileName}");

        var parts = fileName.Split('.');
        return new Region(int.Parse(parts[1]), int.Parse(parts[2]));
    }

    public static Region FromChunk(int x, int z)
    {
        return new Region(
            x < 0 ? x / 32 - 1 : x / 32,
            z < 0 ? z / 32 - 1 : z / 32);
    }

    public bool IsInRangeOf(Chunk chunk, int chunkCount)
    {
        if (chunkCount < 0)
            throw new ArgumentOutOfRangeException(nameof(chunkCount));

        return MinChunkX - chunkCount <= chunk.X && chunk.X <= MaxChunkX + chunkCount
            && MinChunkZ - chunkCount <= chunk.Z && chunk.Z <= MaxChunkZ + chunkCount;
    }

    public override string ToString()
    {
        return $"{X}, {Z}";
    }
}
