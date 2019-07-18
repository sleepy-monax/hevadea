using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace Hevadea.Framework.Data
{
    //- Outputs JSON structures from an object
    //- Really simple API (new List<int> { 1, 2, 3 }).ToJson() == "[1,2,3]"
    //- Will only output public fields and property getters on objects

    /// <summary>
    /// Really simple JSON writer from https://github.com/zanders3/json
    /// with aditional features hacked in.
    /// </summary>
    public static class JsonWriter
    {
        public static string ToJson(this object item)
        {
            var stringBuilder = new StringBuilder();
            AppendValue(stringBuilder, item);
            return stringBuilder.ToString();
        }

        private static void AppendValue(StringBuilder stringBuilder, object item)
        {
            if (item == null)
            {
                stringBuilder.Append("null");
                return;
            }

            var type = item.GetType();
            if (type == typeof(string))
            {
                stringBuilder.Append('"');
                var str = (string) item;
                for (var i = 0; i < str.Length; ++i)
                    switch (str[i])
                    {
                        case '\\':
                            stringBuilder.Append("\\\\");
                            break;

                        case '\"':
                            stringBuilder.Append("\\\"");
                            break;

                        case '\b':
                            stringBuilder.Append("\\b");
                            break;

                        case '\f':
                            stringBuilder.Append("\\f");
                            break;

                        case '\t':
                            stringBuilder.Append("\\t");
                            break;

                        case '\n':
                            stringBuilder.Append("\\n");
                            break;

                        case '\r':
                            stringBuilder.Append("\\r");
                            break;

                        case '\0':
                            stringBuilder.Append("\\0");
                            break;

                        default:
                            stringBuilder.Append(str[i]);
                            break;
                    }

                stringBuilder.Append('"');
            }
            else if (item is float f)
            {
                stringBuilder.Append(f.ToString(CultureInfo.InvariantCulture));
                stringBuilder.Append('f');
            }
            else if (item is double d)
            {
                stringBuilder.Append(d.ToString(CultureInfo.InvariantCulture));
                stringBuilder.Append('d');
            }
            else if (type == typeof(byte) || type == typeof(int) || type == typeof(long))
            {
                stringBuilder.Append(item.ToString());
            }
            else if (type == typeof(bool))
            {
                stringBuilder.Append((bool) item ? "true" : "false");
            }
            else if (item is IList list)
            {
                stringBuilder.Append('[');

                var isFirst = true;
                for (var i = 0; i < list.Count; i++)
                {
                    if (isFirst)
                        isFirst = false;
                    else
                        stringBuilder.Append(',');
                    AppendValue(stringBuilder, list[i]);
                }

                stringBuilder.Append(']');
            }
            else if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Dictionary<,>))
            {
                var keyType = type.GetGenericArguments()[0];

                //Refuse to output dictionary keys that aren't of type string
                if (keyType != typeof(string) && keyType != typeof(int))
                {
                    stringBuilder.Append("{}");
                    return;
                }

                stringBuilder.Append('{');
                var dict = item as IDictionary;
                var isFirst = true;

                foreach (var key in dict.Keys)
                {
                    if (isFirst)
                        isFirst = false;
                    else
                        stringBuilder.Append(',');

                    stringBuilder.Append('\"');
                    if (keyType == typeof(string))
                        stringBuilder.Append((string) key);
                    else
                        stringBuilder.Append(key.ToString());
                    stringBuilder.Append("\":");

                    AppendValue(stringBuilder, dict[key]);
                }

                stringBuilder.Append('}');
            }
            else
            {
                stringBuilder.Append('{');

                var isFirst = true;
                var fieldInfos = type.GetFields();
                for (var i = 0; i < fieldInfos.Length; i++)
                    if (fieldInfos[i].IsPublic && !fieldInfos[i].IsStatic)
                    {
                        var value = fieldInfos[i].GetValue(item);
                        if (value != null)
                        {
                            if (isFirst)
                                isFirst = false;
                            else
                                stringBuilder.Append(',');
                            stringBuilder.Append('\"');
                            stringBuilder.Append(fieldInfos[i].Name);
                            stringBuilder.Append("\":");
                            AppendValue(stringBuilder, value);
                        }
                    }

                var propertyInfo = type.GetProperties();
                for (var i = 0; i < propertyInfo.Length; i++)
                    if (propertyInfo[i].CanRead)
                    {
                        var value = propertyInfo[i].GetValue(item, null);
                        if (value != null)
                        {
                            if (isFirst)
                                isFirst = false;
                            else
                                stringBuilder.Append(',');
                            stringBuilder.Append('\"');
                            stringBuilder.Append(propertyInfo[i].Name);
                            stringBuilder.Append("\":");
                            AppendValue(stringBuilder, value);
                        }
                    }

                stringBuilder.Append('}');
            }
        }
    }
}