using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace RobJan.Minecraft.ChunkRemover;

[Verb("config")]
internal class UseConfigOptions : BaseOptions
{
    public UseConfigOptions(string configFilePath, bool noConfirm)
        : base(noConfirm)
    {
        ConfigFilePath = configFilePath;
    }

    [Value(0, Required = true, HelpText = ".yaml file with all required info to run the remover.")]
    public string ConfigFilePath { get; }

    public override RegionRemoverConfig ToRegionRemoverConfig()
    {

        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();

        var data = File.ReadAllText(ConfigFilePath);

        var config = deserializer.Deserialize<ConfigFileRoot>(data);
        throw new NotImplementedException();
    }
}

internal class ConfigFileRoot
{
    public string WorldPath { get; set; } = ".";
    public IEnumerable<ConfigFileRegionRemover> RegionRemovers { get; set; }
}

internal class ConfigFileRegionRemover
{
    public string? Name { get; set; }
    public int Range { get; set; } = 32;
    public IEnumerable<ConfigFileCoordinate> Coordinates { get; set; } = new List<ConfigFileCoordinate> { new() { X = 0, Z = 0 } };
}

internal class ConfigFileCoordinate
{
    public int X { get; set; }
    public int Z { get; set; }
    public int? Range { get; set; }
}
