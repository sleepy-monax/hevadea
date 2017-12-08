using System;
using WorldOfImagination.Game.Script.CodeStruct;

namespace WorldOfImagination
{
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Console.WriteLine("---begin---");
            
            var test = Tokenizer.Tokenize("print(\"hello world :)\")\npomme(\"rouge\")\n\"1\n2\n3\n4\n5\"");
            foreach (var VARIABLE in test)
            {
                Console.WriteLine($"'{VARIABLE}'");
            }
            
            Console.WriteLine("--- end ---");
            
            return;
            using (var game = new WorldOfImaginationGame())
                game.Run();


        }
    }
}
