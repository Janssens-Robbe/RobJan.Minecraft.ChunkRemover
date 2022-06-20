namespace RobJan.Minecraft.ChunkRemover;

[Verb("config")]
public class UseConfigOptions
{
    public UseConfigOptions(string configFilePath)
    {
        ConfigFilePath = configFilePath;
    }

    [Value(0, Required = true, HelpText = ".yml file with all required info to run the remover.")]
    public string ConfigFilePath { get; }
}
