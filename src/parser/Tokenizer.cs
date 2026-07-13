namespace CodeCrafters.Shell.Parser;

public static class Tokenizer
{
    public static string[] Tokenize(string input)
    {
        return input.Trim().Split(' ', System.StringSplitOptions.RemoveEmptyEntries);
    }
}