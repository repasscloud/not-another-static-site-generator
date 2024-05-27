using NASSG.Models.Static;

namespace NASSG;

public static class AppConfig
{
    public static string appName = "NASSG";
    public static string appLongName = "Not Another Static Site Generator";
    public static Version appVersion = new Version(0, 0, 1);
    public static string currentDirectory = Directory.GetCurrentDirectory();
    public static string baseURL = string.Empty;
    public static bool buildDrafts = false;
    public static bool buildExpired = false;
    public static bool buildFuture = false;
    public static bool clearDestinationDir = false;
    public static string configFile = "config.toml";
    public static string configFilePath = Path.Combine(currentDirectory, configFile);
    public static string contentDir = "source";
    public static DateTime clock = DateTime.UtcNow;
    public static bool debugMode = false;
    public static string destinationDir = "public";
    public static bool enableGitInfo = false;
    public static LogLevel logLevel = LogLevel.Error;
    public static bool minifyHtml { get; set; } = false;
    public static bool quietMode = true;
    public static string theme = string.Empty;
    public static string themesDir = "themes";
    public static bool verboseMode = false;
    public static bool watchMode = false;

    public static string googleAnalytics = string.Empty;

    public static List<string> Commands = new List<string>
    {
        "build",
        "config",
        "deploy",
        "help",
        "new",
        "serve",
        "version",
    };

    public static List<string> Flags = new List<string>
    {
        "-b", "--baseurl",
        "-D", "--builddrafts",
        "-E", "--buildexpired",
        "-f", "--buildfuture",
        "--cleandestinationdir",
        "--clock",
        "--config",
        "-c", "--contentdir",
        "--debug",
        "-d", "--destination",
        "--enablegitinfo",
        "-h", "--help",
        "--loglevel",
        "--minify",
        "--quiet",
        "-t", "--theme",
        "--themesdir",
        "-v", "--verbose",
        "-w", "--watch"
    };
}