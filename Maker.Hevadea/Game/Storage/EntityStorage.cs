using System.Collections.Generic;

namespace Maker.Hevadea.Game.Storage
{
    public class EntityStorage
    {
        public Dictionary<string, object> Data { get; set; }
        public string Type;

        public EntityStorage(string type)
        {
            Type = type;
            Data = new Dictionary<string, object>();
        }

        public T Get<T>(string dataName, T defaultValue)
        {
            if (Data.ContainsKey(dataName))
            {
                return (T)Data[dataName];
            }

            Data.Add(dataName, defaultValue);
            return defaultValue;
        }

        public void Set<T>(string dataName, T value)
        {
            Data[dataName] = value;
        }

    }
}
