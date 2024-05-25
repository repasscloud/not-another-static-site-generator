using NASSG.Helpers;

namespace NASSG.Commands
{
    public static class HelpCommand
    {
        public static void Execute(string? unknownCommand = null)
        {
            unknownCommand.PrintHelp();
            Environment.Exit(0);
        }
    }
}
