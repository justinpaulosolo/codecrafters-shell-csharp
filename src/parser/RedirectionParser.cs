namespace CodeCrafters.Shell.Parser;

internal record RedirectionResult(
    string[] Args,
    string? StdoutTarget,
    string? StderrTarget,
    bool StdoutAppend,
    bool StderrAppend);

internal static class RedirectionParser
{
    public static RedirectionResult Parse(string[] tokens)
    {
        string[] args = tokens;
        string? stdoutTarget = null;
        string? stderrTarget = null;
        bool stdoutAppend = false;
        bool stderrAppend = false;

        for(int i = 0; i < tokens.Length; i++)
        {
            bool isRedirect = tokens[i] == ">" || tokens[i] == ">>";

            if (!isRedirect) continue;

            if (i + 1 >= tokens.Length)
                break;

            bool append = tokens[i] == ">>";
            var target = tokens[i + 1];

            if (i - 1 >= 0 && tokens[i - 1] == "2")
            {
                stderrTarget = target;
                stderrAppend = append;
                args = tokens[..(i-1)];
            }
            else if (i - 1 >= 0 && tokens[i - 1] == "1")
            {
                stdoutTarget = target;
                stdoutAppend = append;
                args = tokens[..(i-1)];
            }
            else
            {
                stdoutTarget = target;
                stdoutAppend = append;
                args = tokens[..1];
            }

            break;
            }
        return new RedirectionResult(args,
                                    stdoutTarget,
                                    stderrTarget,
                                    stdoutAppend,
                                    stderrAppend);
    }
}