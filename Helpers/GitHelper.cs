using System.Diagnostics;

namespace NASSG.Helpers;
public class GitHelper
{
    private readonly string _repositoryPath;

    public GitHelper(string repositoryPath)
    {
        _repositoryPath = repositoryPath;
    }

    public string GetLatestCommitHash(string filePath)
    {
        return RunGitCommand($"log -n 1 --pretty=format:%H {filePath}");
    }

    public (string Hash, string AuthorName, string AuthorDate, string Subject) GetCommitDetails(string commitHash)
    {
        string result = RunGitCommand($"show -s --format=%H%n%an%n%ad%n%s {commitHash}");
        var lines = result.Split(new[] { '\n' }, StringSplitOptions.None);

        if (lines.Length < 4)
        {
            throw new InvalidOperationException("Unexpected result from git show command.");
        }

        return (lines[0], lines[1], lines[2], lines[3]);
    }

    private string RunGitCommand(string arguments)
    {
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = "git",
            Arguments = arguments,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true,
            WorkingDirectory = _repositoryPath
        };

        using (Process process = new Process { StartInfo = startInfo })
        {
            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            if (process.ExitCode != 0)
            {
                string error = process.StandardError.ReadToEnd();
                throw new InvalidOperationException($"Git command failed with error: {error}");
            }

            return output.Trim();
        }
    }
}
