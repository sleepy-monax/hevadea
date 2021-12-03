using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

namespace Hevadea.Framework.Data
{
    /*Really simple JSON parser in ~300 lines
    - Attempts to parse JSON files with minimal GC allocation
    - Nice and simple "[1,2,3]".FromJson<List<int>>() API
    - Classes and structs can be parsed too!
         class Foo { public int Value; }
         "{\"Value\":10}".FromJson<Foo>()
    - Can parse JSON without type information into Dictionary<string,object> and List<object> e.g.
         "[1,2,3]".FromJson<object>().GetType() == typeof(List<object>)
         "{\"Value\":10}".FromJson<object>().GetType() == typeof(Dictionary<string,object>)
    - No JIT Emit support to support AOT compilation on iOS
    - Attempts are made to NOT throw an exception if the JSON is corrupted or invalid: returns null instead.
    - Only public fields and property setters on classes/structs will be written to

    Limitations:
    - No JIT Emit support to parse structures quickly
    - Limited to parsing <2GB JSON files (due to int.MaxValue)
    - Parsing of abstract classes or interfaces is NOT supported and will throw an exception.*/

    public static class JsonReader
    {
        private static Stack<List<string>> splitArrayPool = new Stack<List<string>>();
        private static StringBuilder stringBuilder = new StringBuilder();

        private static readonly Dictionary<Type, Dictionary<string, FieldInfo>> fieldInfoCache =
            new Dictionary<Type, Dictionary<string, FieldInfo>>();

        private static readonly Dictionary<Type, Dictionary<string, PropertyInfo>> propertyInfoCache =
            new Dictionary<Type, Dictionary<string, PropertyInfo>>();

        public static T FromJson<T>(this string json)
        {
            //Remove all whitespace not within strings to make parsing simpler
            stringBuilder.Length = 0;
            for (var i = 0; i < json.Length; i++)
            {
                var c = json[i];
                if (c == '\"')
                {
                    i = AppendUntilStringEnd(true, i, json);
                    continue;
                }

                if (char.IsWhiteSpace(c))
                    continue;

                stringBuilder.Append(c);
            }

            //Parse the thing!
            return (T) ParseValue(typeof(T), stringBuilder.ToString());
        }

        private static int AppendUntilStringEnd(bool appendEscapeCharacter, int startIdx, string json)
        {
            stringBuilder.Append(json[startIdx]);
            for (var i = startIdx + 1; i < json.Length; i++)
                if (json[i] == '\\')
                {
                    if (appendEscapeCharacter)
                        stringBuilder.Append(json[i]);
                    stringBuilder.Append(json[i + 1]);
                    i++; //Skip next character as it is escaped
                }
                else if (json[i] == '\"')
                {
                    stringBuilder.Append(json[i]);
                    return i;
                }
                else
                {
                    stringBuilder.Append(json[i]);
                }

            return json.Length - 1;
        }

        //Splits { <value>:<value>, <value>:<value> } and [ <value>, <value> ] into a list of <value> strings
        private static List<string> Split(string json)
        {
            var splitArray = splitArrayPool.Count > 0 ? splitArrayPool.Pop() : new List<string>();
            splitArray.Clear();
            if (json.Length == 2)
                return splitArray;
            var parseDepth = 0;
            stringBuilder.Length = 0;
            for (var i = 1; i < json.Length - 1; i++)
            {
                switch (json[i])
                {
                    case '[':
                    case '{':
                        parseDepth++;
                        break;

                    case ']':
                    case '}':
                        parseDepth--;
                        break;

                    case '\"':
                        i = AppendUntilStringEnd(true, i, json);
                        continue;
                    case ',':
                    case ':':
                        if (parseDepth == 0)
                        {
                            splitArray.Add(stringBuilder.ToString());
                            stringBuilder.Length = 0;
                            continue;
                        }

                        break;
                }

                stringBuilder.Append(json[i]);
            }

            splitArray.Add(stringBuilder.ToString());

            return splitArray;
        }

        internal static object ParseValue(Type type, string json)
        {
            if (type == typeof(string))
            {
                if (json.Length <= 2)
                    return string.Empty;
                var stringBuilder = new StringBuilder();
                for (var i = 1; i < json.Length - 1; ++i)
                    if (json[i] == '\\' && i + 1 < json.Length - 1)
                    {
                        switch (json[i + 1])
                        {
                            case '"':
                                stringBuilder.Append('"');
                                break;

                            case '\\':
                                stringBuilder.Append("\\");
                                break;

                            case 'b':
                                stringBuilder.Append("\b");
                                break;

                            case 'f':
                                stringBuilder.Append("\f");
                                break;

                            case 't':
                                stringBuilder.Append("\t");
                                break;

                            case 'n':
                                stringBuilder.Append("\n");
                                break;

                            case 'r':
                                stringBuilder.Append("\r");
                                break;

                            case '0':
                                stringBuilder.Append("\0");
                                break;

                            default:
                                stringBuilder.Append(json[i]);
                                break;
                        }

                        ++i;
                    }
                    else
                    {
                        stringBuilder.Append(json[i]);
                    }

                return stringBuilder.ToString();
            }

            if (type == typeof(int))
            {
                int.TryParse(json, out var result);
                return result;
            }

            if (type == typeof(byte))
            {
                byte.TryParse(json, out var result);
                return result;
            }

            if (type == typeof(float))
            {
                var result = float.Parse(json.Replace("f", ""), CultureInfo.InvariantCulture);
                return result;
            }

            if (type == typeof(double))
            {
                var result = double.Parse(json.Replace("d", ""), CultureInfo.InvariantCulture);
                return result;
            }

            if (type == typeof(bool)) return json.ToLower() == "true";

            if (json == "null") return null;

            if (type.IsArray)
            {
                var arrayType = type.GetElementType();
                if (json[0] != '[' || json[json.Length - 1] != ']')
                    return null;

                var elems = Split(json);
                var newArray = Array.CreateInstance(arrayType, elems.Count);
                for (var i = 0; i < elems.Count; i++)
                    newArray.SetValue(ParseValue(arrayType, elems[i]), i);
                splitArrayPool.Push(elems);
                return newArray;
            }

            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
            {
                var listType = type.GetGenericArguments()[0];
                if (json[0] != '[' || json[json.Length - 1] != ']')
                    return null;

                var elems = Split(json);
                var list = (IList) type.GetConstructor(new Type[] {typeof(int)}).Invoke(new object[] {elems.Count});
                for (var i = 0; i < elems.Count; i++)
                    list.Add(ParseValue(listType, elems[i]));
                splitArrayPool.Push(elems);
                return list;
            }

            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Dictionary<,>))
            {
                Type keyType, valueType;
                {
                    var args = type.GetGenericArguments();
                    keyType = args[0];
                    valueType = args[1];
                }

                //Refuse to parse dictionary keys that aren't of type string
                if (keyType != typeof(string))
                    return null;
                //Must be a valid dictionary element
                if (json[0] != '{' || json[json.Length - 1] != '}')
                    return null;
                //The list is split into key/value pairs only, this means the split must be divisible by 2 to be valid JSON
                var elems = Split(json);
                if (elems.Count % 2 != 0)
                    return null;

                var dictionary = (IDictionary) type.GetConstructor(new Type[] {typeof(int)})
                    .Invoke(new object[] {elems.Count / 2});
                for (var i = 0; i < elems.Count; i += 2)
                {
                    if (elems[i].Length <= 2)
                        continue;
                    var keyValue = elems[i].Substring(1, elems[i].Length - 2);
                    var val = ParseValue(valueType, elems[i + 1]);
                    dictionary.Add(keyValue, val);
                }

                return dictionary;
            }

            if (type == typeof(object)) return ParseAnonymousValue(json);

            if (json[0] == '{' && json[json.Length - 1] == '}') return ParseObject(type, json);

            return null;
        }

        private static object ParseAnonymousValue(string json)
        {
            if (json.Length == 0)
                return null;
            if (json[0] == '{' && json[json.Length - 1] == '}')
            {
                var elems = Split(json);
                if (elems.Count % 2 != 0)
                    return null;
                var dict = new Dictionary<string, object>(elems.Count / 2);
                for (var i = 0; i < elems.Count; i += 2)
                    dict.Add(elems[i].Substring(1, elems[i].Length - 2), ParseAnonymousValue(elems[i + 1]));
                return dict;
            }

            if (json[0] == '[' && json[json.Length - 1] == ']')
            {
                var items = Split(json);
                var finalList = new List<object>(items.Count);
                for (var i = 0; i < items.Count; i++)
                    finalList.Add(ParseAnonymousValue(items[i]));
                return finalList;
            }

            if (json[0] == '\"' && json[json.Length - 1] == '\"')
            {
                var str = json.Substring(1, json.Length - 2);
                return str.Replace("\\", string.Empty);
            }

            if (char.IsDigit(json[0]) || json[0] == '-')
            {
                if (json.EndsWith("f"))
                {
                    float.TryParse(json.Replace("f", ""), NumberStyles.Float, CultureInfo.InvariantCulture,
                        out var result);
                    return result;
                }
                else if (json.EndsWith("d"))
                {
                    double.TryParse(json.Replace("f", ""), NumberStyles.Float, CultureInfo.InvariantCulture,
                        out var result);
                    return result;
                }
                else
                {
                    int.TryParse(json, NumberStyles.Integer, CultureInfo.InvariantCulture, out var result);
                    return result;
                }
            }

            if (json == "true")
                return true;
            if (json == "false")
                return false;

            // handles json == "null" as well as invalid JSON
            return null;
        }

        private static object ParseObject(Type type, string json)
        {
            var instance = FormatterServices.GetUninitializedObject(type);

            //The list is split into key/value pairs only, this means the split must be divisible by 2 to be valid JSON
            var elems = Split(json);
            if (elems.Count % 2 != 0)
                return instance;

            if (!fieldInfoCache.TryGetValue(type, out var nameToField))
            {
                nameToField = type.GetFields().Where(field => field.IsPublic).ToDictionary(field => field.Name);
                fieldInfoCache.Add(type, nameToField);
            }

            if (!propertyInfoCache.TryGetValue(type, out var nameToProperty))
            {
                nameToProperty = type.GetProperties().ToDictionary(p => p.Name);
                propertyInfoCache.Add(type, nameToProperty);
            }

            for (var i = 0; i < elems.Count; i += 2)
            {
                if (elems[i].Length <= 2)
                    continue;
                var key = elems[i].Substring(1, elems[i].Length - 2);
                var value = elems[i + 1];

                if (nameToField.TryGetValue(key, out var fieldInfo))
                    fieldInfo.SetValue(instance, ParseValue(fieldInfo.FieldType, value));
                else if (nameToProperty.TryGetValue(key, out var propertyInfo))
                    propertyInfo.SetValue(instance, ParseValue(propertyInfo.PropertyType, value), null);
            }

            return instance;
        }
    }
}