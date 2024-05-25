namespace NASSG.Helpers;

public static class NewSiteHelper
{
    public static void WriteStringToFile(this string content)
    {
        try
        {
            if (Directory.Exists(AppConfig.CurrentDirectory))
            {
                Directory.Delete(AppConfig.CurrentDirectory, true);
            }
            Directory.CreateDirectory(AppConfig.CurrentDirectory);
            File.WriteAllText(Path.Combine(AppConfig.CurrentDirectory, "config.toml"), content, System.Text.Encoding.UTF8);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while writing to the file: {ex.Message}");
        }
    }

    public static string NewConfigToml()
    {
        string tomlDefault = @"[content]
  source = ""content""
  output = ""public""

[theme]
  name = ""default-theme""

[partials]
  header = ""partials/header.html""
  footer = ""partials/footer.html""

[build]
  minify = false
  enableGitInfo = true

[params]
  siteName = ""NASSG Site""
  baseUrl = ""https://example.com""
  mainButtonURL = ""https://example.com/get-started""
  mainButtonText = ""Get Started""

[[params.pages]]
  url = ""/""
  changeFreq = ""daily""
  priority = 1.0

[[params.pages]]
  url = ""/about""
  changeFreq = ""monthly""
  priority = 0.5

[[params.pages]]
  url = ""/contact""
  changeFreq = ""yearly""
  priority = 0.3

[[params.pages]]
  url = ""/blog""
  changeFreq = ""weekly""
  priority = 0.7

[plugins]
  [plugins.markdown]
    enabled = true
    options = [""option1"", ""option2""]

  [plugins.sitemap]
    enabled = true
    output = ""sitemap.xml""
";
        return tomlDefault;
    }
}