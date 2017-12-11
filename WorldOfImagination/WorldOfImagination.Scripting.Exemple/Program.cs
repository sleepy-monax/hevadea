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
            var compiler = new Compiler.Compiler(true);
            var program = compiler.Compile(File.ReadAllText("exemple/SimpleTest.sc"));
        }
    }
}