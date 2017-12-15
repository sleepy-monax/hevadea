using System.Collections.Generic;

namespace Maker.Rise.Logic.Scripting.Runtime
{
    public class Block
    {
        public List<IStatement> Statements;

        public Block()
        {
            Statements = new List<IStatement>();
        }

        public IValue Run(State state)
        {
            foreach (var s in Statements)
            {
                s.Evaluate(state);

                if (state.ExitBlock)
                {
                    break;
                }
            }
            
            return state.ExitValue;
        }
    }
}