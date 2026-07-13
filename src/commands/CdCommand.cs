using CodeCrafters.Shell.State;

namespace CodeCrafters.Shell.Commands;

internal class CdCommand(string[] arg) : BuiltinCommand
{
    private readonly string[] _args = arg;
    public override string CommandName => "cd";

    public override void Execute(ShellState state)
    {
        if (_args.Length == 0)
        {
            return;
        }

        var target = _args[0];

        if (target == "~")
        {
            var home = System.Environment.GetEnvironmentVariable("HOME");

            if (home == null)
                return;

            Directory.SetCurrentDirectory(home);
        }
        else if (Directory.Exists(target))
        {
            Directory.SetCurrentDirectory(target);
        }
        else
        {
            Console.WriteLine($"cd: {target}: No such file or directory");
        }
    }
}