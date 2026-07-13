using System.Text;

namespace CodeCrafters.Shell.Parser;

public static class Tokenizer
{
    public static string[] Tokenize(string input)
    {
        var tokens = new List<string>();
        var currentToken = new StringBuilder();
        bool insideQuotes = false;

        foreach(char c in input)
        {
            if (c == ' ')
            {
                if(insideQuotes)
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
                if (!insideQuotes)
                    insideQuotes = true;
                else
                {
                    if (currentToken.Length > 0)
                    {
                        tokens.Add(currentToken.ToString());
                        currentToken.Clear();
                        insideQuotes = false;
                    }
                }
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