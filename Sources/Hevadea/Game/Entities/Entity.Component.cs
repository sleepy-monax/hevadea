using Hevadea.Game.Entities.Components;
using System.Collections.Generic;
using System.Linq;

namespace Hevadea.Game.Entities
{
    public partial class Entity
    {
        private readonly List<EntityComponent> _components = new List<EntityComponent>();

        public void Attachs(params EntityComponent[] components)
        {
            foreach (var c in components) Attach(c);
        }

        public T Attach<T>(T component) where T : EntityComponent
        {
            if (_components.Any(e => e == component)) return null;

            _components.Add(component);
            component.AttachedEntity = this;

            _components.Sort((a, b) => (0xff - a.Priority).CompareTo(0xff - b.Priority));

            return component;
        }

        public T Get<T>() where T : EntityComponent
        {
            foreach (var e in _components)
                if (e is T component)
                    return component;

            return null;
        }

        public bool Has<T>() where T : EntityComponent
        {
            return _components.OfType<T>().Any();
        }
    }
}