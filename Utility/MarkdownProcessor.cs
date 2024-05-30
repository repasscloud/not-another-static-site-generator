using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Markdig;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace NASSG.Utility;
public class MarkdownProcessor
{
    public static (Dictionary<string, object> FrontMatter, string Content) ExtractFrontMatter(string markdownContent)
    {
        const string frontMatterDelimiter = "---";
        var lines = markdownContent.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        if (lines.Length > 2 && lines[0] == frontMatterDelimiter)
        {
            var endOfFrontMatterIndex = Array.IndexOf(lines, frontMatterDelimiter, 1);
            if (endOfFrontMatterIndex > 0)
            {
                var frontMatterLines = lines.Skip(1).Take(endOfFrontMatterIndex - 1);
                var contentLines = lines.Skip(endOfFrontMatterIndex + 1);
                var frontMatter = string.Join(Environment.NewLine, frontMatterLines);
                var content = string.Join(Environment.NewLine, contentLines);

                var deserializer = new DeserializerBuilder()
                    .WithNamingConvention(CamelCaseNamingConvention.Instance)
                    .Build();

                var metadata = deserializer.Deserialize<Dictionary<string, object>>(frontMatter);
                return (metadata, content);
            }
        }
        return (new Dictionary<string, object>(), markdownContent); // No front matter found
    }

    public static string ConvertMarkdownToHtml(string markdownContent)
    {
        return Markdown.ToHtml(markdownContent);
    }

    public static (Dictionary<string, object> FrontMatter, string HtmlContent) ProcessMarkdownFile(string markdownPath)
    {
        var markdownContent = File.ReadAllText(markdownPath);
        var (frontMatter, content) = ExtractFrontMatter(markdownContent);
        var htmlContent = ConvertMarkdownToHtml(content);
        return (frontMatter, htmlContent);
    }
}
