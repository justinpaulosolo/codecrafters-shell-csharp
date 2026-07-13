using CodeCrafters.Shell.State;

namespace CodeCrafters.Shell.Commands;

internal class ExitCommand : BuiltinCommand
{
    public override string CommandName => "exit";

    public override void Execute(ShellState state)
    {
        state.ShouldExit = true;
    }

}