using System;
using WorldOfImagination.Scripting.Runtime;

namespace WorldOfImagination.Scripting.CodeStruct.Statements
{
    public enum MathOperator {Add, Sub, Mult, Divide, Modulo, Exp, Equal, NotEqual, BiggerThan, SmallerThan}
    
    public class Math : Statement
    {
        private Statement left;
        private Statement right;
        private MathOperator op;
        
        public override Value Evaluate(State state)
        {
            switch (op)
            {
                case MathOperator.Add:     return right.Evaluate(state) + left.Evaluate(state);
                case MathOperator.Sub:     return right.Evaluate(state) - left.Evaluate(state);
                case MathOperator.Mult:    return right.Evaluate(state) * left.Evaluate(state);
                case MathOperator.Divide:  return right.Evaluate(state) / left.Evaluate(state);
                case MathOperator.Modulo:  return right.Evaluate(state) % left.Evaluate(state);
                case MathOperator.Exp:     return right.Evaluate(state) ^ left.Evaluate(state);
                case MathOperator.Equal:
                    break;
                case MathOperator.NotEqual:
                    break;
                case MathOperator.BiggerThan:
                    break;
                case MathOperator.SmallerThan:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            throw new Exception($"Unsuported '{op}' operator");
        }
    }
}