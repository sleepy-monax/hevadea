using WorldOfImagination.Scripting.Runtime;

namespace WorldOfImagination.Scripting.CodeStruct.Statements
{ 
    public class VariableAssignment : Statement
    {
        private string VariableName;
        private Statement VariableValue;
        
        public override Value Evaluate(State state)
        {
            var val = VariableValue.Evaluate(state);
            state.SetVariable(VariableName, val);
            return val;
        }
    }
}