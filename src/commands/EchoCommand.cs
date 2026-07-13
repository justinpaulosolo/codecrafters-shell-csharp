using CodeCrafters.Shell.State;

namespace CodeCrafters.Shell.Commands;

internal class EchoCommand(string[] args) : BuiltinCommand
{
    private readonly string[] _args = args;

    public override string CommandName => "echo";


    public override void Execute(ShellState _)
    {
        Console.WriteLine(string.Join(' ', _args));
    }
}