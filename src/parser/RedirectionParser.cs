namespace CodeCrafters.Shell.Parser;

internal record RedirectionResult(string[] Args, string? StdoutTarget, string? StderrTarget);

internal static class RedirectionParser
{
    public static RedirectionResult Parse(string[] tokens)
    {
        string[] args = tokens;
        string? stdoutTarget = null;
        string? stderrTarget = null;

        for(int i = 0; i < tokens.Length; i++)
        {
            // TODO: If last token is > ie  "echo hello >" this should be a syntax error
            if(tokens[i] == ">" && i + 1 < tokens.Length)
            {
                var target = tokens[i + 1];

                if(i-1 >= 0 && tokens[i - 1] == "2")
                {
                    stderrTarget = target;
                    args = tokens[..(i-1)];
                }
                else if(i-1 >= 0 && tokens[i - 1] == "1")
                {
                    stdoutTarget = target;
                    args = tokens[..(i - 1)];
                }
                else
                {
                    args = tokens[..i];
                    stdoutTarget = tokens[i+1];
                }
                break;
            }
        }
        return new RedirectionResult(args, stdoutTarget, stderrTarget);
    }
}