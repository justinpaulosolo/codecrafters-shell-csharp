using CodeCrafters.Shell.State;

namespace CodeCrafters.Shell.Commands;

internal class TypeCommand : BuiltinCommand
{
    private static readonly string[] _builtinNames = { "echo", "exit", "type", "pwd", "cd" };
    private readonly string[] _args;

    public TypeCommand(string[] args)
    {
        _args = args;
    }

    public override string CommandName => "type";

    public override void Execute(ShellState state)
    {
        var target = string.Join(' ' , _args);

        if (_builtinNames.Contains(target))
        {
            Console.WriteLine($"{target} is a shell builtin");
            return;
        }
        
        var resolvedPath = PathResolver.Resolve(target);
        if (resolvedPath != null)
        {
            Console.WriteLine($"{target} is {resolvedPath}");
        }
        else
        {
            Console.WriteLine($"{target}: not found");
        }
    }
}