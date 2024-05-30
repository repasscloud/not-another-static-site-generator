using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace NASSG.Helpers;

public static class NewSiteHelper
{
    public static void WriteStringToFile(this string content)
    {
        string sourceDir = Path.Combine(AppConfig.currentDirectory, "source");
        try
        {
            if (Directory.Exists(sourceDir))
            {
                Directory.Delete(sourceDir, true);
            }
            Directory.CreateDirectory(sourceDir);
            File.WriteAllText(Path.Combine(AppConfig.currentDirectory, "config.toml"), content, System.Text.Encoding.UTF8);
            File.WriteAllText(Path.Combine(sourceDir, "index.md"), content, System.Text.Encoding.UTF8);
            
            var lines = File.ReadAllLines(Path.Combine(AppConfig.currentDirectory, "config.toml"));
            
            if (lines.Length > 0)
            {
                // remove first line
                var linesToWrite = lines.Skip(1).ToArray();

                // remove "        " from start of each line
                var trimmedLines = linesToWrite.Select(line => line.TrimStart()).ToArray();

                File.WriteAllLines(Path.Combine(AppConfig.currentDirectory, "config.toml"), trimmedLines);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while writing to the file: {ex.Message}");
        }
    }

    public static string NewConfigToml()
    {
        string tomlDefault = @"
        [params]
          siteName = 'NASSG Site'
          baseUrl = 'https://example.com'
          mainButtonURL = 'https://example.com/get-started'
          mainButtonText = 'Get Started'
          copyright = ''
          googleAnalytics = ''

        [theme]
          name = 'default-theme'

        [content]
          source = 'source'
          output = 'public'

        [partials]
          header = 'partials/header.html'
          footer = 'partials/footer.html'

        [includes]
          [[includes.page]]
            url = '/about'
            page = 'about.md'
            changeFreq = 'monthly'
            priority = 0.5

            [[includes.page.subpage]]
              url = '/about/team'
              page = 'about/team.md'
              changeFreq = 'monthly'
              priority = 0.5

              [[includes.page.subpage.subpage]]
                url = '/about/team/leadership'
                page = 'about/team/leadership.md'
                changeFreq = 'monthly'
                priority = 0.5

            [[includes.page.subpage]]
              url = '/about/history'
              page = 'about/history.md'
              changeFreq = 'yearly'
              priority = 0.3

          [[includes.page]]
            url = '/contact'
            page = 'contact.md'
            changeFreq = 'yearly'
            priority = 0.3

        [build]
          debug = false
          destinationDir = ''
          enableGitInfo = true
          enableEmoji = true
          logLevel = 4
          minify = false
          quietMode = true
          themesDir = 'themes'

        [plugins]
          [plugins.markdown]
            enabled = true
            options = ['option1', 'option2']

          [plugins.sitemap]
            enabled = true
            output = 'sitemap.xml'

        # [[params.pages]]
        #   url = '/'
        #   changeFreq = 'daily'
        #   priority = 1.0

        # [[params.pages]]
        #   url = '/about'
        #   changeFreq = 'monthly'
        #   priority = 0.5

        # [[params.pages]]
        #   url = '/contact'
        #   changeFreq = 'yearly'
        #   priority = 0.3

        # [[params.pages]]
        #   url = '/blog'
        #   changeFreq = 'weekly'
        #   priority = 0.7";

        return tomlDefault;
    }
}