using System.Text;
using CodeCrafters.Shell.parser;
using CodeCrafters.Shell.Parser;
using CodeCrafters.Shell.State;

namespace CodeCrafters.Shell;

internal abstract class Program
{
    private static void Main()
    {
        ShellState state = new();

        while (!state.ShouldExit)
        {
            PrintPrompt();

            var parsed = GetCommand();

            if (parsed?.Command == null) continue;
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

    private static void PrintPrompt()
    {
        Console.Write("$ ");
    }

    private static ParsedCommand? GetCommand()
    {
        var buffer = new StringBuilder();
        while (true)
        {
            var keyInfo  = Console.ReadKey(true);
            if (keyInfo.Key == ConsoleKey.Enter)
            {
                Console.WriteLine();
                break;
            }
            else if (keyInfo.Key == ConsoleKey.Tab)
            {
                var current = buffer.ToString();

                if ("echo".StartsWith(current))
                {
                    var match = "echo"; // the one that matched
                    var missing = match.Substring(current.Length);
                    buffer.Append(current + missing);
                    Console.Write(buffer.ToString());
                }
            }
            else if (keyInfo.Key == ConsoleKey.Backspace && buffer.Length > 0)
            {
                buffer.Remove(buffer.Length - 1, 1);
            }
            else
            {
                buffer.Append(keyInfo.KeyChar);
                Console.Write(keyInfo.KeyChar);
            }
        }
        
        var line = Console.ReadLine();

        return line == null ? null : CommandParser.ParseCommand(line);
    }
}