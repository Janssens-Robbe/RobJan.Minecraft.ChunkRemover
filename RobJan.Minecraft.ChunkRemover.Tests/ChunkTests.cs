using RobJan.Minecraft.ChunkRemover.Logic;

namespace RobJan.Minecraft.ChunkRemover.Tests;

public class ChunkTests
{
    [TestCase(0, 0, 0, 0)]
    [TestCase(-1, 0, -1, 0)]
    [TestCase(-1, -1, -1, -1)]
    [TestCase(0, -1, 0, -1)]
    [TestCase(66, -122, 4, -8)]
    [TestCase(10000, 10000, 625, 625)]
    public void FromCoordinates_GeneratesCorrectCoordinates(int x, int y, int chunkX, int chunkY)
    {
        var chunk = Chunk.FromCoodinates(x, y);
        Assert.Multiple(() =>
        {
            Assert.That(chunk.X, Is.EqualTo(chunkX));
            Assert.That(chunk.Z, Is.EqualTo(chunkY));
        });
    }
}
