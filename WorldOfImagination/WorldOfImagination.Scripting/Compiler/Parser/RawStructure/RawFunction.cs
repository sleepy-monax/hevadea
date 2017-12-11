using System.Collections.Generic;

namespace WorldOfImagination.Scripting.Compiler.Parser.RawStructure
{
    public class RawFunction
    {
        public RawFunction(string name, List<string> argsName, List<Token> code)
        {
            Name = name;
            ArgsName = argsName;
            Code = code;
        }

        public string Name;
        public List<string> ArgsName;
        public List<Token> Code;
       
    }

}