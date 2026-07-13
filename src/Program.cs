using CodeCrafters.Shell.Commands;
using CodeCrafters.Shell.Parser;
using CodeCrafters.Shell.State;

class Program
{
    static void Main()
    {
        ShellState state = new();

        while (!state.ShouldExit)
        {
            PrintPrompt();

            var command = GetCommand();

            command?.Execute(state);
        }
    }

    public static void PrintPrompt()
    {
        Console.Write("$ ");
    }

    public static Command? GetCommand()
    {
        var line = Console.ReadLine();

        if (line == null)
        {
            return null;
        }

        return CommandParser.ParseCommand(line);
    }
}
