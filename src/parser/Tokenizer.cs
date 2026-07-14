using System.Text;

namespace CodeCrafters.Shell.Parser;

public static class Tokenizer
{
    public static string[] Tokenize(string input)
    {
        var tokens = new List<string>();
        var currentToken = new StringBuilder();
        var insideQuotes = false;
        var insideDoubleQuotes = false;
        var escapeNext = false;

        foreach(var c in input)
        {
            if (escapeNext)
            {
                currentToken.Append(c);
                escapeNext = !escapeNext;
            }
            else switch (c)
            {
                case ' ' when insideDoubleQuotes:
                case ' ' when insideQuotes:
                    currentToken.Append(c);
                    break;
                case ' ':
                {
                    if (currentToken.Length > 0)
                    {
                        tokens.Add(currentToken.ToString());
                        currentToken.Clear();
                    }

                    break;
                }
                case '\'' when insideDoubleQuotes:
                    currentToken.Append(c);
                    break;
                case '\'':
                    insideQuotes = !insideQuotes;
                    break;
                case '\"' when insideQuotes:
                    currentToken.Append(c);
                    break;
                case '\"':
                    insideDoubleQuotes = !insideDoubleQuotes;
                    break;
                case '\\' when insideQuotes:
                    currentToken.Append(c);
                    break;
                case '\\':
                    escapeNext = true;
                    break;
                case '>' when !insideQuotes && !insideDoubleQuotes:
                {
                    if (currentToken.Length > 0)
                    {
                        tokens.Add(currentToken.ToString());
                        currentToken.Clear();
                    }

                    if (tokens.Count > 0 && tokens[^1] == ">")
                    {
                        tokens[^1] = ">>";
                    }
                    else
                    {
                        tokens.Add(">");
                    }

                    break;
                }
                default:
                    currentToken.Append(c);
                    break;
            }
        }

        if (currentToken.Length > 0)
        {
            tokens.Add(currentToken.ToString());
        }

        return tokens.ToArray();
    }
}