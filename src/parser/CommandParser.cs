using CodeCrafters.Shell.Commands;

namespace CodeCrafters.Shell.Parser;

internal static class CommandParser
{
    public static Command? ParseCommand(string input)
    {
        var tokens = Tokenizer.Tokenize(input);
        
        if (tokens.Length == 0) return null;

        var name = tokens[0];
        var args = tokens[1..];

        return name switch
        {
            "echo" => new EchoCommand(args),
            "type" => new TypeCommand(args),
            "pwd" => new PwdCommand(),
            "exit" => new ExitCommand(),
            _ => new ExternalCommand(name, args)
        };
    }
}
