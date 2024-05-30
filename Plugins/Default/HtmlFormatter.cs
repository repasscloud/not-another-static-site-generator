using HtmlAgilityPack;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace NASSG.Plugins.Default;

public static class HtmlFormatter
{
    public static string FormatHtml(this string htmlContent)
    {
        var doc = new HtmlDocument();
        doc.LoadHtml(htmlContent);

        var stringBuilder = new StringBuilder();
        using (var writer = new StringWriter(stringBuilder))
        {
            FormatNode(doc.DocumentNode, writer, 0);
        }

        return stringBuilder.ToString();
    }

    private static void FormatNode(HtmlNode node, StringWriter writer, int indentLevel)
    {
        var indent = new string(' ', indentLevel * 2);

        switch (node.NodeType)
        {
            case HtmlNodeType.Document:
                foreach (var child in node.ChildNodes)
                {
                    FormatNode(child, writer, indentLevel);
                }
                break;
            case HtmlNodeType.Element:
                bool isInlineElement = IsInlineElement(node.Name);
                bool hasOnlyInlineChildren = node.ChildNodes.All(n => n.NodeType == HtmlNodeType.Text || IsInlineElement(n.Name));
                
                writer.Write($"{indent}<{node.Name}");

                foreach (var attribute in node.Attributes)
                {
                    writer.Write($" {attribute.Name}=\"{attribute.Value}\"");
                }

                if (node.HasChildNodes)
                {
                    writer.Write(">");

                    if (!isInlineElement && !hasOnlyInlineChildren)
                    {
                        writer.WriteLine();
                    }

                    foreach (var child in node.ChildNodes)
                    {
                        FormatNode(child, writer, hasOnlyInlineChildren ? 0 : indentLevel + 1);
                    }

                    if (!isInlineElement && !hasOnlyInlineChildren)
                    {
                        writer.Write(indent);
                    }

                    writer.WriteLine($"</{node.Name}>");
                }
                else
                {
                    writer.WriteLine(" />");
                }
                break;
            case HtmlNodeType.Text:
                var text = node.InnerText;
                if (!string.IsNullOrEmpty(text))
                {
                    if (text.Trim() != string.Empty)
                    {
                        text = text.Trim();
                        writer.Write($"{text}");
                    }
                }
                break;
            case HtmlNodeType.Comment:
                writer.WriteLine($"{indent}<!--{node.InnerText}-->");
                break;
        }
    }

    private static bool IsInlineElement(string tagName)
    {
        // List of common inline elements
        return new HashSet<string>
        {
            "a", "abbr", "acronym", "b", "bdo", "big", "br", "button", "cite", "code",
            "dfn", "em", "i", "img", "input", "kbd", "label", "map", "object", "output",
            "q", "samp", "script", "select", "small", "span", "strong", "sub", "sup",
            "textarea", "time", "tt", "var"
        }.Contains(tagName.ToLower());
    }
}
