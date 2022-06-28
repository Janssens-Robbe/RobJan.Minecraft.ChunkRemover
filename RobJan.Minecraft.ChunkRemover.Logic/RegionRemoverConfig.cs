namespace RobJan.Minecraft.ChunkRemover.Logic
{
    public sealed class RegionRemoverConfig
    {
        public RegionRemoverConfig(string worldPath, IEnumerable<ChunkRange> placesToKeep, int range)
        {
            WorldPath = worldPath;
            RegionPath = Path.Combine(worldPath, "region");
            PlacesToKeep = placesToKeep;
            Range = range;

            if (!Directory.Exists(WorldPath))
                throw new ArgumentException($"Folder \"{WorldPath}\" does not exist.", nameof(worldPath));
            if (!Directory.Exists(RegionPath))
                throw new ArgumentException($"Folder \"{RegionPath}\" does not exist.", nameof(worldPath));
        }

        public string WorldPath { get; }
        public string RegionPath { get; }
        public IEnumerable<ChunkRange> PlacesToKeep { get; }
        public int Range { get; }
    }
}
