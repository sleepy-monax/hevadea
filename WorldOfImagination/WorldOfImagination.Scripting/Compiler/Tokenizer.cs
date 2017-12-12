using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldOfImagination.Scripting.Compiler
{
    public static class Tokenizer
    {
        public static List<string> Tokenize(string str)
        {
            var tokens = new List<string>();
            var token = "";
            bool isString = false;
            foreach (var c in str)
            {
                if ((c == ' ' || c == '\t') && !isString)
                {
                    if (token != "")
                        tokens.Add(token);
                    token = "";
                }
                else if (c == '"' || (c.IsSeparator() && !isString))
                {
                    if (token != "")
                        tokens.Add(token);

                    if (c == '"') isString = !isString;
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
