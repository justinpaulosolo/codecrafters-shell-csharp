using System.Diagnostics;
using CodeCrafters.Shell.State;

namespace CodeCrafters.Shell.Commands;

internal class ExternalCommand(string name,
    string[] args,
    string? stdoutTarget,
    string? stderrTarget,
    bool stdoutAppend,
    bool stderrAppend) : Command
{
    public override void Execute(ShellState state)
    {
        var resolvedPath = PathResolver.Resolve(name);

        if (resolvedPath != null)
        {
            var startInfo = new ProcessStartInfo()
            {
                FileName = name,
                UseShellExecute = false,
            };

            if(stdoutTarget != null)
            {
                startInfo.RedirectStandardOutput = true;
            }

            if (stderrTarget != null)
            {
                startInfo.RedirectStandardError = true;
            }

            foreach(var arg in args)
            {
                startInfo.ArgumentList.Add(arg);
            }

            using var process = Process.Start(startInfo);

            var stdoutContent = stdoutTarget != null ? process?.StandardOutput.ReadToEnd() : null;
            var stderrContent = stderrTarget != null ? process?.StandardError.ReadToEnd() : null;

            if(stdoutTarget != null)
            {
                if (stdoutAppend)
                    File.AppendAllText(stdoutTarget, stdoutContent);
                else
                    File.WriteAllText(stdoutTarget, stdoutContent);
            }

            if(stderrTarget != null)
            {
                if (stderrAppend)
                    File.AppendAllText(stderrTarget, stderrContent);
                else
                    File.WriteAllText(stderrTarget, stderrContent);
            }

            process?.WaitForExit();
        }
        else
        {
            Console.WriteLine($"{name}: command not found");
        }
    }
}