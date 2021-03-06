using RobJan.Minecraft.ChunkRemover.Logic;

namespace RobJan.Minecraft.ChunkRemover.Tests;

public class RegionTests
{
    [TestCase(0, 0, 0, 0)]
    [TestCase(-1, 0, -1, 0)]
    [TestCase(-1, -1, -1, -1)]
    [TestCase(0, -1, 0, -1)]
    [TestCase(4, -8, 0, -1)]
    [TestCase(625, 625, 19, 19)]
    public void FromChunk_GeneratesCorrectRegion(int x, int z, int chunkX, int chunkY)
    {
        var region = Region.FromChunk(x, z);
        Assert.Multiple(() =>
        {
            Assert.That(region.X, Is.EqualTo(chunkX));
            Assert.That(region.Z, Is.EqualTo(chunkY));
        });
    }

    [TestCase(19, 19, 608, 625, 0, true)]
    [TestCase(19, 19, 607, 624, 1, true)]
    [TestCase(19, 19, 607, 624, 0, false)]
    [TestCase(0, 0, 0, 0, 0, true)]
    [TestCase(1, 1, -20, -20, 51, false)]
    [TestCase(1, 1, -20, -20, 52, true)]
    public void IsInRangeOf(int regionX, int regionZ, int chunkX, int chunkZ, int range, bool expected)
    {
        var region = new Region(regionX, regionZ);
        var chunk = new Chunk(chunkX, chunkZ);

        var result = region.IsInRangeOf(chunk, range);

        Assert.That(result, Is.EqualTo(expected));
    }
}
