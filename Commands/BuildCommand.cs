using Tomlyn;
using Tomlyn.Model;

namespace NASSG.Commands;

public static class BuildCommand
{
    public static void Execute(string[] args)
    {
        // check for config.toml file
        string configToml = Path.Combine(AppConfig.currentDirectory, AppConfig.configFile);
        if (!File.Exists(configToml))
        {
            Console.WriteLine($"Config file missing: {configToml})");
            Environment.Exit(1);
        }
        
        // check the "index.md" file is found, else exit
        if (!File.Exists(Path.Combine(AppConfig.currentDirectory, AppConfig.contentDir, "index.md")))
        {
            Console.WriteLine($"Content directory missing file 'index.md': {Path.Combine(AppConfig.currentDirectory, AppConfig.contentDir, "index.md")}");
            Environment.Exit(2);
        }

        Console.WriteLine("Executing 'build' command...");

        // read config.toml file
        var tomlContent = File.ReadAllText(configToml);

        // Parse TOML content into a TomlTable
        var model = Toml.ToModel(tomlContent);

        // process everything in the config.toml first, then replace it with the arguments, if they are provided
        // theme
        if (model.TryGetValue("theme", out var themeName) && themeName is string)
        {
            Console.WriteLine($"Theme used is: {themeName}");
            AppConfig.theme = (string)themeName;
        }

        if (model.TryGetValue("params", out var paramsValue) && paramsValue is TomlTable paramsTable)
        {
            // baseURL
            if (paramsTable.TryGetValue("baseUrl", out var baseUrlValue) && baseUrlValue is string baseUrl)
            {
                AppConfig.baseURL = baseUrl;
            }
        }

        if (model.TryGetValue("build", out var buildValues) && buildValues is TomlTable buildTable)
        {
            // minify
            if (buildTable.TryGetValue("minify", out var minifyValue) && minifyValue is bool minify)
            {
                AppConfig.minifyHtml = minify;
            }

            // enableGitInfo
            if (buildTable.TryGetValue("enableGitInfo", out var enableGitInfoValue) && enableGitInfoValue is bool enableGitInfo)
            {
                AppConfig.enableGitInfo = enableGitInfo;
            }
        }


        // parse all the args (if any)
        if (args.Length > 0)
        {
            // check and dispatch flags
            for (int i = 1; i < args.Length; i++)
            {
                if (FlagDispatcher.CheckFlag(args[i]))
                {
                    if (i + 1 < args.Length && !FlagDispatcher.CheckFlag(args[i + 1]))
                    {
                        FlagDispatcher.DispatchFlag(args[i], args[i + 1]);
                        i++; // skip the next argument as it is used as a parameter for the current flag
                    }
                    else
                    {
                        FlagDispatcher.DispatchFlag(args[i], null);
                    }
                }
                else
                {
                    Console.WriteLine($"Unknown flag: {args[i]}");
                }
            }
        }

        // find the pages to build and routes

        
        // Add your implementation here
        // Access the "includes" table
        if (model.TryGetValue("includes", out var includesValue) && includesValue is TomlTable includesTable)
        {
            // Access the "page" array of tables within the "includes" table
            if (includesTable.TryGetValue("page", out var pagesValue) && pagesValue is TomlArray pagesArray)
            {
                foreach (var pageEntry in pagesArray)
                {
                    if (pageEntry is TomlTable pageTable)
                    {
                        // Access the "url" and "page" values
                        if (pageTable.TryGetValue("url", out var urlValue) && urlValue is string url &&
                            pageTable.TryGetValue("page", out var pageValue) && pageValue is string page)
                        {
                            Console.WriteLine($"URL: {url}, Page: {page}");

                            // Check for subpages (first level)
                            if (pageTable.TryGetValue("subpage", out var subpagesValue) && subpagesValue is TomlArray subpagesArray)
                            {
                                foreach (var subpageEntry in subpagesArray)
                                {
                                    if (subpageEntry is TomlTable subpageTable)
                                    {
                                        if (subpageTable.TryGetValue("url", out var subUrlValue) && subUrlValue is string subUrl &&
                                            subpageTable.TryGetValue("page", out var subPageValue) && subPageValue is string subPage)
                                        {
                                            Console.WriteLine($"\tSubpage URL: {subUrl}, Subpage: {subPage}");

                                            // Check for second level subpages
                                            if (subpageTable.TryGetValue("subpage", out var subSubpagesValue) && subSubpagesValue is TomlArray subSubpagesArray)
                                            {
                                                foreach (var subSubpageEntry in subSubpagesArray)
                                                {
                                                    if (subSubpageEntry is TomlTable subSubpageTable)
                                                    {
                                                        if (subSubpageTable.TryGetValue("url", out var subSubUrlValue) && subSubUrlValue is string subSubUrl &&
                                                            subSubpageTable.TryGetValue("page", out var subSubPageValue) && subSubPageValue is string subSubPage)
                                                        {
                                                            Console.WriteLine($"\t\tSub-subpage URL: {subSubUrl}, Sub-subpage: {subSubPage}");
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("No pages found in includes.");
            }
        }
    }
}
