using Markdig;
using Tomlyn;
using Tomlyn.Model;
using NASSG.Plugins.Default;
using NASSG.Helpers;
using NASSG.Commands;

namespace NASSG;

class Program
{
    static void Main(string[] args)
    {
        // check args passed is correct or print help and exit
        if (args.Length == 0 || !CommandDispatcher.CheckCommand(args[0]))
        {
            string? unknownCommand = args.Length > 0 ? args[0] : null;
            unknownCommand.PrintHelp();
            return;
        }

        // this code needs to go into each Command function instead (where it's needed)
        // // check and dispatch flags
        // for (int i = 1; i < args.Length; i++)
        // {
        //     if (FlagDispatcher.CheckFlag(args[i]))
        //     {
        //         if (i + 1 < args.Length && !FlagDispatcher.CheckFlag(args[i + 1]))
        //         {
        //             FlagDispatcher.DispatchFlag(args[i], args[i + 1]);
        //             i++; // skip the next argument as it is used as a parameter for the current flag
        //         }
        //         else
        //         {
        //             FlagDispatcher.DispatchFlag(args[i], null);
        //         }
        //     }
        //     else
        //     {
        //         Console.WriteLine($"Unknown flag: {args[i]}");
        //     }
        // }

        CommandDispatcher.DispatchCommand(args[0], args.Skip(1).ToArray());


        Console.WriteLine(AppConfig.currentDirectory);


//         // // use githelper library to get commit info
//         // var gitHelper = new GitHelper(currentDirectory);
//         // string commitHash = gitHelper.GetLatestCommitHash("Program.cs");
//         // Console.WriteLine($"Commit Hash: {commitHash}");

//         // // Get the commit details using the commit hash
//         // var commitDetails = gitHelper.GetCommitDetails(commitHash);
//         // Console.WriteLine($"Hash: {commitDetails.Hash}");
//         // Console.WriteLine($"Author Name: {commitDetails.AuthorName}");
//         // Console.WriteLine($"Author Date: {commitDetails.AuthorDate}");
//         // Console.WriteLine($"Subject: {commitDetails.Subject}");

//         // path to config.toml file
//         var configPath = Path.Combine(currentDirectory, "config.toml");

//         // Read content of config.toml file
//         var tomlContent = File.ReadAllText(configPath);

//         // Parse TOML content into a TomlTable
//         var model = Toml.ToModel(tomlContent);

//         if (model.TryGetValue("theme", out var themeName) && themeName is string)
//         {
//             Console.WriteLine($"Theme used is: {themeName}");
//         }

//         // Access the build table
//         if (model.TryGetValue("build", out object buildTableObj) && buildTableObj is TomlTable buildTable)
//         {
//             // Access the minify value
//             if (buildTable.TryGetValue("minify", out object minifyValue) && minifyValue is bool minify)
//             {
//                 AppConfig.Minify = minify;
//                 Console.WriteLine($"minify: {AppConfig.Minify}");
//             }
//             else
//             {
//                 AppConfig.Minify = false;
//                 Console.WriteLine("minify key not found in the build section.");
//             }
//         }
//         else
//         {
//             Console.WriteLine("build section not found in the config.toml file.");
//         }

//         if (model.TryGetValue("params", out var paramsTable) && paramsTable is TomlTable)
//         {
//             var mainButtonURL = ((TomlTable)paramsTable)["mainButtonURL"]?.ToString();
//             Console.WriteLine($"mainButtonURL: {mainButtonURL}");
//         }
//         else
//         {
//             Console.WriteLine("params section not found in the config.toml file");
//         }

//         Console.WriteLine(model["theme"]);

//         string markdown = @"
// # Hello World

// This is a paragraph in **Markdown**.

// This is a really long piece of text<br /> and it's got a line break in it too!

// | Header 1 | Header 2 |
// | -------- | -------- |
// | Cell 1   | Cell 2   |
// ";

//         var pipeline = new MarkdownPipelineBuilder()
//             .UseAdvancedExtensions()
//             .Build();
        
//         var html = Markdown.ToHtml(markdown, pipeline);

//         if (AppConfig.Minify)
//         {
//             html = Minify.MinifyHtml(html);
//         }

//         html = html.FormatHtml();

//         File.WriteAllText("min.html", html);
    }
}
