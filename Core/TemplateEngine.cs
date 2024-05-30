using System.Collections.Generic;
using System.IO;
using Tomlyn.Model;

namespace NASSG.Core;
public class TemplateEngine
{
    private readonly TomlTable _config;
    private readonly Dictionary<string, object?> _variables;

    public TemplateEngine(TomlTable config)
    {
        _config = config;
        _variables = new Dictionary<string, object?>();
    }

    public string ProcessTemplate(string templatePath)
    {
        string templateContent = File.ReadAllText(templatePath);
        return TemplateProcessor.ProcessTemplate(templateContent, _config, _variables);
    }
}
