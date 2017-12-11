using WorldOfImagination.Scripting.Runtime;

namespace WorldOfImagination.Scripting.CodeStruct.Statements
{ 
    public class VariableAssignment : Statement
    {
        private string VariableName;
        private Statement Value;
        
        public override Value Evaluate(State state)
        {
            throw new System.NotImplementedException();
        }
    }
}