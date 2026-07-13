using CodeCrafters.Shell.State;

namespace CodeCrafters.Shell.Commands;

internal class ExternalCommand : Command
{
    private readonly string _name;
    private readonly string[] _args;
    public ExternalCommand(string name, string[] args)
    {
        _name = name;
        _args = args;

    }
    public override void Execute(ShellState state)
    {
        throw new NotImplementedException();
    }
}