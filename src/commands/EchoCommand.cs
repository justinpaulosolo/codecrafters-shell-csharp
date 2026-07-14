using CodeCrafters.Shell.State;

namespace CodeCrafters.Shell.Commands;

internal class EchoCommand(string[] args) : BuiltinCommand
{
    public override string CommandName => "echo";


    public override void Execute(ShellState _)
    {
        Console.WriteLine(string.Join(' ', args));
    }
}