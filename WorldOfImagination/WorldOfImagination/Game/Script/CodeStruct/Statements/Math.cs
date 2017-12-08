using System;

namespace WorldOfImagination.Game.Script.CodeStruct.Statements
{
    public enum MathOperator {Add, Sub, Mult, Divide, Modulo, Exp, Equal, NotEqual, BiggerThan, SmallerThan}
    
    public class Math : Statement
    {
        private Statement left;
        private Statement right;
        private MathOperator op;
        
        public override Value Evaluate(Scope scope)
        {
            switch (op)
            {
                case MathOperator.Add:     return right.Evaluate(scope) + left.Evaluate(scope);
                case MathOperator.Sub:     return right.Evaluate(scope) - left.Evaluate(scope);
                case MathOperator.Mult:    return right.Evaluate(scope) * left.Evaluate(scope);
                case MathOperator.Divide:  return right.Evaluate(scope) / left.Evaluate(scope);
                case MathOperator.Modulo:  return right.Evaluate(scope) % left.Evaluate(scope);
                case MathOperator.Exp:     return right.Evaluate(scope) ^ left.Evaluate(scope);
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