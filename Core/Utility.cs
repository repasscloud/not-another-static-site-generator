using Tomlyn.Model;

namespace NASSG.Core;

public static class Utility
{
    public static bool IsVariableSet(TomlTable model, string variablePath)
    {
        var parts = variablePath.Split('.');
        var current = model as object;

        foreach (var part in parts)
        {
            if (current is TomlTable table && table.TryGetValue(part, out var value))
            {
                current = value;
            }
            else
            {
                return false;
            }
        }
        return current != null;
    }

    public static object? GetVariableValue(TomlTable model, string variablePath)
    {
        var parts = variablePath.Split('.');
        var current = model as object;

        foreach (var part in parts)
        {
            if (current is TomlTable table && table.TryGetValue(part, out var value))
            {
                current = value;
            }
            else
            {
                return null;
            }
        }
        return current;
    }
}
