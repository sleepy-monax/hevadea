using System;
using System.IO;
using WorldOfImagination.Scripting.Compiler;

namespace WorldOfImagination.Scripting.Exemple
{
    internal class Program
    {
        public static void Main(string[] args)
        {

            Console.WriteLine("\n----- RAW FILE CONTENT -----");
            var rawFileContent = File.ReadAllText("exemple/SimpleTest.sc");
            Console.WriteLine(rawFileContent);


            Console.WriteLine("\n----- PREPARED FILE CONTENT -----");
            var preparedFileContent = Utils.PrepareString(rawFileContent);
            Console.WriteLine(preparedFileContent);

            Console.WriteLine("\n----- Raw Token -----");
            var rawToken = Tokenizer.Tokenize(preparedFileContent);
            foreach (var t in rawToken)
            {
                Console.WriteLine($"'{t}'");
            }

            Console.WriteLine("\n----- Token -----");
            var lexer = new Lexer(rawToken);
            lexer.Lexe();

            var token = lexer.Tokens;

            foreach (var t in token)
            {
                Console.WriteLine($"{t.Type, 10}: '{t.Content}'");
            }
            Console.ReadKey();
        }
    }
}