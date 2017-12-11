using System;
using WorldOfImagination.Scripting.Runtime;

namespace WorldOfImagination.Scripting.CodeStruct
{
    public class Function
    {
        public string Name;
        public Block Block;
        public string[] ArgsName;
        
        public Function(string name, Block block, string[] argsName)
        {
            Name = name;
            Block = block;
            ArgsName = argsName;
        }
        
        public Value Call(State state, params Value[] args)
        {
            var blockState = new State(state);

            if (ArgsName.Length == args.Length)
                for (var i = 0; i < args.Length; i++)
                {
                    blockState.SetVariable(ArgsName[i], args[i]);
                }
            else
            {
                throw new Exception($"Invalide argument count wen calling function {Name} with {args}!");
            }
            
            return Block.Evaluate(blockState);
        }
    }
}