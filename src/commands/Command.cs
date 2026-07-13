using CodeCrafters.Shell.State;

namespace CodeCrafters.Shell.Commands;

internal abstract class Command
{
    public abstract void Execute(ShellState state);
}