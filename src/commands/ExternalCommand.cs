using System.Diagnostics;
using CodeCrafters.Shell.Parser;
using CodeCrafters.Shell.State;

namespace CodeCrafters.Shell.Commands;

internal class ExternalCommand(string name, string[] args, string? stdoutTarget, string? stderrTarget) : Command
{
    private readonly string _name = name;
    private readonly string[] _args = args;
    private readonly string? _stdoutTarget = stdoutTarget;
    private readonly string? _stderrTarget = stderrTarget;

    public override void Execute(ShellState state)
    {
        var resolvedPath = PathResolver.Resolve(_name);

        if (resolvedPath != null)
        {
            var startInfo = new ProcessStartInfo()
            {
                FileName = _name,
                UseShellExecute = false,
            };

            if(_stdoutTarget != null)
            {
                startInfo.RedirectStandardOutput = true;
            }

            foreach(var arg in _args)
            {
                startInfo.ArgumentList.Add(arg);
            }

            using Process process = Process.Start(startInfo);

            if(_stdoutTarget != null)
            {
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
                File.WriteAllText(_stdoutTarget, output);
            }
            process.WaitForExit();
        }
        else
        {
            Console.WriteLine($"{_name}: command not found");
        }
    }
}