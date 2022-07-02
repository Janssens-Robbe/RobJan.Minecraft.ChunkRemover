namespace RobJan.Minecraft.ChunkRemover;

internal abstract class BaseOptions
{
    public BaseOptions(bool noConfirm)
    {
        NoConfirm = noConfirm;
    }

    [Option("no-confirm", HelpText = "When spesified, no confirmations will be asked.")]
    public bool NoConfirm { get; }

    public abstract RegionRemoverConfig ToRegionRemoverConfig();
}
