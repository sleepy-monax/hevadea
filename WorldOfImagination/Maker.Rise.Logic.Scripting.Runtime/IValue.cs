using System.Collections.Generic;

namespace Maker.Rise.Logic.Scripting.Runtime
{
    public interface IValue
    {
        int IntVal();
        float FloatVal();
        bool BoolVal();
        char CharVal();
        string StringVal();
        
        IValue GetElement(int Index);
        IValue Count();
    }
}