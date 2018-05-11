using System;
using System.Collections.Generic;
using Hevadea.Framework.Utils.Json;

namespace Hevadea.Storage
{
    public class DataCompound
    {
        Dictionary<string, object> _data;

        public DataCompound()
        {
			_data = new Dictionary<string, object>();
        }

		public string ToJson()
		{
			return _data.ToJson();
		}

		public DataCompound FromJson(string json)
		{
			_data = json.FromJson<Dictionary<string, object>>();
			return this;
		}

		public void Store(string key, object value)
		{
			_data.Add(key, value);
		}

		public object Get(string key)
		{
			if (_data.ContainsKey(key))
			{
				return _data[key];
			}

			return null;
		}

		public T GetAs<T>(string key)
		{
			T type = default(T);

			if (type is float)
			{
				
			}

			return default(T);
		}
    }
}
