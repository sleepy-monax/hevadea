using System.Collections.Generic;
using System.Linq;

namespace Hevadea.Entities
{
    public partial class Entity
    {
        private readonly List<Component> _components = new List<Component>();

        public T AddComponent<T>(T component) where T : Component
        {
            if (_components.Any(e => e == component)) return null;

            _components.Add(component);
            component.Owner = this;

            _components.Sort((a, b) => (0xff - a.Priority).CompareTo(0xff - b.Priority));

            return component;
        }

        public T GetComponent<T>() where T : Component
        {
            for (int i = 0; i < _components.Count; i++)
            {
                if (_components[i] is T component)
                {
                    return component;
                }
            }

            return null;
        }

        public bool HasComponent<T>() where T : Component
        {
            return _components.OfType<T>().Any();
        }
    }
}