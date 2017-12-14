using System;
using System.IO;
using Maker.Rise.Logic.Scripting.Compiler;
using Maker.Rise.Logic.Scripting.Runtime;

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

            var tokens = lexer.Tokens;

            foreach (var t in tokens)
            {
                Console.WriteLine($"{t.Type, 10}: '{t.Content}'");
            }
            
            Console.WriteLine("\n----- Building syntaxe tree -----");
            
            var tree = new Tree(tokens);
            tree.Build();
            var Token = tree.Root;
            ShowTree(Token, 0);


            Console.ReadKey();
        }

        static void ShowTree(Token root, int depth)
        {
            if (root.Content != "")
                Console.WriteLine(root.Content);
            foreach (var c in root.Childs)
            {
                for (int i = 0; i < depth; i++)
                {
                    Console.Write("   |");
                }
                ShowTree(c, depth + 1);
            } 
        }
    }
}