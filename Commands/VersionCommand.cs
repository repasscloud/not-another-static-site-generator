namespace NASSG.Commands;

public static class VersionCommand
{
    public static void Execute()
    {
        Console.WriteLine($"Version: {AppConfig.appVersion}");
    }
}
