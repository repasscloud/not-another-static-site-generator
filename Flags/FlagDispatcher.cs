namespace NASSG.Commands;

public static class FlagDispatcher
{
    public static bool CheckFlag(string flag)
    {
        flag = flag.ToLower();
        return AppConfig.Flags.Contains(flag);
    }

    public static void DispatchFlag(string flag, string? value)
    {
        if (flag.Length == 2)
        {
            switch (flag)
            {
                case "-b":
                    HandleBaseUrl(value);
                    break;
                case "-D":
                    HandleBuildDrafts();
                    break;
                case "-E":
                    HandleBuildExpired();
                    break;
                case "-F":
                    HandleBuildFuture();
                    break;
                case "-c":
                    HandleContentDir(value);
                    break;
                case "-d":
                    HandleDestination(value);
                    break;
                case "-h":
                    HandleHelp();
                    break;
                case "-t":
                    HandleTheme(value);
                    break;
                case "-v":
                    HandleVerbose();
                    break;
                case "-w":
                    HandleWatch();
                    break;
            }
        }
        else
        {
            flag = flag.ToLower();

            switch (flag)
            {
                case "--baseURL":
                    HandleBaseUrl(value);
                    break;
                case "--buildDrafts":
                    HandleBuildDrafts();
                    break;
                case "--buildExpired":
                    HandleBuildExpired();
                    break;
                case "--buildFuture":
                    HandleBuildFuture();
                    break;
                case "--cleandestinationdir":
                    HandleCleanDestinationDir();
                    break;
                case "--clock":
                    HandleClock(value);
                    break;
                case "--config":
                    HandleConfig(value);
                    break;
                case "--contentdir":
                    HandleContentDir(value);
                    break;
                case "--debug":
                    HandleDebug();
                    break;
                case "--destination":
                    HandleDestination(value);
                    break;
                case "--enablegitinfo":
                    HandleEnableGitInfo();
                    break;
                case "--environment":
                    HandleEnvironment(value);
                    break;
                case "--forcesyncstatic":
                    HandleForceSyncStatic();
                    break;
                case "--help":
                    HandleHelp();
                    break;
                case "--ignorecache":
                    HandleIgnoreCache();
                    break;
                case "--ignorevendorpaths":
                    HandleIgnoreVendorPaths(value);
                    break;
                case "--layoutdir":
                    HandleLayoutDir(value);
                    break;
                case "--loglevel":
                    HandleLogLevel(value);
                    break;
                case "--minify":
                    HandleMinify();
                    break;
                case "--nobuildlock":
                    HandleNoBuildLock();
                    break;
                case "--nochmod":
                    HandleNoChmod();
                    break;
                case "--notimes":
                    HandleNoTimes();
                    break;
                case "--paniconwarning":
                    HandlePanicOnWarning();
                    break;
                case "--poll":
                    HandlePoll(value);
                    break;
                case "--printi18nwarnings":
                    HandlePrintI18nWarnings();
                    break;
                case "--printmemoryusage":
                    HandlePrintMemoryUsage();
                    break;
                case "--printpathwarnings":
                    HandlePrintPathWarnings();
                    break;
                case "--printunusedtemplates":
                    HandlePrintUnusedTemplates();
                    break;
                case "--quiet":
                    HandleQuiet();
                    break;
                case "--rendersegments":
                    HandleRenderSegments(value);
                    break;
                case "--rendertomemory":
                    HandleRenderToMemory();
                    break;
                case "--source":
                    HandleSource(value);
                    break;
                case "--templatemetrics":
                    HandleTemplateMetrics();
                    break;
                case "--templatemetricshints":
                    HandleTemplateMetricsHints();
                    break;
                case "--theme":
                    HandleTheme(value);
                    break;
                case "--themesdir":
                    HandleThemesDir(value);
                    break;
                case "--trace":
                    HandleTrace(value);
                    break;
                case "--verbose":
                    HandleVerbose();
                    break;
                case "--watch":
                    HandleWatch();
                    break;
                default:
                    Console.WriteLine($"Unknown flag: {flag}");
                    break;
            }
        }
    }

    private static void HandleBaseUrl(string? value) => Console.WriteLine($"Handling --baseurl flag with value: {value}");
    private static void HandleBuildDrafts() => Console.WriteLine("Handling --builddrafts flag...");
    private static void HandleBuildExpired() => Console.WriteLine("Handling --buildexpired flag...");
    private static void HandleBuildFuture() => Console.WriteLine("Handling --buildfuture flag...");
    private static void HandleCleanDestinationDir() => Console.WriteLine("Handling --cleandestinationdir flag...");
    private static void HandleClock(string? value) => Console.WriteLine($"Handling --clock flag with value: {value}");
    private static void HandleConfig(string? value) => Console.WriteLine($"Handling --config flag with value: {value}");
    private static void HandleContentDir(string? value)
    {
        if (value is not null && Directory.Exists(value))
        {
            AppConfig.currentDirectory = value;
        }

        if (!File.Exists(Path.Combine(AppConfig.currentDirectory, "index.md")))
        {
            Console.WriteLine($"Content dir missing file 'index.md': {AppConfig.currentDirectory}");
            Environment.Exit(1);
        }
    }
    private static void HandleDebug() => Console.WriteLine("Handling --debug flag...");
    private static void HandleDestination(string? value) => Console.WriteLine($"Handling --destination flag with value: {value}");
    private static void HandleEnableGitInfo() => Console.WriteLine("Handling --enablegitinfo flag...");
    private static void HandleEnvironment(string? value) => Console.WriteLine($"Handling --environment flag with value: {value}");
    private static void HandleForceSyncStatic() => Console.WriteLine("Handling --forcesyncstatic flag...");
    private static void HandleGC() => Console.WriteLine("Handling --gc flag...");
    private static void HandleHelp() => Console.WriteLine("Handling --help flag...");
    private static void HandleIgnoreCache() => Console.WriteLine("Handling --ignorecache flag...");
    private static void HandleIgnoreVendorPaths(string? value) => Console.WriteLine($"Handling --ignorevendorpaths flag with value: {value}");
    private static void HandleLayoutDir(string? value) => Console.WriteLine($"Handling --layoutdir flag with value: {value}");
    private static void HandleLogLevel(string? value) => Console.WriteLine($"Handling --loglevel flag with value: {value}");
    private static void HandleMinify() => AppConfig.minifyHtml = true;
    private static void HandleNoBuildLock() => Console.WriteLine("Handling --nobuildlock flag...");
    private static void HandleNoChmod() => Console.WriteLine("Handling --nochmod flag...");
    private static void HandleNoTimes() => Console.WriteLine("Handling --notimes flag...");
    private static void HandlePanicOnWarning() => Console.WriteLine("Handling --paniconwarning flag...");
    private static void HandlePoll(string? value) => Console.WriteLine($"Handling --poll flag with value: {value}");
    private static void HandlePrintI18nWarnings() => Console.WriteLine("Handling --printi18nwarnings flag...");
    private static void HandlePrintMemoryUsage() => Console.WriteLine("Handling --printmemoryusage flag...");
    private static void HandlePrintPathWarnings() => Console.WriteLine("Handling --printpathwarnings flag...");
    private static void HandlePrintUnusedTemplates() => Console.WriteLine("Handling --printunusedtemplates flag...");
    private static void HandleQuiet() => Console.WriteLine("Handling --quiet flag...");
    private static void HandleRenderSegments(string? value) => Console.WriteLine($"Handling --rendersegments flag with value: {value}");
    private static void HandleRenderToMemory() => Console.WriteLine("Handling --rendertomemory flag...");
    private static void HandleSource(string? value) => Console.WriteLine($"Handling --source flag with value: {value}");
    private static void HandleTemplateMetrics() => Console.WriteLine("Handling --templatemetrics flag...");
    private static void HandleTemplateMetricsHints() => Console.WriteLine("Handling --templatemetricshints flag...");
    private static void HandleTheme(string? value) => Console.WriteLine($"Handling --theme flag with value: {value}");
    private static void HandleThemesDir(string? value) => Console.WriteLine($"Handling --themesdir flag with value: {value}");
    private static void HandleTrace(string? value) => Console.WriteLine($"Handling --trace flag with value: {value}");
    private static void HandleVerbose() => Console.WriteLine("Handling --verbose flag...");
    private static void HandleWatch() => Console.WriteLine("Handling --watch flag...");
}
