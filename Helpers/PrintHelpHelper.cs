using System;
using System.Collections.Generic;

namespace NASSG.Helpers
{
    public static class PrintHelpHelper
    {
        public static void PrintHelp(this string? unknownCommand)
        {
            Console.WriteLine("nassg is the main command, used to build your NASSG C# site.\n");
            Console.WriteLine("NASSG is a fast and flexible (Not Another) Static Site Generator");
            Console.WriteLine("built with love by danijeljw and friends in C#.\n");
            Console.WriteLine("Complete documentation is available at: https://nassg.github.io\n");
            Console.WriteLine("Usage:");
            Console.WriteLine("  nassg [command] [flags]");
            Console.WriteLine("Available Commands:");
            PrintCommands();
            Console.WriteLine("\nFlags:");
            PrintFlags();
            Console.WriteLine("\nUse \"nassg [command] --help\" for more information about a command.");
            if (unknownCommand != null)
            {
                Console.WriteLine($"\nError: command error: unknown command {unknownCommand}");
            }
        }

        private static void PrintCommands()
        {
            var commands = new List<string>
            {
                "  config      Print the site configuration",
                "  deploy      Deploy your site to a Cloud provider",
                "  help        Help about any command",
                "  new         Create new content for your site",
                "  serve       A localhost webserver of your site",
                "  version     Print NASSG version"
            };

            foreach (var command in commands)
            {
                Console.WriteLine(command);
            }
        }

        private static void PrintFlags()
        {
            var flags = new List<string>
            {
                "  -b, --baseURL string             hostname (and path) to the root, e.g. https://example.com/",
                "  -D, --buildDrafts                include content marked as draft",
                "  -E, --buildExpired               include expired content",
                "  -F, --buildFuture                include content with publishdate in the future",
                "      --cleanDestinationDir        remove files from destination not found in static directories",
                "      --clock string               set the clock used by NASSG, e.g. --clock 2021-11-06T22:30:00.00+09:00",
                "      --config string              config file (default is config.yaml|json|toml)",
                "  -c, --contentDir string          filesystem path to content directory",
                "      --debug                      debug output",
                "  -d, --destination string         filesystem path to write files to",
                "      --enableGitInfo              add Git revision, date, author, and CODEOWNERS info to the pages",
                "  -h, --help                       help for NASSG",
                "      --logLevel string            log level (debug|info|warn|error)",
                "      --minify                     minify any supported output format (HTML, XML etc.)",
                "      --quiet                      build in quiet mode",
                "  -t, --theme string               theme to use (located in /themes/THEMENAME/)",
                "      --themesDir string           filesystem path to themes directory",
                "  -v, --verbose                    verbose output",
                "  -w, --watch                      watch filesystem for changes and recreate as needed"
            };

            foreach (var flag in flags)
            {
                Console.WriteLine(flag);
            }
        }
    }
}
