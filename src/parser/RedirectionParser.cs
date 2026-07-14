namespace CodeCrafters.Shell.Parser;

internal record RedirectionResult(string[] Args, string? Target);

internal static class RedirectionParser
{
    public static RedirectionResult Parse(string[] tokens)
    {
        string[] args = tokens;
        string? target = null;

        for(int i = 0; i < tokens.Length; i++)
        {
            // TODO: If last token is > ie  "echo hello >" this should be a syntax error
            if(tokens[i] == ">" && i + 1 < tokens.Length)
            {
                target = tokens[i+1];
                args = tokens[..i];
                break;
            }
        }
        return new RedirectionResult(args, target);
    }
}