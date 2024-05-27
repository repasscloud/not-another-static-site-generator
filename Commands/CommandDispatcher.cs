using System;
using System.Linq;
using NASSG.Helpers;

namespace NASSG.Commands;

public static class CommandDispatcher
{
    public static bool CheckCommand(string command)
    {
        command = command.ToLower();
        return AppConfig.Commands.Contains(command);
    }

    public static void DispatchCommand(string command, string[] args)
    {

        // ensure args is not null
        args ??= Array.Empty<string>();

        command = command.ToLower();

        switch (command)
        {
            case "build":
                BuildCommand.Execute(args);  // build command needs args, for processing after config.toml
                break;
            case "config":
                ConfigCommand.Execute();
                break;
            case "deploy":
                DeployCommand.Execute();
                break;
            case "help":
                HelpCommand.Execute();
                break;
            case "new":
                NewCommand.Execute();
                break;
            case "serve":
                ServeCommand.Execute();
                break;
            case "version":
                VersionCommand.Execute();
                break;
            default:
                command.PrintHelp();
                break;
        }
    }
}
