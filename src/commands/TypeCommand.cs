using CodeCrafters.Shell.State;

namespace CodeCrafters.Shell.Commands;

internal class TypeCommand(string[] args) : BuiltinCommand
{
    private static readonly string[] BuiltinNames = ["echo", "exit", "type", "pwd", "cd"];

    public override string CommandName => "type";

    public override void Execute(ShellState state)
    {
        var target = string.Join(' ' , args);

        if (BuiltinNames.Contains(target))
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