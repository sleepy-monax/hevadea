using System;
using System.Collections.Generic;
using System.IO;
using WorldOfImagination.Scripting.Compiler.Parser.RawStructure;
using WorldOfImagination.Scripting.Runtime;

namespace WorldOfImagination.Scripting.Compiler
{
    public class Compiler
    {
        private readonly bool ShowDebugInfo;
        
        public Compiler(bool showDebugInfo = true)
        {
            ShowDebugInfo = showDebugInfo;
        }

        public State Compile(string code)
        {
            var tokens = SplitAndTokenize(code);
            CreateSyntaxeTree(tokens);

            return null;
        }

        private List<Token> SplitAndTokenize(string code)
        {
            var a = Spliter.SplitToken(code);
            var t = Tokenizer.TokenizePass1(a);
            var t1 = t.Clone();
            var t2 = Tokenizer.TokenizePass2(t);
            
            Console.WriteLine("===== TOKENS =====");
            if (ShowDebugInfo) for (var i = 0; i < t.Count; i++)
            {
                Console.WriteLine(t1[i].Type != t2[i].Type
                    ? $"'{a[i],16}' => {t1[i].Type,16} \t=> {t2[i].Type,16}"
                    : $"'{a[i],16}' => {t1[i].Type,16}");
            }

            return t2;
        }

        private void CreateSyntaxeTree(List<Token> tokens)
        {
            var functs = Parser.Parser.ParserPhase1(tokens);

            Console.WriteLine("===== FUNCTIONS =====");
            foreach (var f in functs)
            {
                Console.WriteLine(f.Name);

                foreach (var arg in f.ArgsName)
                {
                    Console.WriteLine("\t - " + arg);
                }
            }

            Console.WriteLine("===== STATEMENTS =====");
            Parser.Parser.ParserPhase2(functs, true);
        }
    }
}