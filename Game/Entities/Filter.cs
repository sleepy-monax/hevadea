using System;
using System.Collections.Generic;

namespace Hevadea.Entities
{
    public class Filter
    {
        public readonly HashSet<Type> AnyType = new HashSet<Type>();
        public readonly HashSet<Type> AllType = new HashSet<Type>();
        public readonly HashSet<Type> NoneType = new HashSet<Type>();

        public Filter AnyOf(params Type[] includeAnyType)
        {
            if (includeAnyType == null) return this;

            foreach (var t in includeAnyType)
                if (!AnyType.Contains(t))
                    AnyType.Add(t);

            return this;
        }

        public Filter AllOf(params Type[] includeAllType)
        {
            if (includeAllType == null) return this;

            foreach (var t in includeAllType)
                if (!AllType.Contains(t))
                    AllType.Add(t);

            return this;
        }

        public Filter NoneOf(params Type[] excludeType)
        {
            if (excludeType == null) return this;

            foreach (var t in excludeType)
                if (!NoneType.Contains(t))
                    NoneType.Add(t);

            return this;
        }

        public void Reset()
        {
            AnyType.Clear();
            AllType.Clear();
            NoneType.Clear();
        }
    }
}