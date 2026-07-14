using CodeCrafters.Shell.Commands;

namespace CodeCrafters.Shell.Parser;

internal static class CommandParser
{
    public static ParsedCommand? ParseCommand(string input)
    {
        var tokens = Tokenizer.Tokenize(input);
        
        if (tokens.Length == 0) return null;

        var result = RedirectionParser.Parse(tokens);

        var name = result.Args[0];
        var args = result.Args[1..];

        Command command = name switch
        {
            "echo" => new EchoCommand(args),
            "type" => new TypeCommand(args),
            "cd" => new CdCommand(args),
            "pwd" => new PwdCommand(),
            "exit" => new ExitCommand(),
            _ => new ExternalCommand(name, args, result.Target)
        };
        return new ParsedCommand(command, result.Target);
    }
}
