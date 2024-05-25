namespace NASSG;

public static class AppConfig
{
    public static string AppName = "NASSG";
    public static string AppLongName = "Not Another Static Site Generator";
    public static Version AppVersion = new Version(0, 0, 1);
    public static bool Minify { get; set; } = false;
    public static string CurrentDirectory = Path.Join(Directory.GetCurrentDirectory(), "source");

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
        "--builddrafts",
        "-e", "--buildexpired",
        "-f", "--buildfuture",
        "--cleandestinationdir",
        "--clock",
        "--config",
        "-c", "--contentdir",
        "--debug",
        "-d", "--destination",
        "--enablegitinfo",
        "-e", "--environment",
        "--forcesyncstatic",
        "--gc",
        "-h", "--help",
        "--ignorecache",
        "--ignorevendorpaths",
        "-l", "--layoutdir",
        "--loglevel",
        "--minify",
        "--nobuildlock",
        "--nochmod",
        "--notimes",
        "--paniconwarning",
        "--poll",
        "--printi18nwarnings",
        "--printmemoryusage",
        "--printpathwarnings",
        "--printunusedtemplates",
        "--quiet",
        "--rendersegments",
        "--rendertomemory",
        "-s", "--source",
        "--templatemetrics",
        "--templatemetricshints",
        "-t", "--theme",
        "--themesdir",
        "--trace",
        "-v", "--verbose",
        "-w", "--watch"
    };
}