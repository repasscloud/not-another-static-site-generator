using Tomlyn;
using Tomlyn.Model;

namespace NASSG.Core;

public static class ConfigLoader
{
    public static TomlTable LoadConfig(string configPath)
    {
        var configContent = File.ReadAllText(configPath);
        return Toml.ToModel(configContent) as TomlTable;
    }
}