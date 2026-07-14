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
                if(insideQuotes)
                {
                    currentToken.Append(c);
                }
                else
                    insideDoubleQuotes = !insideDoubleQuotes;
            }
            else if (c == '\\')
            {
                if (insideQuotes)
                {
                    currentToken.Append(c);
                }
                else
                    escapeNext = true;
            }
            else if (c == '>' && !insideQuotes && !insideDoubleQuotes)
            {
                string op = ">";

                string pending = currentToken.ToString();

                if(pending is "1" or "2")
                {
                    op = pending + op;
                    currentToken.Clear();
                }
                else if (currentToken.Length > 0)
                {
                    tokens.Add(currentToken.ToString());
                    currentToken.Clear();
                }

                if(op is ">" && tokens.Count > 0 && tokens[^1] == ">" || tokens[^1] is "1" or "2")
                {
                    tokens[^1] += ">";
                }
                else
                {
                    tokens.Add(op);
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