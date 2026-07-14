using System.Text;

namespace CodeCrafters.Shell.Parser;

public static class Tokenizer
{
    public static string[] Tokenize(string input)
    {
        var tokens = new List<string>();
        var currentToken = new StringBuilder();
        bool insideQuotes = false;
        bool insideDoubleQuotes = false;
        bool escapeNext = false;

        foreach(char c in input)
        {
            if (escapeNext)
            {
                currentToken.Append(c);
                escapeNext = !escapeNext;
            }
            else if (c == ' ')
            {
                if(insideDoubleQuotes)
                {
                    currentToken.Append(c);
                }
                else if(insideQuotes)
                {
                    currentToken.Append(c);
                }
                else
                {
                    if (currentToken.Length > 0)
                    {
                        tokens.Add(currentToken.ToString());
                        currentToken.Clear();
                    }
                }
            }
            else if (c == '\'')
            {
                if (insideDoubleQuotes)
                    currentToken.Append(c);
                else
                    insideQuotes = !insideQuotes;
            }
            else if(c == '\"')
            {
                insideDoubleQuotes = !insideDoubleQuotes;

            }
            else if (c == '\\')
            {
                if (insideQuotes)
                    currentToken.Append(c);
                else
                    escapeNext = !escapeNext;
            }
            else
            {
                currentToken.Append(c);
            }
        }

        if (currentToken.Length > 0)
        {
            tokens.Add(currentToken.ToString());
        }

        return tokens.ToArray();
    }
}