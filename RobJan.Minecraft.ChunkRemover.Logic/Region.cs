using System.Text.RegularExpressions;

namespace RobJan.Minecraft.ChunkRemover.Logic;

public struct Region
{
    public Region(int x, int y)
    {
        X = x;
        Y = y;
        MinChunkX = X * 32;
        MaxChunkX = MinChunkX + 31;
        MinChunkY = Y * 32;
        MaxChunkY = MinChunkY + 31;
    }

    public int X { get; }
    public int Y { get; }

    public int MinChunkX { get; }
    public int MaxChunkX { get; }
    public int MinChunkY { get; }
    public int MaxChunkY { get; }

    public string FileName => $"r.{X}.{Y}.mca";
    public static Regex FileNameRegex { get; } = new(@"r(\.-?\d+){2}\.mca", RegexOptions.Compiled);

    public static Region FromFileName(string fileName)
    {
        if (!FileNameRegex.IsMatch(fileName))
            throw new ArgumentException($"Invalid file name: {fileName}");

        var parts = fileName.Split('.');
        return new Region(int.Parse(parts[1]), int.Parse(parts[2]));
    }

    public static Region FromChunk(int x, int y)
    {
        return new Region(
            x < 0 ? x / 32 - 1 : x / 32,
            y < 0 ? y / 32 - 1 : y / 32);
    }

    public bool IsInRangeOf(Chunk chunk, int chunkCount)
    {
        if (chunkCount < 0)
            throw new ArgumentOutOfRangeException(nameof(chunkCount));

        return MinChunkX - chunkCount <= chunk.X && chunk.X <= MaxChunkX + chunkCount
            && MinChunkY - chunkCount <= chunk.Y && chunk.Y <= MaxChunkY + chunkCount;
    }

    public override string ToString()
    {
        return $"{X}, {Y}";
    }
}
