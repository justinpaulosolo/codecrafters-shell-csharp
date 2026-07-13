using System.Diagnostics;
using CodeCrafters.Shell.State;

namespace CodeCrafters.Shell.Commands;

internal class ExternalCommand(string name, string[] args) : Command
{
    private readonly string _name = name;
    private readonly string[] _args = args;

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

            foreach(var arg in _args)
            {
                startInfo.ArgumentList.Add(arg);
            }

            using Process process = Process.Start(startInfo);
            process.WaitForExit();
        }
        else
        {
            Console.WriteLine($"{_name}: command not found");
        }
    }
}