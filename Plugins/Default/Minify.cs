namespace NASSG.Plugins.Default;

public static class Minify
{
    public static string MinifyHtml(this string html)
    {
        string minifiedHtml = html.Replace("\r\n", "").Replace("\n", "").Replace("\r", "");
        return minifiedHtml;
    }
}