using System.Collections.Generic;

namespace WorldOfImagination.Game.Scripting
{
    public static class Tokenizer
    {
        public static List<string> Tokenize(string str)
        {
            var tokens = new List<string>();
            var token = "";
            bool is_string = false;
            foreach (var c in str)
            {
                if (c == ' ' && ! is_string)
                {
                    tokens.Add(token);
                    token = "";
                }
                if (c == '"' || ((c == '(' || c == ')' || c == '{' || c == '}') && ! is_string))
                {
                    if (token != "")
                        tokens.Add(token);
                    
                    if (c == '"') is_string = ! is_string;
                    token = c.ToString();
                    
                    tokens.Add(token);
                    token = "";
                }
                else if (c != '\n' || is_string)
                {
                    token += c;
                }
            }
            if (token != "")
                tokens.Add(token);
            return tokens;
        }
    }
}