namespace NASSG.Commands;

public static class BuildCommand
{
    public static void Execute()
    {
        // check the "index.md" file is found, else exit
        if (!File.Exists(Path.Combine(AppConfig.CurrentDirectory, "index.md")))
        {
            Console.WriteLine($"Contend dir missing file 'index.md': {AppConfig.CurrentDirectory}");
            Environment.Exit(1);
        }

        Console.WriteLine("Executing 'build' command...");
        // Add your implementation here
    }
}
