using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using WorldOfImagination.Scripting.CodeStruct;

namespace WorldOfImagination.Scripting.Runtime
{
    public class State
    {
        private readonly Dictionary<string, Value>    _variables;
        private readonly Dictionary<string, Function> _functions;
        private readonly State                        _parentState;
        
        public State()
        {
            _variables   = new Dictionary<string, Value>();
            _functions   = new Dictionary<string, Function>();
            _parentState = null;
        }

        public State(State parentState) : this()
        {
            _parentState = parentState;
        }

        public void SetVariable(string name, Value value)
        {
            _variables.Add(name, value);
        }

        public Value GetVariable(string name, bool askParent = true)
        {
            if (_variables.ContainsKey(name))
            {
                return _variables[name];
            }


            if (_parentState != null && askParent)
            {
                return _parentState.GetVariable(name);
            }
            
            return new Value(0);
        }

        public void AddFunction(Function function)
        {
            
        }

        public Function GetFunction(string name)
        {
            if (_functions.ContainsKey(name))
            {
                return _functions[name];
            }


            if (_parentState != null)
            {
                return _parentState.GetFunction(name);
            }

            return null;
        }

        public State Copy()
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, this);
                ms.Position = 0;
                return (State)formatter.Deserialize(ms);
            }
        }
    }
}