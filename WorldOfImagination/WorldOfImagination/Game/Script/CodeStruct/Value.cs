using System;
using System.IO;
using System.Windows.Forms;
using WorldOfImagination.Game.Script.CodeStruct.Statements;

namespace WorldOfImagination.Game.Script.CodeStruct
{
    public enum ValueType {_string, _int, _bool}
    
    public class Value
    {
        public ValueType MasterType;
        public readonly string StringValue;
        public readonly int IntValue;
        public readonly bool BoolValue;
        
        public Value(int v)
        {
            MasterType = ValueType._int;
            
            IntValue = v;
            BoolValue = v!=0;
            StringValue = v.ToString();
        }

        public Value(bool v)
        {
            MasterType = ValueType._bool;
            
            IntValue = v ? 1 : 0;
            BoolValue = v;
            StringValue = v ? "True" : "False";
        }

        public Value(string v)
        {
            MasterType = ValueType._string;
            
            IntValue = -1;
            BoolValue = false;
            StringValue = v;
        }
        
        public static Value operator + (Value left, Value right)
        {
            if (left.MasterType == ValueType._int && left.MasterType == ValueType._int)
                return  new Value(left.IntValue + right.IntValue);

            if (left.MasterType == ValueType._bool && left.MasterType == ValueType._bool)
                return  new Value(left.BoolValue || right.BoolValue);

            return  new Value(left.StringValue + right.StringValue);
        }

        public static Value operator - (Value left, Value right)
        {
            if (left.MasterType == ValueType._int && right.MasterType == ValueType._int )
                return new Value(left.IntValue - right.IntValue);

            throw new Exception($"Unsuported '-' operator between {left.MasterType} and {right.MasterType}.");
        }

        public static Value operator * (Value left, Value right)
        {
            if (left.MasterType == ValueType._int && right.MasterType == ValueType._int )
                return new Value(left.IntValue * right.IntValue);

            throw new Exception($"Unsuported '*' operator between {left.MasterType} and {right.MasterType}.");
        }
        
        public static Value operator / (Value left, Value right)
        {
            if (left.MasterType == ValueType._int && right.MasterType == ValueType._int )
                return new Value(left.IntValue / right.IntValue);

            throw new Exception($"Unsuported '/' operator between {left.MasterType} and {right.MasterType}.");
        }
        
        public static Value operator % (Value left, Value right)
        {
            if (left.MasterType == ValueType._int && right.MasterType == ValueType._int )
                return new Value(left.IntValue % right.IntValue);

            throw new Exception($"Unsuported '%' operator between {left.MasterType} and {right.MasterType}.");
        }
        
        public static Value operator ^ (Value left, Value right)
        {
            if (left.MasterType == ValueType._int && right.MasterType == ValueType._int )
                return new Value(left.IntValue ^ right.IntValue);

            throw new Exception($"Unsuported '^' operator between {left.MasterType} and {right.MasterType}.");
        }
    }
}