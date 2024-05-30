using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Markdig;
using Tomlyn.Model;

namespace NASSG.Core;

public static class TemplateProcessor
{
    public static string ProcessTemplate(string templateContent, TomlTable model, Dictionary<string, object?> variables)
    {
        // Handle partial includes
        templateContent = Regex.Replace(templateContent, @"{{\s*partial\s+(\w+)\s*}}", match =>
        {
            var partialKey = match.Groups[1].Value;
            var partialPath = GetTomlValue(model, $"theme.partials.{partialKey}");
            if (partialPath != null && File.Exists(partialPath))
            {
                var partialContent = File.ReadAllText(partialPath);
                return ProcessTemplate(partialContent, model, variables);
            }
            return $"<!-- Partial not found: {partialPath} -->";
        });

        // Handle page includes
        templateContent = Regex.Replace(templateContent, @"{{\s*pages\s+(\w+)\s*}}", match =>
        {
            var pageKey = match.Groups[1].Value;
            var pagePath = GetTomlValue(model, $"theme.pages.{pageKey}");
            if (pagePath != null && File.Exists(pagePath))
            {
                var pageContent = File.ReadAllText(pagePath);
                return ProcessTemplate(pageContent, model, variables);
            }
            return $"<!-- Page not found: {pagePath} -->";
        });

        // Handle block includes
        templateContent = Regex.Replace(templateContent, @"{{\s*blocks\s+(\w+)\s*}}", match =>
        {
            var blockKey = match.Groups[1].Value;
            var blockPath = GetTomlValue(model, $"theme.blocks.{blockKey}");
            if (blockPath != null && File.Exists(blockPath))
            {
                var blockContent = File.ReadAllText(blockPath);
                return ProcessTemplate(blockContent, model, variables);
            }
            return $"<!-- Block not found: {blockPath} -->";
        });

        // Handle special variables and TOML values
        templateContent = Regex.Replace(templateContent, @"{{\s*\$(\w+)\s*}}", match =>
        {
            var variableName = match.Groups[1].Value;
            switch (variableName)
            {
                case "copy":
                    return "&copy;";
                case "year":
                    return DateTime.Now.Year.ToString();
                case "reg":
                    return "&reg;";
                default:
                    return variables.ContainsKey(variableName) ? variables[variableName]?.ToString() ?? string.Empty : string.Empty;
            }
        });

        templateContent = Regex.Replace(templateContent, @"{{\s*\.(\w+(\.\w+)*)\s*}}", match =>
        {
            var variablePath = match.Groups[1].Value;
            return Utility.GetVariableValue(model, variablePath)?.ToString() ?? string.Empty;
        });

        // Handle if-elif-else conditions
        templateContent = Regex.Replace(templateContent, @"{{\s*if\s+\.(\w+(\.\w+)*)\s*}}(.*?){{\s*end\s*}}", match =>
        {
            var condition = match.Groups[1].Value;
            var content = match.Groups[3].Value;
            var elseMatch = Regex.Match(content, @"{{\s*else\s*}}");
            var elifMatch = Regex.Match(content, @"{{\s*elif\s+\.(\w+(\.\w+)*)\s*}}");

            if (Utility.IsVariableSet(model, condition))
            {
                if (elseMatch.Success)
                {
                    var trueContent = content.Substring(0, elseMatch.Index);
                    return ProcessTemplate(trueContent, model, variables);
                }
                return ProcessTemplate(content, model, variables);
            }
            else if (elifMatch.Success)
            {
                var elifCondition = elifMatch.Groups[1].Value;
                var elifContent = content.Substring(elifMatch.Index + elifMatch.Length);
                if (Utility.IsVariableSet(model, elifCondition))
                {
                    var elifTrueContent = elifContent.Substring(0, elifContent.IndexOf("{{ end }}", StringComparison.OrdinalIgnoreCase));
                    return ProcessTemplate(elifTrueContent, model, variables);
                }
                else
                {
                    var elifElseMatch = Regex.Match(elifContent, @"{{\s*else\s*}}");
                    if (elifElseMatch.Success)
                    {
                        var elifFalseContent = elifContent.Substring(elifElseMatch.Index + elifElseMatch.Length);
                        return ProcessTemplate(elifFalseContent, model, variables);
                    }
                    return string.Empty;
                }
            }
            else
            {
                if (elseMatch.Success)
                {
                    var falseContent = content.Substring(elseMatch.Index + elseMatch.Length);
                    return ProcessTemplate(falseContent, model, variables);
                }
                return string.Empty;
            }
        }, RegexOptions.Singleline);

        // Handle range blocks
        templateContent = Regex.Replace(templateContent, @"{{\s*range\s+\.(\w+(\.\w+)*)\s*}}(.*?){{\s*end\s*}}", match =>
        {
            var collectionPath = match.Groups[1].Value;
            var content = match.Groups[3].Value;
            var collection = Utility.GetVariableValue(model, collectionPath) as TomlArray;
            if (collection != null)
            {
                var result = string.Empty;
                foreach (var item in collection)
                {
                    if (item != null)
                    {
                        var itemModel = new TomlTable { [""] = item };
                        result += ProcessTemplate(content, itemModel, variables);
                    }
                }
                return result;
            }
            return string.Empty;
        }, RegexOptions.Singleline);

        // Handle content from Markdown
        templateContent = Regex.Replace(templateContent, @"{{\s*content\s*}}", match =>
        {
            var content = match.Groups[1].Value;
            // Convert Markdown to HTML (assuming you have a Markdown parser)
            return Markdown.ToHtml(content);
        });

        return templateContent;
    }

    private static string? GetTomlValue(TomlTable model, string key)
    {
        return Utility.GetVariableValue(model, key)?.ToString();
    }
}
