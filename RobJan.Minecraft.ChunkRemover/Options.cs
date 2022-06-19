﻿using CommandLine;

namespace RobJan.Minecraft.ChunkRemover;

internal class Options
{
    public Options(string worldPath, int range, IEnumerable<string> coordinates)
    {
        WorldPath = worldPath;
        Range = range;
        Coordinates = coordinates;
    }

    [Option('w', "world", Default = ".", HelpText = "Path to the world folder.")]
    public string WorldPath { get; }

    [Option('r', "range", Default = 32, HelpText = "Range (in chunks) from any specified coordinates where no data will be removed.")]
    public int Range { get; }

    [Option('c', "coordinates", Required = true, HelpText = "List of coordinates to not remove data around. Format: x,y")]
    public IEnumerable<string> Coordinates { get; }
}
