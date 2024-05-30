using NASSG.Helpers;
using NASSG.Commands;
using System.Linq;
using System;

namespace NASSG;

class Program
{
    static void Main(string[] args)
    {
        // check args[0] passed is correct or print help and exit
        if (args.Length == 0 || !CommandDispatcher.CheckCommand(args[0]))
        {
            string? unknownCommand = args.Length > 0 ? args[0] : null;
            unknownCommand.PrintHelp();
            Environment.Exit(1);
        }

        CommandDispatcher.DispatchCommand(args[0], args.Skip(1).ToArray());
    }
}
