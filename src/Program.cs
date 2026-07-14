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

            var parsed = GetCommand();

            if (parsed?.Command != null)
            {
                var originalOut = Console.Out;
                StreamWriter? writer = null;
                try
                {
                    if (parsed.StdoutTarget != null)
                    {
                        writer = new StreamWriter(parsed.StdoutTarget, append: parsed.StdoutAppend)
                        {
                            AutoFlush = true
                        };
                        Console.SetOut(writer);
                    }

                    if (parsed.StderrTarget != null)
                    {
                        writer = new StreamWriter(parsed.StderrTarget, append: parsed.StderrAppend)
                        {
                            AutoFlush = true
                        };
                        Console.SetError(writer);
                    }
                    parsed.Command.Execute(state);
                }
                finally
                {
                    Console.SetOut(originalOut);
                    writer?.Dispose();
                }
            }
        }
    }

    public static void PrintPrompt()
    {
        Console.Write("$ ");
    }

    public static ParsedCommand? GetCommand()
    {
        var line = Console.ReadLine();

        if (line == null)
        {
            return null;
        }

        return CommandParser.ParseCommand(line);
    }
}
