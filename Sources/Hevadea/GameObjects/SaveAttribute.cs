using System;
using System.Reflection;

namespace Hevadea.GameObjects
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SavePropertyAttribute : Attribute
    {
        public string Name { get; private set; }
        public SavePropertyAttribute(string name)
        {
            Name = name;
        }
    }
    
    [AttributeUsage(AttributeTargets.Property)]
    public class SaveFieldAttribute : Attribute
    {
        public string Name { get; private set; }
        public SaveFieldAttribute(string name)
        {
            Name = name;
        }
    }
}