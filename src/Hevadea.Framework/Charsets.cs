namespace Hevadea.Framework
{
    public static class Charsets
    {
        public static char Backspace => '\b';
        public static readonly char[] Special = { '\a', '\b', '\n', '\r', '\f', '\t', '\v' };

        public static string Braces => "()[]<>{}";
        public static string Numeric => "0123456789";
        public static string Alpha => "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public static string AlphaNumeric => Alpha + Numeric;

        public static string ASCII => " !\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~";
    }
}