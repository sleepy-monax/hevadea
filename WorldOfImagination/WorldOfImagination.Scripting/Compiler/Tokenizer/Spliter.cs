using System.Collections.Generic;

namespace WorldOfImagination.Scripting.Compiler
{
    public static class Spliter
    {
        public static List<string> SplitToken(string str)
        {
            var tokens = new List<string>();
            var token = "";
            bool isString = false;
            foreach (var c in str)
            {
                if (c == ' ' && !isString)
                {
                    if (token != "")
                        tokens.Add(token);
                    token = "";
                }
                else if (c == '"' || ((c == '(' || c == ')' || c == '{' || c == '}' || c == ';' || c == ',') && ! isString))
                {
                    if (token != "")
                        tokens.Add(token);
                    
                    if (c == '"') isString = ! isString;
                    token = c.ToString();
                    
                    tokens.Add(token);
                    token = "";
                }
                else if (c != '\n' || isString)
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