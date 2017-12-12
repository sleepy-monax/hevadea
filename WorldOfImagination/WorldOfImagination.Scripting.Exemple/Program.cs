using System;
using System.Collections.Generic;
using System.IO;
using WorldOfImagination.Scripting.Compiler;
using WorldOfImagination.Json;

namespace WorldOfImagination.Scripting.Exemple
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            
            var tokens = new Lexer().Lexe(File.ReadAllText("exemple/SimpleTest.sc"));
            foreach (var t in tokens)
            {
                Console.WriteLine($"'{t.Content}' is {t.Type}");
            }
        }
    }
}