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
        var args = tokens;
        string? stdoutTarget = null;
        string? stderrTarget = null;
        var stdoutAppend = false;
        var stderrAppend = false;

        for(var i = 0; i < tokens.Length; i++)
        {
            var isRedirect = tokens[i] == ">" || tokens[i] == ">>";

            if (!isRedirect) continue;

            if (i + 1 >= tokens.Length)
                break;

            var append = tokens[i] == ">>";
            var target = tokens[i + 1];

            switch (i - 1)
            {
                case >= 0 when tokens[i - 1] == "2":
                    stderrTarget = target;
                    stderrAppend = append;
                    args = tokens[..(i-1)];
                    break;
                case >= 0 when tokens[i - 1] == "1":
                    stdoutTarget = target;
                    stdoutAppend = append;
                    args = tokens[..(i-1)];
                    break;
                default:
                    stdoutTarget = target;
                    stdoutAppend = append;
                    args = tokens[..i];
                    break;
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