using System.Text.RegularExpressions;
using Tomlyn.Model;

namespace NASSG.Core;

public static class TemplateProcessor
{
    public static string ProcessTemplate(string templateContent, TomlTable model, Dictionary<string, object?> variables)
    {
        var variableRegex = new Regex(@"{{\s*\.(\w+(\.\w+)*)\s*}}", RegexOptions.Compiled);
        var ifRegex = new Regex(@"{{\s*if\s+isset\s+\.(\w+(\.\w+)*)\s*}}(.*?){{\s*end\s*}}", RegexOptions.Singleline | RegexOptions.Compiled);
        var elseRegex = new Regex(@"{{\s*else\s*}}", RegexOptions.Compiled);
        var rangeRegex = new Regex(@"{{\s*range\s+\.(\w+(\.\w+)*)\s*}}(.*?){{\s*end\s*}}", RegexOptions.Singleline | RegexOptions.Compiled);
        var includeRegex = new Regex(@"{{\s*include\s+""(.*?)""\s*}}", RegexOptions.Compiled);
        var conditionalIncludeRegex = new Regex(@"{{\s*include\s+""(.*?)""\s*if\s+isset\s+\.(\w+(\.\w+)*)\s*}}", RegexOptions.Compiled);
        var setRegex = new Regex(@"{{\s*set\s+\$(\w+)\s*=\s*(\.(\w+(\.\w+)*))\s*}}", RegexOptions.Compiled);

        // Handle includes
        templateContent = includeRegex.Replace(templateContent, match =>
        {
            var includePath = match.Groups[1].Value;
            if (File.Exists(includePath))
            {
                var includeContent = File.ReadAllText(includePath);
                return ProcessTemplate(includeContent, model, variables);
            }
            return $"<!-- Include not found: {includePath} -->";
        });

        // Handle conditional includes
        templateContent = conditionalIncludeRegex.Replace(templateContent, match =>
        {
            var includePath = match.Groups[1].Value;
            var condition = match.Groups[2].Value;
            if (Utility.IsVariableSet(model, condition) && File.Exists(includePath))
            {
                var includeContent = File.ReadAllText(includePath);
                return ProcessTemplate(includeContent, model, variables);
            }
            return string.Empty;
        });

        // Handle set variables
        templateContent = setRegex.Replace(templateContent, match =>
        {
            var variableName = match.Groups[1].Value;
            var variablePath = match.Groups[2].Value;
            variables[variableName] = Utility.GetVariableValue(model, variablePath);
            return string.Empty;
        });

        // Handle conditional blocks
        templateContent = ifRegex.Replace(templateContent, match =>
        {
            var condition = match.Groups[1].Value;
            var content = match.Groups[3].Value;

            if (Utility.IsVariableSet(model, condition))
            {
                var elseMatch = elseRegex.Match(content);
                if (elseMatch.Success)
                {
                    var trueContent = content.Substring(0, elseMatch.Index);
                    return ProcessTemplate(trueContent, model, variables);
                }
                return ProcessTemplate(content, model, variables);
            }
            else
            {
                var elseMatch = elseRegex.Match(content);
                if (elseMatch.Success)
                {
                    var falseContent = content.Substring(elseMatch.Index + elseMatch.Length);
                    return ProcessTemplate(falseContent, model, variables);
                }
                return string.Empty;
            }
        });

        // Handle range blocks
        templateContent = rangeRegex.Replace(templateContent, match =>
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
        });

        // Handle variable interpolation
        templateContent = variableRegex.Replace(templateContent, match =>
        {
            var variableName = match.Groups[1].Value;
            return Utility.GetVariableValue(model, variableName)?.ToString() ?? string.Empty;
        });

        return templateContent;
    }
}
