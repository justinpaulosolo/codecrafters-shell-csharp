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
    private readonly string _name = name;
    private readonly string[] _args = args;
    private readonly string? _stdoutTarget = stdoutTarget;
    private readonly string? _stderrTarget = stderrTarget;
    private readonly bool _stdoutAppend = stdoutAppend;
    private readonly bool _stderrAppend = stderrAppend;

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

            if (_stderrTarget != null)
            {
                startInfo.RedirectStandardError = true;
            }

            foreach(var arg in _args)
            {
                startInfo.ArgumentList.Add(arg);
            }

            using Process process = Process.Start(startInfo);

            string? stdoutContent = _stdoutTarget != null ? process.StandardOutput.ReadToEnd() : null;
            string? stderrContent = _stderrTarget != null ? process.StandardError.ReadToEnd() : null;

            if(_stdoutTarget != null)
            {
                if (_stdoutAppend)
                    File.AppendAllText(_stdoutTarget, stdoutContent);
                else
                    File.WriteAllText(_stdoutTarget, stdoutContent);
            }

            if(_stderrTarget != null)
            {
                if (_stderrAppend)
                    File.AppendAllText(_stderrTarget, stderrContent);
                else
                    File.WriteAllText(_stderrTarget, stderrContent);
            }

            process.WaitForExit();
        }
        else
        {
            Console.WriteLine($"{_name}: command not found");
        }
    }
}