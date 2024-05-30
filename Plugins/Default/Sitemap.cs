using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace NASSG.Plugins.Default;

public class SitemapUrl
{
    public string Loc { get; set; } = null!;
    public DateTime LastMod { get; set; }
    public string ChangeFreq { get; set; } = null!;
    public double Priority { get; set; }
}

public class SitemapGenerator
{
    public static void GenerateSitemap(List<SitemapUrl> urls, string outputPath)
    {
        XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";
        var sitemap = new XDocument(new XDeclaration("1.0", "utf-8", "yes"),
            new XElement(ns + "urlset",
                new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"),
                new XAttribute(XNamespace.Xmlns + "xsi:schemaLocation", "http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd"),
                from url in urls
                select new XElement(ns + "url",
                    new XElement(ns + "loc", url.Loc),
                    new XElement(ns + "lastmod", url.LastMod.ToString("yyyy-MM-dd")),
                    new XElement(ns + "changefreq", url.ChangeFreq),
                    new XElement(ns + "priority", url.Priority.ToString("F1"))
                )
            )
        );

        using (var writer = new StreamWriter(outputPath, false, Encoding.UTF8))
        {
            sitemap.Save(writer);
        }
    }
}