namespace CodeCrafters.Shell.Commands;

internal abstract class BuiltinCommand : Command
{
    public abstract string CommandName { get; }
}