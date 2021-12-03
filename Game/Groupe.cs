using System.Collections.Generic;

namespace Hevadea
{
    public class Groupe<T>
    {
        public string Name { get; }
        public List<T> Members { get; set; } = new List<T>();

        public Groupe(string name)
        {
            Name = name;
        }

        public Groupe(string name, params T[] members)
        {
            Name = name;

            foreach (var m in members) Members.Add(m);
        }
    }
}