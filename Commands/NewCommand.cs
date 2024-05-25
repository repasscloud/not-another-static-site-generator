using NASSG.Helpers;

namespace NASSG.Commands;

public static class NewCommand
{
    public static void Execute()
    {
        Console.WriteLine("Executing 'new' command...");
        // Add your implementation here

        string configContent = NewSiteHelper.NewConfigToml();
        configContent.WriteStringToFile();
    }
}
