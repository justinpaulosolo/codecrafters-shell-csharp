using CodeCrafters.Shell.State;

namespace CodeCrafters.Shell.Commands;

internal class PwdCommand() : BuiltinCommand
{
    public override string CommandName => "pwd";

    public override void Execute(ShellState _)
    {
        string pwd = System.IO.Directory.GetCurrentDirectory();
        Console.WriteLine($"{pwd}");
    }
}