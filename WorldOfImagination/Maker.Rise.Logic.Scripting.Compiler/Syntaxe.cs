using System.Linq;

namespace Maker.Rise.Logic.Scripting.Compiler
{
    public static class Syntaxe
    {
        public static bool IsKeyword(this string str)
        {
            return KeyWords.Contains(str);
        }

        public static readonly string[] KeyWords =
        {
            "fun",
            "return",
            "end",

            "if",
            "else",
            "elseif",

            "for",
            "while",
            "do"
        };


        public static bool IsOperator(this string str)
        {
            return Operator.Contains(str);
        }

        public static readonly string[] Operator =
        {
            "^", // exposant
            "*", // multiplication
            "/", // Division
            "%", // modulo
            "+", // add
            "-", // minus
            "=", // equal
            "!=",// not equal
            ">", // Bigger
            "<", // Smaller
            ">=",// Bigger or equal
            "<=",// Smalller or equal 
            "and", // bool and
            "or",  // bool or
            "xor", // bool xor
            "->"   // Variable assignement
            
            /* NOTES: operator like min, max, sqrt, not... Will be buildin functions */
        };

        public static bool IsSeparator(this char c)
        {
            return Separator.Contains(c);
        }

        public static bool IsSeparator(this string c)
        {
            return Separator.Contains(c[0]);
        }
        
        public static bool IsOpenSeparator(this string c)
        {
            return OpenSeparator.Contains(c[0]);
        }
        
        public static bool IsCloseSeparator(this string c)
        {
            return CloseSeparator.Contains(c[0]);
        }

        public static readonly char[] Separator =
        {
            '{', '}', '[', ']', '(', ')', ';', ',',
        };
        
        public static readonly char[] OpenSeparator =
        {
            '{', '[', '('
        };
        
        public static readonly char[] CloseSeparator =
        {
            '}', ']', ')'
        };

        public static bool IsInt(this string str)
        {
            return int.TryParse(str, out int unused);
        }

        public static bool IsBool(this string str)
        {
            return str == "true" || str == "false";
        }
    }
}
