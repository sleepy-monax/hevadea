using System.Collections.Generic;

namespace WorldOfImagination.Game.Script.CodeStruct
{
    public class Scope
    {
        public Dictionary<string, Value> Variables;

        public Scope()
        {
            Variables = new Dictionary<string, Value>();
        }
    }
}