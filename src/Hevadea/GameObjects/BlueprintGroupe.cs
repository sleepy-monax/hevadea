using System.Collections.Generic;
using Hevadea.GameObjects.Entities;

namespace Hevadea.GameObjects
{
    public class BlueprintGroupe<T>
    {
        public string Name { get; }
        public List<T> Members { get; set; } = new List<T>();
        
        public BlueprintGroupe(string name)
        {
            Name = name;
        }
    }
}