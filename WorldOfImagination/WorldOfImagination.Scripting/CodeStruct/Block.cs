using System.Collections.Generic;
using WorldOfImagination.Scripting.Runtime;

namespace WorldOfImagination.Scripting.CodeStruct
{
    public class Block
    {
        private List<Statement> Statements;

        public Block(List<Statement> statements)
        {
            Statements = statements;
        }

        public Value Evaluate(State state)
        {
            foreach (var s in Statements)
            {
                s.Evaluate(state);
            }

            return state.GetVariable("__return", false);
        }
    }
}