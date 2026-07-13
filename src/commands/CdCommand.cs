using CodeCrafters.Shell.State;

namespace CodeCrafters.Shell.Commands;

internal class CdCommand(string arg) : BuiltinCommand
{
    private readonly string _arg = arg;
    public override string CommandName => "cd";

    public override void Execute(ShellState state)
    {
        if (Directory.Exists(_arg))
        {
            Directory.SetCurrentDirectory(_arg);
        }
    }
}