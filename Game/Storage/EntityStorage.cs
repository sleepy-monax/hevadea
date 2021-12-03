using Hevadea.Entities;
using Hevadea.Registry;
using System.Collections.Generic;

namespace Hevadea.Storage
{
    public class EntityStorage
    {
        public string Type { get; set; }

        public Dictionary<string, object> Data { get; set; }

        public EntityStorage(string type)
        {
            Type = type;
            Data = new Dictionary<string, object>();
        }

        public EntityStorage(string type, Dictionary<string, object> data)
        {
            Type = type;
            Data = data;
        }

        internal T ValueOf<T>(string name, T defaultValue)
        {
            return (T) (Data.ContainsKey(name) ? Data[name] : defaultValue);
        }

        public void Value<T>(string name, T value)
        {
            Data[name] = value;
        }

        public Entity ConstructEntity()
        {
            var entity = ENTITIES.Construct(Type);
            entity.Load(this);

            return entity;
        }
    }
}